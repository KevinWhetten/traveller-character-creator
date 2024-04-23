using SectorCreator.Global;
using SectorCreator.Models.RTTWorldgen;

namespace SectorCreator.Models.Basic;

public class Hex
{
    public Coordinates Coordinates { get; set; } = new();
    public List<StarSystem> StarSystems { get; } = new();
    public RttWorldgenPlanet MostImportantPlanet { get; set; } = new(new RollingService());
}