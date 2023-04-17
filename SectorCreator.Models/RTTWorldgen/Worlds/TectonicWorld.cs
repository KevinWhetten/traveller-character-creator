using SectorCreator.Global;
using SectorCreator.Global.Enums;
using SectorCreator.Models.Services;

namespace SectorCreator.Models.RTTWorldgen.Worlds;

public interface ITectonicWorld
{
    RttWorldgenPlanet Generate(RttWorldgenPlanet planet, RttWorldgenStar primaryStar);
}

public class TectonicWorld : ITectonicWorld
{
    private readonly IRollingService _rollingService;
    private readonly IWorldValidation _worldValidation;

    public TectonicWorld(IRollingService rollingService, IWorldValidation worldValidation)
    {
        _rollingService = rollingService;
        _worldValidation = worldValidation;
    }

    public RttWorldgenPlanet Generate(RttWorldgenPlanet planet, RttWorldgenStar primaryStar)
    {
        planet.Size = _rollingService.D6(1) + 4;
        planet.Chemistry = GetChemistry(primaryStar, planet);
        planet.Biosphere = GetBiosphere(primaryStar, planet);
        planet.Atmosphere = GetAtmosphere(planet);
        planet.Hydrographics = _rollingService.D6(2) - 2;
        planet = _worldValidation.ValidatePlanet(planet);
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
                _ => PlanetChemistry.Chlorine
            },
            (<= 8) => PlanetChemistry.Ammonia,
            _ => PlanetChemistry.Methane
        };
    }

    private int GetBiosphere(RttWorldgenStar primaryStar, RttWorldgenPlanet planet)
    {
        if (primaryStar.Age >= 4 + ChemistryService.GetAgeMod(planet.Chemistry)) {
            var mod = primaryStar.SpectralType == SpectralType.D ? -3 : 0;
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
            var atmosphereRoll = _rollingService.D6(2) + planet.Size - 7;
            atmosphereRoll = Math.Min(atmosphereRoll, 9);
            atmosphereRoll = Math.Max(atmosphereRoll, 2);
            return atmosphereRoll;
        }

        if (planet.Biosphere >= 3 && planet.Chemistry is PlanetChemistry.Sulfur or PlanetChemistry.Chlorine) {
            return 11;
        }

        return 10;
    }
}