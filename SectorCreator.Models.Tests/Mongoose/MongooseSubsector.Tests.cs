using NUnit.Framework;
using SectorCreator.Global;
using SectorCreator.Global.Enums;
using SectorCreator.Models.Mongoose;

namespace SectorCreator.Models.Tests.Mongoose;

[TestFixture]
public class MongooseSubsectorTests
{
    [Test]
    public void WhenConstructing()
    {
        var subsector = new MongooseSubsector(new Coordinates(1, 1), SectorType.Basic);
        
        Assert.That(subsector.Coordinates.X, Is.EqualTo(1));
        Assert.That(subsector.Coordinates.Y, Is.EqualTo(1));
        Assert.That(subsector.Hexes.Count, Is.EqualTo(80));
    }
}