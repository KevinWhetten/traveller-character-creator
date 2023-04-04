using TravellerCreatorGlobalMethods;
using TravellerCreatorModels.Enums;
using TravellerCreatorModels.Interfaces;
using TravellerCreatorModels.Mongoose;

namespace TravellerCharacterCreatorBL.Planet;

public class MongoosePlanetGenerator
{
    public MongoosePlanet GeneratePlanet(SectorType sectorType)
    {
        var planet = new MongoosePlanet();

        planet.Name = GenerateName();
        planet.Size = GenerateSize();
        planet.Atmosphere = GenerateAtmosphere(planet.Size);
        planet.Temperature = GenerateTemperature(planet.Atmosphere);
        planet.Hydrographics = GenerateHydrographics(planet, sectorType);
        planet.Population = GeneratePopulation(planet, sectorType);
        planet.Government = GenerateGovernment(planet.Population);
        planet.LawLevel = GenerateLawLevel(planet.Government);
        planet.Starport = GenerateStarport(planet.Population, sectorType);
        planet.TechLevel = GenerateTechnologyLevel(planet);
        planet.Bases = GetBases(planet.Starport);
        planet.TravelCode = GetTravelCode(planet);

        return planet;
    }

    private string GenerateName()
    {
        return "";
    }

    private int GenerateSize()
    {
        return Roll.D6(2) - 2;
    }

    private int GenerateAtmosphere(int size)
    {
        int roll = Roll.D6(2) - 7 + size;
        return roll > 0 ? roll : 0;
    }

    private int GenerateTemperature(int atmosphere)
    {
        int roll = Roll.D6(2);

        return atmosphere switch {
            0 or 1 or 6 or 7 => roll,
            2 or 3 => roll - 2,
            4 or 5 or 14 => roll - 1,
            8 or 9 => roll + 1,
            10 or 13 or 15 => roll + 2,
            11 or 12 => roll + 6,
            _ => 0
        };
    }

    private int GenerateHydrographics(IPlanet planet, SectorType sectorType)
    {
        if (planet.Size <= 1) {
            return 0;
        }

        int roll = Roll.D6(2) - 7 + planet.Size + AtmosphereMod(planet.Atmosphere);

        switch (planet.Temperature) {
            case >= 12:
                roll -= 6;
                break;
            case >= 10:
                roll -= 2;
                break;
        }

        if (sectorType is SectorType.SpaceOpera or SectorType.HardScience) {
            if ((planet.Size is >= 3 and <= 4 && planet.Atmosphere == 10)
                || planet.Atmosphere <= 1) {
                roll -= 6;
            } else if (planet.Atmosphere is >= 2 and <= 3 or 11 or 12) {
                roll -= 4;
            }
        }

        return roll > 0 ? roll : 0;
    }

    public int GeneratePopulation(IPlanet planet, SectorType sectorType)
    {
        int population = Roll.D6(2) - 2;

        if (sectorType == SectorType.HardScience) {
            if (planet.Size is <= 2 or 10) {
                population--;
            }

            if (planet.Atmosphere is 5 or 6 or 8) {
                population++;
            } else {
                population--;
            }
        }

        return population;
    }

    public int GenerateGovernment(int population)
    {
        int roll = population > 0
            ? Roll.D6(2) - 7 + population
            : 0;
        return roll > 0 ? roll : 0;
    }

    public int GenerateLawLevel(int government)
    {
        int roll = Roll.D6(2) - 7 + government;
        return roll > 0 ? roll : 0;
    }

    public char GenerateStarport(int Population, SectorType sectorType)
    {
        int roll = sectorType != SectorType.HardScience
            ? Roll.D6(2)
            : Roll.D6(2) - 7 + Population;

        return roll switch {
            <= 2 => 'X',
            <= 4 => 'E',
            <= 6 => 'D',
            <= 8 => 'C',
            <= 10 => 'B',
            _ => 'A'
        };
    }

    public int GenerateTechnologyLevel(IPlanet planet)
    {
        int TechLevel = Roll.D6(1);

        switch (planet.Starport) {
            case 'A':
                TechLevel += 6;
                break;
            case 'B':
                TechLevel += 4;
                break;
            case 'C':
                TechLevel += 2;
                break;
            case 'X':
                TechLevel -= 4;
                break;
        }

        switch (planet.Size) {
            case <= 1:
                TechLevel += 2;
                break;
            case <= 4:
                TechLevel++;
                break;
        }

        if (planet.Atmosphere is <= 3 or >= 10) {
            TechLevel += 1;
        }

        switch (planet.Hydrographics) {
            case 0 or 9:
                TechLevel++;
                break;
            case 10:
                TechLevel += 2;
                break;
        }

        switch (planet.Population) {
            case (<= 5 and >= 1) or 9:
                TechLevel++;
                break;
            case 10:
                TechLevel += 2;
                break;
            case 11:
                TechLevel += 3;
                break;
            case 12:
                TechLevel += 4;
                break;
        }

        switch (planet.Government) {
            case 0 or 5:
                TechLevel++;
                break;
            case 7:
                TechLevel += 2;
                break;
            case 13 or 14:
                TechLevel -= 2;
                break;
        }

        return TechLevel > 0 ? TechLevel : 0;
    }

    public TravelCode GetTravelCode(IPlanet planet)
    {
        return planet.Atmosphere >= 10
               && planet.Government is 0 or 7 or 10
               && planet.LawLevel is 0 or >= 9
            ? TravelCode.Amber
            : TravelCode.None;
    }

    public List<Base> GetBases(char starport)
    {
        var Bases = new List<Base>();
        switch (starport) {
            case 'A': {
                if (Roll.D6(2) >= 8) {
                    Bases.Add(Base.Naval);
                }

                if (Roll.D6(2) >= 10) {
                    Bases.Add(Base.Scout);
                }

                if (Roll.D6(2) >= 8) {
                    Bases.Add(Base.Research);
                }

                Bases.Add(Base.TAS);
                break;
            }
            case 'B': {
                if (Roll.D6(2) >= 8) {
                    Bases.Add(Base.Naval);
                }

                if (Roll.D6(2) >= 8) {
                    Bases.Add(Base.Scout);
                }

                if (Roll.D6(2) >= 10) {
                    Bases.Add(Base.Research);
                }

                Bases.Add(Base.TAS);
                break;
            }
            case 'C': {
                if (Roll.D6(2) >= 8) {
                    Bases.Add(Base.Scout);
                }

                if (Roll.D6(2) >= 10) {
                    Bases.Add(Base.Research);
                }

                if (Roll.D6(2) >= 10) {
                    Bases.Add(Base.TAS);
                }

                break;
            }
            case 'D': {
                if (Roll.D6(2) >= 7) {
                    Bases.Add(Base.Scout);
                }

                break;
            }
        }

        return Bases;
    }


    private int AtmosphereMod(int atmosphere)
    {
        return atmosphere switch {
            0 or 1 or 10 or 11 or 12 => -4,
            _ => 0
        };
    }
}