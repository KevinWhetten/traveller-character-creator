using TravellerCreatorGlobalMethods;
using TravellerCreatorModels.SectorCreator.Enums;
using TravellerCreatorModels.SectorCreator.RTTWorldgen;
using TravellerCreatorServices.RTTWorldgen;

namespace TravellerCharacterCreatorBL.SectorCreator.Planet;

public class RTTWorldgenPlanetGenerator
{
    private readonly RTTWorldgenWorldTypeService _rttWorldgenWorldTypeService = new();
    private readonly RTTWorldgenGenerateWorldService _rttWorldgenGenerateWorldService = new();
    private RTTWorldgenPlanet Planet { get; set; } = new();

    public RTTWorldgenPlanet GeneratePlanet(PlanetOrbit planetOrbit, RTTWorldgenStar primaryStar, int orbitNum)
    {
        Planet = new RTTWorldgenPlanet {
            PlanetOrbit = planetOrbit,
            OrbitPosition = orbitNum,
            StarId = primaryStar.Id,
            Rings = Rings.None
        };

        GeneratePlanetType(primaryStar);
        GenerateSatellites();
        GenerateRings();
        _rttWorldgenWorldTypeService.GenerateWorldType(primaryStar, Planet);
        Planet = _rttWorldgenGenerateWorldService.GenerateWorld(Planet, primaryStar);

        Planet.Population = 0;
        Planet.Government = 0;
        Planet.LawLevel = 0;
        Planet.TechLevel = 0;

        GenerateDesirability(primaryStar);

        return Planet;
    }

    private void GenerateDesirability(RTTWorldgenStar primaryStar)
    {
        var desirability = 0;
        switch (Planet.PlanetType) {
            case PlanetType.AsteroidBelt:
                desirability = Roll.D6(1) - Roll.D6(1);
                if (primaryStar is {SpectralType: SpectralType.M, Luminosity: Luminosity.Ve}) {
                    desirability -= Roll.D3(1);
                }

                if (Planet.PlanetOrbit == PlanetOrbit.Inner) {
                    if (primaryStar is {SpectralType: SpectralType.M, Luminosity: Luminosity.V}) {
                        desirability += 1;
                    } else if (primaryStar.SpectralType is SpectralType.K or SpectralType.G or SpectralType.F) {
                        desirability += 2;
                    }
                }

                break;
            case PlanetType.Terrestrial:
            case PlanetType.Jovian:
            case PlanetType.DwarfPlanet:
            case PlanetType.Helian:
                if (Planet.Hydrographics == 0) {
                    desirability--;
                }

                if (Planet.Size >= 13 || Planet.Atmosphere is >= 12 and <= 16 || Planet.Hydrographics == 15) {
                    desirability -= 2;
                }

                if (primaryStar is {SpectralType: SpectralType.M, Luminosity: Luminosity.Ve}) {
                    desirability -= Roll.D3(1);
                }

                if (Planet.Size is >= 1 and <= 11
                    && Planet.Atmosphere is >= 2 and <= 9
                    && Planet.Hydrographics is >= 0 and <= 11) {
                    if (Planet.Size is >= 5 and <= 10
                        && Planet.Atmosphere is >= 4 and <= 9
                        && Planet.Hydrographics is >= 4 and <= 8) {
                        desirability += 5;
                    } else if (Planet.Hydrographics is 10 or 11) {
                        desirability += 3;
                    } else if (Planet.Atmosphere is >= 2 and <= 6
                               && Planet.Hydrographics is >= 0 and <= 3) {
                        desirability += 2;
                    } else {
                        desirability += 4;
                    }
                }

                if (Planet is {Size: >= 11, Atmosphere: <= 15}) {
                    desirability--;
                }

                if (Planet.PlanetOrbit == PlanetOrbit.Inner) {
                    if (primaryStar is {SpectralType: SpectralType.M, Luminosity: Luminosity.V}) {
                        desirability += 1;
                    } else if (primaryStar.SpectralType is SpectralType.K or SpectralType.G or SpectralType.F) {
                        desirability += 2;
                    }
                }

                if (Planet.Size == 0) {
                    desirability--;
                }

                if (Planet.Atmosphere is >= 6 and <= 8) {
                    desirability++;
                }

                break;
            default:
                throw new ArgumentOutOfRangeException();
        }

        Planet.Desirability = desirability;
    }

    private void GenerateRings()
    {
        if (Planet.PlanetType == PlanetType.Jovian) {
            Planet.Rings = Roll.D6(1) <= 4 ? Rings.Minor : Rings.Complex;
        }
    }

    private RTTWorldgenPlanet GenerateSatellite(PlanetType planetType)
    {
        var planet = new RTTWorldgenPlanet {
            PlanetType = planetType,
            ParentId = Planet.Id
        };

        return planet;
    }

    private void GeneratePlanetType(RTTWorldgenStar primaryStar)
    {
        int roll = Roll.D6(1);

        if (primaryStar.SpectralType == SpectralType.L) {
            roll--;
        }

        Planet.PlanetType = roll switch {
            <= 1 => PlanetType.AsteroidBelt,
            2 => PlanetType.DwarfPlanet,
            3 => PlanetType.Terrestrial,
            4 => PlanetType.Helian,
            >= 5 => PlanetType.Jovian
        };
    }

    private void GenerateSatellites()
    {
        int satelliteNum;

        switch (Planet.PlanetType) {
            case PlanetType.AsteroidBelt:
                if (Roll.D6(1) >= 5) {
                    Planet.Satellites.Add(GenerateSatellite(PlanetType.DwarfPlanet));
                }

                break;
            case PlanetType.DwarfPlanet:
                if (Roll.D6(1) == 6) {
                    Planet.Satellites.Add(GenerateSatellite(PlanetType.DwarfPlanet));
                }

                break;
            case PlanetType.Terrestrial:
                if (Roll.D6(1) >= 5) {
                    Planet.Satellites.Add(GenerateSatellite(PlanetType.DwarfPlanet));
                }

                break;
            case PlanetType.Helian:
                satelliteNum = Roll.D6(1) - 3;
                bool terrestrialSatellite = Roll.D6(1) == 6;

                for (var i = 0; i < satelliteNum; i++) {
                    if (i == 0 && terrestrialSatellite) {
                        Planet.Satellites.Add(GenerateSatellite(PlanetType.Terrestrial));
                    } else {
                        Planet.Satellites.Add(GenerateSatellite(PlanetType.DwarfPlanet));
                    }
                }

                break;
            case PlanetType.Jovian:
                satelliteNum = Roll.D6(1);

                for (var i = 0; i < satelliteNum; i++) {
                    if (i == 0 && Roll.D6(1) == 6) {
                        Planet.Satellites.Add(Roll.D6(1) == 6
                            ? GenerateSatellite(PlanetType.Helian)
                            : GenerateSatellite(PlanetType.Terrestrial));
                    } else {
                        Planet.Satellites.Add(GenerateSatellite(PlanetType.DwarfPlanet));
                    }
                }

                break;
        }
    }

    public RTTWorldgenPlanet GenerateSettlement(RTTWorldgenPlanet planet)
    {
        if (planet.Biosphere >= 12) {
            planet.TechLevel = 10 + Roll.D6(1);
            if (planet.Chemistry == PlanetChemistry.Water) {
                planet.Desirability = 8;
                planet.Population = 8 + Roll.D3(1) - Roll.D3(1);
            } else {
                planet.Population = Roll.D6(2);
            }

            planet.Government = planet.Population + Roll.D6(2) - 7;
        }

        planet.LawLevel = planet.Government + Roll.D6(2) - 7;

        int mod = planet.LawLevel switch {
            (>= 1 and <= 3) => 1,
            (>= 6 and <= 9) => -1,
            (>= 10 and <= 12) => -2,
            (>= 13) => -3,
            _ => 0
        };
        if (planet.Atmosphere is >= 0 and <= 4 or 7 or >= 9
            || planet.Hydrographics == 15) {
            mod++;
        }

        mod += planet.TechLevel switch {
            >= 12 and <= 14 => 1,
            >= 15 => 2,
            _ => 0
        };
        planet.IndustrialBase = planet.Population + Roll.D6(2) - 7 + mod;

        switch (planet.IndustrialBase) {
            case 0:
                planet.Population = -1;
                break;
            case >= 4 and <= 9:
                planet.Atmosphere = planet.Atmosphere switch {
                    3 => 2,
                    5 => 4,
                    6 => 7,
                    8 => 9,
                    _ => planet.Atmosphere
                };
                break;
            case >= 10:
                if (planet.Atmosphere is 3 or 5 or 6 or 8) {
                    if (Roll.D3(1) >= 2) {
                        planet.Population++;
                        planet.Atmosphere = planet.Atmosphere switch {
                            3 => 2,
                            5 => 4,
                            6 => 7,
                            8 => 9,
                            _ => planet.Atmosphere
                        };
                    } else {
                        planet.Population += 2;
                    }
                }

                break;
        }
        
        

        return planet;
    }
}