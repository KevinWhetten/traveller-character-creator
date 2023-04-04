using TravellerCreatorGlobalMethods;
using TravellerCreatorModels.Enums;
using TravellerCreatorModels.Interfaces;
using TravellerCreatorModels.Mongoose;
using TravellerCreatorModels.RTTWorldgen;
using TravellerCreatorModels.StarFrontiers;

namespace TravellerCharacterCreatorBL;

public class StarSystemGenerator
{
    private readonly PlanetGenerator _planetGenerator;
    private readonly StarGenerator _starGenerator;

    public StarSystemGenerator()
    {
        _planetGenerator = new PlanetGenerator();
        _starGenerator = new StarGenerator();
    }

    public IStarSystem GenerateMongooseStarSystem(SectorType sectorType)
    {
        var starSystem = new MongooseStarSystem {
            Type = StarSystemType.Basic,
            Planets = new List<IPlanet>()
        };
        var planet = Roll.D6(1) >= 4
            ? (MongoosePlanet) _planetGenerator.GenerateMongoosePlanet(sectorType)
            : null;

        if (planet != null) {
            starSystem.Planets.Add(planet);
        }

        if (sectorType is SectorType.SpaceOpera or SectorType.HardScience
            && starSystem.Planets.Count > 0
            && starSystem.Planets[0] is {Size: <= 4}) {
            int size = starSystem.Planets[0].Size;
            int atmosphere = starSystem.Planets[0].Atmosphere;

            if (size is >= 0 and <= 2) {
                starSystem.Planets[0].Atmosphere = 0;
            } else {
                starSystem.Planets[0].Atmosphere = atmosphere switch {
                    (<= 2) => 0,
                    (>= 3 and <= 5) => 1,
                    (_) => 10
                };
            }
        }

        starSystem.GetGasGiant();

        return starSystem;
    }

    public IStarSystem GenerateStarFrontiersStarSystem()
    {
        var starSystem = new StarFrontiersStarSystem();
        int numStars = Roll.D10(1) <= 7 ? 1 : 2;

        for (var i = 0; i < numStars; i++) {
            starSystem.Stars.Add(_starGenerator.GenerateStarFrontiersStar());
        }

        int numPlanets = starSystem.Stars.First().SpectralType switch {
            SpectralType.M => Roll.D10(1),
            SpectralType.K => Roll.D(15, 1),
            SpectralType.G => Roll.D(15, 1),
            SpectralType.F => Roll.D10(1),
            SpectralType.A => Roll.D(5, 1),
            _ => 0
        };
        int habitableBase = numPlanets / 3;
        var habitablePlanets = new List<int> {habitableBase};

        if (Roll.D10(1) <= 5) {
            habitablePlanets.Add(habitableBase - 1);
        }

        if (Roll.D10(1) <= 5) {
            habitablePlanets.Add(habitableBase + 1);
        }

        for (var i = 0; i < numPlanets; i++) {
            starSystem.Planets.Add(
                _planetGenerator.GenerateStarFrontiersPlanet(habitablePlanets.Contains(i), habitableBase, i));
        }

        return starSystem;
    }

    public IStarSystem GenerateRTTWorldgenStarSystem(RTTWorldgenStarSystemType starSystemType)
    {
        var starSystem = new RTTWorldgenStarSystem {
            Stars = _starGenerator.GenerateRTTWorldgenStarSystemStars(starSystemType)
        };

        starSystem.Planets = _planetGenerator.GenerateRTTWorldgenPlanets(starSystem.Stars);

        return starSystem;
    }

    public IStarSystem GenerateRTTWorldgenStarSystem(RTTWorldgenStar star)
    {
        var starSystem = new RTTWorldgenStarSystem {
            Stars = new List<IStar> {star}
        };

        starSystem.Planets = _planetGenerator.GenerateRTTWorldgenPlanets(starSystem.Stars);


        return starSystem;
    }
}