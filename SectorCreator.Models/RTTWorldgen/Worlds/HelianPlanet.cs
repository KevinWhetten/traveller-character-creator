using SectorCreator.Global;

namespace SectorCreator.Models.RTTWorldgen.Worlds;

public interface IHelianPlanet
{
    RttWorldgenPlanet Generate(RttWorldgenPlanet planet);
}

public class HelianPlanet : IHelianPlanet
{
    private readonly IRollingService _rollingService;
    private readonly IPlanetValidation _planetValidation;

    public HelianPlanet(IRollingService rollingService, IPlanetValidation planetValidation)
    {
        _rollingService = rollingService;
        _planetValidation = planetValidation;
    }

    public RttWorldgenPlanet Generate(RttWorldgenPlanet planet)
    {
        planet.Size = _rollingService.D6(1) + 9;
        planet.Atmosphere = 13;
        planet.Hydrographics = GetHydrographics();
        planet.Biosphere = 0;
        planet = _planetValidation.ValidatePlanet(planet);
        return planet;
    }

    private int GetHydrographics()
    {
        return _rollingService.D6(1) switch {
            (<= 2) => 0,
            (<= 4) => _rollingService.D6(2) - 1,
            (<= 6) => 15,
            _ => 0
        };
    }
}