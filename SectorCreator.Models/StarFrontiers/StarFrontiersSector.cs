﻿using SectorCreator.Global;
using SectorCreator.Global.Enums;
using SectorCreator.Models.Basic;

namespace SectorCreator.Models.StarFrontiers;

public class StarFrontiersSector : Sector
{
    public StarFrontiersSector()
    {
        SectorType = SectorType.StarFrontiers;
        
        for (var y = 1; y <= 4; y++) {
            for (var x = 1; x <= 4; x++) {
                var newSubsector = new StarFrontiersSubsector(new Coordinates(x, y));
                Subsectors.Add(newSubsector);
            }
        }
    }
}