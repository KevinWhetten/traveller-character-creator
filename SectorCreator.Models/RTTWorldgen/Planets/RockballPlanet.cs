using SectorCreator.Global;
using SectorCreator.Global.Enums;

namespace SectorCreator.Models.RTTWorldgen.Planets;

public static class RockballPlanet
{
    public static RttWorldgenPlanet Generate(RttWorldgenPlanet planet, RttWorldgenStar primaryStar)
    {
        planet.Size = Roll.D6(1) - 1;
        planet.Atmosphere = 0;
        planet.Hydrographics = GetHydrographics(primaryStar, planet);
        planet.Biosphere = 0;
        planet = PlanetValidation.ValidatePlanet(planet);
        return planet;
    }

    private static int GetHydrographics(RttWorldgenStar primaryStar, RttWorldgenPlanet planet)
    {
        var hydrographics = Roll.D6(2) + planet.Size - 11;
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