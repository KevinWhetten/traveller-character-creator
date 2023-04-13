using SectorCreator.Global;
using SectorCreator.Global.Enums;
using SectorCreator.Models.RTTWorldgen;

namespace SectorCreator.Models.Factories;

public interface IRttWorldgenStarFactory
{
  RttWorldgenStar Generate(StarType starType, RttWorldgenStar? primaryStar = null);
  CompanionOrbit GenerateOrbit();
}

public class RttWorldgenStarFactory : IRttWorldgenStarFactory
{
  private readonly IRollingService _rollingService;
  private RttWorldgenStar Star = new();

  public RttWorldgenStarFactory(IRollingService rollingService)
  {
    _rollingService = rollingService;
  }
  
    public RttWorldgenStar Generate(StarType starType, RttWorldgenStar? primaryStar = null)
    {
      Star = new RttWorldgenStar();
        var primaryStarLuminosityRoll = 0;
        if (primaryStar != null) {
            primaryStarLuminosityRoll = GetSpectralRoll(primaryStar.SpectralType);
        }

        Star.SpectralType = GenerateSpectralType(starType, primaryStarLuminosityRoll);

        Star = FinishStarGeneration(Star);

        return Star;
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

  private SpectralType GenerateSpectralType(StarType starType, int primaryRoll = 0)
  {
    var roll = starType switch
    {
      StarType.Primary => _rollingService.D6(2),
      StarType.Companion => primaryRoll + _rollingService.D6(1) - 1,
      _ => throw new ArgumentOutOfRangeException(nameof(starType), starType, null)
    };

    return GetSpectralType(roll);
  }

  private SpectralType GetSpectralType(int roll)
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

  private RttWorldgenStar FinishStarGeneration(RttWorldgenStar star, bool isCompanion = false)
  {
    Star.Age = GetAge();

    switch (Star.SpectralType) {
      case SpectralType.A:
        Star = FinishATypeGeneration(star);
        break;
      case SpectralType.F:
        Star = FinishFTypeGeneration(star);
        break;
      case SpectralType.G:
        Star = FinishGTypeGeneration(star);
        break;
      case SpectralType.K:
        Star.Luminosity = Luminosity.V;
        break;
      case SpectralType.M:
        Star = FinishMTypeGeneration(star, isCompanion);
        break;
    }

    if (Star.SpectralType == SpectralType.D || Star.Luminosity == Luminosity.III)
    {
      Star.ExpansionSize = _rollingService.D6(1);
    }

    return star;
  }

  private int GetAge()
  {
    return _rollingService.D6(3) - 3;
  }

  private RttWorldgenStar FinishFTypeGeneration(RttWorldgenStar star)
  {
    if (Star.Age <= 2)
    {
      Star.Luminosity = Luminosity.V;
    }
    else
    {
      var roll = _rollingService.D6(1);
      switch (roll)
      {
        case <= 2:
          Star.SpectralType = SpectralType.G;
          Star.Luminosity = Luminosity.IV;
          break;
        case 3:
          Star.SpectralType = SpectralType.M;
          Star.Luminosity = Luminosity.III;
          break;
        case <= 6:
          Star.SpectralType = SpectralType.D;
          break;
      }
    }

    return star;
  }

  private RttWorldgenStar FinishGTypeGeneration(RttWorldgenStar star)
  {
    if (Star.Age <= 2)
    {
      Star.Luminosity = Luminosity.V;
    }
    else
    {
      var roll = _rollingService.D6(1);
      switch (roll)
      {
        case <= 2:
          Star.SpectralType = SpectralType.K;
          Star.Luminosity = Luminosity.IV;
          break;
        case 3:
          Star.SpectralType = SpectralType.M;
          Star.Luminosity = Luminosity.III;
          break;
        case <= 6:
          Star.SpectralType = SpectralType.D;
          break;
      }
    }

    return star;
  }

  private RttWorldgenStar FinishATypeGeneration(RttWorldgenStar star)
  {
    if (Star.Age <= 2)
    {
      Star.Luminosity = Luminosity.V;
    }
    else
    {
      var roll = _rollingService.D6(1);
      switch (roll)
      {
        case <= 2:
          Star.SpectralType = SpectralType.F;
          Star.Luminosity = Luminosity.IV;
          break;
        case 3:
          Star.SpectralType = SpectralType.K;
          Star.Luminosity = Luminosity.III;
          break;
        case <= 6:
          Star.SpectralType = SpectralType.D;
          break;
      }
    }

    return star;
  }

  private RttWorldgenStar FinishMTypeGeneration(RttWorldgenStar star, bool isCompanion)
  {
    switch (_rollingService.D6(2) + (isCompanion ? 2 : 0))
    {
      case <= 9:
        Star.Luminosity = Luminosity.V;
        break;
      case <= 12:
        Star.Luminosity = Luminosity.Ve;
        break;
      default:
        Star.SpectralType = SpectralType.L;
        break;
    }

    return star;
  }
  
  public CompanionOrbit GenerateOrbit()
  {
    return _rollingService.D6(1) switch {
      <= 2 => CompanionOrbit.Tight,
      <= 4 => CompanionOrbit.Close,
      5 => CompanionOrbit.Moderate,
      6 => CompanionOrbit.Distant,
      _ => CompanionOrbit.None
    };
  }
}