using SectorCreator.Global;

namespace SectorCreator.Models.Basic;

public class StarSystem
{
    public int PBG;
    public Star PrimaryStar = new();
    public List<Star> CompanionStars { get; } = new();
    public List<Planet> Planets { get; } = new();
    public bool GasGiant { get; set; }
    public Coordinates Coordinates { get; set; } = new();
}