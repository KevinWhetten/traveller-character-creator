using Moq;
using NUnit.Framework;
using SectorCreator.Global;
using SectorCreator.Global.Enums;
using SectorCreator.Models.Basic;
using SectorCreator.Models.Factories.Basic;
using SectorCreator.Models.Factories.RttWorldgen;
using SectorCreator.Models.Factories.StarFrontiers;
using SectorCreator.Models.RTTWorldgen;

namespace SectorCreator.Models.Tests.Factories.Basic;

[TestFixture]
public class StarSystemFactoryTests
{
    StarSystemFactory _classUnderTest;
    private readonly Mock<IRollingService> _rollingServiceMock = new();
    private readonly Mock<IPlanetFactory> _planetFactoryMock = new();
    private readonly Mock<IStarFrontiersStarFactory> _starFrontiersStarFactoryMock = new();
    private readonly Mock<IRttWorldgenStarFactory> _rttWorldgenStarFactoryMock = new();
    private readonly Mock<IStarFrontiersPlanetFactory> _starFrontiersPlanetFactoryMock = new();
    private readonly Mock<IRttWorldgenPlanetFactory> _rttWorldgenPlanetFactoryMock = new();

    [SetUp]
    public void SetUp()
    {
        var spectralRoll = 0;
        
        _rollingServiceMock.Setup(x => x.D6(It.IsAny<int>())).Returns(1);
        _planetFactoryMock.Setup(x => x.Generate(It.IsAny<SectorType>(), It.IsAny<Coordinates>()))
            .Returns(new Planet());
        _starFrontiersStarFactoryMock.Setup(x => x.Generate()).Returns(new Star());
        _rttWorldgenStarFactoryMock.Setup(x => x.Generate(It.IsAny<StarType>(), out spectralRoll))
            .Returns(new RttWorldgenStar());
        _rttWorldgenStarFactoryMock.Setup(x => x.Generate(It.IsAny<StarType>(),It.IsAny<int>()))
            .Returns(new RttWorldgenStar());
        _starFrontiersPlanetFactoryMock.Setup(x => x.Generate(It.IsAny<SectorType>(), It.IsAny<Coordinates>())).Returns(new Planet());
        _rttWorldgenPlanetFactoryMock.Setup(x => x.Generate(It.IsAny<SectorType>(), It.IsAny<Coordinates>()))
            .Returns(new RttWorldgenPlanet());

        _classUnderTest = new StarSystemFactory(_rollingServiceMock.Object, _planetFactoryMock.Object,
            _starFrontiersStarFactoryMock.Object, _starFrontiersPlanetFactoryMock.Object);
    }

    [TestCase(0, false)]
    [TestCase(3, false)]
    [TestCase(4, true)]
    [TestCase(12, true)]
    public void WhenGeneratingMongooseStarSystem(int gasGiantRoll, bool expectedGasGiant)
    {
        _rollingServiceMock.Setup(x => x.D6(2))
            .Returns(gasGiantRoll);

        var starSystem = _classUnderTest.GenerateMongooseStarSystem(SectorType.Basic, new Coordinates());

        Assert.That(starSystem.Planets.Count, Is.EqualTo(1));
        Assert.That(starSystem.CompanionStars.Count, Is.EqualTo(0));
        Assert.That(starSystem.GasGiant, Is.EqualTo(expectedGasGiant));
    }

    [Test]
    public void WhenGeneratingT5StarSystem()
    {
        throw new InconclusiveException("Not Implemented");
    }

    [TestCase(1, 1, false, 0, 1)]
    [TestCase(7, 1, false, 0, 1)]
    [TestCase(8, 1, false, 0, 2)]
    [TestCase(10, 1, false, 0, 2)]
    [TestCase(1, 3, false, 0, 1)]
    [TestCase(1, 4, false, 1, 1)]
    [TestCase(1, 6, false, 1, 1)]
    [TestCase(10, 4, false, 1, 2)]
    public void WhenGeneratingStarFrontiersStarSystem(int numStarsRoll, int planetRoll, bool expectedGasGiant, int expectedPlanetNum,
        int expectedStarNum)
    {
        _rollingServiceMock.Setup(x => x.D10(1))
            .Returns(numStarsRoll);
        _rollingServiceMock.Setup(x => x.D6(1))
            .Returns(planetRoll);

        var starSystem = _classUnderTest.GenerateStarFrontiersStarSystem(new Coordinates());

        Assert.That(starSystem.Planets.Count, Is.EqualTo(expectedPlanetNum));
        Assert.That(starSystem.CompanionStars.Count, Is.EqualTo(expectedStarNum));
        Assert.That(starSystem.GasGiant, Is.EqualTo(expectedGasGiant));
    }
}