using NUnit.Framework;
using SectorCreator.Global.Enums;
using SectorCreator.Models.Services;

namespace SectorCreator.Models.Tests.Services;

[TestFixture]
public class ChemistryServiceTests
{
    [TestCase(PlanetChemistry.None, 0, 0)]
    [TestCase(PlanetChemistry.Water, 0, 0)]
    [TestCase(PlanetChemistry.Ammonia, 0, 1)]
    [TestCase(PlanetChemistry.Methane, 0, 3)]
    [TestCase(PlanetChemistry.Methane, 1, 1)]
    [TestCase(PlanetChemistry.Sulfur, 0, 0)]
    [TestCase(PlanetChemistry.Chlorine, 0, 0)]
    [TestCase((PlanetChemistry)100, 0, 0)]
    public void WhenGettingAgeMod(PlanetChemistry chemistry, int panthalassicAgeMod, int expected)
    {
        var result = ChemistryService.GetAgeMod(chemistry, panthalassicAgeMod);
        
        Assert.That(result, Is.EqualTo(expected));
    }
}