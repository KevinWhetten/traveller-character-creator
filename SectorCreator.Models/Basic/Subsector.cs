using SectorCreator.Global;

namespace SectorCreator.Models.Basic;

public class Subsector
{
    public Coordinates Coordinates { get; set; } = new();
    public List<Hex> Hexes { get; set; } = new();
}