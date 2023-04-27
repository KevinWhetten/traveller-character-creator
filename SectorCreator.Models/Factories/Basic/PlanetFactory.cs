using SectorCreator.Global;
using SectorCreator.Global.Enums;
using SectorCreator.Models.Basic;

namespace SectorCreator.Models.Factories.Basic;

public interface IPlanetFactory
{
    public Planet Generate(SectorType sectorType);
}

public class PlanetFactory : IPlanetFactory
{
    protected readonly IRollingService _rollingService;

    public PlanetFactory(IRollingService rollingService)
    {
        _rollingService = rollingService;
    }

    public Planet Planet { get; set; } = new();

    public virtual Planet Generate(SectorType sectorType)
    {
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

        GetTravelCode();
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

        Planet.Starport = roll switch {
            <= 2 => 'X',
            <= 4 => 'E',
            <= 6 => 'D',
            <= 8 => 'C',
            <= 10 => 'B',
            _ => 'A'
        };
    }

    public void GenerateTechnologyLevel()
    {
        Planet.TechLevel = _rollingService.D6(1);

        switch (Planet.Starport) {
            case 'A':
                Planet.TechLevel += 6;
                break;
            case 'B':
                Planet.TechLevel += 4;
                break;
            case 'C':
                Planet.TechLevel += 2;
                break;
            case 'X':
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

    public void GetTravelCode()
    {
        Planet.TravelCode = Planet is {Atmosphere: >= 10, Government: 0 or 7 or 10, LawLevel: 0 or >= 9}
            ? TravelCode.Amber
            : TravelCode.None;
    }

    public void GenerateBases()
    {
        Planet.Bases = new List<Base>();
        switch (Planet.Starport) {
            case 'A': {
                if (_rollingService.D6(2) >= 8) {
                    Planet.Bases.Add(Base.Naval);
                }

                if (_rollingService.D6(2) >= 10) {
                    Planet.Bases.Add(Base.Scout);
                }

                if (_rollingService.D6(2) >= 8) {
                    Planet.Bases.Add(Base.Research);
                }

                Planet.Bases.Add(Base.Tas);
                break;
            }
            case 'B': {
                if (_rollingService.D6(2) >= 8) {
                    Planet.Bases.Add(Base.Naval);
                }

                if (_rollingService.D6(2) >= 8) {
                    Planet.Bases.Add(Base.Scout);
                }

                if (_rollingService.D6(2) >= 10) {
                    Planet.Bases.Add(Base.Research);
                }

                Planet.Bases.Add(Base.Tas);
                break;
            }
            case 'C': {
                if (_rollingService.D6(2) >= 8) {
                    Planet.Bases.Add(Base.Scout);
                }

                if (_rollingService.D6(2) >= 10) {
                    Planet.Bases.Add(Base.Research);
                }

                if (_rollingService.D6(2) >= 10) {
                    Planet.Bases.Add(Base.Tas);
                }

                break;
            }
            case 'D': {
                if (_rollingService.D6(2) >= 7) {
                    Planet.Bases.Add(Base.Scout);
                }

                break;
            }
        }
    }
}