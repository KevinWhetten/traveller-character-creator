using SectorCreator.Global;
using SectorCreator.Global.Enums;
using SectorCreator.Models.Basic;

namespace SectorCreator.Models.Factories.Basic;

public interface ISubsectorFactory
{
    Subsector GenerateMongooseSubsector(SectorType sectorType, Coordinates coordinates);
    Subsector GenerateT5Subsector(Coordinates coordinates);
    Subsector GenerateRttWorldgenSubsector(Coordinates coordinates);
    Subsector GenerateStarFrontiersSubsector(Coordinates coordinates);
}

public class SubsectorFactory : ISubsectorFactory
{
    private readonly IHexFactory _hexFactory;

    public SubsectorFactory(IHexFactory hexFactory)
    {
        _hexFactory = hexFactory;
    }
    
    public Subsector GenerateMongooseSubsector(SectorType sectorType, Coordinates coordinates)
    {
        var subsector = new Subsector {
            Coordinates = coordinates,
            Hexes = new List<Hex>()
        };

        for (var y = 1; y <= 10; y++) {
            for (var x = 1; x <= 8; x++) {
                var newHex = _hexFactory.GenerateMongooseHex(subsector.Coordinates, new Coordinates(x, y), sectorType);
                subsector.Hexes.Add(newHex);
            }
        }

        return subsector;
    }

    public Subsector GenerateT5Subsector(Coordinates coordinates)
    {
        var subsector = new Subsector {
            Coordinates = coordinates,
            Hexes = new List<Hex>()
        };

        for (var y = 1; y <= 10; y++) {
            for (var x = 1; x <= 8; x++) {
                var newHex = _hexFactory.GenerateT5Hex(subsector.Coordinates, new Coordinates(x, y));
                subsector.Hexes.Add(newHex);
            }
        }

        return subsector;
    }

    public Subsector GenerateRttWorldgenSubsector(Coordinates coordinates)
    {
        var subsector = new Subsector {
            Coordinates = coordinates,
            Hexes = new List<Hex>()
        };

        for (var y = 1; y <= 10; y++) {
            for (var x = 1; x <= 8; x++) {
                var newHex = _hexFactory.GenerateRttWorldgenHex(subsector.Coordinates, new Coordinates(x, y));
                subsector.Hexes.Add(newHex);
            }
        }

        return subsector;
    }

    public Subsector GenerateStarFrontiersSubsector(Coordinates coordinates)
    {
        var subsector = new Subsector {
            Coordinates = coordinates,
            Hexes = new List<Hex>()
        };

        for (var y = 1; y <= 10; y++)
        {
            for (var x = 1; x <= 8; x++) {
                var newHex = _hexFactory.GenerateStarFrontiersHex(subsector.Coordinates, new Coordinates(x, y));
                subsector.Hexes.Add(newHex);
            }
        }

        return subsector;
    }
}