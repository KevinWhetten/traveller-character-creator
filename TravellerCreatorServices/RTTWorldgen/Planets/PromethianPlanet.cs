using TravellerCreatorGlobalMethods;
using TravellerCreatorModels.Enums;
using TravellerCreatorModels.RTTWorldgen;

namespace TravellerCreatorServices.RTTWorldgen.Planets;

public static class PromethianPlanet
{
    public static RTTWorldgenPlanet Generate(RTTWorldgenPlanet planet, RTTWorldgenStar primaryStar)
    {
        planet.Size = Roll.D6(1) - 1;
        planet.Chemistry = GetChemistry(primaryStar, planet);
        planet.Biosphere = GetBiosphere(primaryStar, planet);
        planet.Atmosphere = GetAtmosphere(planet);
        planet.Hydrographics = Roll.D6(2) - 2;
        planet = PlanetValidation.ValidatePlanet(planet);
        return planet;
    }

    private static PlanetChemistry GetChemistry(RTTWorldgenStar primaryStar, RTTWorldgenPlanet planet)
    {
        int roll = Roll.D6(1);
        if (primaryStar.SpectralType == SpectralType.L) {
            roll += 2;
        }

        if (planet.PlanetOrbit == PlanetOrbit.Epistellar) {
            roll -= 2;
        } else if (planet.PlanetOrbit == PlanetOrbit.Outer) {
            roll += 2;
        }

        return roll switch {
            <= 4 => PlanetChemistry.Water,
            <= 6 => PlanetChemistry.Ammonia,
            <= 8 => PlanetChemistry.Methane,
            _ => PlanetChemistry.None
        };
    }

    private static int GetBiosphere(RTTWorldgenStar primaryStar, RTTWorldgenPlanet planet)
    {
        if (primaryStar.Age >= 4 + (int) planet.Chemistry) {
            var mod = 0;
            if (primaryStar.SpectralType == SpectralType.D) {
                mod = -3;
            }

            return Roll.D6(2) + mod;
        }

        if (primaryStar.Age < Roll.D3(1) + (int) planet.Chemistry) {
            return Roll.D3(1);
        }

        return 0;
    }

    private static int GetAtmosphere(RTTWorldgenPlanet planet)
    {
        if (planet.Biosphere >= 3 && planet.Chemistry == PlanetChemistry.Water) {
            int atmosphere = Roll.D6(2) + planet.Size - 7;
            atmosphere = Math.Min(atmosphere, 9);
            return Math.Max(atmosphere, 2);
        }

        return 10;
    }
}