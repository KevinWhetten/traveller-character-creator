using SectorCreator.Global;
using SectorCreator.Global.Enums;

namespace SectorCreator.Models.RTTWorldgen.Worlds;

public interface ITelluricWorld
{
    RttWorldgenPlanet Generate(RttWorldgenPlanet planet);
}

public class TelluricWorld : ITelluricWorld
{
    private readonly IRollingService _rollingService;

    public TelluricWorld(IRollingService rollingService)
    {
        _rollingService = rollingService;
    }
    
    public RttWorldgenPlanet Generate(RttWorldgenPlanet planet)
    {
        planet.WorldType = WorldType.Telluric;
        planet.Size = _rollingService.D6(1) + 4;
        planet.Atmosphere = 12;
        planet.Hydrographics = GetHydrographics();
        planet.Biosphere = 0;
        planet = WorldValidation.ValidatePlanet(planet);
        return planet;
    }

    private int GetHydrographics()
    {
        return _rollingService.D6(1) switch {
                    (<= 4) => 0,
                    (>= 5) => 15
                };
    }
}