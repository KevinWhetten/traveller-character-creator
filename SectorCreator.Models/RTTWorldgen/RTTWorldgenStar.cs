using SectorCreator.Global.Enums;
using SectorCreator.Models.Basic;

namespace SectorCreator.Models.RTTWorldgen;

public class RttWorldgenStar : Star
{
    public Guid Id { get; set; }
    public bool IsPrimary { get; set; } = true;
    public Luminosity Luminosity { get; set; }
    public CompanionOrbit CompanionOrbit { get; set; }
    public int ExpansionSize { get; set; }
    public int Age { get; set; }
}