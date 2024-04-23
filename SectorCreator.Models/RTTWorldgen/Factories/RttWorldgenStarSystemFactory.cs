using SectorCreator.Global;
using SectorCreator.Global.Enums;
using SectorCreator.Models.Basic;
using SectorCreator.Models.CustomTypes;
using SectorCreator.Models.Services.TradeCodeService;

namespace SectorCreator.Models.RTTWorldgen.Factories;

public interface IRttWorldgenStarSystemFactory
{
    RttWorldgenStarSystem Generate(StarSystemType starSystemType, Coordinates coordinates);
    RttWorldgenStarSystem Generate(RttWorldgenStar star, Coordinates coordinates);
    RttWorldgenStarSystem GenerateFromFile(string filename);
}

public class RttWorldgenStarSystemFactory : IRttWorldgenStarSystemFactory
{
    private readonly IRollingService _rollingService;
    private readonly IRttWorldgenStarFactory _rttWorldgenStarFactory;
    private readonly IRttWorldgenPlanetFactory _rttWorldgenPlanetFactory;
    private RttWorldgenStarSystem _starSystem = new();

    public RttWorldgenStarSystemFactory(IRollingService rollingService,
        IRttWorldgenStarFactory rttWorldgenStarFactory,
        IRttWorldgenPlanetFactory rttWorldgenPlanetFactory)
    {
        _rollingService = rollingService;
        _rttWorldgenStarFactory = rttWorldgenStarFactory;
        _rttWorldgenPlanetFactory = rttWorldgenPlanetFactory;
    }

    public RttWorldgenStarSystem Generate(StarSystemType starSystemType, Coordinates coordinates)
    {
        _starSystem = new RttWorldgenStarSystem {
            Coordinates = coordinates
        };

        if (starSystemType == StarSystemType.BrownDwarf) {
            AddBrownDwarfStarToSystem();
        } else {
            AddStarsToSystem();
            ModifyStars();
        }

        AddPlanetsToSystem();
        GeneratePBGAndEconomicExtensionForAllPlanets();

        return _starSystem;
    }

    private void ModifyStars()
    {
        _starSystem.Age = _rollingService.D6(3) - 3;
        _starSystem.PrimaryStar = _rttWorldgenStarFactory.ModifyStar((RttWorldgenStar) _starSystem.PrimaryStar, _starSystem.Age);

        for(var i = 0; i < _starSystem.CompanionStars.Count; i++) {
            _starSystem.CompanionStars[i] = _rttWorldgenStarFactory.ModifyStar((RttWorldgenStar) _starSystem.CompanionStars[i], _starSystem.Age);
        }
    }

    private void GeneratePBGAndEconomicExtensionForAllPlanets()
    {
        var belts = _starSystem.Planets.Count(x => x.PlanetType == PlanetType.AsteroidBelt);
        var giants = _starSystem.Planets.Count(x => x.PlanetType == PlanetType.Jovian);

        foreach (RttWorldgenPlanet planet in _starSystem.Planets) {
            GeneratePBGForPlanet(planet, belts, giants);
            planet.GenerateEconomicExtension(giants, belts);
            foreach (RttWorldgenPlanet satellite in planet.Satellites) {
                GeneratePBGForPlanet(satellite, belts, giants);
                satellite.GenerateEconomicExtension(giants, belts);
            }
        }
    }

    private void GeneratePBGForPlanet(RttWorldgenPlanet planet, int belts, int giants)
    {
        var populationModifier = planet.Population >= 1 ? _rollingService.D(9, 1) : 0;
        planet.PBG = $"{ExtendedHex.values[populationModifier]}{ExtendedHex.values[belts]}{ExtendedHex.values[giants]}";
    }

    public RttWorldgenStarSystem Generate(RttWorldgenStar star, Coordinates coordinates)
    {
        _starSystem = new RttWorldgenStarSystem {
            PrimaryStar = star,
            Coordinates = coordinates
        };
        AddPlanetsToSystem();
        GeneratePBGAndEconomicExtensionForAllPlanets();

        return _starSystem;
    }

    public RttWorldgenStarSystem GenerateFromFile(string filename)
    {
        _starSystem = new RttWorldgenStarSystem();
        GetStarData(File.ReadLines(filename).First().Split('\t')[14]);

        var planets = File.ReadAllLines(filename)
            .Select(line => line.Split('\t'))
            .Select(x => new RttWorldgenPlanet(new RollingService()) {
                IsMainWorld = x[0] == "Planet",
                Coordinates = GetCoordinatesFromString(x[1]),
                Name = x[2],
                Size = GetSizeFromUWP(x[3]),
                Atmosphere = GetAtmosphereFromUWP(x[3]),
                Hydrographics = GetHydrographicsFromUWP(x[3]),
                Populations = GetPopulationsFromUWP(x[3], x[13]),
                Government = GetGovernmentFromUWP(x[3]),
                LawLevel = GetLawLevelFromUWP(x[3]),
                TechLevel = GetTechLevelFromUWP(x[3]),
                TradeCodes = x[4].Split().ToList(),
                Importance = int.Parse(x[5]),
                EconomicExtension = x[6],
                CulturalExtension = x[7],
                Nobility = x[8],
                Bases = x[9].Select(newBase => newBase.ToString()).ToList(),
                TravelZone = (TravelZone) Enum.Parse(typeof(TravelZone), x[10], true),
                PBG = x[11],
                Allegiance = x[13],
                Temperature = (Temperature) Enum.Parse(typeof(Temperature), x[15], true),
                Rings = (Rings) Enum.Parse(typeof(Rings), x[16], true),
                Biosphere = int.Parse(x[17]),
                Chemistry = (PlanetChemistry) Enum.Parse(typeof(PlanetChemistry), x[18], true),
                PlanetType = (PlanetType) Enum.Parse(typeof(PlanetType), x[19], true),
                WorldType = (WorldType) Enum.Parse(typeof(WorldType), x[20], true),
                PlanetOrbit = (PlanetOrbit) Enum.Parse(typeof(PlanetOrbit), x[21], true),
                OrbitPosition = int.Parse(x[22]),
                Starport = new Starport(new RollingService()) {
                    Class = GetStarportClassFromUWP(x[3]),
                    SpecialFeature = (StarportSpecialFeature) Enum.Parse(typeof(StarportSpecialFeature), x[23], true),
                    Event = (StarportEvent) Enum.Parse(typeof(StarportEvent), x[24], true),
                    Enforcement = (StarportEnforcement) Enum.Parse(typeof(StarportEnforcement), x[25], true),
                    Defenses = (StarportDefenses) Enum.Parse(typeof(StarportDefenses), x[26], true)
                }
            }).ToList();

        foreach (var planet in planets) {
            _starSystem.Planets.Add(planet);
        }

        return _starSystem;
    }

    private static StarportClass GetStarportClassFromUWP(string uwp)
    {
        if (Enum.TryParse(uwp.First().ToString(), out StarportClass starportClass)) {
            return starportClass;
        }

        return StarportClass.Y;
    }

    private void GetStarData(string stars)
    {
        List<string> starData = stars.Split(' ').ToList();
        var isPrimaryStar = true;
        var newStar = new RttWorldgenStar();

        foreach (var starDatum in starData) {
            if (int.TryParse(starDatum.Last().ToString(), out int spectralSubclass)) {
                newStar.SpectralType = (SpectralType) Enum.Parse(typeof(SpectralType), starDatum.First().ToString(), true);
                newStar.SpectralSubclass = spectralSubclass;
            } else if (Enum.TryParse(starDatum, out SpectralType spectralType)) {
                newStar.SpectralType = spectralType;
                AddStarToSystem(newStar, isPrimaryStar);
                newStar = new RttWorldgenStar();
                isPrimaryStar = false;
            } else if (Enum.TryParse(starDatum, out LuminosityClass luminosity)) {
                newStar.LuminosityClass = luminosity;
                AddStarToSystem(newStar, isPrimaryStar);
                newStar = new RttWorldgenStar();
                isPrimaryStar = false;
            }
        }
    }

    private void AddStarToSystem(RttWorldgenStar newStar, bool isPrimaryStar)
    {
        if (isPrimaryStar) {
            _starSystem.PrimaryStar = newStar;
        } else {
            _starSystem.CompanionStars.Add(newStar);
        }
    }

    private Coordinates GetCoordinatesFromString(string s)
    {
        var x = s.Substring(0, 2);
        var y = s.Substring(2, 2);

        return new Coordinates(int.Parse(x), int.Parse(y));
    }

    private int GetSizeFromUWP(string uwp)
    {
        return uwp is "SGG" or "LGG" ? 16 : ExtendedHex.Reverse(uwp[1]);
    }

    private int GetAtmosphereFromUWP(string uwp)
    {
        return uwp is "SGG" or "LGG" ? 16 : ExtendedHex.Reverse(uwp[2]);
    }

    private int GetHydrographicsFromUWP(string uwp)
    {
        return uwp is "SGG" or "LGG" ? 16 : ExtendedHex.Reverse(uwp[3]);
    }
    
    private List<Population> GetPopulationsFromUWP(string uwp, string allegiance)
    {
        var population = uwp is "SGG" or "LGG" ? 0 : ExtendedHex.Reverse(uwp[4]);
        return new List<Population> {
            new() {
                PopulationNumber = population,
                Race = allegiance
            }
        };
    }

    private int GetGovernmentFromUWP(string uwp)
    {
        return uwp is "SGG" or "LGG" ? 0 : ExtendedHex.Reverse(uwp[5]);
    }

    private int GetLawLevelFromUWP(string uwp)
    {
        return uwp is "SGG" or "LGG" ? 0 : ExtendedHex.Reverse(uwp[6]);
    }

    private int GetTechLevelFromUWP(string uwp)
    {
        return uwp is "SGG" or "LGG" ? 0 : ExtendedHex.Reverse(uwp[8]);
    }

    private void AddBrownDwarfStarToSystem()
    {
        _starSystem.PrimaryStar = _rttWorldgenStarFactory.GenerateBrownDwarf();
    }

    private void AddStarsToSystem()
    {
        var numStars = GetNumStars();

        var primaryRoll = 0;
        for (var i = 0; i < numStars; i++) {
            if (i == 0) {
                _starSystem.PrimaryStar = new RttWorldgenStar(_rttWorldgenStarFactory.Generate(out int spectralRoll));
                primaryRoll = spectralRoll;
            } else {
                RttWorldgenStar newStar = new RttWorldgenStar(_rttWorldgenStarFactory.Generate(primaryRoll));
                if (newStar.SpectralType == _starSystem.PrimaryStar.SpectralType &&
                       newStar.SpectralSubclass < _starSystem.PrimaryStar.SpectralSubclass) {
                    newStar.SpectralSubclass = _starSystem.PrimaryStar.SpectralSubclass;
                }
                _starSystem.CompanionStars.Add(newStar);
            }
        }
    }

    private int GetNumStars()
    {
        return _rollingService.D6(3)switch {
            (<= 10) => 1,
            (<= 15) => 2,
            (>= 16) => 3
        };
    }

    private void AddPlanetsToSystem()
    {
        AddEpistellarPlanetsToSystem();
        AddInnerPlanetsToSystem();
        AddOuterPlanetsToSystem();

        foreach (RttWorldgenPlanet planet in _starSystem.Planets) {
            planet.GenerateEconomicExtension(_starSystem.Planets.Count(x => x.PlanetType == PlanetType.Jovian),
                _starSystem.Planets.Count(x => x.PlanetType == PlanetType.AsteroidBelt));
        }
    }

    private void AddEpistellarPlanetsToSystem()
    {
        var orbitNum = _rollingService.D6(1) - 3;
        var primaryStar = (RttWorldgenStar) _starSystem.PrimaryStar;

        if (primaryStar is {SpectralType: SpectralType.M, LuminosityClass: LuminosityClass.V}) {
            orbitNum--;
        }

        if (primaryStar.SpectralType is SpectralType.D or SpectralType.BD
            || primaryStar.LuminosityClass == LuminosityClass.III) {
            return;
        }

        orbitNum = Math.Min(orbitNum, 2);

        for (var i = 0; i < orbitNum; i++) {
            _starSystem.Planets.Add(
                _rttWorldgenPlanetFactory.GenerateRttWorldgenPlanet(primaryStar, PlanetOrbit.Epistellar, i + 1, _starSystem.Coordinates));
        }
    }


    private void AddInnerPlanetsToSystem()
    {
        var primaryStar = (RttWorldgenStar) _starSystem.PrimaryStar;

        var orbitNum = primaryStar.SpectralType == SpectralType.BD
            ? _rollingService.D3(1) - 1
            : _rollingService.D6(1) - 1;

        if (primaryStar.SpectralType == SpectralType.M && primaryStar.LuminosityClass == LuminosityClass.V) {
            orbitNum--;
        }

        if (_starSystem.CompanionStars.Exists(x => ((RttWorldgenStar) x).CompanionOrbit == CompanionOrbit.Close)) {
            orbitNum = 0;
        }

        for (var i = 0; i < orbitNum; i++) {
            _starSystem.Planets.Add(new RttWorldgenPlanet(new RollingService(), _rttWorldgenPlanetFactory.GenerateRttWorldgenPlanet(primaryStar, PlanetOrbit.Inner,
                _starSystem.Planets.Count + 1, _starSystem.Coordinates)));
        }
    }

    private void AddOuterPlanetsToSystem()
    {
        var primaryStar = (RttWorldgenStar) _starSystem.PrimaryStar;

        var orbitNum = _rollingService.D6(1) - 1;

        if (primaryStar.SpectralType is SpectralType.M or SpectralType.BD && primaryStar.LuminosityClass == LuminosityClass.V) {
            orbitNum--;
        }

        if (_starSystem.CompanionStars.Exists(x => ((RttWorldgenStar) x).CompanionOrbit == CompanionOrbit.Moderate)) {
            orbitNum = 0;
        }

        for (var i = 0; i < orbitNum; i++) {
            _starSystem.Planets.Add(new RttWorldgenPlanet(new RollingService(), _rttWorldgenPlanetFactory.GenerateRttWorldgenPlanet(primaryStar, PlanetOrbit.Outer,
                _starSystem.Planets.Count + 1, _starSystem.Coordinates)));
        }
    }
}