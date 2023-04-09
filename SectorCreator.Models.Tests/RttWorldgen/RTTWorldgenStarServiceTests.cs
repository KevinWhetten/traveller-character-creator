using System;
using System.Collections.Generic;
using NUnit.Framework;
using SectorCreator.Global.Enums;
using SectorCreator.Models.RTTWorldgen;

namespace SectorCreator.Models.Tests.RttWorldgen;

[TestFixture]
public class RttWorldgenStarServiceTests : RttWorldgenStar
{
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
        var result = base.GetSpectralType(roll);

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

        var result = GenerateSpectralType(RttWorldgenStarType.Primary);

        Assert.IsTrue(expected.Contains(result));
    }

    [TestCase(2, SpectralType.A)]
    [TestCase(3, SpectralType.F)]
    [TestCase(4, SpectralType.G)]
    [TestCase(5, SpectralType.K)]
    [Repeat(50)]
    public void GenerateCompanionSpectralType(int primaryRoll, SpectralType expected)
    {
        var result = GenerateSpectralType(RttWorldgenStarType.Companion, primaryRoll);

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
        var result = GenerateSpectralType(RttWorldgenStarType.Companion, primaryRoll);

        Assert.That(result, Is.EqualTo(expected));
    }

    [Test]
    [Repeat(50)]
    public void FinishATypeGeneration()
    {
        var star = new RttWorldgenStar {
            SpectralType = SpectralType.A
        };
        star.FinishStarGeneration();

        if (star.Age <= 2) {
            Assert.That(star.Luminosity, Is.EqualTo(Luminosity.V));
        } else if (star.SpectralType == SpectralType.A) {
            Assert.That(star.Luminosity, Is.EqualTo(Luminosity.V));
        } else if (star.SpectralType == SpectralType.F) {
            Assert.That(star.Luminosity, Is.EqualTo(Luminosity.IV));
        } else if (star.SpectralType == SpectralType.K) {
            Assert.That(star.Luminosity, Is.EqualTo(Luminosity.III));
        } else if (star.SpectralType == SpectralType.D) {
            Assert.That(true);
        } else {
            Console.Write($"Star Code: {star.SpectralType} {star.Luminosity}");
            Assert.That(false);
        }
    }

    [Test]
    [Repeat(50)]
    public void FinishFTypeGeneration()
    {
        var star = new RttWorldgenStar {
            SpectralType = SpectralType.F
        };
        star.FinishStarGeneration();

        if (star.SpectralType == SpectralType.F) {
            Assert.That(star.Luminosity, Is.EqualTo(Luminosity.V));
        } else if (star.SpectralType == SpectralType.G) {
            Assert.That(star.Luminosity, Is.EqualTo(Luminosity.IV));
        } else if (star.SpectralType == SpectralType.M) {
            Assert.That(star.Luminosity, Is.EqualTo(Luminosity.III));
        } else if (star.SpectralType == SpectralType.D) {
            Assert.That(true);
        } else {
            throw new ArgumentOutOfRangeException();
        }
    }

    [Test]
    [Repeat(50)]
    public void FinishGTypeGeneration()
    {
        var star = new RttWorldgenStar {
            SpectralType = SpectralType.G
        };
        star.FinishStarGeneration();

        if (star.SpectralType == SpectralType.G) {
            Assert.That(star.Luminosity, Is.EqualTo(Luminosity.V));
        } else if (star.SpectralType == SpectralType.K) {
            Assert.That(star.Luminosity, Is.EqualTo(Luminosity.IV));
        } else if (star.SpectralType == SpectralType.M) {
            Assert.That(star.Luminosity, Is.EqualTo(Luminosity.III));
        } else if (star.SpectralType == SpectralType.D) {
            Assert.That(true);
        } else {
            throw new ArgumentOutOfRangeException();
        }
    }

    [Test]
    [Repeat(50)]
    public void FinishMTypeGeneration()
    {
        var star = new RttWorldgenStar {
            SpectralType = SpectralType.M
        };
        star.FinishStarGeneration(true);

        if (star.SpectralType == SpectralType.M) {
            Assert.That(new List<Luminosity> {Luminosity.V, Luminosity.Ve}.Contains(star.Luminosity));
        } else {
            Assert.That(star.SpectralType, Is.EqualTo(SpectralType.L));
        }
    }
}