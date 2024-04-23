using SectorCreator.Models.Basic;
using SectorCreator.Models.CustomTypes;

namespace SectorCreator.WorldBuilder.Planet.Planet;

public partial class WorldBuilderPlanet
{
    //  TODO Add Cultural Differences
    //      TODO Including Modifiers for SubScores
    public int Diversity { get; set; }
    public int Xenophilia { get; set; }
    public int Uniqueness { get; set; }
    public int Symbology { get; set; }
    public int Cohesion { get; set; }
    public int Progressiveness { get; set; }
    public int Expansionism { get; set; }
    public int Militancy { get; set; }
    public string CulturalExtension => $"{ExtendedHex.values[Diversity]}{ExtendedHex.values[Xenophilia]}{ExtendedHex.values[Uniqueness]}{ExtendedHex.values[Symbology]}";

    private void GenerateCulture()
    {
        GenerateDiversity();
        GenerateXenophilia();
        GenerateUniqueness();
        GenerateSymbology();
        GenerateCohesion();
        GenerateProgressiveness();
        GenerateExpansionism();
        GenerateMilitancy();
    }

    private void GenerateDiversity()
    {
        var dm = Population switch {
            <= 5 => -2,
            >= 9 => 2,
            _ => 0
        };

        dm += Government switch {
            <= 2 => 1,
            7 => 4,
            >= 13 and <= 15 => -4,
            _ => 0
        };

        dm += LawLevel switch {
            <= 4 => 1,
            >= 10 => -1,
            _ => 0
        };

        dm += PopulationConcentrationRating switch {
            <= 3 => 1,
            >= 7 => -2,
            _ => 0
        };

        Diversity = Math.Max(_rollingService.D6(2) + dm, 0);
    }

    private void GenerateXenophilia()
    {
        var dm = Population switch {
            <= 5 => -1,
            >= 9 => 2,
            _ => 0
        };

        dm += Government switch {
            13 or 14 => -2,
            _ => 0
        };

        dm += LawLevel >= 10 ? -2 : 0;

        dm += Starport.Class switch {
            StarportClass.A => 2,
            StarportClass.B => 1,
            StarportClass.C or StarportClass.F => 0,
            StarportClass.D or StarportClass.G => -1,
            StarportClass.E or StarportClass.H => -2,
            StarportClass.X or StarportClass.Y => -4,
            _ => throw new ArgumentOutOfRangeException()
        };

        dm += Diversity switch {
            <= 3 => -2,
            >= 12 => 1,
            _ => 0
        };

        Xenophilia = Math.Max(_rollingService.D6(2) + dm, 0);
    }

    private void GenerateUniqueness()
    {
        var dm = Starport.Class switch {
            StarportClass.A => -2,
            StarportClass.B => -1,
            StarportClass.C or StarportClass.F => 0,
            StarportClass.D or StarportClass.G => 1,
            StarportClass.E or StarportClass.H => 2,
            StarportClass.X or StarportClass.Y => 4,
            _ => throw new ArgumentOutOfRangeException()
        };

        dm += Diversity <= 3 ? -1 : 0;
        dm += Xenophilia switch {
            <= 3 => 2,
            <= 8 => 0,
            <= 11 => -1,
            >= 12 => -2
        };

        Uniqueness = Math.Max(_rollingService.D6(2) + dm, 0);
    }

    private void GenerateSymbology()
    {
        var dm = Government is 13 or 14 ? 2 : 0;
        dm += TechLevel switch {
            <= 1 => -3,
            <= 3 => -1,
            <= 8 => 0,
            <= 11 => 2,
            >= 12 => 4
        };

        dm += Uniqueness switch {
            <= 8 => 0,
            <= 11 => 1,
            >= 12 => 3
        };

        Symbology = Math.Max(_rollingService.D6(2) + dm, 0);
    }

    private void GenerateCohesion()
    {
        var dm = Government switch {
            3 or 12 => 2,
            5 or 6 or 9 => 1,
            _ => 0
        };

        dm += LawLevel switch {
            <= 2 => -2,
            >= 10 => 2,
            _ => 0
        };

        dm += PopulationConcentrationRating switch {
            <= 3 => -2,
            >= 7 => 2,
            _ => 0
        };

        dm += Diversity switch {
            <= 2 => 4,
            <= 5 => 2,
            <= 8 => 0,
            <= 11 => -2,
            >= 12 => -4
        };

        Cohesion = Math.Max(_rollingService.D6(2) + dm, 0);
    }

    private void GenerateProgressiveness()
    {
        var dm = Population switch {
            <= 5 => 0,
            <= 8 => -1,
            >= 9 => -2
        };

        dm += Government switch {
            5 => 1,
            11 => -2,
            13 or 14 => -6,
            _ => 0
        };

        dm += LawLevel switch {
            <= 8 => 0,
            <= 11 => -1,
            >= 12 => -4
        };

        dm += Diversity switch {
            <= 3 => -2,
            <= 11 => 0,
            >= 12 => 1
        };
        
        dm += Xenophilia switch {
            <= 5 => -1,
            <= 8 => 0,
            >= 9 => 2
        };

        dm += Cohesion switch {
            <= 5 => 2,
            <= 8 => 0,
            >= 9 => -2
        };

        Progressiveness = Math.Max(_rollingService.D6(2) + dm, 0);
    }

    private void GenerateExpansionism()
    {
        var dm = Government switch {
            10 or >= 12 => 2,
            _ => 0
        };

        dm += Diversity switch {
            <= 3 => 3,
            <= 11 => 0,
            >= 12 => -3
        };

        dm += Xenophilia switch {
            <= 5 => 1,
            <= 8 => 0,
            >= 9 => -2
        };

        Expansionism = Math.Max(_rollingService.D6(2) + dm, 0);
    }

    private void GenerateMilitancy()
    {
        var dm = Government >= 10 ? 3 : 0;

        dm += LawLevel switch {
            <= 8 => 0,
            <= 11 => 1,
            >= 12 => 2
        };

        dm += Xenophilia switch {
            <= 5 => 1,
            <= 8 => 0,
            >= 9 => -2
        };

        dm += Expansionism switch {
            <= 5 => -1,
            <= 8 => 0,
            <= 11 => 1,
            >= 12 => 2
        };

        Militancy = _rollingService.D6(2) + dm;
    }

}