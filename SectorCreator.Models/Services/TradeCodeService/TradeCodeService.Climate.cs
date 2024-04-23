using SectorCreator.Global.Enums;
using SectorCreator.Models.Basic;
using SectorCreator.Models.RTTWorldgen;

namespace SectorCreator.Models.Services.TradeCodeService;

public partial class TradeCodeService
{
    public static void AddFrozenTradeCode(Planet planet)
    {
        if (planet is RttWorldgenPlanet {
                Size: >= 2 and <= 9,
                Hydrographics: >= 1,
                PlanetOrbit: PlanetOrbit.Outer
            } || planet.Temperature == Temperature.Frozen) {
            planet.TradeCodes.Add(TradeCode.Frozen);
        }
    }

    public static void AddHotTradeCode(Planet planet)
    {
        if (planet is RttWorldgenPlanet {
                Size: >= 2 and <= 9,
                PlanetOrbit: PlanetOrbit.Epistellar
            } || planet.Temperature is Temperature.Hot or Temperature.Boiling) {
            planet.TradeCodes.Add(TradeCode.Hot);
        }
    }

    public static void AddColdTradeCode(Planet planet)
    {
        if (planet.Temperature == Temperature.Cold) {
            planet.TradeCodes.Add(TradeCode.Cold);
        }
    }

    public static void AddLockedTradeCode(Planet planet)
    {
        if (planet is RttWorldgenPlanet {IsMainWorld: false, SatelliteOrbit: CompanionOrbit.Close}) {
            planet.TradeCodes.Add(TradeCode.Locked);
        }
    }

    public static void AddTropicTradeCode(Planet planet)
    {
        if (planet.Size is >= 6 and <= 9
            && planet.Atmosphere is >= 4 and <= 9
            && planet.Hydrographics is >= 3 and <= 7
            && planet.Temperature is Temperature.Hot or Temperature.Warm) {
            planet.TradeCodes.Add(TradeCode.Tropic);
        }
    }

    public static void AddTundraTradeCode(Planet planet)
    {
        if (planet.Size is >= 6 and <= 9
            && planet.Atmosphere is >= 4 and <= 9
            && planet.Hydrographics is >= 3 and <= 7
            && planet.Temperature is Temperature.Cold or Temperature.Cool) {
            planet.TradeCodes.Add(TradeCode.Tundra);
        }
    }

    public static void AddTwilightZoneTradeCode(Planet planet)
    {
        if (planet is RttWorldgenPlanet {PlanetOrbit: PlanetOrbit.Epistellar, OrbitPosition: <= 1, IsMainWorld: true}) {
            planet.TradeCodes.Add(TradeCode.TwilightZone);
        }
    }
}