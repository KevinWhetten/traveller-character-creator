using SectorCreator.Global.Enums;
using SectorCreator.WorldBuilder.Planet.AsteroidBelt;
using SectorCreator.WorldBuilder.Planet.GasGiant;
using SectorCreator.WorldBuilder.Planet.TerrestrialPlanet;

namespace SectorCreator.WorldBuilder.Planet.Planet;

public partial class WorldBuilderPlanet
{
    public int Size { get; set; }
    public int Diameter { get; set; }
    public double Mass => Density * (Diameter / 12742.0);
    public double Density { get; set; }
    public double Gravity => (Density * Diameter) / 12742.0;
    public double EscapeVelocity => Diameter > 0 ? Math.Sqrt(Mass / (Diameter / 12742.0)) * 11186 : 0;
    public double OrbitalVelocity => EscapeVelocity / Math.Sqrt(2);
    public string SizeProfile => $"{Size}-{Diameter}-{Density:N2}-{Gravity:N2}-{Mass:N2}";

    private void GenerateSizeCharacteristics(WorldBuilderStarSystem starSystem)
    {
        switch (PlanetType) {
            case PlanetType.AsteroidBelt:
                ((WorldBuilderAsteroidBelt)this).GenerateSizeCharacteristics(starSystem);
                break;
            case PlanetType.Jovian:
                ((WorldBuilderGasGiantPlanet)this).GenerateSizeCharacteristics(starSystem);
                break;
            case PlanetType.Terrestrial:
                ((WorldBuilderTerrestrialPlanet)this).GenerateSizeCharacteristics(starSystem);
                break;
        }
    }

    protected void GenerateDiameter()
    {
        var baseDiameter = Size switch {
            0 => 0,
            1 => 400,
            2 => 800,
            3 => 2400,
            4 => 4000,
            5 => 7200,
            6 => 8800,
            7 => 10400,
            8 => 12000,
            9 => 13600,
            10 => 15200,
            11 => 16800,
            12 => 18400,
            13 => 20000,
            14 => 21600,
            15 => 23200,
            _ => 0
        };

        var addition = 0;
        var addition2 = 0;
        do {
            if (Size != 26) {
                addition = _rollingService.D3(1) switch {
                    1 => 0,
                    2 => 600,
                    3 => 1200,
                    _ => 0
                };
            }

            do {
                addition2 = _rollingService.D6(1) switch {
                    1 => 0,
                    2 => 100,
                    3 => 200,
                    4 => 300,
                    5 => 400,
                    6 => 500
                };
            } while ((Size == 26 && addition2 < 400));
        } while (addition + addition2 >= 1600);

        addition += _rollingService.D10(1) * 10 + _rollingService.D10(1);

        Diameter = baseDiameter + addition + addition2;
    }
}