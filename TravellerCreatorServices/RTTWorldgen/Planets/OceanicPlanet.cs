using TravellerCreatorGlobalMethods;
using TravellerCreatorModels.Enums;
using TravellerCreatorModels.RTTWorldgen;

namespace TravellerCreatorServices.RTTWorldgen.Planets;

public static class OceanicPlanet
{
    public static RTTWorldgenPlanet Generate(RTTWorldgenPlanet planet, RTTWorldgenStar primaryStar)
    {
        planet.Size = Roll.D6(1) + 4;
        planet.Chemistry = OceanicPlanet.GetChemistry(primaryStar, planet);
        planet.Biosphere = OceanicPlanet.GetBiosphere(primaryStar, planet);
        planet.Atmosphere = OceanicPlanet.GetAtmosphere(primaryStar, planet);
        planet.Hydrographics = 11;
        planet = PlanetValidation.ValidatePlanet(planet);
        return planet;
    }

    private static PlanetChemistry GetChemistry(RTTWorldgenStar primaryStar, RTTWorldgenPlanet planet)
    {
        var chemistryRoll = Roll.D6(1);
        if (primaryStar.SpectralType == SpectralType.K && primaryStar.Luminosity == Luminosity.V) {
            chemistryRoll += 2;
        } else if (primaryStar.SpectralType == SpectralType.M && primaryStar.Luminosity == Luminosity.V) {
            chemistryRoll += 4;
        } else if (primaryStar.SpectralType == SpectralType.L) {
            chemistryRoll += 5;
        } else if (planet.PlanetOrbit == PlanetOrbit.Outer) {
            chemistryRoll += 2;
        }

        return chemistryRoll switch {
            (<= 6) => PlanetChemistry.Water,
            (<= 8) => PlanetChemistry.Ammonia,
            _ => PlanetChemistry.Methane
        };
    }

    private static int GetBiosphere(RTTWorldgenStar primaryStar, RTTWorldgenPlanet planet)
    {
        int biosphere;

        if (primaryStar.Age >= 4 + (int) planet.Chemistry) {
            biosphere = Roll.D6(2);
            if (primaryStar.SpectralType == SpectralType.D) {
                biosphere -= 3;
            }
        } else if (primaryStar.Age >= Roll.D3(1) + (int) planet.Chemistry) {
            biosphere = Roll.D3(1);
        } else {
            biosphere = 0;
        }

        return biosphere;
    }

    private static int GetAtmosphere(RTTWorldgenStar primaryStar, RTTWorldgenPlanet planet)
    {
        int atmosphere;
        if (planet.Chemistry == PlanetChemistry.Water) {
            atmosphere = Roll.D6(2) + planet.Size - 6;
            if (primaryStar.SpectralType == SpectralType.K && primaryStar.Luminosity == Luminosity.V) {
                atmosphere--;
            } else if (primaryStar.SpectralType == SpectralType.M && primaryStar.Luminosity == Luminosity.V) {
                atmosphere -= 2;
            } else if (primaryStar.SpectralType == SpectralType.L) {
                atmosphere -= 3;
            } else if (primaryStar.Luminosity == Luminosity.IV) {
                atmosphere--;
            }

            atmosphere = Math.Min(atmosphere, 12);
            atmosphere = Math.Max(atmosphere, 1);
        } else {
            atmosphere = Roll.D6(1) switch {
                1 => 1,
                (<= 4) => 10,
                _ => 12
            };
        }

        return atmosphere;
    }
}