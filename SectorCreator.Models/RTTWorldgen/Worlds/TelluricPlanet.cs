using SectorCreator.Global;

namespace SectorCreator.Models.RTTWorldgen.Worlds;

public interface ITelluricPlanet
{
    RttWorldgenPlanet Generate(RttWorldgenPlanet planet);
}

public class TelluricPlanet : ITelluricPlanet
{
    private readonly IRollingService _rollingService;
    private readonly IWorldValidation _worldValidation;

    public TelluricPlanet(IRollingService rollingService, IWorldValidation worldValidation)
    {
        _rollingService = rollingService;
        _worldValidation = worldValidation;
    }
    
    public RttWorldgenPlanet Generate(RttWorldgenPlanet planet)
    {
        planet.Size = _rollingService.D6(1) + 4;
        planet.Atmosphere = 12;
        planet.Hydrographics = GetHydrographics();
        planet.Biosphere = 0;
        planet = _worldValidation.ValidatePlanet(planet);
        return planet;
    }

    private int GetHydrographics()
    {
        return _rollingService.D6(1) switch {
                    (<= 4) => 0,
                    (<= 6) => 15,
                    _ => 0
                };
    }
}