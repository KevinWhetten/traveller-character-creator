using System;
using NUnit.Framework;
using SectorCreator.Global;
using SectorCreator.Global.Enums;
using SectorCreator.Models.Basic;
using SectorCreator.Models.RTTWorldgen;
using SectorCreator.Models.Services.TradeCodeService;

namespace SectorCreator.Models.Tests.Services.TradeCodeServiceTests;

public class TradeCodeService_ClimateTests
{
    private TradeCodeService _classUnderTest = new TradeCodeService(new RollingService());
    
    [TestCase(1, true)]
    [TestCase(2, true)]
    [TestCase(3, false)]
    [TestCase(7, false)]
    [TestCase(10, false)]
    [TestCase(12, false)]
    public void AddFrozenTradeCode(int temperature, bool expected)
    {
        var planet = new Planet {Temperature = temperature};
        _classUnderTest.AddFrozenTradeCode(planet);
        Assert.That(planet.TradeCodes.Contains(TradeCode.Frozen), Is.EqualTo(expected));
    }

    [TestCase(5, 3, PlanetOrbit.Outer, true)]
    [TestCase(1, 3, PlanetOrbit.Outer, false)]
    [TestCase(2, 3, PlanetOrbit.Outer, true)]
    [TestCase(9, 3, PlanetOrbit.Outer, true)]
    [TestCase(10, 3, PlanetOrbit.Outer, false)]
    [TestCase(5, 0, PlanetOrbit.Outer, false)]
    [TestCase(5, 1, PlanetOrbit.Outer, true)]
    [TestCase(5, 7, PlanetOrbit.Outer, true)]
    [TestCase(5, 3, PlanetOrbit.Inner, false)]
    [TestCase(5, 3, PlanetOrbit.Epistellar, false)]
    public void AddFrozenTradeCode(int size, int hydrographics, PlanetOrbit planetOrbit, bool expected)
    {
        var planet = new RttWorldgenPlanet {
            Size = size,
            Hydrographics = hydrographics,
            PlanetOrbit = planetOrbit
        };
        _classUnderTest.AddFrozenTradeCode(planet);
        Assert.That(planet.TradeCodes.Contains(TradeCode.Frozen), Is.EqualTo(expected));
    }

    [TestCase(1, false)]
    [TestCase(9, false)]
    [TestCase(10, true)]
    [TestCase(12, true)]
    public void AddHotTradeCode(int temperature, bool expected)
    {
        var planet = new Planet {Temperature = temperature};
        _classUnderTest.AddHotTradeCode(planet);
        Assert.That(planet.TradeCodes.Contains(TradeCode.Hot), Is.EqualTo(expected));
    }

    [TestCase(1, PlanetOrbit.Epistellar, false)]
    [TestCase(2, PlanetOrbit.Epistellar, true)]
    [TestCase(9, PlanetOrbit.Epistellar, true)]
    [TestCase(10, PlanetOrbit.Epistellar, false)]
    [TestCase(5, PlanetOrbit.Inner, false)]
    [TestCase(5, PlanetOrbit.Outer, false)]
    public void AddHotTradeCode(int size, PlanetOrbit planetOrbit, bool expected)
    {
        
        var planet = new RttWorldgenPlanet {Size = size, PlanetOrbit = planetOrbit};
        _classUnderTest.AddHotTradeCode(planet);
        Assert.That(planet.TradeCodes.Contains(TradeCode.Hot), Is.EqualTo(expected));
    }

    [TestCase(2, false)]
    [TestCase(3, true)]
    [TestCase(5, true)]
    [TestCase(6, false)]
    public void AddColdTradeCode(int temperature, bool expected)
    {
        var planet = new Planet {Temperature = temperature};
        _classUnderTest.AddColdTradeCode(planet);
        Assert.That(planet.TradeCodes.Contains(TradeCode.Cold), Is.EqualTo(expected));
    }

    [TestCase(true, CompanionOrbit.Close, true)]
    [TestCase(true, CompanionOrbit.Distant, false)]
    [TestCase(true, CompanionOrbit.Moderate, false)]
    [TestCase(true, CompanionOrbit.None, false)]
    [TestCase(true, CompanionOrbit.Tight, false)]
    [TestCase(false, CompanionOrbit.Close, false)]
    public void AddLockedTradeCode(bool parent, CompanionOrbit companionOrbit, bool expected)
    {
        var planet = new RttWorldgenPlanet {
            ParentId = parent ? Guid.NewGuid() : Guid.Empty,
            SatelliteOrbit = companionOrbit
        };
        _classUnderTest.AddLockedTradeCode(planet);
        Assert.That(planet.TradeCodes.Contains(TradeCode.Locked), Is.EqualTo(expected));
    }

    [TestCase(7, 7, 5, 10, true)]
    [TestCase(5, 7, 5, 10, false)]
    [TestCase(6, 7, 5, 10, true)]
    [TestCase(9, 7, 5, 10, true)]
    [TestCase(10, 7, 5, 10, false)]
    [TestCase(7, 3, 5, 10, false)]
    [TestCase(7, 4, 5, 10, true)]
    [TestCase(7, 9, 5, 10, true)]
    [TestCase(7, 10, 5, 10, false)]
    [TestCase(7, 7, 2, 10, false)]
    [TestCase(7, 7, 3, 10, true)]
    [TestCase(7, 7, 7, 10, true)]
    [TestCase(7, 7, 8, 10, false)]
    [TestCase(7, 7, 5, 7, false)]
    [TestCase(7, 7, 5, 8, true)]
    public void AddTropicTradeCode(int size, int atmosphere, int hydrographics, int temperature, bool expected)
    {
        var planet = new Planet {
            Size = size,
            Atmosphere = atmosphere,
            Hydrographics = hydrographics,
            Temperature = temperature
        };
        _classUnderTest.AddTropicTradeCode(planet);
        Assert.That(planet.TradeCodes.Contains(TradeCode.Tropic), Is.EqualTo(expected));
    }

    [TestCase(7, 7, 5, 3, true)]
    [TestCase(5, 7, 5, 3, false)]
    [TestCase(6, 7, 5, 3, true)]
    [TestCase(9, 7, 5, 3, true)]
    [TestCase(10, 7, 5, 3, false)]
    [TestCase(7, 3, 5, 3, false)]
    [TestCase(7, 4, 5, 3, true)]
    [TestCase(7, 9, 5, 3, true)]
    [TestCase(7, 10, 5, 3, false)]
    [TestCase(7, 7, 2, 3, false)]
    [TestCase(7, 7, 3, 3, true)]
    [TestCase(7, 7, 7, 3, true)]
    [TestCase(7, 7, 8, 3, false)]
    [TestCase(7, 7, 5, 5, true)]
    [TestCase(7, 7, 5, 6, false)]
    public void AddTundraTradeCode(int size, int atmosphere, int hydrographics, int temperature, bool expected)
    {
        var planet = new Planet {
            Size = size,
            Atmosphere = atmosphere,
            Hydrographics = hydrographics,
            Temperature = temperature
        };
        _classUnderTest.AddTundraTradeCode(planet);
        Assert.That(planet.TradeCodes.Contains(TradeCode.Tundra), Is.EqualTo(expected));
    }
    
    [TestCase(0, true)]
    [TestCase(1, true)]
    [TestCase(2, false)]
    [TestCase(3, false)]
    public void AddTwilightZoneTradeCode(int orbitPosition, bool expected)
    {
        var planet = new RttWorldgenPlanet {
            OrbitPosition = orbitPosition
        };
        _classUnderTest.AddTwilightZoneTradeCode(planet);
        Assert.That(planet.TradeCodes.Contains(TradeCode.TwilightZone), Is.EqualTo(expected));
    }
}