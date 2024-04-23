using SectorCreator.Global.Enums;
using SectorCreator.Models.Basic;

namespace SectorCreator.WorldBuilder.Planet.Planet;

public partial class WorldBuilderPlanet
{
    public int TechLevel { get; set; }
    public int LowCommonTechLevel { get; set; }
    public int EnergyTechLevel { get; set; }
    public int ElectronicsTechLevel { get; set; }
    public int ManufacturingTechLevel { get; set; }
    public int MedicalTechLevel { get; set; }
    public int EnvironmentalTechLevel { get; set; }
    public int LandTransportTechLevel { get; set; }
    public int WaterTransportTechLevel { get; set; }
    public int AirTransportTechLevel { get; set; }
    public int PersonalMilitaryTechLevel { get; set; }
    public int HeavyMilitaryTechLevel { get; set; }

    private void GenerateTechLevel()
    {
        TechLevel = _rollingService.D6(1) + (CurrentSophontExists ? _rollingService.D6(1) + 2 : 0);

        TechLevel += Starport.Class switch {
            StarportClass.A => 6,
            StarportClass.B => 4,
            StarportClass.C => 2,
            StarportClass.D => 0,
            StarportClass.E => 0,
            StarportClass.X => -4,
            StarportClass.F => 1,
            StarportClass.G => 0,
            StarportClass.H => 0,
            StarportClass.Y => -4,
            _ => throw new ArgumentOutOfRangeException()
        };

        TechLevel += Size switch {
            <= 1 => 2,
            <= 4 => 1,
            _ => 0
        };
        
        TechLevel += Atmosphere switch {
            <= 3 => 1,
            <= 9 => 0,
            >= 10 => 1
        };
        
        TechLevel += Hydrographics switch {
            <= 0 => 1,
            <= 8 => 0,
            9 => 1,
            >= 10 => 2
        };

        TechLevel += Population switch {
            <= 5 => 1,
            <= 7 => 0,
            8 => 1,
            9 => 2,
            >= 10 => 4
        };

        TechLevel += Government switch {
            0 or 5 => 1,
            7 => 2,
            13 or 14 => -2,
            _ => 0
        };

        TechLevel = Math.Max(TechLevel, 0);
    }

    private void generateTechLevel()
    {
        GenerateLowCommonTechLevel();
        GenerateBalkanizedGovernmentTechLevels();
        GenerateQualityOfLifeTechLevels();
        GenerateTransportationTechLevels();
        GenerateMilitaryTechLevels();
    }

    private int GetTechLevelModifier()
    {
        return _rollingService.D6(2) switch {
            2 or 12 => -3,
            3 or 11 => -2,
            4 or 10 => -1,
            <= 9 => 0,
            _ => throw new ArgumentOutOfRangeException()
        };
    }

    private void GenerateLowCommonTechLevel()
    {
        var dm = Population switch {
            <= 5 => 1,
            >= 9 => -1,
            _ => 0
        };

        dm += Government switch {
            0 or 6 or 13 or 14 => -1,
            5 => 1,
            7 => -2,
            _ => 0
        };

        dm += PopulationConcentrationRating switch {
            <= 2 => -1,
            >= 7 => 1,
            _ => 0
        };

        LowCommonTechLevel = Math.Min(TechLevel + GetTechLevelModifier() + dm, TechLevel);
        LowCommonTechLevel = (int) Math.Max(LowCommonTechLevel, Math.Floor(TechLevel / 2.0));
    }

    private void GenerateBalkanizedGovernmentTechLevels()
    {
        if (Government == 7) {
            Governments.First().TechLevel = TechLevel;

            foreach (var government in Governments.Where(government => government.TechLevel == 0)) {
                var dm = government.Code switch {
                    5 => 2,
                    0 or 6 or 13 or 14 => -2,
                    _ => 0
                };

                government.TechLevel = Math.Min(TechLevel - 2 + GetTechLevelModifier() + dm, TechLevel);
                government.TechLevel = Math.Max(government.TechLevel, LowCommonTechLevel);

                dm += PopulationConcentrationRating switch {
                    >= 7 => 1,
                    _ => 0
                };

                government.LowCommonTechLevel = Math.Min(LowCommonTechLevel + dm, government.TechLevel);
                government.LowCommonTechLevel = Math.Max(government.LowCommonTechLevel, LowCommonTechLevel);
            }
        }
    }

    private void GenerateQualityOfLifeTechLevels()
    {
        GenerateEnergyTechLevel();
        GenerateElectronicsTechLevel();
        GenerateManufacturingTechLevel();
        GenerateMedicalTechLevel();
        GenerateEnvironmentalTechLevel();
    }

    private void GenerateEnergyTechLevel()
    {
        var dm = Population >= 9 ? 1 : 0;
        if (TradeCodes.Contains(TradeCode.Industrial)) dm++;

        EnergyTechLevel = (int) Math.Min(TechLevel + GetTechLevelModifier() + dm, Math.Floor(TechLevel * 1.2));
        EnergyTechLevel = (int) Math.Max(EnergyTechLevel, Math.Floor(TechLevel / 2.0));
    }

    private void GenerateElectronicsTechLevel()
    {
        var dm = Population switch {
            <= 5 => 1,
            >= 9 => -1,
            _ => 0
        };
        if (TradeCodes.Contains(TradeCode.Industrial)) dm++;

        ElectronicsTechLevel = Math.Min(TechLevel + GetTechLevelModifier() + dm, EnergyTechLevel + 1);
        ElectronicsTechLevel = Math.Max(ElectronicsTechLevel, EnergyTechLevel - 3);
    }

    private void GenerateManufacturingTechLevel()
    {
        var dm = Population switch {
            <= 6 => -1,
            >= 8 => 1,
            _ => 0
        };
        if (TradeCodes.Contains(TradeCode.Industrial)) dm++;

        ManufacturingTechLevel = Math.Min(TechLevel + GetTechLevelModifier() + dm, Math.Max(EnergyTechLevel, ElectronicsTechLevel));
        ManufacturingTechLevel = Math.Max(ManufacturingTechLevel, ElectronicsTechLevel - 2);
    }

    private void GenerateMedicalTechLevel()
    {
        var dm = 0;

        if (TradeCodes.Contains(TradeCode.Rich)) dm++;
        if (TradeCodes.Contains(TradeCode.Poor)) dm--;

        MedicalTechLevel = Math.Min(ElectronicsTechLevel + GetTechLevelModifier() + dm, ElectronicsTechLevel);
        MedicalTechLevel = Math.Max(MedicalTechLevel, Starport.Class switch {
            StarportClass.A => 6,
            StarportClass.B => 4,
            StarportClass.C => 2,
            _ => 0
        });
    }

    private void GenerateEnvironmentalTechLevel()
    {
        var dm = HabitabilityRating < 8 ? 8 - HabitabilityRating : 0;

        EnvironmentalTechLevel = Math.Min(ManufacturingTechLevel + GetTechLevelModifier() + dm, EnergyTechLevel);
        EnvironmentalTechLevel = Math.Max(EnvironmentalTechLevel, EnergyTechLevel - 5);
    }

    private void GenerateTransportationTechLevels()
    {
        GenerateLandTransportTechLevel();
        GenerateWaterTransportTechLevel();
        GenerateAirTransportTechLevel();
    }

    private void GenerateLandTransportTechLevel()
    {
        var dm = 0;

        if (Hydrographics == 10) dm--;
        if (PopulationConcentrationRating <= 2) dm++;

        LandTransportTechLevel = Math.Min(EnergyTechLevel + GetTechLevelModifier() + dm, EnergyTechLevel);
        LandTransportTechLevel = Math.Max(LandTransportTechLevel, ElectronicsTechLevel - 5);
    }

    private void GenerateWaterTransportTechLevel()
    {
        var dm = Hydrographics switch {
            0 => -2,
            8 => 2,
            >= 9 => 2,
            _ => 0
        };

        if (PopulationConcentrationRating <= 2) dm++;

        WaterTransportTechLevel = Math.Min(EnergyTechLevel + GetTechLevelModifier() + dm, EnergyTechLevel);
        WaterTransportTechLevel = Math.Max(WaterTransportTechLevel, Hydrographics == 0 ? 0 : ElectronicsTechLevel - 5);
    }

    private void GenerateAirTransportTechLevel()
    {
        var dm = TechLevel <= 7
            ? Atmosphere switch {
                <= 3 or 14 => -2,
                <= 5 => -1,
                _ => 0
            }
            : Atmosphere  is 4 or 5 ? 1 : 0;

        var max = Atmosphere switch {
            <= 1 => EnergyTechLevel - 3,
            <= 3 => EnergyTechLevel - 2,
            <= 5 => EnergyTechLevel - 1,
            <= 7 => EnergyTechLevel,
            <= 9 => EnergyTechLevel + 1,
            13 => EnergyTechLevel + 2,
            _ => EnergyTechLevel
        };
        var min = Atmosphere switch {
            <= 1 => 0,
            <= 3 => EnergyTechLevel - 7,
            <= 5 => EnergyTechLevel - 6,
            <= 7 => EnergyTechLevel - 5,
            <= 9 => EnergyTechLevel - 4,
            13 => EnergyTechLevel - 3,
            _ => EnergyTechLevel - 5
        };

        if (TechLevel < 8 && Atmosphere == 0) {
            max = 0;
            min = 0;
        }
        
        AirTransportTechLevel = Math.Min(EnergyTechLevel + GetTechLevelModifier() + dm, max);
        AirTransportTechLevel = Math.Max(AirTransportTechLevel, min);
    }

    private void GenerateMilitaryTechLevels()
    {
        GeneratePersonalMilitaryTechLevel();
        GenerateHeavyMilitaryTechLevel();
    }

    private void GeneratePersonalMilitaryTechLevel()
    {
        var dm = Government switch {
            0 or 7 => 2,
            _ => 0
        };
        
        dm += LawLevel switch {
            0 or >= 13 => 2,
            <= 4 or >= 9 => 1,
            _ => 0
        };

        PersonalMilitaryTechLevel = Math.Min(ManufacturingTechLevel + GetTechLevelModifier() + dm, ElectronicsTechLevel);
        PersonalMilitaryTechLevel = Math.Max(PersonalMilitaryTechLevel, LawLevel == 0 ? ManufacturingTechLevel : 0);
    }

    private void GenerateHeavyMilitaryTechLevel()
    {
        var dm = Population switch {
            <= 6 => -1,
            7 => 0,
            >= 8 => 1
        };

        dm += Government switch {
            7 or 10 or 11 or 15 => 2,
            _ => 0
        };

        dm += LawLevel > 13 ? 2 : 0;
        if (TradeCodes.Contains(TradeCode.Industrial)) dm++;

        HeavyMilitaryTechLevel = Math.Min(ManufacturingTechLevel + GetTechLevelModifier() + dm, ManufacturingTechLevel);
        HeavyMilitaryTechLevel = Math.Max(HeavyMilitaryTechLevel, 0);
    }
}