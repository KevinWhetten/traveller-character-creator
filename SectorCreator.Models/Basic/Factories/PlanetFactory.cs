using SectorCreator.Global;
using SectorCreator.Global.Enums;

namespace SectorCreator.Models.Basic.Factories;

public interface IPlanetFactory
{
    public Planet Generate(SectorType sectorType, Coordinates coordinates);
}

public class PlanetFactory : IPlanetFactory
{
    protected readonly IRollingService _rollingService;
    public PlanetFactory(IRollingService rollingService)
    {
        _rollingService = rollingService;
    }

    public Planet Planet { get; set; } = new(new RollingService());

    public virtual Planet Generate(SectorType sectorType, Coordinates coordinates)
    {
        Planet = new Planet(new RollingService()) {
            Coordinates = coordinates
        };

        GenerateName();
        GenerateSize();
        GenerateAtmosphere(sectorType);
        GenerateTemperature();
        GenerateHydrographics(sectorType);

        GeneratePopulation(sectorType);
        GenerateGovernment();
        GenerateLawLevel();
        GenerateStarport(sectorType);
        GenerateTechnologyLevel();

        GetTravelZone();
        GenerateBases();

        return Planet;
    }

    public void GenerateName()
    {
        Planet.Name = "";
    }

    public void GenerateSize()
    {
        Planet.Size = _rollingService.D6(2) - 2;
    }

    public void GenerateAtmosphere(SectorType sectorType)
    {
        Planet.Atmosphere = _rollingService.D6(2) - 7 + Planet.Size;
        Planet.Atmosphere = Math.Max(Planet.Atmosphere, 0);

        if (sectorType is SectorType.SpaceOpera or SectorType.HardScience) {
            ModifyAtmosphere();
        }
    }

    public void ModifyAtmosphere()
    {
        Planet.Atmosphere = Planet.Size switch {
            <= 2 => 0,
            <= 4 => Planet.Atmosphere switch {
                (<= 2) => 0,
                (>= 3 and <= 5) => 1,
                (_) => 10
            },
            _ => Planet.Atmosphere
        };
    }

    public void GenerateTemperature()
    {
        var roll = _rollingService.D6(2);

        roll += Planet.Atmosphere switch {
            2 or 3 => -2,
            4 or 5 or 14 => -1,
            8 or 9 => 1,
            10 or 13 or 15 => + 2,
            11 or 12 => 6,
            _ => 0
        };

        Planet.Temperature = roll switch {
            (<= 2) => Temperature.Frozen,
            (<= 5) => Temperature.Cold,
            (<= 9) => Temperature.Temperate,
            <= 11 => Temperature.Hot,
            >= 12 => Temperature.Boiling
        };
    }

    public void GenerateHydrographics(SectorType sectorType)
    {
        if (Planet.Size <= 1) {
            Planet.Hydrographics = 0;
            return;
        }

        Planet.Hydrographics = _rollingService.D6(2) - 7 + Planet.Size + GetAtmosphereMod(sectorType);

        if (Planet.Atmosphere is not 13 or 15) {
            Planet.Hydrographics += Planet.Temperature switch {
                Temperature.Boiling => -6,
                Temperature.Hot => -2,
                _ => 0
            };
        }

        Planet.Hydrographics = Math.Max(Planet.Hydrographics, 0);
        Planet.Hydrographics = Math.Min(Planet.Hydrographics, 10);
    }

    private int GetAtmosphereMod(SectorType sectorType)
    {
        if (sectorType is SectorType.SpaceOpera or SectorType.HardScience) {
            return Planet.Atmosphere switch {
                10 => Planet.Size is 3 or 4 ? -6 : 0,
                (0 or 1) => -6,
                2 or 3 or 11 or 12 => -4,
                _ => 0
            };
        } else {
            return Planet.Atmosphere switch {
                0 or 1 or 10 or 11 or 12 => -4,
                _ => 0
            };
        }
    }

    public void GeneratePopulation(SectorType sectorType)
    {
        Planet.Population = _rollingService.D6(2) - 2;

        if (sectorType == SectorType.HardScience) {
            if (Planet.Size is <= 2 or 10) {
                Planet.Population--;
            }

            if (Planet.Atmosphere is 5 or 6 or 8) {
                Planet.Population++;
            } else {
                Planet.Population--;
            }
        }
    }

    public void GenerateGovernment()
    {
        Planet.Government = Planet.Population > 0
            ? _rollingService.D6(2) - 7 + Planet.Population
            : 0;

        Planet.Government = Math.Max(Planet.Government, 0);
    }

    public void GenerateLawLevel()
    {
        Planet.LawLevel = Planet.Population > 0
            ? _rollingService.D6(2) - 7 + Planet.Government
            : 0;
        Planet.LawLevel = Math.Max(Planet.LawLevel, 0);
    }

    public void GenerateStarport(SectorType sectorType)
    {
        var roll = sectorType != SectorType.HardScience
            ? _rollingService.D6(2)
            : _rollingService.D6(2) - 7 + Planet.Population;

        Planet.Starport.Class = roll switch {
            <= 2 => StarportClass.X,
            <= 4 => StarportClass.E,
            <= 6 => StarportClass.D,
            <= 8 => StarportClass.C,
            <= 10 => StarportClass.B,
            _ => StarportClass.A
        };
    }

    public void GenerateTechnologyLevel()
    {
        Planet.TechLevel = _rollingService.D6(1);

        switch (Planet.Starport.Class) {
            case StarportClass.A:
                Planet.TechLevel += 6;
                break;
            case StarportClass.B:
                Planet.TechLevel += 4;
                break;
            case StarportClass.C:
                Planet.TechLevel += 2;
                break;
            case StarportClass.D:
                break;
            case StarportClass.X:
                Planet.TechLevel -= 4;
                break;
        }

        switch (Planet.Size) {
            case <= 1:
                Planet.TechLevel += 2;
                break;
            case <= 4:
                Planet.TechLevel++;
                break;
        }

        if (Planet.Atmosphere is <= 3 or >= 10) {
            Planet.TechLevel += 1;
        }

        switch (Planet.Hydrographics) {
            case 0 or 9:
                Planet.TechLevel++;
                break;
            case 10:
                Planet.TechLevel += 2;
                break;
        }

        switch (Planet.Population) {
            case (<= 5 and >= 1) or 9:
                Planet.TechLevel++;
                break;
            case 10:
                Planet.TechLevel += 2;
                break;
            case 11:
                Planet.TechLevel += 3;
                break;
            case 12:
                Planet.TechLevel += 4;
                break;
        }

        switch (Planet.Government) {
            case 0 or 5:
                Planet.TechLevel++;
                break;
            case 7:
                Planet.TechLevel += 2;
                break;
            case 13 or 14:
                Planet.TechLevel -= 2;
                break;
        }

        Planet.TechLevel = Math.Max(Planet.TechLevel, 0);
    }

    public void GetTravelZone()
    {
        Planet.TravelZone = Planet is {Atmosphere: >= 10, Government: 0 or 7 or 10, LawLevel: 0 or >= 9}
            ? TravelZone.Amber
            : TravelZone.None;
    }

    public void GenerateBases()
    {
        Planet.Bases = new List<string>();
        switch (Planet.Starport.Class) {
            case StarportClass.A: {
                if (_rollingService.D6(2) >= 8) {
                    Planet.Bases.Add(Base.Naval);
                    Planet.Starport.Installations.Add(StarportInstallation.ArmyBase);
                }

                if (_rollingService.D6(2) >= 10) {
                    Planet.Bases.Add(Base.Scout);
                    Planet.Starport.Installations.Add(StarportInstallation.ScoutBase);
                }

                if (_rollingService.D6(2) >= 8) {
                    Planet.TradeCodes.Add(TradeCode.ResearchStation);
                    Planet.Starport.Installations.Add(StarportInstallation.ResearchFacility);
                }

                Planet.Starport.Installations.Add(StarportInstallation.TAS);
                break;
            }
            case StarportClass.B: {
                if (_rollingService.D6(2) >= 8) {
                    Planet.Bases.Add(Base.Naval);
                }

                if (_rollingService.D6(2) >= 8) {
                    Planet.Bases.Add(Base.Scout);
                    Planet.Starport.Installations.Add(StarportInstallation.ScoutBase);
                }

                if (_rollingService.D6(2) >= 10) {
                    Planet.TradeCodes.Add(TradeCode.ResearchStation);
                    Planet.Starport.Installations.Add(StarportInstallation.ResearchFacility);
                }

                Planet.Starport.Installations.Add(StarportInstallation.TAS);
                break;
            }
            case StarportClass.C: {
                if (_rollingService.D6(2) >= 8) {
                    Planet.Bases.Add(Base.Scout);
                }

                if (_rollingService.D6(2) >= 10) {
                    Planet.TradeCodes.Add(TradeCode.ResearchStation);
                    Planet.Starport.Installations.Add(StarportInstallation.ResearchFacility);
                }

                if (_rollingService.D6(2) >= 10) {
                    Planet.Starport.Installations.Add(StarportInstallation.TAS);
                }

                break;
            }
            case StarportClass.D: {
                if (_rollingService.D6(2) >= 7) {
                    Planet.Bases.Add(Base.Scout);
                }

                break;
            }
        }
    }
}