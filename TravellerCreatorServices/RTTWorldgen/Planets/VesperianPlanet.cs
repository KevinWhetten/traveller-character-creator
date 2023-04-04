using TravellerCreatorGlobalMethods;
using TravellerCreatorModels.Enums;
using TravellerCreatorModels.RTTWorldgen;

namespace TravellerCreatorServices.RTTWorldgen.Planets;

public static class VesperianPlanet
{
    public static RTTWorldgenPlanet Generate(RTTWorldgenPlanet planet, RTTWorldgenStar primaryStar)
    {
        planet.Size = Roll.D6(1) + 4;
        planet.Chemistry = GetChemistry();
        planet.Biosphere = GetBiosphere(primaryStar);
        planet.Atmosphere = GetAtmosphere(planet);
        planet.Hydrographics = Roll.D6(2) - 2;
        planet = PlanetValidation.ValidatePlanet(planet);
        return planet;
    }

    private static PlanetChemistry GetChemistry()
    {
        return Roll.D6(2) switch {
            (<= 11) => PlanetChemistry.Water,
            12 => PlanetChemistry.Chlorine,
            _ => PlanetChemistry.None
        };
    }

    private static int GetBiosphere(RTTWorldgenStar primaryStar)
    {
        if (primaryStar.Age >= 4) {
            return Roll.D6(2);
        }

        if (primaryStar.Age >= Roll.D3(1)) {
            return Roll.D3(1);
        }

        return 0;
    }

    private static int GetAtmosphere(RTTWorldgenPlanet planet)
    {
        if (planet.Biosphere >= 3) {
            if (planet.Chemistry == PlanetChemistry.Water) {
                int atmosphere = Roll.D6(2) + planet.Size - 7;
                atmosphere = Math.Min(atmosphere, 9);
                return Math.Max(atmosphere, 2);
            }
            if (planet.Chemistry == PlanetChemistry.Chlorine) {
                return 11;
            }
        }

        return 10;
    }
}