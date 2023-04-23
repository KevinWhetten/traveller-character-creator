namespace SectorCreator.Models.RTTWorldgen.Worlds;

public static class WorldValidation
{
    public static RttWorldgenPlanet ValidatePlanet(RttWorldgenPlanet planet)
    {
        planet.Size = Math.Max(planet.Size, 0);
        planet.Atmosphere = Math.Max(planet.Atmosphere, 0);
        planet.Hydrographics = Math.Max(planet.Hydrographics, 0);
        planet.Biosphere = Math.Max(planet.Biosphere, 0);

        return planet;
    }
}