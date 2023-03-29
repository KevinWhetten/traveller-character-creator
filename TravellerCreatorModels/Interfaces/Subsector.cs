using TravellerCreatorModels.Other;

namespace TravellerCreatorModels.Interfaces;

public interface ISubsector
{
    Coordinates? Coordinates { get; set; }
    List<IHex> Hexes { get; set; }
}