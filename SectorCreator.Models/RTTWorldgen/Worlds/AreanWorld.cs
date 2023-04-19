using SectorCreator.Global;
using SectorCreator.Global.Enums;
using SectorCreator.Models.Services;

namespace SectorCreator.Models.RTTWorldgen.Worlds;

public interface IAreanWorld
{
    RttWorldgenPlanet Generate(RttWorldgenPlanet planet, RttWorldgenStar primaryStar);
}

public class AreanWorld : IAreanWorld
{
    private readonly IRollingService _rollingService;
    private readonly IWorldValidation _worldValidation;

    public AreanWorld(IRollingService rollingService, IWorldValidation worldValidation)
    {
        _rollingService = rollingService;
        _worldValidation = worldValidation;
    }

    public RttWorldgenPlanet Generate(RttWorldgenPlanet planet, RttWorldgenStar primaryStar)
    {
        planet.Size = _rollingService.D6(1) - 1;
        planet.Atmosphere = GetAtmosphere(primaryStar);
        planet.Hydrographics = GetHydrographics(planet);
        planet.Chemistry = GetChemistry(primaryStar, planet);
        planet.Biosphere = GetBiosphere(primaryStar, planet);
        planet = _worldValidation.ValidatePlanet(planet);
        return planet;
    }

    private int GetAtmosphere(RttWorldgenStar primaryStar)
    {
        var roll = _rollingService.D6(1);
        if (primaryStar.SpectralType == SpectralType.L) {
            roll -= 2;
        }

        return roll switch {
            (<= 3) => 1,
            _ => 10
        };
    }

    private int GetHydrographics(RttWorldgenPlanet planet)
    {
        var hydrographics = _rollingService.D3(2) + planet.Size - 7;
        if (planet.Atmosphere == 1) {
            hydrographics -= 4;
        }

        return hydrographics;
    }

    private PlanetChemistry GetChemistry(RttWorldgenStar primaryStar, RttWorldgenPlanet planet)
    {
        var roll = _rollingService.D6(1);
        if (primaryStar.SpectralType == SpectralType.L || planet.PlanetOrbit == PlanetOrbit.Outer) {
            roll += 2;
        }

        return roll switch {
            (<= 4) => PlanetChemistry.Water,
            (<= 6) => PlanetChemistry.Ammonia,
            _ => PlanetChemistry.Methane
        };
    }

    private int GetBiosphere(RttWorldgenStar primaryStar, RttWorldgenPlanet planet)
    {
        int biosphere;
        if (primaryStar.Age >= 4 + ChemistryService.GetAgeMod(planet.Chemistry) && planet.Atmosphere == 10) {
            biosphere = _rollingService.D6(1) + planet.Size - 2;
        } else if (primaryStar.Age >= _rollingService.D3(1) + ChemistryService.GetAgeMod(planet.Chemistry)) {
            biosphere = planet.Atmosphere == 1
                ? _rollingService.D6(1) - 4
                : _rollingService.D3(1);
        } else {
            biosphere = 0;
        }

        return biosphere;
    }
}