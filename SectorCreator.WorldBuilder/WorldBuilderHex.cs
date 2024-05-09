using SectorCreator.Global;
using SectorCreator.Global.Enums;
using SectorCreator.WorldBuilder.Enums;
using SectorCreator.WorldBuilder.Planet;
using SectorCreator.WorldBuilder.Planet.AsteroidBelt;
using SectorCreator.WorldBuilder.Planet.GasGiant;
using SectorCreator.WorldBuilder.Planet.Planet;
using SectorCreator.WorldBuilder.Planet.TerrestrialPlanet;
using SectorCreator.WorldBuilder.Star;

namespace SectorCreator.WorldBuilder;

public class WorldBuilderHex
{
    public Guid Id = Guid.NewGuid();
    // Orbits
    public double TotalAvailableOrbits => StarSystems.Select(x => x.Star).Sum(star => star == null ? 0 : star.GetTotalAvailableOrbits());
    public string Name => Stars.Count > 0 ? StarSystems.SelectMany(x => x.Planets).MaxBy(x => x.Importance).Name : "";
    public double Age => Stars.Count > 0 ? Stars.Max(x => x != null ? x.Age : 0) : 0;
    public Coordinates Coordinates { get; } = new();
    private int StarNum => StarSystems.Select(x => x.Star).Sum(star => star.CompanionStar != null ? 2 : 1);
    public List<WorldBuilderStar> Stars
    {
        get
        {
            var stars = new List<WorldBuilderStar>();
            if(MainSystem.Star != null) stars.Add(MainSystem.Star);
            if (MainSystem.Star != null && MainSystem.Star.CompanionStar != null) {
                stars.Add(MainSystem.Star.CompanionStar);
            }

            foreach (var star in SecondarySystems.Select(x => x.Star)) {
                stars.Add(star);
                if (star.CompanionStar != null) {
                    stars.Add(star.CompanionStar);
                }
            }

            return stars;
        }
    }
    public List<WorldBuilderPlanet> Planets
    {
        get
        {
            var planets = new List<WorldBuilderPlanet>(MainSystem.Planets);
            foreach (var starSystem in SecondarySystems) {
                planets.AddRange(starSystem.Planets);
            }

            return planets;
        }
    }
    public List<WorldBuilderStarSystem> StarSystems
    {
        get
        {
            var starSystems = new List<WorldBuilderStarSystem>();
            if (MainSystem != null) {
                starSystems.Add(MainSystem);
            }

            starSystems.AddRange(SecondarySystems);
            return starSystems;
        }
    }
    public WorldBuilderStarSystem MainSystem { get; set; } = new(new RollingService());
    public List<WorldBuilderStarSystem> SecondarySystems { get; set; } = new();
    public int GasGiantQuantity { get; set; }
    public int BeltQuantity { get; set; }
    public int TerrestrialPlanetQuantity { get; set; }
    public int EmptyOrbits { get; set; }
    public int TotalWorlds => GasGiantQuantity + BeltQuantity + TerrestrialPlanetQuantity;


    private readonly IRollingService _rollingService;

    public WorldBuilderHex()
    {
        _rollingService = new RollingService();
    }

    public WorldBuilderHex(IRollingService rollingService, Coordinates coordinates, bool generate = true)
    {
        Coordinates = coordinates;
        _rollingService = rollingService;
        if (generate && _rollingService.D6(1) >= 4) {
            GenerateStars();
            GeneratePlanets();
        }
    }

    private void GeneratePlanets()
    {
        GenerateGasGiantQuantity();
        GenerateBeltQuantity();
        GenerateTerrestrialPlanetQuantity();

        CalculateAvailableOrbits();
        CalculateHZCO();
        AllocatePlanetsForEachSystem();

        foreach (var starSystem in StarSystems) {
            var number = 1;
            var asteroidBeltNumber = 1;
            foreach (var planet in starSystem.Planets) {
                planet.GeneratePlanet(this, starSystem);
                planet.Primary = starSystem.Star.CompanionStar != null ? starSystem.Star.Component + "b" : starSystem.Star.Component;
                planet.GenerateObject(planet.PlanetType == PlanetType.AsteroidBelt ? asteroidBeltNumber++ : number++);
                planet.GenerateNotes(starSystem.HZCO);
                var moonNum = 0;
                foreach (var moon in planet.Moons) {
                    moon.Primary = planet.Object;
                    moon.Object = planet.Object + " " + (char)('a' + moonNum++);
                    moon.GenerateNotes(starSystem.HZCO);
                }
            }
        }

        ChooseMainWorld();
    }

    private void AllocatePlanetsForEachSystem()
    {
        GenerateNumberOfWorldsForEachSystem();
        CalculateBaselineNumbers();
        CalculateBaselineOrbit();

        GenerateEmptyOrbits();
        CalculateSystemSpreads();
        GenerateOrbits();

        AddAnomalousPlanets();
        DistributePlanets();
        CalculatePlanetEccentricities();
        CalculatePlanetOrbitalPeriods();
    }

    private void ChooseMainWorld()
    {
        // TODO
    }

    private void CalculatePlanetOrbitalPeriods()
    {
        foreach (var starSystem in StarSystems) {
            foreach (var planet in starSystem.Planets) {
                planet.CalculatePeriod(MainSystem.Mass);
            }
        }
    }

    private void CalculatePlanetEccentricities()
    {
        var mainSystemStars = MainSystem.Star.CompanionStar != null ? 2 : 1;
        foreach (var planet in MainSystem.Planets) {
            planet.CalculateEccentricity(mainSystemStars, Age);
        }

        foreach (var starSystem in SecondarySystems) {
            var starSystemStars = starSystem.Star.CompanionStar != null ? 2 : 1;
            foreach (var planet in starSystem.Planets) {
                planet.CalculateEccentricity(mainSystemStars + starSystemStars, starSystem.Age);
            }
        }
    }

    private void AddAnomalousPlanets()
    {
        foreach (var starSystem in StarSystems) {
            TerrestrialPlanetQuantity += starSystem.AddAnomalousPlanets();
        }

        if (TerrestrialPlanetQuantity > 13) {
            BeltQuantity += TerrestrialPlanetQuantity - 13;
            TerrestrialPlanetQuantity = 13;
        }
    }

    private void DistributePlanets()
    {
        DistributeEmptyOrbits();
        DistributeGasGiants();
        DistributeBelts();
        DistributeTerrestrialPlanets();
    }

    private void DistributeTerrestrialPlanets()
    {
        foreach (var starSystem in StarSystems) {
            for (var i = 0; i < starSystem.Planets.Count; i++) {
                if (starSystem.Planets[i].PlanetType == PlanetType.None) {
                    starSystem.Planets[i] = new WorldBuilderTerrestrialPlanet(starSystem.Planets[i], Coordinates);
                }
            }
        }
    }

    private void DistributeBelts()
    {
        while (Planets.Count(x => x.PlanetType == PlanetType.AsteroidBelt) < BeltQuantity) {
            var planetNum = _rollingService.D(Planets.Count, 1) - 1;
            var planet = Planets.ToArray()[planetNum];

            if (planet.PlanetType == PlanetType.None) {
                foreach (var starSystem in StarSystems) {
                    for (var i = 0; i < starSystem.Planets.Count; i++) {
                        if (Math.Abs(starSystem.Planets[i].OrbitNumber - planet.OrbitNumber) < .001) {
                            starSystem.Planets[i] = new WorldBuilderAsteroidBelt(planet, Coordinates);
                        }
                    }
                }
            }
        }
    }

    private void DistributeGasGiants()
    {
        while (Planets.Count(x => x.PlanetType == PlanetType.Jovian) < GasGiantQuantity) {
            var planetNum = _rollingService.D(Planets.Count, 1) - 1;
            var planet = Planets.ToArray()[planetNum];

            if (planet.PlanetType == PlanetType.None) {
                foreach (var starSystem in StarSystems) {
                    for (var i = 0; i < starSystem.Planets.Count; i++) {
                        if (Math.Abs(starSystem.Planets[i].OrbitNumber - planet.OrbitNumber) < .001) {
                            starSystem.Planets[i] = new WorldBuilderGasGiantPlanet(planet, Coordinates);
                        }
                    }
                }
            }
        }
    }

    private void DistributeEmptyOrbits()
    {
        EmptyOrbits -= MainSystem.RemovePlanetsForEmptyOrbits();

        foreach (var starSystem in SecondarySystems) {
            EmptyOrbits -= starSystem.RemovePlanetsForEmptyOrbits();
        }
    }

    private void GenerateOrbits()
    {
        foreach (var starSystem in StarSystems) {
            starSystem.GenerateOrbits();
        }
    }

    private void CalculateBaselineNumbers()
    {
        MainSystem.CalculateBaselineNumber(SecondarySystems.Count);

        foreach (var starSystem in SecondarySystems.Where(starSystem => starSystem.WorldNum > 0)) {
            starSystem.CalculateBaselineNumber(SecondarySystems.Count);
        }
    }

    private void CalculateSystemSpreads()
    {
        MainSystem.CalculateSpread();

        foreach (var starSystem in SecondarySystems.Where(starSystem => starSystem.WorldNum > 0)) {
            starSystem.CalculateSpread();
        }
    }

    private void GenerateEmptyOrbits()
    {
        EmptyOrbits = MainSystem.GenerateEmptyOrbits();

        foreach (var starSystem in SecondarySystems.Where(starSystem => starSystem.WorldNum > 0)) {
            EmptyOrbits += starSystem.GenerateEmptyOrbits();
        }
    }

    private void CalculateBaselineOrbit()
    {
        MainSystem.CalculateBaselineOrbit();

        foreach (var starSystem in SecondarySystems.Where(starSystem => starSystem.WorldNum > 0)) {
            starSystem.CalculateBaselineOrbit();
        }
    }

    private void GenerateNumberOfWorldsForEachSystem()
    {
        var remainingWorlds = TotalWorlds;

        MainSystem.WorldNum = (int) Math.Ceiling(TotalWorlds * MainSystem.Star.GetTotalAvailableOrbits() / TotalAvailableOrbits);
        remainingWorlds -= MainSystem.WorldNum;

        foreach (var starSystem in SecondarySystems) {
            starSystem.WorldNum = (int) Math.Floor(TotalWorlds * starSystem.Star.GetTotalAvailableOrbits() / TotalAvailableOrbits);
            remainingWorlds -= starSystem.WorldNum;
        }

        if (remainingWorlds != 0) {
            StarSystems.Last().WorldNum += remainingWorlds;
        }
    }

    private void CalculateHZCO()
    {
        foreach (var star in Stars) {
            star.CaculateHZCO();
        }
    }

    private void CalculateAvailableOrbits()
    {
        foreach (var star in Stars) {
            star.SetMinimumOrbit();
            star.SetMidOrbits(SecondarySystems.Select(x => x.Star).ToList());
            star.SetMaximumOrbit(SecondarySystems.Select(x => x.Star).ToList());
        }
    }

    public void GenerateTerrestrialPlanetQuantity()
    {
        TerrestrialPlanetQuantity = GetTerrestrialPlanetDM(_rollingService.D6(2) - 2);
    }

    private int GetTerrestrialPlanetDM(int roll)
    {
        roll -= Stars.Where(star => star.IsPostStellarObject()).ToList().Count;

        if (roll < 3) {
            roll = _rollingService.D3(1) + 2;
        } else {
            roll += _rollingService.D3(1) - 1;
        }

        return roll;
    }

    public void GenerateBeltQuantity()
    {
        if (_rollingService.D6(2) >= 8) {
            BeltQuantity = (_rollingService.D6(2) + GetBeltDM()) switch {
                <= 6 => 1,
                <= 11 => 2,
                >= 12 => 3
            };
        }
    }

    private int GetBeltDM()
    {
        var dm = 0;

        if (GasGiantQuantity > 0) dm++;
        if (MainSystem.Star.SpecialType == StarSpecialType.Protostar) dm += 3;
        if (MainSystem.Star.IsPostStellarObject()) dm++;
        foreach (var star in SecondarySystems.Select(x => x.Star).Where(star => star.IsPostStellarObject())) {
            dm--;
            if (star.CompanionStar != null) {
                dm--;
            }
        }

        if (GetNumStars() >= 2) dm++;

        return dm;
    }

    public void GenerateGasGiantQuantity()
    {
        if (_rollingService.D6(2) <= 9) {
            GasGiantQuantity = (_rollingService.D6(2) + GetGasGiantDM()) switch {
                <= 4 => 1,
                <= 6 => 2,
                <= 8 => 3,
                <= 11 => 4,
                <= 12 => 5,
                >= 13 => 6
            };
        }
    }

    private int GetGasGiantDM()
    {
        var dm = 0;

        if (MainSystem.Star.LuminosityClass == LuminosityClass.V
            && MainSystem.Star.CompanionStar == null
            && SecondarySystems.Select(x => x.Star).ToList().Count == 0) dm++;
        if (MainSystem.Star.SpectralType == SpectralType.BD) dm -= 2;
        if (MainSystem.Star.IsPostStellarObject()) dm -= 2;
        foreach (var star in SecondarySystems.Select(x => x.Star).Where(star => star.IsPostStellarObject())) {
            dm--;
            if (star.CompanionStar != null) {
                dm--;
            }
        }

        if (GetNumStars() >= 4) dm--;

        return dm;
    }

    private int GetNumStars()
    {
        var numStars = 1;
        if (MainSystem.Star.CompanionStar != null) numStars++;
        foreach (var star in SecondarySystems.Select(x => x.Star)) {
            numStars++;
            if (star.CompanionStar != null) numStars++;
        }

        return numStars;
    }

    private void GenerateStars()
    {
        MainSystem.Star = new WorldBuilderStar(new RollingService());

        var modifier = GetMultiStarModifier();

        var currentComponent = 'B';

        if (_rollingService.D6(2) + modifier >= 10 &&
            MainSystem.Star.LuminosityClass is not LuminosityClass.Ia and not LuminosityClass.Ib and not LuminosityClass.II
                and not LuminosityClass.III) {
            AddNewStarSystem(StarType.Close, currentComponent);
            currentComponent++;
        }

        if (_rollingService.D6(2) + modifier >= 10) {
            AddNewStarSystem(StarType.Near, currentComponent);
            currentComponent++;
        }

        if (_rollingService.D6(2) + modifier >= 10) {
            AddNewStarSystem(StarType.Far, currentComponent);
        }
    }

    private void AddNewStarSystem(StarType starType, char currentComponent)
    {
        WorldBuilderStarSystem starSystem;
        do {
            starSystem = new WorldBuilderStarSystem(_rollingService) {
                Star = new WorldBuilderStar(new RollingService(), starType, StarSystems.Sum(x => x.Mass), currentComponent)
            };
        } while (starSystem.Star.Mass > MainSystem.Star.Mass);

        SecondarySystems.Add(starSystem);
    }

    private int GetMultiStarModifier()
    {
        var modifier = 0;
        if (MainSystem.Star.LuminosityClass is LuminosityClass.Ia or LuminosityClass.Ib or LuminosityClass.II or LuminosityClass.III
            or LuminosityClass.IV) {
            modifier++;
        }

        if (MainSystem.Star.LuminosityClass is LuminosityClass.V or LuminosityClass.VI
            && MainSystem.Star.SpectralType is SpectralType.O or SpectralType.B or SpectralType.A or SpectralType.F) {
            modifier++;
        }

        if (MainSystem.Star.LuminosityClass is LuminosityClass.V or LuminosityClass.VI
            && MainSystem.Star.SpectralType is SpectralType.M) {
            modifier--;
        }

        if (MainSystem.Star.SpectralType is SpectralType.BD or SpectralType.D
            || MainSystem.Star.LuminosityClass is LuminosityClass.BD or LuminosityClass.D) {
            modifier--;
        }

        if (MainSystem.Star.SpecialType is StarSpecialType.Pulsar or StarSpecialType.NeutronStar or StarSpecialType.BlackHole) {
            modifier--;
        }

        return modifier;
    }

    public string GetStarDetails()
    {
        var starString = "";
        if (TotalWorlds > 0) {
            starString = "System Age\tStellar\tGas Giants\tPlanetoid Belts\tTerrestrials\tClass III Status?\n";
            starString += $"{Age:N3}\t{StarNum}\t{GasGiantQuantity}\t{BeltQuantity}\t{TerrestrialPlanetQuantity}\tYes\n";

            starString += "Stars\tComponent\tClass\tMass\tTemp\tDiameter\tLuminosity\tOrbit\tAU\tEcc\tPeriod\tMAO\tHZCO\n";

            var doneStarSystems = new List<WorldBuilderStarSystem>();

            foreach (var starSystem in StarSystems) {
                starString += GetStarString(starSystem.Star, doneStarSystems.Sum(x => x.Mass));
                if (starSystem.Star.CompanionStar != null) {
                    starString += GetStarString(starSystem.Star.CompanionStar, doneStarSystems.Sum(x => x.Mass));
                }

                starString += starSystem.Star.CompanionStar != null
                    ? GetStarSystemString(starSystem, 0)
                    : "";

                doneStarSystems.Add(starSystem);

                if (starSystem.Star.StarType != StarType.Primary) {
                    starString += GetStarSystemsString(doneStarSystems);
                }
            }
        }

        return starString;
    }

    private string GetStarSystemsString(List<WorldBuilderStarSystem> doneStarSystems)
    {
        var component = doneStarSystems.Aggregate("", (current, starSystem) => current + starSystem.Component.First());

        var distance = doneStarSystems.Select(x => x.Star).Max(x => x.OrbitNumber);
        var mass = doneStarSystems.Sum(x => x.Mass);
        var companionMass = doneStarSystems.Last().Star.Mass;

        return
            $"\t{component}\t---\t{mass:N3}\t---\t---\t{doneStarSystems.Sum(x => x.Luminosity):N3}\t{distance:N2}\t{doneStarSystems.Select(x => x.Star).Max(x => x.OrbitDistance):N3}\t{doneStarSystems.Select(x => x.Star).Max(x => x.Eccentricity):N2}\t{doneStarSystems.Select(x => x.Star).Max(x => x.Period):N2}\t{MainSystem.Star.AvailableOrbits.FirstOrDefault(x => x > distance):N2}\t---\n";
    }

    private string GetStarSystemString(WorldBuilderStarSystem starSystem, double totalMass)
    {
        return
            $"\t{starSystem.Component + $" ({starSystem.Star.Component.First()})"}\t---\t{starSystem.Mass:N3}\t---\t---\t{starSystem.Luminosity:N3}\t{starSystem.OrbitNumber:N2}\t{starSystem.OrbitDistance:N3}\t{starSystem.Eccentricity:N2}\t{starSystem.Period:N3}y\t{starSystem.MAO:N2}\t{starSystem.HZCO:N2}\n";
    }

    private string GetStarString(WorldBuilderStar star, double totalMass)
    {
        return
            $"{star.Name}\t{star.Component}\t{star.Class}\t{star.Mass:N3}\t{star.Temperature:N0}\t{star.Diameter:N3}\t{star.Luminosity:N3}\t{(star.StarType == StarType.Primary ? "" : $"{star.OrbitNumber:N2}")}\t{(star.StarType == StarType.Primary ? "" : $"{star.OrbitDistance:N3}")}\t{(star.StarType == StarType.Primary ? "" : $"{star.Eccentricity:N2}")}\t{(star.StarType == StarType.Primary ? "" : $"{star.Period:N3}y")}\t{(star.CompanionStar == null && star.StarType != StarType.Companion ? $"{star.MAO:N2}" : "---")}\t{(star.CompanionStar == null && star.StarType != StarType.Companion ? star.HZCO : "---")}\n";
    }

    public string GetPlanetDetails()
    {
        var planetString = "OBJECTS\tPrimary\tObject\tOrbit#\tAU\tEcc\tPeriod\tSAH/UWP\tSub\tNotes\n";
        planetString += StarSystems.Aggregate("",
            (current1, starSystem) => starSystem.Planets.Aggregate(current1, (current, planet) => current + GetPlanetString(planet, starSystem)));
        return planetString;
    }

    private static string GetPlanetString(WorldBuilderPlanet planet, WorldBuilderStarSystem starSystem)
    {
        return
            $"{planet.Name}\t{planet.Primary}\t{planet.Object}\t{planet.OrbitNumber:N2}{(planet.Anomaly == PlanetAnomaly.Retrograde ? "R" : "")}\t{planet.OrbitDistance:N2}\t{(planet.PlanetType != PlanetType.AsteroidBelt ? $"{planet.Eccentricity:N2}" : "n/a")}\t{planet.Period:N3}\t{planet.SAH}\t{planet.Sub}\t{planet.Notes}\n";
    }

    private static int GetObjectNum(WorldBuilderPlanet planet, WorldBuilderStarSystem starSystem)
    {
        if (planet.PlanetType != PlanetType.AsteroidBelt) {
            return starSystem.Planets.Count(x =>
                x.OrbitNumber < planet.OrbitNumber && x.PlanetType != PlanetType.AsteroidBelt && x.Primary == planet.Primary) + 1;
        }

        return starSystem.Planets.Count(x =>
            x.OrbitNumber < planet.OrbitNumber && x.PlanetType == PlanetType.AsteroidBelt && x.Primary == planet.Primary) + 1;
    }
}