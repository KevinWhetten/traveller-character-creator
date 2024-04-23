using SectorCreator.Global;
using SectorCreator.Global.Enums;
using SectorCreator.WorldBuilder.Planet.Planet;

namespace SectorCreator.WorldBuilder.Services.Services.TradeCodeService;

public static partial class TradeCodeService
{
    private static List<string> TradeCodes { get; set; } = new();

    public static List<string> AddTradeCodes(WorldBuilderPlanet planet)
    {
        TradeCodes = new List<string>();
        
        GetPlanetaryTradeCodes(planet);
        GetPopulationTradeCodes(planet);
        GetEconomicTradeCodes(planet);
        GetClimateTradeCodes(planet);
        GetSecondaryTradeCodes(planet);
        GetPoliticalTradeCodes(planet);
        GetSpecialTradeCodes(planet);

        // Special

        // Other
        AddHighTechnologyTradeCode(planet);
        AddLowTechnologyTradeCode(planet);

        return TradeCodes;
    }

    #region Other

    private static void AddHighTechnologyTradeCode(WorldBuilderPlanet planet)
    {
        if (planet.TechLevel >= 12) {
            TradeCodes.Add(TradeCode.HighTechnology);
        }
    }

    private static void AddLowTechnologyTradeCode(WorldBuilderPlanet planet)
    {
        if (planet.TechLevel <= 5 && planet.Population > 0) {
            TradeCodes.Add(TradeCode.LowTechnology);
        }
    }

    #endregion
}