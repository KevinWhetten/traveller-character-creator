using TravellerCreatorModels;
using TravellerCreatorModels.Basic;

namespace TravellerCharacterCreatorBL.BasicGeneration;

public interface ISubSectorGenerator
{
    SubSector GenerateSubSector(Coordinates coordinates);
}

public class SubSectorGenerator : ISubSectorGenerator
{
    private readonly IHexGenerator _hexGenerator;

    public SubSectorGenerator()
    {
        _hexGenerator = new HexGenerator();
    }

    public SubSectorGenerator(IHexGenerator hexGenerator)
    {
        _hexGenerator = hexGenerator;
    }

    public SubSector GenerateSubSector(Coordinates coordinates)
    {
        var subSector = new SubSector {
            Coordinates = coordinates
        };

        for (var x = 0; x < 8; x++) {
            for (var y = 0; y < 10; y++) {
                Hex newHex = _hexGenerator.GenerateHex(new Coordinates(x, y));
                subSector.Hexes.Add(newHex);
            }
        }

        return subSector;
    }
}