using NUnit.Framework;
using SectorCreator.Global;
using SectorCreator.Models.RTTWorldgen;
using SectorCreator.Models.RTTWorldgen.Worlds;

namespace SectorCreator.Models.Tests.RttWorldgen.Worlds;

[TestFixture]
public class AcheronianWorldTests
{
    private readonly AcheronianWorld _classUnderTest = new(new RollingService(), new PlanetValidation());
    
    [Test]
    [Repeat(50)]
    public void WhenGenerating()
    {
        var acheronianPlanet = _classUnderTest.Generate(new RttWorldgenPlanet());
        
        Assert.That(acheronianPlanet.Size is >= 5 and <= 10);
        Assert.That(acheronianPlanet.Atmosphere, Is.EqualTo(1));
        Assert.That(acheronianPlanet.Hydrographics, Is.EqualTo(0));
        Assert.That(acheronianPlanet.Biosphere, Is.EqualTo(0));
    }
}