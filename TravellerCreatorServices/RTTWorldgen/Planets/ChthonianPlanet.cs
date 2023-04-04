using TravellerCreatorModels.RTTWorldgen;

namespace TravellerCreatorServices.RTTWorldgen.Planets;

public static class ChthonianPlanet
{
    public static RTTWorldgenPlanet Generate(RTTWorldgenPlanet planet)
    {
        planet.Size = 16;
        planet.Atmosphere = 1;
        planet.Hydrographics = 0;
        planet.Biosphere = 0;
        planet = PlanetValidation.ValidatePlanet(planet);
        return planet;
    }
}