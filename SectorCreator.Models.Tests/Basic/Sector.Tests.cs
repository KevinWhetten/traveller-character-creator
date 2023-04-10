using NUnit.Framework;
using SectorCreator.Global.Enums;
using SectorCreator.Models.Basic;

namespace SectorCreator.Models.Tests.Basic;

[TestFixture]
public class SectorTests
{
    [Test]
    public void WhenConstructing()
    {
        var result = new Sector();
        
        Assert.That(result.Subsectors.Count, Is.EqualTo(0));
        Assert.That(result.SectorType, Is.EqualTo(SectorType.Basic));
    }
}