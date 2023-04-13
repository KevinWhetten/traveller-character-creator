using NUnit.Framework;
using SectorCreator.Global;
using SectorCreator.Models.RTTWorldgen;
using SectorCreator.Models.RTTWorldgen.Worlds;

namespace SectorCreator.Models.Tests.RttWorldgen.Worlds;

[TestFixture]
public class AsphodelianWorldTests
{
    private readonly AsphodelianWorld _classUnderTest = new(new RollingService(), new PlanetValidation());

    [Test]
    [Repeat(50)]
    public void WhenGenerating()
    {
        var asphodelianWorld = _classUnderTest.Generate(new RttWorldgenPlanet());
        
        Assert.That(asphodelianWorld.Size is >= 10 and <= 15);
        Assert.That(asphodelianWorld.Atmosphere, Is.EqualTo(1));
        Assert.That(asphodelianWorld.Hydrographics, Is.EqualTo(0));
        Assert.That(asphodelianWorld.Biosphere, Is.EqualTo(0));
    }
}