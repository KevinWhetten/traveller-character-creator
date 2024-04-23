using SectorCreator.Global.Enums;
using SectorCreator.WorldBuilder.Enums;
using SectorCreator.WorldBuilder.Planet.AsteroidBelt;

namespace SectorCreator.WorldBuilder.Planet.Planet;

public partial class WorldBuilderPlanet
{
    public int BiomassRating { get; set; }
    public int BiocomplexityRating { get; set; }
    public bool CurrentSophontExists { get; set; }
    public bool ExtinctSophontExists { get; set; }
    public int BiodiversityRating { get; set; }
    public int CompatibilityRating { get; set; }
    public int HabitabilityRating { get; set; }

    private void GenerateBiology(WorldBuilderStarSystem starSystem)
    {
        if (PlanetType != PlanetType.AsteroidBelt) {
            if (IsInHabitableZone(starSystem.HZCO) || _rollingService.D6(2) == 12) {
                GenerateBiomassRating(starSystem.Age);
                if (BiomassRating > 1) {
                    GenerateBiocomplexityRating(starSystem.Age);
                    GenerateSophonts(starSystem.Age);
                    GenerateBiodiversityRating();
                    GenerateCompatibilityRating(starSystem.Age);
                }
            }
        }
    }

    private void GenerateBiomassRating(double systemAge)
    {
        var dm = 0;

        dm += Atmosphere switch {
            0 => -6,
            1 => -4,
            2 or 3 or 14 => -3,
            4 or 5 => -2,
            6 or 7 => 0,
            8 or 9 or 13 => 2,
            10 => -3,
            11 => -5,
            12 => -7,
            >= 15 => -5,
            _ => throw new ArgumentOutOfRangeException()
        };
        dm += Hydrographics switch {
            <= 0 => -4,
            <= 3 => -2,
            <= 5 => 0,
            <= 8 => 1,
            >= 9 => 2
        };
        dm += systemAge switch {
            <= 0.2 => -6,
            <= 1 => -2,
            <= 4 => 0,
            > 4 => 1,
            _ => throw new ArgumentOutOfRangeException(nameof(systemAge), systemAge, null)
        };
        dm += HighTemperature switch {
            >= 353 => -2,
            <= 273 => -4,
            _ => 0
        };
        dm += Temperature switch {
            >= 353 => -4,
            <= 273 => -2,
            >= 279 and <= 303 => 2,
            _ => 0
        };

        BiomassRating = Math.Max(_rollingService.D6(2) + dm, 0);

        if (BiomassRating == 0 && AtmosphereTaints.Select(x => x.Taint).Contains(Taint.Biologic)) {
            BiomassRating = 1;
        }

        if (BiomassRating >= 1 && Atmosphere is 0 or 1 or 10 or 11 or 12 or >= 15) {
            BiomassRating += Atmosphere switch {
                0 => 5,
                1 => 3,
                10 => 2,
                11 => 4,
                12 => 6,
                >= 15 => 4,
                _ => throw new ArgumentOutOfRangeException()
            };
        }
    }

    private void GenerateBiocomplexityRating(double starSystemAge)
    {
        var dm = 0;

        if (Atmosphere is < 4 or > 9) dm -= 2;
        if (AtmosphereTaints.Select(x => x.Taint).Contains(Taint.LowOxygen)) dm -= 2;
        dm += starSystemAge switch {
            > 3 and <= 4 => -2,
            > 2 and <= 3 => -4,
            > 1 and <= 2 => -8,
            <= 1 => -10,
            _ => 0
        };

        BiocomplexityRating = Math.Max(_rollingService.Flux() + BiomassRating + dm, 1);
        BiocomplexityRating = Math.Min(BiocomplexityRating, 10);
    }

    private void GenerateSophonts(double starSystemAge)
    {
        if (BiocomplexityRating >= 8 && _rollingService.Flux() + BiocomplexityRating >= 12) {
            CurrentSophontExists = true;
        }

        if (BiocomplexityRating >= 8 && _rollingService.Flux() + BiocomplexityRating + (starSystemAge > 5 ? 1 : 0) >= 12) {
            ExtinctSophontExists = true;
        }
    }


    private void GenerateBiodiversityRating()
    {
        BiodiversityRating = (int) (_rollingService.Flux() + (BiomassRating + BiocomplexityRating) / 2.0);
    }

    private void GenerateHabitability()
    {
        if (PlanetType != PlanetType.AsteroidBelt) {
            var dm = Size switch {
                <= 4 => -1,
                <= 8 => 0,
                >= 9 => 1
            };

            dm += Atmosphere switch {
                0 or 1 or 10 => -8,
                2 or 14 => -4,
                3 or 13 => -3,
                4 or 9 => -2,
                5 or 7 or 8 => -1,
                11 => -10,
                12 or >= 15 => -12,
                _ => 0
            };

            if (AtmosphereTaints.Select(x => x.Taint).Contains(Taint.LowOxygen)) dm -= 2;

            dm += Hydrographics switch {
                <= 0 => -4,
                <= 3 => -2,
                <= 8 => 0,
                9 => -1,
                >= 10 => -2
            };

            if (!IsMoon && IsTidallyLocked) dm -= 2;

            dm += HighTemperature switch {
                <= 279 => -2,
                <= 322 => 0,
                >= 323 => -2
            };

            dm += Temperature switch {
                >= 323 => -4,
                >= 304 and <= 323 => -2,
                <= 273 => -2,
                _ => 0
            };

            if (LowTemperature < 200) dm -= 2;

            dm += Gravity switch {
                <= 0.2 => -4,
                <= 0.7 => -1,
                <= 0.9 => 1,
                <= 1.1 => 0,
                <= 1.4 => -1,
                <= 2.0 => -3,
                >= 2.0 => -6,
                _ => throw new ArgumentOutOfRangeException()
            };

            HabitabilityRating = Math.Max(10 + dm, 0);
        }
    }

    private void GenerateCompatibilityRating(double systemAge)
    {
        var dm = 0;

        dm += Atmosphere switch {
            0 or 1 or 11 or 16 or 17 => -8,
            2 or 4 or 7 or 9 => -2,
            3 or 5 or 8 => 1,
            6 => 2,
            10 or 15 => -6,
            12 => -10,
            13 or 14 => -1,
            _ => throw new ArgumentOutOfRangeException()
        };

        if (systemAge > 8) dm -= 2;

        CompatibilityRating = (int) (_rollingService.D6(2) - (BiocomplexityRating / 2.0) + dm);
    }
}