using TravellerCreatorGlobalMethods;
using TravellerCreatorModels.SectorCreator.Enums;
using TravellerCreatorModels.SectorCreator.Interfaces;
using TravellerCreatorModels.SectorCreator.RTTWorldgen;
using TravellerCreatorModels.SectorCreator.StarFrontiers;
using TravellerCreatorServices.RTTWorldgen;

namespace TravellerCharacterCreatorBL.SectorCreator;

public class StarGenerator
{
    private readonly RTTWorldgenStarService _rttWorldgenStarService = new();

    public IStar GenerateStarFrontiersStar()
    {
        StarFrontiersStar star = Roll.D10(1) switch {
            1 => new StarFrontiersStar(SpectralType.WD, 0),
            2 => new StarFrontiersStar(SpectralType.M, Roll.D10(1)),
            3 => new StarFrontiersStar(SpectralType.K, Roll.D10(1)),
            4 => new StarFrontiersStar(SpectralType.G, Roll.D10(1)),
            5 => new StarFrontiersStar(SpectralType.F, Roll.D10(1)),
            6 => new StarFrontiersStar(SpectralType.A, Roll.D10(1)),
            7 => new StarFrontiersStar(SpectralType.B, Roll.D10(1)),
            8 => new StarFrontiersStar(SpectralType.O, Roll.D10(1)),
            9 => new StarFrontiersStar(SpectralType.RG, Roll.D10(1)),
            10 => new StarFrontiersStar(SpectralType.SG, 0),
            _ => throw new ArgumentOutOfRangeException()
        };

        return star;
    }

    internal List<IStar> GenerateRTTWorldgenStarSystemStars(RTTWorldgenStarSystemType starSystemType)
    {
        var stars = new List<IStar>();
        int numStars = _rttWorldgenStarService.GetNumStars(starSystemType);

        if (starSystemType == RTTWorldgenStarSystemType.BrownDwarf) {
            stars.Add(new RTTWorldgenStar() {
                SpectralType = SpectralType.D,
                Luminosity = Luminosity.I,
                SpectralSubclass = Roll.D10(1) - 1
            });
            return stars;
        }

        var flag = false;
        for (var i = 0; i < numStars; i++) {
            RTTWorldgenStarType rttWorldgenStarType =
                flag ? RTTWorldgenStarType.Companion : RTTWorldgenStarType.Primary;

            stars.Add(GenerateRTTWorldgenStar(rttWorldgenStarType,
                (flag ? (RTTWorldgenStar) stars.First() : null)));

            flag = true;
        }

        foreach (RTTWorldgenStar star in stars) {
            star.SpectralSubclass = Roll.D10(1) - 1;

            if (star.RTTWorldgenStarType == RTTWorldgenStarType.Companion) {
                star.CompanionOrbit = GenerateOrbit();
            }
        }

        return stars;
    }

    private CompanionOrbit GenerateOrbit()
    {
        return Roll.D6(1) switch {
            <= 2 => CompanionOrbit.Tight,
            <= 4 => CompanionOrbit.Close,
            5 => CompanionOrbit.Moderate,
            6 => CompanionOrbit.Distant,
            _ => CompanionOrbit.None
        };
    }

    private IStar GenerateRTTWorldgenStar(RTTWorldgenStarType starType, RTTWorldgenStar? primaryStar = null)
    {
        var primaryStarLuminosityRoll = 0;
        if (primaryStar != null) {
            primaryStarLuminosityRoll = _rttWorldgenStarService.GetSpectralRoll(primaryStar.SpectralType);
        }
        
        var star = new RTTWorldgenStar {
            SpectralType = _rttWorldgenStarService.GenerateSpectralType(starType, primaryStarLuminosityRoll)
        };
        return _rttWorldgenStarService.FinishStarGeneration(star);
    }
}