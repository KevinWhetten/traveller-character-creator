using TravellerCreatorModels.SectorCreator.Interfaces;
using TravellerCreatorModels.SectorCreator.Other;

namespace TravellerCreatorModels.SectorCreator.StarFrontiers;

public class StarFrontiersSubsector : ISubsector
{
    public Coordinates? Coordinates { get; set; }
    public List<IHex> Hexes { get; set; } = new();
}