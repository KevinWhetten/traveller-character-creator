using System.Collections.Generic;
using NUnit.Framework;
using SectorCreator.Global;
using SectorCreator.Global.Enums;
using SectorCreator.Models.RTTWorldgen;
using SectorCreator.Models.RTTWorldgen.Worlds;

namespace SectorCreator.Models.Tests.RttWorldgen.Worlds;

[TestFixture]
public class AreanWorldTests
{
    private readonly AreanWorld _classUnderTest = new(new RollingService(), new PlanetValidation());

    [Test]
    [Repeat(50)]
    public void WhenGenerating()
    {
        var areanPlanet = _classUnderTest.Generate(new RttWorldgenPlanet(), new RttWorldgenStar());

        Assert.True(areanPlanet.Size is >= 0 and <= 5);
        Assert.True(areanPlanet.Atmosphere is >= 0 and <= 6 or 10);
        Assert.True(areanPlanet.Hydrographics is >= 0 and <= 4);
        Assert.True(areanPlanet.Chemistry is PlanetChemistry.Water or PlanetChemistry.Ammonia or PlanetChemistry.Methane
            or PlanetChemistry.None);
        Assert.True(areanPlanet.Biosphere is >= 0 and <= 10);
    }
}