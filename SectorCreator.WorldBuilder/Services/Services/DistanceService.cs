using SectorCreator.Global;

namespace SectorCreator.Models.Services;

public static class DistanceService
{
    public static int DistanceBetween(Coordinates planet1Coordinates, Coordinates planet2Coordinates)
    {
        var a0 = planet1Coordinates.X;
        var b0 = planet1Coordinates.Y;
        var a1 = planet2Coordinates.X;
        var b1 = planet2Coordinates.Y;

        var x0 = a0 - (int) Math.Floor((double)b0 / 2);
        var y0 = b0;
        var x1 = a1 - (int) Math.Floor((double)b1 / 2);
        var y1 = b1;
        var dx = x1 - x0;
        var dy = y1 - y0;

        var value1 = Math.Abs(dx);
        var value2 = Math.Abs(dy);
        var value3 = Math.Abs(dx + dy);

        var values = new List<int> {value1, value2, value3};

        return values.Max();
    }
}