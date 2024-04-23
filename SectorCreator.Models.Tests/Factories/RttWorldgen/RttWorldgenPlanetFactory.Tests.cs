using System;
using Moq;
using NUnit.Framework;
using SectorCreator.Global;
using SectorCreator.Global.Enums;
using SectorCreator.Models.RTTWorldgen;
using SectorCreator.Models.RTTWorldgen.Factories;
using SectorCreator.Models.RTTWorldgen.Worlds;

namespace SectorCreator.Models.Tests.Factories.RttWorldgen;

[TestFixture]
public class RttWorldgenPlanetFactoryTests
{
    private RttWorldgenPlanetFactory _classUnderTest;
    private readonly Mock<IRollingService> _mockRollingService = new();

    [Test]
    public void WhenGeneratingPlanet()
    {
        _classUnderTest = new RttWorldgenPlanetFactory(_mockRollingService.Object, new AcheronianWorld(new RollingService()),
            new AreanWorld(new RollingService()),
            new AridWorld(new RollingService()),
            new AsphodelianWorld(new RollingService()),
            new ChthonianWorld(),
            new HebeanWorld(new RollingService()),
            new HelianWorld(new RollingService()),
            new JaniLithicWorld(new RollingService()),
            new JovianWorld(new RollingService()),
            new MeltballWorld(new RollingService()),
            new OceanicWorld(new RollingService()),
            new PanthalassicWorld(new RollingService()),
            new PrometheanWorld(new RollingService()),
            new RockballWorld(new RollingService()),
            new SnowballWorld(new RollingService()),
            new StygianWorld(new RollingService()),
            new TectonicWorld(new RollingService()),
            new TelluricWorld(new RollingService()),
            new VesperianWorld(new RollingService()));

        _classUnderTest.GenerateRttWorldgenPlanet(new RttWorldgenStar(), PlanetOrbit.Epistellar, 1, new Coordinates());

        Assert.That(true);
    }

    [TestCase(1, PlanetType.AsteroidBelt)]
    [TestCase(2, PlanetType.DwarfPlanet)]
    [TestCase(3, PlanetType.Terrestrial)]
    [TestCase(4, PlanetType.Helian)]
    [TestCase(5, PlanetType.Jovian)]
    [TestCase(6, PlanetType.Jovian)]
    public void WhenGeneratingPlanetType(int planetTypeRoll, PlanetType expectedPlanetType)
    {
        _mockRollingService.Setup(x => x.D6(1)).Returns(planetTypeRoll);
        _classUnderTest = new RttWorldgenPlanetFactory(_mockRollingService.Object);
        var planet = new RttWorldgenPlanet();

        planet = _classUnderTest.GeneratePlanetType(planet, new RttWorldgenStar());

        Assert.That(planet.PlanetType, Is.EqualTo(expectedPlanetType));
    }

    [TestCase(1, PlanetType.AsteroidBelt)]
    [TestCase(2, PlanetType.AsteroidBelt)]
    [TestCase(3, PlanetType.DwarfPlanet)]
    [TestCase(4, PlanetType.Terrestrial)]
    [TestCase(5, PlanetType.Helian)]
    [TestCase(6, PlanetType.Jovian)]
    public void WhenGeneratingPlanetTypeForLStar(int planetTypeRoll, PlanetType expectedPlanetType)
    {
        _mockRollingService.Setup(x => x.D6(1)).Returns(planetTypeRoll);
        _classUnderTest = new RttWorldgenPlanetFactory(_mockRollingService.Object);
        var planet = new RttWorldgenPlanet();

        planet = _classUnderTest.GeneratePlanetType(planet, new RttWorldgenStar {SpectralType = SpectralType.L});

        Assert.That(planet.PlanetType, Is.EqualTo(expectedPlanetType));
    }

    [TestCase(1, 0)]
    [TestCase(4, 0)]
    [TestCase(5, 1)]
    [TestCase(6, 1)]
    public void WhenGeneratingSatellitesForAsteroidBelt(int satelliteRoll, int expectedSatellites)
    {
        _mockRollingService.Setup(x => x.D6(1)).Returns(satelliteRoll);
        _classUnderTest = new RttWorldgenPlanetFactory(_mockRollingService.Object) {
            RttWorldgenPlanet = new RttWorldgenPlanet {
                PlanetType = PlanetType.AsteroidBelt
            }
        };
        var planet = new RttWorldgenPlanet();

        planet = _classUnderTest.GenerateSatellites(planet);

        Assert.That(planet.Satellites.Count, Is.EqualTo(expectedSatellites));
    }

    [TestCase(1, 0)]
    [TestCase(3, 0)]
    [TestCase(5, 0)]
    [TestCase(6, 1)]
    public void WhenGeneratingSatellitesForDwarfPlanet(int satelliteRoll, int expectedSatellites)
    {
        _mockRollingService.Setup(x => x.D6(1)).Returns(satelliteRoll);
        _classUnderTest = new RttWorldgenPlanetFactory(_mockRollingService.Object) {
            RttWorldgenPlanet = new RttWorldgenPlanet {
                PlanetType = PlanetType.DwarfPlanet
            }
        };
        var planet = new RttWorldgenPlanet();

        planet = _classUnderTest.GenerateSatellites(planet);

        Assert.That(planet.Satellites.Count, Is.EqualTo(expectedSatellites));
    }

    [TestCase(1, 0)]
    [TestCase(4, 0)]
    [TestCase(5, 1)]
    [TestCase(6, 1)]
    public void WhenGeneratingSatellitesForTerrestrialPlanet(int satelliteRoll, int expectedSatellites)
    {
        _mockRollingService.Setup(x => x.D6(1)).Returns(satelliteRoll);
        _classUnderTest = new RttWorldgenPlanetFactory(_mockRollingService.Object) {
            RttWorldgenPlanet = new RttWorldgenPlanet {
                PlanetType = PlanetType.Terrestrial
            }
        };
        var planet = new RttWorldgenPlanet();

        planet = _classUnderTest.GenerateSatellites(planet);

        Assert.That(planet.Satellites.Count, Is.EqualTo(expectedSatellites));
    }

    [TestCase(1, 0)]
    [TestCase(3, 0)]
    [TestCase(4, 1)]
    [TestCase(5, 2)]
    [TestCase(6, 3)]
    public void WhenGeneratingSatellitesForHelianPlanet(int satelliteRoll, int expectedSatellites)
    {
        _mockRollingService.SetupSequence(x => x.D6(1))
            .Returns(satelliteRoll);
        _classUnderTest = new RttWorldgenPlanetFactory(_mockRollingService.Object) {
            RttWorldgenPlanet = new RttWorldgenPlanet {
                PlanetType = PlanetType.Helian
            }
        };
        var planet = new RttWorldgenPlanet();

        planet = _classUnderTest.GenerateSatellites(planet);

        Assert.That(planet.Satellites.Count, Is.EqualTo(expectedSatellites));
    }

    [TestCase(1, PlanetType.DwarfPlanet)]
    [TestCase(3, PlanetType.DwarfPlanet)]
    [TestCase(4, PlanetType.DwarfPlanet)]
    [TestCase(5, PlanetType.DwarfPlanet)]
    [TestCase(6, PlanetType.Terrestrial)]
    public void WhenGeneratingSatelliteTypesForHelianPlanet(int satelliteRoll, PlanetType expectedSatelliteType)
    {
        _mockRollingService.SetupSequence(x => x.D6(1))
            .Returns(6)
            .Returns(satelliteRoll);
        _classUnderTest = new RttWorldgenPlanetFactory(_mockRollingService.Object) {
            RttWorldgenPlanet = new RttWorldgenPlanet {
                PlanetType = PlanetType.Helian
            }
        };
        var planet = new RttWorldgenPlanet();

        planet = _classUnderTest.GenerateSatellites(planet);

        Assert.That(planet.Satellites.Count, Is.EqualTo(3));
        Assert.That(planet.Satellites[0].PlanetType, Is.EqualTo(expectedSatelliteType));
        Assert.That(planet.Satellites[1].PlanetType, Is.EqualTo(PlanetType.DwarfPlanet));
        Assert.That(planet.Satellites[2].PlanetType, Is.EqualTo(PlanetType.DwarfPlanet));
    }

    [TestCase(1, 1)]
    [TestCase(2, 2)]
    [TestCase(3, 3)]
    [TestCase(4, 4)]
    [TestCase(5, 5)]
    [TestCase(6, 6)]
    public void WhenGeneratingSatellitesForJovianPlanet(int satelliteNumRoll, int expectedSatellites)
    {
        _mockRollingService.SetupSequence(x => x.D6(1))
            .Returns(satelliteNumRoll);
        _classUnderTest = new RttWorldgenPlanetFactory(_mockRollingService.Object) {
            RttWorldgenPlanet = new RttWorldgenPlanet {
                PlanetType = PlanetType.Jovian
            }
        };
        var planet = new RttWorldgenPlanet();

        planet = _classUnderTest.GenerateSatellites(planet);

        Assert.That(planet.Satellites.Count, Is.EqualTo(expectedSatellites));
    }

    [TestCase(1, PlanetType.Terrestrial)]
    [TestCase(3, PlanetType.Terrestrial)]
    [TestCase(4, PlanetType.Terrestrial)]
    [TestCase(5, PlanetType.Terrestrial)]
    [TestCase(6, PlanetType.Helian)]
    public void WhenGeneratingSatelliteTypesForJovianPlanet(int satelliteRoll, PlanetType expectedSatelliteType)
    {
        _mockRollingService.SetupSequence(x => x.D6(1))
            .Returns(3)
            .Returns(6)
            .Returns(satelliteRoll);
        _classUnderTest = new RttWorldgenPlanetFactory(_mockRollingService.Object) {
            RttWorldgenPlanet = new RttWorldgenPlanet {
                PlanetType = PlanetType.Jovian
            }
        };
        var planet = new RttWorldgenPlanet();

        planet = _classUnderTest.GenerateSatellites(planet);

        Assert.That(planet.Satellites.Count, Is.EqualTo(3));
        Assert.That(planet.Satellites[0].PlanetType, Is.EqualTo(expectedSatelliteType));
        Assert.That(planet.Satellites[1].PlanetType, Is.EqualTo(PlanetType.DwarfPlanet));
        Assert.That(planet.Satellites[2].PlanetType, Is.EqualTo(PlanetType.DwarfPlanet));
    }

    [TestCase(PlanetType.AsteroidBelt, WorldType.AsteroidBelt)]
    [TestCase(PlanetType.DwarfPlanet, WorldType.Stygian)]
    [TestCase(PlanetType.Terrestrial, WorldType.Acheronian)]
    [TestCase(PlanetType.Helian, WorldType.Asphodelian)]
    [TestCase(PlanetType.Jovian, WorldType.Chthonian)]
    public void WhenGeneratingWorldTypeOfDStar(PlanetType planetType, WorldType expectedWorldType)
    {
        _classUnderTest = new RttWorldgenPlanetFactory(_mockRollingService.Object) {
            RttWorldgenPlanet = new RttWorldgenPlanet {
                PlanetType = planetType
            }
        };
        var planet = new RttWorldgenPlanet();

        planet = _classUnderTest.GenerateWorldType(planet, new RttWorldgenStar {SpectralType = SpectralType.D});

        Assert.That(planet.WorldType, Is.EqualTo(expectedWorldType));
    }

    [TestCase(PlanetType.AsteroidBelt, WorldType.AsteroidBelt)]
    [TestCase(PlanetType.DwarfPlanet, WorldType.Stygian)]
    [TestCase(PlanetType.Terrestrial, WorldType.Acheronian)]
    [TestCase(PlanetType.Helian, WorldType.Asphodelian)]
    [TestCase(PlanetType.Jovian, WorldType.Chthonian)]
    public void WhenGeneratingWorldTypeOfIIIStar(PlanetType planetType, WorldType expectedWorldType)
    {
        _classUnderTest = new RttWorldgenPlanetFactory(_mockRollingService.Object) {
            RttWorldgenPlanet = new RttWorldgenPlanet {
                PlanetType = planetType
            }
        };
        var planet = new RttWorldgenPlanet();

        planet = _classUnderTest.GenerateWorldType(planet, new RttWorldgenStar {LuminosityClass = LuminosityClass.III});

        Assert.That(planet.WorldType, Is.EqualTo(expectedWorldType));
    }

    [Test]
    public void WhenGeneratingWorldTypeOfEpistellarAsteroidBelt()
    {
        _classUnderTest = new RttWorldgenPlanetFactory(_mockRollingService.Object) {
            RttWorldgenPlanet = new RttWorldgenPlanet {
                PlanetType = PlanetType.AsteroidBelt,
                PlanetOrbit = PlanetOrbit.Epistellar
            }
        };
        var planet = new RttWorldgenPlanet();

        planet = _classUnderTest.GenerateWorldType(planet, new RttWorldgenStar());

        Assert.That(planet.WorldType, Is.EqualTo(WorldType.AsteroidBelt));
    }

    [TestCase(1, 1, WorldType.Rockball)]
    [TestCase(2, 1, WorldType.Rockball)]
    [TestCase(3, 1, WorldType.Rockball)]
    [TestCase(4, 1, WorldType.Meltball)]
    [TestCase(5, 1, WorldType.Meltball)]
    [TestCase(6, 1, WorldType.Hebean)]
    [TestCase(6, 2, WorldType.Hebean)]
    [TestCase(6, 3, WorldType.Hebean)]
    [TestCase(6, 4, WorldType.Hebean)]
    [TestCase(6, 5, WorldType.Promethean)]
    [TestCase(6, 6, WorldType.Promethean)]
    public void WhenGeneratingWorldTypeOfEpistellarDwarfPlanet(int roll, int roll2, WorldType expectedWorldType)
    {
        _mockRollingService.SetupSequence(x => x.D6(1))
            .Returns(roll)
            .Returns(roll2);
        _classUnderTest = new RttWorldgenPlanetFactory(_mockRollingService.Object) {
            RttWorldgenPlanet = new RttWorldgenPlanet {
                PlanetType = PlanetType.DwarfPlanet,
                PlanetOrbit = PlanetOrbit.Epistellar
            }
        };
        var planet = new RttWorldgenPlanet();

        planet = _classUnderTest.GenerateWorldType(planet, new RttWorldgenStar());

        Assert.That(planet.WorldType, Is.EqualTo(expectedWorldType));
    }

    [TestCase(1, WorldType.JaniLithic)]
    [TestCase(2, WorldType.JaniLithic)]
    [TestCase(3, WorldType.JaniLithic)]
    [TestCase(4, WorldType.JaniLithic)]
    [TestCase(5, WorldType.Vesperian)]
    [TestCase(6, WorldType.Telluric)]
    public void WhenGeneratingWorldTypeOfEpistellarTerrestrialPlanet(int roll, WorldType expectedWorldType)
    {
        _mockRollingService.Setup(x => x.D6(1))
            .Returns(roll);
        _classUnderTest = new RttWorldgenPlanetFactory(_mockRollingService.Object) {
            RttWorldgenPlanet = new RttWorldgenPlanet {
                PlanetType = PlanetType.Terrestrial,
                PlanetOrbit = PlanetOrbit.Epistellar
            }
        };
        var planet = new RttWorldgenPlanet();

        planet = _classUnderTest.GenerateWorldType(planet, new RttWorldgenStar());

        Assert.That(planet.WorldType, Is.EqualTo(expectedWorldType));
    }

    [TestCase(1, WorldType.Helian)]
    [TestCase(2, WorldType.Helian)]
    [TestCase(3, WorldType.Helian)]
    [TestCase(4, WorldType.Helian)]
    [TestCase(5, WorldType.Helian)]
    [TestCase(6, WorldType.Asphodelian)]
    public void WhenGeneratingWorldTypeOfEpistellarHelianPlanet(int roll, WorldType expectedWorldType)
    {
        _mockRollingService.Setup(x => x.D6(1))
            .Returns(roll);
        _classUnderTest = new RttWorldgenPlanetFactory(_mockRollingService.Object) {
            RttWorldgenPlanet = new RttWorldgenPlanet {
                PlanetType = PlanetType.Helian,
                PlanetOrbit = PlanetOrbit.Epistellar
            }
        };
        var planet = new RttWorldgenPlanet();

        planet = _classUnderTest.GenerateWorldType(planet, new RttWorldgenStar());

        Assert.That(planet.WorldType, Is.EqualTo(expectedWorldType));
    }

    [TestCase(1, WorldType.Jovian)]
    [TestCase(2, WorldType.Jovian)]
    [TestCase(3, WorldType.Jovian)]
    [TestCase(4, WorldType.Jovian)]
    [TestCase(5, WorldType.Jovian)]
    [TestCase(6, WorldType.Chthonian)]
    public void WhenGeneratingWorldTypeOfEpistellarJovianPlanet(int roll, WorldType expectedWorldType)
    {
        _mockRollingService.Setup(x => x.D6(1))
            .Returns(roll);
        _classUnderTest = new RttWorldgenPlanetFactory(_mockRollingService.Object) {
            RttWorldgenPlanet = new RttWorldgenPlanet {
                PlanetType = PlanetType.Jovian,
                PlanetOrbit = PlanetOrbit.Epistellar
            }
        };
        var planet = new RttWorldgenPlanet();

        planet = _classUnderTest.GenerateWorldType(planet, new RttWorldgenStar());

        Assert.That(planet.WorldType, Is.EqualTo(expectedWorldType));
    }

    [Test]
    public void WhenGeneratingWorldTypeOfInnerAsteroidBelt()
    {
        _classUnderTest = new RttWorldgenPlanetFactory(_mockRollingService.Object) {
            RttWorldgenPlanet = new RttWorldgenPlanet {
                PlanetType = PlanetType.AsteroidBelt,
                PlanetOrbit = PlanetOrbit.Inner
            }
        };
        var planet = new RttWorldgenPlanet();

        planet = _classUnderTest.GenerateWorldType(planet, new RttWorldgenStar());

        Assert.That(planet.WorldType, Is.EqualTo(WorldType.AsteroidBelt));
    }

    [TestCase(1, WorldType.Rockball)]
    [TestCase(2, WorldType.Rockball)]
    [TestCase(3, WorldType.Rockball)]
    [TestCase(4, WorldType.Rockball)]
    [TestCase(5, WorldType.Arean)]
    [TestCase(6, WorldType.Arean)]
    public void WhenGeneratingWorldTypeOfInnerDwarfPlanet(int roll, WorldType expectedWorldType)
    {
        _mockRollingService.Setup(x => x.D6(1))
            .Returns(roll);
        _classUnderTest = new RttWorldgenPlanetFactory(_mockRollingService.Object) {
            RttWorldgenPlanet = new RttWorldgenPlanet {
                PlanetType = PlanetType.DwarfPlanet,
                PlanetOrbit = PlanetOrbit.Inner
            }
        };
        var planet = new RttWorldgenPlanet();

        planet = _classUnderTest.GenerateWorldType(planet, new RttWorldgenStar());

        Assert.That(planet.WorldType, Is.EqualTo(expectedWorldType));
    }

    [TestCase(1, 1, PlanetType.AsteroidBelt, WorldType.Rockball)]
    [TestCase(6, 1, PlanetType.AsteroidBelt, WorldType.Rockball)]
    [TestCase(5, 1, PlanetType.Helian, WorldType.Arean)]
    [TestCase(6, 1, PlanetType.Helian, WorldType.Meltball)]
    [TestCase(6, 1, PlanetType.Jovian, WorldType.Hebean)]
    [TestCase(6, 4, PlanetType.Jovian, WorldType.Hebean)]
    [TestCase(6, 5, PlanetType.Jovian, WorldType.Promethean)]
    [TestCase(6, 6, PlanetType.Jovian, WorldType.Promethean)]
    public void WhenGeneratingWorldTypeOfInnerDwarfSpecial(int roll, int roll2, PlanetType parentType, WorldType expectedWorldType)
    {
        _mockRollingService.SetupSequence(x => x.D6(1))
            .Returns(roll)
            .Returns(roll2);
        _classUnderTest = new RttWorldgenPlanetFactory(_mockRollingService.Object) {
            RttWorldgenPlanet = new RttWorldgenPlanet {
                PlanetType = PlanetType.DwarfPlanet,
                PlanetOrbit = PlanetOrbit.Inner,
                ParentType = parentType
            }
        };
        var planet = new RttWorldgenPlanet();

        planet = _classUnderTest.GenerateWorldType(planet, new RttWorldgenStar());

        Assert.That(planet.WorldType, Is.EqualTo(expectedWorldType));
    }

    [TestCase(2, WorldType.Telluric)]
    [TestCase(3, WorldType.Telluric)]
    [TestCase(4, WorldType.Telluric)]
    [TestCase(5, WorldType.Arid)]
    [TestCase(6, WorldType.Arid)]
    [TestCase(7, WorldType.Tectonic)]
    [TestCase(8, WorldType.Oceanic)]
    [TestCase(9, WorldType.Oceanic)]
    [TestCase(10, WorldType.Tectonic)]
    [TestCase(11, WorldType.Telluric)]
    [TestCase(12, WorldType.Telluric)]
    public void WhenGeneratingWorldTypeOfInnerTerrestrialPlanet(int roll, WorldType expectedWorldType)
    {
        _mockRollingService.Setup(x => x.D6(2))
            .Returns(roll);
        _classUnderTest = new RttWorldgenPlanetFactory(_mockRollingService.Object) {
            RttWorldgenPlanet = new RttWorldgenPlanet {
                PlanetType = PlanetType.Terrestrial,
                PlanetOrbit = PlanetOrbit.Inner
            }
        };
        var planet = new RttWorldgenPlanet();

        planet = _classUnderTest.GenerateWorldType(planet, new RttWorldgenStar());

        Assert.That(planet.WorldType, Is.EqualTo(expectedWorldType));
    }

    [TestCase(1, WorldType.Helian)]
    [TestCase(2, WorldType.Helian)]
    [TestCase(3, WorldType.Helian)]
    [TestCase(4, WorldType.Helian)]
    [TestCase(5, WorldType.Panthalassic)]
    [TestCase(6, WorldType.Panthalassic)]
    public void WhenGeneratingWorldTypeOfInnerHelianPlanet(int roll, WorldType expectedWorldType)
    {
        _mockRollingService.Setup(x => x.D6(1))
            .Returns(roll);
        _classUnderTest = new RttWorldgenPlanetFactory(_mockRollingService.Object) {
            RttWorldgenPlanet = new RttWorldgenPlanet {
                PlanetType = PlanetType.Helian,
                PlanetOrbit = PlanetOrbit.Inner
            }
        };
        var planet = new RttWorldgenPlanet();

        planet = _classUnderTest.GenerateWorldType(planet, new RttWorldgenStar());

        Assert.That(planet.WorldType, Is.EqualTo(expectedWorldType));
    }

    [Test]
    public void WhenGeneratingWorldTypeOfInnerJovianPlanet()
    {
        _classUnderTest = new RttWorldgenPlanetFactory(_mockRollingService.Object) {
            RttWorldgenPlanet = new RttWorldgenPlanet {
                PlanetType = PlanetType.Jovian,
                PlanetOrbit = PlanetOrbit.Inner
            }
        };
        var planet = new RttWorldgenPlanet();

        planet = _classUnderTest.GenerateWorldType(planet, new RttWorldgenStar());

        Assert.That(planet.WorldType, Is.EqualTo(WorldType.Jovian));
    }

    [Test]
    public void WhenGeneratingWorldTypeOfOuterAsteroidBelt()
    {
        _classUnderTest = new RttWorldgenPlanetFactory(_mockRollingService.Object) {
            RttWorldgenPlanet = new RttWorldgenPlanet {
                PlanetType = PlanetType.AsteroidBelt,
                PlanetOrbit = PlanetOrbit.Outer
            }
        };
        var planet = new RttWorldgenPlanet();

        planet = _classUnderTest.GenerateWorldType(planet, new RttWorldgenStar());

        Assert.That(planet.WorldType, Is.EqualTo(WorldType.AsteroidBelt));
    }

    [TestCase(1, WorldType.Rockball)]
    [TestCase(2, WorldType.Rockball)]
    [TestCase(3, WorldType.Rockball)]
    [TestCase(4, WorldType.Snowball)]
    [TestCase(5, WorldType.Snowball)]
    [TestCase(6, WorldType.Snowball)]
    public void WhenGeneratingWorldTypeOfOuterDwarfPlanet(int roll, WorldType expectedWorldType)
    {
        _mockRollingService.Setup(x => x.D6(1))
            .Returns(roll);
        _classUnderTest = new RttWorldgenPlanetFactory(_mockRollingService.Object) {
            RttWorldgenPlanet = new RttWorldgenPlanet {
                PlanetType = PlanetType.DwarfPlanet,
                PlanetOrbit = PlanetOrbit.Outer
            }
        };
        var planet = new RttWorldgenPlanet();

        planet = _classUnderTest.GenerateWorldType(planet, new RttWorldgenStar());

        Assert.That(planet.WorldType, Is.EqualTo(expectedWorldType));
    }

    [TestCase(1, 1, PlanetType.AsteroidBelt, WorldType.Rockball)]
    [TestCase(4, 1, PlanetType.AsteroidBelt, WorldType.Rockball)]
    [TestCase(5, 1, PlanetType.AsteroidBelt, WorldType.Snowball)]
    [TestCase(6, 1, PlanetType.AsteroidBelt, WorldType.Snowball)]
    [TestCase(1, 1, PlanetType.Helian, WorldType.Rockball)]
    [TestCase(2, 1, PlanetType.Helian, WorldType.Rockball)]
    [TestCase(3, 1, PlanetType.Helian, WorldType.Snowball)]
    [TestCase(5, 1, PlanetType.Helian, WorldType.Snowball)]
    [TestCase(6, 1, PlanetType.Helian, WorldType.Meltball)]
    [TestCase(1, 1, PlanetType.Jovian, WorldType.Rockball)]
    [TestCase(2, 1, PlanetType.Jovian, WorldType.Snowball)]
    [TestCase(4, 1, PlanetType.Jovian, WorldType.Snowball)]
    [TestCase(5, 1, PlanetType.Jovian, WorldType.Meltball)]
    [TestCase(6, 1, PlanetType.Jovian, WorldType.Hebean)]
    [TestCase(6, 3, PlanetType.Jovian, WorldType.Hebean)]
    [TestCase(6, 4, PlanetType.Jovian, WorldType.Arean)]
    [TestCase(6, 5, PlanetType.Jovian, WorldType.Arean)]
    [TestCase(6, 6, PlanetType.Jovian, WorldType.Promethean)]
    public void WhenGeneratingWorldTypeOfOuterDwarfSpecial(int roll, int roll2, PlanetType parentType, WorldType expectedWorldType)
    {
        _mockRollingService.SetupSequence(x => x.D6(1))
            .Returns(roll)
            .Returns(roll2);
        _classUnderTest = new RttWorldgenPlanetFactory(_mockRollingService.Object) {
            RttWorldgenPlanet = new RttWorldgenPlanet {
                PlanetType = PlanetType.DwarfPlanet,
                PlanetOrbit = PlanetOrbit.Outer,
                ParentType = parentType
            }
        };
        var planet = new RttWorldgenPlanet();

        planet = _classUnderTest.GenerateWorldType(planet, new RttWorldgenStar());

        Assert.That(planet.WorldType, Is.EqualTo(expectedWorldType));
    }

    [TestCase(1, WorldType.Arid)]
    [TestCase(2, WorldType.Arid)]
    [TestCase(3, WorldType.Arid)]
    [TestCase(4, WorldType.Arid)]
    [TestCase(5, WorldType.Tectonic)]
    [TestCase(6, WorldType.Tectonic)]
    public void WhenGeneratingWorldTypeOfOuterTerrestrialPlanet(int roll, WorldType expectedWorldType)
    {
        _mockRollingService.Setup(x => x.D6(1))
            .Returns(roll);
        _classUnderTest = new RttWorldgenPlanetFactory(_mockRollingService.Object) {
            RttWorldgenPlanet = new RttWorldgenPlanet {
                PlanetType = PlanetType.Terrestrial,
                PlanetOrbit = PlanetOrbit.Outer
            }
        };
        var planet = new RttWorldgenPlanet();

        planet = _classUnderTest.GenerateWorldType(planet, new RttWorldgenStar());

        Assert.That(planet.WorldType, Is.EqualTo(expectedWorldType));
    }

    [TestCase(1, WorldType.Arid)]
    [TestCase(2, WorldType.Arid)]
    [TestCase(3, WorldType.Tectonic)]
    [TestCase(4, WorldType.Tectonic)]
    [TestCase(5, WorldType.Oceanic)]
    [TestCase(6, WorldType.Oceanic)]
    public void WhenGeneratingWorldTypeOfOuterTerrestrialSpecial(int roll, WorldType expectedWorldType)
    {
        _mockRollingService.Setup(x => x.D6(1))
            .Returns(roll);
        _classUnderTest = new RttWorldgenPlanetFactory(_mockRollingService.Object) {
            RttWorldgenPlanet = new RttWorldgenPlanet {
                PlanetType = PlanetType.Terrestrial,
                PlanetOrbit = PlanetOrbit.Outer,
                ParentId = Guid.NewGuid()
            }
        };
        var planet = new RttWorldgenPlanet();

        planet = _classUnderTest.GenerateWorldType(planet, new RttWorldgenStar());

        Assert.That(planet.WorldType, Is.EqualTo(expectedWorldType));
    }

    [Test]
    public void WhenGeneratingWorldTypeOfOuterHelianPlanet()
    {
        _classUnderTest = new RttWorldgenPlanetFactory(_mockRollingService.Object) {
            RttWorldgenPlanet = new RttWorldgenPlanet {
                PlanetType = PlanetType.Helian,
                PlanetOrbit = PlanetOrbit.Outer
            }
        };
        var planet = new RttWorldgenPlanet();

        planet = _classUnderTest.GenerateWorldType(planet, new RttWorldgenStar());

        Assert.That(planet.WorldType, Is.EqualTo(WorldType.Helian));
    }

    [Test]
    public void WhenGeneratingWorldTypeOfOuterJovianPlanet()
    {
        _classUnderTest = new RttWorldgenPlanetFactory(_mockRollingService.Object) {
            RttWorldgenPlanet = new RttWorldgenPlanet {
                PlanetType = PlanetType.Jovian,
                PlanetOrbit = PlanetOrbit.Outer
            }
        };
        var planet = new RttWorldgenPlanet();

        planet = _classUnderTest.GenerateWorldType(planet, new RttWorldgenStar());

        Assert.That(planet.WorldType, Is.EqualTo(WorldType.Jovian));
    }
}