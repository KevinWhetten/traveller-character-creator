using Moq;
using NUnit.Framework;
using SectorCreator.Global;
using SectorCreator.Global.Enums;
using SectorCreator.Models.Basic;
using SectorCreator.Models.Factories;
using SectorCreator.Models.RTTWorldgen;

namespace SectorCreator.Models.Tests.Factories;

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
        _rollingServiceMock.Setup(x => x.D6(It.IsAny<int>())).Returns(1);
        _planetFactoryMock.Setup(x => x.Generate(It.IsAny<SectorType>()))
            .Returns(new Planet());
        _starFrontiersStarFactoryMock.Setup(x => x.Generate()).Returns(new Star());
        _rttWorldgenStarFactoryMock.Setup(x => x.Generate(It.IsAny<StarType>(), It.IsAny<RttWorldgenStar>()))
            .Returns(new RttWorldgenStar());
        _starFrontiersPlanetFactoryMock.Setup(x => x.Generate(It.IsAny<SectorType>())).Returns(new Planet());
        _rttWorldgenPlanetFactoryMock.Setup(x => x.Generate(It.IsAny<SectorType>()))
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

        var starSystem = _classUnderTest.GenerateMongooseStarSystem(SectorType.Basic);

        Assert.That(starSystem.Planets.Count, Is.EqualTo(1));
        Assert.That(starSystem.Stars.Count, Is.EqualTo(0));
        Assert.That(starSystem.GasGiant, Is.EqualTo(expectedGasGiant));
    }

    [Test]
    public void WhenGeneratingT5StarSystem()
    {
        throw new InconclusiveException("Not Implemented");
    }

    [TestCase(0, false)]
    public void WhenGeneratingStarFrontiersStarSystem(int numStarsRoll, int planetRoll, bool expectedGasGiant)
    {
        _rollingServiceMock.Setup(x => x.D10(1))
            .Returns(numStarsRoll);
        _rollingServiceMock.Setup(x => x.D6(1))
            .Returns(planetRoll);

        var starSystem = _classUnderTest.GenerateStarFrontiersStarSystem();

        Assert.That(starSystem.Planets.Count, Is.EqualTo(1));
        Assert.That(starSystem.Stars.Count, Is.EqualTo(0));
        Assert.That(starSystem.GasGiant, Is.EqualTo(expectedGasGiant));
    }
}