using SectorCreator.Global;
using SectorCreator.Global.Enums;

namespace SectorCreator.Models.RTTWorldgen.Worlds;

public interface IJovianPlanet
{
    RttWorldgenPlanet Generate(RttWorldgenPlanet planet, RttWorldgenStar primaryStar);
}

public class JovianPlanet : IJovianPlanet
{
    private readonly IRollingService _rollingService;
    private readonly IPlanetValidation _planetValidation;

    public JovianPlanet(IRollingService rollingService, IPlanetValidation planetValidation)
    {
        _rollingService = rollingService;
        _planetValidation = planetValidation;
    }

    public RttWorldgenPlanet Generate(RttWorldgenPlanet planet, RttWorldgenStar primaryStar)
    {
        planet.Size = 16;
        planet.Atmosphere = 16;
        planet.Hydrographics = 16;
        planet.Biosphere = GetBiosphere(primaryStar, planet);
        planet.Chemistry = GetChemistry(primaryStar, planet);
        planet = _planetValidation.ValidatePlanet(planet);
        return planet;
    }

    private int GetBiosphere(RttWorldgenStar primaryStar, RttWorldgenPlanet planet)
    {
        var biosphere = 0;
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
            default:
                biosphere = 0;
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