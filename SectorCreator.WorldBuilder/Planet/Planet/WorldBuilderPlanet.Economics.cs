using SectorCreator.WorldBuilder.Planet.AsteroidBelt;
using SectorCreator.Global.Enums;
using SectorCreator.Models.Basic;
using SectorCreator.Models.CustomTypes;
using SectorCreator.WorldBuilder.Services.Services.TradeCodeService;

namespace SectorCreator.WorldBuilder.Planet.Planet;

public partial class WorldBuilderPlanet
{
    public int ResourceRating { get; set; }
    public List<string> TradeCodes { get; set; } = new();
    public int Importance { get; set; }
    public string ImportanceExtension => $"{{ {Importance} }}";
    public int ResourceFactor { get; set; }
    public int LaborFactor => Math.Max(Population - 1, 0);
    public int InfrastructureFactor { get; set; }
    public int EfficiencyFactor { get; set; }
    public int ResourceUnits { get; set; }
    public int GWPPerCapita { get; set; }
    public long TotalGWP => GWPPerCapita * TotalWorldPopulation;
    public int WTNStarportModifier { get; set; }
    public int WorldTradeNumber { get; set; }
    public int InequalityRating { get; set; }
    public int DevelopmentScore => (int) ((GWPPerCapita / 1000.0) * (1 - (InequalityRating / 100.0)));
    public TariffRate TariffRates { get; set; }
    public int TariffPercentage { get; set; }

    public string EconomicExtension =>
        $"({ExtendedHex.values[ResourceFactor]}{ExtendedHex.values[LaborFactor]}{ExtendedHex.values[InfrastructureFactor]}{(EfficiencyFactor > 0 ? $"+{EfficiencyFactor}" : EfficiencyFactor)})";

    private void GenerateResources()
    {
        if (PlanetType == PlanetType.AsteroidBelt) {
            ((WorldBuilderAsteroidBelt) this).GenerateResources();
        } else {
            var dm = 0;

            if (Density >= 1.12) dm += 2;
            if (Density <= 0.5) dm -= 2;
            if (BiomassRating >= 3) dm += 2;
            if (BiodiversityRating is >= 8 and <= 10) dm += 1;
            if (BiodiversityRating >= 11) dm += 2;
            if (CompatibilityRating <= 3 && BiomassRating >= 1) dm--;
            if (CompatibilityRating >= 8) dm += 2;

            ResourceRating = Math.Max(_rollingService.Flux() + Size + dm, 2);
        }
    }

    private void GenerateTradeCodes()
    {
        TradeCodes = TradeCodeService.AddTradeCodes(this);
    }

    private void GenerateEconomics()
    {
        GenerateTradeCodes();
        GenerateImportance();
        GenerateResourceFactor();
        GenerateInfrastructureFactor();
        GenerateEfficiencyFactor();
        GenerateResourceUnits();
        GenerateGrossWorldProduct();
        GenerateWorldTradeNumber();
        GenerateInequalityRating();
        GenerateTariffs();
    }

    private void GenerateImportance()
    {
        Importance = Starport.Class switch {
            StarportClass.A or StarportClass.B => 1,
            StarportClass.D or StarportClass.F or StarportClass.E or StarportClass.H or StarportClass.X or StarportClass.Y => -1,
            _ => 0
        };

        Importance += Population switch {
            <= 6 => -1,
            <= 8 => 0,
            >= 9 => 1
        };

        Importance += TechLevel switch {
            <= 8 => -1,
            9 => 0,
            <= 15 => 1,
            >= 16 => 2
        };

        if (TradeCodes.Contains(TradeCode.Agricultural)) Importance++;
        if (TradeCodes.Contains(TradeCode.Industrial)) Importance++;
        if (TradeCodes.Contains(TradeCode.Rich)) Importance++;
        if (Bases.Count(x => x != Base.Corsair) >= 2) Importance++;
        if (Bases.Contains(Base.WayStation)) Importance++;
    }

    private void GenerateResourceFactor()
    {
        var dm = TechLevel >= 8 ? GasGiants + Belts : 0;

        if (TradeCodes.Contains(TradeCode.Industrial)) dm += 1 - _rollingService.D6(1);
        if (TradeCodes.Contains(TradeCode.Agricultural)) dm++;

        ResourceFactor = Math.Max(ResourceRating + dm, 0);
    }

    private void GenerateInfrastructureFactor()
    {
        var dm = Population switch {
            <= 3 => 0,
            <= 6 => _rollingService.D6(1),
            >= 7 => _rollingService.D6(2)
        };

        InfrastructureFactor = Math.Max(Importance + dm, 0);
    }

    private void GenerateEfficiencyFactor()
    {
        var dm = Government switch {
            0 or 3 or 6 or 9 or 11 or 12 or 15 => -1,
            1 or 2 or 4 or 5 or 8 => 1,
            _ => 0
        };

        dm += LawLevel switch {
            <= 4 => 1,
            <= 9 => 0,
            >= 10 => -1
        };

        dm += PopulationConcentrationRating switch {
            <= 3 => -1,
            <= 8 => 0,
            >= 9 => 1
        };

        dm += Progressiveness switch {
            <= 3 => -1,
            <= 8 => 0,
            >= 9 => 1
        };

        dm += Expansionism switch {
            <= 3 => -1,
            <= 8 => 0,
            >= 9 => 1
        };

        EfficiencyFactor = Population switch {
            0 => -5,
            <= 6 => _rollingService.Flux() + dm,
            >= 7 => _rollingService.D3(2) - 4 + dm
        };

        if (EfficiencyFactor == 0) EfficiencyFactor = 1;
    }

    private void GenerateResourceUnits()
    {
        ResourceUnits = (ResourceFactor < 1 ? 1 : ResourceFactor) * (LaborFactor < 1 ? 1 : LaborFactor) *
                        (InfrastructureFactor < 1 ? 1 : InfrastructureFactor) * EfficiencyFactor;
    }

    private void GenerateGrossWorldProduct()
    {
        var baseValue = Math.Min(InfrastructureFactor + ResourceFactor, 2 * InfrastructureFactor);
        baseValue = Math.Max(baseValue, 2);

        var techLevelModifier = TechLevel / 10.0;

        var portModifier = Starport.Class switch {
            StarportClass.A => 1.5,
            StarportClass.B => 1.2,
            StarportClass.C => 1.0,
            StarportClass.D => 0.8,
            StarportClass.E => 0.5,
            StarportClass.F => 0.9,
            StarportClass.G => 0.7,
            StarportClass.H => 0.4,
            StarportClass.X or StarportClass.Y => 0.2,
            _ => throw new ArgumentOutOfRangeException()
        };

        var governmentModifier = Government switch {
            0 => 1.0,
            1 => 1.5,
            2 => 1.2,
            3 => 0.8,
            4 => 1.2,
            5 => 1.3,
            6 => 0.6,
            7 => 1.0,
            8 => 0.9,
            9 => 0.8,
            10 => 1.0,
            11 => 0.7,
            12 => 1.0,
            13 => 0.6,
            14 => 0.5,
            15 => 0.8,
            _ => throw new ArgumentOutOfRangeException()
        };

        var tradeCodeModifier = 1.0;

        foreach (var tradeCode in TradeCodes) {
            if (tradeCode == TradeCode.Agricultural) {
                tradeCodeModifier *= 0.9;
            } else if (tradeCode == TradeCode.Asteroid) {
                tradeCodeModifier *= 1.2;
            } else if (tradeCode == TradeCode.Garden) {
                tradeCodeModifier *= 1.2;
            } else if (tradeCode == TradeCode.Industrial) {
                tradeCodeModifier *= 1.1;
            } else if (tradeCode == TradeCode.NonAgricultural) {
                tradeCodeModifier *= 0.9;
            } else if (tradeCode == TradeCode.NonIndustrial) {
                tradeCodeModifier *= 0.9;
            } else if (tradeCode == TradeCode.Poor) {
                tradeCodeModifier *= 0.8;
            } else if (tradeCode == TradeCode.Rich) {
                tradeCodeModifier *= 1.2;
            }
        }

        var totalModifiers = techLevelModifier * portModifier * governmentModifier * tradeCodeModifier;

        if (EfficiencyFactor > 0) {
            GWPPerCapita = (int) (1000 * baseValue * totalModifiers * EfficiencyFactor);
        } else {
            GWPPerCapita = (int) (1000 * (baseValue * totalModifiers) / -(EfficiencyFactor - 1));
        }
    }

    private void GenerateWorldTradeNumber()
    {
        var dm = TechLevel switch {
            <= 1 => -1,
            <= 4 => 0,
            <= 8 => 1,
            <= 14 => 2,
            >= 15 => 3
        };

        var baseWTN = Population + dm;

        WTNStarportModifier = baseWTN switch {
            <= 1 => Starport.Class switch {
                StarportClass.A => 3,
                StarportClass.B or StarportClass.C or StarportClass.F => 2,
                StarportClass.D or StarportClass.G or StarportClass.E or StarportClass.H => 1,
                _ => 0
            },
            <= 3 => Starport.Class switch {
                StarportClass.A or StarportClass.B => 2,
                StarportClass.C or StarportClass.F or StarportClass.D or StarportClass.G => 1,
                _ => 0
            },
            <= 5 => Starport.Class switch {
                StarportClass.A => 2,
                StarportClass.B or StarportClass.C or StarportClass.F => 1,
                StarportClass.D or StarportClass.G or StarportClass.E or StarportClass.H => 0,
                _ => -5
            },
            <= 7 => Starport.Class switch {
                StarportClass.A or StarportClass.B => 1,
                StarportClass.C or StarportClass.F or StarportClass.D or StarportClass.G => 0,
                StarportClass.E or StarportClass.H => -1,
                _ => -6
            },
            <= 9 => Starport.Class switch {
                StarportClass.A => 1,
                StarportClass.B or StarportClass.C or StarportClass.F => 0,
                StarportClass.D or StarportClass.G => -1,
                StarportClass.E or StarportClass.H => -2,
                _ => -7
            },
            <= 11 => Starport.Class switch {
                StarportClass.A or StarportClass.B => 0,
                StarportClass.C or StarportClass.F => 1,
                StarportClass.D or StarportClass.G => -2,
                StarportClass.E or StarportClass.H => -3,
                _ => -8
            },
            <= 13 => Starport.Class switch {
                StarportClass.A => 0,
                StarportClass.B => -1,
                StarportClass.C or StarportClass.F => -2,
                StarportClass.D or StarportClass.G => -3,
                StarportClass.E or StarportClass.H => -4,
                _ => -9
            },
            >= 14 => Starport.Class switch {
                StarportClass.A => 0,
                StarportClass.B => -2,
                StarportClass.C or StarportClass.F => -3,
                StarportClass.D or StarportClass.G => -4,
                StarportClass.E or StarportClass.H => -5,
                _ => -10
            }
        };

        WorldTradeNumber = baseWTN + WTNStarportModifier;
    }


    private void GenerateInequalityRating()
    {
        var dm = Government switch {
            6 or 11 or 15 => 10,
            0 or 1 or 3 or 9 or 12 => 5,
            4 or 8 => -5,
            2 => -10,
            _ => 0
        };

        dm += LawLevel >= 9 ? LawLevel - 8 : 0;
        dm += PopulationConcentrationRating;
        dm -= InfrastructureFactor;
        
        InequalityRating = 50 - EfficiencyFactor * 5 + _rollingService.Flux() * 2 + dm;
    }

    private void GenerateTariffs()
    {
        var dm = Government switch {
            0 => -7,
            2 or 4 => -4,
            9 => 2,
            _ => 0
        };

        dm += LawLevel >= 9 ? 2 : 0;
        dm += Xenophilia switch {
            <= 3 => 2,
            <= 8 => 0,
            >= 9 => -2
        };

        dm -= WTNStarportModifier;

        TariffRates = (_rollingService.D6(2) + dm) switch {
            <= 3 => TariffRate.Free,
            4 => TariffRate.Foreign,
            5 => TariffRate.Class,
            6 => TariffRate.Low,
            7 => TariffRate.Moderate,
            <= 9 => TariffRate.Varying,
            <= 11 => TariffRate.High,
            <= 13 => TariffRate.Extreme,
            >= 14 => TariffRate.Prohibitive
        };

        TariffPercentage = TariffRates switch {
            TariffRate.Free => 0,
            TariffRate.Foreign or TariffRate.Class => GetForeignOrClassPercentage(),
            TariffRate.Low => _rollingService.D6(1),
            TariffRate.Moderate => _rollingService.D6(2),
            TariffRate.Varying => _rollingService.D6(1) * _rollingService.D6(1),
            TariffRate.High => _rollingService.D6(2) * 5,
            TariffRate.Extreme => _rollingService.D6(2) * 10,
            TariffRate.Prohibitive => _rollingService.D6(2) * 20,
            _ => throw new ArgumentOutOfRangeException()
        };
        
        // TODO Varying
    }


    private int GetForeignOrClassPercentage()
    {
        return (_rollingService.D6(2) + 5) switch {
            <= 7 => _rollingService.D6(2),
            <= 9 => _rollingService.D6(1) * _rollingService.D6(1),
            <= 11 => _rollingService.D6(2) * 5,
            <= 13 => _rollingService.D6(2) * 10,
            >= 14 => _rollingService.D6(2) * 20
        };
    }
}