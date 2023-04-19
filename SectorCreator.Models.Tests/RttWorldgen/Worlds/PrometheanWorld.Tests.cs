using Moq;
using NUnit.Framework;
using SectorCreator.Global;
using SectorCreator.Global.Enums;
using SectorCreator.Models.RTTWorldgen;
using SectorCreator.Models.RTTWorldgen.Worlds;
namespace SectorCreator.Models.Tests.RttWorldgen.Worlds;

[TestFixture]
public class PrometheanWorldTests
{
  private PrometheanWorld _classUnderTest = new PrometheanWorld(new RollingService(), new WorldValidation());

  [TestCase(1, 1, 1, 1, SpectralType.A, 0, PlanetOrbit.Inner, 0, PlanetChemistry.Water, 0, 10)]
  [TestCase(3, 1, 1, 1, SpectralType.A, 0, PlanetOrbit.Inner, 2, PlanetChemistry.Water, 0, 10)]
  [TestCase(6, 1, 1, 1, SpectralType.A, 0, PlanetOrbit.Inner, 5, PlanetChemistry.Water, 0, 10)]
  [TestCase(1, 3, 1, 1, SpectralType.A, 0, PlanetOrbit.Inner, 0, PlanetChemistry.Water, 0, 10)]
  [TestCase(1, 6, 1, 1, SpectralType.A, 0, PlanetOrbit.Inner, 0, PlanetChemistry.Ammonia, 0, 10)]
  [TestCase(1, 6, 1, 1, SpectralType.A, 0, PlanetOrbit.Epistellar, 0, PlanetChemistry.Water, 0, 10)]
  [TestCase(1, 2, 1, 1, SpectralType.A, 0, PlanetOrbit.Outer, 0, PlanetChemistry.Water, 0, 10)]
  [TestCase(1, 4, 1, 1, SpectralType.A, 0, PlanetOrbit.Outer, 0, PlanetChemistry.Ammonia, 0, 10)]
  [TestCase(1, 6, 1, 1, SpectralType.A, 0, PlanetOrbit.Outer, 0, PlanetChemistry.Methane, 0, 10)]
  [TestCase(1, 2, 1, 1, SpectralType.L, 0, PlanetOrbit.Inner, 0, PlanetChemistry.Water, 0, 10)]
  [TestCase(1, 3, 1, 1, SpectralType.L, 0, PlanetOrbit.Inner, 0, PlanetChemistry.Ammonia, 0, 10)]
  [TestCase(1, 4, 1, 1, SpectralType.L, 0, PlanetOrbit.Inner, 0, PlanetChemistry.Ammonia, 0, 10)]
  [TestCase(1, 5, 1, 1, SpectralType.L, 0, PlanetOrbit.Inner, 0, PlanetChemistry.Methane, 0, 10)]
  [TestCase(1, 6, 1, 1, SpectralType.L, 0, PlanetOrbit.Outer, 0, PlanetChemistry.Methane, 0, 10)]
  [TestCase(1, 1, 1, 1, SpectralType.A, 3, PlanetOrbit.Inner, 0, PlanetChemistry.Water, 1, 10)]
  [TestCase(1, 1, 1, 1, SpectralType.A, 7, PlanetOrbit.Inner, 0, PlanetChemistry.Water, 7, 2)]
  [TestCase(1, 1, 1, 1, SpectralType.L, 7, PlanetOrbit.Inner, 0, PlanetChemistry.Water, 4, 2)]
  [TestCase(6, 1, 1, 1, SpectralType.A, 7, PlanetOrbit.Inner, 5, PlanetChemistry.Water, 7, 5)]
  public void WhenGenerating(int sizeRoll, int chemistryRoll, int biosphereRoll, int atmosphereRoll,
    SpectralType spectralType, int age, PlanetOrbit orbit,
    int expectedSize, PlanetChemistry expectedChemistry, int expectedBiosphere, int expectedAtmosphere)
  {
    // Setup
    var mockRollingService = new Mock<IRollingService>();
    mockRollingService.SetupSequence(x => x.D6(1))
      .Returns(sizeRoll)
      .Returns(chemistryRoll)
      .Returns(biosphereRoll);
    mockRollingService.Setup(x => x.D3(1))
      .Returns(1);
    mockRollingService.Setup(x => x.D6(2))
      .Returns(7);
    _classUnderTest = new PrometheanWorld(mockRollingService.Object, new WorldValidation());

    // Act
    var prometheanWorld = _classUnderTest.Generate(new RttWorldgenPlanet {PlanetOrbit = orbit},
      new RttWorldgenStar {SpectralType = spectralType, Age = age});

    // Assert
    Assert.That(prometheanWorld.Size, Is.EqualTo(expectedSize));
    Assert.That(prometheanWorld.Chemistry, Is.EqualTo(expectedChemistry));
    Assert.That(prometheanWorld.Biosphere, Is.EqualTo(expectedBiosphere));
    Assert.That(prometheanWorld.Atmosphere, Is.EqualTo(expectedAtmosphere));
    Assert.That(prometheanWorld.Hydrographics, Is.EqualTo(5));
  }
}
