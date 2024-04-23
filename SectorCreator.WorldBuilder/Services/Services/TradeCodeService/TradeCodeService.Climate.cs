using SectorCreator.Global.Enums;
using SectorCreator.WorldBuilder.Planet.Planet;

namespace SectorCreator.WorldBuilder.Services.Services.TradeCodeService;

public partial class TradeCodeService
{
    private static void GetClimateTradeCodes(WorldBuilderPlanet planet)
    {
        AddFrozenTradeCode(planet);
        AddHotTradeCode(planet);
        AddColdTradeCode(planet);
        AddLockedTradeCode(planet);
        AddTropicTradeCode(planet);
        AddTundraTradeCode(planet);
        AddTwilightZoneTradeCode(planet);
    }

    private static void AddFrozenTradeCode(WorldBuilderPlanet planet)
    {
        if (planet.Temperature < 222) {
            TradeCodes.Add(TradeCode.Frozen);
        }
    }

    private static void AddHotTradeCode(WorldBuilderPlanet planet)
    {
        if (planet.Temperature is >= 304 and <= 353) {
            TradeCodes.Add(TradeCode.Hot);
        }
    }

    private static void AddColdTradeCode(WorldBuilderPlanet planet)
    {
        if (planet.Temperature is >= 222 and <= 273) {
            TradeCodes.Add(TradeCode.Cold);
        }
    }

    private static void AddLockedTradeCode(WorldBuilderPlanet planet)
    {
        if (planet.IsTidallyLocked) {
            TradeCodes.Add(TradeCode.Locked);
        }
    }

    private static void AddTropicTradeCode(WorldBuilderPlanet planet)
    {
        if (planet.Size is >= 6 and <= 9
            && planet.Atmosphere is >= 4 and <= 9
            && planet.Hydrographics is >= 3 and <= 7
            && planet.Temperature is >= 304 and <= 353) {
            TradeCodes.Add(TradeCode.Tropic);
        }
    }

    private static void AddTundraTradeCode(WorldBuilderPlanet planet)
    {
        if (planet.Size is >= 6 and <= 9
            && planet.Atmosphere is >= 4 and <= 9
            && planet.Hydrographics is >= 3 and <= 7
            && planet.Temperature is >= 222 and <= 273) {
            TradeCodes.Add(TradeCode.Tundra);
        }
    }

    private static void AddTwilightZoneTradeCode(WorldBuilderPlanet planet)
    {
        if (planet is {IsTidallyLocked: true, IsMoon: false}) {
            TradeCodes.Add(TradeCode.TwilightZone);
        }
    }
}