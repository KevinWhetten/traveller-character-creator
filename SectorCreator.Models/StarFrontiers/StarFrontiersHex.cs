using SectorCreator.Global;
using SectorCreator.Models.Basic;

namespace SectorCreator.Models.StarFrontiers;

public class StarFrontiersHex : Hex
{
    public StarFrontiersHex(Coordinates subsectorCoordinates, Coordinates hexCoordinates)
    {
        SetCoordinates(subsectorCoordinates, hexCoordinates);
        StarSystems.Add((Roll.D10(1) >= 5
            ? new StarFrontiersStarSystem()
            : null)!);
    }
}