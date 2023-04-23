using SectorCreator.Global;
using SectorCreator.Global.Enums;

namespace SectorCreator.Models.RTTWorldgen.Worlds;

public interface IVesperianWorld
{
    public RttWorldgenPlanet Generate(RttWorldgenPlanet planet, RttWorldgenStar primaryStar);
}

public class VesperianWorld : IVesperianWorld
{
    private static IRollingService _rollingService;

    public VesperianWorld(IRollingService rollingService)
    {
        _rollingService = rollingService;
    }

    public RttWorldgenPlanet Generate(RttWorldgenPlanet planet, RttWorldgenStar primaryStar)
    {
        planet.WorldType = WorldType.Vesperian;
        planet.Size = _rollingService.D6(1) + 4;
        planet.Chemistry = GetChemistry();
        planet.Biosphere = GetBiosphere(primaryStar);
        planet.Atmosphere = GetAtmosphere(planet);
        planet.Hydrographics = _rollingService.D6(2) - 2;
        planet = WorldValidation.ValidatePlanet(planet);
        return planet;
    }

    private static PlanetChemistry GetChemistry()
    {
        return _rollingService.D6(2) switch {
            (<= 11) => PlanetChemistry.Water,
            (>= 12) => PlanetChemistry.Chlorine
        };
    }

    private static int GetBiosphere(RttWorldgenStar primaryStar)
    {
        if (primaryStar.Age >= 4) {
            return _rollingService.D6(2);
        }

        if (primaryStar.Age >= _rollingService.D3(1)) {
            return _rollingService.D3(1);
        }

        return 0;
    }

    private static int GetAtmosphere(RttWorldgenPlanet planet)
    {
        if (planet.Biosphere >= 3) {
            if (planet.Chemistry != PlanetChemistry.Water) return 11;

            var atmosphere = _rollingService.D6(2) + planet.Size - 7;
            atmosphere = Math.Min(atmosphere, 9);
            return Math.Max(atmosphere, 2);
        }

        return 10;
    }
}