using SectorCreator.Global;
using SectorCreator.Global.Enums;

namespace SectorCreator.WorldBuilder.Star;

internal class WorldBuilderStarTemperatureCalculator
{
    private readonly IRollingService _rollingService;
    private readonly WorldBuilderStar _worldBuilderStar;

    public WorldBuilderStarTemperatureCalculator(IRollingService rollingService, WorldBuilderStar worldBuilderStar)
    {
        _rollingService = rollingService;
        _worldBuilderStar = worldBuilderStar;
    }

    public int GenerateTemperature()
    {
        return _worldBuilderStar.SpectralType switch {
            SpectralType.O => GetTemperature(50000, 40000, 30000),
            SpectralType.B => GetTemperature(30000, 15000, 10000),
            SpectralType.A => GetTemperature(10000, 8000, 7500),
            SpectralType.F => GetTemperature(7500, 6500, 6000),
            SpectralType.G => GetTemperature(6000, 5600, 5200),
            SpectralType.K => GetTemperature(5200, 4400, 3700),
            SpectralType.M => GetTemperature(3700, 3000, 2400),
            _ => GetUnusualTemperature()
        };
    }

    private int GetUnusualTemperature()
    {
        if (_worldBuilderStar.LuminosityClass == LuminosityClass.BD) {
            
        } else if (_worldBuilderStar.LuminosityClass == LuminosityClass.D) {
            var massRatio = _worldBuilderStar.Mass / 0.6;
            return _worldBuilderStar.Age switch {
                < .1 => (int)(massRatio * 100000),
                < .5 => (int)(massRatio * 25000),
                < 1 => (int)(massRatio * 10000),
                < 1.5 => (int)(massRatio * 8000),
                < 2.5 => (int)(massRatio * 7000),
                < 5 => (int)(massRatio * 5500),
                < 10 => (int)(massRatio * 5000),
                < 13 => (int)(massRatio * 4000),
                _ => (int)(massRatio * 3800)
            };
        }
        throw new NotImplementedException();
    }

    private int GetTemperature(double subclass0Temperature, double subclass5Temperature, double subclass10Temperature)
    {
        double result;
        switch (_worldBuilderStar.SpectralSubclass) {
            case 0:
                return (int) (subclass0Temperature + _rollingService.Variance(subclass0Temperature, 20));
            case < 5:
                result = (subclass0Temperature - subclass5Temperature) / 5.0 * _worldBuilderStar.SpectralSubclass;
                result += subclass5Temperature;
                return (int) (result + _rollingService.Variance(result, 20));
            case 5:
                return (int) (subclass5Temperature + _rollingService.Variance(subclass5Temperature, 20));
            case < 10:
                result = (subclass5Temperature - subclass10Temperature) / 5.0 * (_worldBuilderStar.SpectralSubclass - 5);
                result += subclass10Temperature;
                return (int) (result + _rollingService.Variance(result, 20));
        }

        return 0;
    }
}