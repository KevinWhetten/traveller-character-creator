using SectorCreator.Global;
using SectorCreator.Global.Enums;
using SectorCreator.Models.Basic;
using SectorCreator.Models.Factories.StarFactories;
using SectorCreator.Models.RTTWorldgen;

namespace SectorCreator.Models.Factories.StarSystemFactories;

public interface IRttWorldgenStarSystemFactory
{
    StarSystem Generate(StarSystemType starSystemType);
    StarSystem Generate(RttWorldgenStar star);
}

public class RttWorldgenStarSystemFactory : IRttWorldgenStarSystemFactory
{
    private readonly IRollingService _rollingService;
    private readonly IRttWorldgenStarFactory _rttWorldgenStarFactory;
    private readonly IRttWorldgenPlanetFactory _rttWorldgenPlanetFactory;

    public RttWorldgenStarSystemFactory(IRollingService rollingService, IRttWorldgenPlanetFactory rttWorldgenPlanetFactory, IRttWorldgenStarFactory rttWorldgenStarFactory)
    {
        _rollingService = rollingService;
        _rttWorldgenPlanetFactory = rttWorldgenPlanetFactory;
        _rttWorldgenStarFactory = rttWorldgenStarFactory;
    }

    public virtual StarSystem Generate(StarSystemType starSystemType)
    {
        var starSystem = new StarSystem();

        if (starSystemType == StarSystemType.BrownDwarf) {
            starSystem.Stars.Add(new RttWorldgenStar() {
                SpectralType = SpectralType.L,
                Luminosity = Luminosity.I,
                SpectralSubclass = _rollingService.D10(1) - 1
            });
        } else {
            var roll1 = _rollingService.D6(3);
            var numPlanets = roll1 switch {
                >= 11 and <= 15 => 2,
                >= 16 => 3,
                _ => 1
            };


            var isPrimaryStar = true;
            for (var i = 0; i < numPlanets; i++) {
                var rttWorldgenStarType = isPrimaryStar ? StarType.Companion : StarType.Primary;
                var star = _rttWorldgenStarFactory.Generate(rttWorldgenStarType);
                if (isPrimaryStar) {
                    var primaryStar =
                        (RttWorldgenStar) starSystem.Stars.First(
                            x => ((RttWorldgenStar) x).StarType == StarType.Primary);
                    _rttWorldgenStarFactory.Generate(rttWorldgenStarType, primaryStar);
                } else {
                    _rttWorldgenStarFactory.Generate(rttWorldgenStarType);
                }

                starSystem.Stars.Add(star);

                isPrimaryStar = false;
            }

            foreach (var star in starSystem.Stars.Cast<RttWorldgenStar>()) {
                star.SpectralSubclass = _rollingService.D10(1) - 1;

                if (star.StarType == StarType.Companion) {
                    _rttWorldgenStarFactory.GenerateOrbit();
                }
            }
        }

        var roll = _rollingService.D6(1) - 3;
        var primaryStar1 =
            (starSystem.Stars.First(x => ((RttWorldgenStar) x).StarType == StarType.Primary) as RttWorldgenStar)!;

        if (primaryStar1 is {SpectralType: SpectralType.M, Luminosity: Luminosity.V}) {
            roll--;
        } else if (primaryStar1.Luminosity == Luminosity.III
                   || primaryStar1.SpectralType == SpectralType.L) {
            roll = 0;
        }

        roll = Math.Min(roll, 2);

        for (var i1 = 0; i1 < roll; i1++) {
            var planet = _rttWorldgenPlanetFactory.GenerateRttWorldgenPlanet(primaryStar1, PlanetOrbit.Epistellar, i1 + 1);
            starSystem.Planets.Add(planet);
        }

        int orbitNum = starSystem.Planets.Count + 1;
        var numPlanets2 = _rollingService.D6(1) - 1;
        var primaryStar2 =
            starSystem.Stars.First(x => ((RttWorldgenStar) x).StarType == StarType.Primary) as RttWorldgenStar;

        if (primaryStar2!.SpectralType == SpectralType.M && primaryStar2.Luminosity == Luminosity.V) {
            numPlanets2--;
        } else if (starSystem.Stars.Exists(x => ((RttWorldgenStar) x).CompanionOrbit == CompanionOrbit.Close)) {
            numPlanets2 = 0;
        } else if (primaryStar2.SpectralType == SpectralType.L) {
            numPlanets2 = _rollingService.D3(1) - 1;
        }

        for (var i2 = 0; i2 < numPlanets2; i2++) {
            var planet1 = _rttWorldgenPlanetFactory.GenerateRttWorldgenPlanet(primaryStar2, PlanetOrbit.Inner, orbitNum + i2);
            starSystem.Planets.Add(planet1);
        }

        int orbitNum1 = starSystem.Planets.Count + 1;
        var numPlanets1 = _rollingService.D6(1) - 1;
        var primaryStar3 =
            starSystem.Stars.First(x => ((RttWorldgenStar) x).StarType == StarType.Primary) as RttWorldgenStar;

        if ((primaryStar3!.SpectralType == SpectralType.M && primaryStar3.Luminosity == Luminosity.V)
            || primaryStar3.SpectralType == SpectralType.L) {
            numPlanets1--;
        } else if (starSystem.Stars.Exists(x => ((RttWorldgenStar) x).CompanionOrbit == CompanionOrbit.Moderate)) {
            numPlanets1 = 0;
        }

        for (var i3 = 0; i3 < numPlanets1; i3++) {
            var planet2 = _rttWorldgenPlanetFactory.GenerateRttWorldgenPlanet(primaryStar3, PlanetOrbit.Outer, orbitNum1 + i3);
            starSystem.Planets.Add(planet2);
        }

        return starSystem;
    }public virtual StarSystem Generate(RttWorldgenStar star)
    {
        var starSystem = new StarSystem();

        starSystem.Stars.Add(star);
        var roll = _rollingService.D6(1) - 3;
        var primaryStar =
            (starSystem.Stars.First(x => ((RttWorldgenStar) x).StarType == StarType.Primary) as RttWorldgenStar)!;

        if (primaryStar is {SpectralType: SpectralType.M, Luminosity: Luminosity.V}) {
            roll--;
        } else if (primaryStar.Luminosity == Luminosity.III
                   || primaryStar.SpectralType is SpectralType.L or SpectralType.L) {
            roll = 0;
        }

        roll = Math.Min(roll, 2);

        for (var i = 0; i < roll; i++) {
            var planet =
                _rttWorldgenPlanetFactory.GenerateRttWorldgenPlanet(primaryStar, PlanetOrbit.Epistellar, i + 1);
            starSystem.Planets.Add(planet);
        }

        int orbitNum = starSystem.Planets.Count + 1;
        var numPlanets = _rollingService.D6(1) - 1;
        var primaryStar1 =
            starSystem.Stars.First(x => ((RttWorldgenStar) x).StarType == StarType.Primary) as RttWorldgenStar;

        if (primaryStar1!.SpectralType == SpectralType.M && primaryStar1.Luminosity == Luminosity.V) {
            numPlanets--;
        } else if (starSystem.Stars.Exists(x => ((RttWorldgenStar) x).CompanionOrbit == CompanionOrbit.Close)) {
            numPlanets = 0;
        } else if (primaryStar1.SpectralType == SpectralType.L) {
            numPlanets = _rollingService.D3(1) - 1;
        }

        for (var i1 = 0; i1 < numPlanets; i1++) {
            var planet1 =
                _rttWorldgenPlanetFactory.GenerateRttWorldgenPlanet(primaryStar1, PlanetOrbit.Inner, orbitNum + i1);
            starSystem.Planets.Add(planet1);
        }

        int orbitNum1 = starSystem.Planets.Count + 1;
        var numPlanets1 = _rollingService.D6(1) - 1;
        var primaryStar2 =
            starSystem.Stars.First(x => ((RttWorldgenStar) x).StarType == StarType.Primary) as RttWorldgenStar;

        if ((primaryStar2!.SpectralType == SpectralType.M && primaryStar2.Luminosity == Luminosity.V)
            || primaryStar2.SpectralType == SpectralType.L) {
            numPlanets1--;
        } else if (starSystem.Stars.Exists(x => ((RttWorldgenStar) x).CompanionOrbit == CompanionOrbit.Moderate)) {
            numPlanets1 = 0;
        }

        for (var i2 = 0; i2 < numPlanets1; i2++) {
            var planet2 =
                _rttWorldgenPlanetFactory.GenerateRttWorldgenPlanet(primaryStar2, PlanetOrbit.Outer, orbitNum1 + i2);
            starSystem.Planets.Add(planet2);
        }

        return starSystem;
    }
}