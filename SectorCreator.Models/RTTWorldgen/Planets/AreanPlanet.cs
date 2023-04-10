using SectorCreator.Global;
using SectorCreator.Global.Enums;

namespace SectorCreator.Models.RTTWorldgen.Planets;

public interface IAreanPlanet
{
    RttWorldgenPlanet Generate(RttWorldgenPlanet planet, RttWorldgenStar primaryStar);
}

public class AreanPlanet : IAreanPlanet
{
    private readonly IRollingService _rollingService;
    private readonly IPlanetValidation _planetValidation;

    public AreanPlanet(IRollingService rollingService, IPlanetValidation planetValidation)
    {
        _rollingService = rollingService;
        _planetValidation = planetValidation;
    }
    
    public RttWorldgenPlanet Generate(RttWorldgenPlanet planet, RttWorldgenStar primaryStar)
    {
        planet.Size = _rollingService.D6(1) - 1;
        planet.Atmosphere = GetAtmosphere(primaryStar);
        planet.Hydrographics = GetHydrographics(planet);
        planet.Chemistry = GetChemistry(primaryStar, planet);
        planet.Biosphere = GetBiosphere(primaryStar, planet);
        planet = _planetValidation.ValidatePlanet(planet);
        return planet;
    }

    private int GetAtmosphere(RttWorldgenStar primaryStar)
    {
        var roll = _rollingService.D6(1);
        if (primaryStar.SpectralType == SpectralType.D) {
            roll -= 2;
        }

        return roll switch {
            (<= 3) => 1,
            (<= 6) => 10,
            _ => 0
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
            (<= 8) => PlanetChemistry.Methane,
            _ => PlanetChemistry.None
        };
    }

    private int GetBiosphere(RttWorldgenStar primaryStar, RttWorldgenPlanet planet)
    {
        int biosphere;
        if (primaryStar.Age >= 4 + (int) planet.Chemistry && planet.Atmosphere == 10) {
            biosphere = _rollingService.D6(1) + planet.Size - 2;
        } else if (primaryStar.Age >= _rollingService.D3(1) + (int) planet.Chemistry) {
            biosphere = planet.Atmosphere switch {
                1 => _rollingService.D6(1) - 4,
                10 => _rollingService.D3(1),
                _ => 0
            };
        } else {
            biosphere = 0;
        }

        return biosphere;
    }
}