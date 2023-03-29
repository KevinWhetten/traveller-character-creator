using TravellerCreatorModels.Enums;
using TravellerCreatorModels.Interfaces;

namespace TravellerCreatorModels.Basic;

public class BasicSector : ISector
{
    public SectorType SectorType => SectorType.Basic;
    public List<ISubsector> Subsectors { get; set; } = new();
}