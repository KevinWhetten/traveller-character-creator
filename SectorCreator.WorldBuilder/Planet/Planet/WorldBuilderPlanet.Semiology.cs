using SectorCreator.Global.Enums;

namespace SectorCreator.WorldBuilder.Planet.Planet;

public partial class WorldBuilderPlanet
{
    public double ResidualSeismicStress { get; set; }
    public double TidalStressFactor => (StarTidalEffect + MoonTidalEffect) / 10.0;
    public double TidalHeatingFactor { get; set; }
    public double TotalSeismicStress => ResidualSeismicStress + TidalStressFactor + TidalHeatingFactor;
    public int MajorTectonicPlates { get; set; }

    public void GenerateSeismicActivity(WorldBuilderStarSystem starSystem)
    {
        if (PlanetType != PlanetType.AsteroidBelt) {
            var dm = 0;

            var moonDM = 0;
            if (Moons.Count(x => x.Size is >= 1 and <= 20) > 0) moonDM = Moons.Where(x => x.Size is >= 1 and <= 20).Sum(x => x.Size);
            dm += Math.Min(moonDM, 12);
            if (Density > 1.0) dm += 2;
            if (Density < 0.5) dm--;

            var seismicStressBase = Math.Floor(Size - starSystem.Age + dm);
            if (seismicStressBase > 0) {
                ResidualSeismicStress = Math.Pow(seismicStressBase, 2);
            }

            TidalHeatingFactor = (Math.Pow(starSystem.Mass, 2) * Math.Pow(Size, 5) * Eccentricity) /
                                 (Math.Pow(OrbitDistance, 5) * Period * Mass);

            LowTemperature = (int) Math.Pow(Math.Pow(LowTemperature, 4) + Math.Pow(TotalSeismicStress, 4), 1.0 / 4.0);
            Temperature = (int) Math.Pow(Math.Pow(Temperature, 4) + Math.Pow(TotalSeismicStress, 4), 1.0 / 4.0);
            HighTemperature = (int) Math.Pow(Math.Pow(HighTemperature, 4) + Math.Pow(TotalSeismicStress, 4), 1.0 / 4.0);

            if (PlanetType == PlanetType.Terrestrial && TotalSeismicStress > 0 && Hydrographics > 0) {
                dm = 0;
                if (TotalSeismicStress is >= 10 and < 100) dm++;
                if (TotalSeismicStress >= 100) dm += 2;

                MajorTectonicPlates = Math.Max(Size + Hydrographics - _rollingService.D6(2) + dm, 0);
            }
        }
    }
}