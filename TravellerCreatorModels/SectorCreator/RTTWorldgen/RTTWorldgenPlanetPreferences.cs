using System.Drawing;
using TravellerCreatorModels.SectorCreator.Enums;
namespace TravellerCreatorModels.SectorCreator.RTTWorldgen
{
  public interface IRttWorldgenPlanetPreferences
  {
    public string Code { get; }
    public Color Color { get; }
    public RTTWorldgenStar Star { get; }
    public RTTWorldgenPlanet Planet { get; }
  }

  public class HumanPreferences: IRttWorldgenPlanetPreferences
  {
    public string Code => "Hu";
    public Color Color => Color.Red;
    public RTTWorldgenStar Star => new RTTWorldgenStar
    {
      Luminosity = Luminosity.V,
      SpectralType = SpectralType.G
    };
    public RTTWorldgenPlanet Planet => new RTTWorldgenPlanet
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
      Bases = new List<Base> {
        Base.Naval,
        Base.Scout,
        Base.Research,
        Base.TAS
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
