using SectorCreator.Global;
using SectorCreator.Global.Enums;
using SectorCreator.Models.Basic;
using SectorCreator.Models.CustomTypes;
using SectorCreator.Models.Services;
using SectorCreator.Models.Services.TradeCodeService;

namespace SectorCreator.Models.RTTWorldgen;

public class RttWorldgenPlanet : Planet
{
    public Guid Id { get; set; }

    public List<Population> Populations { get; set; } = new();

    public new int Population
    {
        get
        {
            return Populations.Count > 0
                ? Populations.Max(x => x.PopulationNumber)
                : 0;
        }
    }

    public int Biosphere { get; set; }
    public PlanetChemistry Chemistry { get; set; }
    public Rings Rings { get; set; }

    public int IndustrialBase { get; set; }

    public WorldType WorldType { get; set; }
    public PlanetOrbit PlanetOrbit { get; set; }
    public int OrbitPosition { get; set; }
    public bool IsMainWorld { get; set; }

    public Guid ParentId { get; set; }
    public PlanetType ParentType { get; set; }
    public Guid StarId { get; set; }
    public CompanionOrbit SatelliteOrbit { get; set; }
    public int Importance { get; set; }
    public string EconomicExtension { get; set; } = $"(000+0)";
    public string CulturalExtension { get; set; } = "[0000]";
    public string Nobility { get; set; } = "";
    public string PBG = "";
    public List<CulturalDifference> CulturalDifferences { get; } = new();

    public RttWorldgenPlanet(IRollingService rollingService, RttWorldgenPlanet rttWorldgenPlanet) : base(rollingService, rttWorldgenPlanet)
    {
        Coordinates = rttWorldgenPlanet.Coordinates;
        Id = rttWorldgenPlanet.Id;
        Biosphere = rttWorldgenPlanet.Biosphere;
        Chemistry = rttWorldgenPlanet.Chemistry;
        Rings = rttWorldgenPlanet.Rings;
        IndustrialBase = rttWorldgenPlanet.IndustrialBase;
        WorldType = rttWorldgenPlanet.WorldType;
        PlanetOrbit = rttWorldgenPlanet.PlanetOrbit;
        OrbitPosition = rttWorldgenPlanet.OrbitPosition;
        IsMainWorld = rttWorldgenPlanet.IsMainWorld;
        ParentId = rttWorldgenPlanet.ParentId;
        StarId = rttWorldgenPlanet.StarId;
        SatelliteOrbit = rttWorldgenPlanet.SatelliteOrbit;
        Allegiance = rttWorldgenPlanet.Allegiance;
        Importance = rttWorldgenPlanet.Importance;
        CulturalExtension = rttWorldgenPlanet.CulturalExtension;
        PBG = rttWorldgenPlanet.PBG;
        Populations = rttWorldgenPlanet.Populations;
    }

    public RttWorldgenPlanet(IRollingService rollingService) : base(rollingService)
    { }

    public RttWorldgenPlanet() : base(new RollingService())
    { }

    public void GenerateEconomicExtension(int gasGiantCount, int beltCount)
    {
        var resources = _rollingService.D6(2) + (TechLevel >= 8 ? gasGiantCount + beltCount : 0);
        var labor = Population - 1 > 0 ? Population - 1 : 0;
        var infrastructure = Population switch {
            0 => 0,
            <= 3 => Importance,
            <= 6 => Importance + _rollingService.D6(1),
            _ => Importance + _rollingService.D6(2)
        };
        if (infrastructure < 0) {
            infrastructure = 0;
        }

        var efficiencies = _rollingService.Flux();


        EconomicExtension =
            $"({ExtendedHex.values[resources]}{ExtendedHex.values[labor]}{ExtendedHex.values[infrastructure]}{(efficiencies >= 0 ? "+" : "")}{(efficiencies != 0 ? efficiencies : 1)})";
    }

    public void GenerateCulturalExtension()
    {
        if (Population > 0) {
            var heterogeneity = Population + _rollingService.Flux();
            if (heterogeneity <= 0) heterogeneity = 1;
            var acceptance = Population + Importance;
            if (acceptance <= 0) acceptance = 1;
            var strangeness = _rollingService.Flux() + 5;
            if (strangeness <= 0) strangeness = 1;
            var symbols = _rollingService.Flux() + TechLevel;
            if (symbols <= 0) symbols = 1;

            CulturalExtension =
                $"[{ExtendedHex.values[heterogeneity]}{ExtendedHex.values[acceptance]}{ExtendedHex.values[strangeness]}{ExtendedHex.values[symbols]}]";
        } else {
            CulturalExtension = "[0000]";
        }
    }

    public void SetNobility()
    {
        if (Population > 0) {
            Nobility += "B";
        }

        if (TradeCodes.Contains("Pa") || TradeCodes.Contains("Pr")) {
            Nobility += "c";
        }

        if (TradeCodes.Contains("Ag") || TradeCodes.Contains("Ri")) {
            Nobility += "C";
        }

        if (TradeCodes.Contains("Pi")) {
            Nobility += "D";
        }

        if (TradeCodes.Contains("Ph")) {
            Nobility += "e";
        }

        if (TradeCodes.Contains("In") || TradeCodes.Contains("Hi")) {
            Nobility += "E";
        }

        if (Importance >= 4 && !TradeCodes.Contains("Cp") && !TradeCodes.Contains("Cs") && !TradeCodes.Contains("Cx")) {
            Nobility += "f";
        }

        if (TradeCodes.Contains("Cp") || TradeCodes.Contains("Cs")) {
            Nobility += "F";
        }

        if (TradeCodes.Contains("Cx")) {
            Nobility += "G";
        }

        if (Nobility == "") Nobility = "-";
    }

    public void GenerateImportance()
    {
        Importance = Starport.Class switch {
            StarportClass.A or StarportClass.B or StarportClass.F => 1,
            StarportClass.D or StarportClass.E or StarportClass.X or StarportClass.H or StarportClass.Y => -1,
            _ => 0
        };

        Importance += TechLevel switch {
            >= 16 => 2,
            >= 10 => 1,
            <= 8 => -1,
            _ => 0
        };

        if (TradeCodes.Contains("Ag")) Importance++;
        if (TradeCodes.Contains("Hi")) Importance++;
        if (TradeCodes.Contains("In")) Importance++;
        if (TradeCodes.Contains("Ri")) Importance++;

        Importance -= Population <= 6 ? 1 : 0;

        if (Bases.Contains(Base.Naval) && Bases.Contains(Base.Scout)) Importance++;
        if (Bases.Contains(Base.WayStation)) Importance++;
    }

    public void GenerateTemperature(SpectralType primaryStarSpectralType)
    {
        switch (WorldType) {
            case WorldType.Snowball:
                Temperature = Temperature.Frozen;
                return;
            case WorldType.Meltball:
                Temperature = Temperature.Boiling;
                return;
        }

        var tempValue = GetTempValue(primaryStarSpectralType);

        Temperature = tempValue switch {
            <= 2 => Temperature.Frozen,
            <= 4 => Temperature.Cold,
            <= 5 => Temperature.Cool,
            <= 8 => Temperature.Temperate,
            <= 9 => Temperature.Warm,
            <= 11 => Temperature.Hot,
            _ => Temperature.Boiling
        };
    }

    private int GetTempValue(SpectralType primaryStarSpectralType)
    {
        var value = _rollingService.D6(2);

        value += Atmosphere switch {
            0 or 1 => 0,
            2 or 3 => -2,
            4 or 5 or 14 => -1,
            6 or 7 => 0,
            8 or 9 => +1,
            10 or 13 or 15 => 2,
            11 or 12 => 6,
            _ => 0
        };

        value += PlanetOrbit switch {
            PlanetOrbit.Epistellar => _rollingService.D6(1),
            PlanetOrbit.Inner => 0,
            PlanetOrbit.Outer => -_rollingService.D6(1),
            _ => throw new ArgumentOutOfRangeException()
        };

        value += primaryStarSpectralType switch {
            SpectralType.O => 5,
            SpectralType.B => 4,
            SpectralType.A => 3,
            SpectralType.F => 2,
            SpectralType.G => 1,
            SpectralType.K => 0,
            SpectralType.M => -1,
            SpectralType.BD => -4,
            SpectralType.D => -2,
            SpectralType.NS => -3,
            SpectralType.BH => -5,
            SpectralType.PSR => -3
        };

        return value;
    }

    public void Populate(Star primaryStar)
    {
        if (Biosphere >= 12) {
            PopulateNatives(primaryStar);
        }

        foreach (var race in Races.races) {
            var roll = _rollingService.D6(3);
            var distanceBetween = DistanceService.DistanceBetween(Coordinates, race.HomeworldCoordinates);

            if (distanceBetween < race.ExpansionRate) {
                var distancePenalty = distanceBetween / (race.ExpansionRate / 2);

                var atmospherePenalty = Math.Abs(race.Homeworld.Atmosphere - Atmosphere);
                var hydrographicPenalty = Math.Abs(race.Homeworld.Hydrographics - Hydrographics) / 2;
                var temperaturePenalty = Math.Abs(race.Homeworld.Temperature - Temperature) * 2;
                var climatePenalty = atmospherePenalty + hydrographicPenalty + temperaturePenalty - race.Resilience;

                var populationPenalty = distancePenalty * climatePenalty;

                var newPopulation = (int) (roll - populationPenalty);

                roll = _rollingService.Flux() / 2 + 1;
                var planetCapacity = Math.Max(Size + roll, 0);
                if (planetCapacity < newPopulation) {
                    newPopulation = planetCapacity;
                }

                if (newPopulation > 0) {
                    Populations.Add(new Population {
                        PopulationNumber = Math.Min(newPopulation, race.MaxPopulation),
                        Race = race.Allegiance,
                        Distance = distanceBetween
                    });
                    var mostPopulatedRace = Races.races.Find(x => x.Allegiance == Populations.MaxBy(y => y.PopulationNumber).Race);

                    if (Population >= 10) {
                        int i = 0;
                    }

                    SetGovernment(mostPopulatedRace);
                    GenerateLawLevel(mostPopulatedRace);
                    GenerateBases();
                    TradeCodes = TradeCodeService.AddTradeCodes(this);
                    GenerateSpaceport();
                    GenerateTechLevel();
                    SetTravelZone();
                    GenerateCulturalDifferences();

                    if (Populations.Count > 0) {
                        var largestPopulation = Populations.Max(x => x.PopulationNumber);
                        var shortestDistance = 100;

                        foreach (var population in Populations.Where(population => population.PopulationNumber == largestPopulation)) {
                            if (population.PopulationNumber == largestPopulation) {
                                if (population.Distance < shortestDistance) {
                                    Allegiance = population.Race;
                                    shortestDistance = population.Distance;
                                }
                            } else {
                                Allegiance = population.Race;
                                shortestDistance = population.Distance;
                            }
                        }
                    }
                }
            }
        }
    }

    private void PopulateNatives(Star primaryStar)
    {
        int population;
        if (Chemistry == PlanetChemistry.Water) {
            population = Math.Max(getDesirability(primaryStar) + _rollingService.D3(1) - _rollingService.D3(1), 0);
        } else {
            population = _rollingService.D6(2) - 2;
        }

        Populations.Add(new Population {
            PopulationNumber = population,
            Distance = 0,
            Race = "Ntvs"
        });

        GenerateNativeGovernment(population);

        GenerateNativeLawLevel();

        int industry;
        if (Population == 0) {
            industry = 0;
        } else {
            industry = Government + _rollingService.Flux();
            industry += LawLevel switch {
                >= 1 and <= 3 => 1,
                >= 6 and <= 9 => -1,
                >= 10 and <= 12 => -2,
                >= 13 => -3,
                _ => 0
            };
            if (Atmosphere is <= 4 or 7 or >= 9 || Hydrographics == 15) {
                industry++;
            }

            industry += TechLevel switch {
                >= 12 and <= 14 => 1,
                >= 15 => 2,
                _ => 0
            };
        }

        switch (industry) {
            case >= 4 and <= 9:
                Populations.First(x => x.Race == "Ntvs").PopulationNumber++;
                Atmosphere = Atmosphere switch {
                    3 => 2,
                    5 => 4,
                    6 => 7,
                    8 => 9,
                    _ => Atmosphere
                };
                break;
            case >= 10:
                if (Atmosphere is 3 or 5 or 6 or 8) {
                    Populations.First(x => x.Race == "Ntvs").PopulationNumber += 2;
                    Atmosphere = Atmosphere switch {
                        3 => 2,
                        5 => 4,
                        6 => 7,
                        8 => 9
                    };
                } else {
                    Populations.First(x => x.Race == "Ntvs").PopulationNumber++;
                }

                break;
        }

        TradeCodes = TradeCodeService.AddTradeCodes(this);

        GenerateSpaceport();
    }

    private void GenerateNativeLawLevel()
    {
        if (Government == 0) {
            LawLevel = 0;
        } else {
            LawLevel = Math.Max(Population + _rollingService.Flux(), 0);
        }
    }

    private void GenerateNativeGovernment(int population)
    {
        GenerateTechLevel();

        if (TechLevel == 0) {
            Government = 0;
        } else {
            if (_rollingService.D6(1) <= TechLevel - 9) {
                Government = 7;
            } else {
                Government = Math.Max(population + _rollingService.Flux(), 0);
            }
        }
    }

    private int getDesirability(Star primaryStar)
    {
        var desirability = 0;
        if (primaryStar is {SpectralType: SpectralType.M, LuminosityClass: LuminosityClass.Ve}) desirability -= _rollingService.D3(1);
        if (PlanetType == PlanetType.AsteroidBelt) {
            if (PlanetOrbit == PlanetOrbit.Inner) {
                if (primaryStar is {SpectralType: SpectralType.M, LuminosityClass: LuminosityClass.V}) desirability += 1;
                else if (primaryStar.LuminosityClass is LuminosityClass.IV or LuminosityClass.VI) desirability += 2;
            }
        } else {
            if (Hydrographics == 0) desirability -= 1;
            if (Size >= 13 || Atmosphere is >= 12 and <= 16 || Hydrographics == 15) desirability -= 2;
            if (primaryStar.LuminosityClass == LuminosityClass.Ve) desirability -= _rollingService.D3(1);
            if (Size is >= 5 and <= 10 && Atmosphere is >= 4 and <= 9 && Hydrographics is >= 4 and <= 8) desirability += 5;
            else if (Hydrographics is 10 or 12) desirability += 3;
            else if (Atmosphere is >= 2 and <= 6 && Hydrographics <= 3) desirability += 2;
            else desirability += 4;
            if (Size >= 10 && Atmosphere <= 15) desirability -= 1;
            if (PlanetOrbit == PlanetOrbit.Inner) {
                if (primaryStar is {SpectralType: SpectralType.M, LuminosityClass: LuminosityClass.V}) desirability += 1;
                else if (primaryStar.LuminosityClass is LuminosityClass.IV or LuminosityClass.VI) desirability += 2;
            }

            if (Size == 0) desirability -= 1;
            if (Atmosphere is >= 6 and <= 8) desirability += 1;
        }

        return desirability;
    }

    private void GenerateTechLevel()
    {
        if (Population == 0) {
            TechLevel = 0;
            return;
        }

        TechLevel = _rollingService.D6(1);

        TechLevel += Starport.Class switch {
            StarportClass.A => 6,
            StarportClass.B => 4,
            StarportClass.C => 2,
            StarportClass.D => 0,
            StarportClass.E => -2,
            StarportClass.X => -4,
            _ => 0
        };

        TechLevel += Size switch {
            <= 1 => 2,
            <= 4 => 1,
            _ => 0
        };

        TechLevel += Atmosphere switch {
            <= 3 => 1,
            >= 10 => 1,
            _ => 0
        };

        TechLevel += Hydrographics switch {
            0 => 1,
            9 => 1,
            >= 10 => 2,
            _ => 0
        };

        TechLevel += Population switch {
            >= 0 and <= 5 => 1,
            9 => 1,
            10 => 2,
            11 => 3,
            >= 12 => 4,
            _ => 0
        };

        TechLevel += Government switch {
            0 => 1,
            5 => 1,
            7 => 2,
            13 => -2,
            14 => -2,
            _ => 0
        };

        if (TechLevel < 0) {
            TechLevel = 0;
        }
    }

    public void GenerateBases()
    {
        if (Population == 0) return;

        switch (Starport.Class) {
            case StarportClass.A:
                if (_rollingService.D6(2) >= 8) {
                    Bases.Add(Base.Naval);
                }

                if (_rollingService.D6(2) >= 10) {
                    Bases.Add(Base.Scout);
                }

                if (_rollingService.D6(2) >= 8) {
                    TradeCodes.Add(TradeCode.ResearchStation);
                }

                Starport.Installations.Add(StarportInstallation.TAS);

                break;
            case StarportClass.B:
                if (_rollingService.D6(2) >= 8) {
                    Bases.Add(Base.Naval);
                }

                if (_rollingService.D6(2) >= 8) {
                    Bases.Add(Base.Scout);
                }

                if (_rollingService.D6(2) >= 10) {
                    TradeCodes.Add(TradeCode.ResearchStation);
                }

                Starport.Installations.Add(StarportInstallation.TAS);
                break;
            case StarportClass.C:
                if (_rollingService.D6(2) >= 8) {
                    Bases.Add(Base.Scout);
                }

                if (_rollingService.D6(2) >= 10) {
                    TradeCodes.Add(TradeCode.ResearchStation);
                }

                if (_rollingService.D6(2) >= 10) {
                    Starport.Installations.Add(StarportInstallation.TAS);
                }

                break;
            case StarportClass.D:
                if (_rollingService.D6(2) >= 7) {
                    Bases.Add(Base.Scout);
                }

                break;
        }
    }

    public void GenerateStarport()
    {
        if (Population == 0) {
            Starport.Class = StarportClass.X;
            return;
        }

        var starportRoll = _rollingService.Flux() + Population;
        if (TradeCodes.Contains(TradeCode.Agricultural)) starportRoll++;
        if (TradeCodes.Contains(TradeCode.Garden)) starportRoll++;
        if (TradeCodes.Contains(TradeCode.HighPopulation)) starportRoll++;
        if (TradeCodes.Contains(TradeCode.Industrial)) starportRoll++;
        if (TradeCodes.Contains(TradeCode.NonAgricultural)) starportRoll++;
        if (TradeCodes.Contains(TradeCode.Rich)) starportRoll++;
        if (TradeCodes.Contains(TradeCode.LowPopulation)) starportRoll--;
        if (TradeCodes.Contains(TradeCode.Poor)) starportRoll--;
        if (TechLevel >= 15) starportRoll += 2;
        if (TechLevel <= 9) starportRoll--;

        GenerateSpaceport();

        Starport.Class = starportRoll switch {
            <= 2 => StarportClass.X,
            <= 4 => StarportClass.E,
            <= 6 => StarportClass.D,
            <= 8 => StarportClass.C,
            <= 10 => StarportClass.B,
            _ => StarportClass.A
        };

        Starport.GenerateSpecialFeature();
        Starport.GenerateEvent();
        Starport.GenerateEnforcement(LawLevel);

        if (Starport.Installations.Contains(StarportInstallation.ArmyBase)) {
            Bases.Add(Base.Military);
        }

        if (Starport.Installations.Contains(StarportInstallation.NavalBase)) {
            Bases.Add(Base.Naval);
        }

        if (Starport.Installations.Contains(StarportInstallation.NavalDepot)) {
            Bases.Add(Base.NavalDepot);
        }

        if (Starport.Installations.Contains(StarportInstallation.ExplorerBase)) {
            Bases.Add(Base.Exploration);
        }

        if (Starport.Installations.Contains(StarportInstallation.WayStation)) {
            Bases.Add(Base.WayStation);
        }

        Starport.GenerateDefenses(Bases.Contains(Base.Naval) || Bases.Contains(Base.Military), Bases.Contains(Base.NavalDepot));
    }

    public new void GenerateSpaceport()
    {
        if (Population == 0) {
            Starport.Class = StarportClass.Y;
            return;
        }

        var starportRoll = _rollingService.Flux() + Population;
        if (TradeCodes.Contains(TradeCode.Agricultural)) starportRoll++;
        if (TradeCodes.Contains(TradeCode.Garden)) starportRoll++;
        if (TradeCodes.Contains(TradeCode.HighPopulation)) starportRoll++;
        if (TradeCodes.Contains(TradeCode.Industrial)) starportRoll++;
        if (TradeCodes.Contains(TradeCode.NonAgricultural)) starportRoll++;
        if (TradeCodes.Contains(TradeCode.Rich)) starportRoll++;
        if (TradeCodes.Contains(TradeCode.LowPopulation)) starportRoll--;
        if (TradeCodes.Contains(TradeCode.Poor)) starportRoll--;
        if (TechLevel >= 15) starportRoll += 2;
        if (TechLevel <= 9) starportRoll--;

        Starport.Class = starportRoll switch {
            <= 4 => StarportClass.Y,
            <= 6 => StarportClass.H,
            <= 8 => StarportClass.G,
            _ => StarportClass.F
        };
    }

    private void GenerateLawLevel(Race race)
    {
        if (Population == 0) {
            LawLevel = 0;
            return;
        }

        if (race != null) {
            LawLevel = race.Homeworld.LawLevel + _rollingService.Flux();
        } else {
            GenerateNativeLawLevel();
        }

        if (LawLevel < 0) LawLevel = 0;
        if (LawLevel > 24) LawLevel = 24;
    }

    private void SetGovernment(Race race)
    {
        if (Population == 0) {
            Government = 0;
            return;
        }

        if (race != null) {
            Government = race.Homeworld.Government + _rollingService.Flux();
        } else {
            GenerateNativeGovernment(Populations.First(x => x.Race == "Ntvs").PopulationNumber);
        }

        if (Government < 0) Government = 0;
        if (Government > 15) Government = 15;
    }

    public void SetTravelZone()
    {
        if (TradeCodes.Contains("Da")
            || TradeCodes.Contains("Pz")) {
            TravelZone = TravelZone.Amber;
        }

        if (TradeCodes.Contains("Fo")) {
            TravelZone = TravelZone.Red;
        }
    }

    public void GenerateCulturalDifferences()
    {
        var numDifferences = ExtendedHex.Reverse(CulturalExtension[3]) / 2;

        while (numDifferences > CulturalDifferences.Count) {
            switch (_rollingService.D66()) {
                case 11:
                    CulturalDifferences.Add(CulturalDifference.Sexist);
                    break;
                case 12:
                    CulturalDifferences.Add(CulturalDifference.Religious);
                    break;
                case 13:
                    CulturalDifferences.Add(CulturalDifference.Artistic);
                    break;
                case 14:
                    CulturalDifferences.Add(CulturalDifference.Ritualized);
                    break;
                case 15:
                    CulturalDifferences.Add(CulturalDifference.Conservative);
                    break;
                case 16:
                    CulturalDifferences.Add(CulturalDifference.Xenophobic);
                    break;
                case 21:
                    CulturalDifferences.Add(CulturalDifference.Taboo);
                    break;
                case 22:
                    CulturalDifferences.Add(CulturalDifference.Deceptive);
                    break;
                case 23:
                    CulturalDifferences.Add(CulturalDifference.Liberal);
                    break;
                case 24:
                    CulturalDifferences.Add(CulturalDifference.Honourable);
                    break;
                case 25:
                    CulturalDifferences.Add(CulturalDifference.Influenced);
                    break;
                case 26:
                    numDifferences++;
                    break;
                case 31:
                    CulturalDifferences.Add(CulturalDifference.Barbaric);
                    break;
                case 32:
                    CulturalDifferences.Add(CulturalDifference.Remnant);
                    break;
                case 33:
                    CulturalDifferences.Add(CulturalDifference.Degenerate);
                    break;
                case 34:
                    CulturalDifferences.Add(CulturalDifference.Progressive);
                    break;
                case 35:
                    CulturalDifferences.Add(CulturalDifference.Recovering);
                    break;
                case 36:
                    CulturalDifferences.Add(CulturalDifference.Nexus);
                    break;
                case 41:
                    CulturalDifferences.Add(CulturalDifference.TouristAttraction);
                    break;
                case 42:
                    CulturalDifferences.Add(CulturalDifference.Violent);
                    break;
                case 43:
                    CulturalDifferences.Add(CulturalDifference.Peaceful);
                    break;
                case 44:
                    CulturalDifferences.Add(CulturalDifference.Obsessed);
                    break;
                case 45:
                    CulturalDifferences.Add(CulturalDifference.Fashion);
                    break;
                case 46:
                    CulturalDifferences.Add(CulturalDifference.AtWar);
                    break;
                case 51:
                    CulturalDifferences.Add(CulturalDifference.OffworldersCustom);
                    break;
                case 52:
                    CulturalDifferences.Add(CulturalDifference.StarportCustom);
                    break;
                case 53:
                    CulturalDifferences.Add(CulturalDifference.MediaCustom);
                    break;
                case 54:
                    CulturalDifferences.Add(CulturalDifference.TechnologyCustom);
                    break;
                case 55:
                    CulturalDifferences.Add(CulturalDifference.LifecycleCustom);
                    break;
                case 56:
                    CulturalDifferences.Add(CulturalDifference.SocialStandingCustom);
                    break;
                case 61:
                    CulturalDifferences.Add(CulturalDifference.TradeCustom);
                    break;
                case 62:
                    CulturalDifferences.Add(CulturalDifference.NobilityCustom);
                    break;
                case 63:
                    CulturalDifferences.Add(CulturalDifference.SexCustom);
                    break;
                case 64:
                    CulturalDifferences.Add(CulturalDifference.EatingCustom);
                    break;
                case 65:
                    CulturalDifferences.Add(CulturalDifference.TravelCustom);
                    break;
                case 66:
                    CulturalDifferences.Add(CulturalDifference.ConspiracyCustom);
                    break;
            }
        }
    }
}

public enum CulturalDifference
{
    Sexist,
    Religious,
    Artistic,
    Ritualized,
    Conservative,
    Xenophobic,
    Taboo,
    Deceptive,
    Liberal,
    Honourable,
    Influenced,
    Barbaric,
    Remnant,
    Degenerate,
    Progressive,
    Recovering,
    Nexus,
    TouristAttraction,
    Violent,
    Peaceful,
    Obsessed,
    Fashion,
    AtWar,
    OffworldersCustom,
    StarportCustom,
    MediaCustom,
    TechnologyCustom,
    LifecycleCustom,
    SocialStandingCustom,
    TradeCustom,
    NobilityCustom,
    SexCustom,
    EatingCustom,
    TravelCustom,
    ConspiracyCustom
}

public class Population
{
    public int PopulationNumber { get; set; }
    public string Race { get; set; }
    public int Distance { get; set; }
}