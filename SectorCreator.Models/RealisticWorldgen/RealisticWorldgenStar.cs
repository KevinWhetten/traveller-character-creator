using SectorCreator.Global;
using SectorCreator.Global.Enums;
using SectorCreator.Models.Basic;

namespace SectorCreator.Models.RealisticWorldgen;

public class RealisticWorldgenStar : Star
{
    private readonly IRollingService _rollingService;


    public RealisticWorldgenStar(IRollingService rollingService)
    {
        _rollingService = rollingService;
        GenerateSpectralType();
        
        switch (_rollingService.Percent()) {
            case < .5:
                LuminosityClass = LuminosityClass.VI;
                break;
            case < 1.5:
                GenerateGiantStar();
                break;
            case < 7.5:
                LuminosityClass = LuminosityClass.VII;
                break;
            default:
                LuminosityClass = LuminosityClass.V;
                break;
        }
        GenerateSpectralSubclass();
    }

    private void GenerateGiantStar()
    {
        LuminosityClass = _rollingService.Percent() switch {
            < .01 => LuminosityClass.Ia,
            < .1 => LuminosityClass.Ib,
            < 1 => LuminosityClass.II,
            < 10 => LuminosityClass.III,
            _ => LuminosityClass.IV
        };
    }

    private void GenerateSpectralType()
    {
        SpectralType = _rollingService.Percent() switch {
            < 0.00003 => SpectralType.O,
            < 0.12 => SpectralType.B,
            < .75 => SpectralType.A,
            < 3.75 => SpectralType.F,
            < 11.5 => SpectralType.G,
            < 25 => SpectralType.K,
            _ => SpectralType.M
        };
    }

    private void GenerateSpectralSubclass()
    {
        SpectralSubclass = _rollingService.Percent() switch {
            < .2 => 9,
            < .4 => 8,
            < .8 => 7,
            < 1.5 => 6,
            < 3 => 5,
            < 6 => 4,
            < 12.5 => 3,
            < 25 => 2,
            < 50 => 1,
            _ => 0
        };
    }
}