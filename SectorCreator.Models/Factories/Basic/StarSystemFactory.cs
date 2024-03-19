using SectorCreator.Global;
using SectorCreator.Global.Enums;
using SectorCreator.Models.Basic;
using SectorCreator.Models.Factories.StarFrontiers;
using SectorCreator.Models.Factories.t5;

namespace SectorCreator.Models.Factories.Basic;

public interface IStarSystemFactory
{
    StarSystem GenerateMongooseStarSystem(SectorType sectorType, Coordinates coordinates);
    StarSystem GenerateStarFrontiersStarSystem(Coordinates coordinates);
}

public class StarSystemFactory : IStarSystemFactory
{
    private readonly IRollingService _rollingService;
    private readonly IPlanetFactory _planetFactory;
    private readonly IStarFrontiersStarFactory _starFrontiersStarFactory;
    private readonly IStarFrontiersPlanetFactory _starFrontiersPlanetFactory;

    public StarSystemFactory(IRollingService rollingService, IPlanetFactory planetFactory,
        IStarFrontiersStarFactory starFrontiersStarFactory, IStarFrontiersPlanetFactory starFrontiersPlanetFactory)
    {
        _rollingService = rollingService;
        _planetFactory = planetFactory;
        _starFrontiersStarFactory = starFrontiersStarFactory;
        _starFrontiersPlanetFactory = starFrontiersPlanetFactory;
    }

    public virtual StarSystem GenerateMongooseStarSystem(SectorType sectorType, Coordinates coordinates)
    {
        var starSystem = new StarSystem {
            Coordinates = coordinates
        };

        starSystem.Planets.Add(_planetFactory.Generate(sectorType, starSystem.Coordinates));

        if (_rollingService.D6(2) >= 4) {
            starSystem.GasGiant = true;
        }

        return starSystem;
    }

    public virtual StarSystem GenerateStarFrontiersStarSystem(Coordinates coordinates)
    {
        var starSystem = new StarSystem {
            Coordinates = coordinates
        };

        var numStars = _rollingService.D10(1) switch {
            (<= 7) => 1,
            _ => 2
        };

        for (var i = 0; i < numStars; i++) {
            starSystem.CompanionStars.Add(_starFrontiersStarFactory.Generate());
        }

        if (_rollingService.D6(1) >= 4) {
            starSystem.Planets.Add(_starFrontiersPlanetFactory.Generate(SectorType.StarFrontiers, starSystem.Coordinates));
        }

        return starSystem;
    }
}