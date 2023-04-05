using TravellerCreatorGlobalMethods;
using TravellerCreatorModels.SectorCreator.Enums;
using TravellerCreatorModels.SectorCreator.RTTWorldgen;

namespace TravellerCreatorServices.RTTWorldgen.Planets;

public static class AreanPlanet
{
    public static RTTWorldgenPlanet Generate(RTTWorldgenPlanet planet, RTTWorldgenStar primaryStar)
    {
        planet.Size = Roll.D6(1) - 1;
        planet.Atmosphere = GetAtmosphere(primaryStar);
        planet.Hydrographics = GetHydrographics(planet);
        planet.Chemistry = GetChemistry(primaryStar, planet);
        planet.Biosphere = GetBiosphere(primaryStar, planet);
        planet = PlanetValidation.ValidatePlanet(planet);
        return planet;
    }

    private static int GetAtmosphere(RTTWorldgenStar primaryStar)
    {
        int roll = Roll.D6(1);
        if (primaryStar.SpectralType == SpectralType.D) {
            roll -= 2;
        }

        return roll switch {
            (<= 3) => 1,
            (<= 6) => 10,
            _ => 0
        };
    }

    private static int GetHydrographics(RTTWorldgenPlanet planet)
    {
        int hydrographics = Roll.D3(2) + planet.Size - 7;
        if (planet.Atmosphere == 1) {
            hydrographics -= 4;
        }

        return hydrographics;
    }

    private static PlanetChemistry GetChemistry(RTTWorldgenStar primaryStar, RTTWorldgenPlanet planet)
    {
        int roll = Roll.D6(1);
        if (primaryStar.SpectralType == SpectralType.L || planet.PlanetOrbit == PlanetOrbit.Outer) {
            roll += 2;
        }

        return roll switch {
            (<= 4) => PlanetChemistry.Water,
            (<= 6) => PlanetChemistry.Ammonia,
            (<= 8) => PlanetChemistry.Methane,
            _ => PlanetChemistry.None
        };
    }

    private static int GetBiosphere(RTTWorldgenStar primaryStar, RTTWorldgenPlanet planet)
    {
        var biosphere = 0;
        if (primaryStar.Age >= 4 + (int) planet.Chemistry && planet.Atmosphere == 10) {
            biosphere = Roll.D6(1) + planet.Size - 2;
        } else if (primaryStar.Age >= Roll.D3(1) + (int) planet.Chemistry) {
            biosphere = planet.Atmosphere switch {
                1 => Roll.D6(1) - 4,
                10 => Roll.D3(1),
                _ => 0
            };
        } else {
            biosphere = 0;
        }

        return biosphere;
    }
}