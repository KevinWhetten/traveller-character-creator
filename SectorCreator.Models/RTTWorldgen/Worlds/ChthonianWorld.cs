using SectorCreator.Global.Enums;

namespace SectorCreator.Models.RTTWorldgen.Worlds;

public interface IChthonianWorld
{
    RttWorldgenPlanet Generate(RttWorldgenPlanet planet);
}

public class ChthonianWorld : IChthonianWorld
{
    public RttWorldgenPlanet Generate(RttWorldgenPlanet planet)
    {
        planet.WorldType = WorldType.Chthonian;
        planet.Size = 16;
        planet.Atmosphere = 1;
        planet.Hydrographics = 0;
        planet.Biosphere = 0;
        planet = WorldValidation.ValidatePlanet(planet);
        return planet;
    }
}