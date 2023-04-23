using SectorCreator.Global;
using SectorCreator.Global.Enums;

namespace SectorCreator.Models.RTTWorldgen.Worlds;

public interface IHelianWorld
{
    RttWorldgenPlanet Generate(RttWorldgenPlanet planet);
}

public class HelianWorld : IHelianWorld
{
    private readonly IRollingService _rollingService;

    public HelianWorld(IRollingService rollingService)
    {
        _rollingService = rollingService;
    }

    public RttWorldgenPlanet Generate(RttWorldgenPlanet planet)
    {
        planet.WorldType = WorldType.Helian;
        planet.Size = _rollingService.D6(1) + 9;
        planet.Atmosphere = 13;
        planet.Hydrographics = GetHydrographics();
        planet.Biosphere = 0;
        planet = WorldValidation.ValidatePlanet(planet);
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