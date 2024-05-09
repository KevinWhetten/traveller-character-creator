using SectorCreator.Global.Enums;
using SectorCreator.WorldBuilder.Enums;
using SectorCreator.WorldBuilder.Services;

namespace SectorCreator.WorldBuilder.Planet.Planet;

public partial class WorldBuilderPlanet
{
    public double OrbitNumber { get; set; }
    public double OrbitDistance => CalculateOrbitDistance();
    public double OrbitDistanceInMKm => OrbitDistance * 149.5979;
    public double Eccentricity { get; set; }
    public double Period { get; set; }
    public double NearAU => OrbitDistance * (1 - Eccentricity);
    public double FarAU => OrbitDistance * (1 + Eccentricity);
    
    protected double CalculateOrbitDistance()
    {
        return Math.Floor(OrbitNumber) switch {
            0 => 0 + (OrbitNumber * .4),
            1 => .4 + (OrbitNumber - 1) * .3,
            2 => .7 + (OrbitNumber - 2) * .3,
            3 => 1 + (OrbitNumber - 3) * .6,
            4 => 1.6 + (OrbitNumber - 4) * 1.2,
            5 => 2.8 + (OrbitNumber - 5) * 2.4,
            6 => 5.2 + (OrbitNumber - 6) * 4.8,
            7 => 10 + (OrbitNumber - 7) * 10,
            8 => 20 + (OrbitNumber - 8) * 20,
            9 => 40 + (OrbitNumber - 9) * 37,
            10 => 77 + (OrbitNumber - 10) * 77,
            11 => 154 + (OrbitNumber - 11) * 154,
            12 => 308 + (OrbitNumber - 12) * 307,
            13 => 615 + (OrbitNumber - 13) * 615,
            14 => 1230 + (OrbitNumber - 14) * 1270,
            15 => 2500 + (OrbitNumber - 15) * 2400,
            16 => 4900 + (OrbitNumber - 16) * 4900,
            17 => 9800 + (OrbitNumber - 17) * 9700,
            18 => 19500 + (OrbitNumber - 18) * 20000,
            19 => 39500 + (OrbitNumber - 19) * 39200,
            20 => 78700,
            _ => 78700
        };
    }
    
    protected bool IsInHabitableZone(double HZCO)
    {
        return (HZCO >= 1 && OrbitNumber >= HZCO - 1 && OrbitNumber <= HZCO + 1)
               || (HZCO < 1 && OrbitNumber >= HZCO - .1 && OrbitNumber <= HZCO + .1);
    }
    
    public void CalculateEccentricity(int bodiesOrbiting, double systemAge)
    {
        var EccentricityDM = bodiesOrbiting - 1;
    
        if (systemAge > 1 && OrbitNumber < 1) {
            EccentricityDM--;
        }
    
        if (PlanetType == PlanetType.AsteroidBelt) {
            EccentricityDM++;
        }
    
        EccentricityDM += Anomaly switch {
            PlanetAnomaly.None => 0,
            PlanetAnomaly.Random => 2,
            PlanetAnomaly.Eccentric => 5,
            PlanetAnomaly.Inclined => 2,
            PlanetAnomaly.Retrograde => 2,
            PlanetAnomaly.LeadingTrojan => 0,
            PlanetAnomaly.TrailingTrojan => 0,
            _ => throw new ArgumentOutOfRangeException()
        };
    
        Eccentricity = EccentricityService.CalculateEccentricity(EccentricityDM);
    }
    
    public void CalculatePeriod(double starSystemMass)
    {
        Period = Math.Sqrt(Math.Pow(OrbitDistance, 3) / (starSystemMass + Mass * 0.000003));
    }
}