using TravellerCreatorGlobalMethods;
using TravellerCreatorModels.Enums;
using TravellerCreatorModels.Interfaces;
using TravellerCreatorModels.Mongoose;
using TravellerCreatorModels.Other;
using TravellerCreatorModels.RTTWorldgen;
using TravellerCreatorModels.StarFrontiers;

namespace TravellerCharacterCreatorBL;

public class HexGenerator
{
    private readonly StarSystemGenerator _starSystemGenerator;

    public HexGenerator()
    {
        _starSystemGenerator = new StarSystemGenerator();
    }

    public IHex GenerateMongooseHex(Coordinates coordinates, SectorType sectorType)
    {
        var hex = new MongooseHex(coordinates);
        hex.StarSystems.Add(_starSystemGenerator.GenerateMongooseStarSystem(sectorType));

        return hex;
    }

    public IHex GenerateStarFrontiersHex(Coordinates hexCoordinates)
    {
        var hex = new StarFrontiersHex(hexCoordinates);
        hex.StarSystems.Add((Roll.D10(1) >= 5
            ? _starSystemGenerator.GenerateStarFrontiersStarSystem()
            : null)!);

        return hex;
    }

    public IHex GenerateRTTWorldgenHex(Coordinates hexCoordinates)
    {
        var hex = new RTTWorldgenHex(hexCoordinates);

        if (Roll.D6(1) >= 4) {
            hex.StarSystems.Add(_starSystemGenerator.GenerateRTTWorldgenStarSystem(RTTWorldgenStarSystemType.BrownDwarf));
        }

        if (Roll.D6(1) >= 4) {
            hex.StarSystems.Add(_starSystemGenerator.GenerateRTTWorldgenStarSystem(RTTWorldgenStarSystemType.Regular));
        }

        foreach (IStarSystem starSystem in hex.StarSystems) {
            foreach (RTTWorldgenStar star in starSystem.Stars.Cast<RTTWorldgenStar>().Where(star => star.StarOrbit == StarOrbit.Distant)) {
                starSystem.Stars.Remove(star);
                hex.StarSystems.Add(_starSystemGenerator.GenerateRTTWorldgenStarSystem(star));
            }
        }

        return hex;
    }
}