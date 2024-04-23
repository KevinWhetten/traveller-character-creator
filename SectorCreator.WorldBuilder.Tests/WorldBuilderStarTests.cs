using Moq;
using SectorCreator.Global;
using SectorCreator.Global.Enums;
using SectorCreator.WorldBuilder.Star;

namespace SectorCreator.WorldBuilder.Tests;

public class WorldBuilderStarTests
{
    private Mock<IRollingService> _mockRollingService = new();

    [SetUp]
    public void Setup()
    { }

    [TestCase(3, SpectralType.M)]
    [TestCase(4, SpectralType.M)]
    [TestCase(5, SpectralType.M)]
    [TestCase(6, SpectralType.M)]
    [TestCase(7, SpectralType.K)]
    [TestCase(8, SpectralType.K)]
    [TestCase(9, SpectralType.G)]
    [TestCase(10, SpectralType.G)]
    [TestCase(11, SpectralType.F)]
    public void WhenGeneratingMainSequenceWorldBuilderStar(int roll, SpectralType expectedSpectralType)
    {
        _mockRollingService.Setup(x => x.D6(It.IsAny<int>())).Returns(roll);

        var star = new WorldBuilderStar(_mockRollingService.Object);

        Assert.That(star.SpectralType, Is.EqualTo(expectedSpectralType));
        Assert.That(star.LuminosityClass, Is.EqualTo(LuminosityClass.V));
    }

    [TestCase(2, SpectralType.A)]
    [TestCase(3, SpectralType.A)]
    [TestCase(4, SpectralType.A)]
    [TestCase(5, SpectralType.A)]
    [TestCase(6, SpectralType.A)]
    [TestCase(7, SpectralType.A)]
    [TestCase(8, SpectralType.A)]
    [TestCase(9, SpectralType.A)]
    [TestCase(10, SpectralType.B)]
    [TestCase(11, SpectralType.B)]
    [TestCase(12, SpectralType.O)]
    public void WhenGeneratingHotStars(int roll, SpectralType expectedSpectralType)
    {
        _mockRollingService.SetupSequence(x => x.D6(It.IsAny<int>())).Returns(12).Returns(roll);

        var star = new WorldBuilderStar(_mockRollingService.Object);

        Assert.That(star.SpectralType, Is.EqualTo(expectedSpectralType));
        Assert.That(star.LuminosityClass, Is.EqualTo(LuminosityClass.V));
    }

    [TestCase(3, LuminosityClass.VI, 2, SpectralType.M)]
    [TestCase(3, LuminosityClass.VI, 3, SpectralType.M)]
    [TestCase(3, LuminosityClass.VI, 4, SpectralType.M)]
    [TestCase(3, LuminosityClass.VI, 5, SpectralType.M)]
    [TestCase(3, LuminosityClass.VI, 6, SpectralType.K)]
    [TestCase(3, LuminosityClass.VI, 7, SpectralType.K)]
    [TestCase(3, LuminosityClass.VI, 8, SpectralType.G)]
    [TestCase(3, LuminosityClass.VI, 9, SpectralType.G)]
    [TestCase(3, LuminosityClass.VI, 10, SpectralType.G)]
    
    [TestCase(4, LuminosityClass.IV, 2, SpectralType.K)]
    [TestCase(4, LuminosityClass.IV, 3, SpectralType.G)]
    [TestCase(4, LuminosityClass.IV, 4, SpectralType.G)]
    [TestCase(4, LuminosityClass.IV, 5, SpectralType.F)]
    [TestCase(4, LuminosityClass.IV, 6, SpectralType.K)]
    [TestCase(4, LuminosityClass.IV, 7, SpectralType.K)]
    [TestCase(4, LuminosityClass.IV, 8, SpectralType.G)]
    [TestCase(4, LuminosityClass.IV, 9, SpectralType.G)]
    [TestCase(4, LuminosityClass.IV, 10, SpectralType.F)]
    
    [TestCase(11, LuminosityClass.III, 2, SpectralType.M)]
    [TestCase(11, LuminosityClass.III, 3, SpectralType.M)]
    [TestCase(11, LuminosityClass.III, 4, SpectralType.M)]
    [TestCase(11, LuminosityClass.III, 5, SpectralType.M)]
    [TestCase(11, LuminosityClass.III, 6, SpectralType.K)]
    [TestCase(11, LuminosityClass.III, 7, SpectralType.K)]
    [TestCase(11, LuminosityClass.III, 8, SpectralType.G)]
    [TestCase(11, LuminosityClass.III, 9, SpectralType.G)]
    [TestCase(11, LuminosityClass.III, 10, SpectralType.F)]
    public void WhenGeneratingUnusualStars(int unusualRoll, LuminosityClass expectedLuminosityClass, int spectralRoll, SpectralType expectedSpectralType)
    {
        _mockRollingService.SetupSequence(x => x.D6(It.IsAny<int>())).Returns(2).Returns(unusualRoll).Returns(spectralRoll);

        var star = new WorldBuilderStar(_mockRollingService.Object);

        Assert.That(star.LuminosityClass, Is.EqualTo(expectedLuminosityClass));
        Assert.That(star.SpectralType, Is.EqualTo(expectedSpectralType));
    }

    [TestCase(3, LuminosityClass.VI, 2, SpectralType.B)]
    [TestCase(3, LuminosityClass.VI, 3, SpectralType.B)]
    [TestCase(3, LuminosityClass.VI, 4, SpectralType.B)]
    [TestCase(3, LuminosityClass.VI, 5, SpectralType.B)]
    [TestCase(3, LuminosityClass.VI, 6, SpectralType.B)]
    [TestCase(3, LuminosityClass.VI, 7, SpectralType.B)]
    [TestCase(3, LuminosityClass.VI, 8, SpectralType.B)]
    [TestCase(3, LuminosityClass.VI, 9, SpectralType.B)]
    [TestCase(3, LuminosityClass.VI, 10, SpectralType.B)]
    [TestCase(3, LuminosityClass.VI, 11, SpectralType.B)]
    [TestCase(3, LuminosityClass.VI, 12, SpectralType.O)]
    
    [TestCase(4, LuminosityClass.IV, 2, SpectralType.A)]
    [TestCase(4, LuminosityClass.IV, 3, SpectralType.A)]
    [TestCase(4, LuminosityClass.IV, 4, SpectralType.A)]
    [TestCase(4, LuminosityClass.IV, 5, SpectralType.A)]
    [TestCase(4, LuminosityClass.IV, 6, SpectralType.A)]
    [TestCase(4, LuminosityClass.IV, 7, SpectralType.A)]
    [TestCase(4, LuminosityClass.IV, 8, SpectralType.A)]
    [TestCase(4, LuminosityClass.IV, 9, SpectralType.A)]
    [TestCase(4, LuminosityClass.IV, 10, SpectralType.B)]
    [TestCase(4, LuminosityClass.IV, 11, SpectralType.B)]
    [TestCase(4, LuminosityClass.IV, 12, SpectralType.B)]
    
    [TestCase(11, LuminosityClass.III, 2, SpectralType.A)]
    [TestCase(11, LuminosityClass.III, 3, SpectralType.A)]
    [TestCase(11, LuminosityClass.III, 4, SpectralType.A)]
    [TestCase(11, LuminosityClass.III, 5, SpectralType.A)]
    [TestCase(11, LuminosityClass.III, 6, SpectralType.A)]
    [TestCase(11, LuminosityClass.III, 7, SpectralType.A)]
    [TestCase(11, LuminosityClass.III, 8, SpectralType.A)]
    [TestCase(11, LuminosityClass.III, 9, SpectralType.A)]
    [TestCase(11, LuminosityClass.III, 10, SpectralType.B)]
    [TestCase(11, LuminosityClass.III, 11, SpectralType.B)]
    [TestCase(11, LuminosityClass.III, 12, SpectralType.O)]
    public void WhenGeneratingUnusualHotStars(int unusualRoll, LuminosityClass expectedLuminosityClass, int spectralRoll, SpectralType expectedSpectralType)
    {
        _mockRollingService.SetupSequence(x => x.D6(It.IsAny<int>())).Returns(2).Returns(unusualRoll).Returns(12).Returns(spectralRoll);

        var star = new WorldBuilderStar(_mockRollingService.Object);

        Assert.That(star.LuminosityClass, Is.EqualTo(expectedLuminosityClass));
        Assert.That(star.SpectralType, Is.EqualTo(expectedSpectralType));
    }

    [TestCase(2,2,LuminosityClass.III,SpectralType.M)]
    [TestCase(2,3,LuminosityClass.III,SpectralType.M)]
    [TestCase(2,4,LuminosityClass.III,SpectralType.M)]
    [TestCase(2,5,LuminosityClass.III,SpectralType.M)]
    [TestCase(2,6,LuminosityClass.III,SpectralType.K)]
    [TestCase(2,7,LuminosityClass.III,SpectralType.K)]
    [TestCase(2,8,LuminosityClass.III,SpectralType.G)]
    [TestCase(2,9,LuminosityClass.III,SpectralType.G)]
    [TestCase(2,10,LuminosityClass.III,SpectralType.F)]
    
    [TestCase(3,2,LuminosityClass.III,SpectralType.M)]
    [TestCase(3,3,LuminosityClass.III,SpectralType.M)]
    [TestCase(3,4,LuminosityClass.III,SpectralType.M)]
    [TestCase(3,5,LuminosityClass.III,SpectralType.M)]
    [TestCase(3,6,LuminosityClass.III,SpectralType.K)]
    [TestCase(3,7,LuminosityClass.III,SpectralType.K)]
    [TestCase(3,8,LuminosityClass.III,SpectralType.G)]
    [TestCase(3,9,LuminosityClass.III,SpectralType.G)]
    [TestCase(3,10,LuminosityClass.III,SpectralType.F)]
    
    [TestCase(4,2,LuminosityClass.III,SpectralType.M)]
    [TestCase(4,3,LuminosityClass.III,SpectralType.M)]
    [TestCase(4,4,LuminosityClass.III,SpectralType.M)]
    [TestCase(4,5,LuminosityClass.III,SpectralType.M)]
    [TestCase(4,6,LuminosityClass.III,SpectralType.K)]
    [TestCase(4,7,LuminosityClass.III,SpectralType.K)]
    [TestCase(4,8,LuminosityClass.III,SpectralType.G)]
    [TestCase(4,9,LuminosityClass.III,SpectralType.G)]
    [TestCase(4,10,LuminosityClass.III,SpectralType.F)]
    
    [TestCase(5,2,LuminosityClass.III,SpectralType.M)]
    [TestCase(5,3,LuminosityClass.III,SpectralType.M)]
    [TestCase(5,4,LuminosityClass.III,SpectralType.M)]
    [TestCase(5,5,LuminosityClass.III,SpectralType.M)]
    [TestCase(5,6,LuminosityClass.III,SpectralType.K)]
    [TestCase(5,7,LuminosityClass.III,SpectralType.K)]
    [TestCase(5,8,LuminosityClass.III,SpectralType.G)]
    [TestCase(5,9,LuminosityClass.III,SpectralType.G)]
    [TestCase(5,10,LuminosityClass.III,SpectralType.F)]
    
    [TestCase(6,2,LuminosityClass.III,SpectralType.M)]
    [TestCase(6,3,LuminosityClass.III,SpectralType.M)]
    [TestCase(6,4,LuminosityClass.III,SpectralType.M)]
    [TestCase(6,5,LuminosityClass.III,SpectralType.M)]
    [TestCase(6,6,LuminosityClass.III,SpectralType.K)]
    [TestCase(6,7,LuminosityClass.III,SpectralType.K)]
    [TestCase(6,8,LuminosityClass.III,SpectralType.G)]
    [TestCase(6,9,LuminosityClass.III,SpectralType.G)]
    [TestCase(6,10,LuminosityClass.III,SpectralType.F)]
    
    [TestCase(7,2,LuminosityClass.III,SpectralType.M)]
    [TestCase(7,3,LuminosityClass.III,SpectralType.M)]
    [TestCase(7,4,LuminosityClass.III,SpectralType.M)]
    [TestCase(7,5,LuminosityClass.III,SpectralType.M)]
    [TestCase(7,6,LuminosityClass.III,SpectralType.K)]
    [TestCase(7,7,LuminosityClass.III,SpectralType.K)]
    [TestCase(7,8,LuminosityClass.III,SpectralType.G)]
    [TestCase(7,9,LuminosityClass.III,SpectralType.G)]
    [TestCase(7,10,LuminosityClass.III,SpectralType.F)]
    
    [TestCase(8,2,LuminosityClass.III,SpectralType.M)]
    [TestCase(8,3,LuminosityClass.III,SpectralType.M)]
    [TestCase(8,4,LuminosityClass.III,SpectralType.M)]
    [TestCase(8,5,LuminosityClass.III,SpectralType.M)]
    [TestCase(8,6,LuminosityClass.III,SpectralType.K)]
    [TestCase(8,7,LuminosityClass.III,SpectralType.K)]
    [TestCase(8,8,LuminosityClass.III,SpectralType.G)]
    [TestCase(8,9,LuminosityClass.III,SpectralType.G)]
    [TestCase(8,10,LuminosityClass.III,SpectralType.F)]
    
    [TestCase(9,2,LuminosityClass.II,SpectralType.M)]
    [TestCase(9,3,LuminosityClass.II,SpectralType.M)]
    [TestCase(9,4,LuminosityClass.II,SpectralType.M)]
    [TestCase(9,5,LuminosityClass.II,SpectralType.M)]
    [TestCase(9,6,LuminosityClass.II,SpectralType.K)]
    [TestCase(9,7,LuminosityClass.II,SpectralType.K)]
    [TestCase(9,8,LuminosityClass.II,SpectralType.G)]
    [TestCase(9,9,LuminosityClass.II,SpectralType.G)]
    [TestCase(9,10,LuminosityClass.II,SpectralType.F)]
    
    [TestCase(10,2,LuminosityClass.II,SpectralType.M)]
    [TestCase(10,3,LuminosityClass.II,SpectralType.M)]
    [TestCase(10,4,LuminosityClass.II,SpectralType.M)]
    [TestCase(10,5,LuminosityClass.II,SpectralType.M)]
    [TestCase(10,6,LuminosityClass.II,SpectralType.K)]
    [TestCase(10,7,LuminosityClass.II,SpectralType.K)]
    [TestCase(10,8,LuminosityClass.II,SpectralType.G)]
    [TestCase(10,9,LuminosityClass.II,SpectralType.G)]
    [TestCase(10,10,LuminosityClass.II,SpectralType.F)]
    
    [TestCase(11,2,LuminosityClass.Ib,SpectralType.M)]
    [TestCase(11,3,LuminosityClass.Ib,SpectralType.M)]
    [TestCase(11,4,LuminosityClass.Ib,SpectralType.M)]
    [TestCase(11,5,LuminosityClass.Ib,SpectralType.M)]
    [TestCase(11,6,LuminosityClass.Ib,SpectralType.K)]
    [TestCase(11,7,LuminosityClass.Ib,SpectralType.K)]
    [TestCase(11,8,LuminosityClass.Ib,SpectralType.G)]
    [TestCase(11,9,LuminosityClass.Ib,SpectralType.G)]
    [TestCase(11,10,LuminosityClass.Ib,SpectralType.F)]
    
    [TestCase(12,2,LuminosityClass.Ia,SpectralType.M)]
    [TestCase(12,3,LuminosityClass.Ia,SpectralType.M)]
    [TestCase(12,4,LuminosityClass.Ia,SpectralType.M)]
    [TestCase(12,5,LuminosityClass.Ia,SpectralType.M)]
    [TestCase(12,6,LuminosityClass.Ia,SpectralType.K)]
    [TestCase(12,7,LuminosityClass.Ia,SpectralType.K)]
    [TestCase(12,8,LuminosityClass.Ia,SpectralType.G)]
    [TestCase(12,9,LuminosityClass.Ia,SpectralType.G)]
    [TestCase(12,10,LuminosityClass.Ia,SpectralType.F)]
    public void WhenGeneratingUnusualGiantStars(int giantRoll, int spectralTypeRoll, LuminosityClass expectedLuminosityClass, SpectralType expectedSpectralType){
        _mockRollingService.SetupSequence(x => x.D6(It.IsAny<int>())).Returns(2).Returns(12).Returns(giantRoll).Returns(spectralTypeRoll);

        var star = new WorldBuilderStar(_mockRollingService.Object);

        Assert.That(star.LuminosityClass, Is.EqualTo(expectedLuminosityClass));
        Assert.That(star.SpectralType, Is.EqualTo(expectedSpectralType));
    }
    
    [TestCase(2,2,LuminosityClass.III,SpectralType.A)]
    [TestCase(2,3,LuminosityClass.III,SpectralType.A)]
    [TestCase(2,4,LuminosityClass.III,SpectralType.A)]
    [TestCase(2,5,LuminosityClass.III,SpectralType.A)]
    [TestCase(2,6,LuminosityClass.III,SpectralType.A)]
    [TestCase(2,7,LuminosityClass.III,SpectralType.A)]
    [TestCase(2,8,LuminosityClass.III,SpectralType.A)]
    [TestCase(2,9,LuminosityClass.III,SpectralType.A)]
    [TestCase(2,10,LuminosityClass.III,SpectralType.B)]
    [TestCase(2,11,LuminosityClass.III,SpectralType.B)]
    [TestCase(2,12,LuminosityClass.III,SpectralType.O)]
    
    [TestCase(3,2,LuminosityClass.III,SpectralType.A)]
    [TestCase(3,3,LuminosityClass.III,SpectralType.A)]
    [TestCase(3,4,LuminosityClass.III,SpectralType.A)]
    [TestCase(3,5,LuminosityClass.III,SpectralType.A)]
    [TestCase(3,6,LuminosityClass.III,SpectralType.A)]
    [TestCase(3,7,LuminosityClass.III,SpectralType.A)]
    [TestCase(3,8,LuminosityClass.III,SpectralType.A)]
    [TestCase(3,9,LuminosityClass.III,SpectralType.A)]
    [TestCase(3,10,LuminosityClass.III,SpectralType.B)]
    [TestCase(3,11,LuminosityClass.III,SpectralType.B)]
    [TestCase(3,12,LuminosityClass.III,SpectralType.O)]
    
    [TestCase(4,2,LuminosityClass.III,SpectralType.A)]
    [TestCase(4,3,LuminosityClass.III,SpectralType.A)]
    [TestCase(4,4,LuminosityClass.III,SpectralType.A)]
    [TestCase(4,5,LuminosityClass.III,SpectralType.A)]
    [TestCase(4,6,LuminosityClass.III,SpectralType.A)]
    [TestCase(4,7,LuminosityClass.III,SpectralType.A)]
    [TestCase(4,8,LuminosityClass.III,SpectralType.A)]
    [TestCase(4,9,LuminosityClass.III,SpectralType.A)]
    [TestCase(4,10,LuminosityClass.III,SpectralType.B)]
    [TestCase(4,11,LuminosityClass.III,SpectralType.B)]
    [TestCase(4,12,LuminosityClass.III,SpectralType.O)]
    
    [TestCase(5,2,LuminosityClass.III,SpectralType.A)]
    [TestCase(5,3,LuminosityClass.III,SpectralType.A)]
    [TestCase(5,4,LuminosityClass.III,SpectralType.A)]
    [TestCase(5,5,LuminosityClass.III,SpectralType.A)]
    [TestCase(5,6,LuminosityClass.III,SpectralType.A)]
    [TestCase(5,7,LuminosityClass.III,SpectralType.A)]
    [TestCase(5,8,LuminosityClass.III,SpectralType.A)]
    [TestCase(5,9,LuminosityClass.III,SpectralType.A)]
    [TestCase(5,10,LuminosityClass.III,SpectralType.B)]
    [TestCase(5,11,LuminosityClass.III,SpectralType.B)]
    [TestCase(5,12,LuminosityClass.III,SpectralType.O)]
    
    [TestCase(6,2,LuminosityClass.III,SpectralType.A)]
    [TestCase(6,3,LuminosityClass.III,SpectralType.A)]
    [TestCase(6,4,LuminosityClass.III,SpectralType.A)]
    [TestCase(6,5,LuminosityClass.III,SpectralType.A)]
    [TestCase(6,6,LuminosityClass.III,SpectralType.A)]
    [TestCase(6,7,LuminosityClass.III,SpectralType.A)]
    [TestCase(6,8,LuminosityClass.III,SpectralType.A)]
    [TestCase(6,9,LuminosityClass.III,SpectralType.A)]
    [TestCase(6,10,LuminosityClass.III,SpectralType.B)]
    [TestCase(6,11,LuminosityClass.III,SpectralType.B)]
    [TestCase(6,12,LuminosityClass.III,SpectralType.O)]
    
    [TestCase(7,2,LuminosityClass.III,SpectralType.A)]
    [TestCase(7,3,LuminosityClass.III,SpectralType.A)]
    [TestCase(7,4,LuminosityClass.III,SpectralType.A)]
    [TestCase(7,5,LuminosityClass.III,SpectralType.A)]
    [TestCase(7,6,LuminosityClass.III,SpectralType.A)]
    [TestCase(7,7,LuminosityClass.III,SpectralType.A)]
    [TestCase(7,8,LuminosityClass.III,SpectralType.A)]
    [TestCase(7,9,LuminosityClass.III,SpectralType.A)]
    [TestCase(7,10,LuminosityClass.III,SpectralType.B)]
    [TestCase(7,11,LuminosityClass.III,SpectralType.B)]
    [TestCase(7,12,LuminosityClass.III,SpectralType.O)]
    
    [TestCase(8,2,LuminosityClass.III,SpectralType.A)]
    [TestCase(8,3,LuminosityClass.III,SpectralType.A)]
    [TestCase(8,4,LuminosityClass.III,SpectralType.A)]
    [TestCase(8,5,LuminosityClass.III,SpectralType.A)]
    [TestCase(8,6,LuminosityClass.III,SpectralType.A)]
    [TestCase(8,7,LuminosityClass.III,SpectralType.A)]
    [TestCase(8,8,LuminosityClass.III,SpectralType.A)]
    [TestCase(8,9,LuminosityClass.III,SpectralType.A)]
    [TestCase(8,10,LuminosityClass.III,SpectralType.B)]
    [TestCase(8,11,LuminosityClass.III,SpectralType.B)]
    [TestCase(8,12,LuminosityClass.III,SpectralType.O)]
    
    [TestCase(9,2,LuminosityClass.II,SpectralType.A)]
    [TestCase(9,3,LuminosityClass.II,SpectralType.A)]
    [TestCase(9,4,LuminosityClass.II,SpectralType.A)]
    [TestCase(9,5,LuminosityClass.II,SpectralType.A)]
    [TestCase(9,6,LuminosityClass.II,SpectralType.A)]
    [TestCase(9,7,LuminosityClass.II,SpectralType.A)]
    [TestCase(9,8,LuminosityClass.II,SpectralType.A)]
    [TestCase(9,9,LuminosityClass.II,SpectralType.A)]
    [TestCase(9,10,LuminosityClass.II,SpectralType.B)]
    [TestCase(9,11,LuminosityClass.II,SpectralType.B)]
    [TestCase(9,12,LuminosityClass.II,SpectralType.O)]
    
    [TestCase(10,2,LuminosityClass.II,SpectralType.A)]
    [TestCase(10,3,LuminosityClass.II,SpectralType.A)]
    [TestCase(10,4,LuminosityClass.II,SpectralType.A)]
    [TestCase(10,5,LuminosityClass.II,SpectralType.A)]
    [TestCase(10,6,LuminosityClass.II,SpectralType.A)]
    [TestCase(10,7,LuminosityClass.II,SpectralType.A)]
    [TestCase(10,8,LuminosityClass.II,SpectralType.A)]
    [TestCase(10,9,LuminosityClass.II,SpectralType.A)]
    [TestCase(10,10,LuminosityClass.II,SpectralType.B)]
    [TestCase(10,11,LuminosityClass.II,SpectralType.B)]
    [TestCase(10,12,LuminosityClass.II,SpectralType.O)]
    
    [TestCase(11,2,LuminosityClass.Ib,SpectralType.A)]
    [TestCase(11,3,LuminosityClass.Ib,SpectralType.A)]
    [TestCase(11,4,LuminosityClass.Ib,SpectralType.A)]
    [TestCase(11,5,LuminosityClass.Ib,SpectralType.A)]
    [TestCase(11,6,LuminosityClass.Ib,SpectralType.A)]
    [TestCase(11,7,LuminosityClass.Ib,SpectralType.A)]
    [TestCase(11,8,LuminosityClass.Ib,SpectralType.A)]
    [TestCase(11,9,LuminosityClass.Ib,SpectralType.A)]
    [TestCase(11,10,LuminosityClass.Ib,SpectralType.B)]
    [TestCase(11,11,LuminosityClass.Ib,SpectralType.B)]
    [TestCase(11,12,LuminosityClass.Ib,SpectralType.O)]
    
    [TestCase(12,2,LuminosityClass.Ia,SpectralType.A)]
    [TestCase(12,3,LuminosityClass.Ia,SpectralType.A)]
    [TestCase(12,4,LuminosityClass.Ia,SpectralType.A)]
    [TestCase(12,5,LuminosityClass.Ia,SpectralType.A)]
    [TestCase(12,6,LuminosityClass.Ia,SpectralType.A)]
    [TestCase(12,7,LuminosityClass.Ia,SpectralType.A)]
    [TestCase(12,8,LuminosityClass.Ia,SpectralType.A)]
    [TestCase(12,9,LuminosityClass.Ia,SpectralType.A)]
    [TestCase(12,10,LuminosityClass.Ia,SpectralType.B)]
    [TestCase(12,11,LuminosityClass.Ia,SpectralType.B)]
    [TestCase(12,12,LuminosityClass.Ia,SpectralType.O)]
    public void WhenGeneratingUnusualGiantHotStars(int giantRoll, int spectralTypeRoll, LuminosityClass expectedLuminosityClass, SpectralType expectedSpectralType){
        _mockRollingService.SetupSequence(x => x.D6(It.IsAny<int>())).Returns(2).Returns(12).Returns(giantRoll).Returns(11).Returns(spectralTypeRoll);

        var star = new WorldBuilderStar(_mockRollingService.Object);

        Assert.That(star.LuminosityClass, Is.EqualTo(expectedLuminosityClass));
        Assert.That(star.SpectralType, Is.EqualTo(expectedSpectralType));
    }

    [TestCase(2, StarSpecialType.BlackHole)]
    [TestCase(3, StarSpecialType.Pulsar)]
    [TestCase(4, StarSpecialType.NeutronStar)]
    [TestCase(5, StarSpecialType.Nebula)]
    [TestCase(6, StarSpecialType.Nebula)]
    [TestCase(7, StarSpecialType.Protostar)]
    [TestCase(8, StarSpecialType.Protostar)]
    [TestCase(9, StarSpecialType.Protostar)]
    [TestCase(10, StarSpecialType.StarCluster)]
    [TestCase(11, StarSpecialType.Anomaly)]
    [TestCase(12, StarSpecialType.Anomaly)]
    public void WhenGeneratingPeculiarStars(int peculiarRoll, StarSpecialType expectedSpecialType)
    {
        _mockRollingService.SetupSequence(x => x.D6(It.IsAny<int>())).Returns(2).Returns(2).Returns(peculiarRoll);

        var star = new WorldBuilderStar(_mockRollingService.Object);

        Assert.That(star.SpecialType, Is.EqualTo(expectedSpecialType));
    }

    [TestCase(2, SpectralType.M)]
    [TestCase(3, SpectralType.M)]
    [TestCase(4, SpectralType.M)]
    [TestCase(5, SpectralType.M)]
    [TestCase(6, SpectralType.K)]
    [TestCase(7, SpectralType.K)]
    [TestCase(8, SpectralType.G)]
    [TestCase(9, SpectralType.G)]
    [TestCase(10, SpectralType.F)]
    public void WhenGeneratingProtostars(int spectralRoll, SpectralType expectedSpectralType)
    {
        _mockRollingService.SetupSequence(x => x.D6(It.IsAny<int>())).Returns(2).Returns(2).Returns(8).Returns(spectralRoll);

        var star = new WorldBuilderStar(_mockRollingService.Object);

        Assert.That(star.SpecialType, Is.EqualTo(StarSpecialType.Protostar));
        Assert.That(star.SpectralType, Is.EqualTo(expectedSpectralType));
    }

    [TestCase(2, SpectralType.A)]
    [TestCase(3, SpectralType.A)]
    [TestCase(4, SpectralType.A)]
    [TestCase(5, SpectralType.A)]
    [TestCase(6, SpectralType.A)]
    [TestCase(7, SpectralType.A)]
    [TestCase(8, SpectralType.A)]
    [TestCase(9, SpectralType.A)]
    [TestCase(10, SpectralType.B)]
    [TestCase(11, SpectralType.B)]
    [TestCase(12, SpectralType.B)]
    public void WhenGeneratingHotProtostars(int spectralRoll, SpectralType expectedSpectralType)
    {
        _mockRollingService.SetupSequence(x => x.D6(It.IsAny<int>())).Returns(2).Returns(2).Returns(8).Returns(12).Returns(spectralRoll);

        var star = new WorldBuilderStar(_mockRollingService.Object);

        Assert.That(star.SpecialType, Is.EqualTo(StarSpecialType.Protostar));
        Assert.That(star.SpectralType, Is.EqualTo(expectedSpectralType));
    }

    [TestCase(2, 0)]
    [TestCase(3, 1)]
    [TestCase(4, 3)]
    [TestCase(5, 5)]
    [TestCase(6, 7)]
    [TestCase(7, 9)]
    [TestCase(8, 8)]
    [TestCase(9, 6)]
    [TestCase(10, 4)]
    [TestCase(11, 2)]
    [TestCase(12, 0)]
    public void WhenGeneratingSpectralSubclass(int roll, int expectedSpectralSubclass)
    {
        _mockRollingService.SetupSequence(x => x.D6(2)).Returns(7).Returns(roll);

        var star = new WorldBuilderStar(_mockRollingService.Object);
        
        Assert.That(star.SpectralSubclass, Is.EqualTo(expectedSpectralSubclass));
    }
    
    [TestCase(2, 8)]
    [TestCase(3, 6)]
    [TestCase(4, 5)]
    [TestCase(5, 4)]
    [TestCase(6, 0)]
    [TestCase(7, 2)]
    [TestCase(8, 1)]
    [TestCase(9, 3)]
    [TestCase(10, 5)]
    [TestCase(11, 7)]
    [TestCase(12, 9)]
    public void WhenGeneratingClassMSpectralSubclass(int roll, int expectedSpectralSubclass)
    {
        _mockRollingService.SetupSequence(x => x.D6(2)).Returns(4).Returns(roll);

        var star = new WorldBuilderStar(_mockRollingService.Object);
        
        Assert.That(star.SpectralSubclass, Is.EqualTo(expectedSpectralSubclass));
    }
    
    [TestCase(2, 0)]
    [TestCase(3, 1)]
    [TestCase(4, 3)]
    [TestCase(5, 5)]
    [TestCase(6, 7)]
    [TestCase(7, 9)]
    [TestCase(8, 8)]
    [TestCase(9, 6)]
    [TestCase(10, 4)]
    [TestCase(11, 2)]
    [TestCase(12, 0)]
    public void WhenGeneratingClassMCompanionSpectralSubclass(int roll, int expectedSpectralSubclass)
    {
        _mockRollingService.SetupSequence(x => x.D6(2)).Returns(7).Returns(roll);

        var star = new WorldBuilderStar(_mockRollingService.Object, StarType.Companion);
        
        Assert.That(star.SpectralSubclass, Is.EqualTo(expectedSpectralSubclass));
    }

    [Test]
    public void WhenGeneratingStar()
    {
        for (var i = 0; i < 1000; i++) {
            var star = new WorldBuilderStar(new RollingService());
            Assert.That(star.OrbitNumber, Is.EqualTo(0));
            Assert.That(star.OrbitDistance, Is.EqualTo(0));
        }
    }

    [Test]
    public void WhenGeneratingCloseStar()
    {
        for (var i = 0; i < 1000; i++) {
            var star = new WorldBuilderStar(new RollingService(), StarType.Close);
            Assert.That(star.OrbitNumber, Is.GreaterThanOrEqualTo(.5m));
            Assert.That(star.OrbitNumber, Is.LessThanOrEqualTo(5.5m));
            Assert.That(star.OrbitDistance, Is.GreaterThanOrEqualTo(.2m));
            Assert.That(star.OrbitDistance, Is.LessThanOrEqualTo(4m));
        }
    }

    [Test]
    public void WhenGeneratingNearStar()
    {
        for (var i = 0; i < 1000; i++) {
            var star = new WorldBuilderStar(new RollingService(), StarType.Near);
            Assert.That(star.OrbitNumber, Is.GreaterThanOrEqualTo(5.5m));
            Assert.That(star.OrbitNumber, Is.LessThanOrEqualTo(11.5m));
            Assert.That(star.OrbitDistance, Is.GreaterThanOrEqualTo(4m));
            Assert.That(star.OrbitDistance, Is.LessThanOrEqualTo(231m));
        }
    }

    [Test]
    public void WhenGeneratingFarStar()
    {
        for (var i = 0; i < 1000; i++) {
            var star = new WorldBuilderStar(new RollingService(), StarType.Far);
            Assert.That(star.OrbitNumber, Is.GreaterThanOrEqualTo(11.5m));
            Assert.That(star.OrbitNumber, Is.LessThanOrEqualTo(17.5m));
            Assert.That(star.OrbitDistance, Is.GreaterThanOrEqualTo(231m));
            Assert.That(star.OrbitDistance, Is.LessThanOrEqualTo(14650m));
        }
    }
}