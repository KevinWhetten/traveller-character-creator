namespace SectorCreator.WorldBuilder.Tests;

public class WorldBuilderSectorTests
{
    [Test]
    public void GenerateWorldBuilderSector()
    {
        var TSV = "";
        var sector = new WorldBuilderSector();

        var populatedPlanets = new List<string>();

        foreach (var hex in sector.Subsectors.SelectMany(subsector => subsector.Hexes)) {
            TSV += $"{hex.Coordinates}\n";
            TSV += hex.GetStarDetails();
            TSV += hex.GetPlanetDetails();
            populatedPlanets.AddRange(hex.Planets.Where(x => x is {TechLevel: >= 10}).Select(x => x.Details));
        }

        var data = "Hex\tName\tUWP\tRemarks\t{Ix}\t(Ex)\t[Cx]\tN\tB\tZ\tPBG\tW\tAllegiance\tStellar\n";
        data += string.Join("", populatedPlanets);

        Assert.That(true);
    }
}