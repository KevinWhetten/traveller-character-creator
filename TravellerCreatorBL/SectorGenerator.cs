using System.ComponentModel.Design;
using TravellerCharacterCreatorBL.BasicGeneration;
using TravellerCreatorModels;
using TravellerCreatorModels.Basic;

namespace TravellerCharacterCreatorBL;

public class SectorGenerator
{
    private readonly ISubSectorGenerator _subSectorGenerator;

    public SectorGenerator()
    {
        _subSectorGenerator = new SubSectorGenerator();
    }

    public SectorGenerator(ISubSectorGenerator subSectorGenerator)
    {
        _subSectorGenerator = subSectorGenerator;
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
        
        for (var x = 0; x < 6; x++) {
            for (var y = 0; y < 6; y++) {
                SubSector newSubSector = _subSectorGenerator.GenerateSubSector(new Coordinates(x, y));
                sector.SubSectors.Add(newSubSector);
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