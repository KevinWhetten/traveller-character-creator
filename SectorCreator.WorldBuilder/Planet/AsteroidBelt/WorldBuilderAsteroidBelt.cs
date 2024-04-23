using SectorCreator.Global;
using SectorCreator.Global.Enums;
using SectorCreator.Models.CustomTypes;
using SectorCreator.WorldBuilder.Enums;
using SectorCreator.WorldBuilder.Planet.Planet;
using SectorCreator.WorldBuilder.Services;

namespace SectorCreator.WorldBuilder.Planet.AsteroidBelt;

public partial class WorldBuilderAsteroidBelt : WorldBuilderPlanet
{
    public new string GetObject(int num) => Primary + " P" + RomanNumeralService.values[num];
    
    public WorldBuilderAsteroidBelt()
    {
        PlanetType = PlanetType.AsteroidBelt;
    }
    
    public WorldBuilderAsteroidBelt(WorldBuilderPlanet planet, Coordinates coordinates)
    {
        Name = planet.Name;
        Coordinates = coordinates;
        Size = planet.Size;
        Atmosphere = planet.Atmosphere;
        Hydrographics = planet.Hydrographics;
        Diameter = planet.Diameter;
        OrbitNumber = planet.OrbitNumber;
        Period = planet.Period;
        Eccentricity = planet.Eccentricity;
        PlanetType = PlanetType.AsteroidBelt;
    }

    public void GenerateBasicCharacteristics()
    {
        Atmosphere = 0;
        Hydrographics = 0;
    }
    
    public new string GetNotes(double HZCO = 0.0)
    {
        var notes = new List<string>();
    
        if (Anomaly == PlanetAnomaly.Random) notes.Add("Anomalous orbit");
        if (Anomaly == PlanetAnomaly.Eccentric) notes.Add("Eccentric orbit");
        if (Anomaly == PlanetAnomaly.Inclined) notes.Add("Inclined orbit");
        if (Anomaly == PlanetAnomaly.Retrograde) notes.Add("Retrograde orbit");
        if (Anomaly == PlanetAnomaly.LeadingTrojan) notes.Add("Leading Trojan orbit");
        if (Anomaly == PlanetAnomaly.TrailingTrojan) notes.Add("Trailing Trojan orbit");
    
        notes.AddRange(Moons.Where(x => x.Size != 25).Select(moon => ExtendedHex.values[moon.Size].ToString()));
    
        return string.Join(", ", notes);
    }
}