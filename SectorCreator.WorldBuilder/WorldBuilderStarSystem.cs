using SectorCreator.Global;
using SectorCreator.Global.Enums;
using SectorCreator.WorldBuilder.Enums;
using SectorCreator.WorldBuilder.Planet;
using SectorCreator.WorldBuilder.Planet.Planet;
using SectorCreator.WorldBuilder.Planet.TerrestrialPlanet;
using SectorCreator.WorldBuilder.Star;

namespace SectorCreator.WorldBuilder;

public class WorldBuilderStarSystem
{
    private readonly IRollingService _rollingService;

    public WorldBuilderStar Star { get; set; }
    public List<WorldBuilderPlanet> Planets { get; set; } = new();
    public int WorldNum { get; set; }
    public int EmptyOrbits { get; set; }
    public double BaselineNumber { get; set; }
    public double BaselineOrbit { get; set; }
    public double Spread { get; set; }
    public double Age => Star.CompanionStar != null ? Math.Max(Star.Age, Star.CompanionStar.Age) : Star.Age;
    public double Mass => Star.CompanionStar != null ? Star.Mass + Star.CompanionStar.Mass : Star.Mass;
    public double Luminosity => Star.CompanionStar != null ? Star.Luminosity + Star.CompanionStar.Luminosity : Star.Luminosity;
    public double OrbitNumber => Star.CompanionStar?.OrbitNumber ?? Star.OrbitNumber;
    public string Component => Star.CompanionStar != null ? Star.Component + Star.CompanionStar.Component.Last() : Star.Component;
    public double OrbitDistance => Star.CompanionStar?.OrbitDistance ?? Star.OrbitDistance;
    public double Eccentricity => Star.CompanionStar?.Eccentricity ?? Star.Eccentricity;
    public double MAO => Star.AvailableOrbits.FirstOrDefault();
    public double HZCO => Star.HZCO;
    public string Name { get; set; }

    public WorldBuilderStarSystem(IRollingService rollingService)
    {
        _rollingService = rollingService;
    }

    public int GenerateEmptyOrbits()
    {
        EmptyOrbits = _rollingService.D6(2) switch {
            <= 9 => 0,
            10 => 1,
            11 => 2,
            12 => 3
        };

        WorldNum += EmptyOrbits;
        return EmptyOrbits;
    }

    public void CalculateBaselineOrbit()
    {
        switch (BaselineNumber) {
            case >= 1 when BaselineNumber <= WorldNum: {
                if (Star.HZCO >= 1) {
                    BaselineOrbit = Star.HZCO + (_rollingService.D6(2) - 7) / 10.0;
                } else {
                    BaselineOrbit = Star.HZCO + (_rollingService.D6(2) - 7) / 100.0;
                }

                if (BaselineOrbit < 0) {
                    BaselineOrbit = Math.Max(Star.HZCO - 0.1, Star.AvailableOrbits.First() + WorldNum * 0.01);
                }

                break;
            }
            case < 1 when Star.AvailableOrbits.First() >= 1:
                BaselineOrbit = Star.HZCO - BaselineNumber + WorldNum + (_rollingService.D6(2) - 2) / 10.0;
                break;
            case < 1:
                BaselineOrbit = Star.AvailableOrbits.First() - (BaselineNumber / 10.0) + (_rollingService.D6(2) - 2) / 10.0;
                break;
            default:
                if ((Star.HZCO - BaselineNumber + WorldNum) >= 1) {
                    BaselineOrbit = Star.HZCO - BaselineNumber + WorldNum + (_rollingService.D6(2) - 2) / 5.0;
                } else {
                    BaselineOrbit = Star.HZCO - (BaselineNumber + WorldNum + (_rollingService.D6(2) - 2) / 5.0) / 10.0;
                }

                break;
        }

        if (BaselineOrbit <= 0) {
            BaselineOrbit = Math.Max(Star.HZCO - 0.1, Star.AvailableOrbits.First() + WorldNum * 0.01);
        }
    }

    public void CalculateSpread()
    {
        Spread = (BaselineOrbit - Star.AvailableOrbits.First()) / (BaselineNumber > 0 ? BaselineNumber : .0001);

        var maximumSpread = Star.GetTotalAvailableOrbits() / (WorldNum + 1);

        if (Spread > maximumSpread) {
            Spread = maximumSpread;
        }

        Spread = Math.Max(Spread, 0.01);
    }

    public void CalculateBaselineNumber(int numStarSystems)
    {
        BaselineNumber = OrbitIsAvailable(Star.HZCO)
            ? Math.Max(_rollingService.D6(2) + GetBaselineDM(numStarSystems), 0)
            : 0;
    }

    private int GetBaselineDM(int numStarSystems)
    {
        var dm = 0;

        if (Star.CompanionStar != null) dm -= 2;
        dm += Star.LuminosityClass switch {
            LuminosityClass.Ia or LuminosityClass.Ib or LuminosityClass.II => 3,
            LuminosityClass.III => 2,
            LuminosityClass.IV => 1,
            LuminosityClass.VI => -1,
            _ => 0
        };
        if (Star.IsPostStellarObject()) dm -= 2;
        dm += WorldNum switch {
            < 6 => -4,
            <= 9 => -3,
            <= 12 => -2,
            <= 15 => -1,
            <= 17 => 0,
            <= 20 => 1,
            > 20 => 2
        };
        dm += numStarSystems;

        return dm;
    }

    public void GenerateOrbits()
    {
        var orbitNum = Star.AvailableOrbits.First() + Spread + (_rollingService.Flux() * Spread / 10.0);
        while (Planets.Count < WorldNum) {
            if (OrbitIsAvailable(orbitNum)) {
                Planets.Add(new WorldBuilderPlanet {
                    OrbitNumber = orbitNum,
                    Primary = Star.StarType == StarType.Primary ? GetPrimary(orbitNum) : Component
                });
            }

            orbitNum += Spread + (_rollingService.Flux() * Spread / 10.0);
        }
    }

    private string GetPrimary(double orbitNum)
    {
        if (Star.AvailableOrbits.Count > 2) {
            int i = 0;
        }

        var currentPrimary = "";
        var nextPrimaryLetter = 'A';

        for (var i = 0; i < Star.AvailableOrbits.Count; i += 2) {
            currentPrimary += nextPrimaryLetter;
            nextPrimaryLetter++;

            if (orbitNum >= Star.AvailableOrbits[i]
                && orbitNum <= Star.AvailableOrbits[i + 1]) {
                break;
            }
        }

        if (currentPrimary is "A" or "") {
            currentPrimary = Component;
        }

        return currentPrimary;
    }

    public bool OrbitIsAvailable(double orbitNum)
    {
        if (orbitNum > Star.AvailableOrbits.Last()) {
            return true;
        }

        for (var i = 0; i < Star.AvailableOrbits.Count; i += 2) {
            if (orbitNum >= Star.AvailableOrbits[i]
                && orbitNum <= Star.AvailableOrbits[i + 1]) {
                return true;
            }
        }

        return false;
    }

    public int AddAnomalousPlanets()
    {
        if (WorldNum == 0) {
            return 0;
        }

        var anomalousWorlds = _rollingService.D6(2) switch {
            <= 9 => 0,
            <= 11 => 1,
            12 => 2
        };

        for (var i = 0; i < anomalousWorlds; i++) {
            GenerateAnomalousWorld();
        }

        WorldNum += anomalousWorlds;

        return anomalousWorlds;
    }

    private void GenerateAnomalousWorld()
    {
        if (Planets.Count > 0) {
            var newPlanet = new WorldBuilderTerrestrialPlanet {
                Anomaly = _rollingService.D6(2) switch {
                    <= 7 => PlanetAnomaly.Random,
                    8 => PlanetAnomaly.Eccentric,
                    9 => PlanetAnomaly.Inclined,
                    <= 11 => PlanetAnomaly.Retrograde,
                    >= 12 => _rollingService.D6(1) switch {
                        <= 3 => PlanetAnomaly.LeadingTrojan,
                        >= 4 => PlanetAnomaly.TrailingTrojan
                    }
                }
            };

            if (newPlanet.Anomaly is PlanetAnomaly.Random or PlanetAnomaly.Eccentric or PlanetAnomaly.Inclined or PlanetAnomaly.Retrograde) {
                do {
                    newPlanet.OrbitNumber = _rollingService.D6(2) - 2 + _rollingService.D10(1) / 10.0 + _rollingService.D10(1) / 100.0;
                    newPlanet.Primary = Star.StarType == StarType.Primary ? GetPrimary(newPlanet.OrbitNumber) : Component;
                } while (!OrbitIsAvailable(newPlanet.OrbitNumber));
            } else if (newPlanet.Anomaly is PlanetAnomaly.LeadingTrojan or PlanetAnomaly.TrailingTrojan) {
                var planetNum = _rollingService.D(Planets.Count, 1) - 1;

                newPlanet.OrbitNumber = Planets.ToArray()[planetNum].OrbitNumber;
                newPlanet.Primary = Star.StarType == StarType.Primary ? GetPrimary(newPlanet.OrbitNumber) : Component;
            }

            Planets.Add(newPlanet);

            Planets = Planets.OrderBy(x => x.OrbitNumber).ToList();
        }
    }

    public int RemovePlanetsForEmptyOrbits()
    {
        var planetsRemoved = 0;
        for (var i = 0; i < EmptyOrbits; i++) {
            var planetNum = _rollingService.D(Planets.Count, 1) - 1;
            Planets.Remove(Planets.ToArray()[planetNum]);
            WorldNum--;
            planetsRemoved++;
        }

        return planetsRemoved;
    }

    public double GetPeriodInYears(double orbitsAroundMass)
    {
        return Star.CompanionStar?.GetPeriodInYears(orbitsAroundMass) ?? 0;
    }

    public bool IsPrimordial()
    {
        // TODO
        return false;
    }
}