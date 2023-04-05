using TravellerCreatorModels.SectorCreator.Enums;
using TravellerCreatorModels.SectorCreator.Interfaces;
using TravellerCreatorModels.SectorCreator.Mongoose;
using TravellerCreatorModels.SectorCreator.Other;
using TravellerCreatorModels.SectorCreator.RTTWorldgen;
using TravellerCreatorModels.SectorCreator.StarFrontiers;

namespace TravellerCharacterCreatorBL.SectorCreator;

public class SectorGenerator
{
    private readonly SubsectorGenerator _subsectorGenerator;
    private readonly PlanetGenerator _planetGenerator;

    public SectorGenerator()
    {
        _planetGenerator = new PlanetGenerator();
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
        var homeworlds = new List<RTTWorldgenPlanet>();

        for (var y = 1; y <= 4; y++) {
            for (var x = 1; x <= 4; x++) {
                ISubsector newSubsector = _subsectorGenerator.GenerateRTTWorldgenSubsector(new Coordinates(x, y));
                homeworlds.AddRange(from hex in newSubsector.Hexes from starSystem in hex.StarSystems from RTTWorldgenPlanet planet in starSystem.Planets where planet.Biosphere >= 12 select planet);
                sector.Subsectors.Add(newSubsector);
            }
        }

        foreach (var homeworld in homeworlds) {
            _planetGenerator.GenerateSettlement(homeworld);
        }

        return sector;
    }
}