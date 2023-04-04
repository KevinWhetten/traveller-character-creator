using TravellerCreatorModels.Interfaces;
using TravellerCreatorModels.Other;

namespace TravellerCreatorModels.RTTWorldgen;

public class RTTWorldgenSubsector : ISubsector
{
    public Coordinates? Coordinates { get; set; }
    public List<IHex> Hexes { get; set; } = new();
}