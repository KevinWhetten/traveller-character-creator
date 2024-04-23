using SectorCreator.Global.Enums;

namespace SectorCreator.Models.Services
{
  public static class ChemistryService
  {
    public static int GetAgeMod(PlanetChemistry planetChemistry, int panthalassicAgeMod = 0)
    {
      return planetChemistry switch
      {
        PlanetChemistry.None => 0,
        PlanetChemistry.Water => 0,
        PlanetChemistry.Ammonia => 1,
        PlanetChemistry.Methane => panthalassicAgeMod > 0 ? panthalassicAgeMod : 3,
        PlanetChemistry.Sulfur => 0,
        PlanetChemistry.Chlorine => 0,
        _ => 0
      };
    }
  }
}
