using SectorCreator.Global.Enums;
using SectorCreator.Models.Basic;
using SectorCreator.Models.Services.TradeCodeService;
using SectorCreator.WorldBuilder.Services;

namespace SectorCreator.WorldBuilder.Planet.Planet;

public partial class WorldBuilderPlanet
{
    public int Population { get; set; }
    public int PValue { get; set; }
    public long TotalWorldPopulation => (long) (PValue * Math.Pow(10, Population));
    public int PopulationConcentrationRating { get; set; }
    public int UrbanizationPercentage { get; set; }
    public long TotalUrbanPopulation => (long) (TotalWorldPopulation * (UrbanizationPercentage / 100.0));
    public List<City> MajorCities { get; set; } = new();
    public long MajorCityPopulation { get; set; }

    private void GeneratePopulation()
    {
        GenerateBasicPopulation();
        GenerateBasicGovernment();
        GenerateStarport();
        GenerateTechLevel();
        GenerateTradeCodes();
        GeneratePValue();
        GeneratePopulationConcentrationRating();
        GenerateUrbanization();
        GenerateMajorCities();
    }

    private void GenerateBasicPopulation()
    {
        Population = _rollingService.D6(2) - 2;

        if (Population < 6) {
            Population = 6;
            while (_rollingService.D6(1) >= 4 && Population > 1) {
                Population--;
            }
        }
    }

    private void GeneratePValue()
    {
        PValue = (_rollingService.D3(1) - 1) * 3 + _rollingService.D3(1);

        if (Population == 10) {
            PValue = 1;
            while (_rollingService.D6(1) >= 5 && PValue < 9) {
                PValue++;
            }
        }
    }

    private void GeneratePopulationConcentrationRating()
    {
        if (_rollingService.D6(1) > Population) {
            PopulationConcentrationRating = 9;
        } else {
            var dm = 0;

            dm += Size switch {
                1 => 2,
                <= 3 => 1,
                _ => 0
            };

            if (IsTidallyLocked && !IsMoon) dm += 2;

            dm += Population switch {
                8 => -1,
                >= 9 => -2,
                _ => 0
            };

            if (Government == 7) dm -= 2;

            dm += TechLevel switch {
                <= 1 => -2,
                <= 3 => -1,
                >= 4 => 1
            };

            if (TradeCodes.Contains(TradeCode.Agricultural)) dm -= 2;
            if (TradeCodes.Contains(TradeCode.Industrial)) dm++;
            if (TradeCodes.Contains(TradeCode.NonAgricultural)) dm--;
            if (TradeCodes.Contains(TradeCode.Rich)) dm++;

            PopulationConcentrationRating = Math.Max(_rollingService.D6(1) + dm, 0);
            PopulationConcentrationRating = Math.Min(PopulationConcentrationRating, 9);
        }
    }

    private void GenerateUrbanization()
    {
        var dm = 0;
        var min = 0;
        var max = 100;

        dm += PopulationConcentrationRating switch {
            <= 2 => -3 + PopulationConcentrationRating,
            >= 7 => -6 + PopulationConcentrationRating,
            _ => 0
        };

        if (Size == 0) dm += 2;

        switch (Population) {
            case 8:
                dm += 1;
                break;
            case 9:
                dm += 2;
                min = Math.Max(18 + _rollingService.D6(1), min);
                break;
            case 10:
                dm += 4;
                min = Math.Max(50 + _rollingService.D6(1), min);
                break;
        }

        if (Government == 0) dm -= 2;

        switch (TechLevel) {
            case <= 2:
                dm += -2;
                max = Math.Min(20 + _rollingService.D6(1), max);
                break;
            case 3:
                dm += -1;
                max = Math.Min(30 + _rollingService.D6(1), max);
                break;
            case 4:
                dm += 1;
                max = Math.Min(60 + _rollingService.D6(1), max);
                break;
            case <= 9:
                dm += 2;
                max = Math.Min(90 + _rollingService.D6(1), max);
                break;
            case >= 10:
                dm += 2;
                break;
        }

        if (TradeCodes.Contains(TradeCode.Agricultural)) {
            dm -= 2;
            max = Math.Min(20 + _rollingService.D6(1), max);
        }

        UrbanizationPercentage = (_rollingService.D6(2) + dm) switch {
            <= 0 => 0,
            1 => _rollingService.D6(1),
            2 => 6 + _rollingService.D6(1),
            3 => 12 + _rollingService.D6(1),
            4 => 18 + _rollingService.D6(1),
            5 => 22 + _rollingService.D6(1) * 2 + _rollingService.D(2, 1),
            6 => 34 + _rollingService.D6(1) * 2 + _rollingService.D(2, 1),
            7 => 46 + _rollingService.D6(1) * 2 + _rollingService.D(2, 1),
            8 => 58 + _rollingService.D6(1) * 2 + _rollingService.D(2, 1),
            9 => 70 + _rollingService.D6(1) * 2 + _rollingService.D(2, 1),
            10 => 84 + _rollingService.D6(1),
            11 => 90 + _rollingService.D6(1),
            12 => 96 + _rollingService.D3(1),
            >= 13 => 100
        };

        if (UrbanizationPercentage > max) {
            UrbanizationPercentage = max;
        }

        if (UrbanizationPercentage < min) {
            UrbanizationPercentage = min;
        }
    }

    private void GenerateMajorCities()
    {
        var cityCount = 0;
        if (PopulationConcentrationRating == 0) {
            cityCount = 0;
            MajorCityPopulation = 0;
        } else if (Population <= 5) {
            if (PopulationConcentrationRating == 9) {
                cityCount = 1;
                MajorCityPopulation = TotalUrbanPopulation;
            } else {
                cityCount = Math.Min(9 - PopulationConcentrationRating, Population);
                MajorCityPopulation = TotalUrbanPopulation;
            }
        } else if (Population >= 6 && PopulationConcentrationRating == 9) {
            cityCount = Math.Max(Population - _rollingService.D6(2), 1);
            MajorCityPopulation = TotalUrbanPopulation;
        } else {
            cityCount =
                (int) (_rollingService.D6(2) - PopulationConcentrationRating + ((UrbanizationPercentage * .2) / PopulationConcentrationRating));
            MajorCityPopulation = ((PopulationConcentrationRating) / _rollingService.D6(1) + 7) * TotalUrbanPopulation;
        }

        if (cityCount > 0) {
            if (cityCount == 1) {
                MajorCities.Add(new City {Population = TotalUrbanPopulation});
            } else if (cityCount <= 3 && PopulationConcentrationRating <= 8) {
                var remainingPopulation = TotalUrbanPopulation;
                for (var i = 0; i < cityCount; i++) {
                    MajorCities.Add(new City
                        {Population = (long) _rollingService.ValueWithVariance((_rollingService.D6(1) + 3) * .1 * remainingPopulation, 10)});
                    remainingPopulation -= MajorCities.Last().Population;
                }

                MajorCities.First().Population += remainingPopulation;
            } else {
                for (var i = 0; i < cityCount; i++) {
                    MajorCities.Add(new City {Population = (int) (TotalUrbanPopulation * .01)});
                }

                var remainingPercent = 100 - cityCount;
                var chunk = Math.Min((int) (remainingPercent / (cityCount * 2.0)), PopulationConcentrationRating);
                var chunks = (int) Math.Floor((double) remainingPercent / chunk);
                var remainingChunks = chunks;

                while (remainingChunks > 0) {
                    foreach (var city in MajorCities) {
                        var chunkNum = _rollingService.D6(1);
                        if (chunkNum > remainingChunks) chunkNum = remainingChunks;

                        city.Population += (int) _rollingService.ValueWithVariance(TotalUrbanPopulation * (chunk * chunkNum / 100.0), 10);

                        remainingChunks -= chunkNum;
                        if (remainingChunks == 0) {
                            break;
                        }
                    }
                }

                var popDifference = TotalUrbanPopulation - MajorCities.Sum(x => x.Population);
                if (popDifference < 0) {
                    MajorCities.First().Population += popDifference;
                }

                MajorCities = MajorCities.OrderByDescending(x => x.Population).ToList();
            }
        }

        GenerateCityAnomolies();
    }

    private void GenerateCityAnomolies()
    {
        var dm = 0;

        dm += Starport.Class switch {
            StarportClass.A => 1,
            StarportClass.E or StarportClass.H or StarportClass.X or StarportClass.Y => -2,
            _ => 0
        };

        dm += Atmosphere switch {
            0 or 1 or 10 => 2,
            11 => 3,
            12 => 4,
            _ => 0
        };

        dm += TechLevel switch {
            <= 8 => 0,
            <= 12 => 1,
            <= 15 => 2,
            >= 16 => 3
        };

        if (TradeCodes.Contains(TradeCode.Industrial)) dm++;
        if (TradeCodes.Contains(TradeCode.NonIndustrial)) dm--;
        if (TradeCodes.Contains(TradeCode.Rich)) dm++;
        if (TradeCodes.Contains(TradeCode.Poor)) dm--;

        foreach (var city in MajorCities) {
            if (_rollingService.D6(2) + dm >= 12) {
                var qualifiedAnomaliesCount = CityAnomalyService.Anomalies.Count(x => x.MinimumTechLevel <= TechLevel);
                city.CityAnomaly = CityAnomalyService.Anomalies.Where(x => x.MinimumTechLevel <= TechLevel).ToArray()[_rollingService.D(qualifiedAnomaliesCount, 1) - 1];
            }
        }
    }
}