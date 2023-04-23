using System;
using System.Linq;
using Moq;
using NUnit.Framework;
using SectorCreator.Global;
using SectorCreator.Global.Enums;
using SectorCreator.Models.Factories.RttWorldgen;
using SectorCreator.Models.RTTWorldgen;

namespace SectorCreator.Models.Tests.Factories.RttWorldgen;

[TestFixture]
public class RttWorldgenStarSystemFactoryTests
{
    private RttWorldgenStarSystemFactory _classUnderTest;
    private readonly Mock<IRollingService> _mockRollingService = new();
    private readonly Mock<IRttWorldgenStarFactory> _mockRttWorldgenStarFactory = new();
    private readonly Mock<IRttWorldgenPlanetFactory> _mockRttWorldgenPlanetFactory = new();

    [TestCase(3, 1)]
    [TestCase(10, 1)]
    [TestCase(11, 2)]
    [TestCase(15, 2)]
    [TestCase(16, 3)]
    [TestCase(18, 3)]
    public void WhenGeneratingNumberOfStars(int starNumRoll, int expectedStarNum)
    {
        _mockRollingService.Setup(x => x.D6(3)).Returns(starNumRoll);
        _classUnderTest = new RttWorldgenStarSystemFactory(_mockRollingService.Object,
            _mockRttWorldgenStarFactory.Object, _mockRttWorldgenPlanetFactory.Object);

        var result = _classUnderTest.Generate(StarSystemType.Regular);

        Assert.That(result.Stars.Count, Is.EqualTo(expectedStarNum));
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
        _mockRttWorldgenStarFactory.Setup(x => x.Generate(It.IsAny<bool>(), out spectralRoll, It.IsAny<int>()))
            .Returns(new RttWorldgenStar());
        _mockRttWorldgenPlanetFactory.Setup(x =>
                x.GenerateRttWorldgenPlanet(It.IsAny<RttWorldgenStar>(), It.IsAny<PlanetOrbit>(), It.IsAny<int>()))
            .Returns(new RttWorldgenPlanet());
        _classUnderTest = new RttWorldgenStarSystemFactory(_mockRollingService.Object,
            _mockRttWorldgenStarFactory.Object, _mockRttWorldgenPlanetFactory.Object);

        var result = _classUnderTest.Generate(StarSystemType.Regular);

        Assert.That(result.Planets.Count, Is.EqualTo(expectedOrbitNum));
    }

    [TestCase(4, 0)]
    [TestCase(5, 1)]
    [TestCase(6, 2)]
    public void WhenAddingNumberOfEpistellarOrbitsForMVStar(int orbitRoll, int expectedOrbitNum)
    {
        var spectralRoll = 0;
        _mockRollingService.SetupSequence(x => x.D6(1)).Returns(orbitRoll);
        _mockRttWorldgenStarFactory.Setup(x => x.Generate(It.IsAny<bool>(), out spectralRoll, It.IsAny<int>()))
            .Returns(new RttWorldgenStar {SpectralType = SpectralType.M, Luminosity = Luminosity.V});
        _mockRttWorldgenPlanetFactory.Setup(x =>
                x.GenerateRttWorldgenPlanet(It.IsAny<RttWorldgenStar>(), It.IsAny<PlanetOrbit>(), It.IsAny<int>()))
            .Returns(new RttWorldgenPlanet());
        _classUnderTest = new RttWorldgenStarSystemFactory(_mockRollingService.Object,
            _mockRttWorldgenStarFactory.Object, _mockRttWorldgenPlanetFactory.Object);

        var result = _classUnderTest.Generate(StarSystemType.Regular);

        Assert.That(result.Planets.Count, Is.EqualTo(expectedOrbitNum));
    }

    [TestCase(SpectralType.D, Luminosity.V, 6, 0)]
    [TestCase(SpectralType.L, Luminosity.V, 6, 0)]
    [TestCase(SpectralType.A, Luminosity.III, 6, 0)]
    public void WhenAddingNumberOfEpistellarOrbitsForDLAndIIIStar(SpectralType spectralType, Luminosity luminosity,
        int orbitRoll, int expectedOrbitNum)
    {
        var spectralRoll = 0;
        _mockRollingService.SetupSequence(x => x.D6(1)).Returns(orbitRoll);
        _mockRttWorldgenStarFactory.Setup(x => x.Generate(It.IsAny<bool>(), out spectralRoll, It.IsAny<int>()))
            .Returns(new RttWorldgenStar {SpectralType = spectralType, Luminosity = luminosity});
        _mockRttWorldgenPlanetFactory.Setup(x =>
                x.GenerateRttWorldgenPlanet(It.IsAny<RttWorldgenStar>(), It.IsAny<PlanetOrbit>(), It.IsAny<int>()))
            .Returns(new RttWorldgenPlanet());
        _classUnderTest = new RttWorldgenStarSystemFactory(_mockRollingService.Object,
            _mockRttWorldgenStarFactory.Object, _mockRttWorldgenPlanetFactory.Object);

        var result = _classUnderTest.Generate(StarSystemType.Regular);

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
        _mockRttWorldgenStarFactory.Setup(x => x.Generate(It.IsAny<bool>(), out spectralRoll, It.IsAny<int>()))
            .Returns(new RttWorldgenStar());
        _mockRttWorldgenPlanetFactory.Setup(x =>
                x.GenerateRttWorldgenPlanet(It.IsAny<RttWorldgenStar>(), It.IsAny<PlanetOrbit>(), It.IsAny<int>()))
            .Returns(new RttWorldgenPlanet());
        _classUnderTest = new RttWorldgenStarSystemFactory(_mockRollingService.Object,
            _mockRttWorldgenStarFactory.Object, _mockRttWorldgenPlanetFactory.Object);

        var result = _classUnderTest.Generate(StarSystemType.Regular);

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
        _mockRttWorldgenStarFactory.Setup(x => x.Generate(It.IsAny<bool>(), out spectralRoll, It.IsAny<int>()))
            .Returns(new RttWorldgenStar {SpectralType = SpectralType.M, Luminosity = Luminosity.V});
        _mockRttWorldgenPlanetFactory.Setup(x =>
                x.GenerateRttWorldgenPlanet(It.IsAny<RttWorldgenStar>(), It.IsAny<PlanetOrbit>(), It.IsAny<int>()))
            .Returns(new RttWorldgenPlanet());
        _classUnderTest = new RttWorldgenStarSystemFactory(_mockRollingService.Object,
            _mockRttWorldgenStarFactory.Object, _mockRttWorldgenPlanetFactory.Object);

        var result = _classUnderTest.Generate(StarSystemType.Regular);

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
        _mockRttWorldgenStarFactory.Setup(x => x.Generate(It.IsAny<bool>(), out spectralRoll, It.IsAny<int>()))
            .Returns(new RttWorldgenStar {SpectralType = SpectralType.L});
        _mockRttWorldgenPlanetFactory.Setup(x =>
                x.GenerateRttWorldgenPlanet(It.IsAny<RttWorldgenStar>(), It.IsAny<PlanetOrbit>(), It.IsAny<int>()))
            .Returns(new RttWorldgenPlanet());
        _classUnderTest = new RttWorldgenStarSystemFactory(_mockRollingService.Object,
            _mockRttWorldgenStarFactory.Object, _mockRttWorldgenPlanetFactory.Object);

        var result = _classUnderTest.Generate(StarSystemType.Regular);

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
        _mockRollingService.SetupSequence(x => x.D3(1))
            .Returns(orbitRoll / 2);
        _mockRttWorldgenStarFactory.Setup(x => x.Generate(It.IsAny<bool>(), out spectralRoll, It.IsAny<int>()))
            .Returns(new RttWorldgenStar {CompanionOrbit = CompanionOrbit.Close});
        _mockRttWorldgenPlanetFactory.Setup(x =>
                x.GenerateRttWorldgenPlanet(It.IsAny<RttWorldgenStar>(), It.IsAny<PlanetOrbit>(), It.IsAny<int>()))
            .Returns(new RttWorldgenPlanet());
        _classUnderTest = new RttWorldgenStarSystemFactory(_mockRollingService.Object,
            _mockRttWorldgenStarFactory.Object, _mockRttWorldgenPlanetFactory.Object);

        var result = _classUnderTest.Generate(StarSystemType.Regular);

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
        _mockRttWorldgenStarFactory.Setup(x => x.Generate(It.IsAny<bool>(), out spectralRoll, It.IsAny<int>()))
            .Returns(new RttWorldgenStar());
        _mockRttWorldgenPlanetFactory.Setup(x =>
                x.GenerateRttWorldgenPlanet(It.IsAny<RttWorldgenStar>(), It.IsAny<PlanetOrbit>(), It.IsAny<int>()))
            .Returns(new RttWorldgenPlanet());
        _classUnderTest = new RttWorldgenStarSystemFactory(_mockRollingService.Object,
            _mockRttWorldgenStarFactory.Object, _mockRttWorldgenPlanetFactory.Object);

        var result = _classUnderTest.Generate(StarSystemType.Regular);

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
        _mockRttWorldgenStarFactory.Setup(x => x.Generate(It.IsAny<bool>(), out spectralRoll, It.IsAny<int>()))
            .Returns(new RttWorldgenStar {SpectralType = SpectralType.M, Luminosity = Luminosity.V});
        _mockRttWorldgenPlanetFactory.Setup(x =>
                x.GenerateRttWorldgenPlanet(It.IsAny<RttWorldgenStar>(), It.IsAny<PlanetOrbit>(), It.IsAny<int>()))
            .Returns(new RttWorldgenPlanet());
        _classUnderTest = new RttWorldgenStarSystemFactory(_mockRollingService.Object,
            _mockRttWorldgenStarFactory.Object, _mockRttWorldgenPlanetFactory.Object);

        var result = _classUnderTest.Generate(StarSystemType.Regular);

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
        _mockRttWorldgenStarFactory.Setup(x => x.Generate(It.IsAny<bool>(), out spectralRoll, It.IsAny<int>()))
            .Returns(new RttWorldgenStar {SpectralType = SpectralType.L});
        _mockRttWorldgenPlanetFactory.Setup(x =>
                x.GenerateRttWorldgenPlanet(It.IsAny<RttWorldgenStar>(), It.IsAny<PlanetOrbit>(), It.IsAny<int>()))
            .Returns(new RttWorldgenPlanet());
        _classUnderTest = new RttWorldgenStarSystemFactory(_mockRollingService.Object,
            _mockRttWorldgenStarFactory.Object, _mockRttWorldgenPlanetFactory.Object);

        var result = _classUnderTest.Generate(StarSystemType.Regular);

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
        _mockRollingService.SetupSequence(x => x.D3(1))
            .Returns(orbitRoll / 2);
        _mockRttWorldgenStarFactory.Setup(x => x.Generate(It.IsAny<bool>(), out spectralRoll, It.IsAny<int>()))
            .Returns(new RttWorldgenStar {CompanionOrbit = CompanionOrbit.Moderate});
        _mockRttWorldgenPlanetFactory.Setup(x =>
                x.GenerateRttWorldgenPlanet(It.IsAny<RttWorldgenStar>(), It.IsAny<PlanetOrbit>(), It.IsAny<int>()))
            .Returns(new RttWorldgenPlanet());
        _classUnderTest = new RttWorldgenStarSystemFactory(_mockRollingService.Object,
            _mockRttWorldgenStarFactory.Object, _mockRttWorldgenPlanetFactory.Object);

        var result = _classUnderTest.Generate(StarSystemType.Regular);

        Assert.That(result.Planets.Count, Is.EqualTo(0));
    }

    [Test]
    public void WhenGeneratingBrownDwarfSystem()
    {
        _mockRttWorldgenStarFactory.Setup(x => x.GenerateBrownDwarf()).Returns(new RttWorldgenStar {
            SpectralType = SpectralType.L,
            Luminosity = Luminosity.I,
            SpectralSubclass = 4
        });
        _classUnderTest = new RttWorldgenStarSystemFactory(_mockRollingService.Object,
            _mockRttWorldgenStarFactory.Object, _mockRttWorldgenPlanetFactory.Object);

        var result = _classUnderTest.Generate(StarSystemType.BrownDwarf);

        Assert.That(result.Stars.Count, Is.EqualTo(1));
        Assert.That(result.Stars.First().SpectralType, Is.EqualTo(SpectralType.L));
    }

    [Test]
    public void WhenGeneratingWithPregeneratedStar()
    {
        var pregenStar = new RttWorldgenStar {
            Age = 7,
            CompanionOrbit = CompanionOrbit.None,
            ExpansionSize = 3,
            Id = Guid.NewGuid(),
            Luminosity = Luminosity.IV,
            SpectralSubclass = 7,
            SpectralType = SpectralType.G,
            IsPrimary = true
        };
        _classUnderTest = new RttWorldgenStarSystemFactory(_mockRollingService.Object,
            _mockRttWorldgenStarFactory.Object, _mockRttWorldgenPlanetFactory.Object);

        var result = _classUnderTest.Generate(pregenStar);

        var resultStar = result.Stars.First();
        Assert.That(((RttWorldgenStar) resultStar).Age, Is.EqualTo(pregenStar.Age));
        Assert.That(((RttWorldgenStar) resultStar).CompanionOrbit, Is.EqualTo(pregenStar.CompanionOrbit));
        Assert.That(((RttWorldgenStar) resultStar).ExpansionSize, Is.EqualTo(pregenStar.ExpansionSize));
        Assert.That(((RttWorldgenStar) resultStar).Id, Is.EqualTo(pregenStar.Id));
        Assert.That(((RttWorldgenStar) resultStar).Luminosity, Is.EqualTo(pregenStar.Luminosity));
        Assert.That(((RttWorldgenStar) resultStar).SpectralSubclass, Is.EqualTo(pregenStar.SpectralSubclass));
        Assert.That(((RttWorldgenStar) resultStar).SpectralType, Is.EqualTo(pregenStar.SpectralType));
    }
}