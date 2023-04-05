using TravellerCreatorModels.SectorCreator.Other;

namespace TravellerCreatorModels.SectorCreator.Interfaces;

public interface ISubsector
{
    Coordinates? Coordinates { get; set; }
    List<IHex> Hexes { get; set; }
}