using SectorCreator.Global;
using SectorCreator.Global.Enums;
using SectorCreator.Models.Basic;

namespace SectorCreator.Models.StarFrontiers;

public class StarFrontiersStar : Star
{
    private readonly IRollingService _rollingService;

    public StarFrontiersStar(IRollingService rollingService)
    {
        _rollingService = rollingService;
    }

    private void Generate()
    {
        SpectralType = _rollingService.D10(1) switch {
            1 => SpectralType.Wd,
            2 => SpectralType.M,
            3 => SpectralType.K,
            4 => SpectralType.G,
            5 => SpectralType.F,
            6 => SpectralType.A,
            7 => SpectralType.B,
            8 => SpectralType.O,
            9 => SpectralType.Rg,
            10 => SpectralType.Sg,
            _ => throw new ArgumentOutOfRangeException()
        };
        SpectralSubclass = _rollingService.D10(1);
    }
}