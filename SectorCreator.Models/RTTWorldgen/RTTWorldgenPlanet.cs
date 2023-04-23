using SectorCreator.Global.Enums;
using SectorCreator.Models.Basic;

namespace SectorCreator.Models.RTTWorldgen;

public class RttWorldgenPlanet : Planet
{
    public RttWorldgenPlanet(RttWorldgenPlanet rttWorldgenPlanet) : base(rttWorldgenPlanet)
    {
        Id = rttWorldgenPlanet.Id;
        Biosphere = rttWorldgenPlanet.Biosphere;
        Chemistry = rttWorldgenPlanet.Chemistry;
        Rings = rttWorldgenPlanet.Rings;
        IndustrialBase = rttWorldgenPlanet.IndustrialBase;
        WorldType = rttWorldgenPlanet.WorldType;
        PlanetOrbit = rttWorldgenPlanet.PlanetOrbit;
        OrbitPosition = rttWorldgenPlanet.OrbitPosition;
        IsMainWorld = rttWorldgenPlanet.IsMainWorld;
        ParentId = rttWorldgenPlanet.ParentId;
        StarId = rttWorldgenPlanet.StarId;
        SatelliteOrbit = rttWorldgenPlanet.SatelliteOrbit;
    }

    public RttWorldgenPlanet()
    { }

    public Guid Id { get; set; }

    public int Biosphere { get; set; }
    public PlanetChemistry Chemistry { get; set; }
    public Rings Rings { get; set; }

    public int IndustrialBase { get; set; }

    public int Desirability { get; set; }

    public WorldType WorldType { get; set; }
    public PlanetOrbit PlanetOrbit { get; set; }
    public int OrbitPosition { get; set; }
    public bool IsMainWorld { get; set; }

    public Guid ParentId { get; set; }
    public PlanetType ParentType { get; set; }
    public Guid StarId { get; set; }
    public CompanionOrbit SatelliteOrbit { get; set; }
}