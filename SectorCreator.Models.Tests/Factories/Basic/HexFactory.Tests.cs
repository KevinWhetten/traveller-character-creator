using Moq;
using NUnit.Framework;
using SectorCreator.Global;
using SectorCreator.Global.Enums;
using SectorCreator.Models.Basic;
using SectorCreator.Models.Factories.Basic;
using SectorCreator.Models.Factories.RttWorldgen;
using SectorCreator.Models.RTTWorldgen;

namespace SectorCreator.Models.Tests.Factories.Basic;

[TestFixture]
public class HexFactoryTests
{
    private HexFactory _classUnderTest;
    private readonly Mock<IStarSystemFactory> _starSystemFactoryMock = new();
    private readonly Mock<IRollingService> _rollingServiceMock = new();
    private readonly Mock<IRttWorldgenStarSystemFactory> _rttWorldgenStarSystemFactoryMock = new();

    [SetUp]
    public void SetUp()
    {
        _starSystemFactoryMock.Setup(x => x.GenerateMongooseStarSystem(It.IsAny<SectorType>()))
            .Returns(new StarSystem());
        _starSystemFactoryMock.Setup(x => x.GenerateT5StarSystem()).Returns(new StarSystem());

        _classUnderTest = new HexFactory(_rollingServiceMock.Object, _starSystemFactoryMock.Object, _rttWorldgenStarSystemFactoryMock.Object);
    }

    [TestCase(false)]
    [TestCase(true)]
    public void WhenGeneratingMongooseHex(bool starSystem)
    {
        _rollingServiceMock.Setup(x => x.D6(1)).Returns(starSystem ? 6 : 0);

        var hex = _classUnderTest.GenerateMongooseHex(new Coordinates(1, 1),
            new Coordinates(1, 1), SectorType.Basic);

        Assert.That(hex.StarSystems.Count, Is.EqualTo(starSystem ? 1 : 0));
    }

    [Test]
    public void WhenGeneratingT5Hex()
    {
        throw new InconclusiveException("Not Implemented");
    }

    [TestCase(false, false, CompanionOrbit.Close, 0)]
    [TestCase(true, false, CompanionOrbit.Close, 1)]
    [TestCase(false, true, CompanionOrbit.Close, 1)]
    [TestCase(true, true, CompanionOrbit.Close, 2)]
    [TestCase(false, true, CompanionOrbit.Distant, 2)]
    [TestCase(true, true, CompanionOrbit.Distant, 3)]
    public void WhenGeneratingRttWorldgenHex(bool brownDwarfStarSystem, bool starSystem, CompanionOrbit companionOrbit,
        int expectedStarSystems)
    {
        var returnedStarSystem = new RttWorldgenStarSystem() {Stars = {new RttWorldgenStar {CompanionOrbit = companionOrbit}}};
        _rttWorldgenStarSystemFactoryMock.Setup(x => x.Generate(It.IsAny<StarSystemType>()))
            .Returns(returnedStarSystem);
        _rttWorldgenStarSystemFactoryMock.Setup(x => x.Generate(It.IsAny<RttWorldgenStar>()))
            .Returns(new RttWorldgenStarSystem());

        _rollingServiceMock.SetupSequence(x => x.D6(1))
            .Returns(brownDwarfStarSystem ? 6 : 0)
            .Returns(starSystem ? 6 : 0);

        var hex = _classUnderTest.GenerateRttWorldgenHex(new Coordinates(1, 1),
            new Coordinates(1, 1));

        Assert.That(hex.StarSystems.Count, Is.EqualTo(expectedStarSystems));
    }

    [TestCase(false)]
    [TestCase(true)]
    public void WhenGeneratingStarFrontiersHex(bool starSystem)
    {
        _rollingServiceMock.Setup(x => x.D10(1)).Returns(starSystem ? 10 : 0);

        var hex = _classUnderTest.GenerateStarFrontiersHex(new Coordinates(1, 1), new Coordinates(1, 1));

        Assert.That(hex.StarSystems.Count, Is.EqualTo(starSystem ? 1 : 0));
    }

    [TestCase(1, 1, 1, 1, 1, 1)]
    [TestCase(1, 1, 5, 6, 5, 6)]
    [TestCase(2, 3, 1, 1, 9, 21)]
    [TestCase(2, 3, 5, 6, 13, 26)]
    public void WhenSettingCoordinates(int subsectorCoordinateX, int subsectorCoordinateY, int hexCoordinateX,
        int hexCoordinateY, int expectedCoordinateX, int expectedCoordinateY)
    {
        var hex = _classUnderTest.GenerateMongooseHex(new Coordinates(subsectorCoordinateX, subsectorCoordinateY),
            new Coordinates(hexCoordinateX, hexCoordinateY), SectorType.Basic);

        Assert.That(hex.Coordinates.X, Is.EqualTo(expectedCoordinateX));
        Assert.That(hex.Coordinates.Y, Is.EqualTo(expectedCoordinateY));
    }
}