using TravellerCreatorGlobalMethods;
using TravellerCreatorModels.SectorCreator.Enums;
using TravellerCreatorModels.SectorCreator.RTTWorldgen;

namespace TravellerCreatorServices.RTTWorldgen;

public class RTTWorldgenStarService
{
    public int GetNumStars(RTTWorldgenStarSystemType starSystemType)
    {
        if (starSystemType == RTTWorldgenStarSystemType.BrownDwarf) {
            return 1;
        }

        int roll = Roll.D6(3);
        return roll switch {
            >= 11 and <= 15 => 2,
            >= 16 => 3,
            _ => 1
        };
    }
    
    public SpectralType GenerateSpectralType(RTTWorldgenStarType starType, int primaryRoll = 0)
    {
        int roll = starType switch {
            RTTWorldgenStarType.Primary => Roll.D6(2),
            RTTWorldgenStarType.Companion => primaryRoll + Roll.D6(1) - 1,
            _ => throw new ArgumentOutOfRangeException(nameof(starType), starType, null)
        };

        return GetSpectralType(roll);
    }

    public SpectralType GetSpectralType(int roll)
    {
        return roll switch {
            2 => SpectralType.A,
            3 => SpectralType.F,
            4 => SpectralType.G,
            5 => SpectralType.K,
            (>= 6 and <= 13) => SpectralType.M,
            (>= 14) => SpectralType.L,
            _ => SpectralType.D
        };
    }

    public int GetSpectralRoll(SpectralType primaryStarSpectralType)
    {
        return primaryStarSpectralType switch {
            SpectralType.A => 2,
            SpectralType.F => 3,
            SpectralType.G => 4,
            SpectralType.K => 5,
            SpectralType.M => 10,
            SpectralType.L => 14,
            SpectralType.D => 0,
            _ => throw new ArgumentOutOfRangeException()
        };
    }

    public RTTWorldgenStar FinishStarGeneration(RTTWorldgenStar star, bool isCompanion = false)
    {
        star.Age = GetAge();

        if (star.SpectralType == SpectralType.A) {
            star = FinishATypeGeneration(star);
        } else if (star.SpectralType == SpectralType.F) {
            star = FinishFTypeGeneration(star);
        } else if (star.SpectralType == SpectralType.G) {
            star = FinishGTypeGeneration(star);
        } else if (star.SpectralType == SpectralType.K) {
            star.Luminosity = Luminosity.V;
        } else if (star.SpectralType == SpectralType.M) {
            star = FinishMTypeGeneration(star, isCompanion);
        }

        if (star.SpectralType == SpectralType.D || star.Luminosity == Luminosity.III) {
            star.ExpansionSize = Roll.D6(1);
        }

        return star;
    }

    private RTTWorldgenStar FinishMTypeGeneration(RTTWorldgenStar star, bool isCompanion)
    {
        switch (Roll.D6(2) + (isCompanion ? 2 : 0)) {
            case <= 9:
                star.Luminosity = Luminosity.V;
                break;
            case <= 12:
                star.Luminosity = Luminosity.Ve;
                break;
            default:
                star.SpectralType = SpectralType.L;
                break;
        }

        return star;
    }

    private RTTWorldgenStar FinishGTypeGeneration(RTTWorldgenStar star)
    {
        if (star.Age <= 2) {
            star.Luminosity = Luminosity.V;
        } else {
            var roll = Roll.D6(1);
            switch (roll) {
                case <= 2:
                    star.SpectralType = SpectralType.K;
                    star.Luminosity = Luminosity.IV;
                    break;
                case 3:
                    star.SpectralType = SpectralType.M;
                    star.Luminosity = Luminosity.III;
                    break;
                case <= 6:
                    star.SpectralType = SpectralType.D;
                    break;
            }
        }

        return star;
    }

    private RTTWorldgenStar FinishFTypeGeneration(RTTWorldgenStar star)
    {
        if (star.Age <= 2) {
            star.Luminosity = Luminosity.V;
        } else {
            var roll = Roll.D6(1);
            switch (roll) {
                case <= 2:
                    star.SpectralType = SpectralType.G;
                    star.Luminosity = Luminosity.IV;
                    break;
                case 3:
                    star.SpectralType = SpectralType.M;
                    star.Luminosity = Luminosity.III;
                    break;
                case <= 6:
                    star.SpectralType = SpectralType.D;
                    break;
            }
        }

        return star;
    }

    private RTTWorldgenStar FinishATypeGeneration(RTTWorldgenStar star)
    {
        if (star.Age <= 2) {
            star.Luminosity = Luminosity.V;
        } else {
            var roll = Roll.D6(1);
            switch (roll) {
                case <= 2:
                    star.SpectralType = SpectralType.F;
                    star.Luminosity = Luminosity.IV;
                    break;
                case 3:
                    star.SpectralType = SpectralType.K;
                    star.Luminosity = Luminosity.III;
                    break;
                case <= 6:
                    star.SpectralType = SpectralType.D;
                    break;
            }
        }

        return star;
    }

    private int GetAge()
    {
        return Roll.D6(3) - 3;
    }
}