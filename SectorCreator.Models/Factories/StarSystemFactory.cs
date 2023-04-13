using SectorCreator.Global;
using SectorCreator.Global.Enums;
using SectorCreator.Models.Basic;
using SectorCreator.Models.RTTWorldgen;
using SectorCreator.Models.RTTWorldgen.Worlds;

namespace SectorCreator.Models.Factories;

public interface IStarSystemFactory
{
    StarSystem GenerateMongooseStarSystem(SectorType sectorType);
    StarSystem GenerateT5StarSystem();
    StarSystem GenerateRttWorldgenStarSystem(StarSystemType starSystemType);
    StarSystem GenerateRttWorldgenStarSystem(RttWorldgenStar star);
    StarSystem GenerateStarFrontiersStarSystem();
}

public class StarSystemFactory : IStarSystemFactory
{
    private readonly IRollingService _rollingService;
    private readonly IPlanetFactory _planetFactory;
    private readonly IStarFrontiersStarFactory _starFrontiersStarFactory;
    private readonly IRttWorldgenStarFactory _rttWorldgenStarFactory;
    private readonly IStarFrontiersPlanetFactory _starFrontiersPlanetFactory;
    private readonly IRttWorldgenPlanetFactory _rttWorldgenPlanetFactory;

    public StarSystemFactory()
    {
        _starFrontiersStarFactory = new StarFrontiersStarFactory(new RollingService());
        _rttWorldgenStarFactory = new RttWorldgenStarFactory(new RollingService());
        _starFrontiersPlanetFactory = new StarFrontiersPlanetFactory(new RollingService());
        _rollingService = new RollingService();
        _planetFactory = new PlanetFactory(new RollingService());
        _rttWorldgenPlanetFactory = new RttWorldgenPlanetFactory(
            new RollingService(),
            new AcheronianWorld(new RollingService(), new PlanetValidation()),
            new AreanWorld(new RollingService(), new PlanetValidation()),
            new AridWorld(new RollingService(), new PlanetValidation()),
            new AsphodelianWorld(new RollingService(), new PlanetValidation()),
            new ChthonianPlanet(new PlanetValidation()),
            new HebeanPlanet(new RollingService(), new PlanetValidation()),
            new HelianPlanet(new RollingService(), new PlanetValidation()),
            new JaniLithicPlanet(new RollingService(), new PlanetValidation()),
            new JovianPlanet(new RollingService(), new PlanetValidation()),
            new MeltballPlanet(new RollingService(), new PlanetValidation()),
            new OceanicPlanet(new RollingService(), new PlanetValidation()),
            new PanthalassicPlanet(new RollingService(), new PlanetValidation()),
            new PromethianPlanet(new RollingService(), new PlanetValidation()),
            new RockballPlanet(new RollingService(), new PlanetValidation()),
            new SnowballPlanet(new RollingService(), new PlanetValidation()),
            new StygianPlanet(new RollingService(), new PlanetValidation()),
            new TectonicPlanet(new RollingService(), new PlanetValidation()),
            new TelluricPlanet(new RollingService(), new PlanetValidation()),
            new VesperianPlanet(new RollingService(), new PlanetValidation()));
    }

    public StarSystemFactory(IRollingService rollingService, IPlanetFactory planetFactory,
        IStarFrontiersStarFactory starFrontiersStarFactory, IRttWorldgenStarFactory rttWorldgenStarFactory,
        StarFrontiersPlanetFactory starFrontiersPlanetFactory, RttWorldgenPlanetFactory rttWorldgenPlanetFactory)
    {
        _starFrontiersPlanetFactory = starFrontiersPlanetFactory;
        _starFrontiersStarFactory = starFrontiersStarFactory;
        _rttWorldgenStarFactory = rttWorldgenStarFactory;
        _rollingService = rollingService;
        _planetFactory = planetFactory;
        _rttWorldgenPlanetFactory = rttWorldgenPlanetFactory;
    }

    public virtual StarSystem GenerateMongooseStarSystem(SectorType sectorType)
    {
        var starSystem = new StarSystem();

        if (_rollingService.D6(1) >= 4) {
            starSystem.Planets.Add(_planetFactory.Generate(sectorType));
        }

        if (_rollingService.D6(2) > 5) {
            starSystem.GasGiant = true;
        } else if (_rollingService.D6(2) < 5) {
            starSystem.GasGiant = true;
        }

        return starSystem;
    }

    public virtual StarSystem GenerateT5StarSystem()
    {
        throw new NotImplementedException();
    }

    public virtual StarSystem GenerateRttWorldgenStarSystem(StarSystemType starSystemType)
    {
        var starSystem = new StarSystem();

        if (starSystemType == StarSystemType.BrownDwarf) {
            starSystem.Stars.Add(new RttWorldgenStar() {
                SpectralType = SpectralType.D,
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
                   || primaryStar1.SpectralType is SpectralType.D or SpectralType.L) {
            roll = 0;
        }

        roll = Math.Min(roll, 2);

        for (var i1 = 0; i1 < roll; i1++) {
            var planet =
                _rttWorldgenPlanetFactory.GenerateRttWorldgenPlanet(primaryStar1, PlanetOrbit.Epistellar, i1 + 1);
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
            var planet1 =
                _rttWorldgenPlanetFactory.GenerateRttWorldgenPlanet(primaryStar2, PlanetOrbit.Inner, orbitNum + i2);
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
            var planet2 =
                _rttWorldgenPlanetFactory.GenerateRttWorldgenPlanet(primaryStar3, PlanetOrbit.Outer, orbitNum1 + i3);
            starSystem.Planets.Add(planet2);
        }

        return starSystem;
    }

    public virtual StarSystem GenerateRttWorldgenStarSystem(RttWorldgenStar star)
    {
        var starSystem = new StarSystem();

        starSystem.Stars.Add(star);
        var roll = _rollingService.D6(1) - 3;
        var primaryStar =
            (starSystem.Stars.First(x => ((RttWorldgenStar) x).StarType == StarType.Primary) as RttWorldgenStar)!;

        if (primaryStar is {SpectralType: SpectralType.M, Luminosity: Luminosity.V}) {
            roll--;
        } else if (primaryStar.Luminosity == Luminosity.III
                   || primaryStar.SpectralType is SpectralType.D or SpectralType.L) {
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

    public virtual StarSystem GenerateStarFrontiersStarSystem()
    {
        var starSystem = new StarSystem();

        var numStars = _rollingService.D10(1) switch {
            (<= 7) => 1,
            _ => 2
        };

        for (var i = 0; i < numStars; i++) {
            starSystem.Stars.Add(_starFrontiersStarFactory.Generate());
        }

        if (_rollingService.D6(1) >= 4) {
            starSystem.Planets.Add(_starFrontiersPlanetFactory.Generate(SectorType.StarFrontiers));
        }

        return starSystem;
    }
}