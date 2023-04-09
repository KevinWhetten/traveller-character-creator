using SectorCreator.Global;
using SectorCreator.Global.Enums;
using SectorCreator.Models.Basic;

namespace SectorCreator.Models.StarFrontiers;

public abstract class StarFrontiersPlanetFactory : PlanetFactory
{
  private static StarFrontiersPlanet StarFrontiersPlanet => (StarFrontiersPlanet) Planet;
  
  public static StarFrontiersPlanet Generate(bool habitable, int habitableBase, int orbitNum)
  {
    GenerateType();
    GenerateSize();
    GenerateAtmosphere(habitable);
    GenerateHydrographics(habitable, habitableBase, orbitNum);
    GenerateMoons(habitable, habitableBase, orbitNum);
    GenerateSettlement();

    return StarFrontiersPlanet;
  }

  private static Planet GenerateSatellite(bool habitable, int habitableBase, int orbitNum)
  {
    GenerateType();
    GenerateSize();
    GenerateAtmosphere(habitable);
    GenerateHydrographics(habitable, habitableBase, orbitNum);
    GenerateSettlement();

    return StarFrontiersPlanet;
  }

  private static Planet GenerateSatellite(int size, int atmosphere, int hydrographics)
  {
    StarFrontiersPlanet.Size = size;
    StarFrontiersPlanet.Atmosphere = atmosphere;
    StarFrontiersPlanet.Hydrographics = hydrographics;
    
    GenerateSettlement();

    return StarFrontiersPlanet;
  }

  private static void GenerateSettlement()
  {
    GeneratePopulation(SectorType.StarFrontiers);
    GenerateGovernment();
    GenerateLawLevel();
    GenerateStarport(SectorType.StarFrontiers);
    GenerateTechnologyLevel();
    GetBases();
    GetTravelCode();
  }

  private static void GenerateType()
  {
    var roll = Roll.D10(1);

    StarFrontiersPlanet.PlanetType = roll switch
    {
      <= 2 => PlanetType.AsteroidBelt,
      <= 6 => PlanetType.Terrestrial,
      _ => PlanetType.Jovian
    };
  }

  private static void GenerateSize()
  {
    StarFrontiersPlanet.Size = StarFrontiersPlanet.PlanetType switch
    {
      PlanetType.AsteroidBelt => 0,
      PlanetType.Terrestrial => Roll.D10(Roll.D10(1) <= 5 ? 1 : 2),
      PlanetType.Jovian => Roll.D10(1) <= 5 ? 10 + Roll.D10(10) : 100 + Roll.D10(10),
      _ => throw new ArgumentOutOfRangeException()
    };
  }

  protected static void GenerateAtmosphere(bool habitable)
  {
    if (StarFrontiersPlanet.Size is 0 or 1)
    {
      StarFrontiersPlanet.Atmosphere = 0;
      return;
    }

    StarFrontiersPlanet.Atmosphere = StarFrontiersPlanet.Size + Roll.D10(1) - Roll.D10(1);

    if (!habitable)
    {
      if (Roll.D10(1) <= 6)
      {
        StarFrontiersPlanet.Atmosphere -= 5;
      }
      else
      {
        StarFrontiersPlanet.Atmosphere += 10;
      }
    }
  }

  protected static void GenerateHydrographics(bool habitable, int habitableBase, int planetNum)
  {
    StarFrontiersPlanet.Hydrographics = StarFrontiersPlanet.Size + Roll.D10(1) - Roll.D10(1);

    if (!habitable)
    {
      if (planetNum < habitableBase)
      {
        StarFrontiersPlanet.Hydrographics = 0;
      }
      else if (planetNum > habitableBase)
      {
        if (Roll.D10(1) <= 9)
        {
          StarFrontiersPlanet.Hydrographics -= 5;
        }
      }
    }

    if (StarFrontiersPlanet.Atmosphere is <= 1 or >= 10)
    {
      StarFrontiersPlanet.Hydrographics -= 5;
    }

    if (StarFrontiersPlanet.PlanetType == PlanetType.Jovian)
    {
      StarFrontiersPlanet.Hydrographics = 10;
    }
  }

  private static void GenerateMoons(bool habitable, int habitableBase, int planetNum)
  {
    var roll = Roll.D10(1);

    var numMoons = roll switch
    {
      <= 3 => (StarFrontiersPlanet.Size / 10) - Roll.D(5, 1),
      <= 7 => StarFrontiersPlanet.Size / 10,
      _ => (StarFrontiersPlanet.Size / 10) + Roll.D(5, 1)
    };

    for (var i = 0; i < numMoons; i++)
    {
      GenerateMoon(habitable, habitableBase, planetNum);
    }
  }

  private static void GenerateMoon(bool habitable, int habitableBase, int orbitNum)
  {
    StarFrontiersPlanet.Satellites.Add(Roll.D10(1) <= 9
      ? GenerateSatellite(0, 0, 0)
      : GenerateSatellite(habitable, habitableBase, orbitNum));
  }
}
