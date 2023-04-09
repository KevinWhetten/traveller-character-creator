using SectorCreator.Global;
using SectorCreator.Global.Enums;
using SectorCreator.Models.Basic;

namespace SectorCreator.Models.RTTWorldgen;

public class RttWorldgenStarSystem : StarSystem
{
  public void Generate(RttWorldgenStarSystemType starSystemType)
  {
    GenerateStars(starSystemType);
    GeneratePlanets();
  }

  public void Generate(RttWorldgenStar star)
  {
    Stars.Add(star);
    GeneratePlanets();
  }

  #region GenerateStars

  private void GenerateStars(RttWorldgenStarSystemType starSystemType)
  {
    if (starSystemType == RttWorldgenStarSystemType.BrownDwarf)
    {
      Stars.Add(new RttWorldgenStar()
      {
        SpectralType = SpectralType.D,
        Luminosity = Luminosity.I,
        SpectralSubclass = Roll.D10(1) - 1
      });
      return;
    }

    var numStars = GetNumStars(starSystemType);

    var isPrimaryStar = true;
    for (var i = 0; i < numStars; i++)
    {
      var rttWorldgenStarType = isPrimaryStar ? RttWorldgenStarType.Companion : RttWorldgenStarType.Primary;
      var star = new RttWorldgenStar();
      if (isPrimaryStar)
      {
        var primaryStar = (RttWorldgenStar) Stars.First(x => ((RttWorldgenStar) x).RttWorldgenStarType == RttWorldgenStarType.Primary);
        star.Generate(rttWorldgenStarType, primaryStar);
      }
      else
      {
        star.Generate(rttWorldgenStarType);
      }

      Stars.Add(star);

      isPrimaryStar = false;
    }

    foreach (var star in Stars.Cast<RttWorldgenStar>())
    {
      star.SpectralSubclass = Roll.D10(1) - 1;

      if (star.RttWorldgenStarType == RttWorldgenStarType.Companion)
      {
        star.GenerateOrbit();
      }
    }
  }

  private int GetNumStars(RttWorldgenStarSystemType starSystemType)
  {
    if (starSystemType == RttWorldgenStarSystemType.BrownDwarf)
    {
      return 1;
    }

    var roll = Roll.D6(3);
    return roll switch
    {
      >= 11 and <= 15 => 2,
      >= 16 => 3,
      _ => 1
    };
  }

  #endregion

  #region GeneratePlanets

  private void GeneratePlanets()
  {
    GenerateEpistellarPlanets();
    GenerateInnerPlanets(Planets.Count + 1);
    GenerateOuterPlanets(Planets.Count + 1);
  }

  private void GenerateEpistellarPlanets()
  {
    var roll = Roll.D6(1) - 3;
    var primaryStar = (Stars.First(x => ((RttWorldgenStar) x).RttWorldgenStarType == RttWorldgenStarType.Primary) as RttWorldgenStar)!;

    if (primaryStar is { SpectralType: SpectralType.M, Luminosity: Luminosity.V }) {
      roll--;
    } else if (primaryStar.Luminosity == Luminosity.III
               || primaryStar.SpectralType is SpectralType.D or SpectralType.L) {
      roll = 0;
    }

    roll = Math.Min(roll, 2);

    for (var i = 0; i < roll; i++)
    {
      var planet = RttWorldgenPlanetFactory.GenerateRttWorldgenPlanet(primaryStar, PlanetOrbit.Epistellar, i + 1);
      Planets.Add(planet);
    }
  }

  private void GenerateInnerPlanets(int orbitNum)
  {
    var numPlanets = Roll.D6(1) - 1;
    var primaryStar = Stars.First(x => ((RttWorldgenStar)x).RttWorldgenStarType == RttWorldgenStarType.Primary) as RttWorldgenStar;
    
    if (primaryStar!.SpectralType == SpectralType.M && primaryStar.Luminosity == Luminosity.V) {
      numPlanets--;
    } else if (Stars.Exists(x => ((RttWorldgenStar)x).CompanionOrbit == CompanionOrbit.Close)) {
      numPlanets = 0;
    } else if (primaryStar.SpectralType == SpectralType.L) {
      numPlanets = Roll.D3(1) - 1;
    }

    for (var i = 0; i < numPlanets; i++)
    {
      var planet = RttWorldgenPlanetFactory.GenerateRttWorldgenPlanet(primaryStar, PlanetOrbit.Inner, orbitNum + i);
      Planets.Add(planet);
    }
  }

  private void GenerateOuterPlanets(int orbitNum)
  {
    var numPlanets = Roll.D6(1) - 1;
    var primaryStar = Stars.First(x => ((RttWorldgenStar)x).RttWorldgenStarType == RttWorldgenStarType.Primary) as RttWorldgenStar;

    if ((primaryStar!.SpectralType == SpectralType.M && primaryStar.Luminosity == Luminosity.V)
        || primaryStar.SpectralType == SpectralType.L) {
      numPlanets--;
    } else if (Stars.Exists(x => ((RttWorldgenStar)x).CompanionOrbit == CompanionOrbit.Moderate)) {
      numPlanets = 0;
    }

    for (var i = 0; i < numPlanets; i++)
    {
      var planet = RttWorldgenPlanetFactory.GenerateRttWorldgenPlanet(primaryStar, PlanetOrbit.Outer, orbitNum + i);
      Planets.Add(planet);
    }
  }

  #endregion
}
