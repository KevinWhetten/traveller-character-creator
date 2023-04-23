using Moq;
using NUnit.Framework;
using SectorCreator.Global;
using SectorCreator.Models.RTTWorldgen;
using SectorCreator.Models.RTTWorldgen.Worlds;

namespace SectorCreator.Models.Tests.RttWorldgen.Worlds;

[TestFixture]
public class HelianWorldTests
{
    private HelianWorld _classUnderTest = new(new RollingService());

    [TestCase(1, 1, 7, 10, 0)]
    [TestCase(3, 1, 7, 12, 0)]
    [TestCase(6, 1, 7, 15, 0)]
    [TestCase(1, 1, 7, 10, 0)]
    [TestCase(1, 3, 7, 10, 6)]
    [TestCase(1, 3, 11, 10, 10)]
    [TestCase(1, 6, 7, 10, 15)]
    public void WhenGenerating(int sizeRoll, int hydrographicsRoll1, int hydrographicsRoll2,
        int expectedSize, int expectedHydrographics)
    {
        var mockRollingService = new Mock<IRollingService>();
        mockRollingService.SetupSequence(x => x.D6(1))
            .Returns(sizeRoll)
            .Returns(hydrographicsRoll1);
        mockRollingService.Setup(x => x.D6(2)).Returns(hydrographicsRoll2);
        _classUnderTest = new HelianWorld(mockRollingService.Object);

        var helianWorld = _classUnderTest.Generate(new RttWorldgenPlanet());

        Assert.That(helianWorld.Size, Is.EqualTo(expectedSize));
        Assert.That(helianWorld.Atmosphere, Is.EqualTo(13));
        Assert.That(helianWorld.Hydrographics, Is.EqualTo(expectedHydrographics));
        Assert.That(helianWorld.Biosphere, Is.EqualTo(0));
    }
}