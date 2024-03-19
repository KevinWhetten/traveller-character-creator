namespace SectorCreator.Global;

public static class AtmosphereService
{
    public static Atmosphere GetAtmosphere(int code)
    {
        switch (code) {
            case 0:
                return new Atmosphere {
                    Density = 0,
                    Tainted = false
                };
            case 1:
                return new Atmosphere {
                    Density = 1,
                    Tainted = false
                };
            case 2:
                return new Atmosphere {
                    Density = 2,
                    Tainted = true
                };
            case 3:
                return new Atmosphere {
                    Density = 2,
                    Tainted = false
                };
            case 4:
                return new Atmosphere {
                    Density = 3,
                    Tainted = true
                };
            case 5:
                return new Atmosphere {
                    Density = 3,
                    Tainted = false
                };
            case 6:
                return new Atmosphere {
                    Density = 4,
                    Tainted = false
                };
            case 7:
                return new Atmosphere {
                    Density = 4,
                    Tainted = true
                };
            case 8:
                return new Atmosphere {
                    Density = 5,
                    Tainted = false
                };
            case 9:
                return new Atmosphere {
                    Density = 5,
                    Tainted = true
                };
            case 10:
                return new Atmosphere {
                    Density = 6,
                    Tainted = true
                };
            case 11:
                return new Atmosphere {
                    Density = 6,
                    Tainted = true
                };
            case 12:
                return new Atmosphere {
                    Density = 6,
                    Tainted = true
                };
            case 13:
                return new Atmosphere {
                    Density = 7,
                    Tainted = false
                };
            case 14:
                return new Atmosphere {
                    Density = 0,
                    Tainted = false
                };
            case 15:
                return new Atmosphere {
                    Density = 0,
                    Tainted = false
                };
            default:
                return new Atmosphere {
                    Density = 0,
                    Tainted = false
                };
        }
    }   
}

public class Atmosphere
{
    public int Density;
    public bool Tainted;
}