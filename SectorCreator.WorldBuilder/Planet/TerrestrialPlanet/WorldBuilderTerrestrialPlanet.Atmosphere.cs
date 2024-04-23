using SectorCreator.WorldBuilder.Enums;

namespace SectorCreator.WorldBuilder.Planet.TerrestrialPlanet;

public partial class WorldBuilderTerrestrialPlanet
{
    private void GenerateAtmosphere(double starHzco)
    {
        if (Size is <= 1 or 26) {
            Atmosphere = 0;
            GenerateBAR();
            return;
        }

        Atmosphere += Gravity switch {
            < .4 => -2,
            < .5 => -1,
            _ => 0
        };

        if (IsInHabitableZone(starHzco)) {
            Atmosphere = Math.Max(_rollingService.Flux() + Size, 0);
            Atmosphere = Math.Min(Atmosphere, 17);
        } else {
            GenerateNonHabitableZoneAtmosphere(starHzco);
        }

        GenerateBAR();
    }

    private void GenerateBAR()
    {
        switch (Atmosphere) {
            case 0:
                BAR = 0.0009 * (_rollingService.D10(1) * 10 + _rollingService.D10(1));
                break;

            case 1:
                BAR = 0.001 + 0.09 * (_rollingService.D10(1) * 10 + _rollingService.D10(1));
                break;

            case <= 3:
                BAR = 0.1 + 0.32 * (_rollingService.D10(1) * 10 + _rollingService.D10(1));
                break;

            case <= 5:
                BAR = 0.43 + 0.27 * (_rollingService.D10(1) * 10 + _rollingService.D10(1));
                break;

            case <= 7:
                BAR = 0.70 + 0.79 * (_rollingService.D10(1) * 10 + _rollingService.D10(1));
                break;

            case <= 9:
                BAR = 1.50 + 0.99 * (_rollingService.D10(1) * 10 + _rollingService.D10(1));
                break;

            case 13:
                BAR = 2.50 + 7.50 * (_rollingService.D10(1) * 10 + _rollingService.D10(1));
                break;

            case 14:
                BAR = 0.10 + 0.32 * (_rollingService.D10(1) * 10 + _rollingService.D10(1));
                break;

            case 16:
                BAR = 100 + 900 * (_rollingService.D10(1) * 10 + _rollingService.D10(1));
                break;

            case 17:
                BAR = 1000 + 1000000 * (_rollingService.D10(1) * 10 + _rollingService.D10(1));
                break;

            case 10:
                GenerateExoticBAR();
                break;
            case 11 or 12:
                GenerateCorrosiveBAR();
                break;
        }
    }

    private void GenerateExoticBAR()
    {
        BAR = AtmosphereSub switch {
            <= 3 => 0.1 + 0.32 * (_rollingService.D10(1) * 10 + _rollingService.D10(1)),
            <= 5 => 0.43 + 0.27 * (_rollingService.D10(1) * 10 + _rollingService.D10(1)),
            <= 7 => 0.70 + 0.79 * (_rollingService.D10(1) * 10 + _rollingService.D10(1)),
            <= 9 => 1.50 + 0.99 * (_rollingService.D10(1) * 10 + _rollingService.D10(1)),
            >= 10 => 2.50 + 7.50 * (_rollingService.D10(1) * 10 + _rollingService.D10(1))
        };
    }

    private void GenerateCorrosiveBAR()
    {
        BAR = AtmosphereSub switch {
            <= 3 => 0.1 + 0.32 * (_rollingService.D10(1) * 10 + _rollingService.D10(1)),
            <= 5 => 0.43 + 0.27 * (_rollingService.D10(1) * 10 + _rollingService.D10(1)),
            <= 7 => 0.70 + 0.79 * (_rollingService.D10(1) * 10 + _rollingService.D10(1)),
            <= 9 => 1.50 + 0.99 * (_rollingService.D10(1) * 10 + _rollingService.D10(1)),
            <= 11 => 2.50 + 7.50 * (_rollingService.D10(1) * 10 + _rollingService.D10(1)),
            >= 12 => 10.0 + (Atmosphere == 12 ? 1000000 : 990) * (_rollingService.D10(1) * 10 + _rollingService.D10(1))
        };
    }

    private void GenerateNonHabitableZoneAtmosphere(double HZCO)
    {
        var mod = HZCO < 1 ? .1 : 1;

        if (OrbitNumber < HZCO - (2 * mod)) {
            GenerateExtraHotAtmosphere(HZCO);
        } else if (OrbitNumber < HZCO - mod) {
            GenerateHotAtmosphere(HZCO);
        } else if (OrbitNumber > HZCO + (3 * mod)) {
            GenerateExtraColdAtmosphere(HZCO);
        } else if (OrbitNumber > HZCO + mod) {
            GenerateColdAtmosphere(HZCO);
        } else {
            throw new ArgumentOutOfRangeException();
        }
    }

    private void GenerateExtraHotAtmosphere(double HZCO)
    {
        switch (_rollingService.Flux() + Size) {
            case <= 1:
                Atmosphere = 0;
                break;
            case <= 3:
                Atmosphere = 1;
                break;
            case 4:
                if (OrbitNumber <= HZCO - 3) {
                    Atmosphere = _rollingService.D6(1) switch {
                        1 => 1,
                        2 => Atmosphere = 10,
                        <= 5 => 11,
                        >= 6 => 12
                    };
                    if (Atmosphere is 10) {
                        GenerateExoticSubtype(HZCO);
                    } else if (Atmosphere is 11 or 12) {
                        GenerateCorrosiveSubtype(HZCO);
                    }
                } else {
                    Atmosphere = 10;
                    AtmosphereSub = 3;
                }

                if (_rollingService.D6(1) >= 4) {
                    TaintAtmosphere();
                }

                break;
            case 5:
                if (OrbitNumber <= HZCO - 3) {
                    Atmosphere = _rollingService.D6(1) switch {
                        1 => 1,
                        2 => Atmosphere = 10,
                        <= 5 => 11,
                        >= 6 => 12
                    };
                    if (Atmosphere is 11 or 12) {
                        GenerateCorrosiveSubtype(HZCO);
                    } else if (Atmosphere is 10) {
                        GenerateExoticSubtype(HZCO);
                    }
                } else {
                    Atmosphere = 10;
                    AtmosphereSub = 5;
                }

                if (_rollingService.D6(1) >= 4) {
                    TaintAtmosphere();
                }

                break;
            case 6:
                if (OrbitNumber <= HZCO - 3) {
                    Atmosphere = _rollingService.D6(1) switch {
                        1 => 1,
                        2 => Atmosphere = 10,
                        <= 5 => 11,
                        >= 6 => 12
                    };
                    if (Atmosphere is 11 or 12) {
                        GenerateCorrosiveSubtype(HZCO);
                    } else if (Atmosphere is 10) {
                        GenerateExoticSubtype(HZCO);
                    }
                } else {
                    Atmosphere = 10;
                    AtmosphereSub = 6;
                }

                if (_rollingService.D6(1) >= 4) {
                    TaintAtmosphere();
                }

                break;
            case 7:
                if (OrbitNumber <= HZCO - 3) {
                    Atmosphere = _rollingService.D6(1) switch {
                        1 => 1,
                        2 => Atmosphere = 10,
                        <= 5 => 11,
                        >= 6 => 12
                    };
                    if (Atmosphere is 11 or 12) {
                        GenerateCorrosiveSubtype(HZCO);
                    } else if (Atmosphere is 10) {
                        GenerateExoticSubtype(HZCO);
                    }
                } else {
                    Atmosphere = 10;
                    AtmosphereSub = 8;
                }

                if (_rollingService.D6(1) >= 4) {
                    TaintAtmosphere();
                }

                break;
            case 8:
                if (OrbitNumber <= HZCO - 3) {
                    Atmosphere = _rollingService.D6(1) switch {
                        1 => 1,
                        2 => Atmosphere = 10,
                        <= 5 => 11,
                        >= 6 => 12
                    };
                    if (Atmosphere is 11 or 12) {
                        GenerateCorrosiveSubtype(HZCO);
                    } else if (Atmosphere is 10) {
                        GenerateExoticSubtype(HZCO);
                    }
                } else {
                    Atmosphere = 10;
                    AtmosphereSub = 10;
                }

                if (_rollingService.D6(1) >= 4) {
                    TaintAtmosphere();
                }

                break;
            case 9 or 10 or 11 or 13:
                Atmosphere = 11;
                GenerateCorrosiveSubtype(HZCO);
                break;
            case 12 or 14:
                Atmosphere = 12;
                GenerateCorrosiveSubtype(HZCO);
                break;
            case 15:
                Atmosphere = 15;
                GenerateUnusualSubtype();
                break;
            case 16:
                Atmosphere = 16;
                break;
            case >= 17:
                Atmosphere = 17;
                break;
        }
    }

    private void GenerateHotAtmosphere(double HZCO)
    {
        switch (_rollingService.Flux() + Size) {
            case <= 0:
                Atmosphere = 0;
                break;
            case 1:
                Atmosphere = 1;
                break;
            case 2:
                Atmosphere = 10;
                AtmosphereSub = 2;
                break;
            case 3:
                Atmosphere = 10;
                AtmosphereSub = 3;
                break;
            case 4:
                Atmosphere = 10;
                AtmosphereSub = 4;
                break;
            case 5:
                Atmosphere = 10;
                AtmosphereSub = 5;
                break;
            case 6:
                Atmosphere = 10;
                AtmosphereSub = 6;
                break;
            case 7:
                Atmosphere = 10;
                AtmosphereSub = 7;
                break;
            case 8:
                Atmosphere = 10;
                AtmosphereSub = 8;
                break;
            case 9:
                Atmosphere = 10;
                AtmosphereSub = 9;
                break;
            case 10:
                Atmosphere = 10;
                AtmosphereSub = 10;
                if (_rollingService.D6(1) >= 4) TaintAtmosphere();
                break;
            case 11 or 13:
                Atmosphere = 11;
                GenerateCorrosiveSubtype(HZCO);
                break;
            case 12 or 14:
                Atmosphere = 12;
                GenerateCorrosiveSubtype(HZCO);
                break;
            case 15:
                Atmosphere = 15;
                GenerateUnusualSubtype();
                break;
            case 16:
                Atmosphere = 16;
                break;
            case >= 17:
                Atmosphere = 17;
                break;
        }
    }

    private void GenerateExtraColdAtmosphere(double HZCO)
    {
        switch (_rollingService.Flux() + Size) {
            case <= 0:
                Atmosphere = 0;
                break;
            case <= 2:
                Atmosphere = 1;
                break;
            case 3:
                Atmosphere = 10;
                AtmosphereSub = _rollingService.D6(1) >= 4 ? 2 : 3;
                break;
            case 4:
                Atmosphere = 10;
                AtmosphereSub = 4;
                break;
            case 5:
                Atmosphere = 10;
                AtmosphereSub = 5;
                break;
            case 6:
                Atmosphere = 10;
                AtmosphereSub = 6;
                break;
            case 7:
                Atmosphere = 10;
                AtmosphereSub = 7;
                break;
            case 8:
                Atmosphere = 10;
                AtmosphereSub = 8;
                break;
            case 9:
                Atmosphere = 10;
                AtmosphereSub = 9;
                break;
            case 10:
                Atmosphere = 10;
                AtmosphereSub = _rollingService.D6(1) >= 4 ? 10 : 11;
                break;
            case 11 or 14:
                Atmosphere = 11;
                GenerateCorrosiveSubtype(HZCO);
                break;
            case 12:
                Atmosphere = 12;
                GenerateCorrosiveSubtype(HZCO);
                break;
            case 13:
                Atmosphere = 13;
                break;
            case 15:
                Atmosphere = 15;
                GenerateUnusualSubtype();
                break;
            case 16:
                Atmosphere = 16;
                break;
            case >= 17:
                Atmosphere = 17;
                break;
        }
    }

    private void GenerateColdAtmosphere(double HZCO)
    {
        switch (_rollingService.Flux() + Size) {
            case <= 0:
                Atmosphere = 0;
                break;
            case <= 2:
                Atmosphere = 1;
                break;
            case 3:
                Atmosphere = 10;
                AtmosphereSub = _rollingService.D6(1) >= 4 ? 2 : 3;
                break;
            case 4:
                Atmosphere = 10;
                AtmosphereSub = 4;
                break;
            case 5:
                Atmosphere = 10;
                AtmosphereSub = 5;
                break;
            case 6:
                Atmosphere = 10;
                AtmosphereSub = 6;
                break;
            case 7:
                Atmosphere = 10;
                AtmosphereSub = 7;
                break;
            case 8:
                Atmosphere = 10;
                AtmosphereSub = 8;
                break;
            case 9:
                Atmosphere = 10;
                AtmosphereSub = 9;
                break;
            case 10:
                Atmosphere = 10;
                AtmosphereSub = _rollingService.D6(1) >= 4 ? 10 : 11;
                break;
            case 11:
                Atmosphere = 11;
                GenerateCorrosiveSubtype(HZCO);
                break;
            case 12:
                Atmosphere = 12;
                GenerateCorrosiveSubtype(HZCO);
                break;
            case 13 or 16:
                Atmosphere = 16;
                break;
            case 14 or >= 17:
                Atmosphere = 17;
                break;
            case 15:
                Atmosphere = 15;
                GenerateUnusualSubtype();
                break;
        }
    }

    private void GenerateExoticSubtype(double HZCO)
    {
        var dm = 0;

        if (Size is >= 2 and <= 4)
            dm -= 2;
        if (OrbitNumber < HZCO - 1)
            dm -= 2;
        if (OrbitNumber > HZCO + 2)
            dm += 2;
        if (RunawayGreenhouse)
            dm += 4;

        var result = _rollingService.D6(2) + dm;
        AtmosphereSub = result switch {
            <= 2 => 2,
            <= 11 => result,
            12 => 12,
            13 => 10,
            >= 14 => 11
        };
    }

    private void GenerateCorrosiveSubtype(double HZCO)
    {
        var dm = 0;

        if (Size is >= 2 and <= 4) dm -= 3;
        if (Size >= 8) dm += 2;
        if (OrbitNumber < HZCO - 1) dm += 4;
        if (OrbitNumber > HZCO + 2) dm -= 2;
        if (Atmosphere == 12) dm += 2;
        if (RunawayGreenhouse) dm += 4;

        var result = _rollingService.D6(2) + dm;

        AtmosphereSub = result switch {
            <= 1 => 1,
            <= 13 => result,
            >= 14 => 14
        };

        ModifyTemperature();
    }

    private void TaintAtmosphere()
    {
        switch (Atmosphere) {
            case 2 or 3:
                Atmosphere = 2;
                break;
            case 4 or 5:
                Atmosphere = 4;
                break;
            case 6 or 7:
                Atmosphere = 7;
                break;
            case 8 or 9:
                Atmosphere = 9;
                break;
            case 10:
                AtmosphereSub = AtmosphereSub switch {
                    2 or 3 => 2,
                    4 or 5 => 4,
                    6 or 7 => 7,
                    8 or 9 => 9,
                    10 or 11 => 11,
                    13 or 14 => 14,
                    12 => 12,
                    _ => throw new ArgumentOutOfRangeException()
                };
                break;
            case 11 or 12:
                AtmosphereSub = AtmosphereSub switch {
                    2 or 3 => 2,
                    4 or 5 => 4,
                    6 or 7 => 7,
                    8 or 9 => 9,
                    10 or 11 => 11,
                    13 or 14 => 14,
                    12 => 12,
                    _ => throw new ArgumentOutOfRangeException()
                };
                break;
        }
    }

    private void GenerateUnusualSubtype()
    {
        var result = _rollingService.D(2, 1) * 10 + _rollingService.D6(1);

        switch (result) {
            case 11:
                if (UnusualSubtypes.Contains(UnusualSubtype.ExtremeDensity)
                    || UnusualSubtypes.Contains(UnusualSubtype.VeryExtremeDensity) ||
                    UnusualSubtypes.Contains(UnusualSubtype.CrushingDensity)) {
                    GenerateUnusualSubtype();
                } else {
                    UnusualSubtypes.Add(UnusualSubtype.ExtremeDensity);
                }

                break;
            case 12:
                if (UnusualSubtypes.Contains(UnusualSubtype.ExtremeDensity)
                    || UnusualSubtypes.Contains(UnusualSubtype.VeryExtremeDensity) ||
                    UnusualSubtypes.Contains(UnusualSubtype.CrushingDensity)) {
                    GenerateUnusualSubtype();
                } else {
                    UnusualSubtypes.Add(UnusualSubtype.VeryExtremeDensity);
                }

                break;
            case 13:
                if (UnusualSubtypes.Contains(UnusualSubtype.ExtremeDensity)
                    || UnusualSubtypes.Contains(UnusualSubtype.VeryExtremeDensity) ||
                    UnusualSubtypes.Contains(UnusualSubtype.CrushingDensity)) {
                    GenerateUnusualSubtype();
                } else {
                    UnusualSubtypes.Add(UnusualSubtype.CrushingDensity);
                }

                break;
            case 14:
                UnusualSubtypes.Add(UnusualSubtype.Ellipsoid);
                break;
            case 15:
                UnusualSubtypes.Add(UnusualSubtype.HighRadiation);
                break;
            case 16:
                if (Gravity > 1.2) {
                    UnusualSubtypes.Add(UnusualSubtype.Layered);
                } else {
                    GenerateUnusualSubtype();
                }

                break;
            case 21:
                if (Hydrographics == 10 && BAR >= 1.0) {
                    UnusualSubtypes.Add(UnusualSubtype.Panthalassic);
                } else {
                    GenerateUnusualSubtype();
                }

                break;
            case 22:
                if (Hydrographics >= 5 && BAR >= 2.5) {
                    UnusualSubtypes.Add(UnusualSubtype.Steam);
                } else {
                    GenerateUnusualSubtype();
                }

                break;
            case 23:
                UnusualSubtypes.Add(UnusualSubtype.VariablePressure);
                break;
            case 24:
                UnusualSubtypes.Add(UnusualSubtype.VariableComposition);
                break;
            case 25:
                GenerateUnusualSubtype();
                GenerateUnusualSubtype();
                break;
            case 26:
                UnusualSubtypes.Add(UnusualSubtype.Other);
                break;
        }
    }

    private void ModifyTemperature()
    {
        if (AtmosphereSub is 1 && Temperature > 50) {
            Temperature = 50;
        }

        if (AtmosphereSub >= 13 && Temperature < 500) {
            Temperature = 500;
        }
    }
}