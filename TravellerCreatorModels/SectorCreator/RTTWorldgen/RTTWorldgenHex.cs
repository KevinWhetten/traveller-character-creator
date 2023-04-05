using TravellerCreatorModels.SectorCreator.Interfaces;
using TravellerCreatorModels.SectorCreator.Other;

namespace TravellerCreatorModels.SectorCreator.RTTWorldgen;

public class RTTWorldgenHex : IHex
{
    public Coordinates Coordinates { get; set; }
    public List<IStarSystem> StarSystems { get; set; } = new();

    public RTTWorldgenHex(Coordinates coordinates)
    {
        Coordinates = coordinates;
    }
}