using SectorCreator.Global;
using SectorCreator.Global.Enums;
using SectorCreator.Models.Basic;

namespace SectorCreator.Models.T5;

public class T5Planet : Planet
{
    public int HZVar { get; set; }
    public char SatelliteOrbit { get; set; }
    public CompanionOrbit SatelliteOrbitType { get; set; }
    public ParentType ParentType { get; set; }

    public T5Planet(IRollingService rollingService) : base(rollingService)
    { }

    public T5Planet(IRollingService rollingService, Planet planet) : base(rollingService, planet)
    { }
}