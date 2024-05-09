using SectorCreator.Global.Enums;
using SectorCreator.WorldBuilder.Star;

namespace SectorCreator.WorldBuilder.Planet.Planet;

public partial class WorldBuilderPlanet
{
    public TravelZone TravelZone { get; set; } = TravelZone.None;
    
    private void GenerateTravelZone(WorldBuilderStarSystem starSystem)
    {
        GenerateAmberZone(starSystem);
        GenerateRedZone(starSystem);
    }

    private void GenerateAmberZone(WorldBuilderStarSystem starSystem)
    {
        var dm = starSystem.IsPrimordial() ? 2 : 0;
        dm += PlanetType == PlanetType.Jovian ? 0 : Atmosphere switch {
            11 or 12 or >= 15 => 2,
            _ => 0
        };

        dm += Temperature > 373 ? 2 : 0;
        dm += BAR > 50 ? 2 : 0;
        dm += TotalSeismicStress >= 100 ? 2 : 0;
        dm += Government switch {
            0 => 4,
            7 => 2,
            _ => 0
        };

        dm += LawLevel == 0 ? 2 : 0;
        dm += Government + LawLevel > 20 ? Government + LawLevel - 16 : 0;
        dm += Xenophilia <= 5 ? 6 - Xenophilia : 0;
        dm += Militancy >= 9 ? Militancy - 8 : 0;
        dm += GetRelationships(Governments.First()).Any(x => x == 6) ? 2 : 0;
        dm += GetRelationships(Governments.First()).Any(x => x >= 8) ? 4 : 0;

        if (_rollingService.D6(2) + dm >= 25) {
            TravelZone = TravelZone.Amber;
        }
    }

    private void GenerateRedZone(WorldBuilderStarSystem starSystem)
    {
        var dm = 0;

        dm += starSystem.Star.SpecialType == StarSpecialType.Magnetar ? 10 : 0;
        dm += starSystem.Star.SpecialType == StarSpecialType.Pulsar ? 8 : 0;
        dm += starSystem.Star.SpecialType == StarSpecialType.Protostar ? 6 : 0;

        dm += TotalSeismicStress >= 200 ? 2 : 0;
        dm += (CurrentSophontExists && TechLevel <= 3) ? 4 : 0;
        dm += Xenophilia <= 2 ? 6 - Xenophilia : 0;
        dm += Militancy >= 12 ? Militancy - 8 : 0;
        dm += GetRelationships(Governments.First()).Any(x => x == 6) ? 2 : 0;
        dm += GetRelationships(Governments.First()).Any(x => x >= 8) ? 4 : 0;

        if (_rollingService.D6(2) + dm >= 25) {
            TravelZone = TravelZone.Red;
        }
    }
}