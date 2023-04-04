using TravellerCreatorGlobalMethods;
using TravellerCreatorModels.Enums;
using TravellerCreatorModels.RTTWorldgen;

namespace TravellerCreatorServices.RTTWorldgen.Planets;

public static class PanthalassicPlanet
{
    public static RTTWorldgenPlanet Generate(RTTWorldgenPlanet planet, RTTWorldgenStar primaryStar)
    {
        planet.Size = Roll.D6(1) + 9;
        planet.Size = Roll.D6(1) + 9;
        planet.Atmosphere = Math.Min(Roll.D6(1) + 8, 13);
        planet.Hydrographics = 11;
        planet.Chemistry = GetChemistry(primaryStar);
        planet.Biosphere = GetBiosphere(primaryStar, planet);

        if (planet.Chemistry == PlanetChemistry.Ammonia) {
            planet.Chemistry = PlanetChemistry.Methane;
        }
        planet = PlanetValidation.ValidatePlanet(planet);
        return planet;
    }

    private static PlanetChemistry GetChemistry(RTTWorldgenStar primaryStar)
    {
        int chemistryRoll = Roll.D6(1);
        if (primaryStar.SpectralType == SpectralType.K && primaryStar.Luminosity == Luminosity.V) {
            chemistryRoll += 2;
        } else if (primaryStar.SpectralType == SpectralType.M && primaryStar.Luminosity == Luminosity.V) {
            chemistryRoll += 4;
        } else if (primaryStar.SpectralType == SpectralType.L) {
            chemistryRoll += 5;
        }

        return chemistryRoll switch {
            <= 6 => Roll.D6(2) switch {
                <= 8 => PlanetChemistry.Water,
                <= 11 => PlanetChemistry.Sulfur,
                <= 12 => PlanetChemistry.Chlorine,
                _ => PlanetChemistry.None
            },
            <= 8 => PlanetChemistry.Ammonia,
            <= 10 => PlanetChemistry.Methane,
            _ => PlanetChemistry.None
        };
    }

    private static int GetBiosphere(RTTWorldgenStar primaryStar, RTTWorldgenPlanet planet)
    {
        if (primaryStar.Age >= 4 + (int) planet.Chemistry)
            return Roll.D6(2);
        if (primaryStar.Age >= Roll.D3(1) + (int) planet.Chemistry)
            return Roll.D3(1);
        
        return 0;
    }
}