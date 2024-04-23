using SectorCreator.Global;
using SectorCreator.Global.Enums;
using SectorCreator.Models.CustomTypes;
using SectorCreator.WorldBuilder.Enums;
using SectorCreator.WorldBuilder.Planet.Moon;
using SectorCreator.WorldBuilder.Planet.Planet;

namespace SectorCreator.WorldBuilder.Planet.GasGiant;

public partial class WorldBuilderGasGiantPlanet : WorldBuilderPlanet
{
    private int DiameterInEarths => Diameter / 12742;
    public new string SAH => Size switch {
        16 => $"GS{ExtendedHex.values[DiameterInEarths]}",
        17 => $"GM{ExtendedHex.values[DiameterInEarths]}",
        18 => $"GL{ExtendedHex.values[DiameterInEarths]}",
    };
    
    public WorldBuilderGasGiantPlanet() {
        PlanetType = PlanetType.Jovian;
        
    }
    public WorldBuilderGasGiantPlanet(WorldBuilderPlanet planet, Coordinates coordinates)
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
        PlanetType = PlanetType.Jovian;
    }

    public void GenerateBasicCharacteristics()
    {
        Atmosphere = 16;
        Hydrographics = 16;
    }
    
    public new void GenerateMoon()
    {
        var moon = new WorldBuilderMoon();
        switch (_rollingService.D6(1)) {
            case <= 3:
                moon.Size = 26;
                break;
            case <= 5:
                moon.Size = _rollingService.D3(1) - 1;
                break;
            case 6:
    
                switch (_rollingService.D6(1)) {
                    case <= 3:
                        moon.Size = _rollingService.D6(1);
                        break;
                    case <= 5:
                        moon.Size = _rollingService.D6(2) - 2;
                        break;
                    case 6:
                        moon.Size = _rollingService.D6(2) + 4;
                        if (moon.Size == 16) moon = GenerateGasGiantMoon(moon);
                        break;
                }
    
                break;
        }
    
        if (moon.Size == 0) {
            moon.Size = 25;
        } else if (moon.Size < 0) {
            moon.Size = 26;
        }
    
        Moons.Add(moon);
    }
    
    private WorldBuilderMoon GenerateGasGiantMoon(WorldBuilderMoon moon)
    {
        if (Size == 18 && _rollingService.D6(2) == 12) {
            moon.Size = 17;
            Diameter = (_rollingService.D6(1) + 6) * 12742;
            Density = 200;
            // Mass = 20 * (_rollingService.D6(3) + 1);
            return moon;
        }
    
        Diameter = _rollingService.D3(2) * 12742;
        Density = 200;
        // Mass = 5 * (_rollingService.D6(1) + 1);
        return moon;
    }
    
    public new string GetNotes(double HZCO)
    {
        var notes = new List<string> {$"{(int)Mass}\u2295"};
    
        if (HZCO >= 1 && OrbitNumber >= HZCO - 1 && OrbitNumber <= HZCO + 1) notes.Add("HZ");
        if (HZCO < 1 && OrbitNumber >= HZCO - .1 && OrbitNumber <= HZCO + .1) notes.Add("HZ");
    
        if (Anomaly == PlanetAnomaly.Random) notes.Add("Anomalous orbit");
        if (Anomaly == PlanetAnomaly.Eccentric) notes.Add("Eccentric orbit");
        if (Anomaly == PlanetAnomaly.Inclined) notes.Add("Inclined orbit");
        if (Anomaly == PlanetAnomaly.Retrograde) notes.Add("Retrograde orbit");
        if (Anomaly == PlanetAnomaly.LeadingTrojan) notes.Add("Leading Trojan orbit");
        if (Anomaly == PlanetAnomaly.TrailingTrojan) notes.Add("Trailing Trojan orbit");
    
        var ringCount = Moons.Count(x => x.Size == 25);
        
        if (ringCount > 0) {
            notes.Add($"R{(ringCount > 9 ? ringCount : "0" + ringCount)}");
        }
        notes.AddRange(Moons.Where(x => x.Size != 25).Select(moon => ExtendedHex.values[moon.Size].ToString()));
    
        return string.Join(", ", notes);
    }
}