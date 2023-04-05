using TravellerCreatorModels.SectorCreator.Interfaces;
using TravellerCreatorModels.SectorCreator.Other;

namespace TravellerCreatorModels.SectorCreator.Mongoose;

public class MongooseSubsector : ISubsector
{
    public Coordinates? Coordinates { get; set; }
    public List<IHex> Hexes { get; set; }

    public MongooseSubsector(Coordinates coordinates)
    {
        Coordinates = coordinates;
        Hexes = new List<IHex>();
    }

    public MongooseSubsector()
    {
        Coordinates = new Coordinates(0, 0);
        Hexes = new List<IHex>();
    }
}