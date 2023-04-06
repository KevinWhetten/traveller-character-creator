namespace SectorCreator.Models.Basic;

public class StarSystem
{
    public List<Star> Stars { get; set; } = new();
    public bool GasGiant { get; set; }
    public List<Planet> Planets { get; set; } = new();
}