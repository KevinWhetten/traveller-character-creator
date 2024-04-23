using SectorCreator.Global;
using SectorCreator.Global.Enums;
using SectorCreator.Models.CustomTypes;
using SectorCreator.Models.Factories.T5;
using SectorCreator.Models.RTTWorldgen;
using SectorCreator.Models.RTTWorldgen.Factories;
using SectorCreator.Models.Services.TradeCodeService;

namespace SectorCreator.Models.Basic.Factories;

public interface IHexFactory
{
    Hex GenerateMongooseHex(Coordinates subsectorCoordinates, Coordinates hexCoordinates, SectorType sectorType);
    Hex GenerateT5Hex(Coordinates subsectorCoordinates, Coordinates hexCoordinates);
    Hex GenerateRttWorldgenHex(Coordinates subsectorCoordinates, Coordinates hexCoordinates);
    Hex GenerateStarFrontiersHex(Coordinates subsectorCoordinates, Coordinates hexCoordinates);
}

public class HexFactory : IHexFactory
{
    private readonly IRollingService _rollingService;
    private readonly IStarSystemFactory _starSystemFactory;
    private readonly IT5StarSystemFactory _t5StarSystemFactory;
    private readonly IRttWorldgenStarSystemFactory _rttWorldgenStarSystemFactory;

    public HexFactory(IRollingService rollingService, IStarSystemFactory starSystemFactory, IT5StarSystemFactory t5StarSystemFactory, IRttWorldgenStarSystemFactory rttWorldgenStarSystemFactory)
    {
        _rollingService = rollingService;
        _starSystemFactory = starSystemFactory;
        _t5StarSystemFactory = t5StarSystemFactory;
        _rttWorldgenStarSystemFactory = rttWorldgenStarSystemFactory;
    }

    public Hex GenerateMongooseHex(Coordinates subsectorCoordinates, Coordinates hexCoordinates, SectorType sectorType)
    {
        var hex = new Hex {
            Coordinates = SetCoordinates(subsectorCoordinates, hexCoordinates)
        };

        if (_rollingService.D6(1) >= 4) {
            hex.StarSystems.Add(_starSystemFactory.GenerateMongooseStarSystem(sectorType, hex.Coordinates));
        }

        return hex;
    }

    public Hex GenerateT5Hex(Coordinates subsectorCoordinates, Coordinates hexCoordinates)
    {
        var hex = new Hex {
            Coordinates = SetCoordinates(subsectorCoordinates, hexCoordinates)
        };

        hex.StarSystems.Add(_t5StarSystemFactory.Generate());

        return hex;
    }

    public Hex GenerateRttWorldgenHex(Coordinates subsectorCoordinates, Coordinates hexCoordinates)
    {
        var hex = new Hex {
            Coordinates = SetCoordinates(subsectorCoordinates, hexCoordinates)
        };

        foreach (var race in Races.races.Where(race => race.HomeworldCoordinates == hex.Coordinates)) {
            hex.StarSystems.Add(race.HomeSystem);
            hex.MostImportantPlanet = race.Homeworld;
            return hex;
        }

        if (_rollingService.D6(1) > 4) {
            hex.StarSystems.Add(_rttWorldgenStarSystemFactory.Generate(StarSystemType.Regular, hex.Coordinates));
        }

        if (_rollingService.D6(1) == 6) {
            hex.StarSystems.Add(_rttWorldgenStarSystemFactory.Generate(StarSystemType.BrownDwarf, hex.Coordinates));
        }

        for (var index = 0; index < hex.StarSystems.Count; index++) {
            var starSystem = hex.StarSystems[index];
            for (var i = 0; i < starSystem.CompanionStars.Count; i++) {
                var star = (RttWorldgenStar) starSystem.CompanionStars[i];
                if (star.CompanionOrbit == CompanionOrbit.Distant) {
                    starSystem.CompanionStars.Remove(star);
                    hex.StarSystems.Add(_rttWorldgenStarSystemFactory.Generate(star, hex.Coordinates));
                }
            }
        }

        ModifyMainPlanet(hex);

        return hex;
    }

    private void ModifyMainPlanet(Hex hex)
    {
        var mostImportantPlanet = new RttWorldgenPlanet(new RollingService()) {Importance = -4};
        var gasGiantCount = 0;
        var beltCount = 0;

        foreach (var starSystem in hex.StarSystems) {
            foreach (var planet in starSystem.Planets.Cast<RttWorldgenPlanet>()) {
                if (IsMoreImportant(planet, mostImportantPlanet)) {
                    mostImportantPlanet = planet;
                    gasGiantCount = starSystem.Planets.Count(x => x.PlanetType == PlanetType.Jovian) + starSystem.Planets.SelectMany(x => x.Satellites).Count(x => x.PlanetType == PlanetType.Jovian);
                    beltCount = starSystem.Planets.Count(x => x.PlanetType == PlanetType.AsteroidBelt) + starSystem.Planets.SelectMany(x => x.Satellites).Count(x => x.PlanetType == PlanetType.AsteroidBelt);
                }

                foreach (var satellite in planet.Satellites.Cast<RttWorldgenPlanet>().Where(satellite => IsMoreImportant(satellite, mostImportantPlanet))) {
                    mostImportantPlanet = satellite;
                    gasGiantCount = starSystem.Planets.Count(x => x.PlanetType == PlanetType.Jovian) + starSystem.Planets.SelectMany(x => x.Satellites).Count(x => x.PlanetType == PlanetType.Jovian);
                    beltCount = starSystem.Planets.Count(x => x.PlanetType == PlanetType.AsteroidBelt) + starSystem.Planets.SelectMany(x => x.Satellites).Count(x => x.PlanetType == PlanetType.AsteroidBelt);
                }
            }
        }

        if (mostImportantPlanet.Importance < -3) {
            return;
        }

        mostImportantPlanet.GenerateStarport();
        mostImportantPlanet.TradeCodes = TradeCodeService.AddTradeCodes(mostImportantPlanet);
        mostImportantPlanet.GenerateImportance();
        mostImportantPlanet.GenerateEconomicExtension(gasGiantCount, beltCount);

        hex.MostImportantPlanet = mostImportantPlanet;
    }

    private bool IsMoreImportant(RttWorldgenPlanet planet, RttWorldgenPlanet mostImportantPlanet)
    {
        if (mostImportantPlanet.Importance < planet.Importance) {
            return true;
        }

        if (mostImportantPlanet.Importance == planet.Importance) {
            if (mostImportantPlanet.Population < planet.Population) {
                return true;
            }

            if (mostImportantPlanet.Population == planet.Population) {
                if (int.Parse(mostImportantPlanet.PBG) < int.Parse(planet.PBG)) {
                    return true;
                }

                if (mostImportantPlanet.PBG == planet.PBG) {
                    if (mostImportantPlanet.TechLevel < planet.TechLevel) {
                        return true;
                    }

                    if (mostImportantPlanet.TechLevel == planet.TechLevel) {
                        if (mostImportantPlanet.Starport.Class > planet.Starport.Class) {
                            return true;
                        }

                        if (mostImportantPlanet.Starport.Class == planet.Starport.Class) {
                            if (int.Parse(mostImportantPlanet.EconomicExtension.Substring(4, 2)) <
                                int.Parse(planet.EconomicExtension.Substring(4, 2))) {
                                return true;
                            }

                            if (int.Parse(mostImportantPlanet.EconomicExtension.Substring(4, 2)) ==
                                int.Parse(planet.EconomicExtension.Substring(4, 2))) {
                                if (ExtendedHex.Reverse(mostImportantPlanet.EconomicExtension[1])
                                    < ExtendedHex.Reverse(planet.EconomicExtension[1])) {
                                    return true;
                                }

                                if (mostImportantPlanet.EconomicExtension[1] == planet.EconomicExtension[1]) {
                                    if (mostImportantPlanet.Biosphere < planet.Biosphere) {
                                        return true;
                                    }

                                    if (mostImportantPlanet.Biosphere == planet.Biosphere) {
                                        if (_rollingService.D6(1) >= 4) {
                                            return true;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        return false;
    }

    public Hex GenerateStarFrontiersHex(Coordinates subsectorCoordinates, Coordinates hexCoordinates)
    {
        var hex = new Hex {
            Coordinates = SetCoordinates(subsectorCoordinates, hexCoordinates)
        };

        if (_rollingService.D10(1) >= 5) {
            hex.StarSystems.Add(_starSystemFactory.GenerateStarFrontiersStarSystem(hex.Coordinates));
        }

        return hex;
    }

    private Coordinates SetCoordinates(Coordinates subsectorCoordinates, Coordinates hexCoordinates)
    {
        return new Coordinates {
            X = hexCoordinates.X + 8 * ((subsectorCoordinates.X - 1) % 8),
            Y = hexCoordinates.Y + 10 * ((subsectorCoordinates.Y - 1) % 10)
        };
    }
}