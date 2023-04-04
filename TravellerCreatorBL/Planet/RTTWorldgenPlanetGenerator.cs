using TravellerCreatorGlobalMethods;
using TravellerCreatorModels.Enums;
using TravellerCreatorModels.RTTWorldgen;
using TravellerCreatorServices.RTTWorldgen;

namespace TravellerCharacterCreatorBL.Planet;

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

        return Planet;
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
}