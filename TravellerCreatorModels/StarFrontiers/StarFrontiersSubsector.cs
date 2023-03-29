using TravellerCreatorModels.Interfaces;
using TravellerCreatorModels.Other;

namespace TravellerCreatorModels.StarFrontiers;

public class StarFrontiersSubsector : ISubsector
{
    public Coordinates? Coordinates { get; set; }
    public List<IHex> Hexes { get; set; } = new();
}