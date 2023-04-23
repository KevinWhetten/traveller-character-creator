using NUnit.Framework;
using SectorCreator.Global;
using SectorCreator.Global.Enums;
using SectorCreator.Models.Basic;
using SectorCreator.Models.Services.TradeCodeService;

namespace SectorCreator.Models.Tests.Services.TradeCodeServiceTests;

[TestFixture]
public class TradeCodeService_PopulationTests
{
    private readonly TradeCodeService _classUnderTest = new(new RollingService());

    [TestCase(0, 0, 0, 0, false)]
    [TestCase(1, 0, 0, 0, false)]
    [TestCase(0, 1, 0, 0, false)]
    [TestCase(0, 0, 1, 0, false)]
    [TestCase(0, 0, 0, 1, true)]
    public void AddDiebackTradeCode(int population, int government, int lawLevel, int techLevel, bool expected)
    {
        var planet = new Planet {
            Population = population,
            Government = government,
            LawLevel = lawLevel,
            TechLevel = techLevel
        };

        _classUnderTest.AddDiebackTradeCode(planet);

        Assert.That(planet.TradeCodes.Contains(TradeCode.Dieback), Is.EqualTo(expected));
    }

    [TestCase(0, 0, 0, true)]
    [TestCase(1, 0, 0, false)]
    [TestCase(0, 1, 0, false)]
    [TestCase(0, 0, 1, false)]
    [TestCase(0, 1, 1, false)]
    [TestCase(1, 0, 1, false)]
    [TestCase(1, 1, 0, false)]
    [TestCase(1, 1, 1, false)]
    public void AddBarrenTradeCode(int population, int government, int lawLevel, bool expected)
    {
        var planet = new Planet {
            Population = population,
            Government = government,
            LawLevel = lawLevel
        };

        _classUnderTest.AddBarrenTradeCode(planet);

        Assert.That(planet.TradeCodes.Contains(TradeCode.Barren), Is.EqualTo(expected));
    }

    [TestCase(0, false)]
    [TestCase(1, true)]
    [TestCase(3, true)]
    [TestCase(4, false)]
    [TestCase(7, false)]
    [TestCase(10, false)]
    public void AddLowPopulationTradeCode(int population, bool expected)
    {
        var planet = new Planet {
            Population = population
        };

        _classUnderTest.AddLowPopulationTradeCode(planet);

        Assert.That(planet.TradeCodes.Contains(TradeCode.LowPopulation), Is.EqualTo(expected));
    }

    [TestCase(0, false)]
    [TestCase(1, false)]
    [TestCase(3, false)]
    [TestCase(4, true)]
    [TestCase(6, true)]
    [TestCase(7, false)]
    [TestCase(10, false)]
    public void AddNonIndustrialTradeCode(int population, bool expected)
    {
        var planet = new Planet {
            Population = population
        };

        _classUnderTest.AddNonIndustrialTradeCode(planet);

        Assert.That(planet.TradeCodes.Contains(TradeCode.NonIndustrial), Is.EqualTo(expected));
    }

    [TestCase(0, false)]
    [TestCase(7, false)]
    [TestCase(8, true)]
    [TestCase(9, false)]
    [TestCase(12, false)]
    public void AddPreHighPopulationTradeCode(int population, bool expected)
    {
        var planet = new Planet {
            Population = population
        };

        _classUnderTest.AddPreHighPopulationTradeCode(planet);
        
        Assert.That(planet.TradeCodes.Contains(TradeCode.PreHighPopulation), Is.EqualTo(expected));
    }

    [TestCase(0, false)]
    [TestCase(8, false)]
    [TestCase(9, true)]
    [TestCase(10, true)]
    public void AddHighPopulationTradeCode(int population, bool expected)
    {
        var planet = new Planet {
            Population = population
        };

        _classUnderTest.AddHighPopulationTradeCode(planet);

        Assert.That(planet.TradeCodes.Contains(TradeCode.HighPopulation), Is.EqualTo(expected));
    }
}