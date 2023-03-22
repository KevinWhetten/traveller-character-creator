namespace TravellerCreatorModels.Basic;

public class Hex
{
    private Coordinates Coordinates { get; set; }
    public Planet? Planet { get; set; }

    public Hex(Coordinates coordinates)
    {
        Coordinates = coordinates;
        Planet = new Planet();
    }
}