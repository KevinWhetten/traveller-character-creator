using TravellerCreatorGlobalMethods;
using TravellerCreatorModels.RTTWorldgen;

namespace TravellerCreatorServices.RTTWorldgen.Planets;

public static class HebeanPlanet
{
    public static RTTWorldgenPlanet Generate(RTTWorldgenPlanet planet)
    {
        planet.Size = Roll.D6(1) - 1;
        planet.Atmosphere = GetAtmosphere(planet);
        planet.Hydrographics = Roll.D6(2) + planet.Size - 11;
        planet.Biosphere = 0;
        planet = PlanetValidation.ValidatePlanet(planet);
        return planet;
    }

    private static int GetAtmosphere(RTTWorldgenPlanet planet)
    {
        var atmosphere = Roll.D6(1) + planet.Size - 6;
        if (atmosphere >= 2) {
            atmosphere = 10;
        }

        return atmosphere;
    }
}