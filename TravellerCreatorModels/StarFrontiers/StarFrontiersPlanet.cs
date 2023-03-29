using TravellerCreatorGlobalMethods;
using TravellerCreatorModels.Enums;
using TravellerCreatorModels.Interfaces;

namespace TravellerCreatorModels.StarFrontiers;

public class StarFrontiersPlanet : IPlanet
{
    public string Name { get; set; } = "";
    public int Size { get; set; }
    public int Atmosphere { get; set; }
    public int Temperature { get; set; }
    public int Hydrographics { get; set; }
    public int Population { get; set; }
    public int Government { get; set; }
    public int LawLevel { get; set; }
    public char Starport { get; set; }
    public int TechLevel { get; set; }
    public List<TradeCode> TradeCodes { get; set; } = new();
    public List<Base> Bases { get; set; } = new();
    public TravelCode TravelCode { get; set; }
    public PlanetType PlanetType { get; set; }
    public List<IPlanet> Moons { get; set; } = new();

    public void Generate(bool habitable, int habitableBase, int planetNum)
    {
        GenerateType();
        GenerateSize();
        GenerateAtmosphere(habitable);
        GenerateHydrographics(habitable, habitableBase, planetNum);
        GenerateMoons(habitable, habitableBase, planetNum);
    }

    private void GenerateType()
    {
        int roll = Roll.D10(1);

        PlanetType = roll switch {
            <= 2 => PlanetType.AsteroidBelt,
            <= 6 => PlanetType.Terrestrial,
            _ => PlanetType.GasGiant
        };
    }

    private void GenerateSize()
    {
        Size = PlanetType switch {
            PlanetType.AsteroidBelt => 0,
            PlanetType.Terrestrial => Roll.D10(Roll.D10(1) <= 5 ? 1 : 2),
            PlanetType.GasGiant => Roll.D10(1) <= 5 ? 10 + Roll.D10(10) : 100 + Roll.D10(10),
            _ => throw new ArgumentOutOfRangeException()
        };
    }

    private void GenerateAtmosphere(bool habitable)
    {
        Atmosphere = Size + Roll.D10(1) - Roll.D10(1);

        if (!habitable) {
            if (Roll.D10(1) <= 6) {
                Atmosphere -= 5;
            } else {
                Atmosphere += 10;
            }
        }

        if (Size is 0 or 1) {
            Atmosphere = 0;
        }
    }

    private void GenerateHydrographics(bool habitable, int habitableBase, int planetNum)
    {
        Hydrographics = Size + Roll.D10(1) - Roll.D10(1);

        if (!habitable) {
            if (planetNum < habitableBase) {
                Hydrographics = 0;
            } else if (planetNum > habitableBase) {
                if (Roll.D10(1) <= 9) {
                    Hydrographics -= 5;
                }
            }
        }

        if (Atmosphere is <= 1 or >= 10) {
            Hydrographics -= 5;
        }

        if (PlanetType == PlanetType.GasGiant) {
            Hydrographics = 10;
        }
    }

    private void GenerateMoons(bool habitable, int habitableBase, int planetNum)
    {
        int roll = Roll.D10(1);

        int numMoons = roll switch {
            <= 3 => (Size / 10) - Roll.D(5, 1),
            <= 7 => Size / 10,
            _ => (Size / 10) + Roll.D(5, 1)
        };

        for (var i = 0; i < numMoons; i++) {
            Moons.Add(GenerateMoon(habitable, habitableBase, planetNum));
        }
    }

    private StarFrontiersPlanet GenerateMoon(bool habitable, int habitableBase, int planetNum)
    {
        var newMoon = new StarFrontiersPlanet();

        if (Roll.D10(1) <= 9) {
            newMoon.Size = 0;
            newMoon.Atmosphere = 0;
            newMoon.Hydrographics = 0;
        } else {
            newMoon.GenerateSize();
            newMoon.GenerateAtmosphere(habitable);
            newMoon.GenerateHydrographics(habitable, habitableBase, planetNum);
        }

        return newMoon;
    }
}