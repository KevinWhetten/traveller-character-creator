using Moq;
using NUnit.Framework;
using SectorCreator.Global;
using SectorCreator.Global.Enums;
using SectorCreator.Models.RTTWorldgen;
using SectorCreator.Models.RTTWorldgen.Worlds;

namespace SectorCreator.Models.Tests.RttWorldgen.Worlds
{
    public class SnowballWorldTests
    {
        private SnowballWorld _classUnderTest = new(new RollingService());

        [TestCase(1, 1, 1, 2, 1, 0, SpectralType.A, PlanetOrbit.Inner, 0, 0, 10, PlanetChemistry.Water, 0)]
        [TestCase(3, 1, 1, 2, 1, 0, SpectralType.A, PlanetOrbit.Inner, 2, 0, 10, PlanetChemistry.Water, 0)]
        [TestCase(6, 1, 1, 2, 1, 0, SpectralType.A, PlanetOrbit.Inner, 5, 0, 10, PlanetChemistry.Water, 0)]
        [TestCase(1, 4, 1, 2, 1, 0, SpectralType.A, PlanetOrbit.Inner, 0, 0, 10, PlanetChemistry.Water, 0)]
        [TestCase(1, 5, 1, 2, 1, 0, SpectralType.A, PlanetOrbit.Inner, 0, 1, 10, PlanetChemistry.Water, 0)]
        [TestCase(1, 6, 1, 2, 1, 0, SpectralType.A, PlanetOrbit.Inner, 0, 1, 10, PlanetChemistry.Water, 0)]
        [TestCase(1, 1, 3, 2, 1, 0, SpectralType.A, PlanetOrbit.Inner, 0, 0, 10, PlanetChemistry.Water, 0)]
        [TestCase(1, 1, 4, 2, 1, 0, SpectralType.A, PlanetOrbit.Inner, 0, 0, 0, PlanetChemistry.Water, 0)]
        [TestCase(1, 1, 4, 7, 1, 0, SpectralType.A, PlanetOrbit.Inner, 0, 0, 5, PlanetChemistry.Water, 0)]
        [TestCase(1, 1, 4, 10, 1, 0, SpectralType.A, PlanetOrbit.Inner, 0, 0, 8, PlanetChemistry.Water, 0)]
        [TestCase(1, 1, 4, 12, 1, 0, SpectralType.A, PlanetOrbit.Inner, 0, 0, 10, PlanetChemistry.Water, 0)]
        [TestCase(1, 1, 6, 2, 1, 0, SpectralType.A, PlanetOrbit.Inner, 0, 0, 0, PlanetChemistry.Water, 0)]
        [TestCase(1, 1, 6, 7, 1, 0, SpectralType.A, PlanetOrbit.Inner, 0, 0, 5, PlanetChemistry.Water, 0)]
        [TestCase(1, 1, 6, 10, 1, 0, SpectralType.A, PlanetOrbit.Inner, 0, 0, 8, PlanetChemistry.Water, 0)]
        [TestCase(1, 1, 6, 12, 1, 0, SpectralType.A, PlanetOrbit.Inner, 0, 0, 10, PlanetChemistry.Water, 0)]
        [TestCase(1, 1, 1, 2, 3, 0, SpectralType.A, PlanetOrbit.Inner, 0, 0, 10, PlanetChemistry.Water, 0)]
        [TestCase(1, 1, 1, 2, 5, 0, SpectralType.A, PlanetOrbit.Inner, 0, 0, 10, PlanetChemistry.Ammonia, 0)]
        [TestCase(1, 1, 1, 2, 6, 0, SpectralType.A, PlanetOrbit.Inner, 0, 0, 10, PlanetChemistry.Ammonia, 0)]
        [TestCase(1, 1, 1, 2, 2, 0, SpectralType.L, PlanetOrbit.Inner, 0, 0, 10, PlanetChemistry.Water, 0)]
        [TestCase(1, 1, 1, 2, 3, 0, SpectralType.L, PlanetOrbit.Inner, 0, 0, 10, PlanetChemistry.Ammonia, 0)]
        [TestCase(1, 1, 1, 2, 5, 0, SpectralType.L, PlanetOrbit.Inner, 0, 0, 10, PlanetChemistry.Methane, 0)]
        [TestCase(1, 1, 1, 2, 3, 0, SpectralType.A, PlanetOrbit.Outer, 0, 0, 10, PlanetChemistry.Ammonia, 0)]
        [TestCase(6, 1, 4, 2, 1, 3, SpectralType.A, PlanetOrbit.Inner, 5, 0, 0, PlanetChemistry.Water, 3)]
        [TestCase(6, 1, 4, 2, 1, 10, SpectralType.A, PlanetOrbit.Inner, 5, 0, 0, PlanetChemistry.Water, 4)]
        public void WhenGenerating(int sizeRoll, int atmosphereRoll, int hydrosphereRoll, int hydrographicsRoll,
            int chemistryRoll,
            int age, SpectralType spectralType, PlanetOrbit planetOrbit,
            int expectedSize, int expectedAtmosphere, int expectedHydrographics, PlanetChemistry expectedChemistry,
            int expectedBiosphere)
        {
            // Setup
            var mockRollingService = new Mock<IRollingService>();
            mockRollingService.SetupSequence(x => x.D6(1))
                .Returns(hydrosphereRoll)
                .Returns(sizeRoll)
                .Returns(atmosphereRoll)
                .Returns(chemistryRoll)
                .Returns(1)
                .Returns(6);
            mockRollingService.Setup(x => x.D6(2))
                .Returns(hydrographicsRoll);
            _classUnderTest = new SnowballWorld(mockRollingService.Object);

            // Act
            var rockballWorld = _classUnderTest.Generate(new RttWorldgenPlanet {PlanetOrbit = planetOrbit},
                new RttWorldgenStar {SpectralType = spectralType, Age = age});

            // Assert
            Assert.That(rockballWorld.Size, Is.EqualTo(expectedSize));
            Assert.That(rockballWorld.Atmosphere, Is.EqualTo(expectedAtmosphere));
            Assert.That(rockballWorld.Hydrographics, Is.EqualTo(expectedHydrographics));
            Assert.That(rockballWorld.Chemistry, Is.EqualTo(expectedChemistry));
            Assert.That(rockballWorld.Biosphere, Is.EqualTo(expectedBiosphere));
        }
    }
}