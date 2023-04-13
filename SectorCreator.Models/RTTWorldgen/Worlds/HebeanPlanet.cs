using SectorCreator.Global;

namespace SectorCreator.Models.RTTWorldgen.Worlds;

public interface IHebeanPlanet
{
    RttWorldgenPlanet Generate(RttWorldgenPlanet planet);
}

public class HebeanPlanet : IHebeanPlanet
{
    private readonly IRollingService _rollingService;
    private readonly IPlanetValidation _planetValidation;

    public HebeanPlanet(IRollingService rollingService, IPlanetValidation planetValidation)
    {
        _rollingService = rollingService;
        _planetValidation = planetValidation;
    }
    
    public RttWorldgenPlanet Generate(RttWorldgenPlanet planet)
    {
        planet.Size = _rollingService.D6(1) - 1;
        planet.Atmosphere = GetAtmosphere(planet);
        planet.Hydrographics = _rollingService.D6(2) + planet.Size - 11;
        planet.Biosphere = 0;
        planet = _planetValidation.ValidatePlanet(planet);
        return planet;
    }

    private int GetAtmosphere(RttWorldgenPlanet planet)
    {
        var atmosphere = _rollingService.D6(1) + planet.Size - 6;
        if (atmosphere >= 2) {
            atmosphere = 10;
        }

        return atmosphere;
    }
}