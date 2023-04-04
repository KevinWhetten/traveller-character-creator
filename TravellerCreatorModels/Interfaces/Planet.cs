using TravellerCreatorModels.Enums;

namespace TravellerCreatorModels.Interfaces;

public interface IPlanet
{
    string Name { get; set; }
    int Size { get; set; }
    int Atmosphere { get; set; }
    int Temperature { get; set; }
    int Hydrographics { get; set; }
    int Population { get; set; }
    int Government { get; set; }
    int LawLevel { get; set; }
    char Starport { get; set; }
    int TechLevel { get; set; }
    List<TradeCode> TradeCodes { get; set; }
    List<Base> Bases { get; set; }
    TravelCode TravelCode { get; set; }
    public PlanetType PlanetType { get; set; }
    public List<IPlanet> Satellites { get; set; }
}