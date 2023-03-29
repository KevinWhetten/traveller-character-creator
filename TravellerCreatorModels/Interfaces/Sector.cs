using TravellerCreatorModels.Enums;

namespace TravellerCreatorModels.Interfaces;

public interface ISector
{
    SectorType SectorType { get; }
    List<ISubsector> Subsectors { get; set; }
}