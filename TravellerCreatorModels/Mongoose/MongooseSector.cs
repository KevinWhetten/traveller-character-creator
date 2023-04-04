using TravellerCreatorModels.Enums;
using TravellerCreatorModels.Interfaces;

namespace TravellerCreatorModels.Mongoose;

public class MongooseSector : ISector
{
    public SectorType SectorType => SectorType.Basic;
    public List<ISubsector> Subsectors { get; set; } = new();
}