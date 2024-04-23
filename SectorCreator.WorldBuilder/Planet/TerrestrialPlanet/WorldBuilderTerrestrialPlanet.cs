using SectorCreator.Global;
using SectorCreator.Global.Enums;
using SectorCreator.Models.CustomTypes;
using SectorCreator.WorldBuilder.Enums;
using SectorCreator.WorldBuilder.Planet.Planet;

namespace SectorCreator.WorldBuilder.Planet.TerrestrialPlanet;

public partial class WorldBuilderTerrestrialPlanet : WorldBuilderPlanet
{
    public WorldBuilderTerrestrialPlanet()
    {
        PlanetType = PlanetType.Terrestrial;
    }

    public WorldBuilderTerrestrialPlanet(WorldBuilderPlanet planet, Coordinates coordinates)
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
        PlanetType = PlanetType.Terrestrial;
    }

    public void GenerateBasicCharacteristics(WorldBuilderStarSystem starSystem)
    {
        GenerateAtmosphere(starSystem.HZCO);
        GenerateHydrographics(starSystem);
    }


    public new string GetNotes(double HZCO)
    {
        var notes = new List<string>();

        if (IsInHabitableZone(HZCO)) notes.Add("HZ");

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