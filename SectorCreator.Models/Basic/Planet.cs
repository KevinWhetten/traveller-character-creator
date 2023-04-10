using SectorCreator.Global.Enums;

namespace SectorCreator.Models.Basic;

public class Planet
{
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

    public int Size { get; set; }
    public int Atmosphere { get; set; }
    public int Hydrographics { get; set; }
    public PlanetType PlanetType { get; set; }
    public List<Planet> Satellites { get; } = new();
    public int Population { get; set; }
    public int Government { get; set; }
    public int LawLevel { get; set; }
    public int TechLevel { get; set; }
    public string Name { get; set; } = "Un-named";
    public int Temperature { get; set; }
    public char Starport { get; set; }
    public List<Base> Bases { get; set; } = new();
    public List<TradeCode> TradeCodes { get; set; } = new();
    public TravelCode TravelCode { get; set; }
}