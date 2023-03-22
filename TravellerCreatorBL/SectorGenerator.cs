using TravellerCharacterCreatorBL.BasicGeneration;
using TravellerCreatorModels;
using TravellerCreatorModels.Basic;

namespace TravellerCharacterCreatorBL;

public class SectorGenerator
{
    private readonly ISubsectorGenerator _subsectorGenerator;

    public SectorGenerator()
    {
        _subsectorGenerator = new SubsectorGenerator();
    }

    public SectorGenerator(ISubsectorGenerator subsectorGenerator)
    {
        _subsectorGenerator = subsectorGenerator;
    }

    public Sector GenerateSector(SectorType sectorType)
    {
        return sectorType switch {
            SectorType.Basic => GenerateBasicSector(),
            SectorType.SpaceOpera => GenerateSpaceOperaSector(),
            SectorType.HardScience => GenerateHardScienceSector(),
            SectorType.SecondSurvey => GenerateSecondSurveySector(),
            SectorType.RTTWorldgen => GenerateRTTWorldgenSector(),
            _ => throw new Exception()
        };
    }

    private Sector GenerateBasicSector()
    {
        var sector = new Sector();
        
        for (var x = 1; x <= 4; x++) {
            for (var y = 1; y <= 4; y++) {
                var newSubsector = _subsectorGenerator.GenerateSubsector(new Coordinates(x, y));
                sector.Subsectors.Add(newSubsector);
            }
        }

        return sector;
    }

    private Sector GenerateSpaceOperaSector()
    {
        throw new NotImplementedException();
    }

    private Sector GenerateHardScienceSector()
    {
        throw new NotImplementedException();
    }

    private Sector GenerateSecondSurveySector()
    {
        throw new NotImplementedException();
    }

    private Sector GenerateRTTWorldgenSector()
    {
        throw new NotImplementedException();
    }
}