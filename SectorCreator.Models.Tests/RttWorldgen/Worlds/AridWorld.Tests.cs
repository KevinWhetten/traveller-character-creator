using NUnit.Framework;
using SectorCreator.Global;
using SectorCreator.Global.Enums;
using SectorCreator.Models.RTTWorldgen;
using SectorCreator.Models.RTTWorldgen.Worlds;

namespace SectorCreator.Models.Tests.RttWorldgen.Worlds;

[TestFixture]
public class AridWorldTests
{
    private readonly AridWorld _classUnderTest = new(new RollingService(), new PlanetValidation());
  
    [TestCase()]
    [Repeat(50)]
    public void WhenGenerating()
    {
        var aridWorld = _classUnderTest.Generate(new RttWorldgenPlanet(), new RttWorldgenStar());
        
        Assert.True(aridWorld.Size is >= 5 and <= 10);
        Assert.True(aridWorld.Chemistry is PlanetChemistry.Water or PlanetChemistry.Ammonia or PlanetChemistry.Methane);
        Assert.True(aridWorld.Biosphere is >= 0 and <= 12);
        Assert.True(aridWorld.Atmosphere is >= 0 and <= 10);
        Assert.True(aridWorld.Hydrographics is >= 1 and <= 3);
    }
}