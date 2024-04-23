using SectorCreator.Global;
using SectorCreator.WorldBuilder.Enums;
using SectorCreator.WorldBuilder.Planet;
using SectorCreator.WorldBuilder.Planet.Planet.OtherObjects;
using SectorCreator.WorldBuilder.Planet.TerrestrialPlanet;
using SectorCreator.WorldBuilder.Services;
using SectorCreator.WorldBuilder.Star;
using WorldBuilderMoon = SectorCreator.WorldBuilder.Planet.Moon.WorldBuilderMoon;

namespace SectorCreator.WorldBuilder.Tests.Planet;

[TestFixture]
public class WorldBuilderPlanet_Tests
{
    private WorldBuilderTerrestrialPlanet Earth = new() {
        Atmosphere = 6,
        Hydrographics = 7,
        OrbitNumber = 3.0,
        Eccentricity = 0.017,
        Period = 1,
        // Size
        Size = 8,
        Diameter = 12742,
        Composition = Composition.RockAndMetal,
        Density = 1,
        // Atmosphere
        BAR = 1.013,
        AtmosphereChemicals = new List<PlanetElement> {
            new() {
                Element = CompositionService.Elements.ToArray()[9],
                Percent = 78.08
            },
            new() {
                Element = CompositionService.Elements.ToArray()[13],
                Percent = 20.95
            },
            new() {
                Element = CompositionService.Elements.ToArray()[16],
                Percent = 0.93
            },
        },
        OxygenFraction = .2095,
        // Hydrographics
        HydroPercent = 70.8,
        LiquidChemicals = new List<PlanetElement> {
            new() {
                Element = CompositionService.Elements.ToArray()[5],
                Percent = 100
            }
        },
        SurfaceDistribution = SurfaceDistribution.Mixed,
        MajorContinents = 2,
        MinorContinents = 5,
        // Rotation
        SiderealDay = 23.9344,
        AxialTilt = 23.439,
        IsTidallyLocked = false,
        MoonTidalEffect = 0.54,
        StarTidalEffect = 0.25,
        // Temperature
        HighTemperature = 311,
        Temperature = 288,
        LowTemperature = 262,
        Albedo = 0.3,
        GreenhouseFactor = 0.622,
        ResidualSeismicStress = 25,
        TidalHeatingFactor = 0,
        MajorTectonicPlates = 7,
        // Life
        BiomassRating = 10,
        BiocomplexityRating = 8,
        CurrentSophontExists = true,
        BiodiversityRating = 9,
        CompatibilityRating = 10,
        // Resources
        ResourceRating = 8,
        // Habitability
        HabitabilityRating = 10,
        // Subordinates
        Moons = new List<WorldBuilderMoon> {
            new() {
                Name = "Luna",
                Size = 2,
                Diameter = 3475,
                Atmosphere = 0,
                Hydrographics = 0,
                OrbitNumber = 30,
                Eccentricity = 0.549,
                ParentDiameter = 12742,
                Density = 0.606,
                Period = 655.72,
            }
        }
    }; 

    public WorldBuilderStarSystem SolSystem = new(new RollingService()) {
        Star = new WorldBuilderStar {
            Name = "Sol",
            Age = 4.568,
            Diameter = 1,
            Temperature = 5772
        }
    };

    [Test]
    public void WhenGettingEarthPlanetDetails()
    {
        Console.Write(Earth.GetDetailedReport(SolSystem));
    }
}