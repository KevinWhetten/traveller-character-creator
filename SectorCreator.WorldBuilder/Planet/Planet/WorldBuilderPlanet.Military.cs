using SectorCreator.Global.Enums;
using SectorCreator.Models.Basic;

namespace SectorCreator.WorldBuilder.Planet.Planet;

public partial class WorldBuilderPlanet
{
    public double MilitaryBudgetPercent { get; set; }
    public long MilitaryBudget => (long) (TotalGWP * MilitaryBudgetPercent);

    private void GenerateMilitary()
    {
        foreach (var government in Governments) {
            GenerateEnforcementBranch(government);
            GenerateMilitiaBranch(government);
            GenerateArmyBranch(government);
            GenerateWetNavyBranch(government);
            GenerateAirForceBranch(government);
            GenerateSystemDefenseBranch(government);
            GenerateNavyBranch(government);
            GenerateMarineBranch(government);
        }

        GenerateMilitaryBudget();
    }

    private void GenerateMilitaryBudget()
    {
        var dm = Government switch {
            0 or 2 or 4 => -2,
            5 => 1,
            9 => -1,
            10 or 15 => 3,
            11 or 12 or 14 => 2,
            _ => 0
        };

        dm += LawLevel >= 12 ? 2 : 0;
        dm += Bases.Contains(Base.Military) ? 4 : 0;
        dm += Bases.Contains(Base.Naval) ? 2 : 0;
        dm += Militancy - 5;
        dm -= 4 + (int) Math.Floor((Governments.First().Enforcement
                                    + Governments.First().Militia
                                    + Governments.First().Army
                                    + Governments.First().WetNavy
                                    + Governments.First().AirForce
                                    + Governments.First().SystemDefense
                                    + Governments.First().Navy
                                    + Governments.First().Marine) / 10.0);

        var stateOfReadinessModifier = GetRelationships(Governments.First()).Max() switch {
            <= 1 => 0.5, // Complacent Peace
            <= 3 => 0.75, // Low
            <= 5 => 1, // Normal
            <= 6 => 1.2, // Heightened Tensions, Threat of war, or Internal uprisings
            <= 8 => 2.0, // War or internal insurgency
            <= 9 => 5.0 // Total war
        };

        MilitaryBudgetPercent = (0.02 * (1 + (EfficiencyFactor / 10.0)) * (1 + Math.Max(_rollingService.Flux() + dm, -9) / 10.0)) * stateOfReadinessModifier;
    }



    private int GetBasicDM(Government government)
    {
        var dm = Militancy switch {
            <= 2 => -4,
            <= 5 => -1,
            <= 8 => 1,
            <= 11 => 2,
            >= 12 => 4
        };

        var maxRelationship = GetRelationships(government).Max();

        dm += maxRelationship switch {
            <= 4 => 0,
            <= 6 => 1,
            7 => 2,
            8 => 4,
            >= 9 => 8
        };

        return dm;
    }

    private List<int> GetRelationships(Government government)
    {
        var relationships = new List<int>();
        for (var i = 0; i < Governments.Count; i++) {
            relationships.Add(GovernmentRelationships[government.Id, i]);
        }

        return relationships;
    }

    private void GenerateEnforcementBranch(Government government)
    {
        var dm = GetBasicDM(government);

        dm += government.Code switch {
            0 => -5,
            11 => 2,
            _ => 0
        };

        dm += government.LawLevel switch {
            0 => -4,
            1 => -2,
            2 => -1,
            <= 8 => 0,
            <= 11 => 2,
            >= 12 => 4
        };

        dm += PopulationConcentrationRating <= 4 ? 2 : 0;
        dm += GetRelationships(government).Any(x => x == 6) ? 2 : 0;

        government.Enforcement = Math.Min(3 + dm, 18);
        government.Enforcement = Math.Max(government.Enforcement, 1);
    }

    private void GenerateMilitiaBranch(Government government)
    {
        var dm = GetBasicDM(government);

        dm += government.Code switch {
            1 => -4,
            2 => 2,
            6 => -6,
            _ => 0
        };

        dm -= government.LawLevel;

        dm += PopulationConcentrationRating switch {
            <= 2 => 2,
            <= 4 => 1,
            5 => 0,
            >= 6 => -1
        };

        government.Militia = _rollingService.D6(2) + dm - 4;

        if (government.Militia == 0) government.Militia++;

        government.Militia = Math.Max(government.Militia, 0);
        government.Militia = Math.Min(government.Militia, 18);
    }

    private void GenerateArmyBranch(Government government)
    {
        var dm = GetBasicDM(government);

        dm += government.Militia != 0 ? -2 : 0;

        dm += government.Code switch {
            0 => -6,
            7 or >= 10 => 4,
            _ => 0
        };

        dm += government.TechLevel switch {
            <= 7 => 4,
            >= 8 => -2
        };

        dm += Bases.Contains(Base.Military) ? 6 : 0;
        dm += GetRelationships(government).Any(x => x == 6) ? 2 : 0;

        government.Army = _rollingService.D6(2) + dm - 4;

        if (government.Army == 0) government.Army++;

        government.Army = Math.Max(government.Army, 0);
        government.Army = Math.Min(government.Army, 18);
    }

    private void GenerateWetNavyBranch(Government government)
    {
        var dm = GetBasicDM(government);

        dm += Hydrographics switch {
            0 => -20,
            <= 3 => -5,
            <= 7 => 0,
            8 => 2,
            9 => 4,
            >= 10 => 8
        };

        dm += government.Code == 7 ? 4 : 0;

        dm += government.TechLevel switch {
            0 => -8,
            <= 7 => 0,
            <= 9 => -2,
            >= 10 => -government.TechLevel
        };

        government.WetNavy = _rollingService.D6(2) + dm - 4;

        if (government.WetNavy == 0) government.WetNavy++;

        government.WetNavy = Math.Max(government.WetNavy, 0);
        government.WetNavy = Math.Min(government.WetNavy, 18);
    }

    private void GenerateAirForceBranch(Government government)
    {
        var dm = GetBasicDM(government);

        if (TechLevel <= 8) {
            dm += Atmosphere switch {
                0 or 1 => -20,
                2 or 3 or 14 => -8,
                4 or 5 => -2,
                _ => 0
            };
        }

        dm += government.Code == 7 ? 4 : 0;

        dm += government.TechLevel switch {
            <= 2 => -20,
            3 => -10,
            <= 9 => 0,
            <= 12 => -4,
            >= 13 => -6
        };

        government.AirForce = _rollingService.D6(2) + dm - 4;

        if (government.AirForce == 0) government.AirForce++;

        government.AirForce = Math.Max(government.AirForce, 0);
        government.AirForce = Math.Min(government.AirForce, 18);
    }

    private void GenerateSystemDefenseBranch(Government government)
    {
        var dm = GetBasicDM(government);

        dm += Population switch {
            <= 3 => -6,
            <= 5 => -2,
            _ => 0
        };

        dm += government.TechLevel switch {
            <= 5 => -20,
            6 => -8,
            7 => -6,
            8 => -2,
            _ => 0
        };

        dm += Starport.Class switch {
            StarportClass.A => 4,
            StarportClass.B => 2,
            StarportClass.C or StarportClass.F => 1,
            StarportClass.D or StarportClass.G => 0,
            StarportClass.E or StarportClass.H => -2,
            StarportClass.X or StarportClass.Y => -8,
            _ => throw new ArgumentOutOfRangeException()
        };

        dm += Starport.Highport ? 2 : 0;
        dm += Bases.Contains(Base.Naval) ? 4 : 0;
        dm += Bases.Contains(Base.Military) ? 2 : 0;

        government.SystemDefense = _rollingService.D6(2) + dm - 4;

        if (government.SystemDefense == 0) government.SystemDefense++;

        government.SystemDefense = Math.Max(government.SystemDefense, 0);
        government.SystemDefense = Math.Min(government.SystemDefense, 18);
    }

    private void GenerateNavyBranch(Government government)
    {
        var dm = GetBasicDM(government);

        dm += Population switch {
            <= 3 => -6,
            <= 6 => -3,
            _ => 0
        };

        dm += government.TechLevel switch {
            <= 5 => -20,
            6 => -12,
            7 => -8,
            8 => -6,
            _ => 0
        };

        dm += Starport.Class switch {
            StarportClass.A => 4,
            StarportClass.B => 1,
            StarportClass.E or StarportClass.H => -2,
            StarportClass.X or StarportClass.Y => -8,
            _ => 0
        };

        dm += Starport.Highport ? 2 : 0;
        dm += Bases.Contains(Base.Naval) ? 4 : 0;
        dm += Bases.Contains(Base.Military) ? 2 : 0;

        dm += Expansionism switch {
            <= 5 => -2,
            <= 8 => 0,
            <= 11 => 2,
            >= 12 => 4
        };

        government.Navy = _rollingService.D6(2) + dm - 4;

        if (government.Navy == 0) government.Navy++;

        government.Navy = Math.Max(government.Navy, 0);
        government.Navy = Math.Min(government.Navy, 18);
    }

    private void GenerateMarineBranch(Government government)
    {
        var dm = GetBasicDM(government);

        dm += Population <= 5 ? -4 : 0;
        dm += TechLevel <= 8 ? -6 : 0;
        dm += Bases.Contains(Base.Naval) ? 2 : 0;
        dm += Bases.Contains(Base.Military) ? 2 : 0;
        dm += government.Navy == 0 ? -6 : 0;
        dm += government.SystemDefense == 0 ? -6 : 0;

        dm += Expansionism switch {
            <= 5 => -4,
            <= 8 => 0,
            <= 11 => 1,
            >= 12 => 2
        };

        government.Marine = _rollingService.D6(2) + dm - 4;

        if (government.Marine == 0) government.Marine++;

        government.Marine = Math.Max(government.Marine, 0);
        government.Marine = Math.Min(government.Marine, 18);
    }
}