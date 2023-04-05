using TravellerCreatorModels.SectorCreator.Other;

namespace TravellerCreatorModels.SectorCreator.Interfaces;

public interface IHex
{
    Coordinates Coordinates { get; set; }
    List<IStarSystem> StarSystems { get; set; }
}