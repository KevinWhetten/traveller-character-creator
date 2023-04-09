using SectorCreator.Global;
using SectorCreator.Global.Enums;
using SectorCreator.Models.Basic;

namespace SectorCreator.Models.RTTWorldgen;

public class RttWorldgenStar : Star
{
  public Guid Id { get; } = Guid.NewGuid();
  public RttWorldgenStarType RttWorldgenStarType { get; set; }
  public Luminosity Luminosity { get; set; }
  public CompanionOrbit CompanionOrbit { get; set; }
  public int ExpansionSize { get; set; }
  public int Age { get; set; }

  public void Generate(RttWorldgenStarType starType, RttWorldgenStar? primaryStar = null)
  {
    var primaryStarLuminosityRoll = 0;
    if (primaryStar != null)
    {
      primaryStarLuminosityRoll = GetSpectralRoll(primaryStar.SpectralType);
    }

    SpectralType = GenerateSpectralType(starType, primaryStarLuminosityRoll);
    
    FinishStarGeneration();
  }

  private int GetSpectralRoll(SpectralType primaryStarSpectralType)
  {
    return primaryStarSpectralType switch
    {
      SpectralType.A => 2,
      SpectralType.F => 3,
      SpectralType.G => 4,
      SpectralType.K => 5,
      SpectralType.M => 10,
      SpectralType.L => 14,
      SpectralType.D => 0,
      _ => throw new ArgumentOutOfRangeException()
    };
  }

  protected SpectralType GenerateSpectralType(RttWorldgenStarType starType, int primaryRoll = 0)
  {
    var roll = starType switch
    {
      RttWorldgenStarType.Primary => Roll.D6(2),
      RttWorldgenStarType.Companion => primaryRoll + Roll.D6(1) - 1,
      _ => throw new ArgumentOutOfRangeException(nameof(starType), starType, null)
    };

    return GetSpectralType(roll);
  }

  protected SpectralType GetSpectralType(int roll)
  {
    return roll switch
    {
      2 => SpectralType.A,
      3 => SpectralType.F,
      4 => SpectralType.G,
      5 => SpectralType.K,
      (>= 6 and <= 13) => SpectralType.M,
      (>= 14) => SpectralType.L,
      _ => SpectralType.D
    };
  }

  public void FinishStarGeneration(bool isCompanion = false)
  {
    Age = GetAge();

    if (SpectralType == SpectralType.A)
    {
      FinishATypeGeneration();
    }
    else if (SpectralType == SpectralType.F)
    {
      FinishFTypeGeneration();
    }
    else if (SpectralType == SpectralType.G)
    {
      FinishGTypeGeneration();
    }
    else if (SpectralType == SpectralType.K)
    {
      Luminosity = Luminosity.V;
    }
    else if (SpectralType == SpectralType.M)
    {
      FinishMTypeGeneration(isCompanion);
    }

    if (SpectralType == SpectralType.D || Luminosity == Luminosity.III)
    {
      ExpansionSize = Roll.D6(1);
    }
  }

  private int GetAge()
  {
    return Roll.D6(3) - 3;
  }

  private void FinishFTypeGeneration()
  {
    if (Age <= 2)
    {
      Luminosity = Luminosity.V;
    }
    else
    {
      var roll = Roll.D6(1);
      switch (roll)
      {
        case <= 2:
          SpectralType = SpectralType.G;
          Luminosity = Luminosity.IV;
          break;
        case 3:
          SpectralType = SpectralType.M;
          Luminosity = Luminosity.III;
          break;
        case <= 6:
          SpectralType = SpectralType.D;
          break;
      }
    }
  }

  private void FinishGTypeGeneration()
  {
    if (Age <= 2)
    {
      Luminosity = Luminosity.V;
    }
    else
    {
      var roll = Roll.D6(1);
      switch (roll)
      {
        case <= 2:
          SpectralType = SpectralType.K;
          Luminosity = Luminosity.IV;
          break;
        case 3:
          SpectralType = SpectralType.M;
          Luminosity = Luminosity.III;
          break;
        case <= 6:
          SpectralType = SpectralType.D;
          break;
      }
    }
  }

  private void FinishATypeGeneration()
  {
    if (Age <= 2)
    {
      Luminosity = Luminosity.V;
    }
    else
    {
      var roll = Roll.D6(1);
      switch (roll)
      {
        case <= 2:
          SpectralType = SpectralType.F;
          Luminosity = Luminosity.IV;
          break;
        case 3:
          SpectralType = SpectralType.K;
          Luminosity = Luminosity.III;
          break;
        case <= 6:
          SpectralType = SpectralType.D;
          break;
      }
    }
  }

  private void FinishMTypeGeneration(bool isCompanion)
  {
    switch (Roll.D6(2) + (isCompanion ? 2 : 0))
    {
      case <= 9:
        Luminosity = Luminosity.V;
        break;
      case <= 12:
        Luminosity = Luminosity.Ve;
        break;
      default:
        SpectralType = SpectralType.L;
        break;
    }
  }
  
  public CompanionOrbit GenerateOrbit()
  {
    return Roll.D6(1) switch {
      <= 2 => CompanionOrbit.Tight,
      <= 4 => CompanionOrbit.Close,
      5 => CompanionOrbit.Moderate,
      6 => CompanionOrbit.Distant,
      _ => CompanionOrbit.None
    };
  }
}
