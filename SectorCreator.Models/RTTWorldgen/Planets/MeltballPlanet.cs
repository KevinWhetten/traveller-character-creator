using SectorCreator.Global;

namespace SectorCreator.Models.RTTWorldgen.Planets;

public static class MeltballPlanet
{
    public static RttWorldgenPlanet Generate(RttWorldgenPlanet planet)
    {
        planet.Size = Roll.D6(1) - 1;
        planet.Atmosphere = 1;
        planet.Hydrographics = 15;
        planet.Biosphere = 0;
        planet = PlanetValidation.ValidatePlanet(planet);
        return planet;
    }
}