using SectorCreator.Global;
using SectorCreator.Global.Enums;

namespace SectorCreator.Models.RTTWorldgen.Worlds;

public interface IJaniLithicWorld
{
    RttWorldgenPlanet Generate(RttWorldgenPlanet planet);
}

public class JaniLithicWorld : IJaniLithicWorld
{
    private readonly IRollingService _rollingService;
    
    public JaniLithicWorld(IRollingService rollingService)
    {
        _rollingService = rollingService;
    }

    public RttWorldgenPlanet Generate(RttWorldgenPlanet planet)
    {
        planet.WorldType = WorldType.JaniLithic;
        planet.Size = _rollingService.D6(1) + 4;
        planet.Atmosphere = GetAtmosphere();
        planet.Hydrographics = 0;
        planet.Biosphere = 0;
        planet = WorldValidation.ValidatePlanet(planet);
        return planet;
    }

    private int GetAtmosphere()
    {
        return _rollingService.D6(1) switch {
            (<= 3) => 1,
            _ => 10
        };
    }
}