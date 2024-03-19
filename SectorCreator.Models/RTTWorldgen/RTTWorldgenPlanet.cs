using SectorCreator.Global;
using SectorCreator.Global.Enums;
using SectorCreator.Models.Basic;
using SectorCreator.Models.CustomTypes;
using SectorCreator.Models.Services.TradeCodeService;

namespace SectorCreator.Models.RTTWorldgen;

public class RttWorldgenPlanet : Planet
{

    public RttWorldgenPlanet(RttWorldgenPlanet rttWorldgenPlanet) : base(rttWorldgenPlanet)
    {
        Id = rttWorldgenPlanet.Id;
        Biosphere = rttWorldgenPlanet.Biosphere;
        Chemistry = rttWorldgenPlanet.Chemistry;
        Rings = rttWorldgenPlanet.Rings;
        IndustrialBase = rttWorldgenPlanet.IndustrialBase;
        WorldType = rttWorldgenPlanet.WorldType;
        PlanetOrbit = rttWorldgenPlanet.PlanetOrbit;
        OrbitPosition = rttWorldgenPlanet.OrbitPosition;
        IsMainWorld = rttWorldgenPlanet.IsMainWorld;
        ParentId = rttWorldgenPlanet.ParentId;
        StarId = rttWorldgenPlanet.StarId;
        SatelliteOrbit = rttWorldgenPlanet.SatelliteOrbit;
    }

    public RttWorldgenPlanet()
    { }

    public Guid Id { get; set; }

    public int Biosphere { get; set; }
    public PlanetChemistry Chemistry { get; set; }
    public Rings Rings { get; set; }

    public int IndustrialBase { get; set; }

    public int Desirability { get; set; }

    public WorldType WorldType { get; set; }
    public PlanetOrbit PlanetOrbit { get; set; }
    public int OrbitPosition { get; set; }
    public bool IsMainWorld { get; set; }

    public Guid ParentId { get; set; }
    public PlanetType ParentType { get; set; }
    public Guid StarId { get; set; }
    public CompanionOrbit SatelliteOrbit { get; set; }
    public int Importance { get; set; }
    public string EconomicExtension { get; set; } = $"(000+0)";
    public string CulturalExtension { get; set; } = "[0000]";
    public string Nobility { get; set; } = "";
    public string PBG;
    public string Allegiance { get; set; } = "----";

    public void SetEconomicExtension(RollingService rollingService, int gasGiantCount, int beltCount)
    {
        var resources = rollingService.D6(2) + TechLevel >= 8 ? gasGiantCount + beltCount : 0;
        var labor = Population - 1 > 0 ? Population - 1 : 0;
        var infrastructure = rollingService.D6(2) + Importance;
        if (infrastructure < 0) infrastructure = 0;
        var efficiencies = rollingService.Flux();


        EconomicExtension =
            $"({ExtendedHex.values[resources]}{ExtendedHex.values[labor]}{ExtendedHex.values[infrastructure]}{(efficiencies >= 0 ? "+" : "")}{efficiencies})";
    }

    public void SetCulturalExtension(RollingService rollingService)
    {
        var heterogeneity = Population + rollingService.Flux();
        if (heterogeneity < 0) heterogeneity = 0;
        var acceptance = Population + Importance;
        if (acceptance < 0) acceptance = 0;
        var strangeness = rollingService.Flux() + 5;
        var symbols = rollingService.Flux() + TechLevel;
        if (symbols < 0) symbols = 0;

        CulturalExtension =
            $"[{ExtendedHex.values[heterogeneity]}{ExtendedHex.values[acceptance]}{ExtendedHex.values[strangeness]}{ExtendedHex.values[symbols]}]";
    }

    public void SetImportance()
    {
        Importance += Starport switch {
            'A' or 'B' => 1,
            'D' or 'E' or 'X' => -1,
            _ => 0
        };

        Importance += TechLevel switch {
            >= 16 => 2,
            >= 10 => 1,
            <= 8 => -1,
            _ => 0
        };

        if (TradeCodes.Contains("Ag")) Importance++;
        if (TradeCodes.Contains("Hi")) Importance++;
        if (TradeCodes.Contains("In")) Importance++;
        if (TradeCodes.Contains("Ri")) Importance++;

        Importance -= Population <= 6 ? 1 : 0;

        if (Bases.Contains(Base.Naval) && Bases.Contains(Base.Scout)) Importance++;
        if (Bases.Contains(Base.WayStation)) Importance++;
    }

    public void SetNobility()
    {
        if (Population > 0) {
            Nobility += "B";
        }

        if (TradeCodes.Contains("Pa") || TradeCodes.Contains("Pr")) {
            Nobility += "c";
        }

        if (TradeCodes.Contains("Ag") || TradeCodes.Contains("Ri")) {
            Nobility += "C";
        }

        if (TradeCodes.Contains("Pi")) {
            Nobility += "D";
        }

        if (TradeCodes.Contains("Ph")) {
            Nobility += "e";
        }

        if (TradeCodes.Contains("In") || TradeCodes.Contains("Hi")) {
            Nobility += "E";
        }

        if (Importance >= 4 && !TradeCodes.Contains("Cp") && !TradeCodes.Contains("Cs") && !TradeCodes.Contains("Cx")) {
            Nobility += "f";
        }

        if (TradeCodes.Contains("Cp") || TradeCodes.Contains("Cs")) {
            Nobility += "F";
        }

        if (TradeCodes.Contains("Cx")) {
            Nobility += "G";
        }

        if (Nobility == "") Nobility = "-";
    }

    public void SetPBG(int population, int belts, int giants)
    {
        if (Population == 0) {
            population = 0;
        }
        PBG = $"{ExtendedHex.values[population]}{ExtendedHex.values[belts]}{ExtendedHex.values[giants]}";
    }
}