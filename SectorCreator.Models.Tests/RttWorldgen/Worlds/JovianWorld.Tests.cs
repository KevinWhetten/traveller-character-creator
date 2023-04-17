using Moq;
using NUnit.Framework;
using SectorCreator.Global;
using SectorCreator.Global.Enums;
using SectorCreator.Models.RTTWorldgen;
using SectorCreator.Models.RTTWorldgen.Worlds;

namespace SectorCreator.Models.Tests.RttWorldgen.Worlds;

[TestFixture]
public class JovianWorldTests
{
    private JovianWorld _classUnderTest = new(new RollingService(), new WorldValidation());

    [TestCase(4, 7, 2, 3, PlanetOrbit.Epistellar, 0, SpectralType.A, 0, PlanetChemistry.None)]
    [TestCase(4, 7, 2, 3, PlanetOrbit.Inner, 0, SpectralType.A, 0, PlanetChemistry.None)]
    [TestCase(4, 7, 2, 3, PlanetOrbit.Inner, 3, SpectralType.A, 2, PlanetChemistry.Water)]
    [TestCase(4, 7, 2, 3, PlanetOrbit.Inner, 7, SpectralType.A, 7, PlanetChemistry.Water)]
    [TestCase(4, 7, 2, 3, PlanetOrbit.Inner, 7, SpectralType.D, 4, PlanetChemistry.Water)]
    [TestCase(4, 7, 2, 3, PlanetOrbit.Inner, 7, SpectralType.D, 4, PlanetChemistry.Water)]
    
    [TestCase(4, 2, 2, 3, PlanetOrbit.Inner, 7, SpectralType.A, 2, PlanetChemistry.Water)]
    [TestCase(4, 2, 2, 4, PlanetOrbit.Inner, 7, SpectralType.A, 2, PlanetChemistry.Ammonia)]
    [TestCase(4, 2, 2, 3, PlanetOrbit.Inner, 7, SpectralType.L, 2, PlanetChemistry.Ammonia)]
    [TestCase(6, 2, 2, 5, PlanetOrbit.Epistellar, 7, SpectralType.A, 2, PlanetChemistry.Water)]
    [TestCase(6, 2, 2, 6, PlanetOrbit.Epistellar, 7, SpectralType.A, 2, PlanetChemistry.Ammonia)]
    [TestCase(6, 2, 2, 1, PlanetOrbit.Outer, 7, SpectralType.A, 2, PlanetChemistry.Water)]
    [TestCase(6, 2, 2, 2, PlanetOrbit.Outer, 7, SpectralType.A, 2, PlanetChemistry.Ammonia)]
    public void WhenGenerating(int biosphereRoll, int d6x2Roll, int d3Roll, int chemistryRoll,
        PlanetOrbit orbit, int starAge, SpectralType spectralType,
        int expectedBiosphere, PlanetChemistry expectedChemistry)
    {
        // Setup
        var mockRollingService = new Mock<IRollingService>();
        mockRollingService.SetupSequence(x => x.D6(1))
            .Returns(biosphereRoll)
            .Returns(chemistryRoll);
        mockRollingService.Setup(x => x.D6(2))
            .Returns(d6x2Roll);
        mockRollingService.Setup(x => x.D3(1))
            .Returns(d3Roll);
        _classUnderTest = new JovianWorld(mockRollingService.Object, new WorldValidation());

        // Act
        var janiLithicWorld = _classUnderTest.Generate(new RttWorldgenPlanet {PlanetOrbit = orbit},
            new RttWorldgenStar {SpectralType = spectralType, Age = starAge});

        // Assert
        Assert.That(janiLithicWorld.Size, Is.EqualTo(16));
        Assert.That(janiLithicWorld.Atmosphere, Is.EqualTo(16));
        Assert.That(janiLithicWorld.Hydrographics, Is.EqualTo(16));
        Assert.That(janiLithicWorld.Biosphere, Is.EqualTo(expectedBiosphere));
        Assert.That(janiLithicWorld.Chemistry, Is.EqualTo(expectedChemistry));
    }
}