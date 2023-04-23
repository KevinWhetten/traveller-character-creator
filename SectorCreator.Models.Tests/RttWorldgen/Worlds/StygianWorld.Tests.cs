using Moq;
using NUnit.Framework;
using SectorCreator.Global;
using SectorCreator.Models.RTTWorldgen;
using SectorCreator.Models.RTTWorldgen.Worlds;

namespace SectorCreator.Models.Tests.RttWorldgen.Worlds;

[TestFixture]
public class StygianWorldTests
{
    private StygianWorld _classUnderTest = new(new RollingService());

    [TestCase(1, 0)]
    [TestCase(2, 1)]
    [TestCase(3, 2)]
    [TestCase(4, 3)]
    [TestCase(5, 4)]
    [TestCase(6, 5)]
    public void WhenGenerating(int sizeRoll, int expectedSize)
    {
        // Setup
        var mockRollingService = new Mock<IRollingService>();
        mockRollingService.Setup(x => x.D6(1)).Returns(sizeRoll);
        _classUnderTest = new StygianWorld(mockRollingService.Object);

        // Act
        var stygianWorld = _classUnderTest.Generate(new RttWorldgenPlanet());

        // Assert
        Assert.That(stygianWorld.Size, Is.EqualTo(expectedSize));
        Assert.That(stygianWorld.Atmosphere, Is.EqualTo(0));
        Assert.That(stygianWorld.Hydrographics, Is.EqualTo(0));
        Assert.That(stygianWorld.Biosphere, Is.EqualTo(0));
    }
}