using SectorCreator.Global;
using SectorCreator.Global.Enums;

namespace SectorCreator.Models.RTTWorldgen.Planets;

public interface IAridPlanet
{
    RttWorldgenPlanet Generate(RttWorldgenPlanet planet, RttWorldgenStar primaryStar);
}

public class AridPlanet : IAridPlanet
{
    private readonly IRollingService _rollingService;
    private readonly IPlanetValidation _planetValidation;

    public AridPlanet(IRollingService rollingService, IPlanetValidation planetValidation)
    {
        _rollingService = rollingService;
        _planetValidation = planetValidation;
    }
    
    public RttWorldgenPlanet Generate(RttWorldgenPlanet planet, RttWorldgenStar primaryStar)
    {
        planet.Size = _rollingService.D6(1) + 4;
        planet.Chemistry = GetChemistry(primaryStar, planet);
        planet.Biosphere = GetBiosphere(primaryStar, planet);
        planet.Atmosphere = GetAtmosphere(planet);
        planet.Hydrographics = _rollingService.D3(1);
        planet = _planetValidation.ValidatePlanet(planet);
        return planet;
    }

    private PlanetChemistry GetChemistry(RttWorldgenStar primaryStar, RttWorldgenPlanet planet)
    {
        var roll = _rollingService.D6(1);
        if (primaryStar.SpectralType == SpectralType.K && primaryStar.Luminosity == Luminosity.V) {
            roll += 2;
        } else if (primaryStar.SpectralType == SpectralType.M && primaryStar.Luminosity == Luminosity.V) {
            roll += 4;
        } else if (primaryStar.SpectralType == SpectralType.L) {
            roll += 5;
        }

        if (planet.PlanetOrbit == PlanetOrbit.Outer) {
            roll += 2;
        }

        return roll switch {
            (<= 6) => PlanetChemistry.Water,
            (<= 8) => PlanetChemistry.Ammonia,
            (<= 10) => PlanetChemistry.Methane,
            _ => PlanetChemistry.None
        };
    }

    private int GetBiosphere(RttWorldgenStar primaryStar, RttWorldgenPlanet planet)
    {
        int biosphere;

        if (primaryStar.Age >= _rollingService.D3(1) + (int) planet.Chemistry) {
            biosphere = _rollingService.D3(1);
        } else if (primaryStar.Age >= 4 + (int) planet.Chemistry) {
            biosphere = _rollingService.D6(2);
            if (primaryStar.SpectralType == SpectralType.D) {
                biosphere -= 3;
            }
        } else {
            biosphere = 0;
        }

        return biosphere;
    }

    private int GetAtmosphere(RttWorldgenPlanet planet)
    {
        return planet.Biosphere >= 3 && planet.Chemistry == PlanetChemistry.Water
        ? Math.Max(Math.Min(_rollingService.D6(2) - 7 + planet.Size, 9), 2)
        : 10;
    }
}