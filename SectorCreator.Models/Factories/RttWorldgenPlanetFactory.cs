using SectorCreator.Global;
using SectorCreator.Global.Enums;
using SectorCreator.Models.Basic;
using SectorCreator.Models.RTTWorldgen;
using SectorCreator.Models.RTTWorldgen.Worlds;

namespace SectorCreator.Models.Factories;

public interface IRttWorldgenPlanetFactory
{
    RttWorldgenPlanet GenerateRttWorldgenPlanet(RttWorldgenStar primaryStar, PlanetOrbit planetOrbit,
        int orbitPosition);

    Planet Generate(SectorType sectorType);
}

public class RttWorldgenPlanetFactory : PlanetFactory, IRttWorldgenPlanetFactory
{
    private readonly IRollingService _rollingService;
    private readonly IAcheronianWorld _acheronianWorld;
    private readonly IAreanWorld _areanWorld;
    private readonly IAridWorld _aridWorld;
    private readonly IAsphodelianWorld _asphodelianWorld;
    private readonly IChthonianWorld _chthonianWorld;
    private readonly IHebeanWorld _hebeanWorld;
    private readonly IHelianWorld _helianWorld;
    private readonly IJaniLithicWorld _janiLithicWorld;
    private readonly IJovianWorld _jovianWorld;
    private readonly IMeltballWorld _meltballWorld;
    private readonly IOceanicWorld _oceanicWorld;
    private readonly IPanthalassicWorld _panthalassicWorld;
    private readonly IPrometheanWorld _prometheanWorld;
    private readonly IRockballWorld _rockballWorld;
    private readonly ISnowballWorld _snowballWorld;
    private readonly IStygianWorld _stygianWorld;
    private readonly ITectonicWorld _tectonicWorld;
    private readonly ITelluricWorld _telluricWorld;
    private readonly IVesperianWorld _vesperianWorld;

    public RttWorldgenPlanetFactory(IRollingService rollingService,
        IAcheronianWorld acheronianWorld,
        IAreanWorld areanWorld,
        IAridWorld aridWorld,
        IAsphodelianWorld asphodelianWorld,
        IChthonianWorld chthonianWorld,
        IHebeanWorld hebeanWorld,
        IHelianWorld helianWorld,
        IJaniLithicWorld janiLithicWorld,
        IJovianWorld jovianWorld,
        IMeltballWorld meltballWorld,
        IOceanicWorld oceanicWorld,
        IPanthalassicWorld panthalassicWorld,
        IPrometheanWorld prometheanWorld,
        IRockballWorld rockballWorld,
        ISnowballWorld snowballWorld,
        IStygianWorld stygianWorld,
        ITectonicWorld tectonicWorld,
        ITelluricWorld telluricWorld,
        IVesperianWorld vesperianWorld)
        : base(rollingService)
    {
        _rollingService = rollingService;
        _acheronianWorld = acheronianWorld;
        _areanWorld = areanWorld;
        _aridWorld = aridWorld;
        _asphodelianWorld = asphodelianWorld;
        _chthonianWorld = chthonianWorld;
        _hebeanWorld = hebeanWorld;
        _helianWorld = helianWorld;
        _janiLithicWorld = janiLithicWorld;
        _jovianWorld = jovianWorld;
        _meltballWorld = meltballWorld;
        _oceanicWorld = oceanicWorld;
        _panthalassicWorld = panthalassicWorld;
        _prometheanWorld = prometheanWorld;
        _rockballWorld = rockballWorld;
        _snowballWorld = snowballWorld;
        _stygianWorld = stygianWorld;
        _tectonicWorld = tectonicWorld;
        _telluricWorld = telluricWorld;
        _vesperianWorld = vesperianWorld;
    }

    private RttWorldgenPlanet RttWorldgenPlanet { get; set; } = new();

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
                RttWorldgenPlanet = _acheronianWorld.Generate(RttWorldgenPlanet);
                break;
            case WorldType.Arean:
                RttWorldgenPlanet = _areanWorld.Generate(RttWorldgenPlanet, primaryStar);
                break;
            case WorldType.Arid:
                RttWorldgenPlanet = _aridWorld.Generate(RttWorldgenPlanet, primaryStar);
                break;
            case WorldType.Asphodelian:
                RttWorldgenPlanet = _asphodelianWorld.Generate(RttWorldgenPlanet);
                break;
            case WorldType.Chthonian:
                RttWorldgenPlanet = _chthonianWorld.Generate(RttWorldgenPlanet);
                break;
            case WorldType.Hebean:
                RttWorldgenPlanet = _hebeanWorld.Generate(RttWorldgenPlanet);
                break;
            case WorldType.Helian:
                RttWorldgenPlanet = _helianWorld.Generate(RttWorldgenPlanet);
                break;
            case WorldType.JaniLithic:
                RttWorldgenPlanet = _janiLithicWorld.Generate(RttWorldgenPlanet);
                break;
            case WorldType.Jovian:
                RttWorldgenPlanet = _jovianWorld.Generate(RttWorldgenPlanet, primaryStar);
                break;
            case WorldType.Meltball:
                RttWorldgenPlanet = _meltballWorld.Generate(RttWorldgenPlanet);
                break;
            case WorldType.Oceanic:
                RttWorldgenPlanet = _oceanicWorld.Generate(RttWorldgenPlanet, primaryStar);
                break;
            case WorldType.Panthalassic:
                RttWorldgenPlanet = _panthalassicWorld.Generate(RttWorldgenPlanet, primaryStar);
                break;
            case WorldType.Promethean:
                RttWorldgenPlanet = _prometheanWorld.Generate(RttWorldgenPlanet, primaryStar);
                break;
            case WorldType.Rockball:
                RttWorldgenPlanet = _rockballWorld.Generate(RttWorldgenPlanet, primaryStar);
                break;
            case WorldType.Snowball:
                RttWorldgenPlanet = _snowballWorld.Generate(RttWorldgenPlanet, primaryStar);
                break;
            case WorldType.Stygian:
                RttWorldgenPlanet = _stygianWorld.Generate(RttWorldgenPlanet);
                break;
            case WorldType.Tectonic:
                RttWorldgenPlanet = _tectonicWorld.Generate(RttWorldgenPlanet, primaryStar);
                break;
            case WorldType.Telluric:
                RttWorldgenPlanet = _telluricWorld.Generate(RttWorldgenPlanet);
                break;
            case WorldType.Vesperian:
                RttWorldgenPlanet = _vesperianWorld.Generate(RttWorldgenPlanet, primaryStar);
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
                (>= 5) => WorldType.Promethean
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
                (<= 6) => WorldType.Promethean,
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
                6 => WorldType.Promethean,
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