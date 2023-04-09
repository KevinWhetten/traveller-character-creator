using SectorCreator.Global;
using SectorCreator.Global.Enums;
using SectorCreator.Models.Basic;

namespace SectorCreator.Models.Mongoose;

public class MongooseSector : Sector
{
    public MongooseSector(SectorType sectorType)
    {
        SectorType = sectorType;
        Generate(sectorType);
    }

    private void Generate(SectorType sectorType)
    {
        for (var y = 1; y <= 4; y++) {
            for (var x = 1; x <= 4; x++) {
                var newSubsector = new MongooseSubsector(new Coordinates(x, y), sectorType);
                Subsectors.Add(newSubsector);
            }
        }
    }
}