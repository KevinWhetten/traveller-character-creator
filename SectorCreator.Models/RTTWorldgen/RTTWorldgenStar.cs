using SectorCreator.Global.Enums;
using SectorCreator.Models.Basic;

namespace SectorCreator.Models.RTTWorldgen;

public class RttWorldgenStar : Star
{
    public RttWorldgenStar() { }
    public RttWorldgenStar(RttWorldgenStar newStar)
    {
        Id = newStar.Id;
        LuminosityClass = newStar.LuminosityClass;
        CompanionOrbit = newStar.CompanionOrbit;
        ExpansionSize = newStar.ExpansionSize;
        SpectralType = newStar.SpectralType;
        SpectralSubclass = newStar.SpectralSubclass;
        Age = newStar.Age;
    }

    public Guid Id { get; set; }
    public CompanionOrbit CompanionOrbit { get; set; }
    public int ExpansionSize { get; set; }
    public int Age { get; set; }
}