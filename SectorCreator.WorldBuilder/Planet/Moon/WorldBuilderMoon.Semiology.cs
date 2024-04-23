using SectorCreator.WorldBuilder.Planet.Planet;

namespace SectorCreator.WorldBuilder.Planet.Moon;

public partial class WorldBuilderMoon
{
    public void GenerateSeismicActivity(WorldBuilderStarSystem starSystem, WorldBuilderPlanet parent)
    {
        var dm = 1;
        if (Density > 1.0) dm += 2;
        if (Density < 0.5) dm--;

        var seismicStressBase = Math.Floor(Size - starSystem.Age + dm);
        if (seismicStressBase > 0) {
            ResidualSeismicStress = Math.Pow(seismicStressBase, 2);
        }

        TidalHeatingFactor = Math.Pow(parent.Mass, 2) * Math.Pow(Size, 5) * Math.Pow(Eccentricity, 2) /
                             (3000 * Math.Pow(OrbitDistanceInKM / 1000000.0, 5) * Period * Mass);

        LowTemperature = (int) Math.Pow(Math.Pow(LowTemperature, 4) + Math.Pow(TotalSeismicStress, 4), 1.0 / 4.0);
        Temperature = (int) Math.Pow(Math.Pow(Temperature, 4) + Math.Pow(TotalSeismicStress, 4), 1.0 / 4.0);
        HighTemperature = (int) Math.Pow(Math.Pow(HighTemperature, 4) + Math.Pow(TotalSeismicStress, 4), 1.0 / 4.0);

        if (TotalSeismicStress > 0 && Hydrographics > 0) {
            dm = 0;
            if (TotalSeismicStress is >= 10 and < 100) dm++;
            if (TotalSeismicStress >= 100) dm += 2;

            MajorTectonicPlates = Math.Max(Size + Hydrographics - _rollingService.D6(2) + dm, 0);
        }
    }
}