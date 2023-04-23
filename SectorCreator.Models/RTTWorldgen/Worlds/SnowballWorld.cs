using SectorCreator.Global;
using SectorCreator.Global.Enums;
using SectorCreator.Models.Services;

namespace SectorCreator.Models.RTTWorldgen.Worlds;

public interface ISnowballWorld
{
    RttWorldgenPlanet Generate(RttWorldgenPlanet planet, RttWorldgenStar primaryStar);
}

public class SnowballWorld : ISnowballWorld
{
    private readonly IRollingService _rollingService;
    public SnowballWorld(IRollingService rollingService)
    {
        _rollingService = rollingService;
    }
    
    public RttWorldgenPlanet Generate(RttWorldgenPlanet planet, RttWorldgenStar primaryStar)
    {
        var hydrosphereRoll = _rollingService.D6(1);
        
        planet.WorldType = WorldType.Snowball;
        planet.Size = _rollingService.D6(1) - 1;
        planet.Atmosphere = GetAtmosphere();
        planet.Hydrographics = GetHydrographics(hydrosphereRoll);
        planet.Chemistry = GetChemistry(primaryStar, planet);
        planet.Biosphere = GetBiosphere(primaryStar, planet, hydrosphereRoll);
        planet = WorldValidation.ValidatePlanet(planet);
        return planet;
    }

    private int GetAtmosphere()
    {
        return _rollingService.D6(1) switch {
            (<= 4) => 0,
            (>= 5) => 1
        };
    }

    private int GetHydrographics(int hydrosphereRoll)
    {
        return hydrosphereRoll switch {
            (<= 3) => 10,
            (>= 4) => _rollingService.D6(2) - 2
        };
    }

    private PlanetChemistry GetChemistry(RttWorldgenStar primaryStar, RttWorldgenPlanet planet)
    {
        var roll = _rollingService.D6(1);
        if (primaryStar.SpectralType == SpectralType.L) {
            roll += 2;
        }

        if (planet.PlanetOrbit == PlanetOrbit.Outer) {
            roll += 2;
        }

        return roll switch {
            (<= 4) => PlanetChemistry.Water,
            (<= 6) => PlanetChemistry.Ammonia,
            (>= 7) => PlanetChemistry.Methane
        };
    }

    private int GetBiosphere(RttWorldgenStar primaryStar, RttWorldgenPlanet planet, int hydrosphereRoll)
    {
        if (hydrosphereRoll >= 4) {
            if (primaryStar.Age >= 6 + ChemistryService.GetAgeMod(planet.Chemistry)) {
                return _rollingService.D6(1) + planet.Size - 2;
            }

            if (primaryStar.Age >= _rollingService.D6(1)) {
                return _rollingService.D6(1) - 3;
            }

            return 0;
        }

        return 0;
    }
}