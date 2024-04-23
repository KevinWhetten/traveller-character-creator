using System;
using Moq;
using NUnit.Framework;
using SectorCreator.Global;
using SectorCreator.Global.Enums;
using SectorCreator.Models.RTTWorldgen;
using SectorCreator.Models.RTTWorldgen.Factories;

namespace SectorCreator.Models.Tests.Factories.RttWorldgen;

[TestFixture]
public class RttWorldgenStarSystemFactoryTests
{
    private RttWorldgenStarSystemFactory _classUnderTest;
    private readonly Mock<IRollingService> _mockRollingService = new();
    private readonly Mock<IRttWorldgenStarFactory> _mockRttWorldgenStarFactory = new();
    private readonly Mock<IRttWorldgenPlanetFactory> _mockRttWorldgenPlanetFactory = new();

    [TestCase(3, 0)]
    [TestCase(10, 0)]
    [TestCase(11, 1)]
    [TestCase(15, 1)]
    [TestCase(16, 2)]
    [TestCase(18, 2)]
    public void WhenGeneratingNumberOfStars(int starNumRoll, int expectedStarNum)
    {
        _mockRollingService.Setup(x => x.D6(3)).Returns(starNumRoll);
        _classUnderTest = new RttWorldgenStarSystemFactory(_mockRollingService.Object,
            _mockRttWorldgenStarFactory.Object, _mockRttWorldgenPlanetFactory.Object);

        var result = _classUnderTest.Generate(StarSystemType.Regular, new Coordinates());

        Assert.That(result.CompanionStars.Count, Is.EqualTo(expectedStarNum));
    }

    [TestCase(1, 0)]
    [TestCase(3, 0)]
    [TestCase(4, 1)]
    [TestCase(5, 2)]
    [TestCase(6, 2)]
    public void WhenAddingNumberOfEpistellarOrbits(int orbitRoll, int expectedOrbitNum)
    {
        var spectralRoll = 0;
        _mockRollingService.SetupSequence(x => x.D6(1)).Returns(orbitRoll);
        _mockRttWorldgenStarFactory.Setup(x => x.Generate(out spectralRoll))
            .Returns(new RttWorldgenStar());
        _mockRttWorldgenStarFactory.Setup(x => x.Generate(It.IsAny<int>()))
            .Returns(new RttWorldgenStar());
        _mockRttWorldgenPlanetFactory.Setup(x =>
                x.GenerateRttWorldgenPlanet(It.IsAny<RttWorldgenStar>(), It.IsAny<PlanetOrbit>(), It.IsAny<int>(), It.IsAny<Coordinates>()))
            .Returns(new RttWorldgenPlanet());
        _classUnderTest = new RttWorldgenStarSystemFactory(_mockRollingService.Object,
            _mockRttWorldgenStarFactory.Object, _mockRttWorldgenPlanetFactory.Object);

        var result = _classUnderTest.Generate(StarSystemType.Regular, new Coordinates());

        Assert.That(result.Planets.Count, Is.EqualTo(expectedOrbitNum));
    }

    [TestCase(4, 0)]
    [TestCase(5, 1)]
    [TestCase(6, 2)]
    public void WhenAddingNumberOfEpistellarOrbitsForMVStar(int orbitRoll, int expectedOrbitNum)
    {
        var spectralRoll = 0;
        _mockRollingService.SetupSequence(x => x.D6(1)).Returns(orbitRoll);
        _mockRttWorldgenStarFactory.Setup(x => x.Generate(out spectralRoll))
            .Returns(new RttWorldgenStar {SpectralType = SpectralType.M, LuminosityClass = LuminosityClass.V});
        _mockRttWorldgenStarFactory.Setup(x => x.Generate(It.IsAny<int>()))
            .Returns(new RttWorldgenStar {SpectralType = SpectralType.M, LuminosityClass = LuminosityClass.V});
        _mockRttWorldgenPlanetFactory.Setup(x =>
                x.GenerateRttWorldgenPlanet(It.IsAny<RttWorldgenStar>(), It.IsAny<PlanetOrbit>(), It.IsAny<int>(), It.IsAny<Coordinates>()))
            .Returns(new RttWorldgenPlanet());
        _classUnderTest = new RttWorldgenStarSystemFactory(_mockRollingService.Object,
            _mockRttWorldgenStarFactory.Object, _mockRttWorldgenPlanetFactory.Object);

        var result = _classUnderTest.Generate(StarSystemType.Regular, new Coordinates());

        Assert.That(result.Planets.Count, Is.EqualTo(expectedOrbitNum));
    }

    [TestCase(SpectralType.D, LuminosityClass.V, 6, 0)]
    [TestCase(SpectralType.L, LuminosityClass.V, 6, 0)]
    [TestCase(SpectralType.A, LuminosityClass.III, 6, 0)]
    public void WhenAddingNumberOfEpistellarOrbitsForDLAndIIIStar(SpectralType spectralType, LuminosityClass luminosityClass,
        int orbitRoll, int expectedOrbitNum)
    {
        var spectralRoll = 0;
        _mockRollingService.SetupSequence(x => x.D6(1)).Returns(orbitRoll);
        _mockRttWorldgenStarFactory.Setup(x => x.Generate(out spectralRoll))
            .Returns(new RttWorldgenStar {SpectralType = spectralType, LuminosityClass = luminosityClass});
        _mockRttWorldgenStarFactory.Setup(x => x.Generate(It.IsAny<int>()))
            .Returns(new RttWorldgenStar {SpectralType = spectralType, LuminosityClass = luminosityClass});
        _mockRttWorldgenPlanetFactory.Setup(x =>
                x.GenerateRttWorldgenPlanet(It.IsAny<RttWorldgenStar>(), It.IsAny<PlanetOrbit>(), It.IsAny<int>(), It.IsAny<Coordinates>()))
            .Returns(new RttWorldgenPlanet());
        _classUnderTest = new RttWorldgenStarSystemFactory(_mockRollingService.Object,
            _mockRttWorldgenStarFactory.Object, _mockRttWorldgenPlanetFactory.Object);

        var result = _classUnderTest.Generate(StarSystemType.Regular, new Coordinates());

        Assert.That(result.Planets.Count, Is.EqualTo(expectedOrbitNum));
    }

    [TestCase(1, 0)]
    [TestCase(2, 1)]
    [TestCase(3, 2)]
    [TestCase(4, 3)]
    [TestCase(5, 4)]
    [TestCase(6, 5)]
    public void WhenAddingNumberOfInnerOrbits(int orbitRoll, int expectedOrbitNum)
    {
        var spectralRoll = 0;
        _mockRollingService.SetupSequence(x => x.D6(1))
            .Returns(0)
            .Returns(orbitRoll);
        _mockRollingService.SetupSequence(x => x.D3(1))
            .Returns(orbitRoll / 2);
        _mockRttWorldgenStarFactory.Setup(x => x.Generate(out spectralRoll))
            .Returns(new RttWorldgenStar());
        _mockRttWorldgenStarFactory.Setup(x => x.Generate(It.IsAny<int>()))
            .Returns(new RttWorldgenStar());
        _mockRttWorldgenPlanetFactory.Setup(x =>
                x.GenerateRttWorldgenPlanet(It.IsAny<RttWorldgenStar>(), It.IsAny<PlanetOrbit>(), It.IsAny<int>(), It.IsAny<Coordinates>()))
            .Returns(new RttWorldgenPlanet());
        _classUnderTest = new RttWorldgenStarSystemFactory(_mockRollingService.Object,
            _mockRttWorldgenStarFactory.Object, _mockRttWorldgenPlanetFactory.Object);

        var result = _classUnderTest.Generate(StarSystemType.Regular, new Coordinates());

        Assert.That(result.Planets.Count, Is.EqualTo(expectedOrbitNum));
    }


    [TestCase(2, 0)]
    [TestCase(3, 1)]
    [TestCase(4, 2)]
    [TestCase(5, 3)]
    [TestCase(6, 4)]
    public void WhenAddingNumberOfInnerOrbitsForMVStar(int orbitRoll, int expectedOrbitNum)
    {
        var spectralRoll = 0;
        _mockRollingService.SetupSequence(x => x.D6(1))
            .Returns(0)
            .Returns(orbitRoll);
        _mockRollingService.SetupSequence(x => x.D3(1))
            .Returns(orbitRoll / 2);
        _mockRttWorldgenStarFactory.Setup(x => x.Generate(out spectralRoll))
            .Returns(new RttWorldgenStar {SpectralType = SpectralType.M, LuminosityClass = LuminosityClass.V});
        _mockRttWorldgenStarFactory.Setup(x => x.Generate(It.IsAny<int>()))
            .Returns(new RttWorldgenStar {SpectralType = SpectralType.M, LuminosityClass = LuminosityClass.V});
        _mockRttWorldgenPlanetFactory.Setup(x =>
                x.GenerateRttWorldgenPlanet(It.IsAny<RttWorldgenStar>(), It.IsAny<PlanetOrbit>(), It.IsAny<int>(), It.IsAny<Coordinates>()))
            .Returns(new RttWorldgenPlanet());
        _classUnderTest = new RttWorldgenStarSystemFactory(_mockRollingService.Object,
            _mockRttWorldgenStarFactory.Object, _mockRttWorldgenPlanetFactory.Object);

        var result = _classUnderTest.Generate(StarSystemType.Regular, new Coordinates());

        Assert.That(result.Planets.Count, Is.EqualTo(expectedOrbitNum));
    }


    [TestCase(1, 0)]
    [TestCase(2, 0)]
    [TestCase(3, 0)]
    [TestCase(4, 1)]
    [TestCase(5, 1)]
    [TestCase(6, 2)]
    public void WhenAddingNumberOfInnerOrbitsForLStar(int orbitRoll, int expectedOrbitNum)
    {
        var spectralRoll = 0;
        _mockRollingService.Setup(x => x.D6(1))
            .Returns(0);
        _mockRollingService.SetupSequence(x => x.D3(1))
            .Returns(orbitRoll / 2);
        _mockRttWorldgenStarFactory.Setup(x => x.Generate(out spectralRoll))
            .Returns(new RttWorldgenStar {SpectralType = SpectralType.L});
        _mockRttWorldgenStarFactory.Setup(x => x.Generate(It.IsAny<int>()))
            .Returns(new RttWorldgenStar {SpectralType = SpectralType.L});
        _mockRttWorldgenPlanetFactory.Setup(x =>
                x.GenerateRttWorldgenPlanet(It.IsAny<RttWorldgenStar>(), It.IsAny<PlanetOrbit>(), It.IsAny<int>(), It.IsAny<Coordinates>()))
            .Returns(new RttWorldgenPlanet());
        _classUnderTest = new RttWorldgenStarSystemFactory(_mockRollingService.Object,
            _mockRttWorldgenStarFactory.Object, _mockRttWorldgenPlanetFactory.Object);

        var result = _classUnderTest.Generate(StarSystemType.Regular, new Coordinates());

        Assert.That(result.Planets.Count, Is.EqualTo(expectedOrbitNum));
    }

    [TestCase(1)]
    [TestCase(2)]
    [TestCase(3)]
    [TestCase(4)]
    [TestCase(5)]
    [TestCase(6)]
    public void WhenAddingNumberOfInnerOrbitsWhenCloseCompanionIsPresent(int orbitRoll)
    {
        var spectralRoll = 0;
        _mockRollingService.SetupSequence(x => x.D6(1))
            .Returns(0)
            .Returns(orbitRoll);
        _mockRollingService.SetupSequence(x => x.D6(3))
            .Returns(15);
        _mockRollingService.SetupSequence(x => x.D3(1))
            .Returns(orbitRoll / 2);
        _mockRttWorldgenStarFactory.Setup(x => x.Generate(out spectralRoll))
            .Returns(new RttWorldgenStar {CompanionOrbit = CompanionOrbit.Close});
        _mockRttWorldgenStarFactory.Setup(x => x.Generate(It.IsAny<int>()))
            .Returns(new RttWorldgenStar {CompanionOrbit = CompanionOrbit.Close});
        _mockRttWorldgenPlanetFactory.Setup(x =>
                x.GenerateRttWorldgenPlanet(It.IsAny<RttWorldgenStar>(), It.IsAny<PlanetOrbit>(), It.IsAny<int>(), It.IsAny<Coordinates>()))
            .Returns(new RttWorldgenPlanet());
        _classUnderTest = new RttWorldgenStarSystemFactory(_mockRollingService.Object,
            _mockRttWorldgenStarFactory.Object, _mockRttWorldgenPlanetFactory.Object);

        var result = _classUnderTest.Generate(StarSystemType.Regular, new Coordinates());

        Assert.That(result.Planets.Count, Is.EqualTo(0));
    }

    [TestCase(1, 0)]
    [TestCase(2, 1)]
    [TestCase(3, 2)]
    [TestCase(4, 3)]
    [TestCase(5, 4)]
    [TestCase(6, 5)]
    public void WhenAddingNumberOfOuterOrbits(int orbitRoll, int expectedOrbitNum)
    {
        var spectralRoll = 0;
        _mockRollingService.SetupSequence(x => x.D6(1))
            .Returns(0)
            .Returns(0)
            .Returns(orbitRoll);
        _mockRollingService.SetupSequence(x => x.D3(1))
            .Returns(orbitRoll / 2);
        _mockRttWorldgenStarFactory.Setup(x => x.Generate(out spectralRoll))
            .Returns(new RttWorldgenStar());
        _mockRttWorldgenStarFactory.Setup(x => x.Generate(It.IsAny<int>()))
            .Returns(new RttWorldgenStar());
        _mockRttWorldgenPlanetFactory.Setup(x =>
                x.GenerateRttWorldgenPlanet(It.IsAny<RttWorldgenStar>(), It.IsAny<PlanetOrbit>(), It.IsAny<int>(), It.IsAny<Coordinates>()))
            .Returns(new RttWorldgenPlanet());
        _classUnderTest = new RttWorldgenStarSystemFactory(_mockRollingService.Object,
            _mockRttWorldgenStarFactory.Object, _mockRttWorldgenPlanetFactory.Object);

        var result = _classUnderTest.Generate(StarSystemType.Regular, new Coordinates());

        Assert.That(result.Planets.Count, Is.EqualTo(expectedOrbitNum));
    }


    [TestCase(2, 0)]
    [TestCase(3, 1)]
    [TestCase(4, 2)]
    [TestCase(5, 3)]
    [TestCase(6, 4)]
    public void WhenAddingNumberOfOuterOrbitsForMVStar(int orbitRoll, int expectedOrbitNum)
    {
        var spectralRoll = 0;
        _mockRollingService.SetupSequence(x => x.D6(1))
            .Returns(0)
            .Returns(0)
            .Returns(orbitRoll);
        _mockRollingService.SetupSequence(x => x.D3(1))
            .Returns(orbitRoll / 2);
        _mockRttWorldgenStarFactory.Setup(x => x.Generate(out spectralRoll))
            .Returns(new RttWorldgenStar {SpectralType = SpectralType.M, LuminosityClass = LuminosityClass.V});
        _mockRttWorldgenStarFactory.Setup(x => x.Generate(It.IsAny<int>()))
            .Returns(new RttWorldgenStar {SpectralType = SpectralType.M, LuminosityClass = LuminosityClass.V});
        _mockRttWorldgenPlanetFactory.Setup(x =>
                x.GenerateRttWorldgenPlanet(It.IsAny<RttWorldgenStar>(), It.IsAny<PlanetOrbit>(), It.IsAny<int>(), It.IsAny<Coordinates>()))
            .Returns(new RttWorldgenPlanet());
        _classUnderTest = new RttWorldgenStarSystemFactory(_mockRollingService.Object,
            _mockRttWorldgenStarFactory.Object, _mockRttWorldgenPlanetFactory.Object);

        var result = _classUnderTest.Generate(StarSystemType.Regular, new Coordinates());

        Assert.That(result.Planets.Count, Is.EqualTo(expectedOrbitNum));
    }


    [TestCase(1, 0)]
    [TestCase(2, 0)]
    [TestCase(3, 0)]
    [TestCase(4, 1)]
    [TestCase(5, 1)]
    [TestCase(6, 2)]
    public void WhenAddingNumberOfOuterOrbitsForLStar(int orbitRoll, int expectedOrbitNum)
    {
        var spectralRoll = 0;
        _mockRollingService.SetupSequence(x => x.D6(1))
            .Returns(0)
            .Returns(0)
            .Returns(orbitRoll);
        _mockRollingService.SetupSequence(x => x.D3(1))
            .Returns(orbitRoll / 2);
        _mockRttWorldgenStarFactory.Setup(x => x.Generate(out spectralRoll))
            .Returns(new RttWorldgenStar {SpectralType = SpectralType.L});
        _mockRttWorldgenStarFactory.Setup(x => x.Generate(It.IsAny<int>()))
            .Returns(new RttWorldgenStar {SpectralType = SpectralType.L});
        _mockRttWorldgenPlanetFactory.Setup(x =>
                x.GenerateRttWorldgenPlanet(It.IsAny<RttWorldgenStar>(), It.IsAny<PlanetOrbit>(), It.IsAny<int>(), It.IsAny<Coordinates>()))
            .Returns(new RttWorldgenPlanet());
        _classUnderTest = new RttWorldgenStarSystemFactory(_mockRollingService.Object,
            _mockRttWorldgenStarFactory.Object, _mockRttWorldgenPlanetFactory.Object);

        var result = _classUnderTest.Generate(StarSystemType.Regular, new Coordinates());

        Assert.That(result.Planets.Count, Is.EqualTo(expectedOrbitNum));
    }

    [TestCase(1)]
    [TestCase(2)]
    [TestCase(3)]
    [TestCase(4)]
    [TestCase(5)]
    [TestCase(6)]
    public void WhenAddingNumberOfOuterOrbitsWhenModerateCompanionIsPresent(int orbitRoll)
    {
        var spectralRoll = 0;
        _mockRollingService.SetupSequence(x => x.D6(1))
            .Returns(0)
            .Returns(0)
            .Returns(orbitRoll);
        _mockRollingService.SetupSequence(x => x.D6(3))
            .Returns(15);
        _mockRollingService.SetupSequence(x => x.D3(1))
            .Returns(orbitRoll / 2);
        _mockRttWorldgenStarFactory.Setup(x => x.Generate(out spectralRoll))
            .Returns(new RttWorldgenStar {CompanionOrbit = CompanionOrbit.Moderate});
        _mockRttWorldgenStarFactory.Setup(x => x.Generate(It.IsAny<int>()))
            .Returns(new RttWorldgenStar {CompanionOrbit = CompanionOrbit.Moderate});
        _mockRttWorldgenPlanetFactory.Setup(x =>
                x.GenerateRttWorldgenPlanet(It.IsAny<RttWorldgenStar>(), It.IsAny<PlanetOrbit>(), It.IsAny<int>(), It.IsAny<Coordinates>()))
            .Returns(new RttWorldgenPlanet());
        _classUnderTest = new RttWorldgenStarSystemFactory(_mockRollingService.Object,
            _mockRttWorldgenStarFactory.Object, _mockRttWorldgenPlanetFactory.Object);

        var result = _classUnderTest.Generate(StarSystemType.Regular, new Coordinates());

        Assert.That(result.Planets.Count, Is.EqualTo(0));
    }

    [Test]
    public void WhenGeneratingBrownDwarfSystem()
    {
        _mockRttWorldgenStarFactory.Setup(x => x.GenerateBrownDwarf())
            .Returns(new RttWorldgenStar {
                SpectralType = SpectralType.L,
                LuminosityClass = LuminosityClass.None,
                SpectralSubclass = 4
            });
        _mockRttWorldgenStarFactory.Setup(x => x.Generate(It.IsAny<int>()))
            .Returns(new RttWorldgenStar {
                SpectralType = SpectralType.L,
                LuminosityClass = LuminosityClass.None,
                SpectralSubclass = 4
            });
        _classUnderTest = new RttWorldgenStarSystemFactory(_mockRollingService.Object,
            _mockRttWorldgenStarFactory.Object, _mockRttWorldgenPlanetFactory.Object);

        var result = _classUnderTest.Generate(StarSystemType.BrownDwarf, new Coordinates());

        Assert.That(result.PrimaryStar.SpectralType, Is.EqualTo(SpectralType.L));
    }

    [Test]
    public void WhenGeneratingWithPregeneratedStar()
    {
        var pregenStar = new RttWorldgenStar {
            Age = 7,
            CompanionOrbit = CompanionOrbit.None,
            ExpansionSize = 3,
            Id = Guid.NewGuid(),
            LuminosityClass = LuminosityClass.IV,
            SpectralSubclass = 7,
            SpectralType = SpectralType.G
        };
        _classUnderTest = new RttWorldgenStarSystemFactory(_mockRollingService.Object,
            _mockRttWorldgenStarFactory.Object, _mockRttWorldgenPlanetFactory.Object);

        var result = _classUnderTest.Generate(pregenStar, new Coordinates());

        var resultStar = result.PrimaryStar;
        Assert.That(((RttWorldgenStar) resultStar).Age, Is.EqualTo(pregenStar.Age));
        Assert.That(((RttWorldgenStar) resultStar).CompanionOrbit, Is.EqualTo(pregenStar.CompanionOrbit));
        Assert.That(((RttWorldgenStar) resultStar).ExpansionSize, Is.EqualTo(pregenStar.ExpansionSize));
        Assert.That(((RttWorldgenStar) resultStar).Id, Is.EqualTo(pregenStar.Id));
        Assert.That(((RttWorldgenStar) resultStar).LuminosityClass, Is.EqualTo(pregenStar.LuminosityClass));
        Assert.That(((RttWorldgenStar) resultStar).SpectralSubclass, Is.EqualTo(pregenStar.SpectralSubclass));
        Assert.That(((RttWorldgenStar) resultStar).SpectralType, Is.EqualTo(pregenStar.SpectralType));
    }
}