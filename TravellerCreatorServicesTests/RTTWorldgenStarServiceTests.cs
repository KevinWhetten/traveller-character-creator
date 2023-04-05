using System;
using System.Collections.Generic;
using NUnit.Framework;
using TravellerCreatorModels.SectorCreator.Enums;
using TravellerCreatorModels.SectorCreator.RTTWorldgen;
using TravellerCreatorServices;
using TravellerCreatorServices.RTTWorldgen;

namespace TravellerCreatorServicesTests;

[TestFixture]
public class RTTWorldgenStarServiceTests
{
    private readonly RTTWorldgenStarService _classUnderTest = new();

    [TestCase(2, SpectralType.A)]
    [TestCase(3, SpectralType.F)]
    [TestCase(4, SpectralType.G)]
    [TestCase(5, SpectralType.K)]
    [TestCase(6, SpectralType.M)]
    [TestCase(8, SpectralType.M)]
    [TestCase(10, SpectralType.M)]
    [TestCase(12, SpectralType.M)]
    [TestCase(13, SpectralType.M)]
    [TestCase(14, SpectralType.L)]
    [TestCase(16, SpectralType.L)]
    public void GetSpectralType(int roll, SpectralType expected)
    {
        SpectralType result = _classUnderTest.GetSpectralType(roll);

        Assert.That(result, Is.EqualTo(expected));
    }

    [Test]
    [Repeat(50)]
    public void GeneratePrimarySpectralType()
    {
        var expected = new List<SpectralType> {
            SpectralType.A,
            SpectralType.F,
            SpectralType.G,
            SpectralType.K,
            SpectralType.M,
            SpectralType.L
        };

        var result = _classUnderTest.GenerateSpectralType(RTTWorldgenStarType.Primary);

        Assert.IsTrue(expected.Contains(result));
    }

    [TestCase(2, SpectralType.A)]
    [TestCase(3, SpectralType.F)]
    [TestCase(4, SpectralType.G)]
    [TestCase(5, SpectralType.K)]
    [Repeat(50)]
    public void GenerateCompanionSpectralType(int primaryRoll, SpectralType expected)
    {
        var result = _classUnderTest.GenerateSpectralType(RTTWorldgenStarType.Companion, primaryRoll);

        Assert.That((int) result, Is.GreaterThanOrEqualTo((int) expected));
    }

    [TestCase(6, SpectralType.M)]
    [TestCase(7, SpectralType.M)]
    [TestCase(8, SpectralType.M)]
    [TestCase(14, SpectralType.L)]
    [TestCase(15, SpectralType.L)]
    [TestCase(16, SpectralType.L)]
    [TestCase(17, SpectralType.L)]
    [TestCase(18, SpectralType.L)]
    [Repeat(50)]
    public void GenerateCompanionSpectralTypeEquals(int primaryRoll, SpectralType expected)
    {
        var result = _classUnderTest.GenerateSpectralType(RTTWorldgenStarType.Companion, primaryRoll);

        Assert.That(result, Is.EqualTo(expected));
    }

    [Test]
    [Repeat(50)]
    public void FinishATypeGeneration()
    {
        var star = new RTTWorldgenStar {
            SpectralType = SpectralType.A
        };
        var result = _classUnderTest.FinishStarGeneration(star);

        if (result.SpectralType == SpectralType.A) {
            Assert.That(result.Luminosity, Is.EqualTo(Luminosity.V));
        } else if (result.SpectralType == SpectralType.F) {
            Assert.That(result.Luminosity, Is.EqualTo(Luminosity.IV));
        } else if (result.SpectralType == SpectralType.K) {
            Assert.That(result.Luminosity, Is.EqualTo(Luminosity.III));
        } else if (result.SpectralType == SpectralType.D) {
            Assert.That(true);
        } else {
            throw new ArgumentOutOfRangeException();
        }
    }

    [Test]
    [Repeat(50)]
    public void FinishFTypeGeneration()
    {
        var star = new RTTWorldgenStar {
            SpectralType = SpectralType.F
        };
        var result = _classUnderTest.FinishStarGeneration(star);

        if (result.SpectralType == SpectralType.F) {
            Assert.That(result.Luminosity, Is.EqualTo(Luminosity.V));
        } else if (result.SpectralType == SpectralType.G) {
            Assert.That(result.Luminosity, Is.EqualTo(Luminosity.IV));
        } else if (result.SpectralType == SpectralType.M) {
            Assert.That(result.Luminosity, Is.EqualTo(Luminosity.III));
        } else if (result.SpectralType == SpectralType.D) {
            Assert.That(true);
        } else {
            throw new ArgumentOutOfRangeException();
        }
    }

    [Test]
    [Repeat(50)]
    public void FinishGTypeGeneration()
    {
        var star = new RTTWorldgenStar {
            SpectralType = SpectralType.G
        };
        var result = _classUnderTest.FinishStarGeneration(star);

        if (result.SpectralType == SpectralType.G) {
            Assert.That(result.Luminosity, Is.EqualTo(Luminosity.V));
        } else if (result.SpectralType == SpectralType.K) {
            Assert.That(result.Luminosity, Is.EqualTo(Luminosity.IV));
        } else if (result.SpectralType == SpectralType.M) {
            Assert.That(result.Luminosity, Is.EqualTo(Luminosity.III));
        } else if (result.SpectralType == SpectralType.D) {
            Assert.That(true);
        } else {
            throw new ArgumentOutOfRangeException();
        }
    }

    [Test]
    [Repeat(50)]
    public void FinishMTypeGeneration()
    {
        var star = new RTTWorldgenStar {
            SpectralType = SpectralType.M
        };
        var result = _classUnderTest.FinishStarGeneration(star, true);

        if (result.SpectralType == SpectralType.M) {
            Assert.That(new List<Luminosity> {Luminosity.V, Luminosity.Ve}.Contains(result.Luminosity));
        } else {
            Assert.That(result.SpectralType, Is.EqualTo(SpectralType.L));
        }
    }
}