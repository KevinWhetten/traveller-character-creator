using TravellerCreatorModels.Basic;
using TravellerCreatorModels.Enums;
using TravellerCreatorModels.Interfaces;
using TravellerCreatorModels.StarFrontiers;

namespace TravellerCharacterCreatorBL;

public class PlanetGenerator
{
    private readonly ITradeCodesService _tradeCodesService;

    public PlanetGenerator(ITradeCodesService tradeCodesService)
    {
        _tradeCodesService = tradeCodesService;
    }
    
    public IPlanet GeneratePlanet(SectorType sectorType)
    {
        var planet = new BasicPlanet();

        planet.Generate(sectorType);
        planet.TradeCodes = _tradeCodesService.GetTradeCodes(planet);

        return planet;
    }

    public IPlanet GenerateStarFrontiersPlanet(bool habitable, int habitableBase, int planetNum)
    {
        var planet = new StarFrontiersPlanet();

        planet.Generate(habitable, habitableBase, planetNum);

        return planet;
    }
}