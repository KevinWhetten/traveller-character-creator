using NUnit.Framework;
using SectorCreator.Global;
using SectorCreator.Global.Enums;
using SectorCreator.Models.Mongoose;

namespace SectorCreator.Services.Tests;

[TestFixture]
public class HexTests
{
    [TestCase(1,1,1,1, 1, 1)]
    [TestCase(2,1,1,1, 9, 1)]
    [TestCase(3,1,1,1, 17, 1)]
    [TestCase(4,1,1,1, 25, 1)]
    
    [TestCase(1,2,1,1, 1, 11)]
    [TestCase(2,2,1,1, 9, 11)]
    [TestCase(3,2,1,1, 17, 11)]
    [TestCase(4,2,1,1, 25, 11)]
    
    [TestCase(1,3,1,1, 1, 21)]
    [TestCase(2,3,1,1, 9, 21)]
    [TestCase(3,3,1,1, 17, 21)]
    [TestCase(4,3,1,1, 25, 21)]
    
    [TestCase(1,4,1,1, 1, 31)]
    [TestCase(2,4,1,1, 9, 31)]
    [TestCase(3,4,1,1, 17, 31)]
    [TestCase(4,4,1,1, 25, 31)]
    
    [TestCase(1,1,8,10, 8, 10)]
    [TestCase(2,1,8,10, 16, 10)]
    [TestCase(3,1,8,10, 24, 10)]
    [TestCase(4,1,8,10, 32, 10)]
    
    [TestCase(1,2,8,10, 8, 20)]
    [TestCase(2,2,8,10, 16, 20)]
    [TestCase(3,2,8,10, 24, 20)]
    [TestCase(4,2,8,10, 32, 20)]
    
    [TestCase(1,3,8,10, 8, 30)]
    [TestCase(2,3,8,10, 16, 30)]
    [TestCase(3,3,8,10, 24, 30)]
    [TestCase(4,3,8,10, 32, 30)]
    
    [TestCase(1,4,8,10, 8, 40)]
    [TestCase(2,4,8,10, 16, 40)]
    [TestCase(3,4,8,10, 24, 40)]
    [TestCase(4,4,8,10, 32, 40)]
    public void GetXCoordinate(int x1, int y1, int x2, int y2, int expectedX, int expectedY)
    {
        var subsectorCoordinates = new Coordinates(x1, y1);
        var hexCoordinates = new Coordinates(x2, y2);
        var hex = new MongooseHex(hexCoordinates, subsectorCoordinates, SectorType.Basic);

        hex.SetCoordinates(subsectorCoordinates, hexCoordinates);
        
        Assert.That(hex.Coordinates.X, Is.EqualTo(expectedX));
        Assert.That(hex.Coordinates.Y, Is.EqualTo(expectedY));
    }
}