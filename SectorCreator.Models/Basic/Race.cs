using SectorCreator.Global;
using SectorCreator.Models.RTTWorldgen;

namespace SectorCreator.Models.Basic;

public class Race
{
    public string Name;
    public RttWorldgenPlanet Homeworld => (RttWorldgenPlanet) HomeSystem.Planets.FirstOrDefault(x => x.TradeCodes.Contains("Cx"));
    public RttWorldgenStarSystem HomeSystem { get; set; }
    public Coordinates HomeworldCoordinates;
    public double ExpansionRate;
    public string Allegiance { get; set; }
    public string HomeSystemFilename { get; set; }
    public double Resilience { get; set; }
    public int MaxPopulation { get; set; }
}