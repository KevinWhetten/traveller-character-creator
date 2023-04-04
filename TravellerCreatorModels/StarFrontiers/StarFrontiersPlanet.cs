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
    public List<IPlanet> Satellites { get; set; } = new();
}