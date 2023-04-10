using NUnit.Framework;
using SectorCreator.Global;
using SectorCreator.Global.Enums;
using SectorCreator.Models.Mongoose;

namespace SectorCreator.Models.Tests.Mongoose;

[TestFixture]
public class MongooseHexTests
{
    [Test]
    public void WhenConstructing()
    {
        var hex = new MongooseHex(new Coordinates(1, 3), new Coordinates(3, 4), SectorType.Basic);

        Assert.That(hex.Coordinates.X, Is.EqualTo(17));
        Assert.That(hex.Coordinates.Y, Is.EqualTo(33));
        Assert.That(hex.StarSystems.Count, Is.EqualTo(1));
    }
}