using TravellerCreatorModels.Enums;

namespace TravellerCreatorModels.Interfaces;

public interface IStarSystem
{
    public List<IStar> Stars { get; set; }
    public StarSystemType Type { get; set; }
    public bool GasGiant { get; set; }
    public List<IPlanet> Planets { get; set; }
}