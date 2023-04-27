namespace SectorCreator.Models.Basic;

public class StarSystem
{
    public int PBG;
    public List<Star> Stars { get; } = new();
    public List<Planet> Planets { get; } = new();
    public bool GasGiant { get; set; }
}