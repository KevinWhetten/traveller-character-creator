using SectorCreator.Global;

namespace SectorCreator.RttWorldgen.Models;

public class Subsector : SectorCreator.Models.Basic.Subsector
{
    public Subsector(Coordinates coordinates)
    {
        Coordinates = coordinates;

        for (int i = 0; i < 8; i++) {
            for (int j = 0; j < 10; j++) {
                var hexCoordinates = getHexCoordinates(Coordinates, i, j);
                Hexes.Add(new Hex());
            }
        }
    }

    private Coordinates getHexCoordinates(Coordinates subsectorCoordinates, int newX, int newY)
    {
        var x = (subsectorCoordinates.X - 1) * 8 + newX;
        var y = (subsectorCoordinates.Y - 1) * 10 + newY;
        return new Coordinates(x, y);
    }
}