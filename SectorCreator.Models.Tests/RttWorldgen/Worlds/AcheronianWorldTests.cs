using Moq;
using NUnit.Framework;
using SectorCreator.Global;
using SectorCreator.Models.RTTWorldgen;
using SectorCreator.Models.RTTWorldgen.Worlds;

namespace SectorCreator.Models.Tests.RttWorldgen.Worlds;

[TestFixture]
public class AcheronianWorldTests
{
    private AcheronianWorld _classUnderTest = new(new RollingService());

    [TestCase(1, 5)]
    [TestCase(2, 6)]
    [TestCase(3, 7)]
    [TestCase(4, 8)]
    [TestCase(5, 9)]
    [TestCase(6, 10)]
    public void WhenGenerating(int roll, int expected)
    {
        // Setup
        var rollingServiceMock = new Mock<IRollingService>();
        rollingServiceMock.Setup(x => x.D6(1)).Returns(roll);
        
        _classUnderTest = new AcheronianWorld(rollingServiceMock.Object);
        
        // Act
        var acheronianPlanet = _classUnderTest.Generate(new RttWorldgenPlanet());

        // Assert
        Assert.That(acheronianPlanet.Size, Is.EqualTo(expected));
        Assert.That(acheronianPlanet.Atmosphere, Is.EqualTo(1));
        Assert.That(acheronianPlanet.Hydrographics, Is.EqualTo(0));
        Assert.That(acheronianPlanet.Biosphere, Is.EqualTo(0));
    }
}