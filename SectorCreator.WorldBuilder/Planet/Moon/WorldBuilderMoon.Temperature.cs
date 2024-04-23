using SectorCreator.WorldBuilder.Planet.Planet;

namespace SectorCreator.WorldBuilder.Planet.Moon;

public partial class WorldBuilderMoon
{
    public void GenerateTemperatureCharacteristics(WorldBuilderStarSystem starSystem, WorldBuilderPlanet parent)
    {
        if (Albedo == 0) {
            GenerateAlbedo(starSystem.HZCO);
        }

        if (GreenhouseFactor == 0) {
            GenerateGreenhouseFactor();
        }


        if (!IsTidallyLocked) {
            var highLuminosity = starSystem.Luminosity * (1 + LuminosityModifier);
            var lowLuminosity = starSystem.Luminosity * (1 - LuminosityModifier);

            HighTemperature = (int) (279 *
                                     Math.Pow(
                                         highLuminosity * (1 - Albedo) * (1 + GreenhouseFactor) /
                                         (parent.NearAU - ((WorldBuilderPlanet) this).OrbitDistance), 1.0 / 4.0));
            Temperature = (int) (279 * Math.Pow(starSystem.Luminosity * (1 - Albedo) * (1 + GreenhouseFactor) / parent.OrbitDistance, 1.0 / 4.0));
            LowTemperature = (int) (279 *
                                    Math.Pow(
                                        lowLuminosity * (1 - Albedo) * (1 + GreenhouseFactor) /
                                        (parent.FarAU + ((WorldBuilderPlanet) this).OrbitDistance), 1.0 / 4.0));
        } else {
            var highTemperatureVarianceFactor = AxialTiltFactor + 1 + GeographicFactor;
            var lowTemperatureVarianceFactor = Math.Max(AxialTiltFactor - 1 + GeographicFactor, 0);

            var highLuminosityModifier = highTemperatureVarianceFactor / AtmosphericFactor;
            var lowLuminosityModifier = lowTemperatureVarianceFactor / AtmosphericFactor;
            var highLuminosity = starSystem.Luminosity * (1 + highLuminosityModifier);
            var lowLuminosity = starSystem.Luminosity * (1 - lowLuminosityModifier);

            HighTemperature = (int) (279 *
                                     Math.Pow(
                                         highLuminosity * (1 - Albedo) * (1 + GreenhouseFactor) /
                                         (parent.NearAU - ((WorldBuilderPlanet) this).OrbitDistance), 1.0 / 4.0));
            Temperature = (int) (279 * Math.Pow(starSystem.Luminosity * (1 - Albedo) * (1 + GreenhouseFactor) / parent.OrbitDistance, 1.0 / 4.0));
            LowTemperature = (int) (279 *
                                    Math.Pow(
                                        lowLuminosity * (1 - Albedo) * (1 + GreenhouseFactor) /
                                        (parent.FarAU + ((WorldBuilderPlanet) this).OrbitDistance), 1.0 / 4.0));
        }
    }
}