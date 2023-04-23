using SectorCreator.Global;
using SectorCreator.Global.Enums;

namespace SectorCreator.Models.RTTWorldgen.Worlds;

public interface IJovianWorld
{
    RttWorldgenPlanet Generate(RttWorldgenPlanet planet, RttWorldgenStar primaryStar);
}

public class JovianWorld : IJovianWorld
{
    private readonly IRollingService _rollingService;

    public JovianWorld(IRollingService rollingService)
    {
        _rollingService = rollingService;
    }

    public RttWorldgenPlanet Generate(RttWorldgenPlanet planet, RttWorldgenStar primaryStar)
    {
        planet.WorldType = WorldType.Jovian;
        planet.Size = 16;
        planet.Atmosphere = 16;
        planet.Hydrographics = 16;
        planet.Biosphere = GetBiosphere(primaryStar, planet);
        planet.Chemistry = GetChemistry(primaryStar, planet);
        planet = WorldValidation.ValidatePlanet(planet);
        return planet;
    }

    private int GetBiosphere(RttWorldgenStar primaryStar, RttWorldgenPlanet planet)
    {
        int biosphere = 0;
        var roll = _rollingService.D6(1);
        if (planet.PlanetOrbit == PlanetOrbit.Inner) {
            roll += 2;
        }

        switch (roll) {
            case <= 5:
                biosphere = 0;
                break;
            case <= 8:
                if (primaryStar.Age >= 7) {
                    biosphere = _rollingService.D6(2);
                    if (primaryStar.SpectralType == SpectralType.D) {
                        biosphere -= 3;
                    }
                } else if (primaryStar.Age >= _rollingService.D6(1)) {
                    biosphere = _rollingService.D3(1);
                } else {
                    biosphere = 0;
                }

                break;
        }

        return biosphere;
    }

    private PlanetChemistry GetChemistry(RttWorldgenStar primaryStar, RttWorldgenPlanet planet)
    {
        if (planet.Biosphere >= 1) {
            var chemistry = _rollingService.D6(1);
            if (primaryStar.SpectralType == SpectralType.L) {
                chemistry++;
            }

            if (planet.PlanetOrbit == PlanetOrbit.Epistellar) {
                chemistry -= 2;
            } else if (planet.PlanetOrbit == PlanetOrbit.Outer) {
                chemistry += 2;
            }

            return planet.Chemistry = chemistry switch {
                (<= 3) => PlanetChemistry.Water,
                (>= 4) => PlanetChemistry.Ammonia
            };
        }

        return PlanetChemistry.None;
    }
}