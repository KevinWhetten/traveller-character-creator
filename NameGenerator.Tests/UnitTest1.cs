namespace TestProject1;

public class Tests
{
    [SetUp]
    public void Setup()
    { }

    [Test]
    public void Generate100HumanNames()
    {
        for (var i = 0; i < 100; i++) {
            Console.WriteLine(NameGenerator.NameGenerator.GeneratePlanetName("../../../../Data/LanguageFiles/human_planets.txt"));
        }
    }

    [Test]
    public void Generate100KaSaraNames()
    {
        for (var i = 0; i < 100; i++) {
            Console.WriteLine(NameGenerator.NameGenerator.GeneratePlanetName("../../../../Data/LanguageFiles/kaSara_planets.txt"));
        }
    }

    [Test]
    public void Generate100SsitolusssNames()
    {
        for (var i = 0; i < 100; i++) {
            Console.WriteLine(NameGenerator.NameGenerator.GeneratePlanetName("../../../../Data/LanguageFiles/ssitolusss_planets.txt"));
        }
    }
}