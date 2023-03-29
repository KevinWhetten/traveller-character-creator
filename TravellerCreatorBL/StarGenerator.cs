using TravellerCreatorGlobalMethods;
using TravellerCreatorModels.Enums;
using TravellerCreatorModels.Interfaces;
using TravellerCreatorModels.StarFrontiers;

namespace TravellerCharacterCreatorBL;

public class StarGenerator
{
    public IStar GenerateStarFrontiersStar()
    {
        StarFrontiersStar star = Roll.D10(1) switch {
            1 => new StarFrontiersStar(Luminosity.WD, 0),
            2 => new StarFrontiersStar(Luminosity.M, Roll.D10(1)),
            3 => new StarFrontiersStar(Luminosity.K, Roll.D10(1)),
            4 => new StarFrontiersStar(Luminosity.G, Roll.D10(1)),
            5 => new StarFrontiersStar(Luminosity.F, Roll.D10(1)),
            6 => new StarFrontiersStar(Luminosity.A, Roll.D10(1)),
            7 => new StarFrontiersStar(Luminosity.B, Roll.D10(1)),
            8 => new StarFrontiersStar(Luminosity.O, Roll.D10(1)),
            9 => new StarFrontiersStar(Luminosity.RG, Roll.D10(1)),
            10 => new StarFrontiersStar(Luminosity.SG, 0),
            _ => throw new ArgumentOutOfRangeException()
        };

        return star;
    }
}