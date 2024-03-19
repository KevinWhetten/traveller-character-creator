using SectorCreator.Global;
using SectorCreator.Global.Enums;
using SectorCreator.Models.RTTWorldgen;

namespace SectorCreator.Models.Factories.RttWorldgen;

public interface IRttWorldgenStarSystemFactory
{
    RttWorldgenStarSystem Generate(StarSystemType starSystemType, Coordinates coordinates);
    RttWorldgenStarSystem Generate(RttWorldgenStar star, Coordinates coordinates);
}

public class RttWorldgenStarSystemFactory : IRttWorldgenStarSystemFactory
{
    private readonly IRollingService _rollingService;
    private readonly IRttWorldgenStarFactory _rttWorldgenStarFactory;
    private readonly IRttWorldgenPlanetFactory _rttWorldgenPlanetFactory;
    private RttWorldgenStarSystem _starSystem = new();

    public RttWorldgenStarSystemFactory(IRollingService rollingService,
        IRttWorldgenStarFactory rttWorldgenStarFactory,
        IRttWorldgenPlanetFactory rttWorldgenPlanetFactory)
    {
        _rollingService = rollingService;
        _rttWorldgenStarFactory = rttWorldgenStarFactory;
        _rttWorldgenPlanetFactory = rttWorldgenPlanetFactory;
    }

    public RttWorldgenStarSystem Generate(StarSystemType starSystemType, Coordinates coordinates)
    {
        _starSystem = new RttWorldgenStarSystem {
            Coordinates = coordinates
        };

        if (starSystemType == StarSystemType.BrownDwarf) {
            AddBrownDwarfStarToSystem();
        } else {
            AddStarsToSystem();
        }

        AddPlanetsToSystem();

        return _starSystem;
    }

    public RttWorldgenStarSystem Generate(RttWorldgenStar star, Coordinates coordinates)
    {
        _starSystem = new RttWorldgenStarSystem {
            PrimaryStar =  star,
            Coordinates = coordinates
        };
        AddPlanetsToSystem();

        return _starSystem;
    }

    private void AddBrownDwarfStarToSystem()
    {
        _starSystem.PrimaryStar = _rttWorldgenStarFactory.GenerateBrownDwarf();
    }

    private void AddStarsToSystem()
    {
        var numStars = GetNumStars();

        var primaryRoll = 0;
        for (var i = 0; i < numStars; i++) {
            if (i == 0) {
                _starSystem.PrimaryStar = _rttWorldgenStarFactory.Generate(StarType.Primary, out int spectralRoll);
                primaryRoll = spectralRoll;
            } else {
                _starSystem.CompanionStars.Add(new RttWorldgenStar(_rttWorldgenStarFactory.Generate(StarType.Companion, primaryRoll)));
            }
        }
    }

    private int GetNumStars()
    {
        return _rollingService.D6(3)switch {
            (<= 10) => 1,
            (<= 15) => 2,
            (>= 16) => 3
        };
    }

    private void AddPlanetsToSystem()
    {
        AddEpistellarPlanetsToSystem();
        AddInnerPlanetsToSystem();
        AddOuterPlanetsToSystem();
    }

    private void AddEpistellarPlanetsToSystem()
    {
        var orbitNum = _rollingService.D6(1) - 3;
        var primaryStar = (RttWorldgenStar) _starSystem.PrimaryStar;

        if (primaryStar is {SpectralType: SpectralType.M, Luminosity: Luminosity.V}) {
            orbitNum--;
        }

        if (primaryStar.SpectralType is SpectralType.D or SpectralType.L
            || primaryStar.Luminosity == Luminosity.III) {
            return;
        }

        orbitNum = Math.Min(orbitNum, 2);

        for (var i = 0; i < orbitNum; i++) {
            _starSystem.Planets.Add(_rttWorldgenPlanetFactory.GenerateRttWorldgenPlanet(primaryStar, PlanetOrbit.Epistellar, i + 1, _starSystem.Coordinates));
        }
    }


    private void AddInnerPlanetsToSystem()
    {
        var primaryStar = (RttWorldgenStar) _starSystem.PrimaryStar;

        var orbitNum = primaryStar.SpectralType == SpectralType.L
            ? _rollingService.D3(1) - 1
            : _rollingService.D6(1) - 1;

        if (primaryStar.SpectralType == SpectralType.M && primaryStar.Luminosity == Luminosity.V) {
            orbitNum--;
        }

        if (_starSystem.CompanionStars.Exists(x => ((RttWorldgenStar) x).CompanionOrbit == CompanionOrbit.Close)) {
            orbitNum = 0;
        }

        for (var i = 0; i < orbitNum; i++) {
            _starSystem.Planets.Add(new RttWorldgenPlanet(_rttWorldgenPlanetFactory.GenerateRttWorldgenPlanet(primaryStar, PlanetOrbit.Inner, _starSystem.Planets.Count + 1, _starSystem.Coordinates)));
        }
    }

    private void AddOuterPlanetsToSystem()
    {
        var primaryStar = (RttWorldgenStar) _starSystem.PrimaryStar;

        var orbitNum = _rollingService.D6(1) - 1;

        if (primaryStar.SpectralType is SpectralType.M or SpectralType.L && primaryStar.Luminosity == Luminosity.V) {
            orbitNum--;
        }

        if (_starSystem.CompanionStars.Exists(x => ((RttWorldgenStar) x).CompanionOrbit == CompanionOrbit.Moderate)) {
            orbitNum = 0;
        }

        for (var i = 0; i < orbitNum; i++) {
            _starSystem.Planets.Add(new RttWorldgenPlanet(_rttWorldgenPlanetFactory.GenerateRttWorldgenPlanet(primaryStar, PlanetOrbit.Outer, _starSystem.Planets.Count + 1, _starSystem.Coordinates)));
        }
    }
}