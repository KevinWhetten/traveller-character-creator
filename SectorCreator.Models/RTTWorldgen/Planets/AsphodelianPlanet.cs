using SectorCreator.Global;

namespace SectorCreator.Models.RTTWorldgen.Planets;

public interface IAsphodelianPlanet
{
    RttWorldgenPlanet Generate(RttWorldgenPlanet planet);
}

public class AsphodelianPlanet : IAsphodelianPlanet
{
    private readonly IRollingService _rollingService;
    private readonly IPlanetValidation _planetValidation;

    public AsphodelianPlanet(IRollingService rollingService, IPlanetValidation planetValidation)
    {
        _rollingService = rollingService;
        _planetValidation = planetValidation;
    }

    public RttWorldgenPlanet Generate(RttWorldgenPlanet planet)
    {
        planet.Size = _rollingService.D6(1) + 9;
        planet.Atmosphere = 1;
        planet.Hydrographics = 0;
        planet.Biosphere = 0;
        planet = _planetValidation.ValidatePlanet(planet);
        return planet;
    }
}