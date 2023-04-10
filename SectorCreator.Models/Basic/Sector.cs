using SectorCreator.Global.Enums;

namespace SectorCreator.Models.Basic;

public class Sector
{
    public SectorType SectorType { get; protected init; }
    public List<Subsector> Subsectors { get; } = new();
}