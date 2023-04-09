using SectorCreator.Global;
using SectorCreator.Global.Enums;
using SectorCreator.Models.Basic;

namespace SectorCreator.Models;

public class PlanetFactory
{
  protected static Planet Planet { get; } = new();

  private static int AtmosphereMod => Planet.Atmosphere switch
  {
    0 or 1 or 10 or 11 or 12 => -4,
    _ => 0
  };

  public static Planet Generate(SectorType sectorType)
  {
    GenerateName();
    GenerateSize();
    GenerateAtmosphere();
    GenerateTemperature();
    GenerateHydrographics(sectorType);

    GeneratePopulation(sectorType);
    GenerateGovernment();
    GenerateLawLevel();
    GenerateStarport(sectorType);
    GenerateTechnologyLevel();
    
    GetTravelCode();
    GetBases();

    return Planet;
  }

  private static void GenerateName()
  {
    Planet.Name = "";
  }

  private static void GenerateSize()
  {
    Planet.Size = Roll.D6(2) - 2;
  }

  private static void GenerateAtmosphere()
  {
    Planet.Atmosphere = Roll.D6(2) - 7 + Planet.Size;

    if (Planet.Size is >= 0 and <= 2)
    {
      Planet.Atmosphere = 0;
    }
    else
    {
      Planet.Atmosphere = Planet.Atmosphere switch
      {
        (<= 2) => 0,
        (>= 3 and <= 5) => 1,
        (_) => 10
      };
    }
  }

  private static void GenerateTemperature()
  {
    var roll = Roll.D6(2);

    Planet.Temperature = Planet.Atmosphere switch
    {
      0 or 1 or 6 or 7 => roll,
      2 or 3 => roll - 2,
      4 or 5 or 14 => roll - 1,
      8 or 9 => roll + 1,
      10 or 13 or 15 => roll + 2,
      11 or 12 => roll + 6,
      _ => 0
    };
  }

  private static void GenerateHydrographics(SectorType sectorType)
  {
    if (Planet.Size <= 1)
    {
      Planet.Hydrographics = 0;
      return;
    }

    Planet.Hydrographics = Roll.D6(2) - 7 + Planet.Size + AtmosphereMod;

    switch (Planet.Temperature)
    {
      case >= 12:
        Planet.Hydrographics -= 6;
        break;
      case >= 10:
        Planet.Hydrographics -= 2;
        break;
    }

    if (sectorType is SectorType.SpaceOpera or SectorType.HardScience)
    {
      if ((Planet.Size is >= 3 and <= 4 && Planet.Atmosphere == 10)
          || Planet.Atmosphere <= 1)
      {
        Planet.Hydrographics -= 6;
      }
      else if (Planet.Atmosphere is >= 2 and <= 3 or 11 or 12)
      {
        Planet.Hydrographics -= 4;
      }
    }
  }

  protected static void GeneratePopulation(SectorType sectorType)
  {
    Planet.Population = Roll.D6(2) - 2;

    if (sectorType == SectorType.HardScience)
    {
      if (Planet.Size is <= 2 or 10)
      {
        Planet.Population--;
      }

      if (Planet.Atmosphere is 5 or 6 or 8)
      {
        Planet.Population++;
      }
      else
      {
        Planet.Population--;
      }
    }
  }

  protected static void GenerateGovernment()
  {
    Planet.Government = Planet.Population > 0
      ? Roll.D6(2) - 7 + Planet.Population
      : 0;
  }

  protected static void GenerateLawLevel()
  {
    Planet.LawLevel = Roll.D6(2) - 7 + Planet.Government;
  }

  public static void GenerateStarport(SectorType sectorType)
  {
    var roll = sectorType != SectorType.HardScience
      ? Roll.D6(2)
      : Roll.D6(2) - 7 + Planet.Population;

    Planet.Starport = roll switch
    {
      <= 2 => 'X',
      <= 4 => 'E',
      <= 6 => 'D',
      <= 8 => 'C',
      <= 10 => 'B',
      _ => 'A'
    };
  }

  protected static void GenerateTechnologyLevel()
  {
    Planet.TechLevel = Roll.D6(1);

    switch (Planet.Starport)
    {
      case 'A':
        Planet.TechLevel += 6;
        break;
      case 'B':
        Planet.TechLevel += 4;
        break;
      case 'C':
        Planet.TechLevel += 2;
        break;
      case 'X':
        Planet.TechLevel -= 4;
        break;
    }

    switch (Planet.Size)
    {
      case <= 1:
        Planet.TechLevel += 2;
        break;
      case <= 4:
        Planet.TechLevel++;
        break;
    }

    if (Planet.Atmosphere is <= 3 or >= 10)
    {
      Planet.TechLevel += 1;
    }

    switch (Planet.Hydrographics)
    {
      case 0 or 9:
        Planet.TechLevel++;
        break;
      case 10:
        Planet.TechLevel += 2;
        break;
    }

    switch (Planet.Population)
    {
      case (<= 5 and >= 1) or 9:
        Planet.TechLevel++;
        break;
      case 10:
        Planet.TechLevel += 2;
        break;
      case 11:
        Planet.TechLevel += 3;
        break;
      case 12:
        Planet.TechLevel += 4;
        break;
    }

    switch (Planet.Government)
    {
      case 0 or 5:
        Planet.TechLevel++;
        break;
      case 7:
        Planet.TechLevel += 2;
        break;
      case 13 or 14:
        Planet.TechLevel -= 2;
        break;
    }
  }

  protected static void GetTravelCode()
  {
    Planet.TravelCode = Planet.Atmosphere >= 10
                 && Planet.Government is 0 or 7 or 10
                 && Planet.LawLevel is 0 or >= 9
      ? TravelCode.Amber
      : TravelCode.None;
  }

  protected static void GetBases()
  {
    Planet.Bases = new List<Global.Enums.Base>();
    switch (Planet.Starport)
    {
      case 'A':
      {
        if (Roll.D6(2) >= 8)
        {
          Planet.Bases.Add(Global.Enums.Base.Naval);
        }

        if (Roll.D6(2) >= 10)
        {
          Planet.Bases.Add(Global.Enums.Base.Scout);
        }

        if (Roll.D6(2) >= 8)
        {
          Planet.Bases.Add(Global.Enums.Base.Research);
        }

        Planet.Bases.Add(Global.Enums.Base.Tas);
        break;
      }
      case 'B':
      {
        if (Roll.D6(2) >= 8)
        {
          Planet.Bases.Add(Global.Enums.Base.Naval);
        }

        if (Roll.D6(2) >= 8)
        {
          Planet.Bases.Add(Global.Enums.Base.Scout);
        }

        if (Roll.D6(2) >= 10)
        {
          Planet.Bases.Add(Global.Enums.Base.Research);
        }

        Planet.Bases.Add(Global.Enums.Base.Tas);
        break;
      }
      case 'C':
      {
        if (Roll.D6(2) >= 8)
        {
          Planet.Bases.Add(Global.Enums.Base.Scout);
        }

        if (Roll.D6(2) >= 10)
        {
          Planet.Bases.Add(Global.Enums.Base.Research);
        }

        if (Roll.D6(2) >= 10)
        {
          Planet.Bases.Add(Global.Enums.Base.Tas);
        }

        break;
      }
      case 'D':
      {
        if (Roll.D6(2) >= 7)
        {
          Planet.Bases.Add(Global.Enums.Base.Scout);
        }

        break;
      }
    }
  }
}
