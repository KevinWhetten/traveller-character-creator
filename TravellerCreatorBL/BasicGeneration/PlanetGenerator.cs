using TravellerCreatorModels;
using TravellerCreatorModels.Basic;

namespace TravellerCharacterCreatorBL.BasicGeneration;

public interface IPlanetGenerator
{
    Planet GeneratePlanet();
}

public class PlanetGenerator : IPlanetGenerator
{
    private readonly ITradeCodesService _tradeCodesService;

    public PlanetGenerator(ITradeCodesService tradeCodesService)
    {
        _tradeCodesService = tradeCodesService;
    }
    
    public Planet GeneratePlanet()
    {
        var planet = new Planet();

        planet.Generate();
        planet.TradeCodes = _tradeCodesService.GetTradeCodes(planet);

        return planet;
    }
}