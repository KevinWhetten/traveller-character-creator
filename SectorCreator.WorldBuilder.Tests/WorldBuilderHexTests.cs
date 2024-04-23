using Moq;
using SectorCreator.Global;
using SectorCreator.Global.Enums;
using SectorCreator.WorldBuilder.Enums;
using SectorCreator.WorldBuilder.Planet;
using SectorCreator.WorldBuilder.Planet.AsteroidBelt;
using SectorCreator.WorldBuilder.Planet.GasGiant;
using SectorCreator.WorldBuilder.Planet.Moon;
using SectorCreator.WorldBuilder.Planet.Planet;
using SectorCreator.WorldBuilder.Planet.TerrestrialPlanet;
using SectorCreator.WorldBuilder.Star;
using WorldBuilderMoon = SectorCreator.WorldBuilder.Planet.Moon.WorldBuilderMoon;

namespace SectorCreator.WorldBuilder.Tests;

public class WorldBuilderHexTests
{
    Mock<IRollingService> _mockRollingService = new();

    private WorldBuilderHex _worldBuilderHex = new() {
        MainSystem = new WorldBuilderStarSystem(new RollingService()) {
            Star = new WorldBuilderStar { }
        }
    };

    private WorldBuilderHex _zeddHex = new() {
        GasGiantQuantity = 4,
        BeltQuantity = 2,
        TerrestrialPlanetQuantity = 12,
        MainSystem = new WorldBuilderStarSystem(new RollingService()) {
            Star = new WorldBuilderStar {
                Age = 6.336,
                Component = "Aa",
                AvailableOrbits = { 0.61, 7.10, 14.10, 20 },
                StarType = StarType.Primary,
                SpectralType = SpectralType.G,
                SpectralSubclass = 7,
                LuminosityClass = LuminosityClass.V,
                Mass = .929,
                Temperature = 5440,
                Diameter = 0.967,
                OrbitNumber = 0,
                HZCO = 3.3,
                CompanionStar = new WorldBuilderStar {
                    Component = "Ab",
                    StarType = StarType.Companion,
                    SpectralType = SpectralType.G,
                    SpectralSubclass = 8,
                    LuminosityClass = LuminosityClass.V,
                    Mass = .907,
                    Temperature = 5360,
                    Diameter = 0.957,
                    OrbitNumber = 0.09,
                    Eccentricity = 0.11
                }
            },
            Planets = new List<WorldBuilderPlanet> {
                new WorldBuilderTerrestrialPlanet {
                    Primary = "Aab",
                    OrbitNumber = 1,
                    Eccentricity = .2,
                    Period = .187,
                    Size = 11,
                    Moons = new List<WorldBuilderMoon> {
                        new WorldBuilderRings {
                            Size = 25
                        }
                    }
                },
                new WorldBuilderTerrestrialPlanet {
                    Primary = "Aab",
                    OrbitNumber = 1.6,
                    Eccentricity = .004,
                    Period = .326,
                    Size = 6,
                    Moons = new List<WorldBuilderMoon> {
                        new() {
                            Size = 1
                        },
                        new() {
                            Size = 26
                        }
                    }
                },
                new WorldBuilderTerrestrialPlanet {
                    Primary = "Aab",
                    OrbitNumber = 2.1,
                    Eccentricity = .06,
                    Period = .460,
                    Size = 7,
                    Moons = new List<WorldBuilderMoon>()
                },
                new WorldBuilderAsteroidBelt {
                    Primary = "Aab",
                    OrbitNumber = 2.7,
                    Eccentricity = 0,
                    Period = .641,
                    Size = 0,
                    Moons = new List<WorldBuilderMoon>()
                },
                new WorldBuilderGasGiantPlanet {
                    Primary = "Aab",
                    OrbitNumber = 3.1,
                    Eccentricity = .10,
                    Period = .805,
                    Size = 18,
                    Diameter = 14 * 12742,
                    Density = 0.43731778425655976676384839650146,
                    // Mass = 1200,
                    Moons = new List<WorldBuilderMoon> {
                        new() {
                            Size = 2
                        },
                        new() {
                            Size = 26
                        },
                        new() {
                            Size = 26
                        },
                        new() {
                            Size = 5
                        },
                        new() {
                            Size = 26
                        }
                    }
                },
                new WorldBuilderGasGiantPlanet {
                    Primary = "Aab",
                    OrbitNumber = 3.5,
                    Eccentricity = .002,
                    Period = 1.094,
                    Size = 18,
                    Diameter = 12 * 12742,
                    Density = 0.46296296296296296296296296296296,
                    // Mass = 800,
                    Moons = new List<WorldBuilderMoon> {
                        new() {
                            Size = 26
                        },
                        new() {
                            Size = 10
                        },
                        new() {
                            Size = 1
                        },
                        new() {
                            Size = 3
                        },
                        new() {
                            Size = 26
                        },
                        new() {
                            Size = 26
                        }
                    }
                },
                new WorldBuilderTerrestrialPlanet {
                    Primary = "Aab",
                    OrbitNumber = 4.1,
                    Eccentricity = .15,
                    Period = 1.665,
                    Size = 10,
                    Moons = new List<WorldBuilderMoon> {
                        new WorldBuilderRings {
                            Size = 25
                        },
                        new WorldBuilderRings {
                            Size = 25
                        },
                        new() {
                            Size = 26
                        },
                        new() {
                            Size = 1
                        },
                        new() {
                            Size = 1
                        }
                    }
                },
                new WorldBuilderTerrestrialPlanet {
                    Primary = "Aab",
                    OrbitNumber = 4.6,
                    Eccentricity = .015,
                    Period = 2.608,
                    Size = 8,
                    Moons = new List<WorldBuilderMoon>()
                },
                new WorldBuilderTerrestrialPlanet {
                    Primary = "Aab",
                    OrbitNumber = 5.2,
                    Eccentricity = 0.10,
                    Period = 4.384,
                    Anomaly = PlanetAnomaly.Retrograde,
                    Size = 1,
                    Moons = new List<WorldBuilderMoon>()
                },
                new WorldBuilderTerrestrialPlanet {
                    Primary = "AB",
                    OrbitNumber = 7.2,
                    Eccentricity = .015,
                    Period = 26493,
                    Size = 6,
                    Moons = new List<WorldBuilderMoon>()
                },
                new WorldBuilderTerrestrialPlanet {
                    Primary = "AB",
                    OrbitNumber = 7.8,
                    Eccentricity = .030,
                    Period = 48.670,
                    Size = 3,
                    Moons = new List<WorldBuilderMoon>()
                },
                new WorldBuilderGasGiantPlanet {
                    Primary = "AB",
                    OrbitNumber = 8.3,
                    Eccentricity = .09,
                    Period = 84.492,
                    Size = 17,
                    Diameter = 11 * 12742,
                    Density = 0.13523666416228399699474079639369,
                    // Mass = 180,
                    Moons = new List<WorldBuilderMoon> {
                        new() {
                            Size = 2
                        },
                        new() {
                            Size = 26
                        },
                        new() {
                            Size = 2
                        },
                        new() {
                            Size = 26
                        },
                        new() {
                            Size = 1
                        },
                        new() {
                            Size = 1
                        }
                    }
                }
            }
        },
        SecondarySystems = {
            new WorldBuilderStarSystem(new RollingService()) {
                Star = new WorldBuilderStar {
                    Component = "B",
                    AvailableOrbits = { 0.02, 20 },
                    StarType = StarType.Near,
                    SpectralType = SpectralType.K,
                    SpectralSubclass = 8,
                    LuminosityClass = LuminosityClass.V,
                    Mass = .626,
                    Temperature = 3980,
                    Diameter = 0.777,
                    OrbitNumber = 6.1,
                    Eccentricity = 0.08,
                    HZCO = 0.92
                },
                Planets = new List<WorldBuilderPlanet> {
                    new WorldBuilderTerrestrialPlanet {
                        Primary = "B",
                        OrbitNumber = .52,
                        Eccentricity = .003,
                        Period = .12,
                        Size = 9,
                        Moons = new List<WorldBuilderMoon>()
                    },
                    new WorldBuilderTerrestrialPlanet {
                        Primary = "B",
                        OrbitNumber = 1.0,
                        Eccentricity = .07,
                        Period = .249,
                        Size = 8,
                        Moons = new List<WorldBuilderMoon>()
                    }
                }
            },
            new WorldBuilderStarSystem(new RollingService()) {
                Star = new WorldBuilderStar {
                    Component = "Ca",
                    AvailableOrbits = { 0.74, 20 },
                    StarType = StarType.Far,
                    SpectralType = SpectralType.M,
                    SpectralSubclass = 0,
                    LuminosityClass = LuminosityClass.V,
                    Mass = .510,
                    Temperature = 3700,
                    Diameter = 0.728,
                    OrbitNumber = 12.1,
                    Eccentricity = 0.47,
                    HZCO = 0.75,
                    CompanionStar = new WorldBuilderStar {
                        Component = "Cb",
                        StarType = StarType.Companion,
                        SpectralType = SpectralType.D,
                        SpectralSubclass = 0,
                        LuminosityClass = LuminosityClass.D,
                        Mass = .490,
                        Temperature = 6700,
                        Diameter = 0.017,
                        OrbitNumber = 0.21,
                        Eccentricity = 0.24
                    }
                },
                Planets = new List<WorldBuilderPlanet> {
                    new WorldBuilderAsteroidBelt {
                        Primary = "Cab",
                        OrbitNumber = 1.4,
                        Eccentricity = 0,
                        Period = .369,
                        Size = 0,
                        Moons = new List<WorldBuilderMoon>()
                    },
                    new WorldBuilderGasGiantPlanet {
                        Primary = "Cab",
                        OrbitNumber = 2.3,
                        Eccentricity = .03,
                        Period = .692,
                        Size = 16,
                        Diameter = 4 * 12742,
                        Density = 0.15625,
                        // Mass = 10,
                        Moons = new List<WorldBuilderMoon> {
                            new WorldBuilderRings {
                                Size = 25
                            },
                            new WorldBuilderRings {
                                Size = 25
                            },
                            new() {
                                Size = 1
                            },
                            new() {
                                Size = 26
                            },
                            new() {
                                Size = 2
                            },
                            new() {
                                Size = 2
                            }
                        }
                    },
                    new WorldBuilderTerrestrialPlanet {
                        Primary = "Cab",
                        OrbitNumber = 2.9,
                        Eccentricity = 0.005,
                        Period = .941,
                        Size = 4,
                        Moons = new List<WorldBuilderMoon>()
                    },
                    new WorldBuilderTerrestrialPlanet {
                        Primary = "Cab",
                        OrbitNumber = 3.3,
                        Eccentricity = 0.015,
                        Period = 1.263,
                        Size = 10,
                        Moons = new List<WorldBuilderMoon> {
                            new WorldBuilderRings {
                                Size = 25
                            }
                        }
                    }
                }
            }
        }
    };

    private WorldBuilderHex _corellaHex = new() {
        MainSystem = new WorldBuilderStarSystem(new RollingService()) {
            Star = new WorldBuilderStar {
                Age = 6.336,
                StarType = StarType.Primary,
                SpectralType = SpectralType.G,
                SpectralSubclass = 2,
                LuminosityClass = LuminosityClass.V,
                Mass = 1.224,
                Temperature = 5840,
                Diameter = 0.998,
                OrbitNumber = 0,
                CompanionStar = new WorldBuilderStar {
                    StarType = StarType.Companion,
                    SpectralType = SpectralType.G,
                    SpectralSubclass = 8,
                    LuminosityClass = LuminosityClass.V,
                    Mass = .974,
                    Temperature = 5360,
                    Diameter = 0.957,
                    OrbitNumber = 0.30,
                    Eccentricity = 0.10
                }
            }
        }
    };

    [Test]
    public void WhenGettingStellarDataAboutZedSystem()
    {
        Console.Write(_zeddHex.GetStarDetails());
    }

    [Test]
    public void WhenGettingPlanetDataAboutZedSystem()
    {
        Console.Write(_zeddHex.GetPlanetDetails());
    }

    [Test]
    public void WhenGettingStellarDataAboutCorellaSystem()
    {
        var starString = _corellaHex.GetStarDetails();
        int i = 0;
    }

    [TestCase(2, true)]
    [TestCase(3, true)]
    [TestCase(4, true)]
    [TestCase(5, true)]
    [TestCase(6, true)]
    [TestCase(7, true)]
    [TestCase(8, true)]
    [TestCase(9, true)]
    [TestCase(10, false)]
    [TestCase(11, false)]
    [TestCase(12, false)]
    public void WhenGeneratingGasGiants(int roll, bool gasGiantExists)
    {
        _mockRollingService.Setup(x => x.D6(2)).Returns(roll);

        _worldBuilderHex = new WorldBuilderHex(_mockRollingService.Object, new Coordinates(), false) {
            MainSystem = new WorldBuilderStarSystem(_mockRollingService.Object) {
                Star = new WorldBuilderStar { }
            }
        };

        _worldBuilderHex.GenerateGasGiantQuantity();

        Assert.That(_worldBuilderHex.GasGiantQuantity > 0, Is.EqualTo(gasGiantExists));
    }

    [TestCase(2, 1)]
    [TestCase(3, 1)]
    [TestCase(4, 2)]
    [TestCase(5, 2)]
    [TestCase(6, 3)]
    [TestCase(7, 3)]
    [TestCase(8, 4)]
    [TestCase(9, 4)]
    [TestCase(10, 4)]
    [TestCase(11, 5)]
    [TestCase(12, 6)]
    public void WhenGeneratingGasGiantsWithSingleClassVStar(int roll, int expectedGasGiantCount)
    {
        _mockRollingService.SetupSequence(x => x.D6(2)).Returns(7).Returns(roll);

        _worldBuilderHex = new WorldBuilderHex(_mockRollingService.Object, new Coordinates(), false) {
            MainSystem = new WorldBuilderStarSystem(_mockRollingService.Object) {
                Star = new WorldBuilderStar {LuminosityClass = LuminosityClass.V}
            },
            GasGiantQuantity = 0
        };

        _worldBuilderHex.GenerateGasGiantQuantity();

        Assert.That(_worldBuilderHex.GasGiantQuantity, Is.EqualTo(expectedGasGiantCount));
    }

    [TestCase(2, 1)]
    [TestCase(3, 1)]
    [TestCase(4, 1)]
    [TestCase(5, 1)]
    [TestCase(6, 1)]
    [TestCase(7, 2)]
    [TestCase(8, 2)]
    [TestCase(9, 3)]
    [TestCase(10, 3)]
    [TestCase(11, 4)]
    [TestCase(12, 4)]
    public void WhenGeneratingGasGiantsWithAPrimaryBrownDwarfStar(int roll, int expectedGasGiantCount)
    {
        _mockRollingService.SetupSequence(x => x.D6(2)).Returns(7).Returns(roll);

        _worldBuilderHex = new WorldBuilderHex(_mockRollingService.Object, new Coordinates(), false) {
            MainSystem = new WorldBuilderStarSystem(_mockRollingService.Object) {
                Star = new WorldBuilderStar {SpectralType = SpectralType.BD}
            },
            GasGiantQuantity = 0
        };

        _worldBuilderHex.GenerateGasGiantQuantity();

        Assert.That(_worldBuilderHex.GasGiantQuantity, Is.EqualTo(expectedGasGiantCount));
    }

    [TestCase(2, false)]
    [TestCase(3, false)]
    [TestCase(4, false)]
    [TestCase(5, false)]
    [TestCase(6, false)]
    [TestCase(7, false)]
    [TestCase(8, true)]
    [TestCase(9, true)]
    [TestCase(10, true)]
    [TestCase(11, true)]
    [TestCase(12, true)]
    public void WhenGeneratingBelts(int roll, bool beltExists)
    {
        _mockRollingService.Setup(x => x.D6(2)).Returns(roll);

        _worldBuilderHex = new WorldBuilderHex(_mockRollingService.Object, new Coordinates(), false) {
            MainSystem = new WorldBuilderStarSystem(_mockRollingService.Object) {
                Star = new WorldBuilderStar { }
            }
        };

        _worldBuilderHex.GenerateBeltQuantity();

        Assert.That(_worldBuilderHex.BeltQuantity > 0, Is.EqualTo(beltExists));
    }

    [TestCase(2, 1)]
    [TestCase(3, 1)]
    [TestCase(4, 1)]
    [TestCase(5, 1)]
    [TestCase(6, 2)]
    [TestCase(7, 2)]
    [TestCase(8, 2)]
    [TestCase(9, 2)]
    [TestCase(10, 2)]
    [TestCase(11, 3)]
    [TestCase(12, 3)]
    public void WhenGeneratingBeltsAndGasGiantsArePresent(int roll, int beltsPresent)
    {
        _mockRollingService.SetupSequence(x => x.D6(2)).Returns(9).Returns(roll);

        _worldBuilderHex = new WorldBuilderHex(_mockRollingService.Object, new Coordinates(), false) {
            MainSystem = new WorldBuilderStarSystem(_mockRollingService.Object) {
                Star = new WorldBuilderStar { }
            },
            GasGiantQuantity = 2
        };

        _worldBuilderHex.GenerateBeltQuantity();

        Assert.That(_worldBuilderHex.BeltQuantity, Is.EqualTo(beltsPresent));
    }

    [TestCase(2, 1)]
    [TestCase(3, 1)]
    [TestCase(4, 2)]
    [TestCase(5, 2)]
    [TestCase(6, 2)]
    [TestCase(7, 2)]
    [TestCase(8, 2)]
    [TestCase(9, 3)]
    [TestCase(10, 3)]
    [TestCase(11, 3)]
    [TestCase(12, 3)]
    public void WhenGeneratingBeltsAndPrimaryStarIsProtostar(int roll, int beltsPresent)
    {
        _mockRollingService.SetupSequence(x => x.D6(2)).Returns(9).Returns(roll);

        _worldBuilderHex = new WorldBuilderHex(_mockRollingService.Object, new Coordinates(), false) {
            MainSystem = new WorldBuilderStarSystem(_mockRollingService.Object) {
                Star = new WorldBuilderStar {SpecialType = StarSpecialType.Protostar}
            }
        };

        _worldBuilderHex.GenerateBeltQuantity();

        Assert.That(_worldBuilderHex.BeltQuantity, Is.EqualTo(beltsPresent));
    }

    [TestCase(2, 1)]
    [TestCase(3, 1)]
    [TestCase(4, 1)]
    [TestCase(5, 1)]
    [TestCase(6, 2)]
    [TestCase(7, 2)]
    [TestCase(8, 2)]
    [TestCase(9, 2)]
    [TestCase(10, 2)]
    [TestCase(11, 3)]
    [TestCase(12, 3)]
    public void WhenGeneratingBeltsAndPrimaryStarIsPostStellarObject(int roll, int beltsPresent)
    {
        _mockRollingService.SetupSequence(x => x.D6(2)).Returns(9).Returns(roll);

        _worldBuilderHex = new WorldBuilderHex(_mockRollingService.Object, new Coordinates(), false) {
            MainSystem = new WorldBuilderStarSystem(_mockRollingService.Object) {
                Star = new WorldBuilderStar {SpectralType = SpectralType.D}
            }
        };

        _worldBuilderHex.GenerateBeltQuantity();

        Assert.That(_worldBuilderHex.BeltQuantity, Is.EqualTo(beltsPresent));
    }

    [TestCase(2, 1)]
    [TestCase(3, 1)]
    [TestCase(4, 1)]
    [TestCase(5, 1)]
    [TestCase(6, 2)]
    [TestCase(7, 2)]
    [TestCase(8, 2)]
    [TestCase(9, 2)]
    [TestCase(10, 2)]
    [TestCase(11, 3)]
    [TestCase(12, 3)]
    public void WhenGeneratingBeltsAndHasMultipleStars(int roll, int beltsPresent)
    {
        _mockRollingService.SetupSequence(x => x.D6(2)).Returns(9).Returns(roll);

        _worldBuilderHex = new WorldBuilderHex(_mockRollingService.Object, new Coordinates(), false) {
            MainSystem = new WorldBuilderStarSystem(_mockRollingService.Object) {
                Star = new WorldBuilderStar {
                    CompanionStar = new WorldBuilderStar { }
                }
            }
        };

        _worldBuilderHex.GenerateBeltQuantity();

        Assert.That(_worldBuilderHex.BeltQuantity, Is.EqualTo(beltsPresent));
    }

    [TestCase(2, 1, 3)]
    [TestCase(2, 2, 4)]
    [TestCase(2, 3, 5)]
    [TestCase(3, 1, 3)]
    [TestCase(3, 2, 4)]
    [TestCase(3, 3, 5)]
    [TestCase(4, 1, 3)]
    [TestCase(4, 2, 4)]
    [TestCase(4, 3, 5)]
    [TestCase(5, 1, 3)]
    [TestCase(5, 2, 4)]
    [TestCase(5, 3, 5)]
    [TestCase(6, 1, 4)]
    [TestCase(6, 2, 5)]
    [TestCase(6, 3, 6)]
    [TestCase(7, 1, 5)]
    [TestCase(7, 2, 6)]
    [TestCase(7, 3, 7)]
    [TestCase(8, 1, 6)]
    [TestCase(8, 2, 7)]
    [TestCase(8, 3, 8)]
    [TestCase(9, 1, 7)]
    [TestCase(9, 2, 8)]
    [TestCase(9, 3, 9)]
    [TestCase(10, 1, 8)]
    [TestCase(10, 2, 9)]
    [TestCase(10, 3, 10)]
    [TestCase(11, 1, 9)]
    [TestCase(11, 2, 10)]
    [TestCase(11, 3, 11)]
    [TestCase(12, 1, 10)]
    [TestCase(12, 2, 11)]
    [TestCase(12, 3, 12)]
    public void WhenGeneratingTerrestrialPlanets(int roll1, int roll2, int terrestrialPresent)
    {
        _mockRollingService.Setup(x => x.D6(2)).Returns(roll1);
        _mockRollingService.Setup(x => x.D3(1)).Returns(roll2);

        _worldBuilderHex = new WorldBuilderHex(_mockRollingService.Object, new Coordinates(), false) {
            MainSystem = new WorldBuilderStarSystem(_mockRollingService.Object) {
                Star = new WorldBuilderStar { }
            }
        };

        _worldBuilderHex.GenerateTerrestrialPlanetQuantity();

        Assert.That(_worldBuilderHex.TerrestrialPlanetQuantity, Is.EqualTo(terrestrialPresent));
    }
}