using SectorCreator.Global;
using SectorCreator.Global.Enums;
using SectorCreator.Models.Base;

namespace SectorCreator.Models.RTTWorldgen;

public class RttWorldgenSector : Sector
{
    public RttWorldgenSector()
    {
        SectorType = SectorType.RttWorldgen;
        Generate();
    }

    private void Generate()
    {
        var homeworlds = new List<RttWorldgenPlanet>();

        for (var y = 1; y <= 4; y++) {
            for (var x = 1; x <= 4; x++) {
                var newSubsector = new RttWorldgenSubsector();
                newSubsector.Generate(new Coordinates(x, y));
                homeworlds.AddRange(from hex in newSubsector.Hexes from starSystem in hex.StarSystems from RttWorldgenPlanet planet in starSystem.Planets where planet.Biosphere >= 12 select planet);
                Subsectors.Add(newSubsector);
            }
        }
        
        foreach(var unused in homeworlds){}
    }
}