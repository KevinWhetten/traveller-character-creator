using SectorCreator.Global;
using SectorCreator.Global.Enums;
using SectorCreator.Models.CustomTypes;
using SectorCreator.WorldBuilder.Enums;
using SectorCreator.WorldBuilder.Planet.AsteroidBelt;
using SectorCreator.WorldBuilder.Planet.GasGiant;
using SectorCreator.WorldBuilder.Planet.Planet.OtherObjects;
using SectorCreator.WorldBuilder.Planet.TerrestrialPlanet;
using SectorCreator.WorldBuilder.Services;

namespace SectorCreator.WorldBuilder.Planet.Planet;

public partial class WorldBuilderPlanet
{
    protected readonly IRollingService _rollingService = new RollingService();
    public Guid Id = Guid.NewGuid();
    public string Name { get; set; } = "";
    public Coordinates Coordinates { get; set; } = new();
    public string PBG => $"{PValue}{ExtendedHex.values[Belts]}{ExtendedHex.values[GasGiants]}";
    public bool IsMoon { get; set; }
    public PlanetType PlanetType { get; set; }
    public string Primary { get; set; } = "";
    public PlanetAnomaly Anomaly { get; set; }

    public int Belts { get; set; }

    public int GasGiants { get; set; }

    public string SAH => PlanetType switch {
        PlanetType.Jovian => ((WorldBuilderGasGiantPlanet) this).SAH,
        _ => $"{ExtendedHex.values[Size]}{ExtendedHex.values[Atmosphere]}{ExtendedHex.values[Hydrographics]}"
    };

    public string ModifiedSAH => PlanetType switch {
        PlanetType.Jovian => $"GGA",
        _ => $"{ExtendedHex.values[Size]}{ExtendedHex.values[Atmosphere]}{ExtendedHex.values[Hydrographics]}"
    };

    public string ModifiedUWP =>
        $"{Starport.Class}{ModifiedSAH}{ExtendedHex.values[Population]}{ExtendedHex.values[Government]}{ExtendedHex.values[LawLevel]}-{ExtendedHex.values[TechLevel]}";

    public string UWP =>
        $"{Starport.Class}{SAH}{ExtendedHex.values[Population]}{ExtendedHex.values[Government]}{ExtendedHex.values[LawLevel]}-{ExtendedHex.values[TechLevel]}";

    public string Details =>
        $"{Coordinates}\t{Name}\t{ModifiedUWP}\t{string.Join(" ", TradeCodes)}\t{ImportanceExtension}\t{EconomicExtension}\t{CulturalExtension}\t{Nobility}\t{string.Join(" ", Bases)}\t{(char) TravelZone}\t{PBG}\t{Worlds}\t{Allegiance}\t{StellarData}\n";

    public string StellarData { get; set; } = "";
    public string Allegiance { get; set; } = "----";
    public int Worlds { get; set; } = 0;
    public List<string?> Bases { get; set; } = new();
    public string Nobility => "-";
    public string Object { get; set; }

    public void GenerateObject(int num)
    {
        Object = PlanetType switch {
            PlanetType.AsteroidBelt => Primary + " P" + RomanNumeralService.values[num],
            _ => Primary + " " + RomanNumeralService.values[num]
        };
    }

    public void GenerateNotes(double starHzco)
    {
        Notes = PlanetType switch {
            PlanetType.AsteroidBelt => ((WorldBuilderAsteroidBelt) this).GetNotes(),
            PlanetType.Jovian => ((WorldBuilderGasGiantPlanet) this).GetNotes(starHzco),
            PlanetType.Terrestrial => ((WorldBuilderTerrestrialPlanet) this).GetNotes(starHzco),
        };
    }

    public string Notes { get; set; }

    public void GeneratePlanet(WorldBuilderHex hex, WorldBuilderStarSystem starSystem)
    {
        GasGiants = hex.GasGiantQuantity;
        Belts = hex.BeltQuantity;

        GenerateSizeCharacteristics(starSystem);
        GenerateBasicCharacteristics(starSystem);
        GenerateTemperature(starSystem);
        GenerateAtmosphereDetails(starSystem);
        GenerateHydrographicsDetails(starSystem);
        GenerateMoons(starSystem);
        GenerateBasicRotationDetails(starSystem);
        GenerateTidalForces(starSystem);
        GenerateTemperatureCharacteristics(starSystem);
        GenerateSeismicActivity(starSystem);
        GenerateBiology(starSystem);
        GenerateResources();
        GenerateHabitability();
        GenerateChemicalMakeup();

        if (CurrentSophontExists) {
            GeneratePopulation();
            GenerateGovernment();
            generateTechLevel();
            GenerateCulture();
            GenerateStarportDetails();
            GenerateEconomics();
            GenerateAdvancedStarportDetails();
            GenerateMilitary();
            GenerateTravelZone(starSystem);
        }
    }

    private void GenerateBasicCharacteristics(WorldBuilderStarSystem starSystem)
    {
        switch (PlanetType) {
            case PlanetType.AsteroidBelt:
                ((WorldBuilderAsteroidBelt) this).GenerateBasicCharacteristics();
                break;
            case PlanetType.Jovian:
                ((WorldBuilderGasGiantPlanet) this).GenerateBasicCharacteristics();
                break;
            case PlanetType.Terrestrial:
                ((WorldBuilderTerrestrialPlanet) this).GenerateBasicCharacteristics(starSystem);
                break;
        }
    }

    private void GenerateChemicalMakeup()
    {
        if (Atmosphere > 0) {
            var maxEscapeValue = (1000 * Mass) / ((Diameter / 12742.0) * Temperature);

            foreach (var element in CompositionService.Elements) {
                AddChemical(element, maxEscapeValue);
            }

            GenerateAtmosphereMakeup();

            if (Hydrographics == 0) {
                LiquidChemicals = new();
            } else {
                GenerateLiquidMakeup();
            }
        }
    }

    private void AddChemical(Element element, double maxEscapeValue)
    {
        if (element.Name == "Carbon Monoxide" &&
            (AtmosphereChemicals.Any(x => x.Element.Name == "Water")
             || LiquidChemicals.Any(x => x.Element.Name == "Water"))) {
            return;
        }

        if (maxEscapeValue >= element.EscapeValue) {
            if (Temperature >= element.BoilingPoint) {
                AtmosphereChemicals.Add(new PlanetElement {Element = element});
            } else if (Temperature >= element.MeltingPoint) {
                LiquidChemicals.Add(new PlanetElement {Element = element});
            }
        }
    }
}