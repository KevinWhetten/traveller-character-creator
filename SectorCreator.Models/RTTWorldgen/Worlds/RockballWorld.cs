using SectorCreator.Global;
using SectorCreator.Global.Enums;

namespace SectorCreator.Models.RTTWorldgen.Worlds;

public interface IRockballWorld
{
    RttWorldgenPlanet Generate(RttWorldgenPlanet planet, RttWorldgenStar primaryStar);
}

public class RockballWorld : IRockballWorld
{
    private readonly IRollingService _rollingService;
    
    public RockballWorld(IRollingService rollingService)
    {
        _rollingService = rollingService;
    }

    public RttWorldgenPlanet Generate(RttWorldgenPlanet planet, RttWorldgenStar primaryStar)
    {
        planet.WorldType = WorldType.Rockball;
        planet.Size = _rollingService.D6(1) - 1;
        planet.Atmosphere = 0;
        planet.Hydrographics = GetHydrographics(primaryStar, planet);
        planet.Biosphere = 0;
        planet = WorldValidation.ValidatePlanet(planet);
        return planet;
    }

    private int GetHydrographics(RttWorldgenStar primaryStar, RttWorldgenPlanet planet)
    {
        var hydrographics = _rollingService.D6(2) + planet.Size - 11;
        if (primaryStar.SpectralType == SpectralType.BD) {
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