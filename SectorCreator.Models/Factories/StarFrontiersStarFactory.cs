using SectorCreator.Global;
using SectorCreator.Global.Enums;
using SectorCreator.Models.Basic;

namespace SectorCreator.Models.Factories;

public interface IStarFrontiersStarFactory
{
    Star Generate();
}

public class StarFrontiersStarFactory : IStarFrontiersStarFactory
{
    private readonly IRollingService _rollingService;
    private Star Star = new();

    public StarFrontiersStarFactory(IRollingService rollingService)
    {
        _rollingService = rollingService;
    }

    public Star Generate()
    {
        // Magnetar 1.0%
        // Magnetar&Pulsar 0.2 %
        // Pulsar = 98.8%
        
        Star = new Star {
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
            },
            SpectralSubclass = _rollingService.D10(1)
        };

        return Star;
    }
}