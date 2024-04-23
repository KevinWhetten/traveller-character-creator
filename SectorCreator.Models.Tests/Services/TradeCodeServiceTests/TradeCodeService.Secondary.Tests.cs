using NUnit.Framework;
using SectorCreator.Global;
using SectorCreator.Global.Enums;
using SectorCreator.Models.Basic;
using SectorCreator.Models.RTTWorldgen;
using SectorCreator.Models.Services.TradeCodeService;

namespace SectorCreator.Models.Tests.Services.TradeCodeServiceTests;

[TestFixture]
public class TradeCodeService_SecondaryTests
{
    [TestCase(6, 6, 4, PlanetOrbit.Inner, false, true)]
    [TestCase(3, 6, 4, PlanetOrbit.Inner, false, false)]
    [TestCase(4, 6, 4, PlanetOrbit.Inner, false, true)]
    [TestCase(9, 6, 4, PlanetOrbit.Inner, false, true)]
    [TestCase(10, 6, 4, PlanetOrbit.Inner, false, false)]
    [TestCase(6, 3, 4, PlanetOrbit.Inner, false, false)]
    [TestCase(6, 4, 4, PlanetOrbit.Inner, false, true)]
    [TestCase(6, 8, 4, PlanetOrbit.Inner, false, true)]
    [TestCase(6, 9, 4, PlanetOrbit.Inner, false, false)]
    [TestCase(6, 6, 1, PlanetOrbit.Inner, false, false)]
    [TestCase(6, 6, 2, PlanetOrbit.Inner, false, true)]
    [TestCase(6, 6, 6, PlanetOrbit.Inner, false, true)]
    [TestCase(6, 6, 7, PlanetOrbit.Inner, false, false)]
    [TestCase(6, 6, 4, PlanetOrbit.Epistellar, false, false)]
    [TestCase(6, 6, 4, PlanetOrbit.Outer, false, false)]
    [TestCase(6, 6, 4, PlanetOrbit.Inner, true, false)]
    public void AddFarmingTradeCode(int atmosphere, int hydrographics, int population, PlanetOrbit planetOrbit,
        bool isMainWorld, bool expected)
    {
        var planet = new RttWorldgenPlanet {
            Atmosphere = atmosphere,
            Hydrographics = hydrographics,
            PlanetOrbit = planetOrbit,
            IsMainWorld = isMainWorld
        };
        planet.Populations.Add(new Population {
            PopulationNumber = population
        });
        TradeCodeService.AddFarmingTradeCode(planet);
        Assert.That(planet.TradeCodes.Contains(TradeCode.Farming), Is.EqualTo(expected));
    }

    [TestCase(4, false, PlanetType.AsteroidBelt, true)]
    [TestCase(1, false, PlanetType.AsteroidBelt, false)]
    [TestCase(2, false, PlanetType.AsteroidBelt, true)]
    [TestCase(6, false, PlanetType.AsteroidBelt, true)]
    [TestCase(7, false, PlanetType.AsteroidBelt, false)]
    [TestCase(4, false, PlanetType.Helian, false)]
    [TestCase(4, false, PlanetType.Jovian, false)]
    [TestCase(4, false, PlanetType.Terrestrial, false)]
    [TestCase(4, false, PlanetType.DwarfPlanet, false)]
    public void AddMiningTradeCode(int population, bool isMainWorld, PlanetType planetType, bool expected)
    {
        var planet = new RttWorldgenPlanet {
            IsMainWorld = isMainWorld,
            PlanetType = planetType
        };
        planet.Populations.Add(new Population {
            PopulationNumber = population
        });
        TradeCodeService.AddMiningTradeCode(planet);
        Assert.That(planet.TradeCodes.Contains(TradeCode.Mining), Is.EqualTo(expected));
    }

    [TestCase(6, false, true)]
    [TestCase(6, true, true)]
    [TestCase(5, false, false)]
    [TestCase(7, false, false)]
    [Repeat(50)]
    public void AddCaptiveTradeCode(int government, bool isMainWorld, bool expected)
    {
        var planet = new RttWorldgenPlanet {
            Government = government,
            IsMainWorld = isMainWorld
        };
        TradeCodeService.AddCaptiveTradeCode(planet);
        var flag = false;
        flag = planet.TradeCodes.Contains(TradeCode.MilitaryRule) || flag;
        flag = planet.TradeCodes.Contains(TradeCode.PrisonCamp) || flag;
        flag = planet.TradeCodes.Contains(TradeCode.PenalColony) || flag;

        Assert.That(flag, Is.EqualTo(expected));
    }

    [TestCase(6, true)]
    [TestCase(6, true)]
    [TestCase(5, false)]
    [TestCase(7, false)]
    [Repeat(50)]
    public void AddCaptiveTradeCode(int government, bool expected)
    {
        var planet = new Planet {
            Government = government
        };
        TradeCodeService.AddCaptiveTradeCode(planet);
        var flag = false;
        flag = planet.TradeCodes.Contains(TradeCode.MilitaryRule) || flag;
        flag = planet.TradeCodes.Contains(TradeCode.PrisonCamp) || flag;
        flag = planet.TradeCodes.Contains(TradeCode.PenalColony) || flag;

        Assert.That(flag, Is.EqualTo(expected));
    }
    
    [TestCase(3, StarportClass.X, 1, 10, true)]
    [TestCase(5, StarportClass.X, 1, 10, true)]
    [TestCase(6, StarportClass.X, 1, 10, false)]
    [TestCase(3, StarportClass.E, 1, 10, false)]
    [TestCase(3, StarportClass.X, 0, 10, false)]
    [TestCase(3, StarportClass.X, 1, 6, false)]
    [TestCase(3, StarportClass.X, 1, 7, true)]
    public void AddReserveTradeCode(int techLevel, StarportClass starport, int population, int biosphere, bool expected)
    {
        var planet = new RttWorldgenPlanet {
            TechLevel = techLevel,
            Starport = new Starport {
                Class= starport
            },
            Biosphere = biosphere
        };
        planet.Populations.Add(new Population {
            PopulationNumber = population
        });
        TradeCodeService.AddReserveTradeCode(planet);

        Assert.That(planet.TradeCodes.Contains(TradeCode.Reserve), Is.EqualTo(expected));
    }
}