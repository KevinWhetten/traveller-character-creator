using SectorCreator.Global;
using SectorCreator.Global.Enums;
using SectorCreator.Models.RTTWorldgen;

namespace SectorCreator.Models.Factories.RttWorldgen;

public interface IRttWorldgenStarFactory
{
    RttWorldgenStar Generate(StarType starType, out int spectralRoll);
    RttWorldgenStar Generate(StarType starType, int primaryRoll = 0);
    RttWorldgenStar GenerateBrownDwarf();
}

public class RttWorldgenStarFactory : IRttWorldgenStarFactory
{
    private readonly IRollingService _rollingService;
    public RttWorldgenStar Star = new();
    public StarType starType;

    public RttWorldgenStarFactory(IRollingService rollingService)
    {
        _rollingService = rollingService;
    }

    public RttWorldgenStar Generate(StarType starType, out int spectralRoll)
    {
        Star = new RttWorldgenStar();
        
        spectralRoll = GenerateSpectralType();
        GenerateAge();
        ModifySpectralType();
        GenerateOrbit();

        return Star;
    }

    public RttWorldgenStar Generate(StarType starType, int primaryRoll)
    {
        GenerateSpectralType(primaryRoll);
        GenerateAge();
        ModifySpectralType();
        GenerateOrbit();

        return Star;
    }

    public RttWorldgenStar GenerateBrownDwarf()
    {
        Star.SpectralType = SpectralType.L;
        GenerateAge();
        ModifySpectralType();
        GenerateOrbit();

        return Star;
    }

    public int GenerateSpectralType(int primaryStarRoll = 0)
    {
        int roll;
        if (starType == StarType.Primary) {
            roll = _rollingService.D6(2);
        } else {
            roll = primaryStarRoll + _rollingService.D6(1) - 1;
        }

        Star.SpectralType = roll switch {
            <= 2 => SpectralType.A,
            3 => SpectralType.F,
            4 => SpectralType.G,
            5 => SpectralType.K,
            >= 6 and <= 13 => SpectralType.M,
            >= 14 => SpectralType.L
        };
        return roll;
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
                Star.Luminosity = Luminosity.V;
                break;
            case SpectralType.M:
                ModifySpectralTypeM();
                break;
        }
    }

    private void ModifySpectralTypeM()
    {
        var roll = _rollingService.D6(2)
                   + (starType == StarType.Primary ? 0 : 2);
        switch (roll) {
            case <= 9:
                Star.Luminosity = Luminosity.V;
                break;
            case >= 10 and <= 12:
                Star.Luminosity = Luminosity.Ve;
                break;
            case >= 13:
                Star.SpectralType = SpectralType.L;
                break;
        }
    }

    private void ModifySpectralTypeG()
    {
        switch (Star.Age) {
            case <= 11:
                Star.Luminosity = Luminosity.V;
                break;
            case 12 or 13:
                if (_rollingService.D6(1) <= 3) {
                    Star.SpectralType = SpectralType.K;
                    Star.Luminosity = Luminosity.IV;
                } else {
                    Star.SpectralType = SpectralType.M;
                    Star.Luminosity = Luminosity.III;
                }

                break;
            case >= 14:
                Star.SpectralType = SpectralType.D;
                break;
        }
    }

    private void ModifySpectralTypeF()
    {
        switch (Star.Age) {
            case <= 5:
                Star.Luminosity = Luminosity.V;
                break;
            case 6:
                if (_rollingService.D6(1) <= 4) {
                    Star.SpectralType = SpectralType.G;
                    Star.Luminosity = Luminosity.IV;
                } else {
                    Star.SpectralType = SpectralType.M;
                    Star.Luminosity = Luminosity.III;
                }

                break;
            case >= 7:
                Star.SpectralType = SpectralType.D;
                break;
        }
    }

    private void ModifySpectralTypeA()
    {
        switch (Star.Age) {
            case <= 2:
                Star.Luminosity = Luminosity.V;
                break;
            case 3:
                switch (_rollingService.D6(1)) {
                    case <= 2:
                        Star.SpectralType = SpectralType.F;
                        Star.Luminosity = Luminosity.IV;
                        break;
                    case 3:
                        Star.SpectralType = SpectralType.K;
                        Star.Luminosity = Luminosity.III;
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