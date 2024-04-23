using SectorCreator.Global;
using SectorCreator.Global.Enums;

namespace SectorCreator.Models.RTTWorldgen.Worlds;

public interface IHebeanWorld
{
    RttWorldgenPlanet Generate(RttWorldgenPlanet planet);
}

public class HebeanWorld : IHebeanWorld
{
    private readonly IRollingService _rollingService;
    
    public HebeanWorld(IRollingService rollingService)
    {
        _rollingService = rollingService;
    }

    public RttWorldgenPlanet Generate(RttWorldgenPlanet planet)
    {
        planet.WorldType = WorldType.Hebean;
        planet.Size = _rollingService.D6(1) - 1;
        planet.Atmosphere = GetAtmosphere(planet);
        planet.Hydrographics = _rollingService.D6(2) + planet.Size - 11;
        planet.Biosphere = 0;
        planet = WorldValidation.ValidatePlanet(planet);
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