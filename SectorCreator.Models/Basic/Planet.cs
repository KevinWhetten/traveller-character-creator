using MarkovChain;
using SectorCreator.Global;
using SectorCreator.Global.Enums;
using SectorCreator.Models.Factories.RttWorldgen;

namespace SectorCreator.Models.Basic;

public class Planet
{
    public int Size { get; set; }
    public int Atmosphere { get; set; }
    public int Hydrographics { get; set; }
    public PlanetType PlanetType { get; set; }
    public List<Planet> Satellites { get; } = new();
    public string PopulatedBy { get; set; }
    public int Population { get; set; }
    public int Government { get; set; }
    public int LawLevel { get; set; }
    public int TechLevel { get; set; }
    public string Name { get; set; } = "";
    public Temperature Temperature { get; set; }
    public char Starport { get; set; } = 'X';
    public List<string> Bases { get; set; } = new();
    public List<string> TradeCodes { get; set; } = new();
    public TravelCode TravelCode { get; set; } = TravelCode.None;
    public Coordinates Coordinates { get; set; }

    public Planet()
    { }

    public Planet(Planet planet)
    {
        Size = planet.Size;
        Atmosphere = planet.Atmosphere;
        Hydrographics = planet.Hydrographics;
        PlanetType = planet.PlanetType;
        Satellites = planet.Satellites;
        Population = planet.Population;
        Government = planet.Government;
        LawLevel = planet.LawLevel;
        TechLevel = planet.TechLevel;
        Name = planet.Name;
        Temperature = planet.Temperature;
        Starport = planet.Starport;
        Bases = planet.Bases;
        TradeCodes = planet.TradeCodes;
        TravelCode = planet.TravelCode;
    }

    public void Populate(int roll, Race race, int distanceBetween)
    {
        PopulatedBy = race.Name;

        var hydrographicDifference = Math.Abs(race.Homeworld.Hydrographics - Hydrographics);
        var sizeDifference = Math.Abs(race.Homeworld.Size - Size);
        var temperatureDifference = Math.Abs(race.Homeworld.Temperature - Temperature);

        Population = roll - hydrographicDifference - (sizeDifference / 2) - (distanceBetween / 2);

        if (Population <= 0) {
            Population = 1;
        }
    }

    public void SetGovernment(int roll, Race race)
    {
        if (Population == 0) {
            Government = 0;
            return;
        }
        
        Government = race.Homeworld.Government + roll;

        if (Government < 0) Government = 0;
        if (Government > 15) Government = 15;
    }

    public void SetLawLevel(int roll, Race race)
    {
        if (Population == 0) {
            LawLevel = 0;
            return;
        }
        
        LawLevel = race.Homeworld.LawLevel + roll;

        if (LawLevel < 0) LawLevel = 0;
        if (LawLevel > 24) LawLevel = 24;
    }

    public void GenerateStarport(int roll)
    {
        if (Population == 0) {
            Starport = 'X';
            return;
        }
        
        Starport = (roll + Population) switch {
            <= 2 => 'X',
            <= 4 => 'E',
            <= 6 => 'D',
            <= 8 => 'C',
            <= 10 => 'B',
            _ => 'A'
        };
    }

    public void GenerateTechLevel(int roll)
    {
        if (Population == 0) {
            TechLevel = 0;
            return;
        }
        
        TechLevel = roll;

        TechLevel += Starport switch {
            'A' => 6,
            'B' => 4,
            'C' => 2,
            'D' => 0,
            'E' => -2,
            'X' => -4,
            _ => 0
        };

        TechLevel += Size switch {
            <= 1 => 2,
            <= 4 => 1,
            _ => 0
        };

        TechLevel += Atmosphere switch {
            <= 3 => 1,
            >= 10 => 1,
            _ => 0
        };

        TechLevel += Hydrographics switch {
            0 => 1,
            9 => 1,
            >= 10 => 2,
            _ => 0
        };

        TechLevel += Population switch {
            >= 0 and <= 5 => 1,
            9 => 1,
            10 => 2,
            11 => 3,
            >= 12 => 4,
            _ => 0
        };

        TechLevel += Government switch {
            0 => 1,
            5 => 1,
            7 => 2,
            13 => -2,
            14 => -2,
            _ => 0
        };

        if (TechLevel < 0) {
            TechLevel = 0;
        }
    }

    public void GenerateBases(IRollingService rollingService)
    {
        if (Population == 0) return;

        switch (Starport) {
            case 'A':
                if (rollingService.D6(2) >= 8) {
                    Bases.Add(Base.Naval);
                }

                if (rollingService.D6(2) >= 10) {
                    Bases.Add(Base.Scout);
                }

                if (rollingService.D6(2) >= 8) {
                    Bases.Add(Base.Research);
                }
                
                Bases.Add(Base.Tas);

                break;
            case 'B':
                if (rollingService.D6(2) >= 8) {
                    Bases.Add(Base.Naval);
                }

                if (rollingService.D6(2) >= 8) {
                    Bases.Add(Base.Scout);
                }

                if (rollingService.D6(2) >= 10) {
                    Bases.Add(Base.Research);
                }
                
                Bases.Add(Base.Tas);
                break;
            case 'C':
                if (rollingService.D6(2) >= 8) {
                    Bases.Add(Base.Scout);
                }

                if (rollingService.D6(2) >= 10) {
                    Bases.Add(Base.Research);
                }

                if (rollingService.D6(2) >= 10) {
                    Bases.Add(Base.Tas);
                }
                
                break;
            case 'D':
                if (rollingService.D6(2) >= 7) {
                    Bases.Add(Base.Scout);
                }
                break;
        }
    }

    public void SetTravelZone()
    {
        if (TradeCodes.Contains("Da") || TradeCodes.Contains("Pz")) {
            TravelCode = TravelCode.Amber;
        }

        if (TradeCodes.Contains("Fo")) {
            TravelCode = TravelCode.Red;
        }
    }

    public void GenerateName()
    {
        var filePath = PopulatedBy switch {
            "Human" => "../../../../MarkovChain/TextFiles/human_planets.txt",
            "Aslan" => "../../../../MarkovChain/TextFiles/aslan_planets.txt",
            "Mannu" => "../../../../MarkovChain/TextFiles/mannu_planets.txt",
            "Largosians" => "../../../../MarkovChain/TextFiles/largosian_planets.txt",
            "Tortosians" => "../../../../MarkovChain/TextFiles/tortosian_planets.txt",
            "Ithromir" => "../../../../MarkovChain/TextFiles/ithromir_planets.txt",
            "Chrotos" => "../../../../MarkovChain/TextFiles/chrotos_planets.txt",
            "Ka'Sara" => "../../../../MarkovChain/TextFiles/kaSara_planets.txt",
            "Vargr" => "../../../../MarkovChain/TextFiles/vargr_planets.txt"
        };

        var markovChain = new global::MarkovChain.MarkovChain(new LargosianSpelling());
        Name = markovChain.GeneratePlanetName(filePath);
    }
}