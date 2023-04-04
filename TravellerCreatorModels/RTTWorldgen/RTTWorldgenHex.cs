using TravellerCreatorModels.Interfaces;
using TravellerCreatorModels.Other;

namespace TravellerCreatorModels.RTTWorldgen;

public class RTTWorldgenHex : IHex
{
    public Coordinates Coordinates { get; set; }
    public List<IStarSystem> StarSystems { get; set; } = new();

    public RTTWorldgenHex(Coordinates coordinates)
    {
        Coordinates = coordinates;
    }
}