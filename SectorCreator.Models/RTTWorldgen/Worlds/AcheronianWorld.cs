using SectorCreator.Global;

namespace SectorCreator.Models.RTTWorldgen.Worlds;

public interface IAcheronianWorld
{
    RttWorldgenPlanet Generate(RttWorldgenPlanet planet);
}

public class AcheronianWorld : IAcheronianWorld
{
    private readonly IRollingService _rollingService;
    private readonly IWorldValidation _worldValidation;

    public AcheronianWorld(IRollingService rollingService, IWorldValidation worldValidation)
    {
        _rollingService = rollingService;
        _worldValidation = worldValidation;
    }

    public RttWorldgenPlanet Generate(RttWorldgenPlanet planet)
    {
        planet.Size = _rollingService.D6(1) + 4;
        planet.Atmosphere = 1;
        planet.Hydrographics = 0;
        planet.Biosphere = 0;
        planet = _worldValidation.ValidatePlanet(planet);
        return planet;
    }
}