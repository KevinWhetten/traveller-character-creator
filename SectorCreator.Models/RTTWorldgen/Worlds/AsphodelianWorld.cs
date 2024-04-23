using SectorCreator.Global;
using SectorCreator.Global.Enums;

namespace SectorCreator.Models.RTTWorldgen.Worlds;

public interface IAsphodelianWorld
{
    RttWorldgenPlanet Generate(RttWorldgenPlanet planet);
}

public class AsphodelianWorld : IAsphodelianWorld
{
    private readonly IRollingService _rollingService;
    
    public AsphodelianWorld(IRollingService rollingService)
    {
        _rollingService = rollingService;
    }

    public RttWorldgenPlanet Generate(RttWorldgenPlanet planet)
    {
        planet.WorldType = WorldType.Asphodelian;
        planet.Size = _rollingService.D6(1) + 9;
        planet.Atmosphere = 1;
        planet.Hydrographics = 0;
        planet.Biosphere = 0;
        planet = WorldValidation.ValidatePlanet(planet);
        return planet;
    }
}