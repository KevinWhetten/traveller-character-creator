using NUnit.Framework;
using SectorCreator.Global.Enums;
using SectorCreator.Models.Mongoose;

namespace SectorCreator.Models.Tests.Mongoose;

[TestFixture]
public class MongooseSector_Tests
{
    [TestCase(SectorType.Basic)]
    [TestCase(SectorType.SpaceOpera)]
    [TestCase(SectorType.HardScience)]
    public void WhenConstructing(SectorType sectorType)
    {
        var sector = new MongooseSector(sectorType);
        
        Assert.That(sector.Subsectors.Count, Is.EqualTo(16));
        Assert.That(sector.SectorType, Is.EqualTo(sectorType));
    }
}