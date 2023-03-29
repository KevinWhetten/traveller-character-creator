using TravellerCreatorModels.Basic;
using TravellerCreatorModels.Enums;
using TravellerCreatorModels.Interfaces;
using TravellerCreatorModels.Other;
using TravellerCreatorModels.StarFrontiers;

namespace TravellerCharacterCreatorBL;

public class SubsectorGenerator
{
    private readonly HexGenerator _hexGenerator;

    public SubsectorGenerator()
    {
        _hexGenerator = new HexGenerator();
    }

    public ISubsector GenerateBasicSubsector(Coordinates coordinates, SectorType sectorType)
    {
        var subsector = new BasicSubsector {
            Coordinates = coordinates
        };

        for (var y = 1; y <= 10; y++) {
            for (var x = 1; x <= 8; x++) {
                Coordinates hexCoordinates = GetCoordinates(subsector.Coordinates, new Coordinates(x, y));
                IHex newHex = _hexGenerator.GenerateBasicHex(hexCoordinates, sectorType);
                subsector.Hexes.Add(newHex);
            }
        }

        return subsector;
    }

    public ISubsector GenerateStarFrontiersSubsector(Coordinates coordinates)
    {
        var subsector = new StarFrontiersSubsector {
            Coordinates = coordinates
        };

        for (var y = 1; y <= 10; y++) {
            for (var x = 1; x <= 8; x++) {
                Coordinates hexCoordinates = GetCoordinates(subsector.Coordinates, new Coordinates(x, y));
                IHex newHex = _hexGenerator.GenerateStarFrontiersHex(hexCoordinates);
                subsector.Hexes.Add(newHex);
            }
        }

        return subsector;
    }

    public Coordinates GetCoordinates(Coordinates subsectorCoordinates, Coordinates hexCoordinates)
    {
        var x = hexCoordinates.X + 8 * ((subsectorCoordinates.X - 1) % 8);
        var y = hexCoordinates.Y + 10 * ((subsectorCoordinates.Y - 1) % 10);

        return new Coordinates(x, y);
    }
}