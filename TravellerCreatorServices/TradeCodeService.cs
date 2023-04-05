using TravellerCreatorGlobalMethods;
using TravellerCreatorModels.SectorCreator.Enums;
using TravellerCreatorModels.SectorCreator.Interfaces;
using TravellerCreatorModels.SectorCreator.RTTWorldgen;

namespace TravellerCreatorServices;

public interface ITradeCodesService
{
    List<TradeCode> GetTradeCodes(IPlanet planet);
}

public class TradeCodeService : ITradeCodesService
{
    public List<TradeCode> TradeCodes = new();

    public List<TradeCode> GetTradeCodes(IPlanet planet)
    {
        TradeCodes = new List<TradeCode>();
        // Planetary
        AddAsteroidTradeCode(planet);
        AddDesertTradeCode(planet);
        AddFluidOceansTradeCode(planet);
        AddGardenTradeCode(planet);
        AddHellworldTradeCode(planet);
        AddIceCappedTradeCode(planet);
        AddOceanWorldTradeCode(planet);
        AddVacuumTradeCode(planet);
        AddWaterWorldTradeCode(planet);

        // Population
        AddDiebackTradeCode(planet);
        AddBarrenTradeCode(planet);
        AddLowPopulationTradeCode(planet);
        AddNonIndustrialTradeCode(planet);
        AddPreHighPopulationTradeCode(planet);
        AddHighPopulationTradeCode(planet);

        // Economic
        AddPreAgriculturalTradeCode(planet);
        AddAgriculturalTradeCode(planet);
        AddNonAgriculturalTradeCode(planet);
        AddPreIndustrializedTradeCode(planet);
        AddIndustrialTradeCode(planet);
        AddPoorTradeCode(planet);
        AddPreRichTradeCode(planet);
        AddRichTradeCode(planet);

        // Climate
        AddFrozenTradeCode(planet);
        AddHotTradeCode(planet);
        AddColdTradeCode(planet);
        AddLockedTradeCode(planet);
        AddTropicTradeCode(planet);
        AddTundraTradeCode(planet);
        AddTwilightZoneTradeCode(planet);

        // Secondary
        AddFarmingTradeCode(planet);
        AddMiningTradeCode(planet);
        AddCaptiveTradeCode(planet);
        AddReserveTradeCode(planet);

        // Political
        AddSubsectorCapitalTradeCode(planet);
        AddSectorCapitalTradeCode(planet);
        AddCapitalTradeCode(planet);
        AddColonyTradeCode(planet);

        // Special
        AddSatelliteTradeCode(planet);
        AddForbiddenTradeCode(planet);
        AddAmberTradeCode(planet);
        AddDataRepositoryTradeCode(planet);
        AddAncientSiteTradeCode(planet);
        AddResearchStationTradeCode(planet);

        // Other
        AddHighTechnologyTradeCode(planet);
        AddLowTechnologyTradeCode(planet);
        return TradeCodes;
    }

    #region Planetary

    public void AddAsteroidTradeCode(IPlanet planet)
    {
        if (planet.Size is 0 && planet.Atmosphere is 0 && planet.Hydrographics is 0) {
            TradeCodes.Add(TradeCode.Asteroid);
        }
    }

    public void AddDesertTradeCode(IPlanet planet)
    {
        if (planet.Atmosphere >= 2 && planet.Hydrographics == 0) {
            TradeCodes.Add(TradeCode.Desert);
        }
    }

    public void AddFluidOceansTradeCode(IPlanet planet)
    {
        if (planet.Atmosphere >= 10 && planet.Hydrographics >= 1) {
            TradeCodes.Add(TradeCode.FluidOceans);
        }
    }

    public void AddGardenTradeCode(IPlanet planet)
    {
        if (planet.Atmosphere >= 5
            && planet.Hydrographics is >= 4 and <= 9
            && planet.Population is >= 4 and <= 8) {
            TradeCodes.Add(TradeCode.Garden);
        }
    }

    private void AddHellworldTradeCode(IPlanet planet)
    {
        if (planet.Size >= 3
            && planet.Atmosphere is 2 or 4 or 7 or >= 9 and <= 12
            && planet.Hydrographics is >= 0 and <= 2) {
            planet.TradeCodes.Add(TradeCode.Hellworld);
        }
    }

    public void AddIceCappedTradeCode(IPlanet planet)
    {
        if (planet.Atmosphere is >= 0 and <= 1
            && planet.Hydrographics >= 1) {
            TradeCodes.Add(TradeCode.IceCapped);
        }
    }

    private void AddOceanWorldTradeCode(IPlanet planet)
    {
        if (planet.Atmosphere is 0 or 1
            && planet.Hydrographics >= 10) {
            planet.TradeCodes.Add(TradeCode.OceanWorld);
        }
    }

    public void AddVacuumTradeCode(IPlanet planet)
    {
        if (planet.Atmosphere == 0) {
            TradeCodes.Add(TradeCode.Vacuum);
        }
    }

    public void AddWaterWorldTradeCode(IPlanet planet)
    {
        if (planet.Hydrographics == 10) {
            TradeCodes.Add(TradeCode.WaterWorld);
        }
    }

    #endregion

    #region Population

    private void AddDiebackTradeCode(IPlanet planet)
    {
        if (planet.Population == 0 && planet.Government == 0 && planet.LawLevel == 0 && planet.TechLevel >= 1) {
            planet.TradeCodes.Add(TradeCode.Dieback);
        }
    }

    public void AddBarrenTradeCode(IPlanet planet)
    {
        if (planet.Population is 0 && planet.Government is 0 && planet.LawLevel == 0) {
            TradeCodes.Add(TradeCode.Barren);
        }
    }

    public void AddLowPopulationTradeCode(IPlanet planet)
    {
        if (planet.Population is >= 1 and <= 3) {
            TradeCodes.Add(TradeCode.LowPopulation);
        }
    }

    public void AddNonIndustrialTradeCode(IPlanet planet)
    {
        if (planet.Population is >= 4 and <= 6) {
            TradeCodes.Add(TradeCode.NonIndustrial);
        }
    }

    private void AddPreHighPopulationTradeCode(IPlanet planet)
    {
        if (planet.Population == 8) {
            planet.TradeCodes.Add(TradeCode.PreHighPopulation);
        }
    }

    public void AddHighPopulationTradeCode(IPlanet planet)
    {
        if (planet.Population >= 9) {
            TradeCodes.Add(TradeCode.HighPopulation);
        }
    }

    #endregion

    #region Economic

    private void AddPreAgriculturalTradeCode(IPlanet planet)
    {
        if (planet.Atmosphere is >= 4 and <= 9
            && planet.Hydrographics is >= 4 and <= 8
            && planet.Population is 4 or 8) {
            planet.TradeCodes.Add(TradeCode.PreAgricultural);
        }
    }

    public void AddAgriculturalTradeCode(IPlanet planet)
    {
        if (planet.Atmosphere is >= 4 and <= 9
            && planet.Hydrographics is >= 4 and <= 8
            && planet.Population is >= 5 and <= 7) {
            TradeCodes.Add(TradeCode.Agricultural);
        }
    }

    public void AddNonAgriculturalTradeCode(IPlanet planet)
    {
        if (planet.Atmosphere is >= 0 and <= 3
            && planet.Hydrographics is >= 0 and <= 3
            && planet.Population >= 6) {
            TradeCodes.Add(TradeCode.NonAgricultural);
        }
    }

    private void AddPreIndustrializedTradeCode(IPlanet planet)
    {
        if (planet.Atmosphere is 0 or 1 or 2 or 4 or 7 or 9
            && planet.Population is 7 or 8) {
            planet.TradeCodes.Add(TradeCode.PreIndustrial);
        }
    }

    public void AddIndustrialTradeCode(IPlanet planet)
    {
        if (planet.Atmosphere is >= 0 and <= 2 or 4 or 7 or 9
            && planet.Population >= 9) {
            TradeCodes.Add(TradeCode.Industrial);
        }
    }

    public void AddPoorTradeCode(IPlanet planet)
    {
        if (planet.Atmosphere is >= 2 and <= 5
            && planet.Hydrographics is >= 0 and <= 3) {
            TradeCodes.Add(TradeCode.Poor);
        }
    }

    private void AddPreRichTradeCode(IPlanet planet)
    {
        if (planet.Atmosphere is 6 or 8
            && planet.Population is 5 or 9) {
            planet.TradeCodes.Add(TradeCode.PreRich);
        }
    }

    public void AddRichTradeCode(IPlanet planet)
    {
        if (planet.Atmosphere is 6 or 8
            && planet.Population is >= 6 and <= 8) {
            TradeCodes.Add(TradeCode.Rich);
        }
    }

    #endregion

    #region Climate

    private void AddFrozenTradeCode(IPlanet planet)
    {
        if (planet.Temperature <= 2
            || planet is RTTWorldgenPlanet {
                Size: >= 2 and <= 9,
                Hydrographics: >= 1,
                PlanetOrbit: PlanetOrbit.Outer
            }) {
            planet.TradeCodes.Add(TradeCode.Frozen);
        }
    }

    private void AddHotTradeCode(IPlanet planet)
    {
        if (planet.Temperature >= 10
            || planet is RTTWorldgenPlanet {
                Size: >= 2 and <= 9,
                PlanetOrbit: PlanetOrbit.Epistellar
            }) {
            planet.TradeCodes.Add(TradeCode.Hot);
        }
    }

    private void AddColdTradeCode(IPlanet planet)
    {
        if (planet.Temperature is >= 3 and <= 5) {
            planet.TradeCodes.Add(TradeCode.Cold);
        }
    }

    private void AddLockedTradeCode(IPlanet planet)
    {
        if (planet is RTTWorldgenPlanet worldgenPlanet
            && worldgenPlanet.ParentId != Guid.Empty
            && worldgenPlanet.SatelliteOrbit == CompanionOrbit.Close) {
            planet.TradeCodes.Add(TradeCode.Locked);
        }
    }

    private void AddTropicTradeCode(IPlanet planet)
    {
        if (planet.Size is >= 6 and <= 9
            && planet.Atmosphere is >= 4 and <= 9
            && planet.Hydrographics is >= 3 and <= 7
            && planet.Temperature >= 8) {
            planet.TradeCodes.Add(TradeCode.Tropic);
        }
    }

    private void AddTundraTradeCode(IPlanet planet)
    {
        if (planet.Size is >= 6 and <= 9
            && planet.Atmosphere is >= 4 and <= 9
            && planet.Hydrographics is >= 3 and <= 7
            && planet.Temperature <= 5) {
            planet.TradeCodes.Add(TradeCode.Tundra);
        }
    }

    private void AddTwilightZoneTradeCode(IPlanet planet)
    {
        if (planet is RTTWorldgenPlanet {OrbitPosition: <= 1}) {
            planet.TradeCodes.Add(TradeCode.TwilightZone);
        }
    }

    #endregion

    #region Secondary

    private void AddFarmingTradeCode(IPlanet planet)
    {
        if (planet is RTTWorldgenPlanet {
                Atmosphere: >= 4 and <= 9,
                Hydrographics: >= 4 and <= 8,
                Population: >= 2 and <= 6,
                PlanetOrbit: PlanetOrbit.Inner,
                IsMainWorld: false
            }) {
            planet.TradeCodes.Add(TradeCode.Farming);
        }
    }

    private void AddMiningTradeCode(IPlanet planet)
    {
        if (planet is RTTWorldgenPlanet {
                Population: >= 2 and <= 6,
                IsMainWorld: false,
                PlanetType: PlanetType.AsteroidBelt
            }) {
            planet.TradeCodes.Add(TradeCode.Mining);
        }
    }

    private void AddCaptiveTradeCode(IPlanet planet)
    {
        if (planet.Government != 6) return;
        
        TradeCode? captiveType = planet switch {
            RTTWorldgenPlanet worldgenPlanet =>
                Roll.D6(1) switch {
                    <= 3 => TradeCode.MilitaryRule,
                    >= 4 => worldgenPlanet.IsMainWorld ? TradeCode.PrisonCamp : TradeCode.PenalColony
                },
            _ => Roll.D3(1) switch {
                1 => TradeCode.MilitaryRule,
                2 => TradeCode.PrisonCamp,
                3 => TradeCode.PenalColony,
                _ => null
            }
        };

        if (captiveType != null) {
            planet.TradeCodes.Add(captiveType.Value);
        }
    }

    private void AddReserveTradeCode(IPlanet planet)
    {
        if (planet.TechLevel <= 5
            && planet.Starport == 'X'
            && planet.Population >= 1) {
            if (planet is RTTWorldgenPlanet {Biosphere: >= 7} or not RTTWorldgenPlanet) {
                planet.TradeCodes.Add(TradeCode.Reserve);
            }
        }
    }

    #endregion

    #region Political

    private void AddSubsectorCapitalTradeCode(IPlanet planet)
    { }

    private void AddSectorCapitalTradeCode(IPlanet planet)
    { }

    private void AddCapitalTradeCode(IPlanet planet)
    {
        if (planet is RTTWorldgenPlanet {
                Biosphere: >= 12
            }) {
            planet.TradeCodes.Add(TradeCode.Capital);
        }
    }

    private void AddColonyTradeCode(IPlanet planet)
    { }

    #endregion

    #region Special

    private void AddSatelliteTradeCode(IPlanet planet)
    {
        if (planet is RTTWorldgenPlanet worldgenPlanet
            && worldgenPlanet.ParentId != Guid.Empty) {
            planet.TradeCodes.Add(TradeCode.Satellite);
        }
    }

    private void AddForbiddenTradeCode(IPlanet planet)
    {
        if (planet.TravelCode == TravelCode.Red) {
            planet.TradeCodes.Add(TradeCode.Forbidden);
        }
    }

    private void AddAmberTradeCode(IPlanet planet)
    {
        if (planet.TravelCode == TravelCode.Amber) {
            planet.TradeCodes.Add(TradeCode.Danger);
        } else if (Roll.D6(2) == 12) {
            planet.TradeCodes.Add(TradeCode.Puzzle);
        }
    }

    private void AddDataRepositoryTradeCode(IPlanet planet)
    {
        if (Roll.D6(3) == 18) {
            planet.TradeCodes.Add(TradeCode.DataRepository);
            planet.Bases.Add(Base.DataRepository);
        }
    }

    private void AddAncientSiteTradeCode(IPlanet planet)
    {
        if (Roll.D6(3) == 18) {
            planet.TradeCodes.Add(TradeCode.AncientSite);
            planet.Bases.Add(Base.AncientSite);
        }
    }

    private void AddResearchStationTradeCode(IPlanet planet)
    {
        int roll = Roll.D6(2);
        switch (planet.Starport) {
            case 'A':
                if (roll >= 6) {
                    planet.TradeCodes.Add(TradeCode.ResearchStation);
                    planet.Bases.Add(Base.Research);
                }

                if (roll >= 9) {
                    planet.Bases.Add(Base.Shipyard);
                }

                if (roll >= 12) {
                    planet.Bases.Add(Base.MegaCorporateHeadquarters);
                }

                break;
            case 'B':
                if (roll >= 8) {
                    planet.TradeCodes.Add(TradeCode.ResearchStation);
                    planet.Bases.Add(Base.Research);
                }

                if (roll >= 11) {
                    planet.Bases.Add(Base.Shipyard);
                }
                break;
            case 'C':
                if (roll >= 10) {
                    planet.TradeCodes.Add(TradeCode.ResearchStation);
                    planet.Bases.Add(Base.Research);
                }

                break;
        }
    }

    #endregion

    #region Other

    public void AddHighTechnologyTradeCode(IPlanet planet)
    {
        if (planet.TechLevel >= 12) {
            TradeCodes.Add(TradeCode.HighTechnology);
        }
    }

    public void AddLowTechnologyTradeCode(IPlanet planet)
    {
        if (planet.TechLevel <= 5) {
            TradeCodes.Add(TradeCode.LowTechnology);
        }
    }

    #endregion
}