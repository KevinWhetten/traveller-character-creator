using SectorCreator.Global;
using SectorCreator.Global.Enums;
using SectorCreator.Models.Base;
using SectorCreator.Models.RTTWorldgen;

namespace SectorCreator.Models;

public static class TradeCodeService
{
  public static List<TradeCode> TradeCodes = new();

  public static List<TradeCode> GetTradeCodes(Planet planet)
  {
    TradeCodes = new List<TradeCode>();
    // Planetary
    AddAsteroidTradeCode(planet);
    AddDesertTradeCode(planet);
    AddFluidOceansTradeCode(planet);
    AddGardenTradeCode(planet);
    AddHellworldTradeCode(planet);
    AddIceCappedTradeCode(planet);
    AddOceanWorldTradeCode(planet);
    AddVacuumTradeCode(planet);
    AddWaterWorldTradeCode(planet);

    // Population
    AddDiebackTradeCode(planet);
    AddBarrenTradeCode(planet);
    AddLowPopulationTradeCode(planet);
    AddNonIndustrialTradeCode(planet);
    AddPreHighPopulationTradeCode(planet);
    AddHighPopulationTradeCode(planet);

    // Economic
    AddPreAgriculturalTradeCode(planet);
    AddAgriculturalTradeCode(planet);
    AddNonAgriculturalTradeCode(planet);
    AddPreIndustrializedTradeCode(planet);
    AddIndustrialTradeCode(planet);
    AddPoorTradeCode(planet);
    AddPreRichTradeCode(planet);
    AddRichTradeCode(planet);

    // Climate
    AddFrozenTradeCode(planet);
    AddHotTradeCode(planet);
    AddColdTradeCode(planet);
    AddLockedTradeCode(planet);
    AddTropicTradeCode(planet);
    AddTundraTradeCode(planet);
    AddTwilightZoneTradeCode(planet);

    // Secondary
    AddFarmingTradeCode(planet);
    AddMiningTradeCode(planet);
    AddCaptiveTradeCode(planet);
    AddReserveTradeCode(planet);

    // Political
    AddSubsectorCapitalTradeCode(planet);
    AddSectorCapitalTradeCode(planet);
    AddCapitalTradeCode(planet);
    AddColonyTradeCode(planet);

    // Special
    AddSatelliteTradeCode(planet);
    AddForbiddenTradeCode(planet);
    AddAmberTradeCode(planet);
    AddDataRepositoryTradeCode(planet);
    AddAncientSiteTradeCode(planet);
    AddResearchStationTradeCode(planet);

    // Other
    AddHighTechnologyTradeCode(planet);
    AddLowTechnologyTradeCode(planet);
    return TradeCodes;
  }

  #region Planetary

  public static void AddAsteroidTradeCode(Planet planet)
  {
    if (planet.Size is 0 && planet.Atmosphere is 0 && planet.Hydrographics is 0)
    {
      TradeCodes.Add(TradeCode.Asteroid);
    }
  }

  public static void AddDesertTradeCode(Planet planet)
  {
    if (planet is { Atmosphere: >= 2, Hydrographics: 0 })
    {
      TradeCodes.Add(TradeCode.Desert);
    }
  }

  public static void AddFluidOceansTradeCode(Planet planet)
  {
    if (planet is { Atmosphere: >= 10, Hydrographics: >= 1 })
    {
      TradeCodes.Add(TradeCode.FluidOceans);
    }
  }

  public static void AddGardenTradeCode(Planet planet)
  {
    if (planet is { Atmosphere: >= 5, Hydrographics: >= 4 and <= 9, Population: >= 4 and <= 8 })
    {
      TradeCodes.Add(TradeCode.Garden);
    }
  }

  private static void AddHellworldTradeCode(Planet planet)
  {
    if (planet is { Size: >= 3, Atmosphere: 2 or 4 or 7 or >= 9 and <= 12, Hydrographics: >= 0 and <= 2 })
    {
      planet.TradeCodes.Add(TradeCode.Hellworld);
    }
  }

  public static void AddIceCappedTradeCode(Planet planet)
  {
    if (planet.Atmosphere is >= 0 and <= 1
        && planet.Hydrographics >= 1)
    {
      TradeCodes.Add(TradeCode.IceCapped);
    }
  }

  private static void AddOceanWorldTradeCode(Planet planet)
  {
    if (planet.Atmosphere is 0 or 1
        && planet.Hydrographics >= 10)
    {
      planet.TradeCodes.Add(TradeCode.OceanWorld);
    }
  }

  public static void AddVacuumTradeCode(Planet planet)
  {
    if (planet.Atmosphere == 0)
    {
      TradeCodes.Add(TradeCode.Vacuum);
    }
  }

  public static void AddWaterWorldTradeCode(Planet planet)
  {
    if (planet.Hydrographics == 10)
    {
      TradeCodes.Add(TradeCode.WaterWorld);
    }
  }

  #endregion

  #region Population

  private static void AddDiebackTradeCode(Planet planet)
  {
    if (planet is { Population: 0, Government: 0, LawLevel: 0, TechLevel: >= 1 })
    {
      planet.TradeCodes.Add(TradeCode.Dieback);
    }
  }

  public static void AddBarrenTradeCode(Planet planet)
  {
    if (planet.Population is 0 && planet.Government is 0 && planet.LawLevel == 0)
    {
      TradeCodes.Add(TradeCode.Barren);
    }
  }

  public static void AddLowPopulationTradeCode(Planet planet)
  {
    if (planet.Population is >= 1 and <= 3)
    {
      TradeCodes.Add(TradeCode.LowPopulation);
    }
  }

  public static void AddNonIndustrialTradeCode(Planet planet)
  {
    if (planet.Population is >= 4 and <= 6)
    {
      TradeCodes.Add(TradeCode.NonIndustrial);
    }
  }

  private static void AddPreHighPopulationTradeCode(Planet planet)
  {
    if (planet.Population == 8)
    {
      planet.TradeCodes.Add(TradeCode.PreHighPopulation);
    }
  }

  public static void AddHighPopulationTradeCode(Planet planet)
  {
    if (planet.Population >= 9)
    {
      TradeCodes.Add(TradeCode.HighPopulation);
    }
  }

  #endregion

  #region Economic

  private static void AddPreAgriculturalTradeCode(Planet planet)
  {
    if (planet.Atmosphere is >= 4 and <= 9
        && planet.Hydrographics is >= 4 and <= 8
        && planet.Population is 4 or 8)
    {
      planet.TradeCodes.Add(TradeCode.PreAgricultural);
    }
  }

  public static void AddAgriculturalTradeCode(Planet planet)
  {
    if (planet.Atmosphere is >= 4 and <= 9
        && planet.Hydrographics is >= 4 and <= 8
        && planet.Population is >= 5 and <= 7)
    {
      TradeCodes.Add(TradeCode.Agricultural);
    }
  }

  public static void AddNonAgriculturalTradeCode(Planet planet)
  {
    if (planet.Atmosphere is >= 0 and <= 3
        && planet.Hydrographics is >= 0 and <= 3
        && planet.Population >= 6)
    {
      TradeCodes.Add(TradeCode.NonAgricultural);
    }
  }

  private static void AddPreIndustrializedTradeCode(Planet planet)
  {
    if (planet.Atmosphere is 0 or 1 or 2 or 4 or 7 or 9
        && planet.Population is 7 or 8)
    {
      planet.TradeCodes.Add(TradeCode.PreIndustrial);
    }
  }

  public static void AddIndustrialTradeCode(Planet planet)
  {
    if (planet.Atmosphere is >= 0 and <= 2 or 4 or 7 or 9
        && planet.Population >= 9)
    {
      TradeCodes.Add(TradeCode.Industrial);
    }
  }

  public static void AddPoorTradeCode(Planet planet)
  {
    if (planet.Atmosphere is >= 2 and <= 5
        && planet.Hydrographics is >= 0 and <= 3)
    {
      TradeCodes.Add(TradeCode.Poor);
    }
  }

  private static void AddPreRichTradeCode(Planet planet)
  {
    if (planet.Atmosphere is 6 or 8
        && planet.Population is 5 or 9)
    {
      planet.TradeCodes.Add(TradeCode.PreRich);
    }
  }

  public static void AddRichTradeCode(Planet planet)
  {
    if (planet.Atmosphere is 6 or 8
        && planet.Population is >= 6 and <= 8)
    {
      TradeCodes.Add(TradeCode.Rich);
    }
  }

  #endregion

  #region Climate

  private static void AddFrozenTradeCode(Planet planet)
  {
    if (planet.Temperature <= 2
        || planet is RttWorldgenPlanet
        {
          Size: >= 2 and <= 9,
          Hydrographics: >= 1,
          PlanetOrbit: PlanetOrbit.Outer
        })
    {
      planet.TradeCodes.Add(TradeCode.Frozen);
    }
  }

  private static void AddHotTradeCode(Planet planet)
  {
    if (planet.Temperature >= 10
        || planet is RttWorldgenPlanet
        {
          Size: >= 2 and <= 9,
          PlanetOrbit: PlanetOrbit.Epistellar
        })
    {
      planet.TradeCodes.Add(TradeCode.Hot);
    }
  }

  private static void AddColdTradeCode(Planet planet)
  {
    if (planet.Temperature is >= 3 and <= 5)
    {
      planet.TradeCodes.Add(TradeCode.Cold);
    }
  }

  private static void AddLockedTradeCode(Planet planet)
  {
    if (planet is RttWorldgenPlanet worldgenPlanet
        && worldgenPlanet.ParentId != Guid.Empty
        && worldgenPlanet.SatelliteOrbit == CompanionOrbit.Close)
    {
      planet.TradeCodes.Add(TradeCode.Locked);
    }
  }

  private static void AddTropicTradeCode(Planet planet)
  {
    if (planet.Size is >= 6 and <= 9
        && planet.Atmosphere is >= 4 and <= 9
        && planet.Hydrographics is >= 3 and <= 7
        && planet.Temperature >= 8)
    {
      planet.TradeCodes.Add(TradeCode.Tropic);
    }
  }

  private static void AddTundraTradeCode(Planet planet)
  {
    if (planet.Size is >= 6 and <= 9
        && planet.Atmosphere is >= 4 and <= 9
        && planet.Hydrographics is >= 3 and <= 7
        && planet.Temperature <= 5)
    {
      planet.TradeCodes.Add(TradeCode.Tundra);
    }
  }

  private static void AddTwilightZoneTradeCode(Planet planet)
  {
    if (planet is RttWorldgenPlanet { OrbitPosition: <= 1 })
    {
      planet.TradeCodes.Add(TradeCode.TwilightZone);
    }
  }

  #endregion

  #region Secondary

  private static void AddFarmingTradeCode(Planet planet)
  {
    if (planet is RttWorldgenPlanet
        {
          Atmosphere: >= 4 and <= 9,
          Hydrographics: >= 4 and <= 8,
          Population: >= 2 and <= 6,
          PlanetOrbit: PlanetOrbit.Inner,
          IsMainWorld: false
        })
    {
      planet.TradeCodes.Add(TradeCode.Farming);
    }
  }

  private static void AddMiningTradeCode(Planet planet)
  {
    if (planet is RttWorldgenPlanet
        {
          Population: >= 2 and <= 6,
          IsMainWorld: false,
          PlanetType: PlanetType.AsteroidBelt
        })
    {
      planet.TradeCodes.Add(TradeCode.Mining);
    }
  }

  private static void AddCaptiveTradeCode(Planet planet)
  {
    if (planet.Government != 6) return;

    TradeCode? captiveType = planet switch
    {
      RttWorldgenPlanet worldgenPlanet =>
        Roll.D6(1) switch
        {
          <= 3 => TradeCode.MilitaryRule,
          >= 4 => worldgenPlanet.IsMainWorld ? TradeCode.PrisonCamp : TradeCode.PenalColony
        },
      _ => Roll.D3(1) switch
      {
        1 => TradeCode.MilitaryRule,
        2 => TradeCode.PrisonCamp,
        3 => TradeCode.PenalColony,
        _ => null
      }
    };

    if (captiveType != null)
    {
      planet.TradeCodes.Add(captiveType.Value);
    }
  }

  private static void AddReserveTradeCode(Planet planet)
  {
    if (planet is { TechLevel: <= 5, Starport: 'X', Population: >= 1 })
    {
      if (planet is RttWorldgenPlanet { Biosphere: >= 7 } or not RttWorldgenPlanet)
      {
        planet.TradeCodes.Add(TradeCode.Reserve);
      }
    }
  }

  #endregion

  #region Political

  private static void AddSubsectorCapitalTradeCode(Planet planet)
  {
    if (planet.Atmosphere >= 0)
    {
    }
  }

  private static void AddSectorCapitalTradeCode(Planet planet)
  {
    if (planet.Atmosphere >= 0)
    {
    }
  }

  private static void AddCapitalTradeCode(Planet planet)
  {
    if (planet is RttWorldgenPlanet
        {
          Biosphere: >= 12
        })
    {
      planet.TradeCodes.Add(TradeCode.Capital);
    }
  }

  private static void AddColonyTradeCode(Planet planet)
  {
    if (planet.Atmosphere >= 0)
    {
    }
  }

  #endregion

  #region Special

  private static void AddSatelliteTradeCode(Planet planet)
  {
    if (planet is RttWorldgenPlanet worldgenPlanet
        && worldgenPlanet.ParentId != Guid.Empty)
    {
      planet.TradeCodes.Add(TradeCode.Satellite);
    }
  }

  private static void AddForbiddenTradeCode(Planet planet)
  {
    if (planet.TravelCode == TravelCode.Red)
    {
      planet.TradeCodes.Add(TradeCode.Forbidden);
    }
  }

  private static void AddAmberTradeCode(Planet planet)
  {
    if (planet.TravelCode == TravelCode.Amber)
    {
      planet.TradeCodes.Add(TradeCode.Danger);
    }
    else if (Roll.D6(2) == 12)
    {
      planet.TradeCodes.Add(TradeCode.Puzzle);
    }
  }

  private static void AddDataRepositoryTradeCode(Planet planet)
  {
    if (Roll.D6(3) == 18)
    {
      planet.TradeCodes.Add(TradeCode.DataRepository);
      planet.Bases.Add(Global.Enums.Base.DataRepository);
    }
  }

  private static void AddAncientSiteTradeCode(Planet planet)
  {
    if (Roll.D6(3) == 18)
    {
      planet.TradeCodes.Add(TradeCode.AncientSite);
      planet.Bases.Add(Global.Enums.Base.AncientSite);
    }
  }

  private static void AddResearchStationTradeCode(Planet planet)
  {
    var roll = Roll.D6(2);
    switch (planet.Starport)
    {
      case 'A':
        if (roll >= 6)
        {
          planet.TradeCodes.Add(TradeCode.ResearchStation);
          planet.Bases.Add(Global.Enums.Base.Research);
        }

        if (roll >= 9)
        {
          planet.Bases.Add(Global.Enums.Base.Shipyard);
        }

        if (roll >= 12)
        {
          planet.Bases.Add(Global.Enums.Base.MegaCorporateHeadquarters);
        }

        break;
      case 'B':
        if (roll >= 8)
        {
          planet.TradeCodes.Add(TradeCode.ResearchStation);
          planet.Bases.Add(Global.Enums.Base.Research);
        }

        if (roll >= 11)
        {
          planet.Bases.Add(Global.Enums.Base.Shipyard);
        }

        break;
      case 'C':
        if (roll >= 10)
        {
          planet.TradeCodes.Add(TradeCode.ResearchStation);
          planet.Bases.Add(Global.Enums.Base.Research);
        }

        break;
    }
  }

  #endregion

  #region Other

  public static void AddHighTechnologyTradeCode(Planet planet)
  {
    if (planet.TechLevel >= 12)
    {
      TradeCodes.Add(TradeCode.HighTechnology);
    }
  }

  public static void AddLowTechnologyTradeCode(Planet planet)
  {
    if (planet.TechLevel <= 5)
    {
      TradeCodes.Add(TradeCode.LowTechnology);
    }
  }

  #endregion
}
