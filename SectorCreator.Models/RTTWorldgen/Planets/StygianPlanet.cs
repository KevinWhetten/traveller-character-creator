using SectorCreator.Global;

namespace SectorCreator.Models.RTTWorldgen.Planets;

public static class StygianPlanet
{
    public static RttWorldgenPlanet Generate(RttWorldgenPlanet planet)
    {
        planet.Size = Roll.D6(1) - 1;
        planet.Atmosphere = 0;
        planet.Hydrographics = 0;
        planet.Biosphere = 0;
        planet = PlanetValidation.ValidatePlanet(planet);
        return planet;
    }
}