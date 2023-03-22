namespace TravellerCreatorModels.Basic;

public class Subsector
{
    public Coordinates Coordinates { get; set; }
    public List<Hex> Hexes { get; set; }

    public Subsector(int x, int y)
    {
        Coordinates = new Coordinates(x, y);
        Hexes = new List<Hex>();
    }

    public Subsector()
    {
        Coordinates = new Coordinates(0, 0);
        Hexes = new List<Hex>();
    }
}