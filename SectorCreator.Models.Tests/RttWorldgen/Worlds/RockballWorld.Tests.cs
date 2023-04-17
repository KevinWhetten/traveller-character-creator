using Moq;
using NUnit.Framework;
using SectorCreator.Global;
using SectorCreator.Global.Enums;
using SectorCreator.Models.RTTWorldgen;
using SectorCreator.Models.RTTWorldgen.Worlds;
namespace SectorCreator.Models.Tests.RttWorldgen.Worlds
{
  public class RockballWorldTests
  {
    private RockballWorld _classUnderTest = new RockballWorld(new RollingService(), new WorldValidation());

    [TestCase(1, 1, SpectralType.A, PlanetOrbit.Inner, 0, 0)]
    [TestCase(3, 1, SpectralType.A, PlanetOrbit.Inner, 2, 0)]
    [TestCase(6, 1, SpectralType.A, PlanetOrbit.Inner, 5, 0)]
    [TestCase(6, 7, SpectralType.L, PlanetOrbit.Inner, 5, 2)]
    [TestCase(6, 10, SpectralType.L, PlanetOrbit.Inner, 5, 5)]
    [TestCase(6, 12, SpectralType.L, PlanetOrbit.Inner, 5, 7)]
    [TestCase(6, 7, SpectralType.L, PlanetOrbit.Inner, 5, 2)]
    [TestCase(6, 7, SpectralType.L, PlanetOrbit.Epistellar, 5, 0)]
    [TestCase(6, 7, SpectralType.L, PlanetOrbit.Outer, 5, 4)]
    public void WhenGenerating(int sizeRoll, int hydrographicsRoll,
      SpectralType spectralType, PlanetOrbit orbit,
      int expectedSize, int expectedHydrographics)
    {
      // Setup
      var mockRollingService = new Mock<IRollingService>();
      mockRollingService.Setup(x => x.D6(1))
        .Returns(sizeRoll);
      mockRollingService.Setup(x => x.D6(2))
        .Returns(hydrographicsRoll);
      _classUnderTest = new RockballWorld(mockRollingService.Object, new WorldValidation());

      // Act
      var rockballWorld = _classUnderTest.Generate(new RttWorldgenPlanet {PlanetOrbit = orbit},
        new RttWorldgenStar {SpectralType = spectralType});

      // Assert
      Assert.That(rockballWorld.Size, Is.EqualTo(expectedSize));
      Assert.That(rockballWorld.Atmosphere, Is.EqualTo(0));
      Assert.That(rockballWorld.Hydrographics, Is.EqualTo(expectedHydrographics));
      Assert.That(rockballWorld.Biosphere, Is.EqualTo(0));
    }
  }
}
