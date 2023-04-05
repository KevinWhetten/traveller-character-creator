using TravellerCreatorModels.SectorCreator.Interfaces;
using TravellerCreatorModels.SectorCreator.Other;

namespace TravellerCreatorModels.SectorCreator.StarFrontiers;

public class StarFrontiersHex : IHex
{
    public Coordinates Coordinates { get; set; }
    public List<IStarSystem> StarSystems { get; set; } = new();

    public StarFrontiersHex(Coordinates coordinates)
    {
        Coordinates = coordinates;
    }
}