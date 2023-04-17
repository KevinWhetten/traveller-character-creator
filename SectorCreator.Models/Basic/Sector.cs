using SectorCreator.Global.Enums;

namespace SectorCreator.Models.Basic;

public class Sector
{
    public List<Subsector> Subsectors { get; } = new();
}