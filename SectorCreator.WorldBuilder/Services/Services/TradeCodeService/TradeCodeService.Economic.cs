using SectorCreator.Global.Enums;
using SectorCreator.WorldBuilder.Planet.Planet;

namespace SectorCreator.WorldBuilder.Services.Services.TradeCodeService;

public partial class TradeCodeService
{
    public static void GetEconomicTradeCodes(WorldBuilderPlanet planet)
    {
        AddPreAgriculturalTradeCode(planet);
        AddAgriculturalTradeCode(planet);
        AddNonAgriculturalTradeCode(planet);
        AddPreIndustrialTradeCode(planet);
        AddIndustrialTradeCode(planet);
        AddPoorTradeCode(planet);
        AddPreRichTradeCode(planet);
        AddRichTradeCode(planet);
    }

    public static void AddPreAgriculturalTradeCode(WorldBuilderPlanet planet)
    {
        if (planet.Atmosphere is >= 4 and <= 9
            && planet.Hydrographics is >= 4 and <= 8
            && planet.Population is 4 or 8) {
            TradeCodes.Add(TradeCode.PreAgricultural);
        }
    }

    public static void AddAgriculturalTradeCode(WorldBuilderPlanet planet)
    {
        if (planet.Atmosphere is >= 4 and <= 9
            && planet.Hydrographics is >= 4 and <= 8
            && planet.Population is >= 5 and <= 7) {
            TradeCodes.Add(TradeCode.Agricultural);
        }
    }

    public static void AddNonAgriculturalTradeCode(WorldBuilderPlanet planet)
    {
        if (planet.Atmosphere is >= 0 and <= 3
            && planet.Hydrographics is >= 0 and <= 3
            && planet.Population >= 6) {
            TradeCodes.Add(TradeCode.NonAgricultural);
        }
    }

    public static void AddPreIndustrialTradeCode(WorldBuilderPlanet planet)
    {
        if (planet.Atmosphere is 0 or 1 or 2 or 4 or 7 or 9
            && planet.Population is 7 or 8) {
            TradeCodes.Add(TradeCode.PreIndustrial);
        }
    }

    public static void AddIndustrialTradeCode(WorldBuilderPlanet planet)
    {
        if (planet.Atmosphere is (>= 0 and <= 2) or 4 or 7 or (>= 9 and <= 12)
            && planet.Population >= 9) {
            TradeCodes.Add(TradeCode.Industrial);
        }
    }

    public static void AddPoorTradeCode(WorldBuilderPlanet planet)
    {
        if (planet.Atmosphere is >= 2 and <= 5
            && planet.Hydrographics is >= 0 and <= 3) {
            TradeCodes.Add(TradeCode.Poor);
        }
    }

    public static void AddPreRichTradeCode(WorldBuilderPlanet planet)
    {
        if (planet.Atmosphere is 6 or 8
            && planet.Population is 5 or 9) {
            TradeCodes.Add(TradeCode.PreRich);
        }
    }

    public static void AddRichTradeCode(WorldBuilderPlanet planet)
    {
        if (planet.Atmosphere is 6 or 8
            && planet.Population is >= 6 and <= 8) {
            TradeCodes.Add(TradeCode.Rich);
        }
    }
}