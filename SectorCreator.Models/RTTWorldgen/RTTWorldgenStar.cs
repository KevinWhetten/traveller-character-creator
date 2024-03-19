using SectorCreator.Global.Enums;
using SectorCreator.Models.Basic;

namespace SectorCreator.Models.RTTWorldgen;

public class RttWorldgenStar : Star
{
    public RttWorldgenStar() { }
    public RttWorldgenStar(RttWorldgenStar newStar)
    {
        Id = newStar.Id;
        Luminosity = newStar.Luminosity;
        CompanionOrbit = newStar.CompanionOrbit;
        ExpansionSize = newStar.ExpansionSize;
        Age = newStar.Age;
    }

    public Guid Id { get; set; }
    public Luminosity Luminosity { get; set; }
    public CompanionOrbit CompanionOrbit { get; set; }
    public int ExpansionSize { get; set; }
    public int Age { get; set; }
}