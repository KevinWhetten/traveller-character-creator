using SectorCreator.Global;
using SectorCreator.Global.Enums;
using SectorCreator.Models.Basic;

namespace SectorCreator.Models.RTTWorldgen.Factories;

public interface IRttWorldgenStarFactory
{
    RttWorldgenStar Generate(out int spectralRoll);
    RttWorldgenStar Generate(int primaryRoll = 0);
    RttWorldgenStar GenerateBrownDwarf();
    Star ModifyStar(RttWorldgenStar star, int starSystemAge);
}

public class RttWorldgenStarFactory : IRttWorldgenStarFactory
{
    private readonly IRollingService _rollingService;
    public RttWorldgenStarFactory(IRollingService rollingService)
    {
        _rollingService = rollingService;
    }
    
    public RttWorldgenStar Star = new();
    public StarType starType;

    public RttWorldgenStar Generate(out int spectralRoll)
    {
        Star = new RttWorldgenStar();
        
        spectralRoll = GenerateSpectralType();
        GenerateAge();
        GenerateOrbit();
        Star.SpectralSubclass = _rollingService.D10(1) - 1;

        return Star;
    }

    public RttWorldgenStar Generate(int primaryRoll)
    {
        GenerateSpectralType(primaryRoll);
        GenerateAge();
        GenerateOrbit();
        Star.SpectralSubclass = _rollingService.D10(1) - 1;

        return Star;
    }

    public RttWorldgenStar GenerateBrownDwarf()
    {
        Star.SpectralType = SpectralType.BD;
        GenerateAge();
        ModifySpectralType();
        GenerateOrbit();

        return Star;
    }

    public Star ModifyStar(RttWorldgenStar star, int starSystemAge)
    {
        Star = star;
        ModifySpectralType();
        return Star;
    }

    public int GenerateSpectralType(int primaryStarRoll = 0)
    {
        int roll;
        if (primaryStarRoll == 0) {
            roll = _rollingService.D6(2);
        } else {
            roll = primaryStarRoll + _rollingService.D6(1) - 1;
        }

        Star.SpectralType = roll switch {
            <= 2 => GetLargeStar(),
            3 => SpectralType.F,
            4 => SpectralType.G,
            5 => SpectralType.K,
            >= 6 and <= 13 => SpectralType.M,
            >= 14 => SpectralType.BD
        };
        return roll;
    }

    private SpectralType GetLargeStar()
    {
        return _rollingService.D6(3) switch {
            <= 12 => SpectralType.A,
            <= 14 => SpectralType.B,
            <= 16 => SpectralType.O,
            17 => GetNeutronStar(),
            18 => SpectralType.BH,
            _ => SpectralType.A
        };
    }

    private SpectralType GetNeutronStar()
    {
        return _rollingService.D6(2) switch {
            <= 10 => SpectralType.NS,
            >= 11 => SpectralType.PSR
        };
    }

    public void GenerateAge()
    {
        Star.Age = _rollingService.D6(3) - 3;
    }

    public void ModifySpectralType()
    {
        switch (Star.SpectralType) {
            case SpectralType.A:
                ModifySpectralTypeA();
                break;
            case SpectralType.F:
                ModifySpectralTypeF();
                break;
            case SpectralType.G:
                ModifySpectralTypeG();
                break;
            case SpectralType.K:
                Star.LuminosityClass = LuminosityClass.V;
                break;
            case SpectralType.M:
                ModifySpectralTypeM();
                break;
            default:
                Star.LuminosityClass = GenerateRandomLuminosity();
                break;
        }
    }

    private LuminosityClass GenerateRandomLuminosity()
    {
        return _rollingService.D6(1) switch {
            1 => LuminosityClass.III,
            2 => LuminosityClass.IV,
            6 => LuminosityClass.Ve,
            _ => LuminosityClass.V
        };
    }

    private void ModifySpectralTypeM()
    {
        var roll = _rollingService.D6(2)
                   + (starType == StarType.Primary ? 0 : 2);
        switch (roll) {
            case <= 9:
                Star.LuminosityClass = LuminosityClass.V;
                break;
            case >= 10 and <= 12:
                Star.LuminosityClass = LuminosityClass.Ve;
                break;
            case >= 13:
                Star.SpectralType = SpectralType.BD;
                break;
        }
    }

    private void ModifySpectralTypeG()
    {
        switch (Star.Age) {
            case <= 11:
                Star.LuminosityClass = LuminosityClass.V;
                break;
            case 12 or 13:
                if (_rollingService.D6(1) <= 3) {
                    Star.SpectralType = SpectralType.K;
                    Star.LuminosityClass = LuminosityClass.IV;
                } else {
                    Star.SpectralType = SpectralType.M;
                    Star.LuminosityClass = LuminosityClass.III;
                }

                break;
            case >= 14:
                Star.SpectralType = SpectralType.D;
                GenerateRandomLuminosity();
                break;
        }
    }

    private void ModifySpectralTypeF()
    {
        switch (Star.Age) {
            case <= 5:
                Star.LuminosityClass = LuminosityClass.V;
                break;
            case 6:
                if (_rollingService.D6(1) <= 4) {
                    Star.SpectralType = SpectralType.G;
                    Star.LuminosityClass = LuminosityClass.IV;
                } else {
                    Star.SpectralType = SpectralType.M;
                    Star.LuminosityClass = LuminosityClass.III;
                }

                break;
            case >= 7:
                Star.SpectralType = SpectralType.D;
                GenerateRandomLuminosity();
                break;
        }
    }

    private void ModifySpectralTypeA()
    {
        switch (Star.Age) {
            case <= 2:
                Star.LuminosityClass = LuminosityClass.V;
                break;
            case 3:
                switch (_rollingService.D6(1)) {
                    case <= 2:
                        Star.SpectralType = SpectralType.F;
                        Star.LuminosityClass = LuminosityClass.IV;
                        break;
                    case 3:
                        Star.SpectralType = SpectralType.K;
                        Star.LuminosityClass = LuminosityClass.III;
                        break;
                    default:
                        Star.SpectralType = SpectralType.D;
                        break;
                }

                break;
            case >= 4:
                Star.SpectralType = SpectralType.D;
                break;
        }
    }

    public void GenerateOrbit()
    {
        if (starType == StarType.Primary) {
            Star.CompanionOrbit = CompanionOrbit.None;
        } else {
            Star.CompanionOrbit = _rollingService.D6(1) switch {
                (<= 2) => CompanionOrbit.Tight,
                3 or 4 => CompanionOrbit.Close,
                5 => CompanionOrbit.Moderate,
                (>= 6) => CompanionOrbit.Distant
            };
        }
    }
}