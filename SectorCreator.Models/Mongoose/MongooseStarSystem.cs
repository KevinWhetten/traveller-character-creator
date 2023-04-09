using SectorCreator.Global;
using SectorCreator.Global.Enums;
using SectorCreator.Models.Basic;

namespace SectorCreator.Models.Mongoose;

public class MongooseStarSystem : StarSystem
{
  public MongooseStarSystem(SectorType sectorType)
  {
    Planets = new List<Planet>();

    if (Roll.D6(1) >= 4)
    {
      var planet = PlanetFactory.Generate(sectorType);
      Planets.Add(planet);
    }

    GetGasGiant();
  }

  private void GetGasGiant()
  {
    if (Roll.D6(2) > 5)
    {
      GasGiant = true;
    }
    else if (Roll.D6(2) < 5)
    {
      GasGiant = true;
    }
  }
}
