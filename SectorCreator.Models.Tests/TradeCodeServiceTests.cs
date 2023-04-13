// using System.Collections.Generic;
// using NUnit.Framework;
// using SectorCreator.Global;
// using SectorCreator.Global.Enums;
// using SectorCreator.Models.Basic;
//
// namespace SectorCreator.Models.Tests;
//
// public class TradeCodeServiceTests
// {
//   private readonly TradeCodeService _tradeCodeService = new(new RollingService());
//   
//   [SetUp]
//   public void Setup()
//   {
//     _tradeCodeService.TradeCodes = new List<TradeCode>();
//   }
//
//   [TestCase(3, 3, 4, false)]
//   [TestCase(3, 3, 5, false)]
//   [TestCase(3, 4, 4, false)]
//   [TestCase(4, 3, 4, false)]
//   [TestCase(3, 4, 5, false)]
//   [TestCase(4, 3, 5, false)]
//   [TestCase(4, 4, 4, false)]
//   [TestCase(4, 4, 5, true)]
//   [TestCase(9, 8, 7, true)]
//   [TestCase(9, 8, 8, false)]
//   [TestCase(9, 9, 7, false)]
//   [TestCase(10, 8, 7, false)]
//   [TestCase(9, 9, 8, false)]
//   [TestCase(10, 8, 8, false)]
//   [TestCase(10, 9, 7, false)]
//   public void AddAgriculturalTradeCode(int atmosphere, int hydrosphere, int population, bool expected)
//   {
//     var planet = new Planet
//     {
//       Atmosphere = atmosphere,
//       Hydrographics = hydrosphere,
//       Population = population
//     };
//
//     _tradeCodeService.AddAgriculturalTradeCode(planet);
//
//     Assert.That(_tradeCodeService.TradeCodes.Contains(TradeCode.Agricultural), Is.EqualTo(expected));
//   }
//
//   [TestCase(0, 0, 0, true)]
//   [TestCase(1, 0, 0, false)]
//   [TestCase(0, 1, 0, false)]
//   [TestCase(0, 0, 1, false)]
//   [TestCase(0, 1, 1, false)]
//   [TestCase(1, 0, 1, false)]
//   [TestCase(1, 1, 0, false)]
//   [TestCase(1, 1, 1, false)]
//   public void AddAsteroidTradeCode(int size, int atmosphere, int hydrographics, bool expected)
//   {
//     var planet = new Planet
//     {
//       Size = size,
//       Atmosphere = atmosphere,
//       Hydrographics = hydrographics
//     };
//
//     _tradeCodeService.AddAsteroidTradeCode(planet);
//
//     Assert.That(_tradeCodeService.TradeCodes.Contains(TradeCode.Asteroid), Is.EqualTo(expected));
//   }
//
//   [TestCase(0, 0, 0, true)]
//   [TestCase(1, 0, 0, false)]
//   [TestCase(0, 1, 0, false)]
//   [TestCase(0, 0, 1, false)]
//   [TestCase(0, 1, 1, false)]
//   [TestCase(1, 0, 1, false)]
//   [TestCase(1, 1, 0, false)]
//   [TestCase(1, 1, 1, false)]
//   public void AddBarrenTradeCode(int population, int government, int lawLevel, bool expected)
//   {
//     var planet = new Planet
//     {
//       Population = population,
//       Government = government,
//       LawLevel = lawLevel
//     };
//
//     _tradeCodeService.AddBarrenTradeCode(planet);
//
//     Assert.That(_tradeCodeService.TradeCodes.Contains(TradeCode.Barren), Is.EqualTo(expected));
//   }
//
//   [TestCase(0, 0, false)]
//   [TestCase(1, 0, false)]
//   [TestCase(2, 0, true)]
//   [TestCase(3, 0, true)]
//   [TestCase(5, 0, true)]
//   [TestCase(8, 0, true)]
//   [TestCase(15, 0, true)]
//   [TestCase(2, 1, false)]
//   [TestCase(3, 1, false)]
//   [TestCase(5, 1, false)]
//   [TestCase(8, 1, false)]
//   [TestCase(15, 1, false)]
//   public void AddDesertTradeCode(int atmosphere, int hydrographics, bool expected)
//   {
//     var planet = new Planet
//     {
//       Atmosphere = atmosphere,
//       Hydrographics = hydrographics
//     };
//
//     _tradeCodeService.AddDesertTradeCode(planet);
//
//     Assert.That(_tradeCodeService.TradeCodes.Contains(TradeCode.Desert), Is.EqualTo(expected));
//   }
//
//   [TestCase(0, 0, false)]
//   [TestCase(10, 0, false)]
//   [TestCase(10, 1, true)]
//   [TestCase(0, 1, false)]
//   [TestCase(9, 1, false)]
//   [TestCase(9, 4, false)]
//   public void AddFluidOceansTradeCode(int atmosphere, int hydrographics, bool expected)
//   {
//     var planet = new Planet
//     {
//       Atmosphere = atmosphere,
//       Hydrographics = hydrographics
//     };
//
//     _tradeCodeService.AddFluidOceansTradeCode(planet);
//
//     Assert.That(_tradeCodeService.TradeCodes.Contains(TradeCode.FluidOceans), Is.EqualTo(expected));
//   }
//
//   [TestCase(0, 0, 0, false)]
//   [TestCase(5, 0, 0, false)]
//   [TestCase(0, 4, 0, false)]
//   [TestCase(0, 0, 4, false)]
//   [TestCase(5, 4, 0, false)]
//   [TestCase(5, 0, 4, false)]
//   [TestCase(0, 4, 4, false)]
//   [TestCase(5, 4, 4, true)]
//   [TestCase(10, 9, 8, true)]
//   [TestCase(10, 10, 8, false)]
//   [TestCase(10, 9, 9, false)]
//   [TestCase(10, 10, 9, false)]
//   [TestCase(10, 10, 10, false)]
//   public void AddGardenCode(int atmosphere, int hydrographics, int population, bool expected)
//   {
//     var planet = new Planet
//     {
//       Atmosphere = atmosphere,
//       Hydrographics = hydrographics,
//       Population = population
//     };
//
//     _tradeCodeService.AddGardenTradeCode(planet);
//
//     Assert.That(_tradeCodeService.TradeCodes.Contains(TradeCode.Garden), Is.EqualTo(expected));
//   }
//
//   [TestCase(0, false)]
//   [TestCase(8, false)]
//   [TestCase(9, true)]
//   [TestCase(10, true)]
//   public void AddHighPopulationTradeCode(int population, bool expected)
//   {
//     var planet = new Planet
//     {
//       Population = population
//     };
//
//     _tradeCodeService.AddHighPopulationTradeCode(planet);
//
//     Assert.That(_tradeCodeService.TradeCodes.Contains(TradeCode.HighPopulation), Is.EqualTo(expected));
//   }
//
//   [TestCase(0, false)]
//   [TestCase(11, false)]
//   [TestCase(12, true)]
//   [TestCase(15, true)]
//   public void AddHighTechnologyTradeCode(int techLevel, bool expected)
//   {
//     var planet = new Planet
//     {
//       TechLevel = techLevel
//     };
//
//     _tradeCodeService.AddHighTechnologyTradeCode(planet);
//
//     Assert.That(_tradeCodeService.TradeCodes.Contains(TradeCode.HighTechnology), Is.EqualTo(expected));
//   }
//
//   [TestCase(0, 0, false)]
//   [TestCase(0, 1, true)]
//   [TestCase(0, 2, true)]
//   [TestCase(0, 5, true)]
//   [TestCase(0, 10, true)]
//   [TestCase(1, 1, true)]
//   [TestCase(1, 2, true)]
//   [TestCase(1, 5, true)]
//   [TestCase(1, 10, true)]
//   [TestCase(2, 1, false)]
//   [TestCase(2, 2, false)]
//   [TestCase(2, 5, false)]
//   [TestCase(2, 10, false)]
//   public void AddIceCappedTradeCode(int atmosphere, int hydrographics, bool expected)
//   {
//     var planet = new Planet
//     {
//       Atmosphere = atmosphere,
//       Hydrographics = hydrographics
//     };
//
//     _tradeCodeService.AddIceCappedTradeCode(planet);
//
//     Assert.That(_tradeCodeService.TradeCodes.Contains(TradeCode.IceCapped), Is.EqualTo(expected));
//   }
//
//   [TestCase(0, 0, false)]
//   [TestCase(1, 0, false)]
//   [TestCase(2, 0, false)]
//   [TestCase(4, 0, false)]
//   [TestCase(7, 0, false)]
//   [TestCase(9, 0, false)]
//   [TestCase(0, 8, false)]
//   [TestCase(1, 8, false)]
//   [TestCase(2, 8, false)]
//   [TestCase(4, 8, false)]
//   [TestCase(7, 8, false)]
//   [TestCase(9, 8, false)]
//   [TestCase(0, 9, true)]
//   [TestCase(1, 9, true)]
//   [TestCase(2, 9, true)]
//   [TestCase(4, 9, true)]
//   [TestCase(7, 9, true)]
//   [TestCase(9, 9, true)]
//   [TestCase(3, 9, false)]
//   [TestCase(5, 9, false)]
//   [TestCase(6, 9, false)]
//   [TestCase(8, 9, false)]
//   [TestCase(10, 9, false)]
//   [TestCase(11, 9, false)]
//   public void AddIndustrialTradeCode(int atmosphere, int population, bool expected)
//   {
//     var planet = new Planet
//     {
//       Atmosphere = atmosphere,
//       Population = population
//     };
//
//     _tradeCodeService.AddIndustrialTradeCode(planet);
//
//     Assert.That(_tradeCodeService.TradeCodes.Contains(TradeCode.Industrial), Is.EqualTo(expected));
//   }
//
//   [TestCase(0, false)]
//   [TestCase(1, true)]
//   [TestCase(3, true)]
//   [TestCase(4, false)]
//   [TestCase(7, false)]
//   [TestCase(10, false)]
//   public void AddLowPopulationTradeCode(int population, bool expected)
//   {
//     var planet = new Planet
//     {
//       Population = population
//     };
//
//     _tradeCodeService.AddLowPopulationTradeCode(planet);
//
//     Assert.That(_tradeCodeService.TradeCodes.Contains(TradeCode.LowPopulation), Is.EqualTo(expected));
//   }
//
//   [TestCase(0, true)]
//   [TestCase(1, true)]
//   [TestCase(3, true)]
//   [TestCase(5, true)]
//   [TestCase(6, false)]
//   [TestCase(8, false)]
//   [TestCase(10, false)]
//   public void AddLowTechnologyTradeCode(int techLevel, bool expected)
//   {
//     var planet = new Planet
//     {
//       TechLevel = techLevel
//     };
//
//     _tradeCodeService.AddLowTechnologyTradeCode(planet);
//
//     Assert.That(_tradeCodeService.TradeCodes.Contains(TradeCode.LowTechnology), Is.EqualTo(expected));
//   }
//
//   [TestCase(0, 0, 0, false)]
//   [TestCase(0, 0, 5, false)]
//   [TestCase(0, 0, 6, true)]
//   [TestCase(3, 3, 6, true)]
//   [TestCase(4, 3, 6, false)]
//   [TestCase(3, 4, 6, false)]
//   [TestCase(4, 4, 6, false)]
//   [TestCase(2, 2, 10, true)]
//   public void AddNonAgriculturalTradeCode(int atmosphere, int hydrographics, int population, bool expected)
//   {
//     var planet = new Planet
//     {
//       Atmosphere = atmosphere,
//       Hydrographics = hydrographics,
//       Population = population
//     };
//
//     _tradeCodeService.AddNonAgriculturalTradeCode(planet);
//
//     Assert.That(_tradeCodeService.TradeCodes.Contains(TradeCode.NonAgricultural), Is.EqualTo(expected));
//   }
//
//   [TestCase(0, false)]
//   [TestCase(1, false)]
//   [TestCase(3, false)]
//   [TestCase(4, true)]
//   [TestCase(6, true)]
//   [TestCase(7, false)]
//   [TestCase(10, false)]
//   public void AddNonIndustrialTradeCode(int population, bool expected)
//   {
//     var planet = new Planet
//     {
//       Population = population
//     };
//
//     _tradeCodeService.AddNonIndustrialTradeCode(planet);
//
//     Assert.That(_tradeCodeService.TradeCodes.Contains(TradeCode.NonIndustrial), Is.EqualTo(expected));
//   }
//
//   [TestCase(0, 0, false)]
//   [TestCase(1, 0, false)]
//   [TestCase(2, 0, true)]
//   [TestCase(5, 0, true)]
//   [TestCase(6, 0, false)]
//   [TestCase(3, 3, true)]
//   [TestCase(3, 4, false)]
//   public void AddPoorTradeCode(int atmosphere, int hydrographics, bool expected)
//   {
//     var planet = new Planet
//     {
//       Atmosphere = atmosphere,
//       Hydrographics = hydrographics
//     };
//
//     _tradeCodeService.AddPoorTradeCode(planet);
//
//     Assert.That(_tradeCodeService.TradeCodes.Contains(TradeCode.Poor), Is.EqualTo(expected));
//   }
//
//   [TestCase(0, 0, false)]
//   [TestCase(6, 0, false)]
//   [TestCase(7, 0, false)]
//   [TestCase(8, 0, false)]
//   [TestCase(6, 6, true)]
//   [TestCase(6, 7, true)]
//   [TestCase(6, 8, true)]
//   [TestCase(7, 6, false)]
//   [TestCase(7, 7, false)]
//   [TestCase(7, 8, false)]
//   [TestCase(8, 6, true)]
//   [TestCase(8, 7, true)]
//   [TestCase(8, 8, true)]
//   [TestCase(8, 9, false)]
//   public void AddRichTradeCode(int atmosphere, int population, bool expected)
//   {
//     var planet = new Planet
//     {
//       Atmosphere = atmosphere,
//       Population = population
//     };
//
//     _tradeCodeService.AddRichTradeCode(planet);
//
//     Assert.That(_tradeCodeService.TradeCodes.Contains(TradeCode.Rich), Is.EqualTo(expected));
//   }
//
//   [TestCase(0, false)]
//   [TestCase(1, false)]
//   [TestCase(3, false)]
//   [TestCase(4, false)]
//   [TestCase(7, false)]
//   [TestCase(10, true)]
//   public void AddWaterWorldTradeCode(int hydrographics, bool expected)
//   {
//     var planet = new Planet
//     {
//       Hydrographics = hydrographics
//     };
//
//     _tradeCodeService.AddWaterWorldTradeCode(planet);
//
//     Assert.That(_tradeCodeService.TradeCodes.Contains(TradeCode.WaterWorld), Is.EqualTo(expected));
//   }
//
//   [TestCase(0, true)]
//   [TestCase(1, false)]
//   [TestCase(3, false)]
//   [TestCase(4, false)]
//   [TestCase(7, false)]
//   [TestCase(10, false)]
//   public void AddVacuumTradeCode(int atmosphere, bool expected)
//   {
//     var planet = new Planet
//     {
//       Atmosphere = atmosphere
//     };
//
//     _tradeCodeService.AddVacuumTradeCode(planet);
//
//     Assert.That(_tradeCodeService.TradeCodes.Contains(TradeCode.Vacuum), Is.EqualTo(expected));
//   }
//
//   [Test]
//   public void GetTradeCodes()
//   {
//     // TODO: Finish
//     _tradeCodeService.GetTradeCodes(new Planet());
//   }
// }
//
