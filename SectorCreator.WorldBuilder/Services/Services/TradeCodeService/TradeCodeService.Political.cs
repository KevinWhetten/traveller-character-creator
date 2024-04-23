using SectorCreator.Global.Enums;
using SectorCreator.WorldBuilder.Planet.Planet;

namespace SectorCreator.WorldBuilder.Services.Services.TradeCodeService;

public partial class TradeCodeService
{
    private static void GetPoliticalTradeCodes(WorldBuilderPlanet planet)
    {
        AddSubsectorCapitalTradeCode(planet);
        AddSectorCapitalTradeCode(planet);
        AddCapitalTradeCode(planet);
        AddColonyTradeCode(planet);
    }

    private static void AddSubsectorCapitalTradeCode(WorldBuilderPlanet planet)
    {
    }

    private static void AddSectorCapitalTradeCode(WorldBuilderPlanet planet)
    {
    }

    private static void AddCapitalTradeCode(WorldBuilderPlanet planet)
    {
        if (planet.CurrentSophontExists) {
            TradeCodes.Add(TradeCode.Capital);
        }
    }

    private static void AddColonyTradeCode(WorldBuilderPlanet planet)
    {
    }
}