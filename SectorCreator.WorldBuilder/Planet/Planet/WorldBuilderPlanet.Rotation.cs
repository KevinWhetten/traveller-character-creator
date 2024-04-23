using SectorCreator.Global.Enums;
using SectorCreator.WorldBuilder.Planet.Moon;
using SectorCreator.WorldBuilder.Services;

namespace SectorCreator.WorldBuilder.Planet.Planet;

public partial class WorldBuilderPlanet
{
    public double SiderealDay { get; set; }
    protected double SolarDaysInLocalYear => ((Period * 365.25 * 24) / SiderealDay) - 1.0;
    protected double SolarDayInHours => ((Period * 365.25 * 24) / SolarDaysInLocalYear);

    public double TidalForce { get; set; }
    public double AxialTilt { get; set; }
    public bool IsTidallyLocked { get; set; }
    public bool IsTidallyLockedWithMoon { get; set; }

    public double StarTidalEffect { get; set; }
    public double MoonTidalEffect { get; set; }

    private void GenerateBasicRotationDetails(WorldBuilderStarSystem starSystem)
    {
        if (PlanetType != PlanetType.AsteroidBelt) {
            GenerateBasicRotation(starSystem.Age);
            AxialTilt = GenerateAxialTilt();
        }
    }

    protected void GenerateBasicRotation(double starSystemAge)
    {
        var dm = (int) Math.Floor(starSystemAge / 2);

        SiderealDay += (_rollingService.D6(2) - 2) * (Size is 0 or 26 or 16 or 17 or 18 ? 2 : 4) + 2 + _rollingService.D6(1) + dm;

        if (SiderealDay >= 40 && _rollingService.D6(1) >= 5) {
            GenerateBasicRotation(starSystemAge);
        }
    }

    protected double GenerateAxialTilt()
    {
        return _rollingService.D6(2) switch {
            <= 4 => (_rollingService.D6(1) - 1) / 50.0,
            5 => _rollingService.D6(1) / 5.0,
            6 => _rollingService.D6(1),
            7 => 6 + _rollingService.D6(1),
            <= 9 => 5 + (_rollingService.D6(1) * 5),
            >= 10 => _rollingService.D6(1) switch {
                <= 2 => 10 + _rollingService.D6(1) * 10,
                3 => 30 + _rollingService.D6(1) * 10,
                4 => 90 + _rollingService.D6(1) * _rollingService.D6(1),
                5 => 180 - _rollingService.D6(1) * _rollingService.D6(1),
                6 => 120 + _rollingService.D6(1) * 10,
                _ => throw new ArgumentOutOfRangeException()
            }
        };
    }

    private void GenerateTidalForces(WorldBuilderStarSystem starSystem)
    {
        if (PlanetType != PlanetType.AsteroidBelt) {
            GenerateTidalForce(starSystem);
            GenerateTidalLockStatus(starSystem);
            CalculateTidalEffects(starSystem);
        }
    }

    private void GenerateTidalForce(WorldBuilderStarSystem starSystem)
    {
        TidalForce = (starSystem.Mass * Diameter) / Math.Pow(OrbitDistance, 3);
    }

    private void GenerateTidalLockStatus(WorldBuilderStarSystem starSystem)
    {
        var dm = GetBasicTidalLockDM(starSystem);

        dm -= 4;

        if (OrbitNumber < 1) dm += (int) Math.Floor(4 + 10 * (1 - OrbitNumber));
        if (OrbitNumber is >= 1 and < 2) dm += 4;
        if (OrbitNumber is >= 2 and < 3) dm += 1;
        if (OrbitNumber >= 3) dm -= (int) Math.Floor(OrbitNumber) * 2;

        if (starSystem.Mass < 0.5) dm -= 2;
        if (starSystem.Mass is >= 0.5 and < 1.0) dm -= 1;
        if (starSystem.Mass is >= 2 and < 5) dm += 1;
        if (starSystem.Mass is >= 5) dm += 2;

        if (starSystem.Star.CompanionStar != null) dm -= 1;
        if (Moons.Count(x => x.Size is >= 1 and <= 18) > 0) dm += Moons.Where(x => x.Size is >= 1 and <= 18).Sum(x => x.Size);

        CalculateTidalLockStatus(dm);

        foreach (var moon in Moons) {
            moon.GenerateTidalLockStatus(this, starSystem);
        }

        if (Moons.Any(x => x is {IsTidallyLocked: true, Size: >= 1 and <= 15}) && !IsTidallyLocked) {
            foreach (var moon in Moons.Where(x => x is {IsTidallyLocked: true, Size: >= 1 and <= 15})) {
                GenerateTidalLockStatusWithMoon(starSystem, moon);
            }
        }
    }

    protected int GetBasicTidalLockDM(WorldBuilderStarSystem starSystem)
    {
        var dm = 0;

        if (Size >= 1) dm += (int) Math.Ceiling(Size / 3.0);

        if (Eccentricity > 0.1) dm -= (int) Math.Floor(Eccentricity * 10.0);

        if (AxialTilt is > 30 and < 60) dm -= 2;
        if (AxialTilt is >= 60 and < 120) dm -= 4;
        if (AxialTilt is >= 80 and < 100) dm -= 4;

        if (BAR > 2.5) dm -= 2;

        if (starSystem.Age < 1) dm -= 2;
        if (starSystem.Age is >= 5 and < 10) dm += 2;
        if (starSystem.Age >= 10) dm += 4;
        return dm;
    }

    private void GenerateTidalLockStatusWithMoon(WorldBuilderStarSystem starSystem, WorldBuilderMoon moon)
    {
        int dm;
        dm = GetBasicTidalLockDM(starSystem);

        dm -= 10;

        if (moon.Size >= 1) dm += moon.Size;
        if (moon.OrbitDistanceInDiamters < 5) dm += (int) Math.Ceiling(5 + (5 - moon.OrbitDistanceInDiamters) * 5);
        if (moon.OrbitDistanceInDiamters is >= 5 and < 10) dm += 4;
        if (moon.OrbitDistanceInDiamters is >= 10 and < 20) dm += 2;
        if (moon.OrbitDistanceInDiamters is >= 20 and < 40) dm += 1;
        if (moon.OrbitDistanceInDiamters > 60) dm -= 6;
        dm -= 2 * (Moons.Count - 1);

        if (dm > -10) {
            if (dm >= 10) {
                SiderealDay = moon.Period;
                if (_rollingService.D6(2) == 12) {
                    GenerateTidalLockStatusWithMoon(starSystem, moon);
                }

                return;
            }

            CalculateMoonTidalLockStatus(dm, moon);
        }
    }

    private void CalculateMoonTidalLockStatus(int dm, WorldBuilderMoon moon)
    {
        switch (_rollingService.D6(2) + dm) {
            case <= 2:
                break;
            case 3:
                SiderealDay *= 1.5;
                break;
            case 4:
                SiderealDay *= 2;
                break;
            case 5:
                SiderealDay *= 3;
                break;
            case 6:
                SiderealDay *= 5;
                break;
            case 7:
                SiderealDay = _rollingService.D6(1) * 5 * 24;
                break;
            case 8:
                SiderealDay = _rollingService.D6(1) * 20 * 24;
                break;
            case 9:
                SiderealDay = _rollingService.D6(1) * 10 * 24;
                if (AxialTilt < 90) AxialTilt = 180 - AxialTilt;
                break;
            case 10:
                SiderealDay = _rollingService.D6(1) * 500 * 24;
                if (AxialTilt < 90) AxialTilt = 180 - AxialTilt;
                break;
            case 11:
                SiderealDay = (2.0 / 3.0) * moon.Period;
                if (AxialTilt > 3) AxialTilt = (_rollingService.D6(2) - 2) / 10.0;
                break;
            case >= 12:
                SiderealDay = moon.Period;
                if (AxialTilt > 3) AxialTilt = (_rollingService.D6(2) - 2) / 10.0;
                if (_rollingService.D6(2) == 12) {
                    CalculateTidalLockStatus(0);
                }

                IsTidallyLocked = true;
                break;
        }

        if (SiderealDay > moon.Period) {
            SiderealDay = moon.Period;
            IsTidallyLockedWithMoon = true;
        }


        if (IsTidallyLockedWithMoon) {
            SiderealDay = moon.Period;
            AxialTilt = (_rollingService.D6(1)) switch {
                <= 4 => (_rollingService.D6(1) - 1) / 50.0,
                5 => _rollingService.D6(1) / 5.0,
                6 => _rollingService.D6(1),
                _ => throw new ArgumentOutOfRangeException()
            };
            Eccentricity = Math.Min(EccentricityService.CalculateEccentricity(-2), Eccentricity);
        }
    }

    private void CalculateTidalLockStatus(int dm)
    {
        switch (_rollingService.D6(2) + dm) {
            case <= 2:
                break;
            case 3:
                SiderealDay *= 1.5;
                break;
            case 4:
                SiderealDay *= 2;
                break;
            case 5:
                SiderealDay *= 3;
                break;
            case 6:
                SiderealDay *= 5;
                break;
            case 7:
                SiderealDay = _rollingService.D6(1) * 5 * 24;
                break;
            case 8:
                SiderealDay = _rollingService.D6(1) * 20 * 24;
                break;
            case 9:
                SiderealDay = _rollingService.D6(1) * 10 * 24;
                if (AxialTilt < 90) AxialTilt = 180 - AxialTilt;
                break;
            case 10:
                SiderealDay = _rollingService.D6(1) * 500 * 24;
                if (AxialTilt < 90) AxialTilt = 180 - AxialTilt;
                break;
            case 11:
                SiderealDay = (2.0 / 3.0) * (Period * 365.25 * 24);
                if (AxialTilt > 3) AxialTilt = (_rollingService.D6(2) - 2) / 10.0;
                break;
            case >= 12:
                SiderealDay = Period * 365.25 * 24;
                if (AxialTilt > 3) AxialTilt = (_rollingService.D6(2) - 2) / 10.0;
                if (_rollingService.D6(2) == 12) {
                    CalculateTidalLockStatus(0);
                }

                IsTidallyLocked = true;
                break;
        }

        if (SiderealDay > (Period * 365.25 * 24)) {
            SiderealDay = Period * 365.25 * 24;
            IsTidallyLocked = true;
        }

        if (IsTidallyLocked) {
            SiderealDay = Period;
            AxialTilt = (_rollingService.D6(1)) switch {
                <= 4 => (_rollingService.D6(1) - 1) / 50.0,
                5 => _rollingService.D6(1) / 5.0,
                6 => _rollingService.D6(1),
                _ => throw new ArgumentOutOfRangeException()
            };
            Eccentricity = Math.Min(EccentricityService.CalculateEccentricity(-2), Eccentricity);
        }
    }

    private void CalculateTidalEffects(WorldBuilderStarSystem starSystem)
    {
        if (!IsTidallyLocked) {
            StarTidalEffect = (starSystem.Mass * Size) / (32 * OrbitDistance);
        }

        foreach (var moon in Moons) {
            if (!IsTidallyLockedWithMoon || Math.Abs(moon.Period - SiderealDay) > .001) {
                MoonTidalEffect += (moon.Mass * Size) / Math.Pow((3.2 * (moon.OrbitDistanceInKM)) / 1000000.0, 3);
            }
        }
    }
}