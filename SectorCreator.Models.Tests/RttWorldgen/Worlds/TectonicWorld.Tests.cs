using Moq;
using NUnit.Framework;
using SectorCreator.Global;
using SectorCreator.Global.Enums;
using SectorCreator.Models.RTTWorldgen;
using SectorCreator.Models.RTTWorldgen.Worlds;

namespace SectorCreator.Models.Tests.RttWorldgen.Worlds;

[TestFixture]
public class TectonicWorldTests
{
    private TectonicWorld _classUnderTest = new(new RollingService(), new WorldValidation());

    [TestCase(1, 1, 2, 1, 2, 2, PlanetOrbit.Inner, 0, SpectralType.A, Luminosity.I, 5, PlanetChemistry.Water, 0, 10, 0)]
    [TestCase(3, 1, 2, 1, 2, 2, PlanetOrbit.Inner, 0, SpectralType.A, Luminosity.I, 7, PlanetChemistry.Water, 0, 10, 0)]
    [TestCase(6, 1, 2, 1, 2, 2, PlanetOrbit.Inner, 0, SpectralType.A, Luminosity.I, 10, PlanetChemistry.Water, 0, 10, 0)]
    [TestCase(1, 4, 2, 1, 2, 2, PlanetOrbit.Inner, 0, SpectralType.K, Luminosity.V, 5, PlanetChemistry.Water, 0, 10, 0)]
    [TestCase(1, 5, 2, 1, 2, 2, PlanetOrbit.Inner, 0, SpectralType.K, Luminosity.V, 5, PlanetChemistry.Ammonia, 0, 10, 0)]
    [TestCase(1, 3, 2, 1, 2, 2, PlanetOrbit.Inner, 0, SpectralType.M, Luminosity.V, 5, PlanetChemistry.Ammonia, 0, 10, 0)]
    [TestCase(1, 5, 2, 1, 2, 2, PlanetOrbit.Inner, 0, SpectralType.M, Luminosity.V, 5, PlanetChemistry.Methane, 0, 10, 0)]
    [TestCase(1, 1, 2, 1, 2, 2, PlanetOrbit.Inner, 0, SpectralType.L, Luminosity.V, 5, PlanetChemistry.Water, 0, 10, 0)]
    [TestCase(1, 3, 2, 1, 2, 2, PlanetOrbit.Inner, 0, SpectralType.L, Luminosity.V, 5, PlanetChemistry.Ammonia, 0, 10, 0)]
    [TestCase(1, 4, 2, 1, 2, 2, PlanetOrbit.Inner, 0, SpectralType.L, Luminosity.V, 5, PlanetChemistry.Methane, 0, 10, 0)]
    [TestCase(1, 1, 8, 1, 2, 2, PlanetOrbit.Inner, 0, SpectralType.L, Luminosity.V, 5, PlanetChemistry.Water, 0, 10, 0)]
    [TestCase(1, 1, 9, 1, 2, 2, PlanetOrbit.Inner, 0, SpectralType.L, Luminosity.V, 5, PlanetChemistry.Sulfur, 0, 10, 0)]
    [TestCase(1, 1, 11, 1, 2,2, PlanetOrbit.Inner, 0, SpectralType.L, Luminosity.V, 5, PlanetChemistry.Sulfur, 0, 10, 0)]
    [TestCase(1, 1, 12, 1, 2,2, PlanetOrbit.Inner, 0, SpectralType.L, Luminosity.V, 5, PlanetChemistry.Chlorine, 0, 10, 0)]
    [TestCase(1, 4, 2, 1, 2, 2, PlanetOrbit.Outer, 0, SpectralType.A, Luminosity.I, 5, PlanetChemistry.Water, 0, 10, 0)]
    [TestCase(1, 5, 2, 1, 2, 2, PlanetOrbit.Outer, 0, SpectralType.A, Luminosity.I, 5, PlanetChemistry.Ammonia, 0, 10, 0)]
    [TestCase(1, 1, 2, 2, 2, 2, PlanetOrbit.Inner, 3, SpectralType.A, Luminosity.I, 5, PlanetChemistry.Water, 2, 10, 0)]
    [TestCase(1, 1, 2, 1, 2, 2, PlanetOrbit.Inner, 9, SpectralType.A, Luminosity.I, 5, PlanetChemistry.Water, 1, 10, 0)]
    [TestCase(1, 1, 2, 7, 2, 2, PlanetOrbit.Inner, 9, SpectralType.A, Luminosity.I, 5, PlanetChemistry.Water, 7, 2, 0)]
    [TestCase(1, 1, 2, 10, 2, 2, PlanetOrbit.Inner, 9, SpectralType.A, Luminosity.I, 5, PlanetChemistry.Water, 10, 2, 0)]
    [TestCase(1, 1, 2, 12, 2, 2, PlanetOrbit.Inner, 9, SpectralType.A, Luminosity.I, 5, PlanetChemistry.Water, 12, 2, 0)]
    [TestCase(1, 1, 2, 1, 2, 2, PlanetOrbit.Inner, 9, SpectralType.L, Luminosity.I, 5, PlanetChemistry.Water, 0, 10, 0)]
    [TestCase(1, 1, 2, 7, 2, 2, PlanetOrbit.Inner, 9, SpectralType.L, Luminosity.I, 5, PlanetChemistry.Water, 4, 2, 0)]
    [TestCase(1, 1, 2, 10, 2, 2, PlanetOrbit.Inner, 9, SpectralType.L, Luminosity.I, 5, PlanetChemistry.Water, 7, 2, 0)]
    [TestCase(1, 1, 2, 12, 2, 2, PlanetOrbit.Inner, 9, SpectralType.L, Luminosity.I, 5, PlanetChemistry.Water, 9, 2, 0)]
    [TestCase(6, 1, 2, 12, 12, 2, PlanetOrbit.Inner, 9, SpectralType.A, Luminosity.I, 10, PlanetChemistry.Water, 12, 9, 0)]
    [TestCase(2, 1, 2, 12, 2, 2, PlanetOrbit.Inner, 9, SpectralType.A, Luminosity.I, 6, PlanetChemistry.Water, 12, 2, 0)]
    [TestCase(1, 6, 12, 10, 2, 2, PlanetOrbit.Inner, 9, SpectralType.A, Luminosity.I, 5, PlanetChemistry.Chlorine, 10, 11, 0)]
    [TestCase(1, 6, 11, 10, 2, 2, PlanetOrbit.Inner, 9, SpectralType.A, Luminosity.I, 5, PlanetChemistry.Sulfur, 10, 11, 0)]
    [TestCase(1, 1, 2, 7, 2, 7, PlanetOrbit.Inner, 9, SpectralType.L, Luminosity.I, 5, PlanetChemistry.Water, 4, 2, 5)]
    [TestCase(1, 1, 2, 7, 2, 10, PlanetOrbit.Inner, 9, SpectralType.L, Luminosity.I, 5, PlanetChemistry.Water, 4, 2, 8)]
    [TestCase(1, 1, 2, 7, 2, 12, PlanetOrbit.Inner, 9, SpectralType.L, Luminosity.I, 5, PlanetChemistry.Water, 4, 2, 10)]
    public void WhenGenerating(int sizeRoll, int chemistryRoll, int chemistryRoll2, int biosphereRoll,
        int atmosphereRoll, int hydrographicsRoll,
        PlanetOrbit orbit, int age, SpectralType spectralType, Luminosity luminosity,
        int expectedSize, PlanetChemistry expectedChemistry, int expectedBiosphere, int expectedAtmosphere,
        int expectedHydrographics)
    {
        // Setup
        var mockRollingService = new Mock<IRollingService>();
        mockRollingService.SetupSequence(x => x.D6(1))
            .Returns(sizeRoll)
            .Returns(chemistryRoll)
            .Returns(biosphereRoll);
        mockRollingService.SetupSequence(x => x.D6(2))
            .Returns(chemistryRoll2)
            .Returns(biosphereRoll)
            .Returns(atmosphereRoll)
            .Returns(hydrographicsRoll);
        mockRollingService.Setup(x => x.D3(1))
            .Returns(biosphereRoll);
        _classUnderTest = new TectonicWorld(mockRollingService.Object, new WorldValidation());

        // Act
        var tectonicWorld = _classUnderTest.Generate(new RttWorldgenPlanet {PlanetOrbit = orbit},
            new RttWorldgenStar {SpectralType = spectralType, Luminosity = luminosity, Age = age});

        // Assert
        Assert.That(tectonicWorld.Size, Is.EqualTo(expectedSize));
        Assert.That(tectonicWorld.Chemistry, Is.EqualTo(expectedChemistry));
        Assert.That(tectonicWorld.Biosphere, Is.EqualTo(expectedBiosphere));
        Assert.That(tectonicWorld.Atmosphere, Is.EqualTo(expectedAtmosphere));
        Assert.That(tectonicWorld.Hydrographics, Is.EqualTo(expectedHydrographics));
    }
}