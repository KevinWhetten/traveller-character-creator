// using Moq;
// using NUnit.Framework;
// using SectorCreator.Global;
// using SectorCreator.Global.Enums;
// using SectorCreator.Models.Basic;
// using SectorCreator.Models.Factories;
//
// namespace SectorCreator.Models.Tests.Factories;
//
// [TestFixture]
// public class HexFactoryTests
// {
//     private HexFactory _hexFactory;
//
//     [SetUp]
//     public void SetUp()
//     {
//         var starSystemFactoryMock = new Mock<StarSystemFactory>();
//         starSystemFactoryMock.Setup(x => x.GenerateMongooseStarSystem(It.IsAny<SectorType>())).Returns(new StarSystem());
//         starSystemFactoryMock.Setup(x => x.GenerateT5StarSystem()).Returns(new StarSystem());
//
//         _hexFactory = new HexFactory(starSystemFactoryMock.Object, new RollingService());
//     }
//
//     [TestCase(1, 1, 1, 1, 1, 1)]
//     [TestCase(2, 1, 1, 1, 9, 1)]
//     [TestCase(3, 1, 1, 1, 17, 1)]
//     [TestCase(4, 1, 1, 1, 25, 1)]
//     [TestCase(1, 2, 1, 1, 1, 11)]
//     [TestCase(2, 2, 1, 1, 9, 11)]
//     [TestCase(3, 2, 1, 1, 17, 11)]
//     [TestCase(4, 2, 1, 1, 25, 11)]
//     [TestCase(1, 3, 1, 1, 1, 21)]
//     [TestCase(2, 3, 1, 1, 9, 21)]
//     [TestCase(3, 3, 1, 1, 17, 21)]
//     [TestCase(4, 3, 1, 1, 25, 21)]
//     [TestCase(1, 4, 1, 1, 1, 31)]
//     [TestCase(2, 4, 1, 1, 9, 31)]
//     [TestCase(3, 4, 1, 1, 17, 31)]
//     [TestCase(4, 4, 1, 1, 25, 31)]
//     [TestCase(1, 1, 8, 10, 8, 10)]
//     [TestCase(2, 1, 8, 10, 16, 10)]
//     [TestCase(3, 1, 8, 10, 24, 10)]
//     [TestCase(4, 1, 8, 10, 32, 10)]
//     [TestCase(1, 2, 8, 10, 8, 20)]
//     [TestCase(2, 2, 8, 10, 16, 20)]
//     [TestCase(3, 2, 8, 10, 24, 20)]
//     [TestCase(4, 2, 8, 10, 32, 20)]
//     [TestCase(1, 3, 8, 10, 8, 30)]
//     [TestCase(2, 3, 8, 10, 16, 30)]
//     [TestCase(3, 3, 8, 10, 24, 30)]
//     [TestCase(4, 3, 8, 10, 32, 30)]
//     [TestCase(1, 4, 8, 10, 8, 40)]
//     [TestCase(2, 4, 8, 10, 16, 40)]
//     [TestCase(3, 4, 8, 10, 24, 40)]
//     [TestCase(4, 4, 8, 10, 32, 40)]
//     public void GenerateMongooseHex(int x1, int y1, int x2, int y2, int expectedX, int expectedY)
//     {
//         var subsectorCoordinates = new Coordinates(x1, y1);
//         var hexCoordinates = new Coordinates(x2, y2);
//         var hex = _hexFactory.GenerateMongooseHex(subsectorCoordinates, hexCoordinates, SectorType.Basic);
//
//         Assert.That(hex.Coordinates.X, Is.EqualTo(expectedX));
//         Assert.That(hex.Coordinates.Y, Is.EqualTo(expectedY));
//         Assert.True(hex.StarSystems.Count is 0 or 1);
//     }
//
//     [TestCase(1, 1, 1, 1, 1, 1)]
//     [TestCase(2, 1, 1, 1, 9, 1)]
//     [TestCase(3, 1, 1, 1, 17, 1)]
//     [TestCase(4, 1, 1, 1, 25, 1)]
//     [TestCase(1, 2, 1, 1, 1, 11)]
//     [TestCase(2, 2, 1, 1, 9, 11)]
//     [TestCase(3, 2, 1, 1, 17, 11)]
//     [TestCase(4, 2, 1, 1, 25, 11)]
//     [TestCase(1, 3, 1, 1, 1, 21)]
//     [TestCase(2, 3, 1, 1, 9, 21)]
//     [TestCase(3, 3, 1, 1, 17, 21)]
//     [TestCase(4, 3, 1, 1, 25, 21)]
//     [TestCase(1, 4, 1, 1, 1, 31)]
//     [TestCase(2, 4, 1, 1, 9, 31)]
//     [TestCase(3, 4, 1, 1, 17, 31)]
//     [TestCase(4, 4, 1, 1, 25, 31)]
//     [TestCase(1, 1, 8, 10, 8, 10)]
//     [TestCase(2, 1, 8, 10, 16, 10)]
//     [TestCase(3, 1, 8, 10, 24, 10)]
//     [TestCase(4, 1, 8, 10, 32, 10)]
//     [TestCase(1, 2, 8, 10, 8, 20)]
//     [TestCase(2, 2, 8, 10, 16, 20)]
//     [TestCase(3, 2, 8, 10, 24, 20)]
//     [TestCase(4, 2, 8, 10, 32, 20)]
//     [TestCase(1, 3, 8, 10, 8, 30)]
//     [TestCase(2, 3, 8, 10, 16, 30)]
//     [TestCase(3, 3, 8, 10, 24, 30)]
//     [TestCase(4, 3, 8, 10, 32, 30)]
//     [TestCase(1, 4, 8, 10, 8, 40)]
//     [TestCase(2, 4, 8, 10, 16, 40)]
//     [TestCase(3, 4, 8, 10, 24, 40)]
//     [TestCase(4, 4, 8, 10, 32, 40)]
//     public void GenerateT5Hex(int x1, int y1, int x2, int y2, int expectedX, int expectedY)
//     {
//         var subsectorCoordinates = new Coordinates(x1, y1);
//         var hexCoordinates = new Coordinates(x2, y2);
//         var hex = _hexFactory.GenerateT5Hex(subsectorCoordinates, hexCoordinates);
//
//         Assert.That(hex.Coordinates.X, Is.EqualTo(expectedX));
//         Assert.That(hex.Coordinates.Y, Is.EqualTo(expectedY));
//         Assert.True(hex.StarSystems.Count is 0 or 1);
//     }
//
//     [TestCase(1, 1, 1, 1, 1, 1)]
//     [TestCase(2, 1, 1, 1, 9, 1)]
//     [TestCase(3, 1, 1, 1, 17, 1)]
//     [TestCase(4, 1, 1, 1, 25, 1)]
//     [TestCase(1, 2, 1, 1, 1, 11)]
//     [TestCase(2, 2, 1, 1, 9, 11)]
//     [TestCase(3, 2, 1, 1, 17, 11)]
//     [TestCase(4, 2, 1, 1, 25, 11)]
//     [TestCase(1, 3, 1, 1, 1, 21)]
//     [TestCase(2, 3, 1, 1, 9, 21)]
//     [TestCase(3, 3, 1, 1, 17, 21)]
//     [TestCase(4, 3, 1, 1, 25, 21)]
//     [TestCase(1, 4, 1, 1, 1, 31)]
//     [TestCase(2, 4, 1, 1, 9, 31)]
//     [TestCase(3, 4, 1, 1, 17, 31)]
//     [TestCase(4, 4, 1, 1, 25, 31)]
//     [TestCase(1, 1, 8, 10, 8, 10)]
//     [TestCase(2, 1, 8, 10, 16, 10)]
//     [TestCase(3, 1, 8, 10, 24, 10)]
//     [TestCase(4, 1, 8, 10, 32, 10)]
//     [TestCase(1, 2, 8, 10, 8, 20)]
//     [TestCase(2, 2, 8, 10, 16, 20)]
//     [TestCase(3, 2, 8, 10, 24, 20)]
//     [TestCase(4, 2, 8, 10, 32, 20)]
//     [TestCase(1, 3, 8, 10, 8, 30)]
//     [TestCase(2, 3, 8, 10, 16, 30)]
//     [TestCase(3, 3, 8, 10, 24, 30)]
//     [TestCase(4, 3, 8, 10, 32, 30)]
//     [TestCase(1, 4, 8, 10, 8, 40)]
//     [TestCase(2, 4, 8, 10, 16, 40)]
//     [TestCase(3, 4, 8, 10, 24, 40)]
//     [TestCase(4, 4, 8, 10, 32, 40)]
//     public void GenerateRttWorldgenHex(int x1, int y1, int x2, int y2, int expectedX, int expectedY)
//     {
//         var subsectorCoordinates = new Coordinates(x1, y1);
//         var hexCoordinates = new Coordinates(x2, y2);
//         var hex = _hexFactory.GenerateRttWorldgenHex(subsectorCoordinates, hexCoordinates);
//
//         Assert.That(hex.Coordinates.X, Is.EqualTo(expectedX));
//         Assert.That(hex.Coordinates.Y, Is.EqualTo(expectedY));
//         Assert.True(hex.StarSystems.Count is 0 or 1);
//     }
// }