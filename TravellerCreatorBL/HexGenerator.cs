using TravellerCreatorGlobalMethods;
using TravellerCreatorModels.Basic;
using TravellerCreatorModels.Enums;
using TravellerCreatorModels.Interfaces;
using TravellerCreatorModels.Other;
using TravellerCreatorModels.StarFrontiers;

namespace TravellerCharacterCreatorBL;

public class HexGenerator
{
    private readonly StarSystemGenerator _starSystemGenerator;

    public HexGenerator()
    {
        _starSystemGenerator = new StarSystemGenerator();
    }

    public IHex GenerateBasicHex(Coordinates coordinates, SectorType sectorType)
    {
        var hex = new BasicHex(coordinates);
        hex.StarSystems.Add(_starSystemGenerator.GenerateBasicStarSystem(sectorType));

        return hex;
    }

    public IHex GenerateStarFrontiersHex(Coordinates hexCoordinates)
    {
        var hex = new StarFrontiersHex(hexCoordinates);
        hex.StarSystems.Add((Roll.D10(1) >= 5
            ? _starSystemGenerator.GenerateStarFrontiersSystem()
            : null)!);

        return hex;
    }
}