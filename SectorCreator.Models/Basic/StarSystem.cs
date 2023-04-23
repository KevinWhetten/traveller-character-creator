namespace SectorCreator.Models.Basic;

public class StarSystem
{
    public List<Star> Stars { get; } = new();
    public List<Planet> Planets { get; } = new();
    public bool GasGiant { get; set; }
}