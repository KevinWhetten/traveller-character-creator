using SectorCreator.Global;

namespace SectorCreator.RttWorldgen.Models;

public class Sector : SectorCreator.Models.Basic.Sector
{
    Sector()
    {
        for (int i = 0; i < 4; i++) {
            for (int j = 0; j < 4; j++) {
                Subsectors.Add(new Subsector(new Coordinates(i, j)));
            }
        }
    }
}