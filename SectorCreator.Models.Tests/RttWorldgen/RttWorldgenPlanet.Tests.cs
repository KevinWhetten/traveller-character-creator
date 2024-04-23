using System;
using NUnit.Framework;
using SectorCreator.Global.Enums;
using SectorCreator.Models.RTTWorldgen;

namespace SectorCreator.Models.Tests.RttWorldgen;

[TestFixture]
public class RttWorldgenPlanetTests
{
    [Test]
    public void WhenConstructing()
    {
        var planet = new RttWorldgenPlanet();
        
        Assert.That(planet.Size, Is.EqualTo(0));
        Assert.That(planet.Atmosphere, Is.EqualTo(0));
        Assert.That(planet.Hydrographics, Is.EqualTo(0));
        Assert.That(planet.PlanetType, Is.EqualTo(PlanetType.None));
        Assert.That(planet.Satellites.Count, Is.EqualTo(0));
        Assert.That(planet.Population, Is.EqualTo(0));
        Assert.That(planet.Government, Is.EqualTo(0));
        Assert.That(planet.LawLevel, Is.EqualTo(0));
        Assert.That(planet.TechLevel, Is.EqualTo(0));
        Assert.That(planet.Name, Is.EqualTo("Un-named"));
        Assert.That(planet.Temperature, Is.EqualTo(Temperature.None));
        Assert.That(planet.Starport, Is.EqualTo(0));
        Assert.That(planet.Bases.Count, Is.EqualTo(0));
        Assert.That(planet.TradeCodes.Count, Is.EqualTo(0));
        Assert.That(planet.TravelZone, Is.EqualTo(TravelZone.None));
        Assert.That(planet.Id, Is.EqualTo(Guid.Empty));
        Assert.That(planet.Biosphere, Is.EqualTo(0));
        Assert.That(planet.Chemistry, Is.EqualTo(PlanetChemistry.None));
        Assert.That(planet.Rings, Is.EqualTo(Rings.None));
        Assert.That(planet.IndustrialBase, Is.EqualTo(0));
        Assert.That(planet.WorldType, Is.EqualTo(WorldType.None));
        Assert.That(planet.PlanetOrbit, Is.EqualTo(PlanetOrbit.Epistellar));
        Assert.That(planet.OrbitPosition, Is.EqualTo(0));
        Assert.That(planet.IsMainWorld, Is.EqualTo(false));
        Assert.That(planet.ParentId, Is.EqualTo(Guid.Empty));
        Assert.That(planet.StarId, Is.EqualTo(Guid.Empty));
        Assert.That(planet.SatelliteOrbit, Is.EqualTo(CompanionOrbit.None));
    }
}