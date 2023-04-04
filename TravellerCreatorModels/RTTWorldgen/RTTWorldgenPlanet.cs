using TravellerCreatorModels.Enums;
using TravellerCreatorModels.Interfaces;

namespace TravellerCreatorModels.RTTWorldgen;

public class RTTWorldgenPlanet : IPlanet
{
    public Guid Id { get; } = Guid.NewGuid();
    public string Name { get; set; } = "";
    
    public int Size { get; set; }
    public int Atmosphere { get; set; }
    public int Temperature { get; set; }
    public int Hydrographics { get; set; }
    public int Biosphere { get; set; }
    public PlanetChemistry Chemistry { get; set; }
    public Rings Rings { get; set; }
    
    public int Population { get; set; }
    public int Government { get; set; }
    public int LawLevel { get; set; }
    public char Starport { get; set; }
    public int TechLevel { get; set; }
    
    public List<TradeCode> TradeCodes { get; set; } = new();
    public List<Base> Bases { get; set; } = new();
    public TravelCode TravelCode { get; set; }
    
    public PlanetType PlanetType { get; set; }
    public WorldType WorldType { get; set; }
    public PlanetOrbit PlanetOrbit { get; set; }
    public int OrbitPosition { get; set; }
    
    public Guid ParentId { get; set; }
    public Guid StarId { get; set; }

    public List<IPlanet> Satellites { get; set; } = new();
}