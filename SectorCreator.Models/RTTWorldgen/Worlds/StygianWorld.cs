using SectorCreator.Global;
using SectorCreator.Global.Enums;

namespace SectorCreator.Models.RTTWorldgen.Worlds;

public interface IStygianWorld
{
    RttWorldgenPlanet Generate(RttWorldgenPlanet planet);
}

public class StygianWorld : IStygianWorld
{
    private readonly IRollingService _rollingService;
    
    public StygianWorld(IRollingService rollingService)
    {
        _rollingService = rollingService;
    }

    public RttWorldgenPlanet Generate(RttWorldgenPlanet planet)
    {
        planet.WorldType = WorldType.Stygian;
        planet.Size = _rollingService.D6(1) - 1;
        planet.Atmosphere = 0;
        planet.Hydrographics = 0;
        planet.Biosphere = 0;
        planet = WorldValidation.ValidatePlanet(planet);
        return planet;
    }
}