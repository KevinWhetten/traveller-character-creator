using SectorCreator.Global;
using SectorCreator.Global.Enums;

namespace SectorCreator.Models.RTTWorldgen.Worlds;

public interface IMeltballWorld
{
    RttWorldgenPlanet Generate(RttWorldgenPlanet planet);
}

public class MeltballWorld : IMeltballWorld
{
    private readonly IRollingService _rollingService;

    public MeltballWorld(IRollingService rollingService)
    {
        _rollingService = rollingService;
    }

    public RttWorldgenPlanet Generate(RttWorldgenPlanet planet)
    {
        planet.WorldType = WorldType.Meltball;
        planet.Size = _rollingService.D6(1) - 1;
        planet.Atmosphere = 1;
        planet.Hydrographics = 15;
        planet.Biosphere = 0;
        planet = WorldValidation.ValidatePlanet(planet);
        return planet;
    }
}