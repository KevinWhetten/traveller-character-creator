using TravellerCreatorModels.Enums;
using TravellerCreatorModels.Interfaces;

namespace TravellerCreatorModels.StarFrontiers;

public class StarFrontiersSector : ISector
{
    public SectorType SectorType => SectorType.StarFrontiers;
    public List<ISubsector> Subsectors { get; set; } = new();
}