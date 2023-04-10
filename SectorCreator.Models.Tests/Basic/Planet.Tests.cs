using System.Collections.Generic;
using NUnit.Framework;
using SectorCreator.Global.Enums;
using SectorCreator.Models.Basic;

namespace SectorCreator.Models.Tests.Basic;

[TestFixture]
public class PlanetTests
{
    [Test]
    public void WhenConstructingParameterless()
    {
        var planet = new Planet();
        Assert.That(planet.Satellites.Count, Is.EqualTo(0));
        Assert.That(planet.Bases.Count, Is.EqualTo(0));
        Assert.That(planet.TradeCodes.Count, Is.EqualTo(0));
        Assert.That(planet.Name, Is.EqualTo("Un-named"));
    }

    private static List<Planet> testPlanets = new() {
        new Planet {
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
        },
        new Planet()
    };

    [TestCaseSource(nameof(testPlanets))]
    public void WhenConstructingWithPlanet(Planet planet)
    {
        var result = new Planet(planet);

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