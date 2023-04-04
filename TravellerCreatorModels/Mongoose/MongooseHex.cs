using TravellerCreatorModels.Interfaces;
using TravellerCreatorModels.Other;

namespace TravellerCreatorModels.Mongoose;

public class MongooseHex: IHex
{
    public Coordinates Coordinates { get; set; }
    public List<IStarSystem> StarSystems { get; set; } = new();

    public MongooseHex(Coordinates coordinates)
    {
        Coordinates = coordinates;
    }
}