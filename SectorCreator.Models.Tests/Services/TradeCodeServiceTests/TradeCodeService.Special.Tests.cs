using System;
using Moq;
using NUnit.Framework;
using SectorCreator.Global;
using SectorCreator.Global.Enums;
using SectorCreator.Models.Basic;
using SectorCreator.Models.RTTWorldgen;
using SectorCreator.Models.Services.TradeCodeService;

namespace SectorCreator.Models.Tests.Services.TradeCodeServiceTests;

[TestFixture]
public class TradeCodeService_SpecialTests
{
    [TestCase(true, true)]
    [TestCase(false, false)]
    public void AddSatelliteTradeCode(bool parent, bool expected)
    {
        var planet = new RttWorldgenPlanet {
            ParentId = parent ? Guid.NewGuid() : Guid.Empty
        };
        TradeCodeService.AddSatelliteTradeCode(planet);
        Assert.That(planet.TradeCodes.Contains(TradeCode.Satellite), Is.EqualTo(expected));
    }

    [TestCase(TravelZone.None, false)]
    [TestCase(TravelZone.Amber, false)]
    [TestCase(TravelZone.Red, true)]
    public void AddForbiddenTradeCode(TravelZone travelCode, bool expected)
    {
        var planet = new Planet {
            TravelZone = travelCode
        };
        TradeCodeService.AddForbiddenTradeCode(planet);
        Assert.That(planet.TradeCodes.Contains(TradeCode.Forbidden), Is.EqualTo(expected));
    }

    [TestCase(TravelZone.None, 2, false, false)]
    [TestCase(TravelZone.Amber, 2, true, false)]
    [TestCase(TravelZone.Red, 2, false, false)]
    [TestCase(TravelZone.None, 11, false, false)]
    [TestCase(TravelZone.None, 12, false, true)]
    [TestCase(TravelZone.Red, 11, false, false)]
    [TestCase(TravelZone.Red, 12, false, true)]
    public void AddAmberTradeCode(TravelZone travelCode, int roll, bool expectedDanger, bool expectedPuzzle)
    {
        var planet = new Planet {
            TravelZone = travelCode
        };

        // Act
        TradeCodeService.AddAmberTradeCode(planet);

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

        // Act
        TradeCodeService.AddDataRepositoryTradeCode(planet);

        // Assert
        Assert.That(planet.TradeCodes.Contains(TradeCode.DataRepository), Is.EqualTo(expected));
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

        // Act
        TradeCodeService.AddAncientSiteTradeCode(planet);

        // Assert
        Assert.That(planet.TradeCodes.Contains(TradeCode.AncientSite), Is.EqualTo(expected));
    }

    [TestCase(StarportClass.A, 5, false, false, false)]
    [TestCase(StarportClass.A, 6, true, false, false)]
    [TestCase(StarportClass.A, 8, true, false, false)]
    [TestCase(StarportClass.A, 9, true, true, false)]
    [TestCase(StarportClass.A, 11, true, true, false)]
    [TestCase(StarportClass.A, 12, true, true, true)]
    [TestCase(StarportClass.B, 7, false, false, false)]
    [TestCase(StarportClass.B, 8, true, false, false)]
    [TestCase(StarportClass.B, 10, true, false, false)]
    [TestCase(StarportClass.B, 11, true, true, false)]
    [TestCase(StarportClass.C, 9, false, false, false)]
    [TestCase(StarportClass.C, 10, true, false, false)]
    [TestCase(StarportClass.D, 12, false, false, false)]
    public void AddResearchStationTradeCode(StarportClass starportClass, int roll, bool expected, bool expectedShipyard, bool expectedMegaCorporate)
    {
        // Setup
        var planet = new Planet {
            Starport = new Starport {Class = starportClass}
        };
        var mockRollingService = new Mock<IRollingService>();
        mockRollingService.Setup(x => x.D6(2)).Returns(roll);

        // Act
        TradeCodeService.AddResearchStationTradeCode(planet);

        // Assert
        Assert.That(planet.TradeCodes.Contains(TradeCode.ResearchStation), Is.EqualTo(expected));
        Assert.That(planet.Starport.Installations.Contains(StarportInstallation.Shipyard), Is.EqualTo(expectedShipyard));
        Assert.That(planet.Starport.Installations.Contains(StarportInstallation.MegaCorporateHeadquarters), Is.EqualTo(expectedMegaCorporate));
    }
}