using SectorCreator.Global;
using SectorCreator.Global.Enums;
using SectorCreator.Models.Basic;

namespace SectorCreator.Models.Factories;

public interface IPlanetFactory
{
  public Planet Generate(SectorType sectorType);
}

public class PlanetFactory : IPlanetFactory
{
  private readonly IRollingService _rollingService;

  public PlanetFactory(IRollingService rollingService)
  {
    _rollingService = rollingService;
  }
  
  protected Planet Planet { get; } = new();

  private int AtmosphereMod => Planet.Atmosphere switch
  {
    0 or 1 or 10 or 11 or 12 => -4,
    _ => 0
  };

  public virtual Planet Generate(SectorType sectorType)
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

  private void GenerateName()
  {
    Planet.Name = "";
  }

  private void GenerateSize()
  {
    Planet.Size = _rollingService.D6(2) - 2;
  }

  private void GenerateAtmosphere()
  {
    Planet.Atmosphere = _rollingService.D6(2) - 7 + Planet.Size;

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

  private void GenerateTemperature()
  {
    var roll = _rollingService.D6(2);

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

  private void GenerateHydrographics(SectorType sectorType)
  {
    if (Planet.Size <= 1)
    {
      Planet.Hydrographics = 0;
      return;
    }

    Planet.Hydrographics = _rollingService.D6(2) - 7 + Planet.Size + AtmosphereMod;

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

  protected void GeneratePopulation(SectorType sectorType)
  {
    Planet.Population = _rollingService.D6(2) - 2;

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

  protected void GenerateGovernment()
  {
    Planet.Government = Planet.Population > 0
      ? _rollingService.D6(2) - 7 + Planet.Population
      : 0;
  }

  protected void GenerateLawLevel()
  {
    Planet.LawLevel = _rollingService.D6(2) - 7 + Planet.Government;
  }

  protected void GenerateStarport(SectorType sectorType)
  {
    var roll = sectorType != SectorType.HardScience
      ? _rollingService.D6(2)
      : _rollingService.D6(2) - 7 + Planet.Population;

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

  protected void GenerateTechnologyLevel()
  {
    Planet.TechLevel = _rollingService.D6(1);

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

  protected void GetTravelCode()
  {
    Planet.TravelCode = Planet is { Atmosphere: >= 10, Government: 0 or 7 or 10, LawLevel: 0 or >= 9 }
      ? TravelCode.Amber
      : TravelCode.None;
  }

  protected void GetBases()
  {
    Planet.Bases = new List<Base>();
    switch (Planet.Starport)
    {
      case 'A':
      {
        if (_rollingService.D6(2) >= 8)
        {
          Planet.Bases.Add(Base.Naval);
        }

        if (_rollingService.D6(2) >= 10)
        {
          Planet.Bases.Add(Base.Scout);
        }

        if (_rollingService.D6(2) >= 8)
        {
          Planet.Bases.Add(Base.Research);
        }

        Planet.Bases.Add(Base.Tas);
        break;
      }
      case 'B':
      {
        if (_rollingService.D6(2) >= 8)
        {
          Planet.Bases.Add(Base.Naval);
        }

        if (_rollingService.D6(2) >= 8)
        {
          Planet.Bases.Add(Base.Scout);
        }

        if (_rollingService.D6(2) >= 10)
        {
          Planet.Bases.Add(Base.Research);
        }

        Planet.Bases.Add(Base.Tas);
        break;
      }
      case 'C':
      {
        if (_rollingService.D6(2) >= 8)
        {
          Planet.Bases.Add(Base.Scout);
        }

        if (_rollingService.D6(2) >= 10)
        {
          Planet.Bases.Add(Base.Research);
        }

        if (_rollingService.D6(2) >= 10)
        {
          Planet.Bases.Add(Base.Tas);
        }

        break;
      }
      case 'D':
      {
        if (_rollingService.D6(2) >= 7)
        {
          Planet.Bases.Add(Base.Scout);
        }

        break;
      }
    }
  }
}
