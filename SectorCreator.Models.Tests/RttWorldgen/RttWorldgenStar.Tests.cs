using System;
using NUnit.Framework;
using SectorCreator.Global.Enums;
using SectorCreator.Models.RTTWorldgen;

namespace SectorCreator.Models.Tests.RttWorldgen;

[TestFixture]
public class RttWorldgenStarTests
{
    [Test]
    public void WhenConstructing()
    {
        var star = new RttWorldgenStar();
        
        Assert.That(star.SpectralType, Is.EqualTo(SpectralType.Wd));
        Assert.That(star.SpectralSubclass, Is.EqualTo(0));
        Assert.That(star.Id, Is.EqualTo(Guid.Empty));
        Assert.That(star.StarType, Is.EqualTo(StarType.Primary));
        Assert.That(star.Luminosity, Is.EqualTo(Luminosity.I));
        Assert.That(star.CompanionOrbit, Is.EqualTo(CompanionOrbit.None));
        Assert.That(star.ExpansionSize, Is.EqualTo(0));
        Assert.That(star.Age, Is.EqualTo(0));
    }
}