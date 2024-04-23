using Moq;
using NUnit.Framework;
using SectorCreator.Global;
using SectorCreator.Global.Enums;
using SectorCreator.Models.Basic.Factories;

namespace SectorCreator.Models.Tests.Factories.Basic;

[TestFixture]
public class SubsectorFactoryTests
{
    private SubsectorFactory _classUnderTest;
    private readonly Mock<IHexFactory> _hexFactoryMock = new();

    [SetUp]
    public void SetUp()
    {
        _classUnderTest = new SubsectorFactory(_hexFactoryMock.Object);
    }

    [TestCase(1, 1, 1, 1)]
    [TestCase(4, 1, 4, 1)]
    [TestCase(1, 3, 1, 3)]
    [TestCase(4, 3, 4, 3)]
    public void WhenGeneratingMongooseSubsector(int xCoordinate, int yCoordinate, int expectedX, int expectedY)
    {
        var subsector =
            _classUnderTest.GenerateMongooseSubsector(SectorType.Basic, new Coordinates(xCoordinate, yCoordinate));

        Assert.That(subsector.Coordinates.X, Is.EqualTo(expectedX));
        Assert.That(subsector.Coordinates.Y, Is.EqualTo(expectedY));
        Assert.That(subsector.Hexes.Count, Is.EqualTo(80));
    }

    [TestCase(1, 1, 1, 1)]
    [TestCase(4, 1, 4, 1)]
    [TestCase(1, 3, 1, 3)]
    [TestCase(4, 3, 4, 3)]
    public void WhenGeneratingT5Subsector(int xCoordinate, int yCoordinate, int expectedX, int expectedY)
    {
        var subsector =
            _classUnderTest.GenerateT5Subsector(new Coordinates(xCoordinate, yCoordinate));

        Assert.That(subsector.Coordinates.X, Is.EqualTo(expectedX));
        Assert.That(subsector.Coordinates.Y, Is.EqualTo(expectedY));
        Assert.That(subsector.Hexes.Count, Is.EqualTo(80));
    }

    [TestCase(1, 1, 1, 1)]
    [TestCase(4, 1, 4, 1)]
    [TestCase(1, 3, 1, 3)]
    [TestCase(4, 3, 4, 3)]
    public void WhenGeneratingRttWorldgenSubsector(int xCoordinate, int yCoordinate, int expectedX, int expectedY)
    {
        var subsector =
            _classUnderTest.GenerateRttWorldgenSubsector(new Coordinates(xCoordinate, yCoordinate));

        Assert.That(subsector.Coordinates.X, Is.EqualTo(expectedX));
        Assert.That(subsector.Coordinates.Y, Is.EqualTo(expectedY));
        Assert.That(subsector.Hexes.Count, Is.EqualTo(80));
    }

    [TestCase(1, 1, 1, 1)]
    [TestCase(4, 1, 4, 1)]
    [TestCase(1, 3, 1, 3)]
    [TestCase(4, 3, 4, 3)]
    public void WhenGeneratingStarFrontiersSubsector(int xCoordinate, int yCoordinate, int expectedX, int expectedY)
    {
        var subsector =
            _classUnderTest.GenerateStarFrontiersSubsector(new Coordinates(xCoordinate, yCoordinate));

        Assert.That(subsector.Coordinates.X, Is.EqualTo(expectedX));
        Assert.That(subsector.Coordinates.Y, Is.EqualTo(expectedY));
        Assert.That(subsector.Hexes.Count, Is.EqualTo(80));
    }
}