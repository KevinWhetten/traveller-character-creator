using SectorCreator.Global;
using SectorCreator.Global.Enums;

namespace SectorCreator.WorldBuilder.Star;

public class WorldBuilderStarDiameterCalculator
{
    private readonly IRollingService _rollingService;
    private readonly WorldBuilderStar _worldBuilderStar;

    public WorldBuilderStarDiameterCalculator(IRollingService rollingService, WorldBuilderStar worldBuilderStar)
    {
        _rollingService = rollingService;
        _worldBuilderStar = worldBuilderStar;
    }

    public double GenerateDiameter()
    {
        return _worldBuilderStar.SpectralType switch {
            SpectralType.O => GetOTypeDiameter(),
            SpectralType.B => GetBTypeDiameter(),
            SpectralType.A => GetATypeDiameter(),
            SpectralType.F => GetFTypeDiameter(),
            SpectralType.G => GetGTypeDiameter(),
            SpectralType.K => GetKTypeDiameter(),
            SpectralType.M => GetMTypeDiameter(),
            _ => GetUnusualDiameter()
        };
    }

    private double GetUnusualDiameter()
    {
        if (_worldBuilderStar.SpectralType == SpectralType.BD) {
            return _worldBuilderStar.Mass switch {
                <= .013 => (int) ((_worldBuilderStar.Mass / .013) * .10),
                <= .025 => (int) ((_worldBuilderStar.Mass / .025) * .10),
                <= .04 => (int) ((_worldBuilderStar.Mass / .04) * .11),
                <= .05 => (int) ((_worldBuilderStar.Mass / .05) * .09),
                <= .06 => (int) ((_worldBuilderStar.Mass / .06) * .08),
                <= .08 => (int) ((_worldBuilderStar.Mass / .08) * .10),
                _ => .10
            };
        }
        throw new NotImplementedException();
    }

    private double GetOTypeDiameter()
    {
        return _worldBuilderStar.LuminosityClass switch {
            LuminosityClass.Ia => GetDiameter(25,22,20),
            LuminosityClass.Ib => GetDiameter(24,20,14),
            LuminosityClass.II => GetDiameter(22,18,12),
            LuminosityClass.III => GetDiameter(21,15,10),
            LuminosityClass.V => GetDiameter(20,12,7),
            LuminosityClass.VI => GetDiameter(.18,.18,.2),
            _ => throw new ArgumentOutOfRangeException()
        };
    }

    private double GetBTypeDiameter()
    {
        return _worldBuilderStar.LuminosityClass switch {
            LuminosityClass.Ia => GetDiameter(20,60,120),
            LuminosityClass.Ib => GetDiameter(14,25,50),
            LuminosityClass.II => GetDiameter(12,14,30),
            LuminosityClass.III => GetDiameter(10,6,5),
            LuminosityClass.IV => GetDiameter(8,5,4),
            LuminosityClass.V => GetDiameter(7,3.5,2.2),
            LuminosityClass.VI => GetDiameter(.2,.5,.7),
            _ => throw new ArgumentOutOfRangeException()
        };
    }

    private double GetATypeDiameter()
    {
        return _worldBuilderStar.LuminosityClass switch {
            LuminosityClass.Ia => GetDiameter(120,180,210),
            LuminosityClass.Ib => GetDiameter(50,75,85),
            LuminosityClass.II => GetDiameter(30,45,50),
            LuminosityClass.III => GetDiameter(5,5,5),
            LuminosityClass.IV => GetDiameter(4,3,3),
            LuminosityClass.V => GetDiameter(2.2,2,1.7),
            _ => throw new ArgumentOutOfRangeException()
        };
    }

    private double GetFTypeDiameter()
    {
        return _worldBuilderStar.LuminosityClass switch {
            LuminosityClass.Ia => GetDiameter(210,280,330),
            LuminosityClass.Ib => GetDiameter(85,115,135),
            LuminosityClass.II => GetDiameter(50,66,77),
            LuminosityClass.III => GetDiameter(5,5,10),
            LuminosityClass.IV => GetDiameter(3,2,3),
            LuminosityClass.V => GetDiameter(1.7,1.5,1.1),
            _ => throw new ArgumentOutOfRangeException()
        };
    }

    private double GetGTypeDiameter()
    {
        return _worldBuilderStar.LuminosityClass switch {
            LuminosityClass.Ia => GetDiameter(330,360,420),
            LuminosityClass.Ib => GetDiameter(135,150,180),
            LuminosityClass.II => GetDiameter(77,90,110),
            LuminosityClass.III => GetDiameter(10,15,20),
            LuminosityClass.IV => GetDiameter(3,4,6),
            LuminosityClass.V => GetDiameter(1.1,.95,.9),
            LuminosityClass.VI => GetDiameter(.8,.7,.6),
            _ => throw new ArgumentOutOfRangeException()
        };
    }

    private double GetKTypeDiameter()
    {
        return _worldBuilderStar.LuminosityClass switch {
            LuminosityClass.Ia => GetDiameter(420,600,900),
            LuminosityClass.Ib => GetDiameter(180,260,380),
            LuminosityClass.II => GetDiameter(110,160,230),
            LuminosityClass.III => GetDiameter(20,40,60),
            LuminosityClass.IV => GetDiameter(6, 5,4),
            LuminosityClass.V => GetDiameter(.9,.8,.7),
            LuminosityClass.VI => GetDiameter(.6,.5,.4),
            _ => throw new ArgumentOutOfRangeException()
        };
    }

    private double GetMTypeDiameter()
    {
        return _worldBuilderStar.LuminosityClass switch {
            LuminosityClass.Ia => GetDiameter(900,1200,1800),
            LuminosityClass.Ib => GetDiameter(380,600,800),
            LuminosityClass.II => GetDiameter(230,350,500),
            LuminosityClass.III => GetDiameter(60,100,200),
            LuminosityClass.V => GetDiameter(.7,.2,.1),
            LuminosityClass.VI => GetDiameter(.4,.1,.08),
            _ => throw new ArgumentOutOfRangeException()
        };
    }

    private double GetDiameter(double subclass0Diameter, double subclass5Diameter, double subclass10Diameter)
    {
        double result;
        switch (_worldBuilderStar.SpectralSubclass) {
            case 0:
                return subclass0Diameter + _rollingService.Variance(subclass0Diameter, 20);
            case < 5:
                result = (subclass0Diameter - subclass5Diameter) / 5.0 * _worldBuilderStar.SpectralSubclass;
                result += subclass5Diameter; 
                return result + _rollingService.Variance(result, 20);
            case 5:
                return subclass5Diameter + _rollingService.Variance(subclass5Diameter, 20);
            case < 10:
                result = (subclass5Diameter - subclass10Diameter) / 5.0 * (_worldBuilderStar.SpectralSubclass - 5);
                result += subclass10Diameter;
                return result + _rollingService.Variance(result, 20);
        }

        return 0;
    }
}