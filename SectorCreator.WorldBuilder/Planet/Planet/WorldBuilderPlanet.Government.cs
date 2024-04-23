using System.Data;
using SectorCreator.Global;

namespace SectorCreator.WorldBuilder.Planet.Planet;

public partial class WorldBuilderPlanet
{
    public int Government { get; set; }
    public List<Government> Governments { get; set; } = new();
    public int[,] GovernmentRelationships { get; set; } = new int [0, 0];

    private void GenerateBasicGovernment()
    {
        if (Population > 0) {
            Government = Math.Max(_rollingService.Flux() + Population, 0);
            Government = Math.Min(Government, 15);
        }

        if (Government != 7) {
            Governments.Add(GenerateNewGovernment());
        }
    }

    private void GenerateGovernment()
    {
        var dm = Government switch {
            0 or 7 => 1,
            >= 10 => -1,
            _ => 0
        };

        if (Government != 0) {
            var numFactions = _rollingService.D3(1) + dm;

            for (var i = 0; i < numFactions; i++) {
                Governments.Add(GenerateNewGovernment());
                if (Governments.Last().Code == 7) {
                    numFactions += _rollingService.D3(1) + 2;
                }
            }
        }

        Governments.RemoveAll(x => x.Code == 7);
        Governments = Governments.OrderBy(x => x.FactionStrength).ToList();

        GovernmentRelationships = new int[Governments.Count, Governments.Count];

        for (var i = 0; i < Governments.Count; i++) {
            Governments[i].Id = i;
            GenerateRelationships(i);
        }

        foreach (var government in Governments) {
            GenerateLawLevel(government);
        }
    }

    private void GenerateRelationships(int governmentNumber)
    {
        for (var i = 0; i < Governments.Count; i++) {
            if (governmentNumber == i) {
                GovernmentRelationships[governmentNumber, i] = -1;
            } else if (governmentNumber > i) {
                GovernmentRelationships[governmentNumber, i] = GovernmentRelationships[i, governmentNumber];
            } else {
                var dm = 0;
                if (governmentNumber == 0) dm++;
                if (Governments[governmentNumber].Code == Governments[i].Code) dm--;
                dm += (int) Math.Floor(Math.Abs(Governments[governmentNumber].Code - Governments[i].Code) / 2.0);

                var relationship = Math.Max(_rollingService.D3(2) + dm, 1);
                relationship = Math.Min(relationship, 9);

                GovernmentRelationships[governmentNumber, i] = relationship;
            }
        }
    }

    private Government GenerateNewGovernment()
    {
        var government = new Government {Code = GenerateGovernmentCode()};
        if (Governments.Count == 0) {
            government = new Government {Code = Government};
        }
        if (government.Code == 7) return government;
        GenerateDegreeOfCentralization(government);
        GenerateAuthority(government);
        GenerateGovernmentPower(government);
        return government;
    }

    private void GenerateGovernmentPower(Government government)
    {
        if (Governments.Count == 0) {
            government.FactionStrength = FactionStrength.OfficialGovernment;
        } else {
            government.FactionStrength = _rollingService.D6(2) switch {
                <= 3 => FactionStrength.Obscure,
                <= 5 => FactionStrength.Fringe,
                <= 7 => FactionStrength.Minor,
                <= 9 => FactionStrength.Notable,
                <= 11 => FactionStrength.Significant,
                >= 12 => FactionStrength.PopularSupport
            };
        }
    }

    private int GenerateGovernmentCode()
    {
        var governmentCode = Math.Max(_rollingService.Flux() + Population, 0);
        governmentCode = Math.Min(governmentCode, 15);
        return governmentCode;
    }

    private void GenerateDegreeOfCentralization(Government government)
    {
        var dm = government.Code switch {
            >= 2 and <= 5 => -1,
            6 or (>= 8 and <= 11) => 1,
            >= 12 => 2,
            _ => 0
        };

        if (Government == 7) dm++;

        dm += PopulationConcentrationRating switch {
            <= 3 => -1,
            <= 6 => 0,
            <= 8 => 1,
            >= 9 => 3
        };

        government.Centralization = (_rollingService.D6(2) + dm) switch {
            <= 5 => Centralization.Confederal,
            <= 8 => Centralization.Federal,
            >= 9 => Centralization.Unitary
        };
    }

    private void GenerateAuthority(Government government)
    {
        var dm = government.Code switch {
            1 or 6 or 10 or 13 or 14 => 6,
            2 => -4,
            2 or 5 or 12 => -2,
            11 or 15 => 4,
            _ => 0
        };

        dm += government.Centralization switch {
            Centralization.Confederal => -2,
            Centralization.Federal => 0,
            Centralization.Unitary => 2,
            _ => throw new ArgumentOutOfRangeException()
        };

        var mainAuthority = (_rollingService.D6(2) + dm) switch {
            <= 4 or 8 => Authority.Legislative,
            5 or 10 or >= 12 => Authority.Executive,
            6 or 11 => Authority.Judicial,
            7 or 9 => Authority.Balance
        };

        government.Authorities.Add(new GovernmentAuthority {
            IsMainAuthority = mainAuthority is Authority.Balance or Authority.Legislative,
            Authority = Authority.Legislative
        });
        government.Authorities.Add(new GovernmentAuthority {
            IsMainAuthority = mainAuthority is Authority.Balance or Authority.Executive,
            Authority = Authority.Executive
        });
        government.Authorities.Add(new GovernmentAuthority {
            IsMainAuthority = mainAuthority is Authority.Balance or Authority.Judicial,
            Authority = Authority.Judicial
        });

        government.Authorities = government.Authorities.OrderByDescending(x => x.IsMainAuthority).ToList();

        foreach (var authority in government.Authorities) {
            authority.GenerateStructure(government, government.Authorities.First().FunctionalStructure == Structure.Ruler);
        }
    }
}

public class Government
{
    public int Code { get; set; }
    public Centralization Centralization { get; set; }
    public List<GovernmentAuthority> Authorities { get; set; } = new();
    public FactionStrength FactionStrength { get; set; }
    public int LawLevel { get; set; }
    public JusticeSystem JusticeSystem { get; set; }
    public Uniformity UniformityOfLaw { get; set; }
    public bool PresumptionOfInnocence { get; set; }
    public bool DeathPenalty { get; set; }
    public int EconomicLawLevel { get; set; }
    public int CriminalLawLevel { get; set; }
    public int PrivateLawLevel { get; set; }
    public int PersonalRightsLevel { get; set; }
    public int TechLevel { get; set; }
    public int LowCommonTechLevel { get; set; }
    public int Id { get; set; }
    public int Enforcement { get; set; }
    public int Militia { get; set; }
    public int Army { get; set; }
    public int WetNavy { get; set; }
    public int AirForce { get; set; }
    public int SystemDefense { get; set; }
    public int Navy { get; set; }
    public int Marine { get; set; }
}

public enum FactionStrength
{
    OfficialGovernment,
    PopularSupport,
    Significant,
    Notable,
    Minor,
    Fringe,
    Obscure
}

public class GovernmentAuthority
{
    private readonly IRollingService _rollingService = new RollingService();
    public bool IsMainAuthority { get; set; } = false;
    public Authority Authority { get; set; }
    public Structure FunctionalStructure { get; set; }

    public void GenerateStructure(Government government, bool mainFunctionIsRuler)
    {
        if (mainFunctionIsRuler) {
            FunctionalStructure = Structure.Ruler;
            return;
        }

        FunctionalStructure = government.Code switch {
            2 => Structure.Demos,
            8 or 9 => Structure.MultipleCouncils,
            3 or 12 or 15 => _rollingService.D6(1) switch {
                <= 4 => Structure.SingleCouncil,
                >= 5 => Structure.MultipleCouncils
            },
            10 or 11 or 13 or 14 => IsMainAuthority switch {
                true => _rollingService.D6(1) switch {
                    <= 5 => Structure.Ruler,
                    >= 6 => Structure.SingleCouncil
                },
                false => GetFunctionalStructure(2)
            },
            _ => GetFunctionalStructure(0)
        };
    }

    private Structure GetFunctionalStructure(int dm)
    {
        return (_rollingService.D6(2) + dm) switch {
            <= 3 => Structure.Demos,
            7 or 8 => Structure.Ruler,
            5 or 6 or 9 or 11 => Structure.MultipleCouncils,
            4 or 10 or >= 12 => Structure.SingleCouncil
        };
    }
}

public enum Structure
{
    None,
    Demos,
    SingleCouncil,
    MultipleCouncils,
    Ruler
}

public enum Authority
{
    None,
    Legislative,
    Executive,
    Judicial,
    Balance
}

public enum Centralization
{
    None,
    Confederal,
    Federal,
    Unitary
}