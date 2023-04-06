using SectorCreator.Global;

namespace SectorCreator.Models.Basic;

public class Hex
{
    public Coordinates Coordinates { get; set; } = new();
    public List<StarSystem> StarSystems { get; set; } = new();


    public void SetCoordinates(Coordinates subsectorCoordinates, Coordinates hexCoordinates)
    {
        Coordinates.X = hexCoordinates.X + 8 * ((subsectorCoordinates.X - 1) % 8);
        Coordinates.Y = hexCoordinates.Y + 10 * ((subsectorCoordinates.Y - 1) % 10);
    }
}