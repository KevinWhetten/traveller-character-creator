using NUnit.Framework;
using SectorCreator.Global.Enums;
using SectorCreator.Models.Basic;

namespace SectorCreator.Models.Tests.Basic;

[TestFixture]
public class StarTests
{
    [Test]
    public void WhenConstructing()
    {
        var result = new Star();
        
        Assert.That(result.SpectralType, Is.EqualTo(SpectralType.O));
        Assert.That(result.SpectralSubclass, Is.EqualTo(0));
    }
}