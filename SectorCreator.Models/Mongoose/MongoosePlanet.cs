

using SectorCreator.Models.Basic;

namespace SectorCreator.Models.Mongoose;

public class MongoosePlanet : Planet
{
  public MongoosePlanet()
  { }

  public MongoosePlanet(MongoosePlanet planet) : base(planet)
  { }
}