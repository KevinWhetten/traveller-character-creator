using TravellerCreatorModels.Enums;
using TravellerCreatorModels.Interfaces;
using TravellerCreatorModels.Mongoose;
using TravellerCreatorModels.Other;
using TravellerCreatorModels.RTTWorldgen;
using TravellerCreatorModels.StarFrontiers;

namespace TravellerCharacterCreatorBL;

public class SectorGenerator
{
    private readonly SubsectorGenerator _subsectorGenerator;

    public SectorGenerator()
    {
        _subsectorGenerator = new SubsectorGenerator();
    }

    public ISector GenerateSector(SectorType sectorType)
    {
        return sectorType switch {
            SectorType.Basic or SectorType.SpaceOpera or SectorType.HardScience => GenerateMongooseSector(sectorType),
            SectorType.T5 => GenerateT5Sector(),
            SectorType.RTTWorldgen => GenerateRTTWorldgenSector(),
            SectorType.StarFrontiers => GenerateStarFrontiersSector(),
            _ => throw new Exception()
        };
    }

    private ISector GenerateMongooseSector(SectorType sectorType)
    {
        var sector = new MongooseSector();
        
        for (var y = 1; y <= 4; y++) {
            for (var x = 1; x <= 4; x++) {
                var newSubsector = _subsectorGenerator.GenerateMongooseSubsector(new Coordinates(x, y), sectorType);
                sector.Subsectors.Add(newSubsector);
            }
        }

        return sector;
    }

    private ISector GenerateT5Sector()
    {
        throw new NotImplementedException();
    }

    private ISector GenerateStarFrontiersSector()
    {
        var sector = new StarFrontiersSector();

        for (var y = 1; y <= 4; y++) {
            for (var x = 1; x <= 4; x++) {
                var newSubsector = _subsectorGenerator.GenerateStarFrontiersSubsector(new Coordinates(x, y));
                sector.Subsectors.Add(newSubsector);
            }
        }
        
        return sector;
    }

    private ISector GenerateRTTWorldgenSector()
    {
        var sector = new RTTWorldgenSector();

        for (var y = 1; y <= 4; y++) {
            for (var x = 1; x <= 4; x++) {
                ISubsector newSubsector = _subsectorGenerator.GenerateRTTWorldgenSubsector(new Coordinates(x, y));
                sector.Subsectors.Add(newSubsector);
            }
        }
        
        return sector;
    }
}