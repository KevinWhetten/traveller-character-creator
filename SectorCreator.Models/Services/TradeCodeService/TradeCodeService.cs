using SectorCreator.Global;
using SectorCreator.Global.Enums;
using SectorCreator.Models.Basic;
using SectorCreator.Models.RTTWorldgen;

namespace SectorCreator.Models.Services.TradeCodeService;

public static partial class TradeCodeService
{
    private static readonly IRollingService _rollingService = new RollingService();

    public static List<string> AddTradeCodes(Planet planet)
    {
        planet.TradeCodes = new List<string>();

        // Planetary
        AddAsteroidTradeCode(planet);
        AddDesertTradeCode(planet);
        AddFluidOceansTradeCode(planet);
        AddGardenTradeCode(planet);
        AddHellworldTradeCode(planet);
        AddIceCappedTradeCode(planet);
        AddOceanWorldTradeCode(planet);
        AddVacuumTradeCode(planet);
        AddWaterWorldTradeCode(planet);

        // Population
        AddDiebackTradeCode(planet);
        AddBarrenTradeCode(planet);
        AddLowPopulationTradeCode(planet);
        AddNonIndustrialTradeCode(planet);
        AddPreHighPopulationTradeCode(planet);
        AddHighPopulationTradeCode(planet);

        // Economic
        AddPreAgriculturalTradeCode(planet);
        AddAgriculturalTradeCode(planet);
        AddNonAgriculturalTradeCode(planet);
        AddPreIndustrialTradeCode(planet);
        AddIndustrialTradeCode(planet);
        AddPoorTradeCode(planet);
        AddPreRichTradeCode(planet);
        AddRichTradeCode(planet);

        // Climate
        AddFrozenTradeCode(planet);
        AddHotTradeCode(planet);
        AddColdTradeCode(planet);
        AddLockedTradeCode(planet);
        AddTropicTradeCode(planet);
        AddTundraTradeCode(planet);
        AddTwilightZoneTradeCode(planet);

        // Secondary
        AddFarmingTradeCode(planet);
        AddMiningTradeCode(planet);
        AddCaptiveTradeCode(planet);
        AddReserveTradeCode(planet);

        // Political
        AddSubsectorCapitalTradeCode(planet);
        AddSectorCapitalTradeCode(planet);
        AddCapitalTradeCode(planet);
        AddColonyTradeCode(planet);

        // Special
        AddSatelliteTradeCode(planet);
        AddForbiddenTradeCode(planet);
        AddAmberTradeCode(planet);
        AddDataRepositoryTradeCode(planet);
        AddAncientSiteTradeCode(planet);
        AddResearchStationTradeCode(planet);

        // Other
        AddHighTechnologyTradeCode(planet);
        AddLowTechnologyTradeCode(planet);

        return planet.TradeCodes;
    }

    #region Other

    public static void AddHighTechnologyTradeCode(Planet planet)
    {
        if (planet.TechLevel >= 12) {
            planet.TradeCodes.Add(TradeCode.HighTechnology);
        }
    }

    public static void AddLowTechnologyTradeCode(Planet planet)
    {
        if (planet.TechLevel <= 5 && planet.Population > 0) {
            planet.TradeCodes.Add(TradeCode.LowTechnology);
        }
    }

    #endregion
}