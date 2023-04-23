using Moq;
using NUnit.Framework;
using SectorCreator.Global;
using SectorCreator.Models.RTTWorldgen;
using SectorCreator.Models.RTTWorldgen.Worlds;

namespace SectorCreator.Models.Tests.RttWorldgen.Worlds;

[TestFixture]
public class AsphodelianWorldTests
{
    private AsphodelianWorld _classUnderTest = new(new RollingService());

    [TestCase(1, 10)]
    [TestCase(2, 11)]
    [TestCase(3, 12)]
    [TestCase(4, 13)]
    [TestCase(5, 14)]
    [TestCase(6, 15)]
    public void WhenGenerating(int sizeRoll, int expectedSize)
    {
        // Setup
        var mockRollingService = new Mock<IRollingService>();
        mockRollingService.Setup(x => x.D6(1)).Returns(sizeRoll);
        _classUnderTest = new AsphodelianWorld(mockRollingService.Object);
        
        // Act
        var asphodelianWorld = _classUnderTest.Generate(new RttWorldgenPlanet());

        // Assert
        Assert.That(asphodelianWorld.Size, Is.EqualTo(expectedSize));
        Assert.That(asphodelianWorld.Atmosphere, Is.EqualTo(1));
        Assert.That(asphodelianWorld.Hydrographics, Is.EqualTo(0));
        Assert.That(asphodelianWorld.Biosphere, Is.EqualTo(0));
    }
}