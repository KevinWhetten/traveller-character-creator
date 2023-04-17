using NUnit.Framework;
using SectorCreator.Models.Basic;

namespace SectorCreator.Models.Tests.Basic;

[TestFixture]
public class SubsectorTests
{
    [Test]
    public void WhenConstructing()
    {
        var subsector = new Subsector();
        
        Assert.That(subsector.Coordinates.X, Is.EqualTo(0));
        Assert.That(subsector.Coordinates.Y, Is.EqualTo(0));
        Assert.That(subsector.Hexes.Count, Is.EqualTo(0));
    }
}