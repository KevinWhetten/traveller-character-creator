using SectorCreator.Global.Enums;
using SectorCreator.Models.Basic;
using SectorCreator.Models.RTTWorldgen;

namespace SectorCreator.Models.Services.TradeCodeService;

public partial class TradeCodeService
{
    public static void AddAsteroidTradeCode(Planet planet)
    {
        if (planet.Size is 0) {
            planet.TradeCodes.Add(TradeCode.Asteroid);
        }
    }

    public static void AddDesertTradeCode(Planet planet)
    {
        if (planet is {Atmosphere: >= 2 and <= 9, Hydrographics: 0}) {
            planet.TradeCodes.Add(TradeCode.Desert);
        }
    }

    public static void AddFluidOceansTradeCode(Planet planet)
    {
        if (planet is {Atmosphere: >= 10 and <= 12, Hydrographics: >= 1 and < 15}) {
            planet.TradeCodes.Add(TradeCode.FluidOceans);
        }
    }

    public static void AddGardenTradeCode(Planet planet)
    {
        if (planet is {Size: >= 6 and <= 8, Atmosphere: 5 or 6 or 8, Hydrographics: >= 5 and <= 7}) {
            planet.TradeCodes.Add(TradeCode.Garden);
        }
    }

    public static void AddHellworldTradeCode(Planet planet)
    {
        if (planet is {
                Size: >= 3,
                Atmosphere: 2 or 4 or 7 or 9 or 10 or 11 or 12,
                Hydrographics: 0 or 1 or 2 or 15
            }) {
            planet.TradeCodes.Add(TradeCode.Hellworld);
        }
    }

    public static void AddIceCappedTradeCode(Planet planet)
    {
        if (planet.Atmosphere is 0 or 1
            && planet.Hydrographics is >= 1 and < 15) {
            planet.TradeCodes.Add(TradeCode.IceCapped);
        }
    }

    public static void AddOceanWorldTradeCode(Planet planet)
    {
        if (planet is {
                Size: >= 10,
                Atmosphere: (>= 3 and <= 9) or (>= 13 and <= 15),
                Hydrographics: >= 10 and < 15
            }) {
            planet.TradeCodes.Add(TradeCode.OceanWorld);
        }
    }

    public static void AddVacuumTradeCode(Planet planet)
    {
        if (planet.Atmosphere == 0) {
            planet.TradeCodes.Add(TradeCode.Vacuum);
        }
    }

    public static void AddWaterWorldTradeCode(Planet planet)
    {
        if (planet.Size is >= 3 and <= 9
            && planet.Atmosphere is (>= 3 and <= 9) or (>= 13 and <= 15)
            && planet.Hydrographics is >= 10 and <= 15) {
            planet.TradeCodes.Add(TradeCode.WaterWorld);
        }
    }
}