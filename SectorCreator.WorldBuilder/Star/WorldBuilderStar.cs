using SectorCreator.Global;
using SectorCreator.Global.Enums;

namespace SectorCreator.WorldBuilder.Star;

public interface IWorldBuilderStar
{ }

public class WorldBuilderStar : IWorldBuilderStar
{
    private readonly IRollingService _rollingService;
    private readonly WorldBuilderStarMassCalculator _worldBuilderStarMassCalculator;

    public Guid Id = Guid.NewGuid();
    public string Component { get; set; }
    public SpectralType SpectralType { get; set; }
    public int SpectralSubclass { get; set; }
    public LuminosityClass LuminosityClass { get; set; }
    public StarSpecialType SpecialType { get; set; }
    public StarType StarType { get; set; }
    public double Mass { get; set; }
    public int Temperature { get; set; }
    public double Diameter { get; set; }
    public double Luminosity => Math.Pow(Diameter, 2) * Math.Pow(Temperature / 5772.0, 4);
    public double Age { get; set; }
    public WorldBuilderStar? CompanionStar { get; set; }
    public double OrbitNumber { get; set; }
    public double Eccentricity { get; set; }
    public double OrbitDistance => GetOrbitDistance();
    public double MinimumSeparation => OrbitDistance * (1 - Eccentricity);
    public double MaximumSeparation => OrbitDistance * (1 + Eccentricity);
    public readonly List<double> AvailableOrbits = new();
    public double MAO => AvailableOrbits.FirstOrDefault();
    public double HZCO { get; set; }
    public string Name { get; set; } = "";
    public string Class => $"{SpectralType}{SpectralSubclass} {LuminosityClass}";
    public double Period { get; set; }

    private double GetOrbitDistance()
    {
        return Math.Floor(OrbitNumber) switch {
            0 => 0 + (OrbitNumber * .4),
            1 => .4 + (OrbitNumber - 1) * .3,
            2 => .7 + (OrbitNumber - 2) * .3,
            3 => 1 + (OrbitNumber - 3) * .6,
            4 => 1.6 + (OrbitNumber - 4) * 1.2,
            5 => 2.8 + (OrbitNumber - 5) * 2.4,
            6 => 5.2 + (OrbitNumber - 6) * 4.8,
            7 => 10 + (OrbitNumber - 7) * 10,
            8 => 20 + (OrbitNumber - 8) * 20,
            9 => 40 + (OrbitNumber - 9) * 37,
            10 => 77 + (OrbitNumber - 10) * 77,
            11 => 154 + (OrbitNumber - 11) * 154,
            12 => 308 + (OrbitNumber - 12) * 307,
            13 => 615 + (OrbitNumber - 13) * 615,
            14 => 1230 + (OrbitNumber - 14) * 1270,
            15 => 2500 + (OrbitNumber - 15) * 2400,
            16 => 4900 + (OrbitNumber - 16) * 4900,
            17 => 9800 + (OrbitNumber - 17) * 9700,
            18 => 19500 + (OrbitNumber - 18) * 20000,
            19 => 39500 + (OrbitNumber - 19) * 39200,
            20 => 78700,
            _ => 0
        };
    }

    public WorldBuilderStar()
    { }

    public WorldBuilderStar(IRollingService rollingService, StarType starType = StarType.Primary, double orbitsAroundMass = 0, char currentComponent = 'A')
    {
        StarType = starType;
        _rollingService = rollingService;
        _worldBuilderStarMassCalculator = new WorldBuilderStarMassCalculator(new RollingService(), this);
        var worldBuilderStarTemperatureCalculator = new WorldBuilderStarTemperatureCalculator(new RollingService(), this);
        var worldBuilderStarDiameterCalculator = new WorldBuilderStarDiameterCalculator(new RollingService(), this);

        LuminosityClass = LuminosityClass.V;

        GenerateSpectralType(_rollingService.D6(2));
        if (LuminosityClass == LuminosityClass.BD) {
            GenerateBrownDwarf();
        } else if (LuminosityClass == LuminosityClass.D) {
            GenerateWhiteDwarf();
        } else if (SpectralType is SpectralType.PSR or SpectralType.NS) {
            GenerateNeutronStar();
        } else if (SpectralType is SpectralType.BH) {
            GenerateBlackHole();
        } else {
            GenerateSpectralSubclass();
            Mass = _worldBuilderStarMassCalculator.GenerateMass();
            Temperature = worldBuilderStarTemperatureCalculator.GenerateTemperature();
            Diameter = worldBuilderStarDiameterCalculator.GenerateDiameter();
            GenerateAge();
        }
        

        GenerateOrbitNum();
        GeneratePeriod(orbitsAroundMass);

        if (StarType != StarType.Companion) {
            GenerateCompanionStar(currentComponent);
        }

        if (CompanionStar != null) {
            Component = currentComponent + "a";
        } else if (StarType == StarType.Companion) {
            Component = currentComponent + "b";
        } else {
            Component = currentComponent.ToString();
        }
        GenerateEccentricity();
    }

    private void GeneratePeriod(double orbitsAroundMass)
    {
        Period = Math.Sqrt(Math.Pow(OrbitDistance, 3) / (Mass + orbitsAroundMass));
    }


    private void GenerateEccentricity()
    {
        var roll = _rollingService.D6(2) + 2 + (StarType != StarType.Primary ? 1 : 0) + (StarType == StarType.Companion ? 1 : 0) -
                   (OrbitNumber < 1.0 && Age >= 1 ? 1 : 0);
        Eccentricity = roll switch {
            <= 5 => -0.001 + (_rollingService.D6(1) / 1000.0),
            <= 7 => 0.00 + (_rollingService.D6(1) / 200.0),
            <= 9 => 0.03 + (_rollingService.D6(1) / 100.0),
            10 => 0.05 + (_rollingService.D6(1) / 20.0),
            11 => 0.05 + (_rollingService.D6(2) / 20.0),
            >= 12 => 0.3 + (_rollingService.D6(2) / 20.0)
        };
    }

    private void GenerateOrbitNum()
    {
        OrbitNumber = StarType switch {
            StarType.Primary => 0,
            StarType.Close => _rollingService.D6(1) - 1,
            StarType.Near => _rollingService.D6(1) + 5,
            StarType.Far => _rollingService.D6(1) + 11,
            StarType.Companion => (_rollingService.D6(1) / 10.0) + ((_rollingService.D6(2) - 7) / 100.0),
            _ => OrbitNumber
        };
        switch (OrbitNumber) {
            case 0 when StarType != StarType.Primary:
                OrbitNumber = .5 + _rollingService.D10(1) / 20.0;
                break;
            case >= 1:
                OrbitNumber = OrbitNumber - .5 + (_rollingService.D10(1) / 10.0);
                break;
        }
    }

    private void GenerateBlackHole()
    {
        var roll = _rollingService.D6(1);
        Mass = 2.1 + roll - 1 + (_rollingService.D10(10) / 10.0);
        while (roll == 6) {
            roll = _rollingService.D6(1);
            Mass += roll;
        }

        Diameter = 5.9 * Mass / 1391400.0;
        var mainSequenceLifespan = 10 / Math.Pow(Mass, 2.5);
        var subgiantLifespan = mainSequenceLifespan / (4 + Mass);
        var giantLifespan = mainSequenceLifespan / (10 * Math.Pow(Mass, 3));
        Age = mainSequenceLifespan + subgiantLifespan + giantLifespan + _rollingService.D6(1) * 2 + _rollingService.D3(1) - 2 +
              (_rollingService.D10(1) / 10.0);
    }

    private void GenerateNeutronStar()
    {
        var roll = _rollingService.D6(1);
        Mass = 1 + (roll / 10.0) + (roll == 6 ? (_rollingService.D6(1) - 1) / 10.0 : 0);
        Diameter = (19 + _rollingService.D6(1)) / 1391400.0;
        var mainSequenceLifespan = 10 / Math.Pow(Mass, 2.5);
        var subgiantLifespan = mainSequenceLifespan / (4 + Mass);
        var giantLifespan = mainSequenceLifespan / (10 * Math.Pow(Mass, 3));
        Age = mainSequenceLifespan + subgiantLifespan + giantLifespan + _rollingService.D6(1) * 2 + _rollingService.D3(1) - 2 +
              (_rollingService.D10(1) / 10.0);

        double massRatio = Mass / .6;
        double ageRatio;
        switch (Age) {
            case <= .1:
                ageRatio = Age / .1;
                Temperature = (int) (ageRatio * 100000 * massRatio);
                break;
            case <= .5:
                ageRatio = Age / .5;
                Temperature = (int) (ageRatio * 25000 * massRatio);
                break;
            case <= 1:
                ageRatio = Age / 1.0;
                Temperature = (int) (ageRatio * 10000 * massRatio);
                break;
            case <= 1.5:
                ageRatio = Age / 1.5;
                Temperature = (int) (ageRatio * 8000 * massRatio);
                break;
            case <= 2.5:
                ageRatio = Age / 2.5;
                Temperature = (int) (ageRatio * 7000 * massRatio);
                break;
            case <= 5:
                ageRatio = Age / 5.0;
                Temperature = (int) (ageRatio * 5500 * massRatio);
                break;
            case <= 10:
                ageRatio = Age / 10.0;
                Temperature = (int) (ageRatio * 5000 * massRatio);
                break;
            case <= 13:
                ageRatio = Age / 13.0;
                Temperature = (int) (ageRatio * 4000 * massRatio);
                break;
            default:
                ageRatio = Age / 13.0;
                Temperature = (int) (ageRatio * 3800 * massRatio);
                break;
        }
    }

    private void GenerateWhiteDwarf()
    {
        Mass = _worldBuilderStarMassCalculator.GenerateWhiteDwarfMass();
        var mass = (_rollingService.D3(1) + 2) * Mass;
        var mainSequenceLifespan = 10 / Math.Pow(mass, 2.5);
        var subgiantLifespan = mainSequenceLifespan / (4 + mass);
        var giantLifespan = mainSequenceLifespan / (10 * Math.Pow(mass, 3));
        Age = mainSequenceLifespan + subgiantLifespan + giantLifespan + _rollingService.D6(1) * 2 + _rollingService.D3(1) - 2 +
              (_rollingService.D10(1) / 10.0);

        double massRatio = Mass / .6;
        double ageRatio;
        switch (Age) {
            case <= .1:
                ageRatio = Age / .1;
                Diameter = .017;
                Temperature = (int) (ageRatio * 100000 * massRatio);
                break;
            case <= .5:
                ageRatio = Age / .5;
                Diameter = .017;
                Temperature = (int) (ageRatio * 25000 * massRatio);
                break;
            case <= 1:
                ageRatio = Age / 1.0;
                Diameter = .017;
                Temperature = (int) (ageRatio * 10000 * massRatio);
                break;
            case <= 1.5:
                ageRatio = Age / 1.5;
                Diameter = .017;
                Temperature = (int) (ageRatio * 8000 * massRatio);
                break;
            case <= 2.5:
                ageRatio = Age / 2.5;
                Diameter = .017;
                Temperature = (int) (ageRatio * 7000 * massRatio);
                break;
            case <= 5:
                ageRatio = Age / 5.0;
                Diameter = .017;
                Temperature = (int) (ageRatio * 5500 * massRatio);
                break;
            case <= 10:
                ageRatio = Age / 10.0;
                Diameter = .017;
                Temperature = (int) (ageRatio * 5000 * massRatio);
                break;
            case <= 13:
                ageRatio = Age / 13.0;
                Diameter = .017;
                Temperature = (int) (ageRatio * 4000 * massRatio);
                break;
            default:
                ageRatio = Age / 13.0;
                Diameter = .017;
                Temperature = (int) (ageRatio * 3800 * massRatio);
                break;
        }
    }

    private void GenerateBrownDwarf()
    {
        Age = _rollingService.D6(1) * 2 + _rollingService.D3(1) - 2 + _rollingService.D10(1) / 10.0;
        Mass = _worldBuilderStarMassCalculator.GenerateBrownDwarfMass();

        double massRatio;
        double massDifferenceRatio;
        switch (Mass) {
            case <= .013:
                massRatio = Mass / .013;
                massDifferenceRatio = (Mass) / .013 * 5;
                Temperature = (int) (massRatio * 300);
                Diameter = massRatio * .1;
                SpectralType = SpectralType.Y;
                SpectralSubclass = (int) (5 - massDifferenceRatio) + 5;
                break;
            case <= .025:
                massRatio = Mass / .025;
                massDifferenceRatio = (Mass - .013) / .012 * 5;
                Temperature = (int) (massRatio * 550);
                Diameter = massRatio * .1;
                SpectralType = SpectralType.Y;
                SpectralSubclass = (int) (5 - massDifferenceRatio);
                break;
            case <= .04:
                massRatio = Mass / .04;
                massDifferenceRatio = (Mass - .025) / .015 * 5;
                Temperature = (int) (massRatio * 900);
                Diameter = massRatio * .1;
                SpectralType = SpectralType.T;
                SpectralSubclass = (int) (5 - massDifferenceRatio) + 5;
                break;
            case <= .05:
                massRatio = Mass / .05;
                massDifferenceRatio = (Mass - .04) / .01 * 5;
                Temperature = (int) (massRatio * 1300);
                Diameter = massRatio * .1;
                SpectralType = SpectralType.T;
                SpectralSubclass = (int) (5 - massDifferenceRatio);
                break;
            case <= .06:
                massRatio = Mass / .06;
                massDifferenceRatio = (Mass - .05) / .01 * 5;
                Temperature = (int) (massRatio * 1850);
                Diameter = massRatio * .1;
                SpectralType = SpectralType.L;
                SpectralSubclass = (int) (5 - massDifferenceRatio) + 5;
                break;
            case <= .08:
                massRatio = Mass / .08;
                massDifferenceRatio = (Mass - .06) / .02 * 5;
                Temperature = (int) (massRatio * 2400);
                Diameter = (int) (massRatio * .1);
                SpectralType = SpectralType.L;
                SpectralSubclass = (int) (5 - massDifferenceRatio);
                break;
            default:
                Temperature = 2400;
                Diameter = .1;
                SpectralType = SpectralType.L;
                SpectralSubclass = 0;
                break;
        }
    }

    private void GenerateCompanionStar(char currentComponent)
    {
        if (_rollingService.D6(2) >= 10) {
            do {
                CompanionStar = new WorldBuilderStar(new RollingService(), StarType.Companion, Mass, currentComponent);
            } while (CompanionStar.Mass > Mass);
        }
    }

    private void GenerateAge()
    {
        var mainSequenceLifespan = 10 / Math.Pow(Mass, 2.5);
        switch (LuminosityClass) {
            case LuminosityClass.III:
                var giantLifespan = mainSequenceLifespan / (10 * Math.Pow(Mass, 3));
                Age = mainSequenceLifespan + (giantLifespan * _rollingService.Percent() / 100.00);
                break;
            case LuminosityClass.IV:
                var subgiantLifespan = mainSequenceLifespan / (4 + Mass);
                Age = mainSequenceLifespan + (subgiantLifespan * _rollingService.Percent() / 100.00);
                break;
            case LuminosityClass.V:
                Age = (Mass <= .9)
                    ? _rollingService.D6(1) * 2 + _rollingService.D3(1) - 2 + (_rollingService.D10(1) / 10.0)
                    : (_rollingService.D6(1) - 1 + (_rollingService.D6(1) / 6.0)) / 6.0;
                break;
        }
    }

    private void GenerateSpectralSubclass()
    {
        if (StarType == StarType.Primary && SpectralType == SpectralType.M) {
            SpectralSubclass = _rollingService.D6(2) switch {
                2 => 8,
                3 => 6,
                4 => 5,
                5 => 4,
                6 => 0,
                7 => 2,
                8 => 1,
                9 => 3,
                10 => 5,
                11 => 7,
                12 => 9,
                _ => 0
            };
        } else {
            SpectralSubclass = _rollingService.D6(2) switch {
                2 => 0,
                3 => 1,
                4 => 3,
                5 => 5,
                6 => 7,
                7 => 9,
                8 => 8,
                9 => 6,
                10 => 4,
                11 => 2,
                12 => 0,
                _ => 0
            };
        }
    }

    private void GenerateSpectralType(int roll)
    {
        switch (roll) {
            case 2:
                GenerateUnusualStar();
                break;
            case >= 3 and <= 6:
                SpectralType = SpectralType.M;
                if (LuminosityClass == LuminosityClass.IV) GenerateSpectralType(roll + 5);
                break;
            case 7 or 8:
                SpectralType = SpectralType.K;
                break;
            case 9 or 10:
                SpectralType = SpectralType.G;
                break;
            case 11:
                SpectralType = SpectralType.F;
                break;
            case >= 12:
                GenerateHotStar();
                break;
        }

        if (LuminosityClass == LuminosityClass.BD || SpectralType == SpectralType.BD) {
            SpectralType = SpectralType.BD;
            LuminosityClass = LuminosityClass.BD;
        }

        if (LuminosityClass == LuminosityClass.D || SpectralType == SpectralType.D) {
            SpectralType = SpectralType.D;
            LuminosityClass = LuminosityClass.D;
        }

        while (LuminosityClass == LuminosityClass.IV && SpectralType == SpectralType.K && SpectralSubclass > 4) {
            SpectralSubclass /= 2;
        }
    }

    private void GenerateUnusualStar()
    {
        switch (_rollingService.D6(2)) {
            case 2:
                GeneratePeculiarStar();
                return;
            case 3:
                LuminosityClass = LuminosityClass.VI;
                break;
            case 4:
                LuminosityClass = LuminosityClass.IV;
                break;
            case 5 or 6 or 7:
                LuminosityClass = LuminosityClass.BD;
                break;
            case 8 or 9 or 10:
                LuminosityClass = LuminosityClass.D;
                break;
            case 11:
                LuminosityClass = LuminosityClass.III;
                break;
            case >= 12:
                GenerateGiant();
                break;
        }

        GenerateSpectralType(_rollingService.D6(2) + 1);

        if (SpectralType == SpectralType.O && LuminosityClass == LuminosityClass.IV) {
            SpectralType = SpectralType.B;
        } else if (LuminosityClass == LuminosityClass.VI) {
            SpectralType = SpectralType switch {
                SpectralType.F => SpectralType.G,
                SpectralType.A => SpectralType.B,
                _ => SpectralType
            };
        }
    }

    private void GeneratePeculiarStar()
    {
        switch (_rollingService.D6(2)) {
            case 2:
                SpectralType = SpectralType.BH;
                LuminosityClass = LuminosityClass.BH;
                SpecialType = StarSpecialType.BlackHole;
                break;
            case 3:
                SpectralType = SpectralType.PSR;
                LuminosityClass = LuminosityClass.PSR;
                SpecialType = StarSpecialType.Pulsar;
                break;
            case 4:
                SpectralType = SpectralType.NS;
                LuminosityClass = LuminosityClass.NS;
                SpecialType = StarSpecialType.NeutronStar;
                break;
            case 5 or 6:
                SpecialType = StarSpecialType.Nebula;
                break;
            case 7 or 8 or 9:
                SpecialType = StarSpecialType.Protostar;
                GenerateSpectralType(_rollingService.D6(2) + 1);
                if (SpectralType == SpectralType.O) {
                    SpectralType = SpectralType.B;
                }

                break;
            case 10:
                SpecialType = StarSpecialType.StarCluster;
                break;
            case 11 or 12:
                SpecialType = StarSpecialType.Anomaly;
                break;
        }
    }

    private void GenerateGiant()
    {
        LuminosityClass = _rollingService.D6(2) switch {
            <= 8 => LuminosityClass.III,
            <= 10 => LuminosityClass.II,
            11 => LuminosityClass.Ib,
            12 => LuminosityClass.Ia,
            _ => LuminosityClass
        };
    }

    private void GenerateHotStar()
    {
        SpectralType = _rollingService.D6(2) switch {
            <= 9 => SpectralType.A,
            <= 11 => SpectralType.B,
            >= 12 => SpectralType.O
        };
    }

    public string GetClass()
    {
        return $"{SpectralType}{SpectralSubclass} {LuminosityClass}";
    }

    public bool IsPostStellarObject()
    {
        return SpectralType is SpectralType.D or SpectralType.NS or SpectralType.PSR or SpectralType.BH;
    }

    public void SetMinimumOrbit()
    {
        double minimumOrbit;
        if (CompanionStar == null) {
            minimumOrbit = FindMAO();
        } else {
            minimumOrbit = FindMAO() + .5 + CompanionStar.Eccentricity;
            if (SpectralType == SpectralType.M && SpectralSubclass >= 5
                || SpectralType == SpectralType.BD) {
                minimumOrbit = .25 * (1 - CompanionStar.Eccentricity) * CompanionStar.OrbitNumber;
            }
        }

        AvailableOrbits.Add(minimumOrbit);
    }

    private double FindMAO()
    {
        double minimumOrbit;
        minimumOrbit = LuminosityClass switch {
            LuminosityClass.Ia => GetMinimumIaOrbit(),
            LuminosityClass.Ib => GetMinimumIbOrbit(),
            LuminosityClass.II => GetMinimumIIOrbit(),
            LuminosityClass.III => GetMinimumIIIOrbit(),
            LuminosityClass.IV => GetMinimumIVOrbit(),
            LuminosityClass.V => GetMinimumVOrbit(),
            LuminosityClass.VI => GetMinimumVIOrbit(),
            _ => 0.01
        };
        return minimumOrbit;
    }

    private double GetMinimumIaOrbit()
    {
        return SpectralType switch {
            SpectralType.O => GetMinOrbit(0.63, 0.60, 0.55),
            SpectralType.B => GetMinOrbit(.5, 1.67, 3.34),
            SpectralType.A => GetMinOrbit(3.34, 4.17, 4.42),
            SpectralType.F => GetMinOrbit(4.42, 5, 5.21),
            SpectralType.G => GetMinOrbit(5.21, 5.34, 5.59),
            SpectralType.K => GetMinOrbit(5.59, 6.17, 6.8),
            SpectralType.M => GetMinOrbit(6.8, 7.2, 7.8),
            _ => throw new ArgumentOutOfRangeException()
        };
    }

    private double GetMinimumIbOrbit()
    {
        return SpectralType switch {
            SpectralType.O => GetMinOrbit(0.6, 0.5, 0.35),
            SpectralType.B => GetMinOrbit(0.35, 0.63, 1.4),
            SpectralType.A => GetMinOrbit(1.4, 2.17, 2.50),
            SpectralType.F => GetMinOrbit(2.5, 3.25, 3.59),
            SpectralType.G => GetMinOrbit(3.59, 3.84, 4.17),
            SpectralType.K => GetMinOrbit(4.17, 4.84, 5.42),
            SpectralType.M => GetMinOrbit(5.42, 6.17, 6.59),
            _ => throw new ArgumentOutOfRangeException()
        };
    }

    private double GetMinimumIIOrbit()
    {
        return SpectralType switch {
            SpectralType.O => GetMinOrbit(0.55, 0.45, 0.3),
            SpectralType.B => GetMinOrbit(0.3, 0.35, 0.75),
            SpectralType.A => GetMinOrbit(0.75, 1.17, 1.33),
            SpectralType.F => GetMinOrbit(1.33, 1.87, 2.24),
            SpectralType.G => GetMinOrbit(2.24, 2.67, 3.17),
            SpectralType.K => GetMinOrbit(3.17, 4.00, 4.59),
            SpectralType.M => GetMinOrbit(4.59, 5.3, 5.92),
            _ => throw new ArgumentOutOfRangeException()
        };
    }

    private double GetMinimumIIIOrbit()
    {
        return SpectralType switch {
            SpectralType.O => GetMinOrbit(0.53, 0.38, 0.25),
            SpectralType.B => GetMinOrbit(0.25, 0.15, 0.13),
            SpectralType.A => GetMinOrbit(0.13, 0.13, 0.13),
            SpectralType.F => GetMinOrbit(0.13, 0.13, 0.25),
            SpectralType.G => GetMinOrbit(0.25, 0.38, 0.50),
            SpectralType.K => GetMinOrbit(0.50, 1.00, 1.68),
            SpectralType.M => GetMinOrbit(1.68, 3.00, 4.34),
            _ => throw new ArgumentOutOfRangeException()
        };
    }

    private double GetMinimumIVOrbit()
    {
        return SpectralType switch {
            SpectralType.B => GetMinOrbit(0.20, 0.13, 0.10),
            SpectralType.A => GetMinOrbit(0.10, 0.07, 0.07),
            SpectralType.F => GetMinOrbit(0.07, 0.06, 0.07),
            SpectralType.G => GetMinOrbit(0.07, 0.10, 0.15),
            SpectralType.K => GetMinOrbit(0.15, 0.17, 0),
            _ => throw new ArgumentOutOfRangeException()
        };
    }

    private double GetMinimumVOrbit()
    {
        return SpectralType switch {
            SpectralType.O => GetMinOrbit(0.5, 0.3, 0.18),
            SpectralType.B => GetMinOrbit(0.18, 0.09, 0.06),
            SpectralType.A => GetMinOrbit(0.06, 0.05, 0.04),
            SpectralType.F => GetMinOrbit(0.04, 0.03, 0.03),
            SpectralType.G => GetMinOrbit(0.03, 0.02, 0.02),
            SpectralType.K => GetMinOrbit(0.02, 0.02, 0.02),
            SpectralType.M => GetMinOrbit(0.02, 0.01, 0.01),
            _ => throw new ArgumentOutOfRangeException()
        };
    }

    private double GetMinimumVIOrbit()
    {
        return SpectralType switch {
            SpectralType.O => GetMinOrbit(0.01, 0.01, 0.01),
            SpectralType.B => GetMinOrbit(0.01, 0.01, 0.01),
            SpectralType.G => GetMinOrbit(0.02, 0.02, 0.02),
            SpectralType.K => GetMinOrbit(0.02, 0.01, 0.01),
            SpectralType.M => GetMinOrbit(0.01, 0.01, 0.01),
            _ => throw new ArgumentOutOfRangeException()
        };
    }

    private double GetMinOrbit(double subclass0Orbit, double subclass5Orbit, double subclass10Orbit)
    {
        double result;
        switch (SpectralSubclass) {
            case 0:
                return subclass0Orbit;
            case < 5:
                result = (subclass0Orbit - subclass5Orbit) / 5.0 * SpectralSubclass;
                result += subclass5Orbit;
                return result;
            case 5:
                return subclass5Orbit;
            case < 10:
                result = (subclass5Orbit - subclass10Orbit) / 5.0 * (SpectralSubclass - 5);
                result += subclass10Orbit;
                return result;
        }

        return 0;
    }

    public void SetMaximumOrbit(List<WorldBuilderStar> stars)
    {
        if (StarType == StarType.Primary) {
            AvailableOrbits.Add(20);
        } else {
            var maxOrbit = OrbitNumber - 3;
            switch (StarType) {
                case StarType.Close: {
                    if (stars.Where(x => x.StarType == StarType.Near).ToList().Count > 1) {
                        maxOrbit -= 1;
                    }

                    if (stars.Where(x => x.StarType == StarType.Near && x.Eccentricity > .2).ToList().Count > 1) {
                        maxOrbit -= 1;
                    }

                    if (Eccentricity >= .5) maxOrbit -= 1;
                    break;
                }
                case StarType.Near: {
                    if (stars.Where(x => x.StarType == StarType.Close || x.StarType == StarType.Far).ToList().Count > 1) {
                        maxOrbit -= 1;
                    }

                    if (stars.Where(x => x.StarType is StarType.Close or StarType.Far && x.Eccentricity >= .2).ToList().Count > 1) {
                        maxOrbit -= 1;
                    }

                    if (Eccentricity >= .5) maxOrbit -= 1;
                    break;
                }
                case StarType.Far:
                    if (stars.Where(x => x.StarType == StarType.Near).ToList().Count > 1) {
                        maxOrbit -= 1;
                    }

                    if (stars.Where(x => x.StarType == StarType.Near && x.Eccentricity >= .2).ToList().Count > 1) {
                        maxOrbit -= 1;
                    }

                    if (Eccentricity >= .5) maxOrbit -= 1;
                    break;
            }

            AvailableOrbits.Add(maxOrbit);
        }
    }

    public void SetMidOrbits(List<WorldBuilderStar> stars)
    {
        if (StarType == StarType.Primary) {
            foreach (var star in stars) {
                var minRange = star.OrbitNumber - 1;
                var maxRange = star.OrbitNumber + 1;
                if (star.FindMAO() >= .2) {
                    minRange -= star.FindMAO();
                    maxRange += star.FindMAO();
                }

                if (star.Eccentricity >= .2) {
                    minRange -= 1;
                    maxRange += 1;
                }

                if (star.StarType is StarType.Close or StarType.Near
                    && star.Eccentricity >= .5) {
                    minRange -= 1;
                    maxRange += 1;
                }

                AvailableOrbits.Add(minRange);
                AvailableOrbits.Add(maxRange);
            }
        }
    }

    public void CaculateHZCO()
    {
        var HZCODistance = Math.Sqrt(Luminosity + (CompanionStar?.Luminosity ?? 0));
        HZCO = HZCODistance switch {
            <= .4 => HZCODistance / .4,
            <= .7 => (HZCODistance - .4) / .3 + 1,
            <= 1 => (HZCODistance - .7) / .3 + 2,
            <= 1.6 => (HZCODistance - 1) / .6 + 3,
            <= 2.8 => (HZCODistance - 1.6) / 1.2 + 4,
            <= 5.2 => (HZCODistance - 2.8) / 2.4 + 5,
            <= 10 => (HZCODistance - 5.2) / 4.8 + 6,
            <= 20 => (HZCODistance - 10) / 10.0 + 7,
            <= 40 => (HZCODistance - 20) / 20.0 + 8,
            <= 77 => (HZCODistance - 40) / 37.0 + 9,
            <= 154 => (HZCODistance - 77) / 77.0 + 10,
            <= 308 => (HZCODistance - 154) / 154.0 + 11,
            <= 615 => (HZCODistance - 308) / 307.0 + 12,
            <= 1230 => (HZCODistance - 615) / 615.0 + 13,
            <= 2500 => (HZCODistance - 1230) / 1270.0 + 14,
            <= 4900 => (HZCODistance - 2500) / 2400.0 + 15,
            <= 9800 => (HZCODistance - 4900) / 4900.0 + 16,
            <= 19500 => (HZCODistance - 9800) / 9700.0 + 17,
            <= 39500 => (HZCODistance - 19500) / 20000.0 + 18,
            <= 78700 => (HZCODistance - 39500) / 39200.0 + 19,
            _ => 20
        };
    }

    public double GetTotalAvailableOrbits()
    {
        var availableOrbits = 0.0;
        for (var i = 0; i < AvailableOrbits.Count; i += 2) {
            var orbitNums = AvailableOrbits[i + 1] - AvailableOrbits[i];
            if (orbitNums > 0) {
                availableOrbits += orbitNums;
            }
        }

        if (CompanionStar == null && availableOrbits > 0) {
            availableOrbits += 1;
        }

        return availableOrbits;
    }
}