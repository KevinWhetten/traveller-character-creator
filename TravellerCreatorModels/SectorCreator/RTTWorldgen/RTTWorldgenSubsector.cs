using TravellerCreatorModels.SectorCreator.Interfaces;
using TravellerCreatorModels.SectorCreator.Other;

namespace TravellerCreatorModels.SectorCreator.RTTWorldgen;

public class RTTWorldgenSubsector : ISubsector
{
    public Coordinates? Coordinates { get; set; }
    public List<IHex> Hexes { get; set; } = new();
}