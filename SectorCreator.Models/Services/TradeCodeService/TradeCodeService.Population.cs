using SectorCreator.Global.Enums;
using SectorCreator.Models.Basic;
using SectorCreator.Models.RTTWorldgen;

namespace SectorCreator.Models.Services.TradeCodeService;

public partial class TradeCodeService
{
    public static void AddDiebackTradeCode(Planet planet)
    {
        if (planet is {Population: 0, Government: 0, LawLevel: 0, TechLevel: >= 1}) {
            planet.TradeCodes.Add(TradeCode.Dieback);
        }
    }

    public static void AddBarrenTradeCode(Planet planet)
    {
        if (planet.Population is 0 && planet.Government is 0 && planet.LawLevel is 0) {
            planet.TradeCodes.Add(TradeCode.Barren);
        }
    }

    public static void AddLowPopulationTradeCode(Planet planet)
    {
        if (planet.Population is >= 1 and <= 3) {
            planet.TradeCodes.Add(TradeCode.LowPopulation);
        }
    }

    public static void AddNonIndustrialTradeCode(Planet planet)
    {
        if (planet.Population is >= 4 and <= 6) {
            planet.TradeCodes.Add(TradeCode.NonIndustrial);
        }
    }

    public static void AddPreHighPopulationTradeCode(Planet planet)
    {
        if (planet.Population == 8) {
            planet.TradeCodes.Add(TradeCode.PreHighPopulation);
        }
    }

    public static void AddHighPopulationTradeCode(Planet planet)
    {
        if (planet.Population >= 9) {
            planet.TradeCodes.Add(TradeCode.HighPopulation);
        }
    }
}