using TravellerCreatorGlobalMethods;
using TravellerCreatorModels.SectorCreator.Enums;
using TravellerCreatorModels.SectorCreator.RTTWorldgen;

namespace TravellerCreatorServices.RTTWorldgen.Planets;

public static class SnowballPlanet
{
    public static RTTWorldgenPlanet Generate(RTTWorldgenPlanet planet, RTTWorldgenStar primaryStar)
    {
        int hydrosphereRoll = Roll.D6(1);
        
        planet.Size = Roll.D6(1) - 1;
        planet.Atmosphere = GetAtmosphere();
        planet.Hydrographics = GetHydrographics(hydrosphereRoll);
        planet.Chemistry = GetChemistry(primaryStar, planet);
        planet.Biosphere = GetBiosphere(primaryStar, planet, hydrosphereRoll);
        planet = PlanetValidation.ValidatePlanet(planet);
        return planet;
    }

    private static int GetAtmosphere()
    {
        return Roll.D6(1) switch {
            (<= 4) => 0,
            (<= 6) => 1,
            _ => 0
        };
    }

    private static int GetHydrographics(int hydrosphereRoll)
    {
        return hydrosphereRoll switch {
            (<= 3) => 10,
            (<= 6) => Roll.D6(2) - 2,
            _ => 0
        };
    }

    private static PlanetChemistry GetChemistry(RTTWorldgenStar primaryStar, RTTWorldgenPlanet planet)
    {
        var roll = Roll.D6(1);
        if (primaryStar.SpectralType == SpectralType.L) {
            roll += 2;
        }

        if (planet.PlanetOrbit == PlanetOrbit.Outer) {
            roll += 2;
        }

        return roll switch {
            (<= 4) => PlanetChemistry.Water,
            (<= 6) => PlanetChemistry.Ammonia,
            (<= 8) => PlanetChemistry.Methane,
            _ => PlanetChemistry.None
        };
    }

    private static int GetBiosphere(RTTWorldgenStar primaryStar, RTTWorldgenPlanet planet, int hydrosphereRoll)
    {
        if (hydrosphereRoll >= 4) {
            if (primaryStar.Age >= 6 + (int) planet.Chemistry) {
                return Roll.D6(1) + planet.Size - 2;
            }

            if (primaryStar.Age >= Roll.D6(1)) {
                return Roll.D6(1) - 3;
            }

            return 0;
        }

        return 0;
    }
}