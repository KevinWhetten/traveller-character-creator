using SectorCreator.Global.Enums;
using SectorCreator.Models.Basic;
using SectorCreator.Models.Basic.Factories;

namespace SectorCreator.BL;

public interface ISectorGenerator
{
    Sector GenerateMongooseSector(SectorType sectorType);
    Sector GenerateT5Sector();
    Sector GenerateRttWorldgenSector();
    Sector GenerateStarFrontiersSector();
}

public class SectorGenerator : ISectorGenerator
{
    private readonly ISectorFactory _sectorFactory;

    public SectorGenerator(ISectorFactory sectorFactory)
    {
        _sectorFactory = sectorFactory;
    }
    
    public Sector GenerateMongooseSector(SectorType sectorType)
    {
        return _sectorFactory.GenerateMongooseSector(sectorType);
    }

    public Sector GenerateT5Sector()
    {
        return _sectorFactory.GenerateT5Sector();
    }

    public Sector GenerateRttWorldgenSector()
    {
        return _sectorFactory.GenerateRttWorldgenSector();
    }

    public Sector GenerateStarFrontiersSector()
    {
        return _sectorFactory.GenerateStarFrontiersSector();
    }
}