using SectorCreator.Global.Enums;
using SectorCreator.WorldBuilder.Enums;
using SectorCreator.WorldBuilder.Planet.AsteroidBelt;

namespace SectorCreator.WorldBuilder.Planet.Planet;

public partial class WorldBuilderPlanet
{
    public int Temperature { get; set; }
    public int HighTemperature { get; set; }
    public int LowTemperature { get; set; }

    public double Albedo { get; set; }
    public double GreenhouseFactor { get; set; }
    protected double AxialTiltFactor => Math.Abs(Math.Sin(AxialTilt));
    private double RotationFactor => Math.Sqrt(SolarDayInHours) / 50.0;

    protected double GeographicFactor => (10 - Hydrographics) / 20.0 + SurfaceDistribution switch {
        SurfaceDistribution.VeryConcentrated or SurfaceDistribution.ExtremelyConcentrated => 0.1,
        SurfaceDistribution.VeryDispersed or SurfaceDistribution.ExtremelyDispersed => -0.1,
        _ => 0
    };

    private double VarianceFactors => AxialTiltFactor + RotationFactor + GeographicFactor;
    protected double AtmosphericFactor => 1 + BAR;
    protected double LuminosityModifier => VarianceFactors / AtmosphericFactor;


    private void GenerateTemperature(WorldBuilderStarSystem starSystem)
    {
        if (PlanetType == PlanetType.AsteroidBelt) {
            ((WorldBuilderAsteroidBelt) this).GenerateTemperature(starSystem);
        } else {
            GenerateAlbedo(starSystem.HZCO);
            GenerateGreenhouseFactor();

            Temperature = (int) (279 * Math.Pow(starSystem.Luminosity * (1 - Albedo) * (1 + GreenhouseFactor) / OrbitDistance, 1.0 / 4.0));

            GenerateRunawayGreenhouseEffect(starSystem);

            if (Temperature < 1) {
                Temperature = 1;
            }
        }
    }

    private void GenerateRunawayGreenhouseEffect(WorldBuilderStarSystem starSystem)
    {
        if (Atmosphere is >= 2 and <= 15 && Temperature > 303) {
            var dm = (int) Math.Ceiling(starSystem.Age);
            if (Temperature > 353) dm += 4;

            if (_rollingService.D6(2) + dm >= 12) {
                RunawayGreenhouse = true;
                if (Atmosphere is 10 or 11 or 12 or >= 15) {
                    while (Temperature < 353) {
                        Temperature = 353;
                    }
                } else {
                    dm = 0;

                    if (Size is >= 2 and <= 5) dm -= 2;
                    if (IsTainted()) dm++;

                    Atmosphere = (_rollingService.D6(1) + dm)switch {
                        <= 1 => 10,
                        <= 4 => 11,
                        >= 5 => 12
                    };
                    GenerateTemperature(starSystem);
                }
            }
        }
    }

    private void GenerateTemperatureCharacteristics(WorldBuilderStarSystem starSystem)
    {
        if (PlanetType != PlanetType.AsteroidBelt) {
            if (Albedo == 0) {
                GenerateAlbedo(starSystem.HZCO);
            }

            if (GreenhouseFactor == 0) {
                GenerateGreenhouseFactor();
            }

            if (!IsTidallyLocked) {
                var highLuminosity = starSystem.Luminosity * (1 + LuminosityModifier);
                var lowLuminosity = starSystem.Luminosity * (1 - LuminosityModifier);

                HighTemperature = (int) (279 * Math.Pow(highLuminosity * (1 - Albedo) * (1 + GreenhouseFactor) / NearAU, 1.0 / 4.0));
                Temperature = (int) (279 * Math.Pow(starSystem.Luminosity * (1 - Albedo) * (1 + GreenhouseFactor) / OrbitDistance, 1.0 / 4.0));
                LowTemperature = (int) (279 * Math.Pow(lowLuminosity * (1 - Albedo) * (1 + GreenhouseFactor) / FarAU, 1.0 / 4.0));
            } else {
                var highTemperatureVarianceFactor = AxialTiltFactor + 1 + GeographicFactor;
                var lowTemperatureVarianceFactor = Math.Max(AxialTiltFactor - 1 + GeographicFactor, 0);

                var highLuminosityModifier = highTemperatureVarianceFactor / AtmosphericFactor;
                var lowLuminosityModifier = lowTemperatureVarianceFactor / AtmosphericFactor;
                var highLuminosity = starSystem.Luminosity * (1 + highLuminosityModifier);
                var lowLuminosity = starSystem.Luminosity * (1 - lowLuminosityModifier);

                HighTemperature = (int) (279 * Math.Pow(highLuminosity * (1 - Albedo) * (1 + GreenhouseFactor) / NearAU, 1.0 / 4.0));
                Temperature = (int) (279 * Math.Pow(starSystem.Luminosity * (1 - Albedo) * (1 + GreenhouseFactor) / OrbitDistance, 1.0 / 4.0));
                LowTemperature = (int) (279 * Math.Pow(lowLuminosity * (1 - Albedo) * (1 + GreenhouseFactor) / FarAU, 1.0 / 4.0));
            }
        }
    }

    protected void GenerateAlbedo(double HZCO)
    {
        Albedo = Density switch {
            > 0.5 => 0.04 + (_rollingService.D6(2) - 2) * 0.02,
            <= .5 => OrbitNumber <= HZCO + 2
                ? 0.2 + (_rollingService.D6(2) - 3) * 0.05
                : 0.25 + (_rollingService.D6(2) - 2) * 0.07
        };

        ModifyAlbedo();

        Albedo = Math.Min(Albedo, 0.98);
        Albedo = Math.Max(Albedo, 0.02);
    }

    private void ModifyAlbedo()
    {
        Albedo += Atmosphere switch {
            (>= 1 and <= 3) or 14 => (_rollingService.D6(2) - 3) * 0.01,
            >= 4 and <= 9 => _rollingService.D6(2) * 0.01,
            (>= 10 and <= 12) or >= 15 => (_rollingService.D6(2) - 2) * 0.05,
            13 => _rollingService.D6(2) * 0.03,
            _ => 0
        };
        Albedo += Hydrographics switch {
            >= 2 and <= 5 => (_rollingService.D6(2) - 3) * 0.02,
            >= 6 => (_rollingService.D6(2) - 4) * 0.03,
            _ => 0
        };
    }

    protected void GenerateGreenhouseFactor()
    {
        GreenhouseFactor = 0.5 * Math.Sqrt(BAR);

        switch (Atmosphere) {
            case (>= 1 and <= 9) or 13 or 14:
                GreenhouseFactor += _rollingService.D6(3) * 0.01;
                break;
            case 10 or 15:
                GreenhouseFactor *= Math.Max(_rollingService.D6(1) - 1, 0.5);
                break;
            case 11 or 12 or 16 or 17:
                var result = _rollingService.D6(1);
                switch (result) {
                    case <= 5:
                        GreenhouseFactor *= result;
                        break;
                    case 6:
                        GreenhouseFactor *= _rollingService.D6(3);
                        break;
                }

                break;
        }
    }
}