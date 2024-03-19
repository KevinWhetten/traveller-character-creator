using SectorCreator.Global.Enums;
using SectorCreator.Models.Basic;

namespace SectorCreator.Models.Services.TradeCodeService;

public partial class TradeCodeService
{
    public void AddAsteroidTradeCode(Planet planet)
    {
        if (planet.Size is 0 && planet.Atmosphere is 0 && planet.Hydrographics is 0)
        {
            planet.TradeCodes.Add(TradeCode.Asteroid);
        }
    }

    public void AddDesertTradeCode(Planet planet)
    {
        if (planet is { Atmosphere: >= 2, Hydrographics: 0 })
        {
            planet.TradeCodes.Add(TradeCode.Desert);
        }
    }

    public void AddFluidOceansTradeCode(Planet planet)
    {
        if (planet is { Atmosphere: >= 10, Hydrographics: >= 1 })
        {
            planet.TradeCodes.Add(TradeCode.FluidOceans);
        }
    }

    public void AddGardenTradeCode(Planet planet)
    {
        if (planet is { Atmosphere: >= 5, Hydrographics: >= 4 and <= 9, Population: >= 4 and <= 8 })
        {
            planet.TradeCodes.Add(TradeCode.Garden);
        }
    }

    public void AddHellworldTradeCode(Planet planet)
    {
        if (planet is { Size: >= 3, Atmosphere: 2 or 4 or 7 or >= 9 and <= 12, Hydrographics: >= 0 and <= 2 })
        {
            planet.TradeCodes.Add(TradeCode.Hellworld);
        }
    }

    public void AddIceCappedTradeCode(Planet planet)
    {
        if (planet.Atmosphere is >= 0 and <= 1
            && planet.Hydrographics >= 1)
        {
            planet.TradeCodes.Add(TradeCode.IceCapped);
        }
    }

    public void AddOceanWorldTradeCode(Planet planet)
    {
        if (planet.Atmosphere is 0 or 1
            && planet.Hydrographics >= 10)
        {
            planet.TradeCodes.Add(TradeCode.OceanWorld);
        }
    }

    public void AddVacuumTradeCode(Planet planet)
    {
        if (planet.Atmosphere == 0)
        {
            planet.TradeCodes.Add(TradeCode.Vacuum);
        }
    }

    public void AddWaterWorldTradeCode(Planet planet)
    {
        if (planet.Hydrographics is >= 10 and <= 15)
        {
            planet.TradeCodes.Add(TradeCode.WaterWorld);
        }
    }
}