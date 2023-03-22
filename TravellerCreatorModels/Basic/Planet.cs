using TravellerCreatorGlobalMethods;

namespace TravellerCreatorModels.Basic;

public interface IPlanet
{ }

public class Planet : IPlanet
{
    public int Size { get; set; }
    public int Atmosphere { get; set; }
    public int Hydrographics { get; set; }
    public int Population { get; set; }
    public int Government { get; set; }
    public int LawLevel { get; set; }
    public char Starport { get; set; }
    public int TechLevel { get; set; }
    public List<TradeCode> TradeCodes { get; set; } = new();

    public void Generate()
    {
        GenerateSize();
        GenerateAtmosphere();
        GenerateHydrographics();
        GeneratePopulation();
        GenerateGovernment();
        GenerateLawLevel();
        GenerateStarport();
        GenerateTechnologyLevel();
    }

    private void GenerateSize()
    {
        Size = Roll.D6(2) - 2;
    }

    private void GenerateAtmosphere()
    {
        var roll = Roll.D6(2) - 7 + Size;
        Atmosphere = roll > 0 ? roll : 0;
    }

    private void GenerateHydrographics()
    {
        if (Size <= 1) {
            Hydrographics = 0;
            return;
        }

        var roll = Roll.D6(2) - 7 + Size + AtmosphereMod();
        Hydrographics = roll > 0 ? roll : 0;
    }

    private void GeneratePopulation()
    {
        Population = Roll.D6(2) - 2;
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

    private void GenerateStarport()
    {
        switch (Roll.D6(2)) {
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


    private int AtmosphereMod()
    {
        return Atmosphere switch {
            0 or 1 or 10 or 11 or 12 => -4,
            _ => 0
        };
    }
}