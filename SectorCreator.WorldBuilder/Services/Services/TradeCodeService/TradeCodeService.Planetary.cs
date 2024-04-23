using SectorCreator.Global.Enums;
using SectorCreator.WorldBuilder.Planet.Planet;

namespace SectorCreator.WorldBuilder.Services.Services.TradeCodeService;

public partial class TradeCodeService
{
    private static void GetPlanetaryTradeCodes(WorldBuilderPlanet planet)
    {
        AddAsteroidTradeCode(planet);
        AddDesertTradeCode(planet);
        AddFluidOceansTradeCode(planet);
        AddGardenTradeCode(planet);
        AddHellworldTradeCode(planet);
        AddIceCappedTradeCode(planet);
        AddOceanWorldTradeCode(planet);
        AddVacuumTradeCode(planet);
        AddWaterWorldTradeCode(planet);
    }

    private static void AddAsteroidTradeCode(WorldBuilderPlanet planet)
    {
        if (planet.Size is 0) {
            TradeCodes.Add(TradeCode.Asteroid);
        }
    }

    private static void AddDesertTradeCode(WorldBuilderPlanet planet)
    {
        if (planet is {Atmosphere: >= 2 and <= 9, Hydrographics: 0}) {
            TradeCodes.Add(TradeCode.Desert);
        }
    }

    private static void AddFluidOceansTradeCode(WorldBuilderPlanet planet)
    {
        if (planet is {Atmosphere: >= 10 and <= 12, Hydrographics: >= 1 and < 15}) {
            TradeCodes.Add(TradeCode.FluidOceans);
        }
    }

    private static void AddGardenTradeCode(WorldBuilderPlanet planet)
    {
        if (planet is {Size: >= 6 and <= 8, Atmosphere: 5 or 6 or 8, Hydrographics: >= 5 and <= 7}) {
            TradeCodes.Add(TradeCode.Garden);
        }
    }

    private static void AddHellworldTradeCode(WorldBuilderPlanet planet)
    {
        if (planet is {
                Size: >= 3,
                Atmosphere: 2 or 4 or 7 or 9 or 10 or 11 or 12,
                Hydrographics: 0 or 1 or 2 or 15
            }) {
            TradeCodes.Add(TradeCode.Hellworld);
        }
    }

    private static void AddIceCappedTradeCode(WorldBuilderPlanet planet)
    {
        if (planet.Atmosphere is 0 or 1
            && planet.Hydrographics is >= 1 and < 15) {
            TradeCodes.Add(TradeCode.IceCapped);
        }
    }

    private static void AddOceanWorldTradeCode(WorldBuilderPlanet planet)
    {
        if (planet is {
                Size: >= 10,
                Atmosphere: (>= 3 and <= 9) or (>= 13 and <= 15),
                Hydrographics: >= 10 and < 15
            }) {
            TradeCodes.Add(TradeCode.OceanWorld);
        }
    }

    private static void AddVacuumTradeCode(WorldBuilderPlanet planet)
    {
        if (planet.Atmosphere == 0) {
            TradeCodes.Add(TradeCode.Vacuum);
        }
    }

    private static void AddWaterWorldTradeCode(WorldBuilderPlanet planet)
    {
        if (planet.Size is >= 3 and <= 9
            && planet.Atmosphere is (>= 3 and <= 9) or (>= 13 and <= 15)
            && planet.Hydrographics is >= 10 and <= 15) {
            TradeCodes.Add(TradeCode.WaterWorld);
        }
    }
}