namespace SectorCreator.WorldBuilder.Planet.AsteroidBelt;
using SectorCreator.Global.Enums;

public partial class WorldBuilderAsteroidBelt
{
    public void GenerateSizeCharacteristics(WorldBuilderStarSystem starSystem)
    {
        Size = 0;

        BeltSpan = starSystem.Spread * (_rollingService.D6(2) + BeltSpanDM(starSystem)) / 10.0;
        GenerateComposition(starSystem.Star.HZCO);
        Bulk = Math.Max(_rollingService.D(2, 2) + GetBulkDM(starSystem.Age), 1);
    }
    
    private int BeltSpanDM(WorldBuilderStarSystem starSystem)
    {
        var DM = 0;
        for (var i = 0; i < starSystem.Planets.Count; i++) {
            if (Math.Abs(starSystem.Planets[i].OrbitNumber - OrbitNumber) < .001) {
                if (i != starSystem.Planets.Count - 1) {
                    if (starSystem.Planets[i + 1].PlanetType == PlanetType.Jovian) {
                        DM--;
                    }
                } else {
                    DM += 3;
                }
            }
        }
    
        return DM;
    }
    
    private void GenerateComposition(double starHzco)
    {
        var dm = 0;
        if (OrbitNumber >= starHzco - 1 && OrbitNumber <= starHzco + 1) {
            dm = -4;
        } else if (OrbitNumber >= starHzco + 2) {
            dm = 4;
        }
    
        Composition = (_rollingService.D6(2) + dm) switch {
            <= 0 => new AsteroidBeltComposition {
                m = 60 + _rollingService.D6(1) * 5,
                s = _rollingService.D6(1) * 5,
                c = 0
            },
            1 => new AsteroidBeltComposition {
                m = 50 + _rollingService.D6(1) * 5,
                s = 5 + _rollingService.D6(1) * 5,
                c = _rollingService.D3(1)
            },
            2 => new AsteroidBeltComposition {
                m = 40 + _rollingService.D6(1) * 5,
                s = 15 + _rollingService.D6(1) * 5,
                c = _rollingService.D6(1)
            },
            3 => new AsteroidBeltComposition {
                m = 25 + _rollingService.D6(1) * 5,
                s = 30 + _rollingService.D6(1) * 5,
                c = _rollingService.D6(1)
            },
            4 => new AsteroidBeltComposition {
                m = 15 + _rollingService.D6(1) * 5,
                s = 35 + _rollingService.D6(1) * 5,
                c = 5 + _rollingService.D6(1)
            },
            5 => new AsteroidBeltComposition {
                m = 5 + _rollingService.D6(1) * 5,
                s = 40 + _rollingService.D6(1) * 5,
                c = 5 + _rollingService.D6(1) * 2
            },
            6 => new AsteroidBeltComposition {
                m = _rollingService.D6(1) * 5,
                s = 40 + _rollingService.D6(1) * 5,
                c = _rollingService.D6(1) * 5
            },
            7 => new AsteroidBeltComposition {
                m = 5 + _rollingService.D6(1) * 2,
                s = 35 + _rollingService.D6(1) * 5,
                c = 10 + _rollingService.D6(1) * 5
            },
            8 => new AsteroidBeltComposition {
                m = 5 + _rollingService.D6(1),
                s = 30 + _rollingService.D6(1) * 5,
                c = 20 + _rollingService.D6(1) * 5
            },
            9 => new AsteroidBeltComposition {
                m = _rollingService.D6(1),
                s = 15 + _rollingService.D6(1) * 5,
                c = 40 + _rollingService.D6(1) * 5
            },
            10 => new AsteroidBeltComposition {
                m = _rollingService.D6(1),
                s = 5 + _rollingService.D6(1) * 5,
                c = 50 + _rollingService.D6(1) * 5
            },
            11 => new AsteroidBeltComposition {
                m = _rollingService.D3(1),
                s = 5 + _rollingService.D6(1) * 2,
                c = 60 + _rollingService.D6(1) * 5
            },
            >= 12 => new AsteroidBeltComposition {
                m = 0,
                s = _rollingService.D6(1),
                c = 70 + _rollingService.D6(1) * 5
            },
        };
        while (Composition.Total > 100) {
            var difference = Composition.Total - 100;
    
            if (Composition.m > 0) {
                Composition.m = Math.Max(Composition.m - difference, 0);
            } else if (Composition.s > 0) {
                Composition.s = Math.Max(Composition.s - difference, 0);
            } else if (Composition.c > 0) {
                Composition.c = Math.Max(Composition.c - difference, 0);
            }
        }
    }
    
    private int GetBulkDM(double starSystemAge)
    {
        var dm = 0;
    
        dm -= (int) Math.Floor(starSystemAge / 2);
        dm += (int) Math.Floor(Composition.c / 10m);
    
        return dm;
    }
}