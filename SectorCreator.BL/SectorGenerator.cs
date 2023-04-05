using SectorCreator.Global.Enums;
using SectorCreator.Models.Base;
using SectorCreator.Models.Mongoose;
using SectorCreator.Models.RTTWorldgen;
using SectorCreator.Models.StarFrontiers;
using SectorCreator.Models.T5;

namespace TravellerCharacterCreatorBL;

public class SectorGenerator
{
    public Sector GenerateSector(SectorType sectorType)
    {
        return sectorType switch {
            SectorType.Basic or SectorType.SpaceOpera or SectorType.HardScience => new MongooseSector(sectorType),
            SectorType.T5 => new T5Sector(),
            SectorType.RttWorldgen => new RttWorldgenSector(),
            SectorType.StarFrontiers => new StarFrontiersSector(),
            _ => throw new Exception()
        };
    }
}