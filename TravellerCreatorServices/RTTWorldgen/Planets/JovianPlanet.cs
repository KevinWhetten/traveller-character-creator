using TravellerCreatorGlobalMethods;
using TravellerCreatorModels.Enums;
using TravellerCreatorModels.RTTWorldgen;

namespace TravellerCreatorServices.RTTWorldgen.Planets;

public static class JovianPlanet
{
    public static RTTWorldgenPlanet Generate(RTTWorldgenPlanet planet, RTTWorldgenStar primaryStar)
    {
        planet.Size = 16;
        planet.Atmosphere = 16;
        planet.Hydrographics = 16;
        planet.Biosphere = JovianPlanet.GetBiosphere(primaryStar, planet);
        planet.Chemistry = JovianPlanet.GetChemistry(primaryStar, planet);
        planet = PlanetValidation.ValidatePlanet(planet);
        return planet;
    }

    private static int GetBiosphere(RTTWorldgenStar primaryStar, RTTWorldgenPlanet planet)
    {
        var biosphere = 0;
        int roll = Roll.D6(1);
        if (planet.PlanetOrbit == PlanetOrbit.Inner) {
            roll += 2;
        }

        switch (roll) {
            case <= 5:
                biosphere = 0;
                break;
            case <= 8:
                if (primaryStar.Age >= 7) {
                    biosphere = Roll.D6(2);
                    if (primaryStar.SpectralType == SpectralType.D) {
                        biosphere -= 3;
                    }
                } else if (primaryStar.Age >= Roll.D6(1)) {
                    biosphere = Roll.D3(1);
                } else {
                    biosphere = 0;
                }

                break;
            default:
                biosphere = 0;
                break;
        }

        return biosphere;
    }

    private static PlanetChemistry GetChemistry(RTTWorldgenStar primaryStar, RTTWorldgenPlanet planet)
    {
        if (planet.Biosphere >= 1) {
            int chemistry = Roll.D6(1);
            if (primaryStar.SpectralType == SpectralType.L) {
                chemistry++;
            }

            if (planet.PlanetOrbit == PlanetOrbit.Epistellar) {
                chemistry -= 2;
            } else if (planet.PlanetOrbit == PlanetOrbit.Outer) {
                chemistry += 2;
            }

            return planet.Chemistry = chemistry switch {
                (<= 3) => PlanetChemistry.Water,
                (>= 4) => PlanetChemistry.Ammonia
            };
        }

        return PlanetChemistry.None;
    }
}