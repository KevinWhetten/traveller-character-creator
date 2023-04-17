using SectorCreator.Global.Enums;
using SectorCreator.Models.Basic;

namespace SectorCreator.Models.Services.TradeCodeService;

public partial class TradeCodeService
{
    public void AddPreAgriculturalTradeCode(Planet planet)
    {
        if (planet.Atmosphere is >= 4 and <= 9
            && planet.Hydrographics is >= 4 and <= 8
            && planet.Population is 4 or 8) {
            planet.TradeCodes.Add(TradeCode.PreAgricultural);
        }
    }

    public void AddAgriculturalTradeCode(Planet planet)
    {
        if (planet.Atmosphere is >= 4 and <= 9
            && planet.Hydrographics is >= 4 and <= 8
            && planet.Population is >= 5 and <= 7) {
            planet.TradeCodes.Add(TradeCode.Agricultural);
        }
    }

    public void AddNonAgriculturalTradeCode(Planet planet)
    {
        if (planet.Atmosphere is >= 0 and <= 3
            && planet.Hydrographics is >= 0 and <= 3
            && planet.Population >= 6) {
            planet.TradeCodes.Add(TradeCode.NonAgricultural);
        }
    }

    public void AddPreIndustrialTradeCode(Planet planet)
    {
        if (planet.Atmosphere is 0 or 1 or 2 or 4 or 7 or 9
            && planet.Population is 7 or 8) {
            planet.TradeCodes.Add(TradeCode.PreIndustrial);
        }
    }

    public void AddIndustrialTradeCode(Planet planet)
    {
        if (planet.Atmosphere is >= 0 and <= 2 or 4 or 7 or 9
            && planet.Population >= 9) {
            planet.TradeCodes.Add(TradeCode.Industrial);
        }
    }

    public void AddPoorTradeCode(Planet planet)
    {
        if (planet.Atmosphere is >= 2 and <= 5
            && planet.Hydrographics is >= 0 and <= 3) {
            planet.TradeCodes.Add(TradeCode.Poor);
        }
    }

    public void AddPreRichTradeCode(Planet planet)
    {
        if (planet.Atmosphere is 6 or 8
            && planet.Population is 5 or 9) {
            planet.TradeCodes.Add(TradeCode.PreRich);
        }
    }

    public void AddRichTradeCode(Planet planet)
    {
        if (planet.Atmosphere is 6 or 8
            && planet.Population is >= 6 and <= 8) {
            planet.TradeCodes.Add(TradeCode.Rich);
        }
    }
}