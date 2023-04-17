using Moq;
using NUnit.Framework;
using SectorCreator.Global;
using SectorCreator.Models.RTTWorldgen;
using SectorCreator.Models.RTTWorldgen.Worlds;

namespace SectorCreator.Models.Tests.RttWorldgen.Worlds;

[TestFixture]
public class TelluricWorldTests
{
    private TelluricWorld _classUnderTest = new(new RollingService(), new WorldValidation());

    [TestCase(1, 1,5, 0)]
    [TestCase(3, 1,7, 0)]
    [TestCase(6, 1,10, 0)]
    [TestCase(1, 1,5, 0)]
    [TestCase(1, 4,5, 0)]
    [TestCase(1, 5,5, 15)]
    [TestCase(1, 6,5, 15)]
    public void WhenGenerating(int sizeRoll, int hydrographicsRoll, int expectedSize, int expectedHydrographics)
    {
        // Setup
        var mockRollingService = new Mock<IRollingService>();
        mockRollingService.SetupSequence(x => x.D6(1))
            .Returns(sizeRoll)
            .Returns(hydrographicsRoll);
        _classUnderTest = new TelluricWorld(mockRollingService.Object, new WorldValidation());

        // Act
        var telluricWorld = _classUnderTest.Generate(new RttWorldgenPlanet());

        // Assert
        Assert.That(telluricWorld.Size, Is.EqualTo(expectedSize));
        Assert.That(telluricWorld.Atmosphere, Is.EqualTo(12));
        Assert.That(telluricWorld.Hydrographics, Is.EqualTo(expectedHydrographics));
        Assert.That(telluricWorld.Biosphere, Is.EqualTo(0));
    }
}