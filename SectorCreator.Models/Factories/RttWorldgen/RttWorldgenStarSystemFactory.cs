using SectorCreator.Global;
using SectorCreator.Global.Enums;
using SectorCreator.Models.RTTWorldgen;

namespace SectorCreator.Models.Factories.RttWorldgen;

public interface IRttWorldgenStarSystemFactory
{
    RttWorldgenStarSystem Generate(StarSystemType starSystemType);
    RttWorldgenStarSystem Generate(RttWorldgenStar star);
}

public class RttWorldgenStarSystemFactory : IRttWorldgenStarSystemFactory
{
    private readonly IRollingService _rollingService;
    private readonly IRttWorldgenStarFactory _rttWorldgenStarFactory;
    private readonly IRttWorldgenPlanetFactory _rttWorldgenPlanetFactory;
    private readonly RttWorldgenStarSystem _starSystem = new();

    public RttWorldgenStarSystemFactory(IRollingService rollingService,
        IRttWorldgenStarFactory rttWorldgenStarFactory,
        IRttWorldgenPlanetFactory rttWorldgenPlanetFactory)
    {
        _rollingService = rollingService;
        _rttWorldgenStarFactory = rttWorldgenStarFactory;
        _rttWorldgenPlanetFactory = rttWorldgenPlanetFactory;
    }

    public RttWorldgenStarSystem Generate(StarSystemType starSystemType)
    {
        if (starSystemType == StarSystemType.BrownDwarf) {
            AddBrownDwarfStarToSystem();
        } else {
            AddStarsToSystem();
        }

        AddPlanetsToSystem();

        return _starSystem;
    }

    public RttWorldgenStarSystem Generate(RttWorldgenStar star)
    {
        _starSystem.Stars.Add(star);
        AddPlanetsToSystem();

        return _starSystem;
    }

    private void AddBrownDwarfStarToSystem()
    {
        _starSystem.Stars.Add(_rttWorldgenStarFactory.GenerateBrownDwarf());
    }

    private void AddStarsToSystem()
    {
        var numStars = GetNumStars();

        var isPrimary = true;
        var primaryRoll = 0;
        for (var i = 0; i < numStars; i++) {
            _starSystem.Stars.Add(_rttWorldgenStarFactory.Generate(isPrimary, out int spectralRoll, primaryRoll));
            if (isPrimary) {
                primaryRoll = spectralRoll;
            }
            isPrimary = false;
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
        var primaryStar = (RttWorldgenStar) _starSystem.Stars.First(x => ((RttWorldgenStar) x).IsPrimary);

        if (primaryStar.SpectralType == SpectralType.M && primaryStar.Luminosity == Luminosity.V) {
            orbitNum--;
        }

        if (primaryStar.SpectralType is SpectralType.D or SpectralType.L
            || primaryStar.Luminosity == Luminosity.III) {
            return;
        }

        orbitNum = Math.Min(orbitNum, 2);

        for (var i = 0; i < orbitNum; i++) {
            _starSystem.Planets.Add(_rttWorldgenPlanetFactory.GenerateRttWorldgenPlanet(primaryStar, PlanetOrbit.Epistellar, i + 1));
        }
    }

    private void AddInnerPlanetsToSystem()
    {
        var primaryStar = (RttWorldgenStar) _starSystem.Stars.First(x => ((RttWorldgenStar) x).IsPrimary);

        var orbitNum = primaryStar.SpectralType == SpectralType.L
            ? _rollingService.D3(1) - 1
            : _rollingService.D6(1) - 1;

        if (primaryStar.SpectralType == SpectralType.M && primaryStar.Luminosity == Luminosity.V) {
            orbitNum--;
        }

        if (_starSystem.Stars.Exists(x => ((RttWorldgenStar) x).CompanionOrbit == CompanionOrbit.Close)) {
            orbitNum = 0;
        }

        for (var i = 0; i < orbitNum; i++) {
            _starSystem.Planets.Add(
                _rttWorldgenPlanetFactory.GenerateRttWorldgenPlanet(primaryStar, PlanetOrbit.Inner, _starSystem.Planets.Count + 1));
        }
    }

    private void AddOuterPlanetsToSystem()
    {
        var primaryStar =
            (RttWorldgenStar) _starSystem.Stars.First(x => ((RttWorldgenStar) x).IsPrimary);

        var orbitNum = _rollingService.D6(1) - 1;

        if (primaryStar.SpectralType is SpectralType.M or SpectralType.L && primaryStar.Luminosity == Luminosity.V) {
            orbitNum--;
        }

        if (_starSystem.Stars.Exists(x => ((RttWorldgenStar) x).CompanionOrbit == CompanionOrbit.Moderate)) {
            orbitNum = 0;
        }

        for (var i = 0; i < orbitNum; i++) {
            _starSystem.Planets.Add(_rttWorldgenPlanetFactory.GenerateRttWorldgenPlanet(primaryStar, PlanetOrbit.Inner,
                _starSystem.Planets.Count + 1));
        }
    }
}