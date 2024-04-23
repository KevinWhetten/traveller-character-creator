using SectorCreator.WorldBuilder.Enums;
using SectorCreator.WorldBuilder.Planet.Planet;
using SectorCreator.WorldBuilder.Services;

namespace SectorCreator.WorldBuilder.Planet.Moon;

public partial class WorldBuilderMoon
{
    public double OrbitDistanceInKM => ParentDiameter * OrbitNumber;
    public new double OrbitDistanceInDiamters => OrbitDistanceInKM / ParentDiameter;
    
    private void GenerateOrbitLocation(WorldBuilderPlanet parent)
    {
        var rocheLimit = 1.22 * Math.Pow(parent.Density / Density, 1.0 / 3.0);
        
        switch (_rollingService.D6(1) + (parent.MoonOrbitRange < 60 ? 1 : 0)) {
            case <= 3:
                MoonOrbit = MoonOrbit.Inner;
                OrbitNumber = (_rollingService.D6(2) - 2) * parent.MoonOrbitRange / 60.0 + rocheLimit;
                break;
            case <= 5:
                MoonOrbit = MoonOrbit.Middle;
                OrbitNumber = (_rollingService.D6(2) - 2) * parent.MoonOrbitRange / 30.0 + parent.MoonOrbitRange / 6.0 + rocheLimit + 1;
                break;
            case >= 6:
                MoonOrbit = MoonOrbit.Outer;
                OrbitNumber = (_rollingService.D6(2) - 2) * parent.MoonOrbitRange / 20.0 + parent.MoonOrbitRange / 2.0 + rocheLimit + 3;
                break;
        }
    }
    
    private void CalculateEccentricity(int MOR)
    {
        var dm = MoonOrbit switch {
            MoonOrbit.Inner => -1,
            MoonOrbit.Middle => 1,
            MoonOrbit.Outer => OrbitNumber > MOR ? 6 : 4,
            _ => 0
        };
    
        Eccentricity = EccentricityService.CalculateEccentricity(dm);
    
        Anomaly = (_rollingService.D6(2) + dm) switch {
            >= 10 => PlanetAnomaly.Retrograde,
            _ => PlanetAnomaly.None
        };
    }
    
    private void CalculatePeriod(WorldBuilderPlanet parent)
    {
        Period = 0.176927 * Math.Sqrt(Math.Pow(OrbitNumber * parent.Size, 3) / (Mass + parent.Mass));
    }
}