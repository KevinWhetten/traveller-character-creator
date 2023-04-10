using SectorCreator.Global;

namespace SectorCreator.Models.Basic;

public class Hex
{
    public Coordinates Coordinates { get; set; } = new();
    public List<StarSystem> StarSystems { get; } = new();
}