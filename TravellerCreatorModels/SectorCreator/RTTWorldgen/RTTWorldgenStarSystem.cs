using TravellerCreatorModels.SectorCreator.Enums;
using TravellerCreatorModels.SectorCreator.Interfaces;

namespace TravellerCreatorModels.SectorCreator.RTTWorldgen;

public class RTTWorldgenStarSystem : IStarSystem
{
    public List<IStar> Stars { get; set; } = new();
    public StarSystemType Type { get; set; }
    
    public bool GasGiant { get; set; }
    public List<IPlanet> Planets { get; set; } = new();
}