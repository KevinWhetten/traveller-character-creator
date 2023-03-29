using TravellerCreatorModels.Other;

namespace TravellerCreatorModels.Interfaces;

public interface IHex
{
    Coordinates Coordinates { get; set; }
    List<IStarSystem> StarSystems { get; set; }
}