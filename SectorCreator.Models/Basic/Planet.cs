using SectorCreator.Global;
using SectorCreator.Global.Enums;

namespace SectorCreator.Models.Basic;

public class Planet
{
    protected readonly IRollingService _rollingService;
    public int Size { get; set; }
    public int Atmosphere { get; set; }
    public int Hydrographics { get; set; }
    public PlanetType PlanetType { get; set; }
    public List<Planet> Satellites { get; set; } = new();
    public int Population { get; set; }
    public int Government { get; set; }
    public int LawLevel { get; set; }
    public int TechLevel { get; set; }
    public string Name { get; set; } = "";
    public Temperature Temperature { get; set; }
    public Starport Starport { get; set; } = new(new RollingService()) {Class = StarportClass.X};
    public List<string> Bases { get; set; } = new();
    public List<string> TradeCodes { get; set; } = new();
    public TravelZone TravelZone { get; set; } = TravelZone.None;
    public string Allegiance { get; set; } = "----";
    public Coordinates Coordinates { get; set; }

    public Planet(IRollingService rollingService)
    {
        _rollingService = rollingService;
    }

    public Planet(IRollingService rollingService, Planet planet)
    {
        _rollingService = rollingService;
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
        TravelZone = planet.TravelZone;
        Coordinates = planet.Coordinates;
    }

    public Planet(){ }

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

    public void GenerateSpaceport()
    {
        if (Population == 0) {
            Starport.Class = StarportClass.Y;
            return;
        }

        Starport.Class = (_rollingService.Flux() + Population) switch {
            <= 6 => StarportClass.Y,
            <= 8 => StarportClass.H,
            <= 10 => StarportClass.G,
            _ => StarportClass.F
        };
    }

    public void GenerateStarport()
    {
        if (Population == 0) {
            Starport.Class = StarportClass.X;
            return;
        }

        Starport.Class = (_rollingService.Flux() + Population) switch {
            <= 2 => StarportClass.X,
            <= 4 => StarportClass.E,
            <= 6 => StarportClass.D,
            <= 8 => StarportClass.C,
            <= 10 => StarportClass.B,
            _ => StarportClass.A
        };

        Starport.GenerateSpecialFeature();
        Starport.GenerateEvent();
        Starport.GenerateEnforcement(LawLevel);

        if (Starport.Installations.Contains(StarportInstallation.ArmyBase)) {
            Bases.Add(Base.Military);
        }

        if (Starport.Installations.Contains(StarportInstallation.NavalBase)) {
            Bases.Add(Base.Naval);
        }

        if (Starport.Installations.Contains(StarportInstallation.NavalDepot)) {
            Bases.Add(Base.NavalDepot);
        }

        if (Starport.Installations.Contains(StarportInstallation.ExplorerBase)) {
            Bases.Add(Base.Exploration);
        }

        if (Starport.Installations.Contains(StarportInstallation.WayStation)) {
            Bases.Add(Base.WayStation);
        }

        Starport.GenerateDefenses(Bases.Contains(Base.Naval) || Bases.Contains(Base.Military), Bases.Contains(Base.NavalDepot));
    }

    public void GenerateTechLevel(int roll)
    {
        if (Population == 0) {
            TechLevel = 0;
            return;
        }

        TechLevel = roll;

        TechLevel += Starport.Class switch {
            StarportClass.A => 6,
            StarportClass.B => 4,
            StarportClass.C => 2,
            StarportClass.D => 0,
            StarportClass.E => -2,
            StarportClass.X => -4,
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

    public void GenerateBases()
    {
        if (Population == 0) return;

        switch (Starport.Class) {
            case StarportClass.A:
                if (_rollingService.D6(2) >= 8) {
                    Bases.Add(Base.Naval);
                }

                if (_rollingService.D6(2) >= 10) {
                    Bases.Add(Base.Scout);
                }

                if (_rollingService.D6(2) >= 8) {
                    TradeCodes.Add(TradeCode.ResearchStation);
                }

                Starport.Installations.Add(StarportInstallation.TAS);

                break;
            case StarportClass.B:
                if (_rollingService.D6(2) >= 8) {
                    Bases.Add(Base.Naval);
                }

                if (_rollingService.D6(2) >= 8) {
                    Bases.Add(Base.Scout);
                }

                if (_rollingService.D6(2) >= 10) {
                    TradeCodes.Add(TradeCode.ResearchStation);
                }

                Starport.Installations.Add(StarportInstallation.TAS);
                break;
            case StarportClass.C:
                if (_rollingService.D6(2) >= 8) {
                    Bases.Add(Base.Scout);
                }

                if (_rollingService.D6(2) >= 10) {
                    TradeCodes.Add(TradeCode.ResearchStation);
                }

                if (_rollingService.D6(2) >= 10) {
                    Starport.Installations.Add(StarportInstallation.TAS);
                }

                break;
            case StarportClass.D:
                if (_rollingService.D6(2) >= 7) {
                    Bases.Add(Base.Scout);
                }

                break;
        }
    }

    public void GenerateName()
    {
        if (Allegiance == "----") {
            return;
        }

        var filePath = Allegiance switch {
            "Humn" => "../../../../Data/LanguageFiles/human_planets.txt",
            "Asln" => "../../../../Data/LanguageFiles/aslan_planets.txt",
            "Keku" => "../../../../Data/LanguageFiles/kekuu_planets.txt",
            "Stls" => "../../../../Data/LanguageFiles/ssitolusss_planets.txt",
            "Tort" => "../../../../Data/LanguageFiles/tortosian_planets.txt",
            "Blbs" => "../../../../Data/LanguageFiles/blubbus_planets.txt",
            "Crts" => "../../../../Data/LanguageFiles/chrotos_planets.txt",
            "KaSa" => "../../../../Data/LanguageFiles/kaSara_planets.txt",
            "Vrgr" => "../../../../Data/LanguageFiles/vargr_planets.txt",
            _ => "../../../../Data/LanguageFiles/human_planets.txt"
        };

        Name = NameGenerator.NameGenerator.GeneratePlanetName(filePath);
    }
}