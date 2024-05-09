using SectorCreator.Global.Enums;
using SectorCreator.WorldBuilder.Enums;
using SectorCreator.WorldBuilder.Planet.Planet.OtherObjects;
using SectorCreator.WorldBuilder.Planet.TerrestrialPlanet;
using SectorCreator.WorldBuilder.Services;

namespace SectorCreator.WorldBuilder.Planet.Planet;

public partial class WorldBuilderPlanet
{
    public int Hydrographics { get; set; }
    public double HydroPercent { get; set; }
    public Geography FundamentalGeography { get; set; }
    public List<PlanetElement> LiquidChemicals { get; set; } = new();
    public SurfaceDistribution SurfaceDistribution { get; set; }
    public int MajorContinents { get; set; }
    public int MinorContinents { get; set; }


    private void GenerateHydrographicsDetails(WorldBuilderStarSystem starSystem)
    {
        if (PlanetType != PlanetType.AsteroidBelt) {
            GenerateHydrographics(starSystem);
            GenerateHydroPercent();
            GenerateSurfaceDistribution();
            GenerateContinents();
        }
    }

    protected void GenerateHydrographics(WorldBuilderStarSystem starSystem)
    {
        if (Size <= 1) {
            Hydrographics = 0;
        } else {
            var dm = 0;

            if (Atmosphere is 0 or 1 or 10) dm -= 4;
            if (OrbitNumber < starSystem.HZCO - 1.0) {
                dm -= 6;
            } else if (OrbitNumber < starSystem.HZCO - 0.5) {
                dm -= 2;
            }

            Hydrographics = Math.Min(_rollingService.Flux() + Atmosphere + dm, 10);
            Hydrographics = Math.Max(Hydrographics, 0);
        }
    }
    
    private void GenerateHydroPercent()
    {
        HydroPercent = Hydrographics switch {
            0 => _rollingService.D(5, 1),
            <= 9 => _rollingService.D10(1) + (Hydrographics * 10 - 5),
            10 => 95 + _rollingService.D(5, 1),
            _ => throw new ArgumentOutOfRangeException()
        };
    }
    
    private void GenerateSurfaceDistribution()
    {
        FundamentalGeography = (_rollingService.D6(1) + (Hydrographics - 5)) switch {
            <= 3 => Geography.Land,
            >= 4 => Geography.Water
        };
    
        SurfaceDistribution = (_rollingService.D6(2) - 2) switch {
            0 => SurfaceDistribution.ExtremelyDispersed,
            1 => SurfaceDistribution.VeryDispersed,
            2 => SurfaceDistribution.Dispersed,
            3 => SurfaceDistribution.Scattered,
            4 => SurfaceDistribution.SlightlyScattered,
            5 => SurfaceDistribution.Mixed,
            6 => SurfaceDistribution.SlightlySkewed,
            7 => SurfaceDistribution.Skewed,
            8 => SurfaceDistribution.Concentrated,
            9 => SurfaceDistribution.VeryConcentrated,
            10 => SurfaceDistribution.ExtremelyConcentrated,
            _ => throw new ArgumentOutOfRangeException()
        };
    }
    
    private void GenerateContinents()
    {
        var landDistribution = SurfaceDistribution switch {
            SurfaceDistribution.ExtremelyDispersed => 0.0,
            SurfaceDistribution.VeryDispersed => 0.1,
            SurfaceDistribution.Dispersed => 0.2,
            SurfaceDistribution.Scattered => 0.3,
            SurfaceDistribution.SlightlyScattered => 0.4,
            SurfaceDistribution.Mixed => 0.6,
            SurfaceDistribution.SlightlySkewed => 0.7,
            SurfaceDistribution.Skewed => 0.8,
            SurfaceDistribution.Concentrated => 0.9,
            SurfaceDistribution.VeryConcentrated => 0.95,
            SurfaceDistribution.ExtremelyConcentrated => 1.0,
            _ => throw new ArgumentOutOfRangeException()
        };
    
        var maxMajorBodies = (int) Math.Floor(((100 - HydroPercent) * landDistribution) / 5.0);
        var maxMinorBodies = (int) Math.Floor((100 - HydroPercent) * (1 - landDistribution));
    
        var majorFactor = maxMajorBodies switch {
            <= 2 => 1,
            <= 4 => 2,
            <= 6 => 3,
            <= 8 => 4,
            _ => 5
        };
        var minorFactor = maxMinorBodies switch {
            <= 3 => 1,
            <= 8 => 2,
            <= 15 => 3,
            <= 32 => 4,
            _ => 5
        };
    
        MajorContinents = _rollingService.D((int) Math.Floor((double) (maxMajorBodies + majorFactor) / majorFactor), majorFactor) -
                          majorFactor;
        MinorContinents = _rollingService.D((int) Math.Floor((double) (maxMinorBodies + minorFactor) / minorFactor), minorFactor) -
                          minorFactor;
    }

    private void GenerateLiquidMakeup()
    {
        var liquidAbundanceTotal = LiquidChemicals.Select(x => x.Element).Sum(x => x.RelativeAbundance);

        foreach (var planetElement in LiquidChemicals) {
            planetElement.Percent =
                _rollingService.ValueWithVariance(((double) planetElement.Element.RelativeAbundance / liquidAbundanceTotal) * 100, 5);
        }

        if (LiquidChemicals.Count == 0) {
            LiquidChemicals.Add(new PlanetElement {Element = CompositionService.GetRandomElement(), Percent = _rollingService.Percent() * .75});
            LiquidChemicals.Add(new PlanetElement {Element = CompositionService.GetRandomElement(), Percent = _rollingService.Percent() * .5});
            LiquidChemicals.Add(new PlanetElement {Element = CompositionService.GetRandomElement(), Percent = _rollingService.Percent() * .33});
        }

        LiquidChemicals = LiquidChemicals.OrderByDescending(x => x.Percent).ToList();

        var percentDifference = 100 - LiquidChemicals.Sum(x => x.Percent);
        LiquidChemicals.First().Percent += percentDifference;

        LiquidChemicals = LiquidChemicals.OrderByDescending(x => x.Percent).ToList();
    }
}