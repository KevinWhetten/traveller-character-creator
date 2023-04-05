using TravellerCreatorModels.SectorCreator.Enums;
using TravellerCreatorModels.SectorCreator.Interfaces;

namespace TravellerCreatorModels.SectorCreator.StarFrontiers;

public class StarFrontiersSector : ISector
{
    public SectorType SectorType => SectorType.StarFrontiers;
    public List<ISubsector> Subsectors { get; set; } = new();
}