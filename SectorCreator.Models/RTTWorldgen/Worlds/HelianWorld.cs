using SectorCreator.Global;

namespace SectorCreator.Models.RTTWorldgen.Worlds;

public interface IHelianWorld
{
    RttWorldgenPlanet Generate(RttWorldgenPlanet planet);
}

public class HelianWorld : IHelianWorld
{
    private readonly IRollingService _rollingService;
    private readonly IWorldValidation _worldValidation;

    public HelianWorld(IRollingService rollingService, IWorldValidation worldValidation)
    {
        _rollingService = rollingService;
        _worldValidation = worldValidation;
    }

    public RttWorldgenPlanet Generate(RttWorldgenPlanet planet)
    {
        planet.Size = _rollingService.D6(1) + 9;
        planet.Atmosphere = 13;
        planet.Hydrographics = GetHydrographics();
        planet.Biosphere = 0;
        planet = _worldValidation.ValidatePlanet(planet);
        return planet;
    }

    private int GetHydrographics()
    {
        return _rollingService.D6(1) switch {
            (<= 2) => 0,
            (<= 4) => _rollingService.D6(2) - 1,
            _ => 15
        };
    }
}