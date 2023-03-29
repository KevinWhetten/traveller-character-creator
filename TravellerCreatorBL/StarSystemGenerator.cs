using TravellerCreatorGlobalMethods;
using TravellerCreatorModels.Basic;
using TravellerCreatorModels.Enums;
using TravellerCreatorModels.Interfaces;
using TravellerCreatorModels.StarFrontiers;

namespace TravellerCharacterCreatorBL;

public class StarSystemGenerator
{
    private readonly PlanetGenerator _planetGenerator;
    private readonly StarGenerator _starGenerator;

    public StarSystemGenerator()
    {
        _planetGenerator = new PlanetGenerator(new TradeCodeService());
        _starGenerator = new StarGenerator();
    }

    public IStarSystem GenerateBasicStarSystem(SectorType sectorType)
    {
        var starSystem = new BasicStarSystem {
            Type = StarSystemType.Basic,
            Planet = Roll.D6(1) >= 4
                ? (BasicPlanet) _planetGenerator.GeneratePlanet(sectorType)
                : null
        };

        if (sectorType is SectorType.SpaceOpera or SectorType.HardScience
            && starSystem.Planet is {Size: <= 4}) {
            int size = starSystem.Planet.Size;
            int atmosphere = starSystem.Planet.Atmosphere;

            if (size is >= 0 and <= 2) {
                starSystem.Planet.Atmosphere = 0;
            } else {
                starSystem.Planet.Atmosphere = atmosphere switch {
                    (<= 2) => 0,
                    (>= 3 and <= 5) => 1,
                    (_) => 10
                };
            }
        }

        starSystem.GetGasGiant();

        return starSystem;
    }

    public IStarSystem GenerateStarFrontiersSystem()
    {
        var starSystem = new StarFrontiersStarSystem();
        int numStars = Roll.D10(1) <= 7 ? 1 : 2;

        for (var i = 0; i < numStars; i++) {
            starSystem.Stars.Add(_starGenerator.GenerateStarFrontiersStar());
        }

        int numPlanets = starSystem.Stars.First().Luminosity switch {
            Luminosity.M => Roll.D10(1),
            Luminosity.K => Roll.D(15, 1),
            Luminosity.G => Roll.D(15, 1),
            Luminosity.F => Roll.D10(1),
            Luminosity.A => Roll.D(5, 1),
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
            starSystem.Planets.Add(_planetGenerator.GenerateStarFrontiersPlanet(habitablePlanets.Contains(i), habitableBase, i));
        }

        return starSystem;
    }
}