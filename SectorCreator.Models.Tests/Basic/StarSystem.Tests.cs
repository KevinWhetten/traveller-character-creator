using NUnit.Framework;
using SectorCreator.Models.Basic;

namespace SectorCreator.Models.Tests.Basic;

[TestFixture]
public class StarSystemTests
{
    [Test]
    public void WhenConstructing()
    {
        var starSystem = new StarSystem();
        
        Assert.That(starSystem.CompanionStars.Count, Is.EqualTo(0));
        Assert.That(starSystem.Planets.Count, Is.EqualTo(0));
        Assert.That(starSystem.GasGiant, Is.EqualTo(false));
    }
}