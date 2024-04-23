using System;
using NUnit.Framework;
using SectorCreator.Global;
using SectorCreator.Global.Enums;
using SectorCreator.Models.Basic;
using SectorCreator.Models.RTTWorldgen;
using SectorCreator.Models.Services.TradeCodeService;

namespace SectorCreator.Models.Tests.Services.TradeCodeServiceTests;

[TestFixture]
public class TradeCodeService_ClimateTests
{
    [TestCase(Temperature.Frozen, true)]
    [TestCase(Temperature.Cold, false)]
    [TestCase(Temperature.Temperate, false)]
    [TestCase(Temperature.Hot, false)]
    [TestCase(Temperature.Boiling, false)]
    public void AddFrozenTradeCode(Temperature temperature, bool expected)
    {
        var planet = new Planet {Temperature = temperature};
        TradeCodeService.AddFrozenTradeCode(planet);
        Assert.That(planet.TradeCodes.Contains(TradeCode.Frozen), Is.EqualTo(expected));
    }

    [TestCase(5, Temperature.Cold, PlanetOrbit.Outer, true)]
    [TestCase(1, Temperature.Cold, PlanetOrbit.Outer, false)]
    [TestCase(2, Temperature.Cold, PlanetOrbit.Outer, true)]
    [TestCase(9, Temperature.Cold, PlanetOrbit.Outer, true)]
    [TestCase(10, Temperature.Cold, PlanetOrbit.Outer, false)]
    [TestCase(5, 0, PlanetOrbit.Outer, false)]
    [TestCase(5, 1, PlanetOrbit.Outer, true)]
    [TestCase(5, 7, PlanetOrbit.Outer, true)]
    [TestCase(5, Temperature.Cold, PlanetOrbit.Inner, false)]
    [TestCase(5, Temperature.Cold, PlanetOrbit.Epistellar, false)]
    public void AddFrozenTradeCode(int size, int hydrographics, PlanetOrbit planetOrbit, bool expected)
    {
        var planet = new RttWorldgenPlanet {
            Size = size,
            Hydrographics = hydrographics,
            PlanetOrbit = planetOrbit
        };
        TradeCodeService.AddFrozenTradeCode(planet);
        Assert.That(planet.TradeCodes.Contains(TradeCode.Frozen), Is.EqualTo(expected));
    }

    [TestCase(Temperature.Frozen, false)]
    [TestCase(Temperature.Cold, false)]
    [TestCase(Temperature.Temperate, false)]
    [TestCase(Temperature.Hot, true)]
    [TestCase(Temperature.Boiling, true)]
    public void AddHotTradeCode(Temperature temperature, bool expected)
    {
        var planet = new Planet {Temperature = temperature};
        TradeCodeService.AddHotTradeCode(planet);
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
        TradeCodeService.AddHotTradeCode(planet);
        Assert.That(planet.TradeCodes.Contains(TradeCode.Hot), Is.EqualTo(expected));
    }

    [TestCase(Temperature.Frozen, false)]
    [TestCase(Temperature.Cold, true)]
    [TestCase(Temperature.Temperate, false)]
    [TestCase(Temperature.Hot, false)]
    [TestCase(Temperature.Boiling, false)]
    public void AddColdTradeCode(Temperature temperature, bool expected)
    {
        var planet = new Planet {Temperature = temperature};
        TradeCodeService.AddColdTradeCode(planet);
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
        TradeCodeService.AddLockedTradeCode(planet);
        Assert.That(planet.TradeCodes.Contains(TradeCode.Locked), Is.EqualTo(expected));
    }

    [TestCase(7, 7, 5, Temperature.Hot, true)]
    [TestCase(5, 7, 5, Temperature.Hot, false)]
    [TestCase(6, 7, 5, Temperature.Hot, true)]
    [TestCase(9, 7, 5, Temperature.Hot, true)]
    [TestCase(10, 7, 5, Temperature.Hot, false)]
    [TestCase(7, 3, 5, Temperature.Hot, false)]
    [TestCase(7, 4, 5, Temperature.Hot, true)]
    [TestCase(7, 9, 5, Temperature.Hot, true)]
    [TestCase(7, 10, 5, Temperature.Hot, false)]
    [TestCase(7, 7, 2, Temperature.Hot, false)]
    [TestCase(7, 7, 3, Temperature.Hot, true)]
    [TestCase(7, 7, 7, Temperature.Hot, true)]
    [TestCase(7, 7, 8, Temperature.Hot, false)]
    [TestCase(7, 7, 5, Temperature.Temperate, false)]
    [TestCase(7, 7, 5, Temperature.Hot, true)]
    public void AddTropicTradeCode(int size, int atmosphere, int hydrographics, Temperature temperature, bool expected)
    {
        var planet = new Planet {
            Size = size,
            Atmosphere = atmosphere,
            Hydrographics = hydrographics,
            Temperature = temperature
        };
        TradeCodeService.AddTropicTradeCode(planet);
        Assert.That(planet.TradeCodes.Contains(TradeCode.Tropic), Is.EqualTo(expected));
    }

    [TestCase(7, 7, 5, Temperature.Cold, true)]
    [TestCase(5, 7, 5, Temperature.Cold, false)]
    [TestCase(6, 7, 5, Temperature.Cold, true)]
    [TestCase(9, 7, 5, Temperature.Cold, true)]
    [TestCase(10, 7, 5, Temperature.Cold, false)]
    [TestCase(7, 4, 5, Temperature.Cold, true)]
    [TestCase(7, 9, 5, Temperature.Cold, true)]
    [TestCase(7, 10, 5, Temperature.Cold, false)]
    [TestCase(7, 7, 2, Temperature.Cold, false)]
    [TestCase(7, 7, 7, Temperature.Cold, true)]
    [TestCase(7, 7, 7, Temperature.Cold, true)]
    [TestCase(7, 7, 8, Temperature.Cold, false)]
    [TestCase(7, 7, 5, Temperature.Cold, true)]
    [TestCase(7, 7, 5, Temperature.Temperate, false)]
    public void AddTundraTradeCode(int size, int atmosphere, int hydrographics, Temperature temperature, bool expected)
    {
        var planet = new Planet {
            Size = size,
            Atmosphere = atmosphere,
            Hydrographics = hydrographics,
            Temperature = temperature
        };
        TradeCodeService.AddTundraTradeCode(planet);
        Assert.That(planet.TradeCodes.Contains(TradeCode.Tundra), Is.EqualTo(expected));
    }
    
    [TestCase(0, true)]
    [TestCase(1, true)]
    [TestCase(2, false)]
    [TestCase(Temperature.Cold, false)]
    public void AddTwilightZoneTradeCode(int orbitPosition, bool expected)
    {
        var planet = new RttWorldgenPlanet {
            OrbitPosition = orbitPosition
        };
        TradeCodeService.AddTwilightZoneTradeCode(planet);
        Assert.That(planet.TradeCodes.Contains(TradeCode.TwilightZone), Is.EqualTo(expected));
    }
}