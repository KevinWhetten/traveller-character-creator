using Moq;
using NUnit.Framework;
using SectorCreator.Global;
using SectorCreator.Global.Enums;
using SectorCreator.Models.RTTWorldgen;
using SectorCreator.Models.RTTWorldgen.Worlds;

namespace SectorCreator.Models.Tests.RttWorldgen.Worlds;

[TestFixture]
public class AreanWorldTests
{
    private AreanWorld _classUnderTest = new(new RollingService(), new WorldValidation());

    [TestCase(6, 5, 6, 3, 6, 0, SpectralType.A, 5, 10, 4, PlanetChemistry.Water, 0)]
    [TestCase(6, 5, 6, 3, 6, 0, SpectralType.D, 5, 1, 0, PlanetChemistry.Water, 0)]
    [TestCase(6, 5, 6, 3, 6, 0, SpectralType.L, 5, 10, 4, PlanetChemistry.Ammonia, 0)]
    [TestCase(6, 5, 6, 6, 6, 0, SpectralType.L, 5, 10, 4, PlanetChemistry.Methane, 0)]
    [TestCase(6, 5, 6, 3, 6, 2, SpectralType.A, 5, 10, 4, PlanetChemistry.Water, 1)]
    [TestCase(6, 5, 6, 3, 6, 2, SpectralType.D, 5, 1, 0, PlanetChemistry.Water, 2)]
    [TestCase(6, 5, 6, 3, 6, 5, SpectralType.A, 5, 10, 4, PlanetChemistry.Water, 9)]
    [TestCase(6, 5, 6, 3, 6, 5, SpectralType.D, 5, 1, 0, PlanetChemistry.Water, 2)]
    public void WhenGenerating(int sizeRoll, int atmosphereRoll, int hydrographicsRoll, int chemistryRoll,
        int biosphereRoll, int age, SpectralType starSpectralType, int expectedSize, int expectedAtmosphere,
        int expectedHydrographics,
        PlanetChemistry expectedChemistry, int expectedBiosphere)
    {
        // Setup
        var rollingServiceMock = new Mock<RollingService>();
        rollingServiceMock.SetupSequence(x => x.D6(1))
            .Returns(sizeRoll)
            .Returns(atmosphereRoll)
            .Returns(chemistryRoll)
            .Returns(biosphereRoll);
        rollingServiceMock.SetupSequence(x => x.D3(2))
            .Returns(hydrographicsRoll);
        rollingServiceMock.Setup(x => x.D3(1)).Returns(1);
        _classUnderTest = new AreanWorld(rollingServiceMock.Object, new WorldValidation());

        // Act
        var areanPlanet = _classUnderTest.Generate(new RttWorldgenPlanet(),
            new RttWorldgenStar {SpectralType = starSpectralType, Age = age});

        // Assert
        Assert.That(areanPlanet.Size, Is.EqualTo(expectedSize));
        Assert.That(areanPlanet.Atmosphere, Is.EqualTo(expectedAtmosphere));
        Assert.That(areanPlanet.Hydrographics, Is.EqualTo(expectedHydrographics));
        Assert.That(areanPlanet.Chemistry, Is.EqualTo(expectedChemistry));
        Assert.That(areanPlanet.Biosphere, Is.EqualTo(expectedBiosphere));
    }
}