using Moq;
using NUnit.Framework;
using SectorCreator.Global;
using SectorCreator.Global.Enums;
using SectorCreator.Models.RTTWorldgen;
using SectorCreator.Models.RTTWorldgen.Worlds;

namespace SectorCreator.Models.Tests.RttWorldgen.Worlds;

[TestFixture]
public class OceanicWorldTests
{
  private OceanicWorld _classUnderTest = new(new RollingService());

  [TestCase(1, 1, 1, 0, SpectralType.A, Luminosity.I, PlanetOrbit.Inner, 5, PlanetChemistry.Water, 0, 1)]
  [TestCase(3, 1, 2, 0, SpectralType.A, Luminosity.I, PlanetOrbit.Inner, 7, PlanetChemistry.Water, 0, 3)]
  [TestCase(6, 1, 3, 0, SpectralType.A, Luminosity.I, PlanetOrbit.Inner, 10, PlanetChemistry.Water, 0, 7)]
  [TestCase(1, 4, 4, 0, SpectralType.K, Luminosity.V, PlanetOrbit.Inner, 5, PlanetChemistry.Water, 0, 2)]
  [TestCase(1, 5, 5, 0, SpectralType.K, Luminosity.V, PlanetOrbit.Inner, 5, PlanetChemistry.Ammonia, 0, 12)]
  [TestCase(1, 2, 6, 0, SpectralType.M, Luminosity.V, PlanetOrbit.Inner, 5, PlanetChemistry.Water, 0, 3)]
  [TestCase(1, 3, 1, 0, SpectralType.M, Luminosity.V, PlanetOrbit.Inner, 5, PlanetChemistry.Ammonia, 0, 1)]
  [TestCase(1, 4, 2, 0, SpectralType.M, Luminosity.V, PlanetOrbit.Inner, 5, PlanetChemistry.Ammonia, 0, 10)]
  [TestCase(1, 5, 3, 0, SpectralType.M, Luminosity.V, PlanetOrbit.Inner, 5, PlanetChemistry.Methane, 0, 10)]
  [TestCase(1, 1, 4, 0, SpectralType.L, Luminosity.V, PlanetOrbit.Inner, 5, PlanetChemistry.Water, 0, 1)]
  [TestCase(1, 2, 5, 0, SpectralType.L, Luminosity.V, PlanetOrbit.Inner, 5, PlanetChemistry.Ammonia, 0, 12)]
  [TestCase(1, 3, 6, 0, SpectralType.L, Luminosity.V, PlanetOrbit.Inner, 5, PlanetChemistry.Ammonia, 0, 12)]
  [TestCase(1, 4, 1, 0, SpectralType.L, Luminosity.V, PlanetOrbit.Inner, 5, PlanetChemistry.Methane, 0, 1)]
  [TestCase(1, 4, 2, 0, SpectralType.A, Luminosity.I, PlanetOrbit.Outer, 5, PlanetChemistry.Water, 0, 1)]
  [TestCase(1, 5, 3, 0, SpectralType.A, Luminosity.I, PlanetOrbit.Outer, 5, PlanetChemistry.Ammonia, 0, 10)]
  [TestCase(6, 1, 12, 0, SpectralType.A, Luminosity.I, PlanetOrbit.Inner, 10, PlanetChemistry.Water, 0, 12)]
  [TestCase(3, 1, 2, 0, SpectralType.A, Luminosity.IV, PlanetOrbit.Inner, 7, PlanetChemistry.Water, 0, 2)]
  [TestCase(1, 1, 1, 3, SpectralType.A, Luminosity.I, PlanetOrbit.Inner, 5, PlanetChemistry.Water, 1, 1)]
  [TestCase(1, 1, 6, 6, SpectralType.A, Luminosity.I, PlanetOrbit.Inner, 5, PlanetChemistry.Water, 6, 5)]
  [TestCase(1, 1, 6, 6, SpectralType.D, Luminosity.I, PlanetOrbit.Inner, 5, PlanetChemistry.Water, 3, 5)]
  public void WhenGenerating(int sizeRoll, int chemistryRoll, int atmosphereRoll, int age,
    SpectralType spectralType, Luminosity luminosity, PlanetOrbit planetOrbit,
    int expectedSize, PlanetChemistry expectedChemistry, int expectedBiosphere, int expectedAtmosphere)
  {
    // Setup
    var mockRollingService = new Mock<IRollingService>();
    mockRollingService.SetupSequence(x => x.D6(1))
      .Returns(sizeRoll)
      .Returns(chemistryRoll)
      .Returns(atmosphereRoll);
    mockRollingService.Setup(x => x.D6(2)).Returns(atmosphereRoll);
    mockRollingService.Setup(x => x.D3(1)).Returns(1);
    _classUnderTest = new OceanicWorld(mockRollingService.Object);

    // Act
    var oceanicWorld = _classUnderTest.Generate(new RttWorldgenPlanet {PlanetOrbit = planetOrbit},
      new RttWorldgenStar {SpectralType = spectralType, Luminosity = luminosity, Age = age});

    // Assert
    Assert.That(oceanicWorld.Size, Is.EqualTo(expectedSize));
    Assert.That(oceanicWorld.Chemistry, Is.EqualTo(expectedChemistry));
    Assert.That(oceanicWorld.Biosphere, Is.EqualTo(expectedBiosphere));
    Assert.That(oceanicWorld.Atmosphere, Is.EqualTo(expectedAtmosphere));
  }
}
