using TravellerCreatorGlobalMethods;
using TravellerCreatorModels.RTTWorldgen;

namespace TravellerCreatorServices.RTTWorldgen.Planets;

public static class JaniLithicPlanet
{
    public static RTTWorldgenPlanet Generate(RTTWorldgenPlanet planet)
    {
        planet.Size = Roll.D6(1) + 4;
        planet.Atmosphere = GetAtmosphere();
        planet.Hydrographics = 0;
        planet.Biosphere = 0;
        planet = PlanetValidation.ValidatePlanet(planet);
        return planet;
    }

    private static int GetAtmosphere()
    {
        return Roll.D6(1) switch {
            (<= 3) => 1,
            (<= 6) => 10,
            _ => 0
        };
    }
}