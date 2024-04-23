using SectorCreator.Global.Enums;
using SectorCreator.WorldBuilder.Planet.Planet;

namespace SectorCreator.WorldBuilder.Services.Services.TradeCodeService;

public partial class TradeCodeService
{
    private static void GetPopulationTradeCodes(WorldBuilderPlanet planet)
    {
        AddDiebackTradeCode(planet);
        AddBarrenTradeCode(planet);
        AddLowPopulationTradeCode(planet);
        AddNonIndustrialTradeCode(planet);
        AddPreHighPopulationTradeCode(planet);
        AddHighPopulationTradeCode(planet);
    }

    public static void AddDiebackTradeCode(WorldBuilderPlanet planet)
    {
        if (planet is {Population: 0, Government: 0, LawLevel: 0, TechLevel: >= 1}) {
            TradeCodes.Add(TradeCode.Dieback);
        }
    }

    public static void AddBarrenTradeCode(WorldBuilderPlanet planet)
    {
        if (planet.Population is 0 && planet.Government is 0 && planet.LawLevel is 0) {
            TradeCodes.Add(TradeCode.Barren);
        }
    }

    public static void AddLowPopulationTradeCode(WorldBuilderPlanet planet)
    {
        if (planet.Population is >= 1 and <= 3) {
            TradeCodes.Add(TradeCode.LowPopulation);
        }
    }

    public static void AddNonIndustrialTradeCode(WorldBuilderPlanet planet)
    {
        if (planet.Population is >= 4 and <= 6) {
            TradeCodes.Add(TradeCode.NonIndustrial);
        }
    }

    public static void AddPreHighPopulationTradeCode(WorldBuilderPlanet planet)
    {
        if (planet.Population == 8) {
            TradeCodes.Add(TradeCode.PreHighPopulation);
        }
    }

    public static void AddHighPopulationTradeCode(WorldBuilderPlanet planet)
    {
        if (planet.Population >= 9) {
            TradeCodes.Add(TradeCode.HighPopulation);
        }
    }
}