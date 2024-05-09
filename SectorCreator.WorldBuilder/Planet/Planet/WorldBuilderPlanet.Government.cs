using System.Data;

namespace SectorCreator.WorldBuilder.Planet.Planet;

public partial class WorldBuilderPlanet
{
    public int Government { get; set; }
    public List<Government> Governments { get; set; } = new();

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
        Governments = Governments.OrderByDescending(x => x.FactionStrength.Power).ToList();

        for (var i = 0; i < Governments.Count; i++) {
            Governments[i].Id = i;
            GenerateRelationships(Governments[i]);
        }

        foreach (var government in Governments) {
            GenerateLawLevel(government);
        }
    }

    private void GenerateRelationships(Government government)
    {
        for (var i = 0; i < Governments.Count; i++) {
            if (government.Id == i) {
                government.Relationships.Add(-1);
            } else if (government.Id > i) {
                government.Relationships.Add(Governments[i].Relationships[government.Id]);
            } else {
                var dm = 0;
                if (government.Id == 0) dm++;
                if (Governments[government.Id].Code == Governments[i].Code) dm--;
                dm += (int) Math.Floor(Math.Abs(Governments[government.Id].Code - Governments[i].Code) / 2.0);

                var relationship = Math.Max(_rollingService.D3(2) + dm, 1);
                relationship = Math.Min(relationship, 9);

                government.Relationships.Add(relationship);
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
            government.FactionStrength = FactionStrength.FactionStrengths[6];
        } else {
            government.FactionStrength = _rollingService.D6(2) switch {
                <= 3 => FactionStrength.FactionStrengths[0],
                <= 5 => FactionStrength.FactionStrengths[1],
                <= 7 => FactionStrength.FactionStrengths[2],
                <= 9 => FactionStrength.FactionStrengths[3],
                <= 11 => FactionStrength.FactionStrengths[4],
                >= 12 => FactionStrength.FactionStrengths[5]
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
            <= 5 => Centralization.Centralizations[0],
            <= 8 => Centralization.Centralizations[1],
            >= 9 => Centralization.Centralizations[2]
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

        dm += government.Centralization.Code switch {
            'C' => -2,
            'F' => 0,
            'U' => 2,
            _ => throw new ArgumentOutOfRangeException()
        };

        var mainAuthority = (_rollingService.D6(2) + dm) switch {
            <= 4 or 8 => Authority.Authorities[0],
            5 or 10 or >= 12 => Authority.Authorities[1],
            6 or 11 => Authority.Authorities[2],
            7 or 9 => Authority.Authorities[3]
        };

        government.Authorities.Add(new GovernmentAuthority {
            IsMainAuthority = mainAuthority.Code is 'B' or 'L',
            Authority = Authority.Authorities[0]
        });
        government.Authorities.Add(new GovernmentAuthority {
            IsMainAuthority = mainAuthority.Code is 'B' or 'E',
            Authority = Authority.Authorities[1]
        });
        government.Authorities.Add(new GovernmentAuthority {
            IsMainAuthority = mainAuthority.Code is 'B' or 'J',
            Authority = Authority.Authorities[2]
        });

        government.Authorities = government.Authorities.OrderByDescending(x => x.IsMainAuthority).ToList();

        foreach (var authority in government.Authorities) {
            authority.GenerateStructure(government, government.Authorities.First().FunctionalStructure == Structure.Structures[3]);
        }
    }
}