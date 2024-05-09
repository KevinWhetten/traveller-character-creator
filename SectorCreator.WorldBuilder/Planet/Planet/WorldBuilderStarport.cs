using SectorCreator.Models.Basic;

namespace SectorCreator.WorldBuilder.Planet.Planet;

public class WorldBuilderStarport
{
    public StarportClass Class { get; set; }
    public bool Highport { get; set; }
    public int DailyTraffic { get; set; }
    public int DailyMaximum { get; set; }
    public int ExpectedWeekly { get; set; }
    public int HighportTotalDockingCapacity { get; set; }
    public int DownportTotalDockingCapacity { get; set; }
    public int ShipyardBuildCapacity { get; set; }
    public int AnnualShipyardOutput { get; set; }
}