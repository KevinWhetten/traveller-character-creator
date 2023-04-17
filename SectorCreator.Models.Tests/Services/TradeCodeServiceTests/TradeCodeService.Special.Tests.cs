using System;
using System.Diagnostics;
using Moq;
using NUnit.Framework;
using SectorCreator.Global;
using SectorCreator.Global.Enums;
using SectorCreator.Models.Basic;
using SectorCreator.Models.RTTWorldgen;
using SectorCreator.Models.Services.TradeCodeService;

namespace SectorCreator.Models.Tests.Services.TradeCodeServiceTests;

public class TradeCodeService_SpecialTests
{
    private TradeCodeService _classUnderTest = new TradeCodeService(new RollingService());

    [TestCase(true, true)]
    [TestCase(false, false)]
    public void AddSatelliteTradeCode(bool parent, bool expected)
    {
        var planet = new RttWorldgenPlanet {
            ParentId = parent ? Guid.NewGuid() : Guid.Empty
        };
        _classUnderTest.AddSatelliteTradeCode(planet);
        Assert.That(planet.TradeCodes.Contains(TradeCode.Satellite), Is.EqualTo(expected));
    }

    [TestCase(TravelCode.None, false)]
    [TestCase(TravelCode.Amber, false)]
    [TestCase(TravelCode.Red, true)]
    public void AddForbiddenTradeCode(TravelCode travelCode, bool expected)
    {
        var planet = new Planet {
            TravelCode = travelCode
        };
        _classUnderTest.AddForbiddenTradeCode(planet);
        Assert.That(planet.TradeCodes.Contains(TradeCode.Forbidden), Is.EqualTo(expected));
    }

    [TestCase(TravelCode.None, 2, false, false)]
    [TestCase(TravelCode.Amber, 2, true, false)]
    [TestCase(TravelCode.Red, 2, false, false)]
    [TestCase(TravelCode.None, 11, false, false)]
    [TestCase(TravelCode.None, 12, false, true)]
    [TestCase(TravelCode.Red, 11, false, false)]
    [TestCase(TravelCode.Red, 12, false, true)]
    public void AddAmberTradeCode(TravelCode travelCode, int roll, bool expectedDanger, bool expectedPuzzle)
    {
        // Setup
        var mockRollingService = new Mock<IRollingService>();
        mockRollingService.Setup(x => x.D6(2)).Returns(roll);
        _classUnderTest = new TradeCodeService(mockRollingService.Object);
        var planet = new Planet {
            TravelCode = travelCode
        };
        
        // Act
        _classUnderTest.AddAmberTradeCode(planet);
        
        // Assert
        Assert.That(planet.TradeCodes.Contains(TradeCode.Danger), Is.EqualTo(expectedDanger));
        Assert.That(planet.TradeCodes.Contains(TradeCode.Puzzle), Is.EqualTo(expectedPuzzle));
    }

    [TestCase(3, false)]
    [TestCase(6, false)]
    [TestCase(10, false)]
    [TestCase(12, false)]
    [TestCase(18, true)]
    public void AddDataRepositoryTradeCode(int roll, bool expected)
    {
        // Setup
        var planet = new Planet();
        var mockRollingService = new Mock<IRollingService>();
        mockRollingService.Setup(x => x.D6(3)).Returns(roll);
        _classUnderTest = new TradeCodeService(mockRollingService.Object);

        // Act
        _classUnderTest.AddDataRepositoryTradeCode(planet);
        
        // Assert
        Assert.That(planet.TradeCodes.Contains(TradeCode.DataRepository), Is.EqualTo(expected));
        Assert.That(planet.Bases.Contains(Base.DataRepository), Is.EqualTo(expected));
    }

    [TestCase(3, false)]
    [TestCase(6, false)]
    [TestCase(10, false)]
    [TestCase(12, false)]
    [TestCase(18, true)]
    public void AddAncientSiteTradeCode(int roll, bool expected)
    {
        // Setup
        var planet = new Planet();
        var mockRollingService = new Mock<IRollingService>();
        mockRollingService.Setup(x => x.D6(3)).Returns(roll);
        _classUnderTest = new TradeCodeService(mockRollingService.Object);

        // Act
        _classUnderTest.AddAncientSiteTradeCode(planet);
        
        // Assert
        Assert.That(planet.TradeCodes.Contains(TradeCode.AncientSite), Is.EqualTo(expected));
        Assert.That(planet.Bases.Contains(Base.AncientSite), Is.EqualTo(expected));
    }

    [TestCase('A', 5, false, false, false)]
    [TestCase('A', 6, true, false, false)]
    [TestCase('A', 8, true, false, false)]
    [TestCase('A', 9, true, true, false)]
    [TestCase('A', 11, true, true, false)]
    [TestCase('A', 12, true, true, true)]
    [TestCase('B', 7, false, false, false)]
    [TestCase('B', 8, true, false, false)]
    [TestCase('B', 10, true, false, false)]
    [TestCase('B', 11, true, true, false)]
    [TestCase('C', 9, false, false, false)]
    [TestCase('C', 10, true, false, false)]
    [TestCase('D', 12, false, false, false)]
    public void AddResearchStationTradeCode(char starport, int roll, bool expected, bool expectedShipyard, bool expectedMegaCorporate)
    {
        // Setup
        var planet = new Planet {Starport = starport};
        var mockRollingService = new Mock<IRollingService>();
        mockRollingService.Setup(x => x.D6(2)).Returns(roll);
        _classUnderTest = new TradeCodeService(mockRollingService.Object);

        // Act
        _classUnderTest.AddResearchStationTradeCode(planet);
        
        // Assert
        Assert.That(planet.TradeCodes.Contains(TradeCode.ResearchStation), Is.EqualTo(expected));
        Assert.That(planet.Bases.Contains(Base.Research), Is.EqualTo(expected));
        Assert.That(planet.Bases.Contains(Base.Shipyard), Is.EqualTo(expectedShipyard));
        Assert.That(planet.Bases.Contains(Base.MegaCorporateHeadquarters), Is.EqualTo(expectedMegaCorporate));
    }
}