namespace TravellerCreatorModels.Basic;

public class SubSector
{
    public Coordinates Coordinates { get; set; }
    public List<Hex> Hexes { get; set; }

    public SubSector(int x, int y)
    {
        Coordinates = new Coordinates(x, y);
        Hexes = new List<Hex>();
    }

    public SubSector()
    {
        Coordinates = new Coordinates(0, 0);
        Hexes = new List<Hex>();
    }
}