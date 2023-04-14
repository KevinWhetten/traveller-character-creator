using SectorCreator.Global;

namespace SectorCreator.Models.RTTWorldgen.Worlds;

public interface IJaniLithicWorld
{
    RttWorldgenPlanet Generate(RttWorldgenPlanet planet);
}

public class JaniLithicWorld : IJaniLithicWorld
{
    private readonly IRollingService _rollingService;
    private readonly IWorldValidation _worldValidation;

    public JaniLithicWorld(IRollingService rollingService, IWorldValidation worldValidation)
    {
        _rollingService = rollingService;
        _worldValidation = worldValidation;
    }

    public RttWorldgenPlanet Generate(RttWorldgenPlanet planet)
    {
        planet.Size = _rollingService.D6(1) + 4;
        planet.Atmosphere = GetAtmosphere();
        planet.Hydrographics = 0;
        planet.Biosphere = 0;
        planet = _worldValidation.ValidatePlanet(planet);
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