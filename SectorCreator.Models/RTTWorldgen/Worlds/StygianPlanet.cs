using SectorCreator.Global;

namespace SectorCreator.Models.RTTWorldgen.Worlds;

public interface IStygianPlanet
{
    RttWorldgenPlanet Generate(RttWorldgenPlanet planet);
}

public class StygianPlanet : IStygianPlanet
{
    private readonly IRollingService _rollingService;
    private readonly IWorldValidation _worldValidation;

    public StygianPlanet(IRollingService rollingService, IWorldValidation worldValidation)
    {
        _rollingService = rollingService;
        _worldValidation = worldValidation;
    }

    public RttWorldgenPlanet Generate(RttWorldgenPlanet planet)
    {
        planet.Size = _rollingService.D6(1) - 1;
        planet.Atmosphere = 0;
        planet.Hydrographics = 0;
        planet.Biosphere = 0;
        planet = _worldValidation.ValidatePlanet(planet);
        return planet;
    }
}