using SectorCreator.Global;

namespace SectorCreator.WorldBuilder;

public class WorldBuilderSubsector
{
    public List<WorldBuilderHex> Hexes { get; set; } = new();
    public Coordinates Coordinates { get; set; }
    
    public WorldBuilderSubsector(Coordinates coordinates)
    {
        Coordinates = coordinates;
        for (var i = 0; i < 8; i++) {
            for (var j = 0; j < 10; j++) {
                Hexes.Add(new WorldBuilderHex(new RollingService(), GetHexCoordinates(i, j)));
            }
        }
    }


    private Coordinates GetHexCoordinates(int i, int j)
    {
        var x = (Coordinates.X - 1) * 8 + i + 1;
        var y = (Coordinates.Y - 1) * 10 + j + 1;
        return new Coordinates(x, y);
    }
}