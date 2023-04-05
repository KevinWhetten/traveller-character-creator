using TravellerCreatorGlobalMethods;
using TravellerCreatorModels.SectorCreator.RTTWorldgen;

namespace TravellerCreatorServices.RTTWorldgen.Planets;

public static class MeltballPlanet
{
    public static RTTWorldgenPlanet Generate(RTTWorldgenPlanet planet)
    {
        planet.Size = Roll.D6(1) - 1;
        planet.Atmosphere = 1;
        planet.Hydrographics = 15;
        planet.Biosphere = 0;
        planet = PlanetValidation.ValidatePlanet(planet);
        return planet;
    }
}