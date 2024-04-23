using SectorCreator.Global.Enums;
using SectorCreator.Models.Basic;
using SectorCreator.Models.RTTWorldgen;

namespace SectorCreator.Models.Services.TradeCodeService;

public partial class TradeCodeService
{
    public static void AddPreAgriculturalTradeCode(Planet planet)
    {
        if (planet.Atmosphere is >= 4 and <= 9
            && planet.Hydrographics is >= 4 and <= 8
            && planet.Population is 4 or 8) {
            planet.TradeCodes.Add(TradeCode.PreAgricultural);
        }
    }

    public static void AddAgriculturalTradeCode(Planet planet)
    {
        if (planet.Atmosphere is >= 4 and <= 9
            && planet.Hydrographics is >= 4 and <= 8
            && planet.Population is >= 5 and <= 7) {
            planet.TradeCodes.Add(TradeCode.Agricultural);
        }
    }

    public static void AddNonAgriculturalTradeCode(Planet planet)
    {
        if (planet.Atmosphere is >= 0 and <= 3
            && planet.Hydrographics is >= 0 and <= 3
            && planet.Population >= 6) {
            planet.TradeCodes.Add(TradeCode.NonAgricultural);
        }
    }

    public static void AddPreIndustrialTradeCode(Planet planet)
    {
        if (planet.Atmosphere is 0 or 1 or 2 or 4 or 7 or 9
            && planet.Population is 7 or 8) {
            planet.TradeCodes.Add(TradeCode.PreIndustrial);
        }
    }

    public static void AddIndustrialTradeCode(Planet planet)
    {
        if (planet.Atmosphere is (>= 0 and <= 2) or 4 or 7 or (>= 9 and <= 12)
            && planet.Population >= 9) {
            planet.TradeCodes.Add(TradeCode.Industrial);
        }
    }

    public static void AddPoorTradeCode(Planet planet)
    {
        if (planet.Atmosphere is >= 2 and <= 5
            && planet.Hydrographics is >= 0 and <= 3) {
            planet.TradeCodes.Add(TradeCode.Poor);
        }
    }

    public static void AddPreRichTradeCode(Planet planet)
    {
        if (planet.Atmosphere is 6 or 8
            && planet.Population is 5 or 9) {
            planet.TradeCodes.Add(TradeCode.PreRich);
        }
    }

    public static void AddRichTradeCode(Planet planet)
    {
        if (planet.Atmosphere is 6 or 8
            && planet.Population is >= 6 and <= 8) {
            planet.TradeCodes.Add(TradeCode.Rich);
        }
    }
}