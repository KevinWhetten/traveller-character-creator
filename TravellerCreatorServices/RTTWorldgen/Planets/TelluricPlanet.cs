using TravellerCreatorGlobalMethods;
using TravellerCreatorModels.SectorCreator.RTTWorldgen;

namespace TravellerCreatorServices.RTTWorldgen.Planets;

public static class TelluricPlanet
{
    public static RTTWorldgenPlanet Generate(RTTWorldgenPlanet planet)
    {
        planet.Size = Roll.D6(1) + 4;
        planet.Atmosphere = 12;
        planet.Hydrographics = GetHydrographics();
        planet.Biosphere = 0;
        planet = PlanetValidation.ValidatePlanet(planet);
        return planet;
    }

    private static int GetHydrographics()
    {
        return Roll.D6(1) switch {
                    (<= 4) => 0,
                    (<= 6) => 15,
                    _ => 0
                };
    }
}