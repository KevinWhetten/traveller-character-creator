using NUnit.Framework;
using SectorCreator.Global;
using SectorCreator.Global.Enums;
using SectorCreator.Models.Basic;
using SectorCreator.Models.Services.TradeCodeService;

namespace SectorCreator.Models.Tests.Services.TradeCodeServiceTests;

[TestFixture]
public class TradeCodeServiceTests
{
    private readonly TradeCodeService _classUnderTest = new(new RollingService());

    [TestCase(0, false)]
    [TestCase(11, false)]
    [TestCase(12, true)]
    [TestCase(15, true)]
    public void AddHighTechnologyTradeCode(int techLevel, bool expected)
    {
        var planet = new Planet {
            TechLevel = techLevel
        };

        _classUnderTest.AddHighTechnologyTradeCode(planet);

        Assert.That(planet.TradeCodes.Contains(TradeCode.HighTechnology), Is.EqualTo(expected));
    }

    [TestCase(0, true)]
    [TestCase(1, true)]
    [TestCase(3, true)]
    [TestCase(5, true)]
    [TestCase(6, false)]
    [TestCase(8, false)]
    [TestCase(10, false)]
    public void AddLowTechnologyTradeCode(int techLevel, bool expected)
    {
        var planet = new Planet {
            TechLevel = techLevel
        };

        _classUnderTest.AddLowTechnologyTradeCode(planet);

        Assert.That(planet.TradeCodes.Contains(TradeCode.LowTechnology), Is.EqualTo(expected));
    }

    [Test]
    public void GetTradeCodes()
    {
        // TODO: Finish
        _classUnderTest.GetTradeCodes(new Planet());
    }
}