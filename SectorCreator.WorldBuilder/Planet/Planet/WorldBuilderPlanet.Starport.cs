using SectorCreator.Global.Enums;
using SectorCreator.Models.Basic;

namespace SectorCreator.WorldBuilder.Planet.Planet;

public partial class WorldBuilderPlanet
{
    public WorldBuilderStarport Starport { get; set; } = new();

    private void GenerateStarport()
    {
        GenerateStarportClass();
    }

    private void GenerateStarportDetails()
    {
        GenerateHighport();
        GenerateNavalBase();
        GenerateScoutBase();
        GenerateMilitaryBase();
        GenerateCorsairBase();
    }

    private void GenerateAdvancedStarportDetails()
    {
        GenerateTraffic();
        GenerateHighportTotalDockingCapacity();
        GenerateShipyardBuildCapacity();
    }

    private void GenerateStarportClass()
    {
        var dm = Population switch {
            <= 2 => -2,
            <= 4 => -1,
            <= 7 => 0,
            <= 9 => 1,
            >= 10 => 2
        };
        Starport.Class = (_rollingService.D6(2) + dm) switch {
            <= 2 => StarportClass.X,
            <= 4 => StarportClass.E,
            <= 6 => StarportClass.D,
            <= 8 => StarportClass.C,
            <= 10 => StarportClass.B,
            >= 11 => StarportClass.A
        };
    }

    private void GenerateHighport()
    {
        var dm = Population >= 9 ? 1 : 0;
        dm += TechLevel switch {
            <= 8 => 0,
            <= 11 => 1,
            >= 12 => 2
        };

        Starport.Highport = Starport.Class switch {
            StarportClass.A => _rollingService.D6(2) + dm >= 6,
            StarportClass.B => _rollingService.D6(2) + dm >= 8,
            StarportClass.C => _rollingService.D6(2) + dm >= 10,
            StarportClass.D => _rollingService.D6(2) + dm >= 12,
            _ => false
        };
    }

    private void GenerateNavalBase()
    {
        if (Starport.Class is StarportClass.A or StarportClass.B && _rollingService.D6(2) >= 8) Bases.Add(Base.Naval);
    }

    private void GenerateScoutBase()
    {
        switch (Starport.Class) {
            case StarportClass.A:
                if (_rollingService.D6(2) >= 10) Bases.Add(Base.Scout);
                break;
            case StarportClass.B or StarportClass.C:
                if (_rollingService.D6(2) >= 9) Bases.Add(Base.Scout);
                break;
            case StarportClass.D:
                if (_rollingService.D6(2) >= 8) Bases.Add(Base.Scout);
                break;
        }
    }

    private void GenerateMilitaryBase()
    {
        switch (Starport.Class) {
            case StarportClass.A or StarportClass.B:
                if (_rollingService.D6(2) >= 8) Bases.Add(Base.Military);
                break;
            case StarportClass.C:
                if (_rollingService.D6(2) >= 10) Bases.Add(Base.Military);
                break;
        }
    }

    private void GenerateCorsairBase()
    {
        if (Bases.Contains(Base.Scout)) return;

        switch (Starport.Class) {
            case StarportClass.D:
                if (_rollingService.D6(2) >= 12) Bases.Add(Base.Corsair);
                break;
            case StarportClass.E:
                if (_rollingService.D6(2) >= 10) Bases.Add(Base.Corsair);
                break;
        }
    }

    private void GenerateTraffic()
    {
        switch (Importance) {
            case 5:
                Starport.DailyTraffic = Math.Max(_rollingService.D6(3) * 10 + _rollingService.Flux(), 0);
                Starport.DailyMaximum = 185;
                Starport.ExpectedWeekly = 1000;
                break;
            case 4:
                Starport.DailyTraffic = Math.Max(_rollingService.D6(2) + 10, 0);
                Starport.DailyMaximum = 22;
                Starport.ExpectedWeekly = 150;
                break;
            case 3:
                Starport.DailyTraffic = Math.Max(_rollingService.D6(2) - 5, 0);
                Starport.DailyMaximum = 7;
                Starport.ExpectedWeekly = 30;
                break;
            case 2:
                Starport.DailyTraffic = Math.Max(_rollingService.D6(1) - 2, 0);
                Starport.DailyMaximum = 4;
                Starport.ExpectedWeekly = 20;
                break;
            case 1:
                Starport.DailyTraffic = Math.Max(_rollingService.D6(1) - 3, 0);
                Starport.DailyMaximum = 3;
                Starport.ExpectedWeekly = 10;
                break;
            case 0:
                Starport.DailyTraffic = Math.Max(_rollingService.D6(1) - 4, 0);
                Starport.DailyMaximum = 2;
                Starport.ExpectedWeekly = 5;
                break;
            case -1:
                Starport.DailyTraffic = Math.Max(_rollingService.D6(1) - 5, 0);
                Starport.DailyMaximum = 1;
                Starport.ExpectedWeekly = 3;
                break;
            case -2:
                Starport.DailyTraffic = Math.Max(_rollingService.D6(2) - 11, 0);
                Starport.DailyMaximum = 1;
                Starport.ExpectedWeekly = 2;
                break;
            case -3:
                Starport.DailyTraffic = Math.Max(_rollingService.D6(2) - 11, 0);
                Starport.DailyMaximum = 1;
                Starport.ExpectedWeekly = 1;
                break;
        }
    }

    private void GenerateHighportTotalDockingCapacity()
    {
        if (Starport.Highport) {
            Starport.HighportTotalDockingCapacity = Starport.Class switch {
                StarportClass.A => (int) (100000 + ((Importance <= 1 ? 1 : Importance) * Starport.ExpectedWeekly * 500 * Population *
                    (1 + _rollingService.D6(1) + EfficiencyFactor) / 5.0)),
                StarportClass.B => (int) (50000 + ((Importance <= 1 ? 1 : Importance) * Starport.ExpectedWeekly * 500 * Population *
                    (1 + _rollingService.D6(1) + EfficiencyFactor) / 5.0)),
                StarportClass.C => (int) (20000 + ((Importance <= 1 ? 1 : Importance) * Starport.ExpectedWeekly * 200 * Population *
                    (1 + _rollingService.D6(1) + EfficiencyFactor) / 5.0)),
                StarportClass.D => (int) (500 + ((Importance <= 1 ? 1 : Importance) * Starport.ExpectedWeekly * 100 * Population *
                    (1 + _rollingService.D6(1) + EfficiencyFactor) / 5.0)),
                _ => 0
            };
        } else {
            Starport.DownportTotalDockingCapacity = Starport.Class switch {
                StarportClass.A => (int) (100000 + ((Importance <= 1 ? 1 : Importance) * Starport.ExpectedWeekly * 500 * Population *
                    (1 + _rollingService.D6(1) + EfficiencyFactor) / 5.0)),
                StarportClass.B => (int) (50000 + ((Importance <= 1 ? 1 : Importance) * Starport.ExpectedWeekly * 500 * Population *
                    (1 + _rollingService.D6(1) + EfficiencyFactor) / 5.0)),
                StarportClass.C => (int) (20000 + ((Importance <= 1 ? 1 : Importance) * Starport.ExpectedWeekly * 200 * Population *
                    (1 + _rollingService.D6(1) + EfficiencyFactor) / 5.0)),
                StarportClass.D => (int) (500 + ((Importance <= 1 ? 1 : Importance) * Starport.ExpectedWeekly * 100 * Population *
                    (1 + _rollingService.D6(1) + EfficiencyFactor) / 5.0)),
                StarportClass.E => (int) (400 + ((Importance <= 1 ? 1 : Importance) * Starport.ExpectedWeekly * 100 * Population *
                    (1 + _rollingService.D6(1) + EfficiencyFactor) / 5.0)),
                _ => 0
            };
        }
    }

    private void GenerateShipyardBuildCapacity()
    {
        var dm = TechLevel switch {
            <= 8 => -4,
            <= 11 => 0,
            <= 14 => 2,
            >= 15 => 4
        };

        if (TradeCodes.Contains(TradeCode.NonIndustrial)) dm -= 2;
        if (TradeCodes.Contains(TradeCode.Industrial)) dm += 2;
        
        Starport.ShipyardBuildCapacity = Starport.Class switch {
            StarportClass.A => (int) ((EfficiencyFactor + InfrastructureFactor + _rollingService.D6(1) + dm) * (TotalWorldPopulation / 20000.0)),
            StarportClass.B => (int) ((EfficiencyFactor + InfrastructureFactor + _rollingService.D6(1) + dm) * (TotalWorldPopulation / 100000.0)),
            StarportClass.C => (int) ((EfficiencyFactor + InfrastructureFactor + _rollingService.D6(1) - 3 + dm) * (TotalWorldPopulation / 15000.0)),
            _ => 0
        };

        if (Starport.ShipyardBuildCapacity < 10000) Starport.ShipyardBuildCapacity = 9000 + _rollingService.D6(1) * 500;

        Starport.AnnualShipyardOutput = Importance switch {
            >= 1 => (int) ((double) Starport.ShipyardBuildCapacity / Importance),
            _ => (int) ((double) Starport.ShipyardBuildCapacity * (1 - Importance))
        };
    }
}