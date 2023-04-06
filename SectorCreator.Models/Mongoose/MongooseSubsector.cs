using SectorCreator.Global;
using SectorCreator.Global.Enums;
using SectorCreator.Models.Basic;

namespace SectorCreator.Models.Mongoose;

public class MongooseSubsector : Subsector
{
    public MongooseSubsector(Coordinates coordinates, SectorType sectorType)
    {
        Coordinates = coordinates;
        Hexes = new List<Hex>();

        for (var y = 1; y <= 10; y++) {
            for (var x = 1; x <= 8; x++) {
                Hex newHex = new MongooseHex(new Coordinates(x, y), coordinates, sectorType);
                Hexes.Add(newHex);
            }
        }
    }
}