using SectorCreator.Global;
using SectorCreator.Global.Enums;
using SectorCreator.Models.Base;

namespace SectorCreator.Models.StarFrontiers;

public class StarFrontiersStar : Star
{

    public StarFrontiersStar()
    {
        SpectralType = Roll.D10(1) switch {
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
        SpectralSubclass = Roll.D10(1);
    }
}