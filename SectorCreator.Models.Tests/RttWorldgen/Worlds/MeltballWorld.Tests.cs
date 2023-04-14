using Moq;
using NUnit.Framework;
using SectorCreator.Global;
using SectorCreator.Models.RTTWorldgen;
using SectorCreator.Models.RTTWorldgen.Worlds;

namespace SectorCreator.Models.Tests.RttWorldgen.Worlds;

[TestFixture]
public class MeltballWorldTests
{
    private MeltballWorld _classUnderTest = new(new RollingService(), new WorldValidation());

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
        _classUnderTest = new MeltballWorld(mockRollingService.Object, new WorldValidation());

        // Act
        var meltballWorld = _classUnderTest.Generate(new RttWorldgenPlanet());

        // Assert
        Assert.That(meltballWorld.Size, Is.EqualTo(expectedSize));
        Assert.That(meltballWorld.Atmosphere, Is.EqualTo(1));
        Assert.That(meltballWorld.Hydrographics, Is.EqualTo(15));
        Assert.That(meltballWorld.Biosphere, Is.EqualTo(0));
    }
}