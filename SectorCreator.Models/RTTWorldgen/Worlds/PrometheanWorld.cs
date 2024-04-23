using SectorCreator.Global;
using SectorCreator.Global.Enums;
using SectorCreator.Models.Services;

namespace SectorCreator.Models.RTTWorldgen.Worlds;

public interface IPrometheanWorld
{
    RttWorldgenPlanet Generate(RttWorldgenPlanet planet, RttWorldgenStar primaryStar);
}

public class PrometheanWorld : IPrometheanWorld
{
    private readonly IRollingService _rollingService;
    
    public PrometheanWorld(IRollingService rollingService)
    {
        _rollingService = rollingService;
    }

    public RttWorldgenPlanet Generate(RttWorldgenPlanet planet, RttWorldgenStar primaryStar)
    {
        planet.WorldType = WorldType.Promethean;
        planet.Size = _rollingService.D6(1) - 1;
        planet.Chemistry = GetChemistry(primaryStar, planet);
        planet.Biosphere = GetBiosphere(primaryStar, planet);
        planet.Atmosphere = GetAtmosphere(planet);
        planet.Hydrographics = _rollingService.D6(2) - 2;
        planet = WorldValidation.ValidatePlanet(planet);
        return planet;
    }

    private PlanetChemistry GetChemistry(RttWorldgenStar primaryStar, RttWorldgenPlanet planet)
    {
        var roll = _rollingService.D6(1);
        if (primaryStar.SpectralType == SpectralType.BD) {
            roll += 2;
        }

        if (planet.PlanetOrbit == PlanetOrbit.Epistellar) {
            roll -= 2;
        } else if (planet.PlanetOrbit == PlanetOrbit.Outer) {
            roll += 2;
        }

        return roll switch {
            <= 4 => PlanetChemistry.Water,
            <= 6 => PlanetChemistry.Ammonia,
            _ => PlanetChemistry.Methane
        };
    }

    private int GetBiosphere(RttWorldgenStar primaryStar, RttWorldgenPlanet planet)
    {
        if (primaryStar.Age >= 4 + ChemistryService.GetAgeMod(planet.Chemistry)) {
            var mod = 0;
            if (primaryStar.SpectralType == SpectralType.BD) {
                mod = -3;
            }

            return _rollingService.D6(2) + mod;
        }

        if (primaryStar.Age >= _rollingService.D3(1) + ChemistryService.GetAgeMod(planet.Chemistry)) {
            return _rollingService.D3(1);
        }

        return 0;
    }

    private int GetAtmosphere(RttWorldgenPlanet planet)
    {
        if (planet.Biosphere >= 3 && planet.Chemistry == PlanetChemistry.Water) {
            var atmosphere = _rollingService.D6(2) + planet.Size - 7;
            atmosphere = Math.Min(atmosphere, 9);
            return Math.Max(atmosphere, 2);
        }

        return 10;
    }
}