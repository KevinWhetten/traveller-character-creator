using TravellerCreatorGlobalMethods;
using TravellerCreatorModels.Enums;
using TravellerCreatorModels.Interfaces;

namespace TravellerCreatorModels.Basic;

public class BasicPlanet : IPlanet
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
    public PlanetType PlanetType { get; set; } = PlanetType.Terrestrial;
    public List<IPlanet> Moons { get; set; } = new();

    public void Generate(SectorType sectorType)
    {
        GenerateName();
        GenerateSize();
        GenerateAtmosphere();
        GenerateTemperature();
        GenerateHydrographics(sectorType);
        GeneratePopulation(sectorType);
        GenerateGovernment();
        GenerateLawLevel();
        GenerateStarport(sectorType);
        GenerateTechnologyLevel();
        GetBases();
        GetTravelCode();
    }

    private void GenerateName()
    {
        Name = "";
    }

    private void GenerateSize()
    {
        Size = Roll.D6(2) - 2;
    }

    private void GenerateAtmosphere()
    {
        int roll = Roll.D6(2) - 7 + Size;
        Atmosphere = roll > 0 ? roll : 0;
    }

    private void GenerateTemperature()
    {
        int roll = Roll.D6(2);

        switch (Atmosphere) {
            case 0:
            case 1:
            case 6:
            case 7:
                Temperature = roll;
                break;
            case 2:
            case 3:
                Temperature = roll - 2;
                break;
            case 4:
            case 5:
            case 14:
                Temperature = roll - 1;
                break;
            case 8:
            case 9:
                Temperature = roll + 1;
                break;
            case 10:
            case 13:
            case 15:
                Temperature = roll + 2;
                break;
            case 11:
            case 12:
                Temperature = roll + 6;
                break;
        }
    }

    private void GenerateHydrographics(SectorType sectorType)
    {
        if (Size <= 1) {
            Hydrographics = 0;
            return;
        }

        var roll = Roll.D6(2) - 7 + Size + AtmosphereMod();

        switch (Temperature) {
            case >= 12:
                roll -= 6;
                break;
            case >= 10:
                roll -= 2;
                break;
        }

        if (sectorType is SectorType.SpaceOpera or SectorType.HardScience) {
            if ((Size is >= 3 and <= 4 && Atmosphere == 10)
                || Atmosphere <= 1) {
                roll -= 6;
            } else if (Atmosphere is >= 2 and <= 3 or 11 or 12) {
                roll -= 4;
            }
        }

        Hydrographics = roll > 0 ? roll : 0;
    }

    private void GeneratePopulation(SectorType sectorType)
    {
        Population = Roll.D6(2) - 2;

        if (sectorType == SectorType.HardScience) {
            if (Size is <= 2 or 10) {
                Population--;
            }

            if (Atmosphere is 5 or 6 or 8) {
                Population++;
            } else {
                Population--;
            }
        }
    }

    private void GenerateGovernment()
    {
        var roll = Population > 0
            ? Roll.D6(2) - 7 + Population
            : 0;
        Government = roll > 0 ? roll : 0;
    }

    private void GenerateLawLevel()
    {
        var roll = Roll.D6(2) - 7 + Government;
        LawLevel = roll > 0 ? roll : 0;
    }

    private void GenerateStarport(SectorType sectorType)
    {
        int roll = sectorType != SectorType.HardScience
            ? Roll.D6(2)
            : Roll.D6(2) - 7 + Population;

        switch (roll) {
            case <= 2:
                Starport = 'X';
                return;
            case <= 4:
                Starport = 'E';
                return;
            case <= 6:
                Starport = 'D';
                return;
            case <= 8:
                Starport = 'C';
                return;
            case <= 10:
                Starport = 'B';
                return;
            default:
                Starport = 'A';
                return;
        }
    }

    private void GenerateTechnologyLevel()
    {
        TechLevel = Roll.D6(1);

        switch (Starport) {
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

        switch (Size) {
            case <= 1:
                TechLevel += 2;
                break;
            case <= 4:
                TechLevel++;
                break;
        }

        if (Atmosphere is <= 3 or >= 10) {
            TechLevel += 1;
        }

        switch (Hydrographics) {
            case 0 or 9:
                TechLevel++;
                break;
            case 10:
                TechLevel += 2;
                break;
        }

        switch (Population) {
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

        switch (Government) {
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

        TechLevel = TechLevel > 0 ? TechLevel : 0;
    }

    private void GetTravelCode()
    {
        if (Atmosphere >= 10
            && Government is 0 or 7 or 10
            && LawLevel is 0 or >= 9) {
            TravelCode = TravelCode.Amber;
        } else {
            TravelCode = TravelCode.None;
        }
    }

    private void GetBases()
    {
        switch (Starport) {
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
    }


    private int AtmosphereMod()
    {
        return Atmosphere switch {
            0 or 1 or 10 or 11 or 12 => -4,
            _ => 0
        };
    }
}