using NUnit.Framework;
using SectorCreator.Models.Basic;

namespace SectorCreator.Models.Tests.Basic;

[TestFixture]
public class HexTests
{
    [Test]
    public void WhenConstructing()
    {
        var hex = new Hex();
        
        Assert.That(hex.Coordinates.X, Is.EqualTo(0));
        Assert.That(hex.Coordinates.Y, Is.EqualTo(0));
        Assert.That(hex.StarSystems.Count, Is.EqualTo(0));
    }
}