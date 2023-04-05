using TravellerCreatorModels.SectorCreator.Enums;
using TravellerCreatorModels.SectorCreator.Interfaces;

namespace TravellerCreatorModels.SectorCreator.RTTWorldgen;

public class RTTWorldgenSector : ISector
{
    public SectorType SectorType => SectorType.RTTWorldgen;
    public List<ISubsector> Subsectors { get; set; } = new();
}