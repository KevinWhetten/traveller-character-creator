using System.Collections.Generic;
using NUnit.Framework;
using SectorCreator.Global;
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
        
        Assert.That(planet.Size, Is.EqualTo(0));
        Assert.That(planet.Atmosphere, Is.EqualTo(0));
        Assert.That(planet.Hydrographics, Is.EqualTo(0));
        Assert.That(planet.PlanetType, Is.EqualTo(PlanetType.None));
        Assert.That(planet.Satellites.Count, Is.EqualTo(0));
        Assert.That(planet.Population, Is.EqualTo(0));
        Assert.That(planet.Government, Is.EqualTo(0));
        Assert.That(planet.LawLevel, Is.EqualTo(0));
        Assert.That(planet.TechLevel, Is.EqualTo(0));
        Assert.That(planet.Name, Is.EqualTo(""));
        Assert.That(planet.Temperature, Is.EqualTo(Temperature.None));
        Assert.That(planet.Starport.Class, Is.EqualTo(StarportClass.X));
        Assert.That(planet.Bases.Count, Is.EqualTo(0));
        Assert.That(planet.TradeCodes.Count, Is.EqualTo(0));
        Assert.That(planet.TravelZone, Is.EqualTo(TravelZone.None));
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
            Temperature = Temperature.Temperate,
            Starport = new Starport{ Class = StarportClass.D},
            Bases = new List<string>(),
            TradeCodes = new List<string>(),
            TravelZone = TravelZone.Amber
        },
        new Planet()
    };

    [TestCaseSource(nameof(testPlanets))]
    public void WhenConstructingWithPlanet(Planet planet)
    {
        var result = new Planet(new RollingService(), planet);

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
        Assert.That(result.TravelZone, Is.EqualTo(planet.TravelZone));
    }
}