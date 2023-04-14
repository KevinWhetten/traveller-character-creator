namespace SectorCreator.Models.RTTWorldgen.Worlds;

public interface IChthonianWorld
{
    RttWorldgenPlanet Generate(RttWorldgenPlanet planet);
}

public class ChthonianWorld : IChthonianWorld
{
    private readonly IWorldValidation _worldValidation;

    public ChthonianWorld(IWorldValidation worldValidation)
    {
        _worldValidation = worldValidation;
    }
    
    public RttWorldgenPlanet Generate(RttWorldgenPlanet planet)
    {
        planet.Size = 16;
        planet.Atmosphere = 1;
        planet.Hydrographics = 0;
        planet.Biosphere = 0;
        planet = _worldValidation.ValidatePlanet(planet);
        return planet;
    }
}