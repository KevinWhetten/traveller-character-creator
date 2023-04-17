using NUnit.Framework;
using SectorCreator.Global;
using SectorCreator.Global.Enums;
using SectorCreator.Models.RTTWorldgen;
using SectorCreator.Models.Services.TradeCodeService;

namespace SectorCreator.Models.Tests.Services.TradeCodeServiceTests;

public class TradeCodeService_PoliticalTests
{
    private readonly TradeCodeService _classUnderTest = new(new RollingService());

    [Test]
    public void AddSubsectorCapitalTradeCode()
    {
        throw new InconclusiveException("Not Implemented");
    }

    [Test]
    public void AddSectorCapitalTradeCode()
    {
        throw new InconclusiveException("Not Implemented");
    }

    [TestCase(1, false)]
    [TestCase(11, false)]
    [TestCase(12, true)]
    [TestCase(15, true)]
    public void AddCapitalTradeCode(int biosphere, bool expected)
    {
        var planet = new RttWorldgenPlanet {
            Biosphere = biosphere
        };
        _classUnderTest.AddCapitalTradeCode(planet);
        Assert.That(planet.TradeCodes.Contains(TradeCode.Capital), Is.EqualTo(expected));
    }

    [Test]
    public void AddColonyTradeCode()
    {
        throw new InconclusiveException("Not Implemented");
    }
}