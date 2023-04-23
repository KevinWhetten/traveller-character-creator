using NUnit.Framework;
using SectorCreator.Global;
using SectorCreator.Global.Enums;
using SectorCreator.Models.Basic;
using SectorCreator.Models.Services.TradeCodeService;

namespace SectorCreator.Models.Tests.Services.TradeCodeServiceTests;

[TestFixture]
public class TradeCodeService_EconomicTests
{
    private readonly TradeCodeService _classUnderTest = new(new RollingService());
    
    [TestCase(0, 0, 0, false)]
    [TestCase(3, 0, 0, false)]
    [TestCase(4, 0, 0, false)]
    [TestCase(9, 0, 0, false)]
    [TestCase(10, 0, 0, false)]
    [TestCase(0, 3, 0, false)]
    [TestCase(0, 4, 0, false)]
    [TestCase(0, 8, 0, false)]
    [TestCase(0, 9, 0, false)]
    [TestCase(0, 0, 4, false)]
    [TestCase(0, 0, 8, false)]
    [TestCase(3, 5, 4, false)]
    [TestCase(4, 5, 4, true)]
    [TestCase(9, 5, 4, true)]
    [TestCase(10, 5, 4, false)]
    [TestCase(5, 3, 4, false)]
    [TestCase(5, 4, 4, true)]
    [TestCase(5, 8, 4, true)]
    [TestCase(5, 9, 4, false)]
    [TestCase(5, 5, 3, false)]
    [TestCase(5, 5, 4, true)]
    [TestCase(5, 5, 5, false)]
    [TestCase(5, 5, 7, false)]
    [TestCase(5, 5, 8, true)]
    [TestCase(5, 5, 9, false)]
    public void AddPreAgriculturalTradeCode(int atmosphere, int hydrographics, int population, bool expected)
    {
        var planet = new Planet {
            Atmosphere = atmosphere,
            Hydrographics = hydrographics,
            Population = population
        };
        _classUnderTest.AddPreAgriculturalTradeCode(planet);

        Assert.That(planet.TradeCodes.Contains(TradeCode.PreAgricultural), Is.EqualTo(expected));
    }

    [TestCase(3, 3, 4, false)]
    [TestCase(3, 3, 5, false)]
    [TestCase(3, 4, 4, false)]
    [TestCase(4, 3, 4, false)]
    [TestCase(3, 4, 5, false)]
    [TestCase(4, 3, 5, false)]
    [TestCase(4, 4, 4, false)]
    [TestCase(4, 4, 5, true)]
    [TestCase(9, 8, 7, true)]
    [TestCase(9, 8, 8, false)]
    [TestCase(9, 9, 7, false)]
    [TestCase(10, 8, 7, false)]
    [TestCase(9, 9, 8, false)]
    [TestCase(10, 8, 8, false)]
    [TestCase(10, 9, 7, false)]
    public void AddAgriculturalTradeCode(int atmosphere, int hydrosphere, int population, bool expected)
    {
        var planet = new Planet {
            Atmosphere = atmosphere,
            Hydrographics = hydrosphere,
            Population = population
        };

        _classUnderTest.AddAgriculturalTradeCode(planet);

        Assert.That(planet.TradeCodes.Contains(TradeCode.Agricultural), Is.EqualTo(expected));
    }

    [TestCase(0, 0, 0, false)]
    [TestCase(0, 0, 5, false)]
    [TestCase(0, 0, 6, true)]
    [TestCase(3, 3, 6, true)]
    [TestCase(4, 3, 6, false)]
    [TestCase(3, 4, 6, false)]
    [TestCase(4, 4, 6, false)]
    [TestCase(2, 2, 10, true)]
    public void AddNonAgriculturalTradeCode(int atmosphere, int hydrographics, int population, bool expected)
    {
        var planet = new Planet {
            Atmosphere = atmosphere,
            Hydrographics = hydrographics,
            Population = population
        };

        _classUnderTest.AddNonAgriculturalTradeCode(planet);

        Assert.That(planet.TradeCodes.Contains(TradeCode.NonAgricultural), Is.EqualTo(expected));
    }

    [TestCase(0, 7, true)]
    [TestCase(1, 7, true)]
    [TestCase(2, 7, true)]
    [TestCase(3, 7, false)]
    [TestCase(4, 7, true)]
    [TestCase(5, 7, false)]
    [TestCase(6, 7, false)]
    [TestCase(7, 7, true)]
    [TestCase(8, 7, false)]
    [TestCase(9, 7, true)]
    [TestCase(10, 7, false)]
    [TestCase(4, 8, true)]
    public void AddPreIndustrialTradeCode(int atmosphere, int population, bool expected)
    {
        var planet = new Planet {
            Atmosphere = atmosphere,
            Population = population
        };
        _classUnderTest.AddPreIndustrialTradeCode(planet);

        Assert.That(planet.TradeCodes.Contains(TradeCode.PreIndustrial), Is.EqualTo(expected));
    }

    [TestCase(0, 0, false)]
    [TestCase(1, 0, false)]
    [TestCase(2, 0, false)]
    [TestCase(4, 0, false)]
    [TestCase(7, 0, false)]
    [TestCase(9, 0, false)]
    [TestCase(0, 8, false)]
    [TestCase(1, 8, false)]
    [TestCase(2, 8, false)]
    [TestCase(4, 8, false)]
    [TestCase(7, 8, false)]
    [TestCase(9, 8, false)]
    [TestCase(0, 9, true)]
    [TestCase(1, 9, true)]
    [TestCase(2, 9, true)]
    [TestCase(4, 9, true)]
    [TestCase(7, 9, true)]
    [TestCase(9, 9, true)]
    [TestCase(3, 9, false)]
    [TestCase(5, 9, false)]
    [TestCase(6, 9, false)]
    [TestCase(8, 9, false)]
    [TestCase(10, 9, false)]
    [TestCase(11, 9, false)]
    public void AddIndustrialTradeCode(int atmosphere, int population, bool expected)
    {
        var planet = new Planet {
            Atmosphere = atmosphere,
            Population = population
        };

        _classUnderTest.AddIndustrialTradeCode(planet);

        Assert.That(planet.TradeCodes.Contains(TradeCode.Industrial), Is.EqualTo(expected));
    }

    [TestCase(0, 0, false)]
    [TestCase(1, 0, false)]
    [TestCase(2, 0, true)]
    [TestCase(5, 0, true)]
    [TestCase(6, 0, false)]
    [TestCase(3, 3, true)]
    [TestCase(3, 4, false)]
    public void AddPoorTradeCode(int atmosphere, int hydrographics, bool expected)
    {
        var planet = new Planet {
            Atmosphere = atmosphere,
            Hydrographics = hydrographics
        };

        _classUnderTest.AddPoorTradeCode(planet);

        Assert.That(planet.TradeCodes.Contains(TradeCode.Poor), Is.EqualTo(expected));
    }

    [TestCase(0, 0, false)]
    [TestCase(5, 5, false)]
    [TestCase(6, 4, false)]
    [TestCase(6, 5, true)]
    [TestCase(6, 6, false)]
    [TestCase(7, 5, false)]
    public void AddPreRichTradeCode(int atmosphere, int population, bool expected)
    {
        var planet = new Planet {
            Atmosphere = atmosphere,
            Population = population
        };
        _classUnderTest.AddPreRichTradeCode(planet);
        Assert.That(planet.TradeCodes.Contains(TradeCode.PreRich), Is.EqualTo(expected));
    }

    [TestCase(0, 0, false)]
    [TestCase(6, 0, false)]
    [TestCase(7, 0, false)]
    [TestCase(8, 0, false)]
    [TestCase(6, 6, true)]
    [TestCase(6, 7, true)]
    [TestCase(6, 8, true)]
    [TestCase(7, 6, false)]
    [TestCase(7, 7, false)]
    [TestCase(7, 8, false)]
    [TestCase(8, 6, true)]
    [TestCase(8, 7, true)]
    [TestCase(8, 8, true)]
    [TestCase(8, 9, false)]
    public void AddRichTradeCode(int atmosphere, int population, bool expected)
    {
        var planet = new Planet {
            Atmosphere = atmosphere,
            Population = population
        };

        _classUnderTest.AddRichTradeCode(planet);

        Assert.That(planet.TradeCodes.Contains(TradeCode.Rich), Is.EqualTo(expected));
    }
}