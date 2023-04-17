using SectorCreator.Global.Enums;
using SectorCreator.Models.Basic;
using SectorCreator.Models.RTTWorldgen;

namespace SectorCreator.Models.Services.TradeCodeService;

public partial class TradeCodeService
{
    public void AddSatelliteTradeCode(Planet planet)
    {
        if (planet is RttWorldgenPlanet worldgenPlanet
            && worldgenPlanet.ParentId != Guid.Empty) {
            planet.TradeCodes.Add(TradeCode.Satellite);
        }
    }

    public void AddForbiddenTradeCode(Planet planet)
    {
        if (planet.TravelCode == TravelCode.Red) {
            planet.TradeCodes.Add(TradeCode.Forbidden);
        }
    }

    public void AddAmberTradeCode(Planet planet)
    {
        if (planet.TravelCode == TravelCode.Amber) {
            planet.TradeCodes.Add(TradeCode.Danger);
        } else if (_rollingService.D6(2) == 12) {
            planet.TradeCodes.Add(TradeCode.Puzzle);
        }
    }

    public void AddDataRepositoryTradeCode(Planet planet)
    {
        if (_rollingService.D6(3) == 18) {
            planet.TradeCodes.Add(TradeCode.DataRepository);
            planet.Bases.Add(Base.DataRepository);
        }
    }

    public void AddAncientSiteTradeCode(Planet planet)
    {
        if (_rollingService.D6(3) == 18) {
            planet.TradeCodes.Add(TradeCode.AncientSite);
            planet.Bases.Add(Base.AncientSite);
        }
    }

    public void AddResearchStationTradeCode(Planet planet)
    {
        var roll = _rollingService.D6(2);
        switch (planet.Starport) {
            case 'A':
                if (roll >= 6) {
                    planet.TradeCodes.Add(TradeCode.ResearchStation);
                    planet.Bases.Add(Base.Research);
                }

                if (roll >= 9) {
                    planet.Bases.Add(Base.Shipyard);
                }

                if (roll >= 12) {
                    planet.Bases.Add(Base.MegaCorporateHeadquarters);
                }

                break;
            case 'B':
                if (roll >= 8) {
                    planet.TradeCodes.Add(TradeCode.ResearchStation);
                    planet.Bases.Add(Base.Research);
                }

                if (roll >= 11) {
                    planet.Bases.Add(Base.Shipyard);
                }

                break;
            case 'C':
                if (roll >= 10) {
                    planet.TradeCodes.Add(TradeCode.ResearchStation);
                    planet.Bases.Add(Base.Research);
                }

                break;
        }
    }
}