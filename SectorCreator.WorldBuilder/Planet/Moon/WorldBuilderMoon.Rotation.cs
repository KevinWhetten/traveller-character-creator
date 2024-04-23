using SectorCreator.WorldBuilder.Enums;
using SectorCreator.WorldBuilder.Planet.Planet;
using SectorCreator.WorldBuilder.Services;

namespace SectorCreator.WorldBuilder.Planet.Moon;

public partial class WorldBuilderMoon
{
    public new void GenerateBasicRotationDetails(WorldBuilderStarSystem starSystem)
    {
        GenerateBasicRotation(starSystem.Age);
        AxialTilt = GenerateAxialTilt();
    }
    
    public void GenerateTidalForces(WorldBuilderStarSystem starSystem, WorldBuilderPlanet parent)
    {
        GenerateTidalForce(parent);
        CalculateTidalEffects(starSystem, parent);
    }
    
    private void GenerateTidalForce(WorldBuilderPlanet parent)
    {
        TidalForce = (parent.Mass * Diameter) / Math.Pow(OrbitDistance, 3);
    }
    
    private void CalculateTidalEffects(WorldBuilderStarSystem starSystem, WorldBuilderPlanet parent)
    {
        StarTidalEffect = (starSystem.Mass * Size) / (32 * OrbitDistance);
    
        PlanetTidalEffect = (parent.Mass * Size) / (3.2 * Math.Pow(OrbitDistanceInKM / 1000000.0, 3));
    }
    
    internal void GenerateTidalLockStatus(WorldBuilderPlanet parent, WorldBuilderStarSystem starSystem)
    {
        var dm = GetBasicTidalLockDM(starSystem);
        dm += 6;
    
        if (OrbitNumber > 20) dm -= (int) Math.Floor(OrbitNumber / 20.0);
    
        if (Anomaly == PlanetAnomaly.Retrograde) dm -= 2;
    
        dm += parent.Mass switch {
            >= 1 and < 10 => 2,
            >= 10 and < 100 => 4,
            >= 100 and < 1000 => 6,
            >= 1000 => 8,
            _ => 0
        };
    
        CalculateTidalLockStatus(dm);
    
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
                SiderealDay = (2.0 / 3.0) * Period;
                if (AxialTilt > 3) AxialTilt = (_rollingService.D6(2) - 2) / 10.0;
                break;
            case >= 12:
                SiderealDay = Period;
                if (AxialTilt > 3) AxialTilt = (_rollingService.D6(2) - 2) / 10.0;
                if (_rollingService.D6(2) == 12) {
                    CalculateTidalLockStatus(0);
                }
    
                IsTidallyLocked = true;
                break;
        }
    
        if (SiderealDay > Period) {
            SiderealDay = Period;
            IsTidallyLocked = true;
        }
    }
}