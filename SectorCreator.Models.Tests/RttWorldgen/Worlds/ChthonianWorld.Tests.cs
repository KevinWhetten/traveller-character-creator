using NUnit.Framework;
using SectorCreator.Models.RTTWorldgen;
using SectorCreator.Models.RTTWorldgen.Worlds;

namespace SectorCreator.Models.Tests.RttWorldgen.Worlds;

[TestFixture]
public class ChthonianWorldTests
{
    private readonly ChthonianWorld _classUnderTest = new();

    [Test]
    public void WhenGenerating()
    {
        var chthonianWorld = _classUnderTest.Generate(new RttWorldgenPlanet());

        Assert.That(chthonianWorld.Size, Is.EqualTo(16));
        Assert.That(chthonianWorld.Atmosphere, Is.EqualTo(1));
        Assert.That(chthonianWorld.Hydrographics, Is.EqualTo(0));
        Assert.That(chthonianWorld.Biosphere, Is.EqualTo(0));
    }
}