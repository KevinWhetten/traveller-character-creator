﻿using TravellerCreatorGlobalMethods;
using TravellerCreatorModels.RTTWorldgen;

namespace TravellerCreatorServices.RTTWorldgen.Planets;

public static class HelianPlanet
{
    public static RTTWorldgenPlanet Generate(RTTWorldgenPlanet planet)
    {
        planet.Size = Roll.D6(1) + 9;
        planet.Atmosphere = 13;
        planet.Hydrographics = GetHydrographics();
        planet.Biosphere = 0;
        planet = PlanetValidation.ValidatePlanet(planet);
        return planet;
    }

    private static int GetHydrographics()
    {
        return Roll.D6(1) switch {
            (<= 2) => 0,
            (<= 4) => Roll.D6(2) - 1,
            (<= 6) => 15,
            _ => 0
        };
    }
}