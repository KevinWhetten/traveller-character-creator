using TravellerCreatorModels.RTTWorldgen;

namespace TravellerCreatorServices.RTTWorldgen.Planets;

public static class PlanetValidation
{
    public static RTTWorldgenPlanet ValidatePlanet(RTTWorldgenPlanet planet)
    {
        planet.Size = Math.Max(planet.Size, 0);
        planet.Atmosphere = Math.Max(planet.Atmosphere, 0);
        planet.Hydrographics = Math.Max(planet.Hydrographics, 0);
        planet.Biosphere = Math.Max(planet.Biosphere, 0);

        return planet;
    }
}