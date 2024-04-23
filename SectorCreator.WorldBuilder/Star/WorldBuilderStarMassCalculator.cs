using SectorCreator.Global;
using SectorCreator.Global.Enums;

namespace SectorCreator.WorldBuilder.Star;

public class WorldBuilderStarMassCalculator
{
    private readonly IRollingService _rollingService;
    private readonly WorldBuilderStar _worldBuilderStar;
    
    public WorldBuilderStarMassCalculator(IRollingService rollingService, WorldBuilderStar worldBuilderStar)
    {
        _rollingService = rollingService;
        _worldBuilderStar = worldBuilderStar;
    }

    public double GenerateMass()
    {
        return _worldBuilderStar.SpectralType switch {
            SpectralType.O => GetOTypeMass(),
            SpectralType.B => GetBTypeMass(),
            SpectralType.A => GetATypeMass(),
            SpectralType.F => GetFTypeMass(),
            SpectralType.G => GetGTypeMass(),
            SpectralType.K => GetKTypeMass(),
            SpectralType.M => GetMTypeMass(),
            _ => GetUnusualMass()
        };
    }

    private double GetUnusualMass()
    {
        throw new NotImplementedException();
    }

    private double GetMTypeMass()
    {
        return _worldBuilderStar.LuminosityClass switch {
            LuminosityClass.Ia => GetMass(20,25,30),
            LuminosityClass.Ib => GetMass(15,20,25),
            LuminosityClass.II => GetMass(14,16,18),
            LuminosityClass.III => GetMass(1.8,2.4,8),
            LuminosityClass.V => GetMass(.5,.16,.08),
            LuminosityClass.VI => GetMass(.4,.12,.075),
            _ => throw new ArgumentOutOfRangeException()
        };
    }

    private double GetKTypeMass()
    {
        return _worldBuilderStar.LuminosityClass switch {
            LuminosityClass.Ia => GetMass(14,18,20),
            LuminosityClass.Ib => GetMass(12,13,15),
            LuminosityClass.II => GetMass(10,12,14),
            LuminosityClass.III => GetMass(1.1,1.5,1.8),
            LuminosityClass.IV => GetMass(1.5, 1.8,2),
            LuminosityClass.V => GetMass(.8,.7,.5),
            LuminosityClass.VI => GetMass(.6,.5,.4),
            _ => throw new ArgumentOutOfRangeException()
        };
    }

    private double GetGTypeMass()
    {
        return _worldBuilderStar.LuminosityClass switch {
            LuminosityClass.Ia => GetMass(12,13,14),
            LuminosityClass.Ib => GetMass(10,11,12),
            LuminosityClass.II => GetMass(8,10,10),
            LuminosityClass.III => GetMass(2.5,2.4,1.1),
            LuminosityClass.IV => GetMass(1.7,1.2,1.5),
            LuminosityClass.V => GetMass(1.1,.9,.8),
            LuminosityClass.VI => GetMass(.8,.7,.6),
            _ => throw new ArgumentOutOfRangeException()
        };
    }

    private double GetFTypeMass()
    {
        return _worldBuilderStar.LuminosityClass switch {
            LuminosityClass.Ia => GetMass(13,12,12),
            LuminosityClass.Ib => GetMass(12,10,10),
            LuminosityClass.II => GetMass(10,8,8),
            LuminosityClass.III => GetMass(4,3,2.5),
            LuminosityClass.IV => GetMass(2,1.7,1.5),
            LuminosityClass.V => GetMass(1.5,1.3,1.1),
            _ => throw new ArgumentOutOfRangeException()
        };
    }

    private double GetATypeMass()
    {
        return _worldBuilderStar.LuminosityClass switch {
            LuminosityClass.Ia => GetMass(20,15,13),
            LuminosityClass.Ib => GetMass(15,13,12),
            LuminosityClass.II => GetMass(14,11,10),
            LuminosityClass.III => GetMass(8,6,4),
            LuminosityClass.IV => GetMass(4,2.3,2),
            LuminosityClass.V => GetMass(2.2,1.8,1.5),
            _ => throw new ArgumentOutOfRangeException()
        };
    }

    private double GetBTypeMass()
    {
        return _worldBuilderStar.LuminosityClass switch {
            LuminosityClass.Ia => GetMass(60,30,20),
            LuminosityClass.Ib => GetMass(40,25,15),
            LuminosityClass.II => GetMass(30,20,14),
            LuminosityClass.III => GetMass(20,10,8),
            LuminosityClass.IV => GetMass(20,10,4),
            LuminosityClass.V => GetMass(18,5,2.2),
            LuminosityClass.VI => GetMass(.5,.4,.3),
            _ => throw new ArgumentOutOfRangeException()
        };
    }

    private double GetOTypeMass()
    {
        return _worldBuilderStar.LuminosityClass switch {
            LuminosityClass.Ia => GetMass(200, 80, 60),
            LuminosityClass.Ib => GetMass(150, 60, 40),
            LuminosityClass.II => GetMass(130, 40, 30),
            LuminosityClass.III => GetMass(110,30,20),
            LuminosityClass.V => GetMass(90,60,18),
            LuminosityClass.VI => GetMass(2,1.5,0.5),
            _ => throw new ArgumentOutOfRangeException()
        };
    }

    private double GetMass(double subclass0Mass, double subclass5Mass, double subclass10Mass)
    {
        double result;
        switch (_worldBuilderStar.SpectralSubclass) {
            case 0:
                return subclass0Mass + _rollingService.Variance(subclass0Mass, 20);
            case < 5:
                result = (subclass0Mass - subclass5Mass) / 5.0 * _worldBuilderStar.SpectralSubclass;
                result += subclass5Mass;
                return result + _rollingService.Variance(result, 20);
            case 5:
                return subclass5Mass + _rollingService.Variance(subclass5Mass, 20);
            case < 10:
                result = (subclass5Mass - subclass10Mass) / 5.0 * (_worldBuilderStar.SpectralSubclass - 5);
                result += subclass10Mass;
                return result + _rollingService.Variance(result, 20);
        }

        return 0;
    }

    public double GenerateBrownDwarfMass()
    {
        return _rollingService.D6(1) / 100.0 + (_rollingService.D6(4) - 1) / 1000.0;
    }

    public double GenerateWhiteDwarfMass()
    {
        return (_rollingService.D6(2) - 1) / 10.0 + (_rollingService.D10(1) / 100.0);
    }
}