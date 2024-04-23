using MarkovChain;
using NUnit.Framework;

namespace MarkovChainTests;

[TestFixture]
internal class SsitolusssSpellingTests
{
    private ISsitolusssSpelling classUnderTest = null!;
    private List<string> invalidVowelPairs = null!;

    [SetUp]
    public void SetUp()
    {
        classUnderTest = new SsitolusssSpelling();
        invalidVowelPairs = new List<string>
        {
            "aa", "au", "ea", "ee", "ei", "eo", "eu", "ia", "ii", "io", "iu", "oa", "oe", "oi", "oo", "ua", "ue", "ui",
            "uo", "uu"
        };
    }

    [TestCase('a', true)]
    [TestCase('b', false)]
    [TestCase('c', false)]
    [TestCase('d', false)]
    [TestCase('e', true)]
    [TestCase('y', false)]
    public void IsVowelTest(char c, bool expected)
    {
        Assert.That(classUnderTest.IsVowel(c), Is.EqualTo(expected));
    }

    [TestCase]
    [TestCase]
    [TestCase]
    [TestCase]
    [TestCase]
    [TestCase]
    [TestCase]
    [TestCase]
    [TestCase]
    [TestCase]
    public void GetRandomVowelTest()
    {
        Assert.That(classUnderTest.GetRandomVowel(), Is.AnyOf("a", "e", "i", "o", "u", "ae", "ai", "ao", "ie", "ou"));
    }

    [TestCase("Ssaa")]
    [TestCase("Ssau")]
    [TestCase("Ssea")]
    [TestCase("Ssee")]
    [TestCase("Ssei")]
    [TestCase("Sseo")]
    [TestCase("Sseu")]
    [TestCase("Ssia")]
    [TestCase("Ssii")]
    [TestCase("Ssio")]
    [TestCase("Ssiu")]
    [TestCase("Ssoa")]
    [TestCase("Ssoe")]
    [TestCase("Ssoi")]
    [TestCase("Ssoo")]
    [TestCase("Ssua")]
    [TestCase("Ssue")]
    [TestCase("Ssui")]
    [TestCase("Ssuo")]
    [TestCase("Ssuu")]
    public void ReplaceInvalidVowelPairsTest(string givenName)
    {
        string result = classUnderTest.ReplaceInvalidVowelPairs(givenName);

        foreach (string invalidVowelPair in invalidVowelPairs)
        {
            Assert.That(result, Does.Not.Contain(invalidVowelPair));
        }
    }

    [TestCase("Ssaie")]
    [TestCase("Ssaoe")]
    [TestCase("Ssaeo")]
    [TestCase("Ssiua")]
    [TestCase("Ssueo")]
    [TestCase("Ssaaeo")]
    [TestCase("Ssueiaoieu")]
    [TestCase("Ssoaeiu")]
    [TestCase("Ssaoi")]
    [TestCase("Ssou")]
    [TestCase("Ssae")]
    public void ReplaceTripleVowelsTest(string givenName)
    {
        string result = classUnderTest.ReplaceTripleVowels(givenName);
        var vowelsInARow = 0;

        foreach (char letter in result)
        {
            if (classUnderTest.IsVowel(letter))
            {
                vowelsInARow++;
            }
            else
            {
                vowelsInARow = 0;
            }

            if (vowelsInARow >= 3)
            {
                Assert.IsTrue(false, "Too many vowels in a row!");
            }
        }

        foreach (string invalidVowelPair in invalidVowelPairs)
        {
            Assert.That(result, Does.Not.Contain(invalidVowelPair));
        }
    }

    [TestCase("Ssy")]
    [TestCase("Ssiy")]
    [TestCase("Ssouy")]
    [TestCase("Ssiouy")]
    [TestCase("Ssiotuy")]
    public void ReplaceEndingYTest(string givenName)
    {
        string result = classUnderTest.ReplaceEndingY(givenName);

        Assert.That(result.Last(), Is.Not.EqualTo('y'));
    }

    [TestCase("Tassssti")]
    [TestCase("Ssssarto")]
    [TestCase("Ssssarto")]
    [TestCase("Tartossss")]
    [TestCase("Tartosssssssssss")]
    public void RemoveQuadSsTest(string givenName)
    {
        string result = classUnderTest.RemoveQuadSs(givenName);

        Assert.That(result, Does.Not.Contain("ssss"));
        Assert.That(result, Does.Not.Contain("Ssss"));
        Assert.That(char.IsLower(result.First()), Is.False);
    }

    [TestCase("Ttasardi", "Tasardi")]
    [TestCase("Taasardi", "Tasardi")]
    [TestCase("Tassardi", "Tassardi")]
    [TestCase("Tasaardi", "Tasardi")]
    [TestCase("Tasarrdi", "Tasardi")]
    [TestCase("Tasarddi", "Tasardi")]
    [TestCase("Tasardii", "Tasardi")]
    public void RemoveDoubleLettersTest(string givenName, string expected)
    {
        string result = classUnderTest.RemoveDoubleLetters(givenName);

        Assert.AreEqual(expected, result);
    }

    [TestCase("Tasardi", "Tasssardi")]
    [TestCase("Tasradi", "Tassradi")]
    [TestCase("Tasrasdi", "Tassrassdi")]
    [TestCase("Tasrasi", "Tassrasssi")]
    public void CheckMiddleSsTest(string givenName, string expected)
    {
        string result = classUnderTest.CheckMiddleSs(givenName);

        Assert.AreEqual(expected, result);
    }

    [TestCase("Ssaradi", "Ssaradi")]
    [TestCase("Saradi", "Ssaradi")]
    [TestCase("Sssaradi", "Ssaradi")]
    [TestCase("Taradi", "Taradi")]
    public void CheckBeginningSsTest(string givenName, string expected)
    {
        string result = classUnderTest.CheckBeginningSs(givenName);

        Assert.AreEqual(expected, result);
    }

    [TestCase("Tardis", "Tardisss")]
    [TestCase("Tardi", "Tardi")]
    public void CheckLastSsTest(string givenName, string expected)
    {
        string result = classUnderTest.CheckLastSs(givenName);

        Assert.AreEqual(expected, result);
    }

    [TestCase("Taidl")]
    [TestCase("Taidr")]
    [TestCase("Taihr")]
    [TestCase("Taihl")]
    [TestCase("Tainr")]
    [TestCase("Tainl")]
    [TestCase("Tail")]
    [TestCase("Tair")]
    public void ReplaceEndingConsonantsTest(string givenName)
    {
        string result = classUnderTest.ReplaceEndingConsonants(givenName);

        Assert.That(givenName.Length, Is.LessThanOrEqualTo(result.Length));
        if (result[^1] == 'r' || result[^1] == 'l')
        {
            Assert.That(classUnderTest.IsVowel(result[^2]), Is.True);
        }
    }
}