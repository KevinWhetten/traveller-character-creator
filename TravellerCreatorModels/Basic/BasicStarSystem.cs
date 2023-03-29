using TravellerCreatorGlobalMethods;
using TravellerCreatorModels.Enums;
using TravellerCreatorModels.Interfaces;

namespace TravellerCreatorModels.Basic;

public class BasicStarSystem : IStarSystem
{
    public List<IStar> Stars { get; set; } = new();
    public StarSystemType Type { get; set; } = StarSystemType.Basic;
    public IPlanet? Planet { get; set; }
    public List<IPlanet> Planets { get; set; } = new();
    public bool GasGiant { get; set; }

    public void GetGasGiant()
    {
        if (Roll.D6(2) > 5) {
            GasGiant = true;
        } else if (Roll.D6(2) < 5) {
            GasGiant = true;
        }
    }
}