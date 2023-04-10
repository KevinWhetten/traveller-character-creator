using SectorCreator.Global;
using SectorCreator.Global.Enums;
using SectorCreator.Models.Basic;
using SectorCreator.Models.RTTWorldgen;

namespace SectorCreator.Models.Factories;

public interface ISectorFactory
{
    Sector GenerateMongooseSector(SectorType sectorType);
    Sector GenerateT5Sector();
    Sector GenerateRttWorldgenSector();
    Sector GenerateStarFrontiersSector();
}

public class SectorFactory : ISectorFactory
{
    private readonly ISubsectorFactory _subsectorFactory;

    public SectorFactory(ISubsectorFactory subsectorFactory)
    {
        _subsectorFactory = subsectorFactory;
    }

    public Sector GenerateMongooseSector(SectorType sectorType)
    {
        var sector = new Sector();

        for (var y = 1; y <= 4; y++) {
            for (var x = 1; x <= 4; x++) {
                var newSubsector = _subsectorFactory.GenerateMongooseSubsector(sectorType, new Coordinates(x, y));
                sector.Subsectors.Add(newSubsector);
            }
        }

        return sector;
    }

    public Sector GenerateT5Sector()
    {
        var sector = new Sector();

        for (var y = 1; y <= 4; y++) {
            for (var x = 1; x <= 4; x++) {
                var newSubsector = _subsectorFactory.GenerateT5Subsector(new Coordinates(x, y));
                sector.Subsectors.Add(newSubsector);
            }
        }

        return sector;
    }

    public Sector GenerateRttWorldgenSector()
    {
        var sector = new Sector();
        var homeworlds = new List<RttWorldgenPlanet>();

        for (var y = 1; y <= 4; y++) {
            for (var x = 1; x <= 4; x++) {
                var newSubsector = _subsectorFactory.GenerateRttWorldgenSubsector(new Coordinates(x, y));
                homeworlds.AddRange(from hex in newSubsector.Hexes
                    from starSystem in hex.StarSystems
                    from RttWorldgenPlanet planet in starSystem.Planets
                    where planet.Biosphere >= 12
                    select planet);
                sector.Subsectors.Add(newSubsector);
            }
        }

        foreach (var unused in homeworlds) { }

        return sector;
    }

    public Sector GenerateStarFrontiersSector()
    {
        var sector = new Sector();

        for (var y = 1; y <= 4; y++) {
            for (var x = 1; x <= 4; x++) {
                var newSubsector = _subsectorFactory.GenerateStarFrontiersSubsector(new Coordinates(x, y));
                sector.Subsectors.Add(newSubsector);
            }
        }

        return sector;
    }
}