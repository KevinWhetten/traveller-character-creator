using Moq;
using NUnit.Framework;
using SectorCreator.Global;
using SectorCreator.Global.Enums;
using SectorCreator.Models.RTTWorldgen;
using SectorCreator.Models.RTTWorldgen.Worlds;
namespace SectorCreator.Models.Tests.RttWorldgen.Worlds;

[TestFixture]
public class PanthalassicWorldTests
{
  private PanthalassicWorld _classUnderTest = new(new RollingService());

  [TestCase(1, 1, 1, 1, 1, SpectralType.A, LuminosityClass.Ia, 0, 10, 9, PlanetChemistry.Water, 0)]
  [TestCase(3, 1, 1, 1, 1, SpectralType.A, LuminosityClass.Ia, 0, 12, 9, PlanetChemistry.Water, 0)]
  [TestCase(6, 1, 1, 1, 1, SpectralType.A, LuminosityClass.Ia, 0, 15, 9, PlanetChemistry.Water, 0)]
  [TestCase(1, 3, 1, 1, 1, SpectralType.A, LuminosityClass.Ia, 0, 10, 11, PlanetChemistry.Water, 0)]
  [TestCase(1, 5, 1, 1, 1, SpectralType.A, LuminosityClass.Ia, 0, 10, 13, PlanetChemistry.Water, 0)]
  [TestCase(1, 6, 1, 1, 1, SpectralType.A, LuminosityClass.Ia, 0, 10, 13, PlanetChemistry.Water, 0)]
  [TestCase(1, 1, 3, 7, 1, SpectralType.A, LuminosityClass.Ia, 0, 10, 9, PlanetChemistry.Water, 0)]
  [TestCase(1, 1, 3, 10, 1, SpectralType.A, LuminosityClass.Ia, 0, 10, 9, PlanetChemistry.Sulfur, 0)]
  [TestCase(1, 1, 3, 12, 1, SpectralType.A, LuminosityClass.Ia, 0, 10, 9, PlanetChemistry.Chlorine, 0)]
  [TestCase(1, 1, 4, 1, 1, SpectralType.K, LuminosityClass.V, 0, 10, 9, PlanetChemistry.Water, 0)]
  [TestCase(1, 1, 5, 1, 1, SpectralType.K, LuminosityClass.V, 0, 10, 9, PlanetChemistry.Methane, 0)]
  [TestCase(1, 1, 2, 1, 1, SpectralType.M, LuminosityClass.V, 0, 10, 9, PlanetChemistry.Water, 0)]
  [TestCase(1, 1, 3, 1, 1, SpectralType.M, LuminosityClass.V, 0, 10, 9, PlanetChemistry.Methane, 0)]
  [TestCase(1, 1, 1, 1, 1, SpectralType.L, LuminosityClass.V, 0, 10, 9, PlanetChemistry.Water, 0)]
  [TestCase(1, 1, 2, 1, 1, SpectralType.L, LuminosityClass.V, 0, 10, 9, PlanetChemistry.Methane, 0)]
  [TestCase(1, 1, 1, 1, 1, SpectralType.A, LuminosityClass.Ia, 3, 10, 9, PlanetChemistry.Water, 1)]
  [TestCase(1, 1, 1, 1, 1, SpectralType.A, LuminosityClass.Ia, 6, 10, 9, PlanetChemistry.Water, 1)]
  [TestCase(1, 1, 1, 7, 1, SpectralType.A, LuminosityClass.Ia, 6, 10, 9, PlanetChemistry.Water, 7)]
  [TestCase(1, 1, 1, 12, 1, SpectralType.A, LuminosityClass.Ia, 6, 10, 9, PlanetChemistry.Chlorine, 12)]
  public void WhenGenerating(int sizeRoll, int atmosphereRoll, int chemistryRoll, int chemistryRoll2d6, int biosphereRoll,
    SpectralType spectralType, LuminosityClass luminosityClass, int age,
    int expectedSize, int expectedAtmosphere, PlanetChemistry expectedChemistry, int expectedBiosphere)
  {
    // Setup
    var mockRollingService = new Mock<IRollingService>();
    mockRollingService.SetupSequence(x => x.D6(1))
      .Returns(sizeRoll)
      .Returns(atmosphereRoll)
      .Returns(chemistryRoll);
    mockRollingService.Setup(x => x.D6(2))
      .Returns(chemistryRoll2d6);
    mockRollingService.Setup(x => x.D3(1))
      .Returns(1);
    _classUnderTest = new PanthalassicWorld(mockRollingService.Object);

    // Act
    var panthalassicWorld = _classUnderTest.Generate(new RttWorldgenPlanet(),
      new RttWorldgenStar {SpectralType = spectralType, LuminosityClass = luminosityClass, Age = age});

    // Assert
    Assert.That(panthalassicWorld.Size, Is.EqualTo(expectedSize));
    Assert.That(panthalassicWorld.Atmosphere, Is.EqualTo(expectedAtmosphere));
    Assert.That(panthalassicWorld.Hydrographics, Is.EqualTo(11));
    Assert.That(panthalassicWorld.Chemistry, Is.EqualTo(expectedChemistry));
    Assert.That(panthalassicWorld.Biosphere, Is.EqualTo(expectedBiosphere));
  }
}
