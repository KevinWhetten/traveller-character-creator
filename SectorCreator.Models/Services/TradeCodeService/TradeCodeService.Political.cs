using SectorCreator.Global.Enums;
using SectorCreator.Models.Basic;
using SectorCreator.Models.RTTWorldgen;

namespace SectorCreator.Models.Services.TradeCodeService;

public partial class TradeCodeService
{
    public static void AddSubsectorCapitalTradeCode(Planet planet)
    {
        if (planet.Atmosphere >= 0) { }
    }

    public static void AddSectorCapitalTradeCode(Planet planet)
    {
        if (planet.Atmosphere >= 0) { }
    }

    public static void AddCapitalTradeCode(Planet planet)
    {
        if (planet is RttWorldgenPlanet {
                Biosphere: >= 12
            }) {
            planet.TradeCodes.Add(TradeCode.Capital);
        }
    }

    public static void AddColonyTradeCode(Planet planet)
    {
        if (planet.Atmosphere >= 0) { }
    }
}