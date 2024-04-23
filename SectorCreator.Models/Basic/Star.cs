using SectorCreator.Global.Enums;

namespace SectorCreator.Models.Basic;

public class Star
{
    public string Name { get; set; }
    public SpectralType SpectralType { get; set; }
    public int SpectralSubclass { get; set; }
    public LuminosityClass LuminosityClass { get; set; }
}