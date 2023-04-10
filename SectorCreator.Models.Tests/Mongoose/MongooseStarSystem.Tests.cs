using Moq;
using NUnit.Framework;
using SectorCreator.Global.Enums;
using SectorCreator.Models.Factories;
using SectorCreator.Models.Mongoose;

namespace SectorCreator.Models.Tests.Mongoose;

[TestFixture]
public class MongooseStarSystemTests
{
    [Test]
    public void WhenConstructing()
    {
        // Setup
        var planetFactoryMock = new Mock<PlanetFactory>();
        planetFactoryMock.Setup(x => x.Generate(It.IsAny<SectorType>())).Returns(new MongoosePlanet());

        // Act
        var starSystem = new MongooseStarSystem(SectorType.Basic);

        // Asserts
        Assert.That(starSystem.Stars.Count, Is.EqualTo(0));
    }

    [TestCase(1, false)]
    [TestCase(2, false)]
    [TestCase(3, false)]
    [TestCase(4, false)]
    [TestCase(5, true)]
    [TestCase(6, false)]
    [TestCase(7, false)]
    [TestCase(8, false)]
    [TestCase(9, false)]
    [TestCase(10, false)]
    [TestCase(11, false)]
    [TestCase(12, false)]
    public void WhenGettingGasGiant(int roll, bool expected)
    {
        
    }
}