using SectorCreator.Global;
using SectorCreator.Global.Enums;
using SectorCreator.Models.Base;

namespace SectorCreator.Models.Mongoose;

public class MongooseHex: Hex
{
    public MongooseHex(Coordinates hexCoordinates, Coordinates subsectorCoordinates, SectorType sectorType)
    {
        SetCoordinates(subsectorCoordinates, hexCoordinates);
        StarSystems.Add(new MongooseStarSystem(sectorType));
    }
}