using SectorCreator.Global;

namespace SectorCreator.Models.RTTWorldgen.Planets;

public interface IAcheronianPlanet
{
    RttWorldgenPlanet Generate(RttWorldgenPlanet planet);
}

public class AcheronianPlanet : IAcheronianPlanet
{
    private readonly IRollingService _rollingService;
    private readonly IPlanetValidation _planetValidation;

    public AcheronianPlanet(IRollingService rollingService, IPlanetValidation planetValidation)
    {
        _rollingService = rollingService;
        _planetValidation = planetValidation;
    }

    public RttWorldgenPlanet Generate(RttWorldgenPlanet planet)
    {
        planet.Size = _rollingService.D6(1) + 4;
        planet.Atmosphere = 1;
        planet.Hydrographics = 0;
        planet.Biosphere = 0;
        planet = _planetValidation.ValidatePlanet(planet);
        return planet;
    }
}