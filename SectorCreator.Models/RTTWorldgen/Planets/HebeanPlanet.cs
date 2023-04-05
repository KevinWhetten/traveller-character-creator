using SectorCreator.Global;

namespace SectorCreator.Models.RTTWorldgen.Planets;

public static class HebeanPlanet
{
    public static RttWorldgenPlanet Generate(RttWorldgenPlanet planet)
    {
        planet.Size = Roll.D6(1) - 1;
        planet.Atmosphere = GetAtmosphere(planet);
        planet.Hydrographics = Roll.D6(2) + planet.Size - 11;
        planet.Biosphere = 0;
        planet = PlanetValidation.ValidatePlanet(planet);
        return planet;
    }

    private static int GetAtmosphere(RttWorldgenPlanet planet)
    {
        var atmosphere = Roll.D6(1) + planet.Size - 6;
        if (atmosphere >= 2) {
            atmosphere = 10;
        }

        return atmosphere;
    }
}