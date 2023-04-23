using Moq;
using NUnit.Framework;
using SectorCreator.Global;
using SectorCreator.Models.RTTWorldgen;
using SectorCreator.Models.RTTWorldgen.Worlds;

namespace SectorCreator.Models.Tests.RttWorldgen.Worlds;

[TestFixture]
public class HebeanWorldTests
{
    private HebeanWorld _classUnderTest = new(new RollingService());

    [TestCase(1, 1, 1, 0, 0, 0)]
    [TestCase(1, 1, 7, 0, 0, 0)]
    [TestCase(1, 1, 12, 0, 0, 1)]
    
    [TestCase(1, 3, 1, 0, 0, 0)]
    [TestCase(1, 3, 7, 0, 0, 0)]
    [TestCase(1, 3, 12, 0, 0, 1)]
    
    [TestCase(1, 6, 1, 0, 0, 0)]
    [TestCase(1, 6, 7, 0, 0, 0)]
    [TestCase(1, 6, 12, 0, 0, 1)]
    
    [TestCase(3, 1, 1, 2, 0, 0)]
    [TestCase(3, 1, 7, 2, 0, 0)]
    [TestCase(3, 1, 12, 2, 0, 3)]
    
    [TestCase(3, 3, 1, 2, 0, 0)]
    [TestCase(3, 3, 7, 2, 0, 0)]
    [TestCase(3, 3, 12, 2, 0, 3)]
    
    [TestCase(3, 6, 1, 2, 10, 0)]
    [TestCase(3, 6, 7, 2, 10, 0)]
    [TestCase(3, 6, 12, 2, 10, 3)]
    
    [TestCase(6, 1, 1, 5, 0, 0)]
    [TestCase(6, 1, 7, 5, 0, 1)]
    [TestCase(6, 1, 12, 5, 0, 6)]
    
    [TestCase(6, 3, 1, 5, 10, 0)]
    [TestCase(6, 3, 7, 5, 10, 1)]
    [TestCase(6, 3, 12, 5, 10, 6)]
    
    [TestCase(6, 6, 1, 5, 10, 0)]
    [TestCase(6, 6, 7, 5, 10, 1)]
    [TestCase(6, 6, 12, 5, 10, 6)]
    public void WhenGenerating(int sizeRoll, int atmosphereRoll, int hydrographicsRoll,
        int expectedSize, int expectedAtmosphere, int expectedHydrographics)
    {
        // Setup
        var mockRollingService = new Mock<IRollingService>();
        mockRollingService.SetupSequence(x => x.D6(1))
            .Returns(sizeRoll)
            .Returns(atmosphereRoll);
        mockRollingService.Setup(x => x.D6(2))
            .Returns(hydrographicsRoll);
        _classUnderTest = new HebeanWorld(mockRollingService.Object);

        // Act
        var hebeanWorld = _classUnderTest.Generate(new RttWorldgenPlanet());

        // Assert
        Assert.That(hebeanWorld.Size, Is.EqualTo(expectedSize));
        Assert.That(hebeanWorld.Atmosphere, Is.EqualTo(expectedAtmosphere));
        Assert.That(hebeanWorld.Hydrographics, Is.EqualTo(expectedHydrographics));
        Assert.That(hebeanWorld.Biosphere, Is.EqualTo(0));
    }
}