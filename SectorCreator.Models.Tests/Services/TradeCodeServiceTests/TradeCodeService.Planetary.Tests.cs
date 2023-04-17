using NUnit.Framework;
using SectorCreator.Global;
using SectorCreator.Global.Enums;
using SectorCreator.Models.Basic;
using SectorCreator.Models.Services.TradeCodeService;

namespace SectorCreator.Models.Tests.Services.TradeCodeServiceTests;

public class TradeCodeService_PlanetaryTests
{
    private TradeCodeService _classUnderTest = new TradeCodeService(new RollingService());

    [TestCase(0, 0, 0, true)]
    [TestCase(1, 0, 0, false)]
    [TestCase(0, 1, 0, false)]
    [TestCase(0, 0, 1, false)]
    [TestCase(0, 1, 1, false)]
    [TestCase(1, 0, 1, false)]
    [TestCase(1, 1, 0, false)]
    [TestCase(1, 1, 1, false)]
    public void AddAsteroidTradeCode(int size, int atmosphere, int hydrographics, bool expected)
    {
        var planet = new Planet {
            Size = size,
            Atmosphere = atmosphere,
            Hydrographics = hydrographics
        };

        _classUnderTest.AddAsteroidTradeCode(planet);

        Assert.That(planet.TradeCodes.Contains(TradeCode.Asteroid), Is.EqualTo(expected));
    }

    [TestCase(0, 0, false)]
    [TestCase(1, 0, false)]
    [TestCase(2, 0, true)]
    [TestCase(3, 0, true)]
    [TestCase(5, 0, true)]
    [TestCase(8, 0, true)]
    [TestCase(15, 0, true)]
    [TestCase(2, 1, false)]
    [TestCase(3, 1, false)]
    [TestCase(5, 1, false)]
    [TestCase(8, 1, false)]
    [TestCase(15, 1, false)]
    public void AddDesertTradeCode(int atmosphere, int hydrographics, bool expected)
    {
        var planet = new Planet {
            Atmosphere = atmosphere,
            Hydrographics = hydrographics
        };

        _classUnderTest.AddDesertTradeCode(planet);

        Assert.That(planet.TradeCodes.Contains(TradeCode.Desert), Is.EqualTo(expected));
    }

    [TestCase(0, 0, false)]
    [TestCase(10, 0, false)]
    [TestCase(10, 1, true)]
    [TestCase(0, 1, false)]
    [TestCase(9, 1, false)]
    [TestCase(9, 4, false)]
    public void AddFluidOceansTradeCode(int atmosphere, int hydrographics, bool expected)
    {
        var planet = new Planet {
            Atmosphere = atmosphere,
            Hydrographics = hydrographics
        };

        _classUnderTest.AddFluidOceansTradeCode(planet);

        Assert.That(planet.TradeCodes.Contains(TradeCode.FluidOceans), Is.EqualTo(expected));
    }

    [TestCase(0, 0, 0, false)]
    [TestCase(5, 0, 0, false)]
    [TestCase(0, 4, 0, false)]
    [TestCase(0, 0, 4, false)]
    [TestCase(5, 4, 0, false)]
    [TestCase(5, 0, 4, false)]
    [TestCase(0, 4, 4, false)]
    [TestCase(5, 4, 4, true)]
    [TestCase(10, 9, 8, true)]
    [TestCase(10, 10, 8, false)]
    [TestCase(10, 9, 9, false)]
    [TestCase(10, 10, 9, false)]
    [TestCase(10, 10, 10, false)]
    public void AddGardenCode(int atmosphere, int hydrographics, int population, bool expected)
    {
        var planet = new Planet {
            Atmosphere = atmosphere,
            Hydrographics = hydrographics,
            Population = population
        };

        _classUnderTest.AddGardenTradeCode(planet);

        Assert.That(planet.TradeCodes.Contains(TradeCode.Garden), Is.EqualTo(expected));
    }

    [TestCase(0, 0, 0, false)]
    [TestCase(3, 0, 0, false)]
    [TestCase(4, 2, 0, true)]
    [TestCase(5, 4, 1, true)]
    [TestCase(6, 7, 2, true)]
    [TestCase(7, 8, 1, false)]
    [TestCase(8, 9, 1, true)]
    [TestCase(9, 12, 0, true)]
    [TestCase(10, 13, 1, false)]
    public void AddHellworldCode(int size, int atmosphere, int hydrographics, bool expected)
    {
        var planet = new Planet {
            Size = size,
            Atmosphere = atmosphere,
            Hydrographics = hydrographics
        };

        _classUnderTest.AddHellworldTradeCode(planet);

        Assert.That(planet.TradeCodes.Contains(TradeCode.Hellworld), Is.EqualTo(expected));
    }

    [TestCase(0, 0, false)]
    [TestCase(0, 1, true)]
    [TestCase(0, 2, true)]
    [TestCase(0, 5, true)]
    [TestCase(0, 10, true)]
    [TestCase(1, 1, true)]
    [TestCase(1, 2, true)]
    [TestCase(1, 5, true)]
    [TestCase(1, 10, true)]
    [TestCase(2, 1, false)]
    [TestCase(2, 2, false)]
    [TestCase(2, 5, false)]
    [TestCase(2, 10, false)]
    public void AddIceCappedTradeCode(int atmosphere, int hydrographics, bool expected)
    {
        var planet = new Planet {
            Atmosphere = atmosphere,
            Hydrographics = hydrographics
        };

        _classUnderTest.AddIceCappedTradeCode(planet);

        Assert.That(planet.TradeCodes.Contains(TradeCode.IceCapped), Is.EqualTo(expected));
    }

    [TestCase(0, 0, false)]
    [TestCase(0, 10, true)]
    [TestCase(1, 11, true)]
    [TestCase(2, 10, false)]
    public void AddOceanWorldCode(int atmosphere, int hydrographics, bool expected)
    {
        var planet = new Planet {
            Atmosphere = atmosphere,
            Hydrographics = hydrographics
        };

        _classUnderTest.AddOceanWorldTradeCode(planet);

        Assert.That(planet.TradeCodes.Contains(TradeCode.OceanWorld), Is.EqualTo(expected));
    }

    [TestCase(0, true)]
    [TestCase(1, false)]
    [TestCase(3, false)]
    [TestCase(4, false)]
    [TestCase(7, false)]
    [TestCase(10, false)]
    public void AddVacuumTradeCode(int atmosphere, bool expected)
    {
        var planet = new Planet {
            Atmosphere = atmosphere
        };

        _classUnderTest.AddVacuumTradeCode(planet);

        Assert.That(planet.TradeCodes.Contains(TradeCode.Vacuum), Is.EqualTo(expected));
    }

    [TestCase(0, false)]
    [TestCase(1, false)]
    [TestCase(3, false)]
    [TestCase(4, false)]
    [TestCase(7, false)]
    [TestCase(10, true)]
    public void AddWaterWorldTradeCode(int hydrographics, bool expected)
    {
        var planet = new Planet {
            Hydrographics = hydrographics
        };

        _classUnderTest.AddWaterWorldTradeCode(planet);

        Assert.That(planet.TradeCodes.Contains(TradeCode.WaterWorld), Is.EqualTo(expected));
    }
}