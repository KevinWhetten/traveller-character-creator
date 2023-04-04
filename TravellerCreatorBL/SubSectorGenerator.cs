using TravellerCreatorModels.Enums;
using TravellerCreatorModels.Interfaces;
using TravellerCreatorModels.Mongoose;
using TravellerCreatorModels.Other;
using TravellerCreatorModels.RTTWorldgen;
using TravellerCreatorModels.StarFrontiers;

namespace TravellerCharacterCreatorBL;

public class SubsectorGenerator
{
    private readonly HexGenerator _hexGenerator;

    public SubsectorGenerator()
    {
        _hexGenerator = new HexGenerator();
    }

    public Coordinates GetCoordinates(Coordinates subsectorCoordinates, Coordinates hexCoordinates)
    {
        var x = hexCoordinates.X + 8 * ((subsectorCoordinates.X - 1) % 8);
        var y = hexCoordinates.Y + 10 * ((subsectorCoordinates.Y - 1) % 10);

        return new Coordinates(x, y);
    }

    public ISubsector GenerateMongooseSubsector(Coordinates coordinates, SectorType sectorType)
    {
        var subsector = new MongooseSubsector {
            Coordinates = coordinates
        };

        for (var y = 1; y <= 10; y++) {
            for (var x = 1; x <= 8; x++) {
                Coordinates hexCoordinates = GetCoordinates(subsector.Coordinates, new Coordinates(x, y));
                IHex newHex = _hexGenerator.GenerateMongooseHex(hexCoordinates, sectorType);
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

    public ISubsector GenerateRTTWorldgenSubsector(Coordinates coordinates)
    {
        var subsector = new RTTWorldgenSubsector {
            Coordinates = coordinates
        };

        for (var y = 1; y <= 10; y++) {
            for (var x = 1; x <= 8; x++) {
                Coordinates hexCoordinates = GetCoordinates(subsector.Coordinates, new Coordinates(x, y));
                IHex newHex = _hexGenerator.GenerateRTTWorldgenHex(hexCoordinates);
                subsector.Hexes.Add(newHex);
            }
        }

        return subsector;
    }
}