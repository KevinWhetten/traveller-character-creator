using Moq;
using NUnit.Framework;
using SectorCreator.Global;
using SectorCreator.Global.Enums;
using SectorCreator.Models.Factories.T5;

namespace SectorCreator.Models.Tests.Factories.T5;

public class T5StarSystemFactoryTests
{
    private It5StarSystemFactory _classUnderTest;
    private readonly Mock<IRollingService> _mockRollingService = new();

    [SetUp]
    public void SetUp()
    {
        _classUnderTest = new It5StarSystemFactory(_mockRollingService.Object);
    }

    [TestCase(2, 'A')]
    [TestCase(4, 'A')]
    [TestCase(5, 'B')]
    [TestCase(6, 'B')]
    [TestCase(7, 'C')]
    [TestCase(8, 'C')]
    [TestCase(9, 'D')]
    [TestCase(10, 'E')]
    [TestCase(11, 'E')]
    [TestCase(12, 'X')]
    public void WhenGeneratingStarport(int starportRoll, char expectedStarport)
    {
        _mockRollingService.Setup(x => x.D6(2)).Returns(starportRoll);

        _classUnderTest.GeneratePlanetStarport();

        Assert.That(_classUnderTest.Planet.Starport, Is.EqualTo(expectedStarport));
    }

    [TestCase(-6, SpectralType.O)]
    [TestCase(-5, SpectralType.B)]
    [TestCase(-4, SpectralType.A)]
    [TestCase(-3, SpectralType.A)]
    [TestCase(-2, SpectralType.F)]
    [TestCase(-1, SpectralType.F)]
    [TestCase(0, SpectralType.G)]
    [TestCase(1, SpectralType.K)]
    [TestCase(2, SpectralType.K)]
    [TestCase(3, SpectralType.M)]
    [TestCase(6, SpectralType.M)]
    public void WhenGeneratingStarSpectralType(int flux, SpectralType expectedSpectralType)
    {
        _mockRollingService.Setup(x => x.Flux()).Returns(flux);

        _classUnderTest.GenerateSpectralType();

        Assert.That(_classUnderTest.Star.SpectralType, Is.EqualTo(expectedSpectralType));
    }

    [TestCase(-6, LuminosityClass.Ia)]
    [TestCase(-5, LuminosityClass.Ia)]
    [TestCase(-4, LuminosityClass.Ib)]
    [TestCase(-3, LuminosityClass.II)]
    [TestCase(-2, LuminosityClass.III)]
    [TestCase(-1, LuminosityClass.III)]
    [TestCase(0, LuminosityClass.III)]
    [TestCase(1, LuminosityClass.V)]
    [TestCase(2, LuminosityClass.V)]
    [TestCase(3, LuminosityClass.V)]
    [TestCase(4, LuminosityClass.IV)]
    [TestCase(5, LuminosityClass.D)]
    [TestCase(6, LuminosityClass.D)]
    public void WhenGeneratingOStarLuminosity(int flux, LuminosityClass expectedLuminosityClass)
    {
        _mockRollingService.Setup(x => x.Flux()).Returns(flux);
        _classUnderTest.Star.SpectralType = SpectralType.O;

        _classUnderTest.GenerateLuminosity();

        Assert.That(_classUnderTest.Star.LuminosityClass, Is.EqualTo(expectedLuminosityClass));
    }

    [TestCase(-6, LuminosityClass.Ia)]
    [TestCase(-5, LuminosityClass.Ia)]
    [TestCase(-4, LuminosityClass.Ib)]
    [TestCase(-3, LuminosityClass.II)]
    [TestCase(-2, LuminosityClass.III)]
    [TestCase(-1, LuminosityClass.III)]
    [TestCase(0, LuminosityClass.III)]
    [TestCase(1, LuminosityClass.III)]
    [TestCase(2, LuminosityClass.V)]
    [TestCase(3, LuminosityClass.V)]
    [TestCase(4, LuminosityClass.IV)]
    [TestCase(5, LuminosityClass.D)]
    [TestCase(6, LuminosityClass.D)]
    public void WhenGeneratingBStarLuminosity(int flux, LuminosityClass expectedLuminosityClass)
    {
        _mockRollingService.Setup(x => x.Flux()).Returns(flux);
        _classUnderTest.Star.SpectralType = SpectralType.B;

        _classUnderTest.GenerateLuminosity();

        Assert.That(_classUnderTest.Star.LuminosityClass, Is.EqualTo(expectedLuminosityClass));
    }

    [TestCase(-6, LuminosityClass.Ia)]
    [TestCase(-5, LuminosityClass.Ia)]
    [TestCase(-4, LuminosityClass.Ib)]
    [TestCase(-3, LuminosityClass.II)]
    [TestCase(-2, LuminosityClass.III)]
    [TestCase(-1, LuminosityClass.IV)]
    [TestCase(0, LuminosityClass.V)]
    [TestCase(1, LuminosityClass.V)]
    [TestCase(2, LuminosityClass.V)]
    [TestCase(3, LuminosityClass.V)]
    [TestCase(4, LuminosityClass.V)]
    [TestCase(5, LuminosityClass.D)]
    [TestCase(6, LuminosityClass.D)]
    public void WhenGeneratingAStarLuminosity(int flux, LuminosityClass expectedLuminosityClass)
    {
        _mockRollingService.Setup(x => x.Flux()).Returns(flux);
        _classUnderTest.Star.SpectralType = SpectralType.A;

        _classUnderTest.GenerateLuminosity();

        Assert.That(_classUnderTest.Star.LuminosityClass, Is.EqualTo(expectedLuminosityClass));
    }

    [TestCase(-6, LuminosityClass.II)]
    [TestCase(-5, LuminosityClass.II)]
    [TestCase(-4, LuminosityClass.III)]
    [TestCase(-3, LuminosityClass.IV)]
    [TestCase(-2, LuminosityClass.V)]
    [TestCase(-1, LuminosityClass.V)]
    [TestCase(0, LuminosityClass.V)]
    [TestCase(1, LuminosityClass.V)]
    [TestCase(2, LuminosityClass.V)]
    [TestCase(3, LuminosityClass.V)]
    [TestCase(4, LuminosityClass.VI)]
    [TestCase(5, LuminosityClass.D)]
    [TestCase(6, LuminosityClass.D)]
    public void WhenGeneratingFStarLuminosity(int flux, LuminosityClass expectedLuminosityClass)
    {
        _mockRollingService.Setup(x => x.Flux()).Returns(flux);
        _classUnderTest.Star.SpectralType = SpectralType.F;

        _classUnderTest.GenerateLuminosity();

        Assert.That(_classUnderTest.Star.LuminosityClass, Is.EqualTo(expectedLuminosityClass));
    }

    [TestCase(-6, LuminosityClass.II)]
    [TestCase(-5, LuminosityClass.II)]
    [TestCase(-4, LuminosityClass.III)]
    [TestCase(-3, LuminosityClass.IV)]
    [TestCase(-2, LuminosityClass.V)]
    [TestCase(-1, LuminosityClass.V)]
    [TestCase(0, LuminosityClass.V)]
    [TestCase(1, LuminosityClass.V)]
    [TestCase(2, LuminosityClass.V)]
    [TestCase(3, LuminosityClass.V)]
    [TestCase(4, LuminosityClass.VI)]
    [TestCase(5, LuminosityClass.D)]
    [TestCase(6, LuminosityClass.D)]
    public void WhenGeneratingGStarLuminosity(int flux, LuminosityClass expectedLuminosityClass)
    {
        _mockRollingService.Setup(x => x.Flux()).Returns(flux);
        _classUnderTest.Star.SpectralType = SpectralType.G;

        _classUnderTest.GenerateLuminosity();

        Assert.That(_classUnderTest.Star.LuminosityClass, Is.EqualTo(expectedLuminosityClass));
    }

    [TestCase(-6, LuminosityClass.II)]
    [TestCase(-5, LuminosityClass.II)]
    [TestCase(-4, LuminosityClass.III)]
    [TestCase(-3, LuminosityClass.IV)]
    [TestCase(-2, LuminosityClass.V)]
    [TestCase(-1, LuminosityClass.V)]
    [TestCase(0, LuminosityClass.V)]
    [TestCase(1, LuminosityClass.V)]
    [TestCase(2, LuminosityClass.V)]
    [TestCase(3, LuminosityClass.V)]
    [TestCase(4, LuminosityClass.VI)]
    [TestCase(5, LuminosityClass.D)]
    [TestCase(6, LuminosityClass.D)]
    public void WhenGeneratingKStarLuminosity(int flux, LuminosityClass expectedLuminosityClass)
    {
        _mockRollingService.Setup(x => x.Flux()).Returns(flux);
        _classUnderTest.Star.SpectralType = SpectralType.K;

        _classUnderTest.GenerateLuminosity();

        Assert.That(_classUnderTest.Star.LuminosityClass, Is.EqualTo(expectedLuminosityClass));
    }

    [TestCase(-6, LuminosityClass.II)]
    [TestCase(-5, LuminosityClass.II)]
    [TestCase(-4, LuminosityClass.II)]
    [TestCase(-3, LuminosityClass.II)]
    [TestCase(-2, LuminosityClass.III)]
    [TestCase(-1, LuminosityClass.V)]
    [TestCase(0, LuminosityClass.V)]
    [TestCase(1, LuminosityClass.V)]
    [TestCase(2, LuminosityClass.V)]
    [TestCase(3, LuminosityClass.V)]
    [TestCase(4, LuminosityClass.VI)]
    [TestCase(5, LuminosityClass.D)]
    [TestCase(6, LuminosityClass.D)]
    public void WhenGeneratingMStarLuminosity(int flux, LuminosityClass expectedLuminosityClass)
    {
        _mockRollingService.Setup(x => x.Flux()).Returns(flux);
        _classUnderTest.Star.SpectralType = SpectralType.M;

        _classUnderTest.GenerateLuminosity();

        Assert.That(_classUnderTest.Star.LuminosityClass, Is.EqualTo(expectedLuminosityClass));
    }

    [TestCase(-6, -2, Temperature.Boiling)]
    [TestCase(-5, -1, Temperature.Hot)]
    [TestCase(-4, -1, Temperature.Hot)]
    [TestCase(-3, -1, Temperature.Hot)]
    [TestCase(-2, 0, Temperature.Temperate)]
    [TestCase(-1, 0, Temperature.Temperate)]
    [TestCase(0, 0, Temperature.Temperate)]
    [TestCase(1, 0, Temperature.Temperate)]
    [TestCase(2, 0, Temperature.Temperate)]
    [TestCase(3, 1, Temperature.Cold)]
    [TestCase(4, 1, Temperature.Cold)]
    [TestCase(5, 1, Temperature.Cold)]
    [TestCase(6, 2, Temperature.Frozen)]
    public void WhenGeneratingMainworldOrbit(int flux, int expectedHZVar, Temperature expectedTemperature)
    {
        _mockRollingService.Setup(x => x.Flux()).Returns(flux);

        _classUnderTest.GenerateMainworldOrbit();

        Assert.That(_classUnderTest.Planet.Temperature, Is.EqualTo(expectedTemperature));
        Assert.That(_classUnderTest.Planet.HZVar, Is.EqualTo(expectedHZVar));
    }

    [TestCase(-6, -1, Temperature.Hot)]
    [TestCase(-5, -1, Temperature.Hot)]
    [TestCase(-4, 0, Temperature.Temperate)]
    [TestCase(-3, 0, Temperature.Temperate)]
    [TestCase(-2, 0, Temperature.Temperate)]
    [TestCase(-1, 0, Temperature.Temperate)]
    [TestCase(0, 0, Temperature.Temperate)]
    [TestCase(1, 1, Temperature.Cold)]
    [TestCase(2, 1, Temperature.Cold)]
    [TestCase(3, 1, Temperature.Cold)]
    [TestCase(4, 2, Temperature.Frozen)]
    [TestCase(5, 2, Temperature.Frozen)]
    [TestCase(6, 2, Temperature.Frozen)]
    public void WhenGeneratingMainworldOrbitForMStar(int flux, int expectedHZVar, Temperature expectedTemperature)
    {
        _mockRollingService.Setup(x => x.Flux()).Returns(flux);
        _classUnderTest.Star.SpectralType = SpectralType.M;

        _classUnderTest.GenerateMainworldOrbit();

        Assert.That(_classUnderTest.Planet.Temperature, Is.EqualTo(expectedTemperature));
        Assert.That(_classUnderTest.Planet.HZVar, Is.EqualTo(expectedHZVar));
    }

    [TestCase(-6, -2, Temperature.Boiling)]
    [TestCase(-5, -2, Temperature.Boiling)]
    [TestCase(-4, -2, Temperature.Boiling)]
    [TestCase(-3, -1, Temperature.Hot)]
    [TestCase(-2, -1, Temperature.Hot)]
    [TestCase(-1, -1, Temperature.Hot)]
    [TestCase(0, 0, Temperature.Temperate)]
    [TestCase(1, 0, Temperature.Temperate)]
    [TestCase(2, 0, Temperature.Temperate)]
    [TestCase(3, 0, Temperature.Temperate)]
    [TestCase(4, 0, Temperature.Temperate)]
    [TestCase(5, 1, Temperature.Cold)]
    [TestCase(6, 1, Temperature.Cold)]
    public void WhenGeneratingMainworldOrbitForOStar(int flux, int expectedHZVar, Temperature expectedTemperature)
    {
        _mockRollingService.Setup(x => x.Flux()).Returns(flux);
        _classUnderTest.Star.SpectralType = SpectralType.O;

        _classUnderTest.GenerateMainworldOrbit();

        Assert.That(_classUnderTest.Planet.Temperature, Is.EqualTo(expectedTemperature));
        Assert.That(_classUnderTest.Planet.HZVar, Is.EqualTo(expectedHZVar));
    }

    [TestCase(-6, -2, Temperature.Boiling)]
    [TestCase(-5, -2, Temperature.Boiling)]
    [TestCase(-4, -2, Temperature.Boiling)]
    [TestCase(-3, -1, Temperature.Hot)]
    [TestCase(-2, -1, Temperature.Hot)]
    [TestCase(-1, -1, Temperature.Hot)]
    [TestCase(0, 0, Temperature.Temperate)]
    [TestCase(1, 0, Temperature.Temperate)]
    [TestCase(2, 0, Temperature.Temperate)]
    [TestCase(3, 0, Temperature.Temperate)]
    [TestCase(4, 0, Temperature.Temperate)]
    [TestCase(5, 1, Temperature.Cold)]
    [TestCase(6, 1, Temperature.Cold)]
    public void WhenGeneratingMainworldOrbitForBStar(int flux, int expectedHZVar, Temperature expectedTemperature)
    {
        _mockRollingService.Setup(x => x.Flux()).Returns(flux);
        _classUnderTest.Star.SpectralType = SpectralType.B;

        _classUnderTest.GenerateMainworldOrbit();

        Assert.That(_classUnderTest.Planet.Temperature, Is.EqualTo(expectedTemperature));
        Assert.That(_classUnderTest.Planet.HZVar, Is.EqualTo(expectedHZVar));
    }

    [TestCase(-5, CompanionOrbit.Distant)]
    [TestCase(-4, CompanionOrbit.Distant)]
    [TestCase(-3, CompanionOrbit.Close)]
    [TestCase(-2, CompanionOrbit.None)]
    [TestCase(-1, CompanionOrbit.None)]
    [TestCase(0, CompanionOrbit.None)]
    [TestCase(1, CompanionOrbit.None)]
    [TestCase(2, CompanionOrbit.None)]
    [TestCase(3, CompanionOrbit.None)]
    [TestCase(4, CompanionOrbit.None)]
    [TestCase(5, CompanionOrbit.None)]
    public void WhenGeneratingIsSatellite(int flux, CompanionOrbit expectedSatelliteOrbitType)
    {
        _mockRollingService.Setup(x => x.Flux()).Returns(flux);
        _classUnderTest.Star.SpectralType = SpectralType.B;

        _classUnderTest.GenerateMainworldIsSatellite();

        Assert.That(_classUnderTest.Planet.SatelliteOrbitType, Is.EqualTo(expectedSatelliteOrbitType));
    }

    [TestCase(-5, ParentType.GasGiant)]
    [TestCase(-4, ParentType.GasGiant)]
    [TestCase(-3, ParentType.GasGiant)]
    [TestCase(-2, ParentType.GasGiant)]
    [TestCase(-1, ParentType.GasGiant)]
    [TestCase(0, ParentType.GasGiant)]
    [TestCase(1, ParentType.Planet)]
    [TestCase(2, ParentType.Planet)]
    [TestCase(3, ParentType.Planet)]
    [TestCase(4, ParentType.Planet)]
    [TestCase(5, ParentType.Planet)]
    public void WhenGeneratingParentType(int flux, ParentType expectedParentType)
    {
        _mockRollingService.Setup(x => x.Flux()).Returns(flux);
        _classUnderTest.Star.SpectralType = SpectralType.B;

        _classUnderTest.GenerateParentType();

        Assert.That(_classUnderTest.Planet.ParentType, Is.EqualTo(expectedParentType));
    }

    [TestCase(-6, 'A')]
    [TestCase(-5, 'B')]
    [TestCase(-4, 'C')]
    [TestCase(-3, 'D')]
    [TestCase(-2, 'E')]
    [TestCase(-1, 'F')]
    [TestCase(0, 'G')]
    [TestCase(1, 'H')]
    [TestCase(2, 'I')]
    [TestCase(3, 'J')]
    [TestCase(4, 'K')]
    [TestCase(5, 'L')]
    [TestCase(6, 'M')]
    public void WhenGeneratingOrbitLetterForCloseOrbit(int flux, char expectedOrbitLetter)
    {
        _mockRollingService.Setup(x => x.Flux()).Returns(flux);
        _classUnderTest.Planet.SatelliteOrbitType = CompanionOrbit.Close;

        _classUnderTest.GenerateOrbitLetter();

        Assert.That(_classUnderTest.Planet.SatelliteOrbit, Is.EqualTo(expectedOrbitLetter));
    }

    [TestCase(-6, 'N')]
    [TestCase(-5, 'O')]
    [TestCase(-4, 'P')]
    [TestCase(-3, 'Q')]
    [TestCase(-2, 'R')]
    [TestCase(-1, 'S')]
    [TestCase(0, 'T')]
    [TestCase(1, 'U')]
    [TestCase(2, 'V')]
    [TestCase(3, 'W')]
    [TestCase(4, 'X')]
    [TestCase(5, 'Y')]
    [TestCase(6, 'Z')]
    public void WhenGeneratingOrbitLetterForDistantOrbit(int flux, char expectedOrbitLetter)
    {
        _mockRollingService.Setup(x => x.Flux()).Returns(flux);
        _classUnderTest.Planet.SatelliteOrbitType = CompanionOrbit.Distant;

        _classUnderTest.GenerateOrbitLetter();

        Assert.That(_classUnderTest.Planet.SatelliteOrbit, Is.EqualTo(expectedOrbitLetter));
    }

    [TestCase(1, 1, 2, 000)]
    [TestCase(2, 4, 6, 111)]
    [TestCase(5, 3, 7, 401)]
    [TestCase(10, 6, 12, 934)]
    public void WhenGeneratingPBG(int planetsRoll, int beltsRoll, int gasGiantsRoll, int expectedPBG)
    {
        _mockRollingService.Setup(x => x.D10(1)).Returns(planetsRoll);
        _mockRollingService.Setup(x => x.D6(1)).Returns(beltsRoll);
        _mockRollingService.Setup(x => x.D6(2)).Returns(gasGiantsRoll);

        _classUnderTest.GenerateStarSystemPBG();
        
        Assert.That(_classUnderTest.StarSystem.PBG, Is.EqualTo(expectedPBG));
    }
}