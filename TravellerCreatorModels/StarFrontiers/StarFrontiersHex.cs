using TravellerCreatorModels.Interfaces;
using TravellerCreatorModels.Other;

namespace TravellerCreatorModels.StarFrontiers;

public class StarFrontiersHex : IHex
{
    public Coordinates Coordinates { get; set; }
    public List<IStarSystem> StarSystems { get; set; } = new();

    public StarFrontiersHex(Coordinates coordinates)
    {
        Coordinates = coordinates;
    }
}