using SectorCreator.Global;

namespace SectorCreator.WorldBuilder.Services;

public static class EccentricityService
{
    private static IRollingService _rollingService = new RollingService();

    public static double CalculateEccentricity(int dm)
    {
        return (_rollingService.D6(2) + dm) switch {
            <= 5 => -0.001 + _rollingService.D6(1) / 1000.0,
            <= 7 => 0.00 + _rollingService.D6(1) / 200.0,
            <= 9 => 0.03 + _rollingService.D6(1) / 100.0,
            10 => 0.05 + _rollingService.D6(1) / 20.0,
            11 => 0.05 + _rollingService.D6(2) / 20.0,
            >= 12 => 0.30 + _rollingService.D6(2) / 20.0
        };
    }
}