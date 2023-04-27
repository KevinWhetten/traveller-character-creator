using SectorCreator.Global.Enums;
using SectorCreator.Models.Basic;
using SectorCreator.Models.RTTWorldgen;

namespace SectorCreator.Models.Services.TradeCodeService;

public partial class TradeCodeService
{
    public void AddFrozenTradeCode(Planet planet)
    {
        if (planet is RttWorldgenPlanet {
                Size: >= 2 and <= 9,
                Hydrographics: >= 1,
                PlanetOrbit: PlanetOrbit.Outer
            } || planet is not RttWorldgenPlanet && planet.Temperature == Temperature.Frozen) {
            planet.TradeCodes.Add(TradeCode.Frozen);
        }
    }

    public void AddHotTradeCode(Planet planet)
    {
        if (planet is RttWorldgenPlanet {
                Size: >= 2 and <= 9,
                PlanetOrbit: PlanetOrbit.Epistellar
            } || planet is not RttWorldgenPlanet && planet.Temperature is Temperature.Hot or Temperature.Boiling) {
            planet.TradeCodes.Add(TradeCode.Hot);
        }
    }

    public void AddColdTradeCode(Planet planet)
    {
        if (planet.Temperature == Temperature.Cold) {
            planet.TradeCodes.Add(TradeCode.Cold);
        }
    }

    public void AddLockedTradeCode(Planet planet)
    {
        if (planet is RttWorldgenPlanet worldgenPlanet
            && worldgenPlanet.ParentId != Guid.Empty
            && worldgenPlanet.SatelliteOrbit == CompanionOrbit.Close) {
            planet.TradeCodes.Add(TradeCode.Locked);
        }
    }

    public void AddTropicTradeCode(Planet planet)
    {
        if (planet.Size is >= 6 and <= 9
            && planet.Atmosphere is >= 4 and <= 9
            && planet.Hydrographics is >= 3 and <= 7
            && planet.Temperature == Temperature.Hot) {
            planet.TradeCodes.Add(TradeCode.Tropic);
        }
    }

    public void AddTundraTradeCode(Planet planet)
    {
        if (planet.Size is >= 6 and <= 9
            && planet.Atmosphere is >= 4 and <= 9
            && planet.Hydrographics is >= 3 and <= 7
            && planet.Temperature == Temperature.Cold) {
            planet.TradeCodes.Add(TradeCode.Tundra);
        }
    }

    public void AddTwilightZoneTradeCode(Planet planet)
    {
        if (planet is RttWorldgenPlanet {OrbitPosition: <= 1}) {
            planet.TradeCodes.Add(TradeCode.TwilightZone);
        }
    }
}