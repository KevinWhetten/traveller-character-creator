using SectorCreator.Global;
using SectorCreator.Models.Base;

namespace SectorCreator.Models.RTTWorldgen;

public class RttWorldgenSubsector : Subsector
{
  public void Generate(Coordinates coordinates)
  {
    Coordinates = coordinates;

    for (var y = 1; y <= 10; y++)
    {
      for (var x = 1; x <= 8; x++)
      {
        var newHex = new RttWorldgenHex();
        newHex.Generate(Coordinates, new Coordinates(x, y));
        Hexes.Add(newHex);
      }
    }
  }
}
