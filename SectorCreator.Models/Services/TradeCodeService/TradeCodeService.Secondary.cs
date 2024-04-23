using SectorCreator.Global.Enums;
using SectorCreator.Models.Basic;
using SectorCreator.Models.RTTWorldgen;

namespace SectorCreator.Models.Services.TradeCodeService;

public partial class TradeCodeService
{
    
    public static void AddFarmingTradeCode(Planet planet)
    {
        if (planet is RttWorldgenPlanet {
                Atmosphere: >= 4 and <= 9,
                Hydrographics: >= 4 and <= 8,
                Population: >= 2 and <= 6,
                PlanetOrbit: PlanetOrbit.Inner,
                IsMainWorld: false
            }) {
            planet.TradeCodes.Add(TradeCode.Farming);
        }
    }

    public static void AddMiningTradeCode(Planet planet)
    {
        if (planet is RttWorldgenPlanet {
                Population: >= 2 and <= 6,
                IsMainWorld: false,
                PlanetType: PlanetType.AsteroidBelt
            }) {
            planet.TradeCodes.Add(TradeCode.Mining);
        }
    }

    public static void AddCaptiveTradeCode(Planet planet)
    {
        if (planet.Government != 6) return;

        string? captiveType = planet switch {
            RttWorldgenPlanet  worldgenPlanet =>
                _rollingService.D6(1) switch {
                    <= 3 => TradeCode.MilitaryRule,
                    >= 4 => worldgenPlanet.IsMainWorld ? TradeCode.PrisonCamp : TradeCode.PenalColony
                },
            _ => _rollingService.D3(1) switch {
                1 => TradeCode.MilitaryRule,
                2 => TradeCode.PrisonCamp,
                3 => TradeCode.PenalColony,
                _ => throw new ArgumentOutOfRangeException()
            }
        };

        planet.TradeCodes.Add(captiveType);
    }

    public static void AddReserveTradeCode(Planet planet)
    {
        if (planet is {TechLevel: <= 5, Starport.Class: StarportClass.X, Population: >= 1} 
            and (RttWorldgenPlanet  {Biosphere: >= 7} or not RttWorldgenPlanet)) {
            planet.TradeCodes.Add(TradeCode.Reserve);
        }
    }
}