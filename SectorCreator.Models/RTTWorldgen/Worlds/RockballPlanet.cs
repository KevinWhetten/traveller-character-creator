using SectorCreator.Global;
using SectorCreator.Global.Enums;

namespace SectorCreator.Models.RTTWorldgen.Worlds;

public interface IRockballPlanet
{
    RttWorldgenPlanet Generate(RttWorldgenPlanet planet, RttWorldgenStar primaryStar);
}

public class RockballPlanet : IRockballPlanet
{
    private readonly IRollingService _rollingService;
    private readonly IPlanetValidation _planetValidation;

    public RockballPlanet(IRollingService rollingService, IPlanetValidation planetValidation)
    {
        _rollingService = rollingService;
        _planetValidation = planetValidation;
    }

    public RttWorldgenPlanet Generate(RttWorldgenPlanet planet, RttWorldgenStar primaryStar)
    {
        planet.Size = _rollingService.D6(1) - 1;
        planet.Atmosphere = 0;
        planet.Hydrographics = GetHydrographics(primaryStar, planet);
        planet.Biosphere = 0;
        planet = _planetValidation.ValidatePlanet(planet);
        return planet;
    }

    private int GetHydrographics(RttWorldgenStar primaryStar, RttWorldgenPlanet planet)
    {
        var hydrographics = _rollingService.D6(2) + planet.Size - 11;
        if (primaryStar.SpectralType == SpectralType.L) {
            hydrographics++;
        }

        if (planet.PlanetOrbit == PlanetOrbit.Epistellar) {
            hydrographics -= 2;
        } else if (planet.PlanetOrbit == PlanetOrbit.Outer) {
            hydrographics += 2;
        }

        return hydrographics;
    }
}