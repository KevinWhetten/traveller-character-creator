using TravellerCreatorModels.SectorCreator.Interfaces;
using TravellerCreatorModels.SectorCreator.Other;

namespace TravellerCreatorModels.SectorCreator.Mongoose;

public class MongooseHex: IHex
{
    public Coordinates Coordinates { get; set; }
    public List<IStarSystem> StarSystems { get; set; } = new();

    public MongooseHex(Coordinates coordinates)
    {
        Coordinates = coordinates;
    }
}