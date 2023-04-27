using System.Collections.Generic;
using System.Linq;
using Moq;
using NUnit.Framework;
using SectorCreator.Global;
using SectorCreator.Global.Enums;
using SectorCreator.Models.Basic;
using SectorCreator.Models.Factories.Basic;
using SectorCreator.Models.RTTWorldgen;

namespace SectorCreator.Models.Tests.Factories.Basic;

[TestFixture]
public class PlanetFactoryTests
{
    private PlanetFactory _classUnderTest = null!;
    private readonly Mock<IRollingService> _mockRollingService = new();

    [SetUp]
    public void SetUp()
    {
        _classUnderTest = new PlanetFactory(_mockRollingService.Object);
    }

    [Test]
    public void WhenGenerating()
    {
        _classUnderTest.Generate(SectorType.Basic);

        Assert.That(true);
    }

    [Test]
    public void WhenGeneratingName()
    {
        _classUnderTest.GenerateName();

        Assert.That(_classUnderTest.Planet.Name, Is.EqualTo(""));
    }

    [TestCase(2, 0)]
    [TestCase(7, 5)]
    [TestCase(10, 8)]
    [TestCase(12, 10)]
    public void WhenGeneratingSize(int sizeRoll, int expectedSize)
    {
        _mockRollingService.Setup(x => x.D6(2))
            .Returns(sizeRoll);

        _classUnderTest.GenerateSize();

        Assert.That(_classUnderTest.Planet.Size, Is.EqualTo(expectedSize));
    }

    [TestCase(0, 2, 0)]
    [TestCase(0, 7, 0)]
    [TestCase(0, 10, 3)]
    [TestCase(0, 12, 5)]
    [TestCase(5, 2, 0)]
    [TestCase(5, 7, 5)]
    [TestCase(5, 10, 8)]
    [TestCase(5, 12, 10)]
    [TestCase(8, 2, 3)]
    [TestCase(8, 7, 8)]
    [TestCase(8, 10, 11)]
    [TestCase(8, 12, 13)]
    [TestCase(10, 2, 5)]
    [TestCase(10, 7, 10)]
    [TestCase(10, 10, 13)]
    [TestCase(10, 12, 15)]
    public void WhenGeneratingAtmosphere(int size, int atmosphereRoll, int expectedAtmosphere)
    {
        _mockRollingService.Setup(x => x.D6(2))
            .Returns(atmosphereRoll);
        _classUnderTest = new PlanetFactory(_mockRollingService.Object) {
            Planet = new RttWorldgenPlanet {
                Size = size
            }
        };

        _classUnderTest.GenerateAtmosphere(SectorType.Basic);

        Assert.That(_classUnderTest.Planet.Atmosphere, Is.EqualTo(expectedAtmosphere));
    }

    [TestCase(0, 2, 0)]
    [TestCase(0, 7, 0)]
    [TestCase(0, 10, 0)]
    [TestCase(0, 12, 0)]
    [TestCase(3, 2, 0)]
    [TestCase(3, 4, 0)]
    [TestCase(3, 6, 0)]
    [TestCase(3, 8, 1)]
    [TestCase(3, 10, 10)]
    [TestCase(3, 12, 10)]
    public void WhenGeneratingSpaceOperaAtmosphere(int size, int atmosphereRoll, int expectedAtmosphere)
    {
        _mockRollingService.Setup(x => x.D6(2))
            .Returns(atmosphereRoll);
        _classUnderTest = new PlanetFactory(_mockRollingService.Object) {
            Planet = new RttWorldgenPlanet {
                Size = size
            }
        };

        _classUnderTest.GenerateAtmosphere(SectorType.SpaceOpera);

        Assert.That(_classUnderTest.Planet.Atmosphere, Is.EqualTo(expectedAtmosphere));
    }

    [TestCase(5, 7, 7)]
    [TestCase(4, 0, 0)]
    [TestCase(4, 2, 0)]
    [TestCase(4, 3, 1)]
    [TestCase(4, 5, 1)]
    [TestCase(4, 6, 10)]
    [TestCase(4, 10, 10)]
    [TestCase(4, 13, 10)]
    [TestCase(4, 15, 10)]
    [TestCase(3, 0, 0)]
    [TestCase(3, 2, 0)]
    [TestCase(3, 3, 1)]
    [TestCase(3, 5, 1)]
    [TestCase(3, 6, 10)]
    [TestCase(3, 10, 10)]
    [TestCase(3, 13, 10)]
    [TestCase(3, 15, 10)]
    [TestCase(2, 7, 0)]
    [TestCase(1, 7, 0)]
    [TestCase(0, 7, 0)]
    public void WhenModifyingAtmosphereForSpaceOpera(int size, int atmosphere, int expectedAtmosphere)
    {
        _classUnderTest = new PlanetFactory(_mockRollingService.Object) {
            Planet = new RttWorldgenPlanet {
                Size = size,
                Atmosphere = atmosphere
            }
        };

        _classUnderTest.ModifyAtmosphere();

        Assert.That(_classUnderTest.Planet.Atmosphere, Is.EqualTo(expectedAtmosphere));
    }

    [TestCase(0, 7, Temperature.Temperate)]
    [TestCase(1, 7, Temperature.Temperate)]
    [TestCase(2, 7, Temperature.Cold)]
    [TestCase(3, 7, Temperature.Cold)]
    [TestCase(4, 7, Temperature.Temperate)]
    [TestCase(5, 7, Temperature.Temperate)]
    [TestCase(6, 7, Temperature.Temperate)]
    [TestCase(7, 7, Temperature.Temperate)]
    [TestCase(8, 7, Temperature.Temperate)]
    [TestCase(9, 7, Temperature.Temperate)]
    [TestCase(10, 7, Temperature.Temperate)]
    [TestCase(11, 7, Temperature.Boiling)]
    [TestCase(12, 7, Temperature.Boiling)]
    [TestCase(13, 7, Temperature.Temperate)]
    [TestCase(14, 7, Temperature.Temperate)]
    [TestCase(15, 7, Temperature.Temperate)]
    public void WhenGeneratingPlanetTemperature(int atmosphere, int temperatureRoll, Temperature expectedTemperature)
    {
        _mockRollingService.Setup(x => x.D6(2))
            .Returns(temperatureRoll);
        _classUnderTest = new PlanetFactory(_mockRollingService.Object) {
            Planet = new RttWorldgenPlanet {
                Atmosphere = atmosphere
            }
        };

        _classUnderTest.GenerateTemperature();

        Assert.That(_classUnderTest.Planet.Temperature, Is.EqualTo(expectedTemperature));
    }

    [TestCase(0, 2, 0)]
    [TestCase(0, 7, 0)]
    [TestCase(0, 10, 0)]
    [TestCase(0, 12, 0)]
    [TestCase(5, 2, 2 - 7 + 5)]
    [TestCase(5, 7, 7 - 7 + 5)]
    [TestCase(5, 10, 10 - 7 + 5)]
    [TestCase(5, 12, 12 - 7 + 5)]
    [TestCase(7, 2, 2 - 7 + 7)]
    [TestCase(7, 7, 7 - 7 + 7)]
    [TestCase(7, 10, 10)]
    [TestCase(7, 12, 10)]
    [TestCase(10, 2, 2 - 7 + 10)]
    [TestCase(10, 7, 7 - 7 + 10)]
    [TestCase(10, 10, 10)]
    [TestCase(10, 12, 10)]
    public void WhenGeneratingHydrographics(int size, int hydrographicsRoll, int expectedHydrographics)
    {
        _mockRollingService.Setup(x => x.D6(2))
            .Returns(hydrographicsRoll);
        _classUnderTest = new PlanetFactory(_mockRollingService.Object) {
            Planet = new Planet {
                Size = size,
                Atmosphere = 3
            }
        };

        _classUnderTest.GenerateHydrographics(SectorType.Basic);

        Assert.That(_classUnderTest.Planet.Hydrographics, Is.EqualTo(expectedHydrographics));
    }

    [TestCase(Temperature.Frozen, 5)]
    [TestCase(Temperature.Cold, 5)]
    [TestCase(Temperature.Temperate, 5)]
    [TestCase(Temperature.Hot, 3)]
    [TestCase(Temperature.Boiling, 0)]
    public void WhenGeneratingHydrographicsWithTemperature(Temperature temperature, int expectedHydrographics)
    {
        _mockRollingService.Setup(x => x.D6(2))
            .Returns(7);
        _classUnderTest = new PlanetFactory(_mockRollingService.Object) {
            Planet = new Planet {
                Size = 5,
                Temperature = temperature,
                Atmosphere = 3
            }
        };

        _classUnderTest.GenerateHydrographics(SectorType.Basic);

        Assert.That(_classUnderTest.Planet.Hydrographics, Is.EqualTo(expectedHydrographics));
    }

    [TestCase(0)]
    [TestCase(1)]
    public void WhenGeneratingHydrographicsAndSizeIs0Or1(int size)
    {
        _mockRollingService.Setup(x => x.D6(2))
            .Returns(12);
        _classUnderTest = new PlanetFactory(_mockRollingService.Object) {
            Planet = new Planet {
                Size = size,
                Atmosphere = 3
            }
        };

        _classUnderTest.GenerateHydrographics(SectorType.Basic);

        Assert.That(_classUnderTest.Planet.Hydrographics, Is.EqualTo(0));
    }

    [TestCase(0, 10 - 4)]
    [TestCase(1, 10 - 4)]
    [TestCase(5, 10)]
    [TestCase(10, 10 - 4)]
    [TestCase(11, 10 - 4)]
    [TestCase(12, 10 - 4)]
    [TestCase(13, 10)]
    [TestCase(15, 10)]
    public void WhenGeneratingHydrographicsAndAtmosphereIsSpecial(int atmoshpere, int expectedHydrographics)
    {
        _mockRollingService.Setup(x => x.D6(2))
            .Returns(12);
        _classUnderTest = new PlanetFactory(_mockRollingService.Object) {
            Planet = new Planet {
                Size = 5,
                Atmosphere = atmoshpere
            }
        };

        _classUnderTest.GenerateHydrographics(SectorType.Basic);

        Assert.That(_classUnderTest.Planet.Hydrographics, Is.EqualTo(expectedHydrographics));
    }

    [TestCase(5, 5, 7, 7 - 7 + 5)]
    [TestCase(3, 10, 10, 10 - 7 + 3 - 6)]
    [TestCase(3, 10, 11, 11 - 7 + 3 - 6)]
    [TestCase(5, 0, 8, 8 - 7 + 5 - 6)]
    [TestCase(5, 0, 9, 9 - 7 + 5 - 6)]
    [TestCase(5, 1, 8, 8 - 7 + 5 - 6)]
    [TestCase(5, 1, 9, 9 - 7 + 5 - 6)]
    [TestCase(5, 2, 6, 6 - 7 + 5 - 4)]
    [TestCase(5, 2, 7, 7 - 7 + 5 - 4)]
    [TestCase(5, 3, 6, 6 - 7 + 5 - 4)]
    [TestCase(5, 3, 7, 7 - 7 + 5 - 4)]
    [TestCase(5, 11, 6, 6 - 7 + 5 - 4)]
    [TestCase(5, 11, 7, 7 - 7 + 5 - 4)]
    [TestCase(5, 12, 6, 6 - 7 + 5 - 4)]
    [TestCase(5, 12, 7, 7 - 7 + 5 - 4)]
    public void WhenGeneratingSpaceOperaHydrographics(int size, int atmosphere, int hydrographicsRoll, int expectedHydrographics)
    {
        _mockRollingService.Setup(x => x.D6(2))
            .Returns(hydrographicsRoll);
        _classUnderTest = new PlanetFactory(_mockRollingService.Object) {
            Planet = new Planet {
                Size = size,
                Atmosphere = atmosphere
            }
        };

        _classUnderTest.GenerateHydrographics(SectorType.SpaceOpera);

        Assert.That(_classUnderTest.Planet.Hydrographics, Is.EqualTo(expectedHydrographics));
    }

    [TestCase(2, 0)]
    [TestCase(7, 5)]
    [TestCase(10, 8)]
    [TestCase(12, 10)]
    public void WhenGeneratingPopulation(int popRoll, int expectedPop)
    {
        _mockRollingService.Setup(x => x.D6(2))
            .Returns(popRoll);
        _classUnderTest = new PlanetFactory(_mockRollingService.Object);

        _classUnderTest.GeneratePopulation(SectorType.Basic);

        Assert.That(_classUnderTest.Planet.Population, Is.EqualTo(expectedPop));
    }

    [TestCase(5, 4, 7, 4)]
    [TestCase(5, 5, 7, 6)]
    [TestCase(5, 6, 7, 6)]
    [TestCase(5, 7, 7, 4)]
    [TestCase(5, 8, 7, 6)]
    [TestCase(5, 9, 7, 4)]
    [TestCase(0, 4, 7, 3)]
    [TestCase(0, 5, 7, 5)]
    [TestCase(0, 6, 7, 5)]
    [TestCase(0, 7, 7, 3)]
    [TestCase(0, 8, 7, 5)]
    [TestCase(0, 9, 7, 3)]
    [TestCase(2, 4, 7, 3)]
    [TestCase(2, 5, 7, 5)]
    [TestCase(2, 6, 7, 5)]
    [TestCase(2, 7, 7, 3)]
    [TestCase(2, 8, 7, 5)]
    [TestCase(2, 9, 7, 3)]
    [TestCase(10, 4, 7, 3)]
    [TestCase(10, 5, 7, 5)]
    [TestCase(10, 6, 7, 5)]
    [TestCase(10, 7, 7, 3)]
    [TestCase(10, 8, 7, 5)]
    [TestCase(10, 9, 7, 3)]
    public void WhenGeneratingPopulationForHardScience(int size, int atmosphere, int popRoll, int expectedPop)
    {
        _mockRollingService.Setup(x => x.D6(2))
            .Returns(popRoll);
        _classUnderTest = new PlanetFactory(_mockRollingService.Object) {
            Planet = new RttWorldgenPlanet {
                Size = size,
                Atmosphere = atmosphere
            }
        };

        _classUnderTest.GeneratePopulation(SectorType.HardScience);

        Assert.That(_classUnderTest.Planet.Population, Is.EqualTo(expectedPop));
    }

    [TestCase(0, 2, 0)]
    [TestCase(0, 7, 0)]
    [TestCase(0, 10, 0)]
    [TestCase(0, 12, 0)]
    [TestCase(3, 2, 0)]
    [TestCase(3, 7, 7 - 7 + 3)]
    [TestCase(3, 10, 10 - 7 + 3)]
    [TestCase(3, 12, 12 - 7 + 3)]
    [TestCase(5, 2, 2 - 7 + 5)]
    [TestCase(5, 7, 7 - 7 + 5)]
    [TestCase(5, 10, 10 - 7 + 5)]
    [TestCase(5, 12, 12 - 7 + 5)]
    [TestCase(7, 2, 2 - 7 + 7)]
    [TestCase(7, 7, 7 - 7 + 7)]
    [TestCase(7, 10, 10 - 7 + 7)]
    [TestCase(7, 12, 12 - 7 + 7)]
    [TestCase(10, 2, 2 - 7 + 10)]
    [TestCase(10, 7, 7 - 7 + 10)]
    [TestCase(10, 10, 10 - 7 + 10)]
    [TestCase(10, 12, 12 - 7 + 10)]
    public void WhenGeneratingGovernment(int population, int govRoll, int expectedGov)
    {
        _mockRollingService.Setup(x => x.D6(2))
            .Returns(govRoll);
        _classUnderTest = new PlanetFactory(_mockRollingService.Object) {
            Planet = new RttWorldgenPlanet {
                Population = population
            }
        };

        _classUnderTest.GenerateGovernment();

        Assert.That(_classUnderTest.Planet.Government, Is.EqualTo(expectedGov));
    }

    [TestCase(0, 2, 0)]
    [TestCase(0, 7, 0)]
    [TestCase(0, 10, 10 - 7 + 0)]
    [TestCase(0, 12, 12 - 7 + 0)]
    [TestCase(3, 2, 0)]
    [TestCase(3, 7, 7 - 7 + 3)]
    [TestCase(3, 10, 10 - 7 + 3)]
    [TestCase(3, 12, 12 - 7 + 3)]
    [TestCase(5, 2, 2 - 7 + 5)]
    [TestCase(5, 7, 7 - 7 + 5)]
    [TestCase(5, 10, 10 - 7 + 5)]
    [TestCase(5, 12, 12 - 7 + 5)]
    [TestCase(7, 2, 2 - 7 + 7)]
    [TestCase(7, 7, 7 - 7 + 7)]
    [TestCase(7, 10, 10 - 7 + 7)]
    [TestCase(7, 12, 12 - 7 + 7)]
    [TestCase(10, 2, 2 - 7 + 10)]
    [TestCase(10, 7, 7 - 7 + 10)]
    [TestCase(10, 10, 10 - 7 + 10)]
    [TestCase(10, 12, 12 - 7 + 10)]
    [TestCase(15, 2, 2 - 7 + 15)]
    [TestCase(15, 7, 7 - 7 + 15)]
    [TestCase(15, 10, 10 - 7 + 15)]
    [TestCase(15, 12, 12 - 7 + 15)]
    public void WhenGeneratingLawLevel(int government, int lawLevelRoll, int expectedLawLevel)
    {
        _mockRollingService.Setup(x => x.D6(2))
            .Returns(lawLevelRoll);
        _classUnderTest = new PlanetFactory(_mockRollingService.Object) {
            Planet = new RttWorldgenPlanet {
                Population = 5,
                Government = government
            }
        };

        _classUnderTest.GenerateLawLevel();

        Assert.That(_classUnderTest.Planet.LawLevel, Is.EqualTo(expectedLawLevel));
    }

    [TestCase(2, 'X')]
    [TestCase(3, 'E')]
    [TestCase(4, 'E')]
    [TestCase(5, 'D')]
    [TestCase(6, 'D')]
    [TestCase(7, 'C')]
    [TestCase(8, 'C')]
    [TestCase(9, 'B')]
    [TestCase(10, 'B')]
    [TestCase(11, 'A')]
    [TestCase(12, 'A')]
    public void WhenGeneratingStarport(int starportRoll, char expectedStarport)
    {
        _mockRollingService.Setup(x => x.D6(2))
            .Returns(starportRoll);
        _classUnderTest = new PlanetFactory(_mockRollingService.Object);

        _classUnderTest.GenerateStarport(SectorType.Basic);

        Assert.That(_classUnderTest.Planet.Starport, Is.EqualTo(expectedStarport));
    }

    [TestCase(2, 7, 'X')]
    [TestCase(3, 7, 'E')]
    [TestCase(4, 7, 'E')]
    [TestCase(5, 7, 'D')]
    [TestCase(6, 7, 'D')]
    [TestCase(7, 7, 'C')]
    [TestCase(8, 7, 'C')]
    [TestCase(9, 7, 'B')]
    [TestCase(10, 7, 'B')]
    [TestCase(11, 7, 'A')]
    [TestCase(12, 7, 'A')]
    public void WhenGeneratingStarportForHardScience(int population, int starportRoll, char expectedStarport)
    {
        _mockRollingService.Setup(x => x.D6(2))
            .Returns(starportRoll);
        _classUnderTest = new PlanetFactory(_mockRollingService.Object) {
            Planet = new RttWorldgenPlanet {
                Population = population
            }
        };

        _classUnderTest.GenerateStarport(SectorType.HardScience);

        Assert.That(_classUnderTest.Planet.Starport, Is.EqualTo(expectedStarport));
    }

    [TestCase('X', 0)]
    [TestCase('E', 0)]
    [TestCase('D', 0)]
    [TestCase('C', 2)]
    [TestCase('B', 4)]
    [TestCase('A', 6)]
    public void WhenGeneratingTechLevelForStarport(char starport, int expectedTechLevel)
    {
        _classUnderTest = new PlanetFactory(_mockRollingService.Object) {
            Planet = {
                Starport = starport,
                Size = 5,
                Atmosphere = 5,
                Hydrographics = 5,
                Government = 4
            }
        };

        _classUnderTest.GenerateTechnologyLevel();

        Assert.That(_classUnderTest.Planet.TechLevel, Is.EqualTo(expectedTechLevel));
    }

    [TestCase(0, 2)]
    [TestCase(1, 2)]
    [TestCase(2, 1)]
    [TestCase(3, 1)]
    [TestCase(4, 1)]
    [TestCase(5, 0)]
    [TestCase(7, 0)]
    [TestCase(10, 0)]
    [TestCase(15, 0)]
    public void WhenGeneratingTechLevelForSize(int size, int expectedTechLevel)
    {
        _classUnderTest = new PlanetFactory(_mockRollingService.Object) {
            Planet = {
                Starport = 'D',
                Size = size,
                Atmosphere = 5,
                Hydrographics = 5,
                Government = 4
            }
        };

        _classUnderTest.GenerateTechnologyLevel();

        Assert.That(_classUnderTest.Planet.TechLevel, Is.EqualTo(expectedTechLevel));
    }


    [TestCase(0, 1)]
    [TestCase(1, 1)]
    [TestCase(2, 1)]
    [TestCase(3, 1)]
    [TestCase(4, 0)]
    [TestCase(7, 0)]
    [TestCase(9, 0)]
    [TestCase(10, 1)]
    [TestCase(11, 1)]
    [TestCase(12, 1)]
    [TestCase(13, 1)]
    [TestCase(14, 1)]
    [TestCase(15, 1)]
    public void WhenGeneratingTechLevelForAtmosphere(int atmosphere, int expectedTechLevel)
    {
        _classUnderTest = new PlanetFactory(_mockRollingService.Object) {
            Planet = {
                Starport = 'D',
                Size = 5,
                Atmosphere = atmosphere,
                Hydrographics = 5,
                Government = 4
            }
        };

        _classUnderTest.GenerateTechnologyLevel();

        Assert.That(_classUnderTest.Planet.TechLevel, Is.EqualTo(expectedTechLevel));
    }

    [TestCase(0, 1)]
    [TestCase(1, 0)]
    [TestCase(3, 0)]
    [TestCase(5, 0)]
    [TestCase(7, 0)]
    [TestCase(8, 0)]
    [TestCase(9, 1)]
    [TestCase(10, 2)]
    public void WhenGeneratingTechLevelForHydrographics(int hydrographics, int expectedTechLevel)
    {
        _classUnderTest = new PlanetFactory(_mockRollingService.Object) {
            Planet = {
                Starport = 'D',
                Size = 5,
                Atmosphere = 5,
                Hydrographics = hydrographics,
                Government = 4
            }
        };

        _classUnderTest.GenerateTechnologyLevel();

        Assert.That(_classUnderTest.Planet.TechLevel, Is.EqualTo(expectedTechLevel));
    }

    [TestCase(0, 0)]
    [TestCase(1, 1)]
    [TestCase(2, 1)]
    [TestCase(3, 1)]
    [TestCase(4, 1)]
    [TestCase(5, 1)]
    [TestCase(6, 0)]
    [TestCase(7, 0)]
    [TestCase(8, 0)]
    [TestCase(9, 1)]
    [TestCase(10, 2)]
    [TestCase(11, 3)]
    [TestCase(12, 4)]
    public void WhenGeneratingTechLevelForPopulation(int population, int expectedTechLevel)
    {
        _classUnderTest = new PlanetFactory(_mockRollingService.Object) {
            Planet = {
                Starport = 'D',
                Size = 5,
                Atmosphere = 5,
                Hydrographics = 5,
                Population = population,
                Government = 4
            }
        };

        _classUnderTest.GenerateTechnologyLevel();

        Assert.That(_classUnderTest.Planet.TechLevel, Is.EqualTo(expectedTechLevel));
    }

    [TestCase(0, 1)]
    [TestCase(1, 0)]
    [TestCase(2, 0)]
    [TestCase(3, 0)]
    [TestCase(4, 0)]
    [TestCase(5, 1)]
    [TestCase(6, 0)]
    [TestCase(7, 2)]
    [TestCase(8, 0)]
    [TestCase(9, 0)]
    [TestCase(10, 0)]
    [TestCase(11, 0)]
    [TestCase(12, 0)]
    [TestCase(13, 0)]
    [TestCase(14, 0)]
    [TestCase(15, 0)]
    public void WhenGeneratingTechLevelForGovernment(int government, int expectedTechLevel)
    {
        _classUnderTest = new PlanetFactory(_mockRollingService.Object) {
            Planet = {
                Starport = 'D',
                Size = 5,
                Atmosphere = 5,
                Hydrographics = 5,
                Population = 7,
                Government = government
            }
        };

        _classUnderTest.GenerateTechnologyLevel();

        Assert.That(_classUnderTest.Planet.TechLevel, Is.EqualTo(expectedTechLevel));
    }

    [Test]
    public void WhenGeneratingTechLevelForXStarport()
    {
        _classUnderTest = new PlanetFactory(_mockRollingService.Object) {
            Planet = {
                Starport = 'X',
                Size = 0,
                Atmosphere = 0,
                Hydrographics = 0,
                Population = 10,
                Government = 7
            }
        };

        _classUnderTest.GenerateTechnologyLevel();

        Assert.That(_classUnderTest.Planet.TechLevel, Is.EqualTo(4));
    }

    [TestCase(13)]
    [TestCase(14)]
    public void WhenGeneratingTechLevelForNegativeGovernment(int government)
    {
        _classUnderTest = new PlanetFactory(_mockRollingService.Object) {
            Planet = {
                Starport = 'A',
                Size = 5,
                Atmosphere = 5,
                Hydrographics = 5,
                Population = 6,
                Government = government
            }
        };

        _classUnderTest.GenerateTechnologyLevel();

        Assert.That(_classUnderTest.Planet.TechLevel, Is.EqualTo(4));
    }

    [TestCase(5, 5, 5, TravelCode.None)]
    [TestCase(10, 5, 5, TravelCode.None)]
    [TestCase(5, 0, 5, TravelCode.None)]
    [TestCase(5, 7, 5, TravelCode.None)]
    [TestCase(5, 10, 5, TravelCode.None)]
    [TestCase(5, 5, 0, TravelCode.None)]
    [TestCase(5, 5, 10, TravelCode.None)]
    [TestCase(10, 0, 0, TravelCode.Amber)]
    [TestCase(11, 7, 9, TravelCode.Amber)]
    [TestCase(12, 10, 10, TravelCode.Amber)]
    [TestCase(9, 0, 0, TravelCode.None)]
    [TestCase(10, 0, 8, TravelCode.None)]
    public void WhenGettingTravelCode(int atmosphere, int government, int lawLevel, TravelCode expected)
    {
        _classUnderTest = new PlanetFactory(_mockRollingService.Object) {
            Planet = {
                Atmosphere = atmosphere,
                Government = government,
                LawLevel = lawLevel
            }
        };

        _classUnderTest.GetTravelCode();

        Assert.That(_classUnderTest.Planet.TravelCode, Is.EqualTo(expected));
    }


    private static List<object> StarportDBasesTestCase = new(new List<BaseTestCase> {
        new() {
            Rolls = new List<int> {6}
        },
        new() {
            Rolls = new List<int> {7},
            Bases = new List<Base> {Base.Scout}
        }
    });

    [TestCaseSource(nameof(StarportDBasesTestCase))]
    public void WhenGeneratingBasesForDStarport(object args)
    {
        _mockRollingService.SetupSequence(x => x.D6(2))
            .Returns(((BaseTestCase) args).Rolls.First());
        _classUnderTest = new PlanetFactory(_mockRollingService.Object) {
            Planet = {
                Starport = 'D'
            }
        };

        _classUnderTest.GenerateBases();

        Assert.That(_classUnderTest.Planet.Bases.Count, Is.EqualTo(((BaseTestCase) args).Bases.Count));

        foreach (var expected in ((BaseTestCase) args).Bases) {
            Assert.That(_classUnderTest.Planet.Bases, Does.Contain(expected));
        }
    }


    private static List<object> StarportCBasesTestCase = new(new List<BaseTestCase> {
        new() {
            Rolls = new List<int> {0, 0, 0}
        },
        new() {
            Rolls = new List<int> {7, 9, 9}
        },
        new() {
            Rolls = new List<int> {8, 9, 9},
            Bases = new List<Base> {Base.Scout}
        },
        new() {
            Rolls = new List<int> {7, 10, 9},
            Bases = new List<Base> {Base.Research}
        },
        new() {
            Rolls = new List<int> {7, 9, 10},
            Bases = new List<Base> {Base.Tas}
        },
        new() {
            Rolls = new List<int> {8, 10, 9},
            Bases = new List<Base> {Base.Scout, Base.Research}
        },
        new() {
            Rolls = new List<int> {8, 9, 10},
            Bases = new List<Base> {Base.Scout, Base.Tas}
        },
        new() {
            Rolls = new List<int> {7, 10, 10},
            Bases = new List<Base> {Base.Research, Base.Tas}
        },
        new() {
            Rolls = new List<int> {8, 10, 10},
            Bases = new List<Base> {Base.Scout, Base.Research, Base.Tas}
        }
    });

    [TestCaseSource(nameof(StarportCBasesTestCase))]
    public void WhenGeneratingBasesForCStarport(object args)
    {
        _mockRollingService.SetupSequence(x => x.D6(2))
            .Returns(((BaseTestCase) args).Rolls.First())
            .Returns(((BaseTestCase) args).Rolls[1])
            .Returns(((BaseTestCase) args).Rolls.Last());
        _classUnderTest = new PlanetFactory(_mockRollingService.Object) {
            Planet = {
                Starport = 'C'
            }
        };

        _classUnderTest.GenerateBases();

        Assert.That(_classUnderTest.Planet.Bases.Count, Is.EqualTo(((BaseTestCase) args).Bases.Count));

        foreach (var expected in ((BaseTestCase) args).Bases) {
            Assert.That(_classUnderTest.Planet.Bases, Does.Contain(expected));
        }
    }

    private static List<object> StarportBBasesTestCase = new(new List<BaseTestCase> {
        new() {
            Rolls = new List<int> {0, 0, 0},
            Bases = new List<Base> {Base.Tas}
        },
        new() {
            Rolls = new List<int> {7, 7, 9},
            Bases = new List<Base> {Base.Tas}
        },
        new() {
            Rolls = new List<int> {8, 7, 9},
            Bases = new List<Base> {Base.Naval, Base.Tas}
        },
        new() {
            Rolls = new List<int> {7, 8, 9},
            Bases = new List<Base> {Base.Scout, Base.Tas}
        },
        new() {
            Rolls = new List<int> {7, 7, 10},
            Bases = new List<Base> {Base.Research, Base.Tas}
        },
        new() {
            Rolls = new List<int> {8, 8, 9},
            Bases = new List<Base> {Base.Naval, Base.Scout, Base.Tas}
        },
        new() {
            Rolls = new List<int> {8, 7, 10},
            Bases = new List<Base> {Base.Naval, Base.Research, Base.Tas}
        },
        new() {
            Rolls = new List<int> {7, 8, 10},
            Bases = new List<Base> {Base.Scout, Base.Research, Base.Tas}
        },
        new() {
            Rolls = new List<int> {8, 8, 10},
            Bases = new List<Base> {Base.Naval, Base.Scout, Base.Research, Base.Tas}
        }
    });

    [TestCaseSource(nameof(StarportBBasesTestCase))]
    public void WhenGeneratingBasesForBStarport(object args)
    {
        _mockRollingService.SetupSequence(x => x.D6(2))
            .Returns(((BaseTestCase) args).Rolls.First())
            .Returns(((BaseTestCase) args).Rolls[1])
            .Returns(((BaseTestCase) args).Rolls.Last());
        _classUnderTest = new PlanetFactory(_mockRollingService.Object) {
            Planet = {
                Starport = 'B'
            }
        };

        _classUnderTest.GenerateBases();

        Assert.That(_classUnderTest.Planet.Bases.Count, Is.EqualTo(((BaseTestCase) args).Bases.Count));

        foreach (var expected in ((BaseTestCase) args).Bases) {
            Assert.That(_classUnderTest.Planet.Bases, Does.Contain(expected));
        }
    }


    private static List<object> StarportABasesTestCase = new(new List<BaseTestCase> {
        new() {
            Rolls = new List<int> {0, 0, 0},
            Bases = new List<Base> {Base.Tas}
        },
        new() {
            Rolls = new List<int> {7, 9, 7},
            Bases = new List<Base> {Base.Tas}
        },
        new() {
            Rolls = new List<int> {8, 9, 7},
            Bases = new List<Base> {Base.Naval, Base.Tas}
        },
        new() {
            Rolls = new List<int> {7, 10, 7},
            Bases = new List<Base> {Base.Scout, Base.Tas}
        },
        new() {
            Rolls = new List<int> {7, 9, 8},
            Bases = new List<Base> {Base.Research, Base.Tas}
        },
        new() {
            Rolls = new List<int> {8, 10, 7},
            Bases = new List<Base> {Base.Naval, Base.Scout, Base.Tas}
        },
        new() {
            Rolls = new List<int> {8, 9, 8},
            Bases = new List<Base> {Base.Naval, Base.Research, Base.Tas}
        },
        new() {
            Rolls = new List<int> {7, 10, 8},
            Bases = new List<Base> {Base.Scout, Base.Research, Base.Tas}
        },
        new() {
            Rolls = new List<int> {8, 10, 8},
            Bases = new List<Base> {Base.Naval, Base.Scout, Base.Research, Base.Tas}
        }
    });

    [TestCaseSource(nameof(StarportABasesTestCase))]
    public void WhenGeneratingBasesForAStarport(object args)
    {
        _mockRollingService.SetupSequence(x => x.D6(2))
            .Returns(((BaseTestCase) args).Rolls.First())
            .Returns(((BaseTestCase) args).Rolls[1])
            .Returns(((BaseTestCase) args).Rolls.Last());
        _classUnderTest = new PlanetFactory(_mockRollingService.Object) {
            Planet = {
                Starport = 'A'
            }
        };

        _classUnderTest.GenerateBases();

        Assert.That(_classUnderTest.Planet.Bases.Count, Is.EqualTo(((BaseTestCase) args).Bases.Count));

        foreach (var expected in ((BaseTestCase) args).Bases) {
            Assert.That(_classUnderTest.Planet.Bases, Does.Contain(expected));
        }
    }
}

internal class BaseTestCase
{
    internal List<Base> Bases = new();
    internal List<int> Rolls = new();
}