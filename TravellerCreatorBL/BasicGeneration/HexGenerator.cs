using TravellerCreatorGlobalMethods;
using TravellerCreatorModels;
using TravellerCreatorModels.Basic;

namespace TravellerCharacterCreatorBL.BasicGeneration;

public interface IHexGenerator
{
    Hex GenerateHex(Coordinates coordinates);
}

public class HexGenerator : IHexGenerator
{
    private readonly IPlanetGenerator _planetGenerator;

    public HexGenerator()
    {
        _planetGenerator = new PlanetGenerator(new TradeCodeService());
    }

    public HexGenerator (IPlanetGenerator planetGenerator)
    {
        _planetGenerator = planetGenerator;
    }

    public Hex GenerateHex(Coordinates coordinates)
    {
        var hex = new Hex(coordinates) {
            Planet = Roll.D6(1) >= 4 
                ? _planetGenerator.GeneratePlanet() 
                : null
        };

        return hex;
    }
}