using TravellerCreatorModels.SectorCreator.Enums;

namespace TravellerCreatorModels.SectorCreator.Interfaces;

public interface ISector
{
    SectorType SectorType { get; }
    List<ISubsector> Subsectors { get; set; }
}