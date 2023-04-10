using SectorCreator.Global;

namespace SectorCreator.Models.RTTWorldgen.Planets;

public interface IMeltballPlanet
{
    RttWorldgenPlanet Generate(RttWorldgenPlanet planet);
}

public class MeltballPlanet : IMeltballPlanet
{
    private readonly IRollingService _rollingService;
    private readonly IPlanetValidation _planetValidation;

    public MeltballPlanet(IRollingService rollingService, IPlanetValidation planetValidation)
    {
        _rollingService = rollingService;
        _planetValidation = planetValidation;
    }

    public RttWorldgenPlanet Generate(RttWorldgenPlanet planet)
    {
        planet.Size = _rollingService.D6(1) - 1;
        planet.Atmosphere = 1;
        planet.Hydrographics = 15;
        planet.Biosphere = 0;
        planet = _planetValidation.ValidatePlanet(planet);
        return planet;
    }
}