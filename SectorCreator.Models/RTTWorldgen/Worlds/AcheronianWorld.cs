using SectorCreator.Global;

namespace SectorCreator.Models.RTTWorldgen.Worlds;

public interface IAcheronianWorld
{
    RttWorldgenPlanet Generate(RttWorldgenPlanet planet);
}

public class AcheronianWorld : IAcheronianWorld
{
    private readonly IRollingService _rollingService;
    private readonly IPlanetValidation _planetValidation;

    public AcheronianWorld(IRollingService rollingService, IPlanetValidation planetValidation)
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