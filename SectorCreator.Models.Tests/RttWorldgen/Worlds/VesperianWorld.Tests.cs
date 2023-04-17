using Moq;
using NUnit.Framework;
using SectorCreator.Global;
using SectorCreator.Global.Enums;
using SectorCreator.Models.RTTWorldgen;
using SectorCreator.Models.RTTWorldgen.Worlds;

namespace SectorCreator.Models.Tests.RttWorldgen.Worlds;

[TestFixture]
public class VesperianWorldTests
{
    private VesperianWorld _classUnderTest = new(new RollingService(), new WorldValidation());

    [TestCase(1, 2, 2, 2, 0, 5, PlanetChemistry.Water, 0, 10)]
    [TestCase(3, 2, 2, 2, 0, 7, PlanetChemistry.Water, 0, 10)]
    [TestCase(6, 2, 2, 2, 0, 10, PlanetChemistry.Water, 0, 10)]
    [TestCase(1, 7, 2, 2, 0, 5, PlanetChemistry.Water, 0, 10)]
    [TestCase(1, 10, 2, 2, 0, 5, PlanetChemistry.Water, 0, 10)]
    [TestCase(1, 12, 2, 2, 0, 5, PlanetChemistry.Chlorine, 0, 10)]
    [TestCase(1, 2, 2, 2, 3, 5, PlanetChemistry.Water, 2, 10)]
    [TestCase(1, 2, 2, 2, 5, 5, PlanetChemistry.Water, 2, 10)]
    [TestCase(1, 2, 7, 2, 5, 5, PlanetChemistry.Water, 7, 2)]
    [TestCase(1, 2, 10, 2, 5, 5, PlanetChemistry.Water, 10, 2)]
    [TestCase(1, 2, 12, 2, 5, 5, PlanetChemistry.Water, 12, 2)]
    [TestCase(3, 2, 7, 2, 5, 7, PlanetChemistry.Water, 7, 2)]
    [TestCase(1, 2, 7, 2, 5, 5, PlanetChemistry.Water, 7, 2)]
    [TestCase(3, 2, 7, 7, 5, 7, PlanetChemistry.Water, 7, 7)]
    [TestCase(3, 2, 7, 10, 5, 7, PlanetChemistry.Water, 7, 9)]
    [TestCase(3, 2, 7, 12, 5, 7, PlanetChemistry.Water, 7, 9)]
    [TestCase(1, 12, 12, 2, 5, 5, PlanetChemistry.Chlorine, 12, 11)]
    public void WhenGenerating(int sizeRoll, int chemistryRoll, int biosphereRoll, int atmosphereRoll, int age,
        int expectedSize, PlanetChemistry expectedChemistry, int expectedBiosphere, int expectedAtmosphere)
    {
        // Setup
        var mockRollingService = new Mock<IRollingService>();
        mockRollingService.SetupSequence(x => x.D6(1))
            .Returns(sizeRoll);
        mockRollingService.SetupSequence(x => x.D6(2))
            .Returns(chemistryRoll)
            .Returns(biosphereRoll)
            .Returns(atmosphereRoll);
        mockRollingService.Setup(x => x.D3(1))
            .Returns(biosphereRoll);
        _classUnderTest = new VesperianWorld(mockRollingService.Object, new WorldValidation());

        // Act
        var telluricWorld = _classUnderTest.Generate(new RttWorldgenPlanet(),
            new RttWorldgenStar {Age = age});

        // Assert
        Assert.That(telluricWorld.Size, Is.EqualTo(expectedSize));
        Assert.That(telluricWorld.Chemistry, Is.EqualTo(expectedChemistry));
        Assert.That(telluricWorld.Biosphere, Is.EqualTo(expectedBiosphere));
        Assert.That(telluricWorld.Atmosphere, Is.EqualTo(expectedAtmosphere));
        // Assert.That(telluricWorld.Hydrographics, Is.EqualTo(expectedHydrographics));
    }
}