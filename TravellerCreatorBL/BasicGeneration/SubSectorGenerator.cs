using TravellerCreatorModels;
using TravellerCreatorModels.Basic;

namespace TravellerCharacterCreatorBL.BasicGeneration;

public interface ISubsectorGenerator
{
    Subsector GenerateSubsector(Coordinates coordinates);
}

public class SubsectorGenerator : ISubsectorGenerator
{
    private readonly IHexGenerator _hexGenerator;

    public SubsectorGenerator()
    {
        _hexGenerator = new HexGenerator();
    }

    public SubsectorGenerator(IHexGenerator hexGenerator)
    {
        _hexGenerator = hexGenerator;
    }

    public Subsector GenerateSubsector(Coordinates coordinates)
    {
        var subsector = new Subsector {
            Coordinates = coordinates
        };

        for (var x = 0; x < 8; x++) {
            for (var y = 0; y < 10; y++) {
                Hex newHex = _hexGenerator.GenerateHex(new Coordinates(x, y));
                subsector.Hexes.Add(newHex);
            }
        }

        return subsector;
    }
}