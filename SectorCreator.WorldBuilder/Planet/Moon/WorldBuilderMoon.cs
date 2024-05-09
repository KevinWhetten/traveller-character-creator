using SectorCreator.Global.Enums;
using SectorCreator.WorldBuilder.Planet.Planet;
using SectorCreator.WorldBuilder.Planet.TerrestrialPlanet;

namespace SectorCreator.WorldBuilder.Planet.Moon;

public partial class WorldBuilderMoon : WorldBuilderTerrestrialPlanet
{
    public double ParentDiameter { get; set; }
    public MoonOrbit MoonOrbit { get; set; }
    private double PlanetTidalEffect { get; set; }
    public new double TidalStressFactor => (StarTidalEffect + PlanetTidalEffect) / 10.0;

    public WorldBuilderMoon() { }

    public WorldBuilderMoon(int size)
    {
        PlanetType = PlanetType.Terrestrial;
        IsMoon = true;
        Size = size;
    }

    public void Generate(WorldBuilderStarSystem starSystem, WorldBuilderPlanet parent)
    {
        IsMoon = true;
        ParentDiameter = parent.Diameter;
        GenerateSizeCharacteristics(starSystem);

        GenerateOrbitLocation(parent);
        CalculateEccentricity(parent.MoonOrbitRange);
        CalculatePeriod(parent);

        GenerateBasicCharacteristics(starSystem);
    }

    public new void GenerateSizeCharacteristics(WorldBuilderStarSystem starSystem)
    {
        GenerateDiameter();
        GenerateComposition(starSystem);
        GenerateDensity();
    }
}