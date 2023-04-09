using SectorCreator.Global;
using SectorCreator.Models.Basic;

namespace SectorCreator.Models.StarFrontiers;

public class StarFrontiersStarSystem : StarSystem
{
  public StarFrontiersStarSystem()
  {
    var numStars = Roll.D10(1) switch
    {
      (<= 7) => 1,
      _ => 2
    };

    for (var i = 0; i < numStars; i++)
    {
      // Magnetar 1.0%
      // Magnetar&Pulsar 0.2 %
      // Pulsar = 98.8%
    }
    
    if (Roll.D6(1) >= 4)
    {
      var planet = new StarFrontiersPlanet(StarFrontiersPlanetFactory.Generate());
      Planets.Add(planet);
    }
  }
}