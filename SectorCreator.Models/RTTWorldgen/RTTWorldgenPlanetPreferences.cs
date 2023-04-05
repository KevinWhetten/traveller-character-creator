using System.Drawing;
using SectorCreator.Global.Enums;

namespace SectorCreator.Models.RTTWorldgen
{
  public interface IRttWorldgenPlanetPreferences
  {
    public string Code { get; }
    public Color Color { get; }
    public RttWorldgenStar Star { get; }
    public RttWorldgenPlanet Planet { get; }
  }

  public class HumanPreferences: IRttWorldgenPlanetPreferences
  {
    public string Code => "Hu";
    public Color Color => Color.Red;
    public RttWorldgenStar Star => new()
    {
      Luminosity = Luminosity.V,
      SpectralType = SpectralType.G
    };
    public RttWorldgenPlanet Planet => new()
    {
      Name = "Earth",
      Size = 8,
      Atmosphere = 6,
      Hydrographics = 7,
      Temperature = 7,
      Population = 8,
      Government = 4,
      LawLevel = 1,
      Starport = 'C',
      TechLevel = 11,
      Bases = new List<Global.Enums.Base> {
        Global.Enums.Base.Naval,
        Global.Enums.Base.Scout,
        Global.Enums.Base.Research,
        Global.Enums.Base.Tas
      },
      TradeCodes = new List<TradeCode>
      {
        TradeCode.Garden,
        TradeCode.PreHighPopulation,
        TradeCode.PreAgricultural,
        TradeCode.Rich
      }
    };
  }
}
