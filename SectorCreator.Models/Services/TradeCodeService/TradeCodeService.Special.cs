using SectorCreator.Global.Enums;
using SectorCreator.Models.Basic;
using SectorCreator.Models.RTTWorldgen;

namespace SectorCreator.Models.Services.TradeCodeService;

public partial class TradeCodeService
{
    public static void AddSatelliteTradeCode(Planet planet)
    {
        if (planet is RttWorldgenPlanet  worldgenPlanet
            && !worldgenPlanet.IsMainWorld) {
            planet.TradeCodes.Add(TradeCode.Satellite);
        }
    }

    public static void AddForbiddenTradeCode(Planet planet)
    {
        if (planet.TravelZone == TravelZone.Red) {
            planet.TradeCodes.Add(TradeCode.Forbidden);
        } else if (_rollingService.D6(3) == 18) {
            planet.TradeCodes.Add(TradeCode.Forbidden);
        }
    }

    public static void AddAmberTradeCode(Planet planet)
    {
        if (planet.TravelZone == TravelZone.Amber) {
            planet.TradeCodes.Add(planet.Population <= 6 ? TradeCode.Danger : TradeCode.Puzzle);
        } else if (_rollingService.D6(3) >= 17) {
            planet.TradeCodes.Add(planet.Population <= 6 ? TradeCode.Danger : TradeCode.Puzzle);
        }
    }

    public static void AddDataRepositoryTradeCode(Planet planet)
    {
        if (_rollingService.D6(3) == 18) {
            planet.TradeCodes.Add(TradeCode.DataRepository);
        }
    }

    public static void AddAncientSiteTradeCode(Planet planet)
    {
        if (_rollingService.D6(3) == 18) {
            planet.TradeCodes.Add(TradeCode.AncientSite);
        }
    }

    public static void AddResearchStationTradeCode(Planet planet)
    {
        var roll = _rollingService.D6(2);
        switch (planet.Starport.Class) {
            case StarportClass.A:
                if (roll >= 6) {
                    planet.TradeCodes.Add(TradeCode.ResearchStation);
                }

                if (roll >= 9) {
                    planet.Starport.Installations.Add(StarportInstallation.Shipyard);
                }

                if (roll >= 12) {
                    planet.Starport.Installations.Add(StarportInstallation.MegaCorporateHeadquarters);
                }

                break;
            case StarportClass.B:
                if (roll >= 8) {
                    planet.TradeCodes.Add(TradeCode.ResearchStation);
                }

                if (roll >= 11) {
                    planet.Starport.Installations.Add(StarportInstallation.Shipyard);
                }

                break;
            case StarportClass.C:
                if (roll >= 10) {
                    planet.TradeCodes.Add(TradeCode.ResearchStation);
                }

                break;
        }
    }
}