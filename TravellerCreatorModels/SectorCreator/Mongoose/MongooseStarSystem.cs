using TravellerCreatorGlobalMethods;
using TravellerCreatorModels.SectorCreator.Enums;
using TravellerCreatorModels.SectorCreator.Interfaces;

namespace TravellerCreatorModels.SectorCreator.Mongoose;

public class MongooseStarSystem : IStarSystem
{
    public StarSystemType Type { get; set; } = StarSystemType.Basic;
    public bool GasGiant { get; set; }
    public List<IPlanet> Planets { get; set; } = new();
    public List<IStar> Stars { get; set; } = new();

    public void GetGasGiant()
    {
        if (Roll.D6(2) > 5) {
            GasGiant = true;
        } else if (Roll.D6(2) < 5) {
            GasGiant = true;
        }
    }
}