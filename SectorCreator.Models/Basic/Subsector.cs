using SectorCreator.Global;

namespace SectorCreator.Models.Basic;

public class Subsector
{
    protected Coordinates? Coordinates { get; set; }
    public List<Hex> Hexes { get; set; } = new();
}