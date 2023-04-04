using System.Drawing;
using TravellerCreatorGlobalMethods;
using TravellerCreatorModels.Enums;
using TravellerCreatorModels.Interfaces;
using TravellerCreatorModels.StarFrontiers;

namespace TravellerCharacterCreatorBL.Planet;

public class StarFrontiersPlanetGenerator
{
    private readonly MongoosePlanetGenerator _mongoosePlanetGenerator;

    public StarFrontiersPlanetGenerator()
    {
        _mongoosePlanetGenerator = new MongoosePlanetGenerator();
    }
    
    public StarFrontiersPlanet GeneratePlanet(bool habitable, int habitableBase, int planetNum)
    {
        var planet = new StarFrontiersPlanet();
        
        planet.PlanetType = GenerateType();
        planet.Size = GenerateSize(planet.PlanetType);
        planet.Atmosphere = GenerateAtmosphere(habitable, planet.Size);
        planet.Hydrographics = GenerateHydrographics(planet, habitable, habitableBase, planetNum);
        planet.Satellites = GenerateMoons(planet.Size, habitable, habitableBase, planetNum);
        planet.Population = _mongoosePlanetGenerator.GeneratePopulation(planet, SectorType.StarFrontiers);
        planet.Government = _mongoosePlanetGenerator.GenerateGovernment(planet.Population);
        planet.LawLevel = _mongoosePlanetGenerator.GenerateLawLevel(planet.Government);
        planet.Starport = _mongoosePlanetGenerator.GenerateStarport(planet.Population, SectorType.StarFrontiers);
        planet.TechLevel = _mongoosePlanetGenerator.GenerateTechnologyLevel(planet);
        planet.Bases = _mongoosePlanetGenerator.GetBases(planet.Starport);
        planet.TravelCode = _mongoosePlanetGenerator.GetTravelCode(planet);

        return planet;
    }

    private PlanetType GenerateType()
    {
        int roll = Roll.D10(1);

        return roll switch {
            <= 2 => PlanetType.AsteroidBelt,
            <= 6 => PlanetType.Terrestrial,
            _ => PlanetType.Jovian
        };
    }

    private int GenerateSize(PlanetType planetType)
    {
        return planetType switch {
            PlanetType.AsteroidBelt => 0,
            PlanetType.Terrestrial => Roll.D10(Roll.D10(1) <= 5 ? 1 : 2),
            PlanetType.Jovian => Roll.D10(1) <= 5 ? 10 + Roll.D10(10) : 100 + Roll.D10(10),
            _ => throw new ArgumentOutOfRangeException()
        };
    }

    private int GenerateAtmosphere(bool habitable, int size)
    {
        var atmosphere = size + Roll.D10(1) - Roll.D10(1);

        if (!habitable) {
            if (Roll.D10(1) <= 6) {
                atmosphere -= 5;
            } else {
                atmosphere += 10;
            }
        }

        if (size is 0 or 1) {
            atmosphere = 0;
        }

        return atmosphere;
    }

    private int GenerateHydrographics(StarFrontiersPlanet planet, bool habitable, int habitableBase, int planetNum)
    {
        var hydrographics = planet.Size + Roll.D10(1) - Roll.D10(1);

        if (!habitable) {
            if (planetNum < habitableBase) {
                hydrographics = 0;
            } else if (planetNum > habitableBase) {
                if (Roll.D10(1) <= 9) {
                    hydrographics -= 5;
                }
            }
        }

        if (planet.Atmosphere is <= 1 or >= 10) {
            hydrographics -= 5;
        }

        if (planet.PlanetType == PlanetType.Jovian) {
            hydrographics = 10;
        }

        return hydrographics;
    }

    private List<IPlanet> GenerateMoons(int size, bool habitable, int habitableBase, int planetNum)
    {
        var moons = new List<IPlanet>();
        int roll = Roll.D10(1);

        int numMoons = roll switch {
            <= 3 => (size / 10) - Roll.D(5, 1),
            <= 7 => size / 10,
            _ => (size / 10) + Roll.D(5, 1)
        };

        for (var i = 0; i < numMoons; i++) {
            moons.Add(GenerateMoon(habitable, habitableBase, planetNum));
        }

        return moons;
    }

    private StarFrontiersPlanet GenerateMoon(bool habitable, int habitableBase, int planetNum)
    {
        var newMoon = new StarFrontiersPlanet();

        if (Roll.D10(1) <= 9) {
            newMoon.Size = 0;
            newMoon.Atmosphere = 0;
            newMoon.Hydrographics = 0;
        } else {
            newMoon.Size = GenerateSize(PlanetType.Terrestrial);
            newMoon.Atmosphere = GenerateAtmosphere(habitable, newMoon.Size);
            newMoon.Hydrographics = GenerateHydrographics(newMoon, habitable, habitableBase, planetNum);
        }

        return newMoon;
    }
}