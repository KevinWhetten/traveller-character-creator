using System;
using System.Linq;
using Moq;
using NUnit.Framework;
using SectorCreator.Global;
using SectorCreator.Global.Enums;
using SectorCreator.Models.Basic;
using SectorCreator.Models.Basic.Factories;
using SectorCreator.Models.CustomTypes;
using SectorCreator.Models.Factories.StarFrontiers;
using SectorCreator.Models.Factories.T5;
using SectorCreator.Models.RTTWorldgen;
using SectorCreator.Models.RTTWorldgen.Factories;
using SectorCreator.Models.RTTWorldgen.Worlds;

namespace SectorCreator.Models.Tests.Factories.Basic;

[TestFixture]
public class SectorFactoryTests
{
    private readonly SectorFactory _classUnderTest = new SectorFactory(new SubsectorFactory(new HexFactory(new RollingService(),
        new StarSystemFactory(new RollingService(), new PlanetFactory(new RollingService()), new StarFrontiersStarFactory(new RollingService()),
            new StarFrontiersPlanetFactory(new RollingService())), new It5StarSystemFactory(new RollingService()),
        new RttWorldgenStarSystemFactory(new RollingService(), new RttWorldgenStarFactory(new RollingService()),
            new RttWorldgenPlanetFactory(new RollingService(), new AcheronianWorld(new RollingService()), new AreanWorld(new RollingService()),
                new AridWorld(new RollingService()), new AsphodelianWorld(new RollingService()), new ChthonianWorld(),
                new HebeanWorld(new RollingService()), new HelianWorld(new RollingService()), new JaniLithicWorld(new RollingService()),
                new JovianWorld(new RollingService()), new MeltballWorld(new RollingService()), new OceanicWorld(new RollingService()),
                new PanthalassicWorld(new RollingService()), new PrometheanWorld(new RollingService()), new RockballWorld(new RollingService()),
                new SnowballWorld(new RollingService()), new StygianWorld(new RollingService()), new TectonicWorld(new RollingService()),
                new TelluricWorld(new RollingService()), new VesperianWorld(new RollingService()))))));

    private readonly Mock<ISubsectorFactory> _subsectorFactoryMock = new();

    private Random rand = new Random();
    private RollingService _rollingService = new RollingService();

    // [SetUp]
    // public void SetUp()
    // {
    //     _subsectorFactoryMock.Setup(x => x.GenerateMongooseSubsector(It.IsAny<SectorType>(), It.IsAny<Coordinates>()))
    //         .Returns(new Subsector());
    //     _subsectorFactoryMock.Setup(x => x.GenerateT5Subsector(It.IsAny<Coordinates>()))
    //         .Returns(new Subsector());
    //     _subsectorFactoryMock.Setup(x => x.GenerateRttWorldgenSubsector(It.IsAny<Coordinates>()))
    //         .Returns(new Subsector());
    //     _subsectorFactoryMock.Setup(x => x.GenerateStarFrontiersSubsector(It.IsAny<Coordinates>()))
    //         .Returns(new Subsector());
    //     _classUnderTest = new SectorFactory(_subsectorFactoryMock.Object);
    // }

    [Test]
    public void WhenGeneratingMongooseSector()
    {
        var sector = _classUnderTest.GenerateMongooseSector(SectorType.Basic);

        Assert.That(sector.Subsectors.Count, Is.EqualTo(16));
    }

    [Test]
    public void WhenGeneratingT5SectorSector()
    {
        var sector = _classUnderTest.GenerateT5Sector();

        Assert.That(sector.Subsectors.Count, Is.EqualTo(16));
    }

    [Test]
    public void WhenGeneratingRttWorldgenSectorSector()
    {
        var sector = _classUnderTest.GenerateRttWorldgenSector();

        var fileStream = "";

        foreach (var subsector in sector.Subsectors) {
            foreach (var hex in subsector.Hexes) {
                foreach (var starSystem in hex.StarSystems) {
                    foreach (RttWorldgenPlanet planet in starSystem.Planets) {
                        fileStream += $"Planet,{GetPlanetDetails(hex, planet, starSystem)}";
                        foreach (RttWorldgenPlanet satellite in planet.Satellites) {
                            fileStream += $"Moon,{GetPlanetDetails(hex, satellite, starSystem)}";
                        }
                    }
                }
            }
        }

        Assert.That(sector.Subsectors.Count, Is.EqualTo(16));
    }

    private string GetPlanetDetails(Hex hex, RttWorldgenPlanet planet, StarSystem starSystem)
    {
        var primaryStar = (RttWorldgenStar) starSystem.PrimaryStar;
        return
            $"{GetCoordinates(hex)},{planet.Name},{planet.Allegiance},{GetUWP(planet)},{GetTradeCodes(planet)},{GetImportance(planet)},{GetEconomicExtension(planet, starSystem.Planets.Count(x => x.PlanetType == PlanetType.Jovian), starSystem.Planets.Count(x => x.PlanetType == PlanetType.AsteroidBelt))},{GetCulturalExtension(planet)},{GetNobility(planet)},{GetBases(planet)},{GetTravelZone(planet)},{planet.PBG},{GetWorlds(starSystem)},{GetAllegiance(planet)},{GetStarData(starSystem)},{planet.WorldType},{planet.Biosphere},{planet.Chemistry},{planet.Temperature},{primaryStar.Age}\n";
    }

    private string GetStarData(StarSystem starSystem)
    {
        var primaryStar = (RttWorldgenStar) starSystem.PrimaryStar;
        var starDataString = $"{primaryStar.SpectralType}-{primaryStar.LuminosityClass} ";

        return starSystem.CompanionStars.Cast<RttWorldgenStar>()
            .Aggregate(starDataString, (current, star) => current + $"{star.SpectralType}-{star.LuminosityClass} ");
    }

    private string GetAllegiance(RttWorldgenPlanet planet)
    {
        return planet.Allegiance;
    }

    private char GetWorlds(StarSystem starSystem)
    {
        var sum = 0;

        foreach (var planet in starSystem.Planets) {
            sum++;
            sum += planet.Satellites.Count;
        }

        return ExtendedHex.values[sum];
    }

    private string GetTravelZone(RttWorldgenPlanet planet)
    {
        return ((char) planet.TravelZone).ToString();
    }

    private string GetBases(RttWorldgenPlanet planet)
    {
        return planet.Bases.Aggregate("", (current, baseStr) => current + $"{baseStr}");
    }

    private string GetNobility(RttWorldgenPlanet planet)
    {
        return planet.Nobility;
    }

    private string GetCulturalExtension(RttWorldgenPlanet planet)
    {
        return planet.CulturalExtension;
    }

    private string GetEconomicExtension(RttWorldgenPlanet planet, int gasGiantCount, int beltCount)
    {
        planet.GenerateEconomicExtension(gasGiantCount, beltCount);
        return planet.EconomicExtension;
    }

    private string GetImportance(RttWorldgenPlanet planet)
    {
        return $"{{ {planet.Importance} }}";
    }

    private string GetTradeCodes(Planet planet)
    {
        return string.Join(" ", planet.TradeCodes);
    }

    private string GetCoordinates(Hex hex)
    {
        var xCoordinate = hex.Coordinates.X.ToString().Length == 1 ? $"0{hex.Coordinates.X}" : $"{hex.Coordinates.X.ToString()}";
        var yCoordinate = hex.Coordinates.Y.ToString().Length == 1 ? $"0{hex.Coordinates.Y}" : $"{hex.Coordinates.Y.ToString()}";
        return $"{xCoordinate}{yCoordinate}";
    }

    private string GetUWP(Planet planet)
    {
        return
            $"{planet.Starport},{ExtendedHex.values[planet.Size]},{ExtendedHex.values[planet.Atmosphere]},{ExtendedHex.values[planet.Hydrographics]},{ExtendedHex.values[planet.Population]},{ExtendedHex.values[planet.Government]},{ExtendedHex.values[planet.LawLevel]},{ExtendedHex.values[planet.TechLevel]}";
    }

    [Test]
    public void WhenGeneratingStarFrontiersSectorSector()
    {
        var sector = _classUnderTest.GenerateStarFrontiersSector();

        Assert.That(sector.Subsectors.Count, Is.EqualTo(16));
    }
}