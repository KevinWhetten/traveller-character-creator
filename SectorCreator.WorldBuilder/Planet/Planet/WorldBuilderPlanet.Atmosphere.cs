using SectorCreator.Global.Enums;
using SectorCreator.WorldBuilder.Enums;
using SectorCreator.WorldBuilder.Planet.Planet.OtherObjects;
using SectorCreator.WorldBuilder.Services;

namespace SectorCreator.WorldBuilder.Planet.Planet;

public partial class WorldBuilderPlanet
{
    public int Atmosphere { get; set; }
    public int AtmosphereSub { get; set; }
    public double BAR { get; set; }
    public double OxygenFraction { get; set; }
    public double PartialPressureOfOxygen => OxygenFraction * BAR;
    public List<AtmosphereTaint> AtmosphereTaints { get; set; } = new();
    public List<Taint> AtmosphereHazards { get; set; } = new();
    public double ScaleHeight => Gravity > 0 ? (8.5 * Temperature) / (Gravity * 288.0)  : 0;
    public double GetPressure(double height) => BAR / Math.Pow(Math.E, height / ScaleHeight);
    public List<UnusualSubtype> UnusualSubtypes { get; set; } = new();
    public List<PlanetElement> AtmosphereChemicals { get; set; } = new();
    public bool RunawayGreenhouse { get; set; }

    private void GenerateAtmosphereDetails(WorldBuilderStarSystem starSystem)
    {
        if (PlanetType != PlanetType.AsteroidBelt) {
            GenerateOxygenFraction(starSystem);
            GenerateTaint();
        }
    }

    private void GenerateOxygenFraction(WorldBuilderStarSystem starSystem)
    {
        if (Atmosphere is (>= 2 and <= 9) or 13 or 14) {
            var dm = 0;

            dm += starSystem.Age switch {
                <= 2 => -4,
                <= 3 => -2,
                <= 3.5 => -1,
                <= 4 => 0,
                > 4 => 1,
                _ => throw new ArgumentOutOfRangeException()
            };

            OxygenFraction = (_rollingService.D6(1) + dm) / 20.0 + (_rollingService.D6(2) - 7) / 100.0 + (_rollingService.D6(1) - 1) / 20.0;

            if (OxygenFraction <= 0) {
                OxygenFraction = _rollingService.D6(1) * 0.01;
            }
        }
    }

    private void GenerateTaint()
    {
        if (Atmosphere <= 3) {
            AtmosphereTaints.Add(new AtmosphereTaint
                {Taint = Taint.LowOxygen, Severity = GenerateSeverity(Taint.LowOxygen), Persistence = GeneratePersistence(Taint.LowOxygen)});
        } else if (Atmosphere == 13) {
            AtmosphereTaints.Add(new AtmosphereTaint
                {Taint = Taint.HighOxygen, Severity = GenerateSeverity(Taint.HighOxygen), Persistence = GeneratePersistence(Taint.HighOxygen)});
        } else if (Atmosphere == 12) {
            GenerateHazard();
        }

        if (IsTainted()) {
            var dm = 0;
            if (Atmosphere == 4) dm -= 2;
            if (Atmosphere == 9) dm += 2;

            switch (_rollingService.D6(2) + dm) {
                case <= 2:
                    if (AtmosphereTaints.Select(x => x.Taint).Contains(Taint.LowOxygen)
                        || AtmosphereTaints.Select(x => x.Taint).Contains(Taint.HighOxygen)) {
                        GenerateTaint();
                    } else {
                        AtmosphereTaints.Add(new AtmosphereTaint {
                            Taint = Taint.LowOxygen, Severity = GenerateSeverity(Taint.LowOxygen), Persistence = GeneratePersistence(Taint.LowOxygen)
                        });
                    }

                    break;
                case 3 or 11:
                    AtmosphereTaints.Add(new AtmosphereTaint
                        {Taint = Taint.Radioactivity, Severity = GenerateSeverity(), Persistence = GeneratePersistence()});
                    break;
                case 4 or 9:
                    AtmosphereTaints.Add(new AtmosphereTaint
                        {Taint = Taint.Biologic, Severity = GenerateSeverity(), Persistence = GeneratePersistence()});
                    break;
                case 5 or 7:
                    AtmosphereTaints.Add(new AtmosphereTaint
                        {Taint = Taint.GasMix, Severity = GenerateSeverity(), Persistence = GeneratePersistence()});
                    break;
                case 6:
                    AtmosphereTaints.Add(new AtmosphereTaint
                        {Taint = Taint.Particulates, Severity = GenerateSeverity(), Persistence = GeneratePersistence()});
                    break;
                case 8:
                    AtmosphereTaints.Add(Temperature >= 273
                        ? new AtmosphereTaint {Taint = Taint.SulphurCompounds, Severity = GenerateSeverity(), Persistence = GeneratePersistence()}
                        : new AtmosphereTaint {Taint = Taint.Particulates, Severity = GenerateSeverity(), Persistence = GeneratePersistence()});

                    break;
                case 10:
                    AtmosphereTaints.Add(new AtmosphereTaint
                        {Taint = Taint.Particulates, Severity = GenerateSeverity(), Persistence = GeneratePersistence()});
                    GenerateTaint();
                    break;
                case >= 12:
                    if (AtmosphereTaints.Select(x => x.Taint).Contains(Taint.LowOxygen)
                        || AtmosphereTaints.Select(x => x.Taint).Contains(Taint.HighOxygen)) {
                        GenerateTaint();
                    } else {
                        AtmosphereTaints.Add(new AtmosphereTaint {
                            Taint = Taint.HighOxygen, Severity = GenerateSeverity(Taint.HighOxygen),
                            Persistence = GeneratePersistence(Taint.HighOxygen)
                        });
                    }

                    break;
            }
        }
    }

    private int GenerateSeverity(Taint taint = Taint.None)
    {
        if (taint == Taint.LowOxygen) {
            return PartialPressureOfOxygen switch {
                >= 0.09 => 2,
                >= 0.08 => 8,
                _ => 9
            };
        }

        if (taint == Taint.HighOxygen) {
            return PartialPressureOfOxygen switch {
                <= 0.6 => 2,
                <= 0.7 => 8,
                _ => 9
            };
        }

        var dm = 0;
        if (Atmosphere == 12) dm += 6;

        return (_rollingService.D6(2) + dm) switch {
            <= 4 => 1,
            5 => 2,
            6 => 3,
            7 => 4,
            8 => 5,
            9 => 6,
            10 => 7,
            11 => 8,
            >= 12 => 9
        };
    }

    private int GeneratePersistence(Taint taint = Taint.None)
    {
        var dm = 0;

        if (taint is Taint.LowOxygen or Taint.HighOxygen) dm += 4;

        if (Atmosphere == 12) dm += 6;

        var result = _rollingService.D6(2) + dm;
        return result switch {
            <= 2 => 2,
            <= 8 => result,
            >= 9 => 9
        };
    }

    private void GenerateHazard()
    {
        var dm = 0;
        if (Atmosphere == 4) dm -= 2;
        if (Atmosphere == 9) dm += 2;

        switch (_rollingService.D6(2) + dm) {
            case <= 2:
                if (AtmosphereHazards.Contains(Taint.LowOxygen)
                    || AtmosphereHazards.Contains(Taint.HighOxygen)) {
                    GenerateTaint();
                } else {
                    AtmosphereHazards.Add(Taint.LowOxygen);
                }

                break;
            case 3 or 11:
                AtmosphereHazards.Add(Taint.Radioactivity);
                break;
            case 4 or 9:
                AtmosphereHazards.Add(Taint.Biologic);
                break;
            case 5 or 7:
                AtmosphereHazards.Add(Taint.GasMix);
                break;
            case 6:
                AtmosphereHazards.Add(Taint.Particulates);
                break;
            case 8:
                AtmosphereHazards.Add(Temperature >= 273
                    ? Taint.SulphurCompounds
                    : Taint.Particulates);

                break;
            case 10:
                AtmosphereHazards.Add(Taint.Particulates);
                GenerateHazard();
                break;
            case >= 12:
                if (AtmosphereHazards.Contains(Taint.LowOxygen)
                    || AtmosphereHazards.Contains(Taint.HighOxygen)) {
                    GenerateHazard();
                } else {
                    AtmosphereHazards.Add(Taint.HighOxygen);
                }

                break;
        }
    }

    private void GenerateAtmosphereMakeup()
    {
        var atmosphereAbundanceTotal = AtmosphereChemicals.Select(x => x.Element).Sum(x => x.RelativeAbundance);

        foreach (var planetElement in AtmosphereChemicals) {
            planetElement.Percent =
                _rollingService.ValueWithVariance(((double) planetElement.Element.RelativeAbundance / atmosphereAbundanceTotal) * 100, 5);
        }

        if (AtmosphereChemicals.Count == 0) {
            AtmosphereChemicals.Add(new PlanetElement {Element = CompositionService.GetRandomElement(), Percent = _rollingService.Percent() * .75});
            AtmosphereChemicals.Add(new PlanetElement {Element = CompositionService.GetRandomElement(), Percent = _rollingService.Percent() * .5});
            AtmosphereChemicals.Add(new PlanetElement {Element = CompositionService.GetRandomElement(), Percent = _rollingService.Percent() * .33});
        }

        AtmosphereChemicals = AtmosphereChemicals.OrderByDescending(x => x.Percent).ToList();

        var percentDifference = 100 - AtmosphereChemicals.Sum(x => x.Percent);
        AtmosphereChemicals.First().Percent += percentDifference;

        AtmosphereChemicals = AtmosphereChemicals.OrderByDescending(x => x.Percent).ToList();
    }

    private bool IsTainted()
    {
        return Atmosphere switch {
            2 or 4 or 7 or 9 => true,
            10 or 11 or 12 => AtmosphereSub switch {
                2 or 4 or 7 or 9 or 11 or 14 => true,
                _ => false
            },
            _ => false
        };
    }
}