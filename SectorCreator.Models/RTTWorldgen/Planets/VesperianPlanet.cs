using SectorCreator.Global;
using SectorCreator.Global.Enums;

namespace SectorCreator.Models.RTTWorldgen.Planets;

public interface IVesperianPlanet
{
    RttWorldgenPlanet Generate(RttWorldgenPlanet planet, RttWorldgenStar primaryStar);
}

public class VesperianPlanet : IVesperianPlanet
{
    private readonly IRollingService _rollingService;
    private readonly IPlanetValidation _planetValidation;

    public VesperianPlanet(IRollingService rollingService, IPlanetValidation planetValidation)
    {
        _rollingService = rollingService;
        _planetValidation = planetValidation;
    }   
    
    public RttWorldgenPlanet Generate(RttWorldgenPlanet planet, RttWorldgenStar primaryStar)
    {
        planet.Size = _rollingService.D6(1) + 4;
        planet.Chemistry = GetChemistry();
        planet.Biosphere = GetBiosphere(primaryStar);
        planet.Atmosphere = GetAtmosphere(planet);
        planet.Hydrographics = _rollingService.D6(2) - 2;
        planet = _planetValidation.ValidatePlanet(planet);
        return planet;
    }

    private PlanetChemistry GetChemistry()
    {
        return _rollingService.D6(2) switch {
            (<= 11) => PlanetChemistry.Water,
            12 => PlanetChemistry.Chlorine,
            _ => PlanetChemistry.None
        };
    }

    private int GetBiosphere(RttWorldgenStar primaryStar)
    {
        if (primaryStar.Age >= 4) {
            return _rollingService.D6(2);
        }

        if (primaryStar.Age >= _rollingService.D3(1)) {
            return _rollingService.D3(1);
        }

        return 0;
    }

    private int GetAtmosphere(RttWorldgenPlanet planet)
    {
        if (planet.Biosphere >= 3) {
            if (planet.Chemistry == PlanetChemistry.Water) {
                var atmosphere = _rollingService.D6(2) + planet.Size - 7;
                atmosphere = Math.Min(atmosphere, 9);
                return Math.Max(atmosphere, 2);
            }
            if (planet.Chemistry == PlanetChemistry.Chlorine) {
                return 11;
            }
        }

        return 10;
    }
}