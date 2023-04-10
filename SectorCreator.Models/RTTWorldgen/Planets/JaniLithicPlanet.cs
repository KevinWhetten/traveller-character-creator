using SectorCreator.Global;

namespace SectorCreator.Models.RTTWorldgen.Planets;

public interface IJaniLithicPlanet
{
    RttWorldgenPlanet Generate(RttWorldgenPlanet planet);
}

public class JaniLithicPlanet : IJaniLithicPlanet
{
    private readonly IRollingService _rollingService;
    private readonly IPlanetValidation _planetValidation;

    public JaniLithicPlanet(IRollingService rollingService, IPlanetValidation planetValidation)
    {
        _rollingService = rollingService;
        _planetValidation = planetValidation;
    }

    public RttWorldgenPlanet Generate(RttWorldgenPlanet planet)
    {
        planet.Size = _rollingService.D6(1) + 4;
        planet.Atmosphere = GetAtmosphere();
        planet.Hydrographics = 0;
        planet.Biosphere = 0;
        planet = _planetValidation.ValidatePlanet(planet);
        return planet;
    }

    private int GetAtmosphere()
    {
        return _rollingService.D6(1) switch {
            (<= 3) => 1,
            (<= 6) => 10,
            _ => 0
        };
    }
}