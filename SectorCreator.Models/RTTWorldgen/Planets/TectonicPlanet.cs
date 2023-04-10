using SectorCreator.Global;
using SectorCreator.Global.Enums;

namespace SectorCreator.Models.RTTWorldgen.Planets;

public interface ITectonicPlanet
{
    RttWorldgenPlanet Generate(RttWorldgenPlanet planet, RttWorldgenStar primaryStar);
}

public class TectonicPlanet : ITectonicPlanet
{
    private readonly IRollingService _rollingService;
    private readonly IPlanetValidation _planetValidation;

    public TectonicPlanet(IRollingService rollingService, IPlanetValidation planetValidation)
    {
        _rollingService = rollingService;
        _planetValidation = planetValidation;
    }
    
    public RttWorldgenPlanet Generate(RttWorldgenPlanet planet, RttWorldgenStar primaryStar)
    {
        planet.Size = _rollingService.D6(1) + 4;
        planet.Chemistry = GetChemistry(primaryStar, planet);
        planet.Biosphere = GetBiosphere(primaryStar, planet);
        planet.Hydrographics = GetAtmosphere(planet);
        planet.Hydrographics = _rollingService.D6(2) - 2;
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
            (<= 6) => _rollingService.D6(2) switch {
                (<= 8) => PlanetChemistry.Water,
                (<= 11) => PlanetChemistry.Sulfur,
                (<= 12) => PlanetChemistry.Chlorine,
                _ => PlanetChemistry.None
            },
            (<= 8) => PlanetChemistry.Ammonia,
            (<= 10) => PlanetChemistry.Methane,
            _ => PlanetChemistry.None
        };
    }

    private int GetBiosphere(RttWorldgenStar primaryStar, RttWorldgenPlanet planet)
    {
        if (primaryStar.Age >= 4 + (int) planet.Chemistry) {
            var mod = primaryStar.SpectralType == SpectralType.D ? -3 : 0;
            return _rollingService.D6(2) + mod;
        }

        if (primaryStar.Age >= _rollingService.D3(1) + (int) planet.Chemistry) {
            return _rollingService.D3(1);
        }

        return 0;
    }

    private int GetAtmosphere(RttWorldgenPlanet planet)
    {
        if (planet.Biosphere >= 3 && planet.Chemistry == PlanetChemistry.Water) {
            var atmosphereRoll = _rollingService.D6(2) + planet.Size - 7;
            atmosphereRoll = Math.Min(atmosphereRoll, 9);
            atmosphereRoll = Math.Max(atmosphereRoll, 2);
            return atmosphereRoll;
        }

        if (planet.Biosphere >= 3
            && planet.Chemistry is PlanetChemistry.Sulfur or PlanetChemistry.Chlorine) {
            return 11;
        }

        return 10;
    }
}