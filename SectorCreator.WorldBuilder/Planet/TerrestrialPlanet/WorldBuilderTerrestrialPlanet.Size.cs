using SectorCreator.WorldBuilder.Enums;

namespace SectorCreator.WorldBuilder.Planet.TerrestrialPlanet;

public partial class WorldBuilderTerrestrialPlanet
{
    public Composition Composition { get; set; }
    
    public void GenerateSizeCharacteristics(WorldBuilderStarSystem starSystem)
    {
        switch (_rollingService.D6(1)) {
            case <= 2:
                Size = _rollingService.D6(1);
                GenerateDiameter();
                GenerateComposition(starSystem);
                GenerateDensity();
                break;
            case <= 4:
                Size = _rollingService.D6(2);
                GenerateDiameter();
                GenerateComposition(starSystem);
                GenerateDensity();
                break;
            default:
                Size = _rollingService.D6(2) + 3;
                GenerateDiameter();
                GenerateComposition(starSystem);
                GenerateDensity();
                break;
        }
    }
    
    public void GenerateComposition(WorldBuilderStarSystem starSystem, Composition composition = Composition.None)
    {
        var compositionDM = CalculateCompositionDM(starSystem);
    
        if (composition == Composition.None) {
            Composition = (_rollingService.D6(2) + compositionDM) switch {
                <= -4 => Composition.ExoticIce,
                <= 2 => Composition.MostlyIce,
                <= 6 => Composition.MostlyRock,
                <= 11 => Composition.RockAndMetal,
                <= 14 => Composition.MostlyMetal,
                >= 15 => Composition.CompressedMetal
            };
        } else {
            Composition = composition;
        }
    }
    
    private int CalculateCompositionDM(WorldBuilderStarSystem starSystem)
    {
        var compositionDM = Size switch {
            <= 4 => -1,
            <= 5 => 0,
            <= 9 => 1,
            >= 10 => 3
        };
    
        compositionDM += OrbitNumber <= starSystem.Star.HZCO ? 1 : 0;
    
        if (OrbitNumber > starSystem.Star.HZCO) {
            compositionDM--;
            compositionDM -= (int) Math.Floor((OrbitNumber - starSystem.Star.HZCO) / starSystem.Spread);
        }
    
        if (starSystem.Age > 10) {
            compositionDM++;
        }
    
        return compositionDM;
    }

    protected void GenerateDensity()
    {
        Density = Composition switch {
            Composition.ExoticIce => _rollingService.D6(2) switch {
                2 => 0.03,
                3 => 0.06,
                4 => 0.09,
                5 => 0.12,
                6 => 0.15,
                7 => 0.18,
                8 => 0.21,
                9 => 0.24,
                10 => 0.27,
                11 => 0.30,
                12 => 0.33,
                _ => throw new ArgumentOutOfRangeException()
            },
            Composition.MostlyIce => _rollingService.D6(2) switch {
                2 => 0.18,
                3 => 0.21,
                4 => 0.24,
                5 => 0.27,
                6 => 0.30,
                7 => 0.33,
                8 => 0.36,
                9 => 0.39,
                10 => 0.41,
                11 => 0.44,
                12 => 0.47,
                _ => throw new ArgumentOutOfRangeException()
            },
            Composition.MostlyRock => _rollingService.D6(2) switch {
                2 => 0.50,
                3 => 0.53,
                4 => 0.56,
                5 => 0.59,
                6 => 0.62,
                7 => 0.65,
                8 => 0.68,
                9 => 0.71,
                10 => 0.74,
                11 => 0.77,
                12 => 0.80,
                _ => throw new ArgumentOutOfRangeException()
            },
            Composition.RockAndMetal => _rollingService.D6(2) switch {
                2 => 0.82,
                3 => 0.85,
                4 => 0.88,
                5 => 0.91,
                6 => 0.94,
                7 => 0.97,
                8 => 1.00,
                9 => 1.03,
                10 => 1.06,
                11 => 1.09,
                12 => 1.12,
                _ => throw new ArgumentOutOfRangeException()
            },
            Composition.MostlyMetal => _rollingService.D6(2) switch {
                2 => 1.15,
                3 => 1.18,
                4 => 1.21,
                5 => 1.24,
                6 => 1.27,
                7 => 1.30,
                8 => 1.33,
                9 => 1.36,
                10 => 1.39,
                11 => 1.42,
                12 => 1.45,
                _ => throw new ArgumentOutOfRangeException()
            },
            Composition.CompressedMetal => _rollingService.D6(2) switch {
                2 => 1.50,
                3 => 1.55,
                4 => 1.60,
                5 => 1.65,
                6 => 1.70,
                7 => 1.75,
                8 => 1.80,
                9 => 1.85,
                10 => 1.90,
                11 => 1.95,
                12 => 2.00,
                _ => throw new ArgumentOutOfRangeException()
            },
            _ => throw new ArgumentOutOfRangeException()
        };
    }
}