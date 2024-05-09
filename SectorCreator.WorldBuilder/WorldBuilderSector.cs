using SectorCreator.Global;

namespace SectorCreator.WorldBuilder;

public class WorldBuilderSector
{
    public Guid Id = Guid.NewGuid();
    public string Name { get; set; } = "";
    public List<WorldBuilderSubsector> Subsectors { get; set; } = new();

    public WorldBuilderSector()
    {
        for (var i = 0; i < 4; i++) {
            for (var j = 0; j < 4; j++) {
                Subsectors.Add(new WorldBuilderSubsector(new Coordinates(i + 1, j + 1)));
            }
        }
    }
}