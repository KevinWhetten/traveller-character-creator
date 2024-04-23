using SectorCreator.Global.Enums;
using SectorCreator.Models.Basic;
using SectorCreator.Models.RTTWorldgen;
using SectorCreator.WorldBuilder.Planet.Planet;

namespace SectorCreator.WorldBuilder.Services.Services.TradeCodeService;

public partial class TradeCodeService
{
    public static void GetSpecialTradeCodes(WorldBuilderPlanet planet)
    {
        AddSatelliteTradeCode(planet);
        AddForbiddenTradeCode(planet);
        AddAmberTradeCode(planet);
        AddDataRepositoryTradeCode(planet);
        AddAncientSiteTradeCode(planet);
        AddResearchStationTradeCode(planet);
    }

    private static void AddSatelliteTradeCode(WorldBuilderPlanet planet)
    {
        if (planet.IsMoon) {
            TradeCodes.Add(TradeCode.Satellite);
        }
    }

    private static void AddForbiddenTradeCode(WorldBuilderPlanet planet)
    {
    }

    private static void AddAmberTradeCode(WorldBuilderPlanet planet)
    {
    }

    private static void AddDataRepositoryTradeCode(WorldBuilderPlanet planet)
    {
    }

    private static void AddAncientSiteTradeCode(WorldBuilderPlanet planet)
    {
    }

    private static void AddResearchStationTradeCode(WorldBuilderPlanet planet)
    {
    }
}