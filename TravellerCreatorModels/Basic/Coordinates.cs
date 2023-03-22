namespace TravellerCreatorModels.Basic;

public class Coordinates
{
    private int x { get; set; }
    private int y { get; set; }
    
    public Coordinates(int x, int y)
    {
        this.x = x;
        this.y = y;
    }
}