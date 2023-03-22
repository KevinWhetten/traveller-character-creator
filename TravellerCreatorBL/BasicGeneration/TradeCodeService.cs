using TravellerCreatorModels.Basic;

namespace TravellerCharacterCreatorBL.BasicGeneration;

public interface ITradeCodesService
{
    List<TradeCode> GetTradeCodes(Planet planet);
}

public class TradeCodeService : ITradeCodesService
{
    public List<TradeCode> TradeCodes = new();

    public List<TradeCode> GetTradeCodes(Planet planet)
    {
        TradeCodes = new List<TradeCode>();
        AddAgriculturalTradeCode(planet);
        AddAsteroidTradeCode(planet);
        AddBarrenTradeCode(planet);
        AddDesertTradeCode(planet);
        AddFluidOceansTradeCode(planet);
        AddGardenTradeCode(planet);
        AddHighPopulationTradeCode(planet);
        AddHighTechnologyTradeCode(planet);
        AddIceCappedTradeCode(planet);
        AddIndustrialTradeCode(planet);
        AddLowPopulationTradeCode(planet);
        AddLowTechnologyTradeCode(planet);
        AddNonAgriculturalTradeCode(planet);
        AddNonIndustrialTradeCode(planet);
        AddPoorTradeCode(planet);
        AddRichTradeCode(planet);
        AddWaterWorldTradeCode(planet);
        AddVacuumTradeCode(planet);
        return TradeCodes;
    }

    public void AddAgriculturalTradeCode(Planet planet)
    {
        if (planet.Atmosphere is >= 4 and <= 9
            && planet.Hydrographics is >= 4 and <= 8
            && planet.Population is >= 5 and <= 7) {
            TradeCodes.Add(TradeCode.Agricultural);
        }
    }

    public void AddAsteroidTradeCode(Planet planet)
    {
        if (planet.Size is 0 && planet.Atmosphere is 0 && planet.Hydrographics is 0) {
            TradeCodes.Add(TradeCode.Asteroid);
        }
    }

    public void AddBarrenTradeCode(Planet planet)
    {
        if (planet.Population is 0 && planet.Government is 0 && planet.LawLevel == 0) {
            TradeCodes.Add(TradeCode.Barren);
        }
    }

    public void AddDesertTradeCode(Planet planet)
    {
        if (planet.Atmosphere >= 2 && planet.Hydrographics == 0) {
            TradeCodes.Add(TradeCode.Desert);
        }
    }

    public void AddFluidOceansTradeCode(Planet planet)
    {
        if (planet.Atmosphere >= 10 && planet.Hydrographics >= 1) {
            TradeCodes.Add(TradeCode.FluidOceans);
        }
    }

    public void AddGardenTradeCode(Planet planet)
    {
        if (planet.Atmosphere >= 5
            && planet.Hydrographics is >= 4 and <= 9
            && planet.Population is >= 4 and <= 8) {
            TradeCodes.Add(TradeCode.Garden);
        }
    }

    public void AddHighPopulationTradeCode(Planet planet)
    {
        if (planet.Population >= 9) {
            TradeCodes.Add(TradeCode.HighPopulation);
        }
    }

    public void AddHighTechnologyTradeCode(Planet planet)
    {
        if (planet.TechLevel >= 12) {
            TradeCodes.Add(TradeCode.HighTechnology);
        }
    }

    public void AddIceCappedTradeCode(Planet planet)
    {
        if (planet.Atmosphere is >= 0 and <= 1
            && planet.Hydrographics >= 1) {
            TradeCodes.Add(TradeCode.IceCapped);
        }
    }

    public void AddIndustrialTradeCode(Planet planet)
    {
        if (planet.Atmosphere is >= 0 and <= 2 or 4 or 7 or 9
            && planet.Population >= 9) {
            TradeCodes.Add(TradeCode.Industrial);
        }
    }

    public void AddLowPopulationTradeCode(Planet planet)
    {
        if (planet.Population is >= 1 and <= 3) {
            TradeCodes.Add(TradeCode.LowPopulation);
        }
    }

    public void AddLowTechnologyTradeCode(Planet planet)
    {
        if (planet.TechLevel <= 5) {
            TradeCodes.Add(TradeCode.LowTechnology);
        }
    }

    public void AddNonAgriculturalTradeCode(Planet planet)
    {
        if (planet.Atmosphere is >= 0 and <= 3
            && planet.Hydrographics is >= 0 and <= 3
            && planet.Population >= 6) {
            TradeCodes.Add(TradeCode.NonAgricultural);
        }
    }

    public void AddNonIndustrialTradeCode(Planet planet)
    {
        if (planet.Population is >= 4 and <= 6) {
            TradeCodes.Add(TradeCode.NonIndustrial);
        }
    }

    public void AddPoorTradeCode(Planet planet)
    {
        if (planet.Atmosphere is >= 2 and <= 5
            && planet.Hydrographics is >= 0 and <= 3) {
            TradeCodes.Add(TradeCode.Poor);
        }
    }

    public void AddRichTradeCode(Planet planet)
    {
        if (planet.Atmosphere is 6 or 8
            && planet.Population is >= 6 and <= 8) {
            TradeCodes.Add(TradeCode.Rich);
        }
    }

    public void AddWaterWorldTradeCode(Planet planet)
    {
        if (planet.Hydrographics == 10) {
            TradeCodes.Add(TradeCode.WaterWorld);
        }
    }

    public void AddVacuumTradeCode(Planet planet)
    {
        if (planet.Atmosphere == 0) {
            TradeCodes.Add(TradeCode.Vacuum);
        }
    }
}