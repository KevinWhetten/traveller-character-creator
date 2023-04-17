using Moq;
using NUnit.Framework;
using SectorCreator.Global;
using SectorCreator.Global.Enums;
using SectorCreator.Models.Basic;
using SectorCreator.Models.Factories;

namespace SectorCreator.Models.Tests.Factories;

public class SectorFactoryTests
{
    private SectorFactory _classUnderTest;
    private readonly Mock<ISubsectorFactory> _subsectorFactoryMock = new();

    [SetUp]
    public void SetUp()
    {
        _subsectorFactoryMock.Setup(x => x.GenerateMongooseSubsector(It.IsAny<SectorType>(), It.IsAny<Coordinates>()))
            .Returns(new Subsector());
        _subsectorFactoryMock.Setup(x => x.GenerateT5Subsector(It.IsAny<Coordinates>()))
            .Returns(new Subsector());
        _subsectorFactoryMock.Setup(x => x.GenerateRttWorldgenSubsector(It.IsAny<Coordinates>()))
            .Returns(new Subsector());
        _subsectorFactoryMock.Setup(x => x.GenerateStarFrontiersSubsector(It.IsAny<Coordinates>()))
            .Returns(new Subsector());
        _classUnderTest = new SectorFactory(_subsectorFactoryMock.Object);
    }

    [Test]
    public void WhenGeneratingMongooseSector()
    {
        var sector = _classUnderTest.GenerateMongooseSector(SectorType.Basic);

        Assert.That(sector.Subsectors.Count, Is.EqualTo(16));
    }

    [Test]
    public void WhenGeneratingT5SectorSector()
    {
        var sector = _classUnderTest.GenerateT5Sector();

        Assert.That(sector.Subsectors.Count, Is.EqualTo(16));
    }

    [Test]
    public void WhenGeneratingRttWorldgenSectorSector()
    {
        var sector = _classUnderTest.GenerateRttWorldgenSector();

        Assert.That(sector.Subsectors.Count, Is.EqualTo(16));
    }

    [Test]
    public void WhenGeneratingStarFrontiersSectorSector()
    {
        var sector = _classUnderTest.GenerateStarFrontiersSector();

        Assert.That(sector.Subsectors.Count, Is.EqualTo(16));
    }
}