using TravellerCreatorModels.Interfaces;
using TravellerCreatorModels.Other;

namespace TravellerCreatorModels.Basic;

public class BasicHex: IHex
{
    public Coordinates Coordinates { get; set; }
    public List<IStarSystem> StarSystems { get; set; } = new();

    public BasicHex(Coordinates coordinates)
    {
        Coordinates = coordinates;
    }
}