using SectorCreator.Global;
using SectorCreator.Global.Enums;

namespace SectorCreator.Models.RTTWorldgen.Worlds;

public interface IAcheronianWorld
{
    RttWorldgenPlanet Generate(RttWorldgenPlanet planet);
}

public class AcheronianWorld : IAcheronianWorld
{
    private readonly IRollingService _rollingService;

    public AcheronianWorld(IRollingService rollingService)
    {
        _rollingService = rollingService;
    }

    public RttWorldgenPlanet Generate(RttWorldgenPlanet planet)
    {
        planet.WorldType = WorldType.Acheronian;
        planet.Size = _rollingService.D6(1) + 4;
        planet.Atmosphere = 1;
        planet.Hydrographics = 0;
        planet.Biosphere = 0;
        planet = WorldValidation.ValidatePlanet(planet);
        return planet;
    }
}