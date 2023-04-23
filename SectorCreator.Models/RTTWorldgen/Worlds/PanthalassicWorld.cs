using SectorCreator.Global;
using SectorCreator.Global.Enums;
using SectorCreator.Models.Services;

namespace SectorCreator.Models.RTTWorldgen.Worlds;

public interface IPanthalassicWorld
{
  RttWorldgenPlanet Generate(RttWorldgenPlanet planet, RttWorldgenStar primaryStar);
}
public class PanthalassicWorld: IPanthalassicWorld
{
  private int AgeMod;

  private readonly IRollingService _rollingService;

  public PanthalassicWorld(IRollingService rollingService)
  {
    _rollingService = rollingService;
  }

  public RttWorldgenPlanet Generate(RttWorldgenPlanet planet, RttWorldgenStar primaryStar)
  {
    planet.WorldType = WorldType.Panthalassic;
    planet.Size = _rollingService.D6(1) + 9;
    planet.Atmosphere = Math.Min(_rollingService.D6(1) + 8, 13);
    planet.Hydrographics = 11;
    planet.Chemistry = GetChemistry(primaryStar);
    planet.Biosphere = GetBiosphere(primaryStar, planet);
    planet = WorldValidation.ValidatePlanet(planet);
    return planet;
  }

  private PlanetChemistry GetChemistry(RttWorldgenStar primaryStar)
  {
    var chemistryRoll = _rollingService.D6(1);
    if (primaryStar.SpectralType == SpectralType.K && primaryStar.Luminosity == Luminosity.V)
    {
      chemistryRoll += 2;
    }
    else if (primaryStar.SpectralType == SpectralType.M && primaryStar.Luminosity == Luminosity.V)
    {
      chemistryRoll += 4;
    }
    else if (primaryStar.SpectralType == SpectralType.L)
    {
      chemistryRoll += 5;
    }

    switch (chemistryRoll)
    {
      case <= 6:
        return _rollingService.D6(2) switch
        {
          <= 8 => PlanetChemistry.Water,
          <= 11 => PlanetChemistry.Sulfur,
          _ => PlanetChemistry.Chlorine
        };
      case >= 7:
        AgeMod = chemistryRoll <= 8 ? 1 : 3;
        return PlanetChemistry.Methane;
    }
  }

  private int GetBiosphere(RttWorldgenStar primaryStar, RttWorldgenPlanet planet)
  {
    if (primaryStar.Age >= 4 + ChemistryService.GetAgeMod(planet.Chemistry, AgeMod))
      return _rollingService.D6(2);
    if (primaryStar.Age >= _rollingService.D3(1) + ChemistryService.GetAgeMod(planet.Chemistry, AgeMod))
      return _rollingService.D3(1);

    return 0;
  }
}
