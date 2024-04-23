using Moq;
using NUnit.Framework;
using SectorCreator.Global;
using SectorCreator.Global.Enums;
using SectorCreator.Models.RTTWorldgen.Factories;

namespace SectorCreator.Models.Tests.Factories.RttWorldgen;

[TestFixture]
public class RttWorldgenStarFactoryTests
{
    private RttWorldgenStarFactory _classUnderTest;
    private readonly Mock<IRollingService> _mockRollingService = new();

    [Test]
    public void WhenGeneratingStar()
    {
        _classUnderTest = new RttWorldgenStarFactory(_mockRollingService.Object);

        _classUnderTest.Generate(out var spectralRoll);
        
        Assert.That(spectralRoll, Is.TypeOf<int>());
    }

    [Test]
    public void WhenGeneratingBrownDwarf()
    {
        _classUnderTest = new RttWorldgenStarFactory(_mockRollingService.Object);

        _classUnderTest.GenerateBrownDwarf();
        
        Assert.That(_classUnderTest.Star.SpectralType, Is.EqualTo(SpectralType.L));
    }

    [TestCase(2, SpectralType.A)]
    [TestCase(3, SpectralType.F)]
    [TestCase(4, SpectralType.G)]
    [TestCase(5, SpectralType.K)]
    [TestCase(6, SpectralType.M)]
    [TestCase(13, SpectralType.M)]
    [TestCase(14, SpectralType.L)]
    [TestCase(16, SpectralType.L)]
    public void WhenGeneratingSpectralType(int spectralTypeRoll, SpectralType expectedSpectralType)
    {
        _mockRollingService.Setup(x => x.D6(2)).Returns(spectralTypeRoll);
        _classUnderTest = new RttWorldgenStarFactory(_mockRollingService.Object);

        _classUnderTest.GenerateSpectralType();

        Assert.That(_classUnderTest.Star.SpectralType, Is.EqualTo(expectedSpectralType));
    }

    [TestCase(1, 2, SpectralType.A)]
    [TestCase(2, 2, SpectralType.F)]
    [TestCase(3, 2, SpectralType.G)]
    [TestCase(4, 2, SpectralType.K)]
    [TestCase(5, 2, SpectralType.M)]
    [TestCase(6, 2, SpectralType.M)]
    [TestCase(1, 3, SpectralType.F)]
    [TestCase(2, 3, SpectralType.G)]
    [TestCase(3, 3, SpectralType.K)]
    [TestCase(4, 3, SpectralType.M)]
    [TestCase(5, 3, SpectralType.M)]
    [TestCase(6, 3, SpectralType.M)]
    [TestCase(3, 11, SpectralType.M)]
    [TestCase(3, 12, SpectralType.L)]
    public void WhenGeneratingSpectralTypeWithMultipleStars(int spectralTypeRoll, int primaryStarRoll, SpectralType expectedSpectralType)
    {
        _mockRollingService.Setup(x => x.D6(1)).Returns(spectralTypeRoll);
        _classUnderTest = new RttWorldgenStarFactory(_mockRollingService.Object) {
            starType = StarType.Companion
        };

        _classUnderTest.GenerateSpectralType(primaryStarRoll);

        Assert.That(_classUnderTest.Star.SpectralType, Is.EqualTo(expectedSpectralType));
    }

    [TestCase(3, 0)]
    [TestCase(7, 4)]
    [TestCase(10, 7)]
    [TestCase(12, 9)]
    [TestCase(15, 12)]
    [TestCase(18, 15)]
    public void WhenGeneratingSystemAge(int ageRoll, int expectedAge)
    {
        _mockRollingService.Setup(x => x.D6(3)).Returns(ageRoll);
        _classUnderTest = new RttWorldgenStarFactory(_mockRollingService.Object);

        _classUnderTest.GenerateAge();

        Assert.That(_classUnderTest.Star.Age, Is.EqualTo(expectedAge));
    }

    [TestCase(0, 1, SpectralType.A, LuminosityClass.V)]
    [TestCase(3, 1, SpectralType.F, LuminosityClass.IV)]
    [TestCase(3, 2, SpectralType.F, LuminosityClass.IV)]
    [TestCase(3, 3, SpectralType.K, LuminosityClass.III)]
    [TestCase(3, 4, SpectralType.D, LuminosityClass.None)]
    [TestCase(3, 5, SpectralType.D, LuminosityClass.None)]
    [TestCase(3, 6, SpectralType.D, LuminosityClass.None)]
    [TestCase(4, 1, SpectralType.D, LuminosityClass.None)]
    [TestCase(7, 1, SpectralType.D, LuminosityClass.None)]
    [TestCase(10, 1, SpectralType.D, LuminosityClass.None)]
    [TestCase(12, 1, SpectralType.D, LuminosityClass.None)]
    [TestCase(15, 1, SpectralType.D, LuminosityClass.None)]
    [TestCase(18, 1, SpectralType.D, LuminosityClass.None)]
    public void WhenModifyingSpectralTypeA(int age, int roll, SpectralType expectedSpectralType, LuminosityClass expectedLuminosityClass)
    {
        _mockRollingService.Setup(x => x.D6(1)).Returns(roll);
        _classUnderTest = new RttWorldgenStarFactory(_mockRollingService.Object) {
            Star = {
                SpectralType = SpectralType.A,
                Age = age
            }
        };

        _classUnderTest.ModifySpectralType();

        Assert.That(_classUnderTest.Star.SpectralType, Is.EqualTo(expectedSpectralType));
        Assert.That(_classUnderTest.Star.LuminosityClass, Is.EqualTo(expectedLuminosityClass));
    }

    [TestCase(0, 1, SpectralType.F, LuminosityClass.V)]
    [TestCase(4, 1, SpectralType.F, LuminosityClass.V)]
    [TestCase(6, 1, SpectralType.G, LuminosityClass.IV)]
    [TestCase(6, 2, SpectralType.G, LuminosityClass.IV)]
    [TestCase(6, 3, SpectralType.G, LuminosityClass.IV)]
    [TestCase(6, 4, SpectralType.G, LuminosityClass.IV)]
    [TestCase(6, 5, SpectralType.M, LuminosityClass.III)]
    [TestCase(6, 6, SpectralType.M, LuminosityClass.III)]
    [TestCase(7, 1, SpectralType.D, LuminosityClass.None)]
    [TestCase(10, 1, SpectralType.D, LuminosityClass.None)]
    [TestCase(12, 1, SpectralType.D, LuminosityClass.None)]
    [TestCase(15, 1, SpectralType.D, LuminosityClass.None)]
    [TestCase(18, 1, SpectralType.D, LuminosityClass.None)]
    public void WhenModifyingSpectralTypeF(int age, int roll, SpectralType expectedSpectralType, LuminosityClass expectedLuminosityClass)
    {
        _mockRollingService.Setup(x => x.D6(1)).Returns(roll);
        _classUnderTest = new RttWorldgenStarFactory(_mockRollingService.Object) {
            Star = {
                SpectralType = SpectralType.F,
                Age = age
            }
        };

        _classUnderTest.ModifySpectralType();

        Assert.That(_classUnderTest.Star.SpectralType, Is.EqualTo(expectedSpectralType));
        Assert.That(_classUnderTest.Star.LuminosityClass, Is.EqualTo(expectedLuminosityClass));
    }

    [TestCase(0, 1, SpectralType.G, LuminosityClass.V)]
    [TestCase(4, 1, SpectralType.G, LuminosityClass.V)]
    [TestCase(7, 1, SpectralType.G, LuminosityClass.V)]
    [TestCase(10, 1, SpectralType.G, LuminosityClass.V)]
    [TestCase(12, 1, SpectralType.K, LuminosityClass.IV)]
    [TestCase(13, 2, SpectralType.K, LuminosityClass.IV)]
    [TestCase(12, 3, SpectralType.K, LuminosityClass.IV)]
    [TestCase(13, 4, SpectralType.M, LuminosityClass.III)]
    [TestCase(12, 5, SpectralType.M, LuminosityClass.III)]
    [TestCase(13, 6, SpectralType.M, LuminosityClass.III)]
    [TestCase(15, 1, SpectralType.D, LuminosityClass.None)]
    [TestCase(18, 1, SpectralType.D, LuminosityClass.None)]
    public void WhenModifyingSpectralTypeG(int age, int roll, SpectralType expectedSpectralType, LuminosityClass expectedLuminosityClass)
    {
        _mockRollingService.Setup(x => x.D6(1)).Returns(roll);
        _classUnderTest = new RttWorldgenStarFactory(_mockRollingService.Object) {
            Star = {
                SpectralType = SpectralType.G,
                Age = age
            }
        };

        _classUnderTest.ModifySpectralType();

        Assert.That(_classUnderTest.Star.SpectralType, Is.EqualTo(expectedSpectralType));
        Assert.That(_classUnderTest.Star.LuminosityClass, Is.EqualTo(expectedLuminosityClass));
    }

    [TestCase(0)]
    [TestCase(4)]
    [TestCase(7)]
    [TestCase(10)]
    [TestCase(12)]
    [TestCase(13)]
    [TestCase(12)]
    [TestCase(13)]
    [TestCase(12)]
    [TestCase(13)]
    [TestCase(15)]
    [TestCase(18)]
    public void WhenModifyingSpectralTypeK(int age)
    {
        _classUnderTest = new RttWorldgenStarFactory(_mockRollingService.Object) {
            Star = {
                SpectralType = SpectralType.K,
                Age = age
            }
        };

        _classUnderTest.ModifySpectralType();

        Assert.That(_classUnderTest.Star.SpectralType, Is.EqualTo(SpectralType.K));
        Assert.That(_classUnderTest.Star.LuminosityClass, Is.EqualTo(LuminosityClass.V));
    }

    [TestCase(true, 2, SpectralType.M, LuminosityClass.V)]
    [TestCase(true, 7, SpectralType.M, LuminosityClass.V)]
    [TestCase(true, 9, SpectralType.M, LuminosityClass.V)]
    [TestCase(true, 10, SpectralType.M, LuminosityClass.Ve)]
    [TestCase(true, 12, SpectralType.M, LuminosityClass.Ve)]
    [TestCase(false, 2, SpectralType.M, LuminosityClass.V)]
    [TestCase(false, 7, SpectralType.M, LuminosityClass.V)]
    [TestCase(false, 8, SpectralType.M, LuminosityClass.Ve)]
    [TestCase(false, 10, SpectralType.M, LuminosityClass.Ve)]
    [TestCase(false, 11, SpectralType.L, LuminosityClass.None)]
    [TestCase(false, 12, SpectralType.L, LuminosityClass.None)]
    public void WhenModifyingSpectralTypeM(bool isPrimary, int roll, SpectralType expectedSpectralType, LuminosityClass expectedLuminosityClass)
    {
        _mockRollingService.Setup(x => x.D6(2)).Returns(roll);
        _classUnderTest = new RttWorldgenStarFactory(_mockRollingService.Object) {
            Star = {
                SpectralType = SpectralType.M
            },
            starType = isPrimary ? StarType.Primary : StarType.Companion
        };

        _classUnderTest.ModifySpectralType();

        Assert.That(_classUnderTest.Star.SpectralType, Is.EqualTo(expectedSpectralType));
        Assert.That(_classUnderTest.Star.LuminosityClass, Is.EqualTo(expectedLuminosityClass));
    }

    [TestCase(true, 1, CompanionOrbit.None)]
    [TestCase(true, 2, CompanionOrbit.None)]
    [TestCase(true, 3, CompanionOrbit.None)]
    [TestCase(true, 4, CompanionOrbit.None)]
    [TestCase(true, 5, CompanionOrbit.None)]
    [TestCase(true, 6, CompanionOrbit.None)]
    [TestCase(false, 1, CompanionOrbit.Tight)]
    [TestCase(false, 2, CompanionOrbit.Tight)]
    [TestCase(false, 3, CompanionOrbit.Close)]
    [TestCase(false, 4, CompanionOrbit.Close)]
    [TestCase(false, 5, CompanionOrbit.Moderate)]
    [TestCase(false, 6, CompanionOrbit.Distant)]
    public void WhenGeneratingOrbit(bool isPrimary, int orbitRoll, CompanionOrbit expectedOrbit)
    {
        _mockRollingService.Setup(x => x.D6(1)).Returns(orbitRoll);
        _classUnderTest = new RttWorldgenStarFactory(_mockRollingService.Object) {
            starType = isPrimary ? StarType.Primary : StarType.Companion
        };

        _classUnderTest.GenerateOrbit();
        
        Assert.That(_classUnderTest.Star.CompanionOrbit, Is.EqualTo(expectedOrbit));
    }
}