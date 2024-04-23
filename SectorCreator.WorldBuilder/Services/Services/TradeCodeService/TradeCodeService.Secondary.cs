using SectorCreator.Global.Enums;
using SectorCreator.Models.Basic;
using SectorCreator.Models.RTTWorldgen;
using SectorCreator.WorldBuilder.Planet.Planet;

namespace SectorCreator.WorldBuilder.Services.Services.TradeCodeService;

public partial class TradeCodeService
{
    public static void GetSecondaryTradeCodes(WorldBuilderPlanet planet)
    {
        AddFarmingTradeCode(planet);
        AddMiningTradeCode(planet);
        AddCaptiveTradeCode(planet);
        AddReserveTradeCode(planet);
    }


    private static void AddFarmingTradeCode(WorldBuilderPlanet planet)
    {
    }

    private static void AddMiningTradeCode(WorldBuilderPlanet planet)
    {
    }

    private static void AddCaptiveTradeCode(WorldBuilderPlanet planet)
    {
    }

    private static void AddReserveTradeCode(WorldBuilderPlanet planet)
    {
    }
}