using TravellerCreatorModels.SectorCreator.Enums;
using TravellerCreatorModels.SectorCreator.Interfaces;

namespace TravellerCreatorModels.SectorCreator.Mongoose;

public class MongooseSector : ISector
{
    public SectorType SectorType => SectorType.Basic;
    public List<ISubsector> Subsectors { get; set; } = new();
}