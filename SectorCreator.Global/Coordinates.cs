namespace SectorCreator.Global;

public class Coordinates
{
    public int X { get; set; }
    public int Y { get; set; }

    public Coordinates()
    {
        X = 0;
        Y = 0;
    }
    
    public Coordinates(int x, int y)
    {
        X = x;
        Y = y;
    }

    public static bool operator ==(Coordinates coordinates1, Coordinates coordinates2)
    {
        return coordinates1.X == coordinates2.X && coordinates1.Y == coordinates2.Y;
    }
    
    public static bool operator !=(Coordinates coordinates1, Coordinates coordinates2)
    {
        return coordinates1.X != coordinates2.X || coordinates1.Y != coordinates2.Y;
    }

    public override string ToString()
    {
        var coordinateString = "";

        if (X <= 9) {
            coordinateString += $"0{X}";
        } else {
            coordinateString += X;
        }
        if (Y <= 9) {
            coordinateString += $"0{Y}";
        } else {
            coordinateString += Y;
        }

        return coordinateString;
    }
}