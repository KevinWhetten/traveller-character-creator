using Moq;
using NUnit.Framework;
using SectorCreator.Global;
using SectorCreator.Global.Enums;
using SectorCreator.Models.RTTWorldgen;
using SectorCreator.Models.RTTWorldgen.Worlds;

namespace SectorCreator.Models.Tests.RttWorldgen.Worlds;

[TestFixture]
public class AridWorldTests
{
    private AridWorld _classUnderTest = new(new RollingService(), new WorldValidation());

    [TestCase(1, 5, 12, 1, 0, PlanetOrbit.Inner, SpectralType.A, Luminosity.I, 5, PlanetChemistry.Water, 0, 10)]
    [TestCase(1, 5, 12, 1, 0, PlanetOrbit.Inner, SpectralType.K, Luminosity.V, 5, PlanetChemistry.Ammonia, 0, 10)]
    [TestCase(1, 5, 12, 1, 0, PlanetOrbit.Inner, SpectralType.M, Luminosity.V, 5, PlanetChemistry.Methane, 0, 10)]
    [TestCase(1, 5, 12, 1, 0, PlanetOrbit.Inner, SpectralType.L, Luminosity.V, 5, PlanetChemistry.Methane, 0, 10)]
    [TestCase(1, 3, 12, 1, 0, PlanetOrbit.Outer, SpectralType.L, Luminosity.V, 5, PlanetChemistry.Methane, 0, 10)]
    [TestCase(1, 5, 12, 1, 2, PlanetOrbit.Inner, SpectralType.A, Luminosity.I, 5, PlanetChemistry.Water, 1, 10)]
    [TestCase(1, 5, 12, 1, 5, PlanetOrbit.Inner, SpectralType.A, Luminosity.I, 5, PlanetChemistry.Water, 12, 9)]
    [TestCase(1, 5, 12, 1, 5, PlanetOrbit.Inner, SpectralType.L, Luminosity.I, 5, PlanetChemistry.Water, 9, 9)]
    [Repeat(50)]
    public void WhenGenerating(int sizeRoll, int chemistryRoll, int biosphereRoll, int atmosphereRoll,
        int age, PlanetOrbit orbit, SpectralType spectralType, Luminosity luminosity,
        int expectedSize, PlanetChemistry expectedChemistry, int expectedBiosphere, int expectedAtmosphere)
    {
        // Setup
        var rollingServiceMock = new Mock<IRollingService>();
        rollingServiceMock.SetupSequence(x => x.D6(1))
            .Returns(sizeRoll)
            .Returns(chemistryRoll);
        rollingServiceMock.Setup(x => x.D6(2)).Returns(biosphereRoll);
        rollingServiceMock.Setup(x => x.D3(1)).Returns(1);
        _classUnderTest = new AridWorld(rollingServiceMock.Object, new WorldValidation());

        // Act
        var aridWorld = _classUnderTest.Generate(new RttWorldgenPlanet {PlanetOrbit = orbit},
            new RttWorldgenStar {SpectralType = spectralType, Luminosity = luminosity, Age = age});

        // Assert
        Assert.That(aridWorld.Size, Is.EqualTo(expectedSize));
        Assert.That(aridWorld.Chemistry, Is.EqualTo(expectedChemistry));
        Assert.That(aridWorld.Biosphere, Is.EqualTo(expectedBiosphere));
        Assert.That(aridWorld.Atmosphere, Is.EqualTo(expectedAtmosphere));
    }
}