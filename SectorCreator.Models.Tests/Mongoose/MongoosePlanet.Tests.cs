using System.Collections.Generic;
using NUnit.Framework;
using SectorCreator.Global.Enums;
using SectorCreator.Models.Mongoose;

namespace SectorCreator.Models.Tests.Mongoose;

[TestFixture]
public class MongoosePlanetTests
{
    [Test]
    public void WhenConstructing()
    {
        var planet = new MongoosePlanet();

        Assert.That(planet.Satellites.Count, Is.EqualTo(0));
        Assert.That(planet.Bases.Count, Is.EqualTo(0));
        Assert.That(planet.TradeCodes.Count, Is.EqualTo(0));
        Assert.That(planet.Name, Is.EqualTo("Un-named"));
    }

    private static List<MongoosePlanet> testPlanets = new() {
        new MongoosePlanet {
            Size = 0,
            Atmosphere = 7,
            Hydrographics = 4,
            PlanetType = PlanetType.Terrestrial,
            Population = 6,
            Government = 5,
            LawLevel = 6,
            TechLevel = 2,
            Name = "This is a name",
            Temperature = 8,
            Starport = 'D',
            Bases = new List<Base>(),
            TradeCodes = new List<TradeCode>(),
            TravelCode = TravelCode.Amber
        }
    };

    [TestCaseSource(nameof(testPlanets))]
    public void WhenConstructingWithPlanet(MongoosePlanet planet)
    {
        var result = new MongoosePlanet(planet);

        Assert.That(result.Size, Is.EqualTo(planet.Size));
        Assert.That(result.Atmosphere, Is.EqualTo(planet.Atmosphere));
        Assert.That(result.Hydrographics, Is.EqualTo(planet.Hydrographics));
        Assert.That(result.PlanetType, Is.EqualTo(planet.PlanetType));
        Assert.That(result.Population, Is.EqualTo(planet.Population));
        Assert.That(result.Government, Is.EqualTo(planet.Government));
        Assert.That(result.LawLevel, Is.EqualTo(planet.LawLevel));
        Assert.That(result.TechLevel, Is.EqualTo(planet.TechLevel));
        Assert.That(result.Name, Is.EqualTo(planet.Name));
        Assert.That(result.Temperature, Is.EqualTo(planet.Temperature));
        Assert.That(result.Starport, Is.EqualTo(planet.Starport));
        Assert.That(result.Bases, Is.EqualTo(planet.Bases));
        Assert.That(result.TradeCodes, Is.EqualTo(planet.TradeCodes));
        Assert.That(result.TravelCode, Is.EqualTo(planet.TravelCode));
    }
}