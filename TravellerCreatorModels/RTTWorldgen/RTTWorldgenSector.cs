using TravellerCreatorModels.Enums;
using TravellerCreatorModels.Interfaces;

namespace TravellerCreatorModels.RTTWorldgen;

public class RTTWorldgenSector : ISector
{
    public SectorType SectorType => SectorType.RTTWorldgen;
    public List<ISubsector> Subsectors { get; set; } = new();
}