using SectorCreator.Global.Enums;

namespace SectorCreator.Models.Base;

public class Sector
{
    public SectorType SectorType { get; set; }
    public List<Subsector> Subsectors { get; set; } = new();
}