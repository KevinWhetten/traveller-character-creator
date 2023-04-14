using Moq;
using NUnit.Framework;
using SectorCreator.Global;
using SectorCreator.Models.RTTWorldgen;
using SectorCreator.Models.RTTWorldgen.Worlds;

namespace SectorCreator.Models.Tests.RttWorldgen.Worlds;

[TestFixture]
public class JaniLithicWorldTests
{
    private JaniLithicWorld _classUnderTest = new(new RollingService(), new WorldValidation());

    [TestCase(1, 1, 5, 1)]
    [TestCase(3, 1, 7, 1)]
    [TestCase(6, 1, 10, 1)]
    [TestCase(1, 3, 5, 1)]
    [TestCase(1, 4, 5, 10)]
    [TestCase(1, 6, 5, 10)]
    public void WhenGenerating(int sizeRoll, int atmosphereRoll, int expectedSize, int expectedAtmosphere)
    {
        // Setup
        var mockRollingService = new Mock<IRollingService>();
        mockRollingService.SetupSequence(x => x.D6(1))
            .Returns(sizeRoll)
            .Returns(atmosphereRoll);
        _classUnderTest = new JaniLithicWorld(mockRollingService.Object, new WorldValidation());

        // Act
        var janiLithicWorld = _classUnderTest.Generate(new RttWorldgenPlanet());

        // Assert
        Assert.That(janiLithicWorld.Size, Is.EqualTo(expectedSize));
        Assert.That(janiLithicWorld.Atmosphere, Is.EqualTo(expectedAtmosphere));
        Assert.That(janiLithicWorld.Hydrographics, Is.EqualTo(0));
        Assert.That(janiLithicWorld.Biosphere, Is.EqualTo(0));
    }
}