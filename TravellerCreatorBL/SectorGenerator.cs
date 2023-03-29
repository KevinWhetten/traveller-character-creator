using TravellerCreatorModels.Basic;
using TravellerCreatorModels.Enums;
using TravellerCreatorModels.Interfaces;
using TravellerCreatorModels.Other;
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
            SectorType.Basic or SectorType.SpaceOpera or SectorType.HardScience => GenerateBasicSector(sectorType),
            SectorType.StarFrontiers => GenerateStarFrontiersSector(),
            SectorType.SecondSurvey => GenerateT5Sector(),
            SectorType.RTTWorldgen => GenerateRTTWorldgenSector(),
            _ => throw new Exception()
        };
    }

    private ISector GenerateBasicSector(SectorType sectorType)
    {
        var sector = new BasicSector();
        
        for (var y = 1; y <= 4; y++) {
            for (var x = 1; x <= 4; x++) {
                var newSubsector = _subsectorGenerator.GenerateBasicSubsector(new Coordinates(x, y), sectorType);
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
        throw new NotImplementedException();
    }
}