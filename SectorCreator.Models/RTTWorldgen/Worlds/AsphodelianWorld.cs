using SectorCreator.Global;

namespace SectorCreator.Models.RTTWorldgen.Worlds;

public interface IAsphodelianWorld
{
    RttWorldgenPlanet Generate(RttWorldgenPlanet planet);
}

public class AsphodelianWorld : IAsphodelianWorld
{
    private readonly IRollingService _rollingService;
    private readonly IWorldValidation _worldValidation;

    public AsphodelianWorld(IRollingService rollingService, IWorldValidation worldValidation)
    {
        _rollingService = rollingService;
        _worldValidation = worldValidation;
    }

    public RttWorldgenPlanet Generate(RttWorldgenPlanet planet)
    {
        planet.Size = _rollingService.D6(1) + 9;
        planet.Atmosphere = 1;
        planet.Hydrographics = 0;
        planet.Biosphere = 0;
        planet = _worldValidation.ValidatePlanet(planet);
        return planet;
    }
}