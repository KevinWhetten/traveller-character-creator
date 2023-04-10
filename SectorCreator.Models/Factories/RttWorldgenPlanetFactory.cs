using SectorCreator.Global;
using SectorCreator.Global.Enums;
using SectorCreator.Models.RTTWorldgen;
using SectorCreator.Models.RTTWorldgen.Planets;

namespace SectorCreator.Models.Factories;

public class RttWorldgenPlanetFactory : PlanetFactory
{
    private readonly IRollingService _rollingService;
    private readonly IAcheronianPlanet _acheronianPlanet;
    private readonly IAreanPlanet _areanPlanet;
    private readonly IAridPlanet _aridPlanet;
    private readonly IAsphodelianPlanet _asphodelianPlanet;
    private readonly IChthonianPlanet _chthonianPlanet;
    private readonly IHebeanPlanet _hebeanPlanet;
    private readonly IHelianPlanet _helianPlanet;
    private readonly IJaniLithicPlanet _janiLithicPlanet;
    private readonly IJovianPlanet _jovianPlanet;
    private readonly IMeltballPlanet _meltballPlanet;
    private readonly IOceanicPlanet _oceanicPlanet;
    private readonly IPanthalassicPlanet _panthalassicPlanet;
    private readonly IPromethianPlanet _promethianPlanet;
    private readonly IRockballPlanet _rockballPlanet;
    private readonly ISnowballPlanet _snowballPlanet;
    private readonly IStygianPlanet _stygianPlanet;
    private readonly ITectonicPlanet _tectonicPlanet;
    private readonly ITelluricPlanet _telluricPlanet;
    private readonly IVesperianPlanet _vesperianPlanet;

    public RttWorldgenPlanetFactory(IRollingService rollingService,
        IAcheronianPlanet acheronianPlanet,
        IAreanPlanet areanPlanet,
        IAridPlanet aridPlanet,
        IAsphodelianPlanet asphodelianPlanet,
        IChthonianPlanet chthonianPlanet,
        IHebeanPlanet hebeanPlanet,
        IHelianPlanet helianPlanet,
        IJaniLithicPlanet janiLithicPlanet,
        IJovianPlanet jovianPlanet,
        IMeltballPlanet meltballPlanet,
        IOceanicPlanet oceanicPlanet,
        IPanthalassicPlanet panthalassicPlanet,
        IPromethianPlanet promethianPlanet,
        IRockballPlanet rockballPlanet,
        ISnowballPlanet snowballPlanet,
        IStygianPlanet stygianPlanet,
        ITectonicPlanet tectonicPlanet,
        ITelluricPlanet telluricPlanet,
        IVesperianPlanet vesperianPlanet)
        : base(rollingService)
    {
        _rollingService = rollingService;
        _acheronianPlanet = acheronianPlanet;
        _areanPlanet = areanPlanet;
        _aridPlanet = aridPlanet;
        _asphodelianPlanet = asphodelianPlanet;
        _chthonianPlanet = chthonianPlanet;
        _hebeanPlanet = hebeanPlanet;
        _helianPlanet = helianPlanet;
        _janiLithicPlanet = janiLithicPlanet;
        _jovianPlanet = jovianPlanet;
        _meltballPlanet = meltballPlanet;
        _oceanicPlanet = oceanicPlanet;
        _panthalassicPlanet = panthalassicPlanet;
        _promethianPlanet = promethianPlanet;
        _rockballPlanet = rockballPlanet;
        _snowballPlanet = snowballPlanet;
        _stygianPlanet = stygianPlanet;
        _tectonicPlanet = tectonicPlanet;
        _telluricPlanet = telluricPlanet;
        _vesperianPlanet = vesperianPlanet;
    }

    private RttWorldgenPlanet RttWorldgenPlanet
    {
        get => (RttWorldgenPlanet) Planet;
        set => throw new NotImplementedException();
    }

    public RttWorldgenPlanet GenerateRttWorldgenPlanet(RttWorldgenStar primaryStar, PlanetOrbit planetOrbit,
        int orbitPosition)
    {
        RttWorldgenPlanet.PlanetOrbit = planetOrbit;
        RttWorldgenPlanet.OrbitPosition = orbitPosition;
        RttWorldgenPlanet.StarId = primaryStar.Id;
        RttWorldgenPlanet.Rings = Rings.None;

        GeneratePlanetType(primaryStar);
        GenerateSatellites(primaryStar);
        GenerateRings();
        GenerateWorld(primaryStar);

        GeneratePopulation(SectorType.RttWorldgen);
        GenerateGovernment();
        GenerateLawLevel();
        GenerateTechnologyLevel();

        GenerateDesirability(primaryStar);

        return RttWorldgenPlanet;
    }

    private RttWorldgenPlanet GenerateRttWorldgenSatellite(RttWorldgenStar primaryStar, PlanetType planetType,
        Guid parentId)
    {
        RttWorldgenPlanet.PlanetType = planetType;
        RttWorldgenPlanet.ParentId = parentId;
        RttWorldgenPlanet.StarId = primaryStar.Id;
        RttWorldgenPlanet.Rings = Rings.None;

        GenerateRings();
        GenerateWorld(primaryStar);

        GenerateSettlement();

        GenerateDesirability(primaryStar);

        return RttWorldgenPlanet;
    }

    private void GenerateWorld(RttWorldgenStar primaryStar)
    {
        GetWorldType(primaryStar);

        switch (RttWorldgenPlanet.WorldType) {
            case WorldType.Acheronian:
                RttWorldgenPlanet = _acheronianPlanet.Generate(RttWorldgenPlanet);
                break;
            case WorldType.Arean:
                RttWorldgenPlanet = _areanPlanet.Generate(RttWorldgenPlanet, primaryStar);
                break;
            case WorldType.Arid:
                RttWorldgenPlanet = _aridPlanet.Generate(RttWorldgenPlanet, primaryStar);
                break;
            case WorldType.Asphodelian:
                RttWorldgenPlanet = _asphodelianPlanet.Generate(RttWorldgenPlanet);
                break;
            case WorldType.Chthonian:
                RttWorldgenPlanet = _chthonianPlanet.Generate(RttWorldgenPlanet);
                break;
            case WorldType.Hebean:
                RttWorldgenPlanet = _hebeanPlanet.Generate(RttWorldgenPlanet);
                break;
            case WorldType.Helian:
                RttWorldgenPlanet = _helianPlanet.Generate(RttWorldgenPlanet);
                break;
            case WorldType.JaniLithic:
                RttWorldgenPlanet = _janiLithicPlanet.Generate(RttWorldgenPlanet);
                break;
            case WorldType.Jovian:
                RttWorldgenPlanet = _jovianPlanet.Generate(RttWorldgenPlanet, primaryStar);
                break;
            case WorldType.Meltball:
                RttWorldgenPlanet = _meltballPlanet.Generate(RttWorldgenPlanet);
                break;
            case WorldType.Oceanic:
                RttWorldgenPlanet = _oceanicPlanet.Generate(RttWorldgenPlanet, primaryStar);
                break;
            case WorldType.Panthalassic:
                RttWorldgenPlanet = _panthalassicPlanet.Generate(RttWorldgenPlanet, primaryStar);
                break;
            case WorldType.Promethian:
                RttWorldgenPlanet = _promethianPlanet.Generate(RttWorldgenPlanet, primaryStar);
                break;
            case WorldType.Rockball:
                RttWorldgenPlanet = _rockballPlanet.Generate(RttWorldgenPlanet, primaryStar);
                break;
            case WorldType.Snowball:
                RttWorldgenPlanet = _snowballPlanet.Generate(RttWorldgenPlanet, primaryStar);
                break;
            case WorldType.Stygian:
                RttWorldgenPlanet = _stygianPlanet.Generate(RttWorldgenPlanet);
                break;
            case WorldType.Tectonic:
                RttWorldgenPlanet = _tectonicPlanet.Generate(RttWorldgenPlanet, primaryStar);
                break;
            case WorldType.Telluric:
                RttWorldgenPlanet = _telluricPlanet.Generate(RttWorldgenPlanet);
                break;
            case WorldType.Vesperian:
                RttWorldgenPlanet = _vesperianPlanet.Generate(RttWorldgenPlanet, primaryStar);
                break;
            case WorldType.None:
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    private void GetWorldType(RttWorldgenStar primaryStar)
    {
        if (
            primaryStar.Luminosity == Luminosity.III || primaryStar.SpectralType == SpectralType.D) {
            if (RttWorldgenPlanet.OrbitPosition <= primaryStar.ExpansionSize) {
                RttWorldgenPlanet.WorldType = RttWorldgenPlanet.PlanetType switch {
                    PlanetType.DwarfPlanet => WorldType.Stygian,
                    PlanetType.Terrestrial => WorldType.Acheronian,
                    PlanetType.Helian => WorldType.Asphodelian,
                    PlanetType.Jovian => WorldType.Chthonian,
                    _ => WorldType.None
                };
            }
        } else
            RttWorldgenPlanet.WorldType = RttWorldgenPlanet.PlanetType switch {
                PlanetType.DwarfPlanet => RttWorldgenPlanet.PlanetOrbit switch {
                    PlanetOrbit.Epistellar => GenerateEpistellarDwarfWorldType(),
                    PlanetOrbit.Inner => GenerateInnerDwarfWorldType(),
                    PlanetOrbit.Outer => GenerateOuterDwarfWorldType(),
                    _ => WorldType.None
                },
                PlanetType.Terrestrial => RttWorldgenPlanet.PlanetOrbit switch {
                    PlanetOrbit.Epistellar => GenerateEpistellarTerrestrialWorldType(),
                    PlanetOrbit.Inner => GenerateInnerTerrestrialWorldType(),
                    PlanetOrbit.Outer => GenerateOuterTerrestrialWorldType(),
                    _ => WorldType.None
                },
                PlanetType.Helian => RttWorldgenPlanet.PlanetOrbit switch {
                    PlanetOrbit.Epistellar => GenerateEpistellarHelianWorldType(),
                    PlanetOrbit.Inner => GenerateInnerHelianWorldType(),
                    PlanetOrbit.Outer => GenerateOuterHelianWorldType(),
                    _ => WorldType.None
                },
                PlanetType.Jovian => RttWorldgenPlanet.PlanetOrbit switch {
                    PlanetOrbit.Epistellar => GenerateEpistellarJovianWorldType(),
                    PlanetOrbit.Inner => GenerateInnerJovianWorldType(),
                    PlanetOrbit.Outer => GenerateOuterJovianWorldType(),
                    _ => WorldType.None
                },
                _ => WorldType.None
            };
    }

    private WorldType GenerateEpistellarDwarfWorldType()
    {
        return _rollingService.D6(1) switch {
            (<= 3) => WorldType.Rockball,
            (<= 5) => WorldType.Meltball,
            (>= 6) => _rollingService.D6(1) switch {
                (<= 4) => WorldType.Hebean,
                (>= 5) => WorldType.Promethian
            }
        };
    }

    private WorldType GenerateInnerDwarfWorldType()
    {
        return _rollingService.D6(1) switch {
            (<= 4) => WorldType.Rockball,
            (<= 6) => WorldType.Arean,
            7 => WorldType.Meltball,
            8 => _rollingService.D6(1) switch {
                (<= 4) => WorldType.Hebean,
                (<= 6) => WorldType.Promethian,
                _ => WorldType.None
            },
            _ => WorldType.None
        };
    }

    private WorldType GenerateOuterDwarfWorldType()
    {
        return _rollingService.D6(1) switch {
            (<= 4) => WorldType.Snowball,
            (<= 6) => WorldType.Rockball,
            7 => WorldType.Meltball,
            8 => _rollingService.D6(1) switch {
                (<= 3) => WorldType.Hebean,
                (<= 5) => WorldType.Arean,
                6 => WorldType.Promethian,
                _ => WorldType.None
            },
            _ => WorldType.None
        };
    }

    private WorldType GenerateEpistellarTerrestrialWorldType()
    {
        return _rollingService.D6(1) switch {
            (<= 4) => WorldType.JaniLithic,
            5 => WorldType.Vesperian,
            6 => WorldType.Telluric,
            _ => WorldType.None
        };
    }

    private WorldType GenerateInnerTerrestrialWorldType()
    {
        return _rollingService.D6(2) switch {
            (<= 4) => WorldType.Telluric,
            (<= 6) => WorldType.Arid,
            7 => WorldType.Tectonic,
            (<= 9) => WorldType.Oceanic,
            10 => WorldType.Tectonic,
            (<= 12) => WorldType.Telluric,
            _ => WorldType.None
        };
    }

    private WorldType GenerateOuterTerrestrialWorldType()
    {
        return _rollingService.D6(1) switch {
            (<= 4) => WorldType.Arid,
            (<= 6) => WorldType.Tectonic,
            (<= 8) => WorldType.Oceanic,
            _ => WorldType.None
        };
    }

    private WorldType GenerateEpistellarHelianWorldType()
    {
        return _rollingService.D6(1) switch {
            (<= 5) => WorldType.Helian,
            6 => WorldType.Asphodelian,
            _ => WorldType.None
        };
    }

    private WorldType GenerateInnerHelianWorldType()
    {
        return _rollingService.D6(1) switch {
            (<= 4) => WorldType.Helian,
            (<= 6) => WorldType.Panthalassic,
            _ => WorldType.None
        };
    }

    private WorldType GenerateOuterHelianWorldType()
    {
        return WorldType.Helian;
    }

    private WorldType GenerateEpistellarJovianWorldType()
    {
        return _rollingService.D6(1) switch {
            (<= 5) => WorldType.Jovian,
            6 => WorldType.Chthonian,
            _ => WorldType.None
        };
    }

    private WorldType GenerateInnerJovianWorldType()
    {
        return WorldType.Jovian;
    }

    private WorldType GenerateOuterJovianWorldType()
    {
        return WorldType.Jovian;
    }

    private void GenerateDesirability(RttWorldgenStar primaryStar)
    {
        RttWorldgenPlanet.Desirability = 0;
        switch (RttWorldgenPlanet.PlanetType) {
            case PlanetType.AsteroidBelt:
                RttWorldgenPlanet.Desirability = _rollingService.D6(1) - _rollingService.D6(1);
                if (primaryStar is {SpectralType: SpectralType.M, Luminosity: Luminosity.Ve}) {
                    RttWorldgenPlanet.Desirability -= _rollingService.D3(1);
                }

                if (RttWorldgenPlanet.PlanetOrbit == PlanetOrbit.Inner) {
                    if (primaryStar is {SpectralType: SpectralType.M, Luminosity: Luminosity.V}) {
                        RttWorldgenPlanet.Desirability += 1;
                    } else if (primaryStar.SpectralType is SpectralType.K or SpectralType.G or SpectralType.F) {
                        RttWorldgenPlanet.Desirability += 2;
                    }
                }

                break;
            case PlanetType.Terrestrial:
            case PlanetType.Jovian:
            case PlanetType.DwarfPlanet:
            case PlanetType.Helian:
                if (RttWorldgenPlanet.Hydrographics == 0) {
                    RttWorldgenPlanet.Desirability--;
                }

                if (RttWorldgenPlanet.Size >= 13 || RttWorldgenPlanet.Atmosphere is >= 12 and <= 16 ||
                    RttWorldgenPlanet.Hydrographics == 15) {
                    RttWorldgenPlanet.Desirability -= 2;
                }

                if (primaryStar is {SpectralType: SpectralType.M, Luminosity: Luminosity.Ve}) {
                    RttWorldgenPlanet.Desirability -= _rollingService.D3(1);
                }

                if (RttWorldgenPlanet.Size is >= 1 and <= 11
                    && RttWorldgenPlanet.Atmosphere is >= 2 and <= 9
                    && RttWorldgenPlanet.Hydrographics is >= 0 and <= 11) {
                    if (RttWorldgenPlanet.Size is >= 5 and <= 10
                        && RttWorldgenPlanet.Atmosphere is >= 4 and <= 9
                        && RttWorldgenPlanet.Hydrographics is >= 4 and <= 8) {
                        RttWorldgenPlanet.Desirability += 5;
                    } else if (RttWorldgenPlanet.Hydrographics is 10 or 11) {
                        RttWorldgenPlanet.Desirability += 3;
                    } else if (RttWorldgenPlanet.Atmosphere is >= 2 and <= 6
                               && RttWorldgenPlanet.Hydrographics is >= 0 and <= 3) {
                        RttWorldgenPlanet.Desirability += 2;
                    } else {
                        RttWorldgenPlanet.Desirability += 4;
                    }
                }

                if (RttWorldgenPlanet.Size >= 11 && RttWorldgenPlanet.Atmosphere <= 15) {
                    RttWorldgenPlanet.Desirability--;
                }

                if (RttWorldgenPlanet.PlanetOrbit == PlanetOrbit.Inner) {
                    if (primaryStar is {SpectralType: SpectralType.M, Luminosity: Luminosity.V}) {
                        RttWorldgenPlanet.Desirability += 1;
                    } else if (primaryStar.SpectralType is SpectralType.K or SpectralType.G or SpectralType.F) {
                        RttWorldgenPlanet.Desirability += 2;
                    }
                }

                if (RttWorldgenPlanet.Size == 0) {
                    RttWorldgenPlanet.Desirability--;
                }

                if (RttWorldgenPlanet.Atmosphere is >= 6 and <= 8) {
                    RttWorldgenPlanet.Desirability++;
                }

                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    private void GenerateRings()
    {
        if (RttWorldgenPlanet.PlanetType == PlanetType.Jovian) {
            RttWorldgenPlanet.Rings = _rollingService.D6(1) <= 4 ? Rings.Minor : Rings.Complex;
        }
    }

    private void GeneratePlanetType(RttWorldgenStar primaryStar)
    {
        var roll = _rollingService.D6(1);

        if (primaryStar.SpectralType == SpectralType.L) {
            roll--;
        }

        RttWorldgenPlanet.PlanetType = roll switch {
            <= 1 => PlanetType.AsteroidBelt,
            2 => PlanetType.DwarfPlanet,
            3 => PlanetType.Terrestrial,
            4 => PlanetType.Helian,
            >= 5 => PlanetType.Jovian
        };
    }

    private void GenerateSatellites(RttWorldgenStar primaryStar)
    {
        int satelliteNum;

        switch (RttWorldgenPlanet.PlanetType) {
            case PlanetType.AsteroidBelt:
                if (_rollingService.D6(1) >= 5) {
                    RttWorldgenPlanet.Satellites.Add(GenerateRttWorldgenSatellite(primaryStar, PlanetType.DwarfPlanet,
                        RttWorldgenPlanet.Id));
                }

                break;
            case PlanetType.DwarfPlanet:
                if (_rollingService.D6(1) == 6) {
                    RttWorldgenPlanet.Satellites.Add(GenerateRttWorldgenSatellite(primaryStar, PlanetType.DwarfPlanet,
                        RttWorldgenPlanet.Id));
                }

                break;
            case PlanetType.Terrestrial:
                if (_rollingService.D6(1) >= 5) {
                    RttWorldgenPlanet.Satellites.Add(GenerateRttWorldgenSatellite(primaryStar, PlanetType.DwarfPlanet,
                        RttWorldgenPlanet.Id));
                }

                break;
            case PlanetType.Helian:
                satelliteNum = _rollingService.D6(1) - 3;
                var terrestrialSatellite = _rollingService.D6(1) == 6;

                for (var i = 0; i < satelliteNum; i++) {
                    if (i == 0 && terrestrialSatellite) {
                        RttWorldgenPlanet.Satellites.Add(GenerateRttWorldgenSatellite(primaryStar,
                            PlanetType.Terrestrial, RttWorldgenPlanet.Id));
                    } else {
                        RttWorldgenPlanet.Satellites.Add(GenerateRttWorldgenSatellite(primaryStar,
                            PlanetType.DwarfPlanet, RttWorldgenPlanet.Id));
                    }
                }

                break;
            case PlanetType.Jovian:
                satelliteNum = _rollingService.D6(1);

                for (var i = 0; i < satelliteNum; i++) {
                    if (i == 0 && _rollingService.D6(1) == 6) {
                        RttWorldgenPlanet.Satellites.Add(_rollingService.D6(1) == 6
                            ? GenerateRttWorldgenSatellite(primaryStar, PlanetType.Helian, RttWorldgenPlanet.Id)
                            : GenerateRttWorldgenSatellite(primaryStar, PlanetType.Terrestrial, RttWorldgenPlanet.Id));
                    } else {
                        RttWorldgenPlanet.Satellites.Add(GenerateRttWorldgenSatellite(primaryStar,
                            PlanetType.DwarfPlanet, RttWorldgenPlanet.Id));
                    }
                }

                break;
        }
    }

    private void GenerateSettlement()
    {
        if (RttWorldgenPlanet.Population >= 1) {
            return;
        }

        if (RttWorldgenPlanet.Biosphere >= 12) {
            RttWorldgenPlanet.TechLevel = 10 + _rollingService.D6(1);
            if (RttWorldgenPlanet.Chemistry == PlanetChemistry.Water) {
                RttWorldgenPlanet.Desirability = 8;
                RttWorldgenPlanet.Population = 8 + _rollingService.D3(1) - _rollingService.D3(1);
            } else {
                RttWorldgenPlanet.Population = _rollingService.D6(2);
            }

            RttWorldgenPlanet.Government = RttWorldgenPlanet.Population + _rollingService.D6(2) - 7;
        }

        RttWorldgenPlanet.LawLevel = RttWorldgenPlanet.Government + _rollingService.D6(2) - 7;

        var mod = RttWorldgenPlanet.LawLevel switch {
            (>= 1 and <= 3) => 1,
            (>= 6 and <= 9) => -1,
            (>= 10 and <= 12) => -2,
            (>= 13) => -3,
            _ => 0
        };
        if (RttWorldgenPlanet.Atmosphere is >= 0 and <= 4 or 7 or >= 9
            || RttWorldgenPlanet.Hydrographics == 15) {
            mod++;
        }

        mod += RttWorldgenPlanet.TechLevel switch {
            >= 12 and <= 14 => 1,
            >= 15 => 2,
            _ => 0
        };
        RttWorldgenPlanet.IndustrialBase = RttWorldgenPlanet.Population + _rollingService.D6(2) - 7 + mod;

        switch (RttWorldgenPlanet.IndustrialBase) {
            case 0:
                RttWorldgenPlanet.Population = -1;
                break;
            case >= 4 and <= 9:
                RttWorldgenPlanet.Atmosphere = RttWorldgenPlanet.Atmosphere switch {
                    3 => 2,
                    5 => 4,
                    6 => 7,
                    8 => 9,
                    _ => RttWorldgenPlanet.Atmosphere
                };
                break;
            case >= 10:
                if (RttWorldgenPlanet.Atmosphere is 3 or 5 or 6 or 8) {
                    if (_rollingService.D3(1) >= 2) {
                        RttWorldgenPlanet.Population++;
                        RttWorldgenPlanet.Atmosphere = RttWorldgenPlanet.Atmosphere switch {
                            3 => 2,
                            5 => 4,
                            6 => 7,
                            8 => 9,
                            _ => RttWorldgenPlanet.Atmosphere
                        };
                    } else {
                        RttWorldgenPlanet.Population += 2;
                    }
                }

                break;
        }
    }
}