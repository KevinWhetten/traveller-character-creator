using TravellerCreatorModels.Interfaces;
using TravellerCreatorModels.Other;

namespace TravellerCreatorModels.Basic;

public class BasicSubsector : ISubsector
{
    public Coordinates? Coordinates { get; set; }
    public List<IHex> Hexes { get; set; }

    public BasicSubsector(Coordinates coordinates)
    {
        Coordinates = coordinates;
        Hexes = new List<IHex>();
    }

    public BasicSubsector()
    {
        Coordinates = new Coordinates(0, 0);
        Hexes = new List<IHex>();
    }
}