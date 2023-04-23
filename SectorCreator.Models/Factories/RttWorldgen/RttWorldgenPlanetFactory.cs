using SectorCreator.Global;
using SectorCreator.Global.Enums;
using SectorCreator.Models.Basic;
using SectorCreator.Models.Factories.Basic;
using SectorCreator.Models.RTTWorldgen;
using SectorCreator.Models.RTTWorldgen.Worlds;

namespace SectorCreator.Models.Factories.RttWorldgen;

public interface IRttWorldgenPlanetFactory
{
    RttWorldgenPlanet GenerateRttWorldgenPlanet(RttWorldgenStar primaryStar, PlanetOrbit planetOrbit, int orbitPosition);

    Planet Generate(SectorType sectorType);
}

public class RttWorldgenPlanetFactory : PlanetFactory, IRttWorldgenPlanetFactory
{
    private readonly IAcheronianWorld _acheronianWorld = new AcheronianWorld(new RollingService());
    private readonly IAreanWorld _areanWorld = new AreanWorld(new RollingService());
    private readonly IAridWorld _aridWorld = new AridWorld(new RollingService());
    private readonly IAsphodelianWorld _asphodelianWorld = new AsphodelianWorld(new RollingService());
    private readonly IChthonianWorld _chthonianWorld = new ChthonianWorld();
    private readonly IHebeanWorld _hebeanWorld = new HebeanWorld(new RollingService());
    private readonly IHelianWorld _helianWorld = new HelianWorld(new RollingService());
    private readonly IJaniLithicWorld _janiLithicWorld = new JaniLithicWorld(new RollingService());
    private readonly IJovianWorld _jovianWorld = new JovianWorld(new RollingService());
    private readonly IMeltballWorld _meltballWorld = new MeltballWorld(new RollingService());
    private readonly IOceanicWorld _oceanicWorld = new OceanicWorld(new RollingService());
    private readonly IPanthalassicWorld _panthalassicWorld = new PanthalassicWorld(new RollingService());
    private readonly IPrometheanWorld _prometheanWorld = new PrometheanWorld(new RollingService());
    private readonly IRockballWorld _rockballWorld = new RockballWorld(new RollingService());
    private readonly ISnowballWorld _snowballWorld = new SnowballWorld(new RollingService());
    private readonly IStygianWorld _stygianWorld = new StygianWorld(new RollingService());
    private readonly ITectonicWorld _tectonicWorld = new TectonicWorld(new RollingService());
    private readonly ITelluricWorld _telluricWorld = new TelluricWorld(new RollingService());
    private readonly IVesperianWorld _vesperianWorld = new VesperianWorld(new RollingService());

    public RttWorldgenPlanetFactory(IRollingService rollingService) : base(rollingService)
    { }

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

    public RttWorldgenPlanet RttWorldgenPlanet { get; set; } = new();

    public RttWorldgenPlanet GenerateRttWorldgenPlanet(RttWorldgenStar primaryStar, PlanetOrbit planetOrbit, int orbitPosition)
    {
        GeneratePlanetType(primaryStar);
        GenerateSatellites();
        GenerateWorldType(primaryStar);

        RttWorldgenPlanet.PlanetOrbit = planetOrbit;
        RttWorldgenPlanet.OrbitPosition = orbitPosition;

        return RttWorldgenPlanet;
    }

    private Planet GenerateNewSatellite(PlanetType planetType)
    {
        return new RttWorldgenPlanet {
            PlanetType = planetType,
            ParentId = RttWorldgenPlanet.Id,
            ParentType = RttWorldgenPlanet.PlanetType
        };
    }

    public void GeneratePlanetType(RttWorldgenStar primaryStar)
    {
        var roll = _rollingService.D6(1);

        if (primaryStar.SpectralType == SpectralType.L) {
            roll--;
        }

        RttWorldgenPlanet.PlanetType = roll switch {
            (<= 1) => PlanetType.AsteroidBelt,
            2 => PlanetType.DwarfPlanet,
            3 => PlanetType.Terrestrial,
            4 => PlanetType.Helian,
            (>= 5) => PlanetType.Jovian
        };
    }

    public void GenerateSatellites()
    {
        int numSatellites;
        switch (RttWorldgenPlanet.PlanetType) {
            case PlanetType.AsteroidBelt:
                if (_rollingService.D6(1) >= 5) {
                    RttWorldgenPlanet.Satellites.Add(GenerateNewSatellite(PlanetType.DwarfPlanet));
                }

                break;
            case PlanetType.DwarfPlanet:
                if (_rollingService.D6(1) >= 6) {
                    RttWorldgenPlanet.Satellites.Add(GenerateNewSatellite(PlanetType.DwarfPlanet));
                }

                break;
            case PlanetType.Terrestrial:
                if (_rollingService.D6(1) >= 5) {
                    RttWorldgenPlanet.Satellites.Add(GenerateNewSatellite(PlanetType.DwarfPlanet));
                }

                break;
            case PlanetType.Helian:
                numSatellites = _rollingService.D6(1) - 3;

                for (var i = 0; i < numSatellites; i++) {
                    RttWorldgenPlanet.Satellites.Add(_rollingService.D6(1) == 6
                        ? GenerateNewSatellite(PlanetType.Terrestrial)
                        : GenerateNewSatellite(PlanetType.DwarfPlanet));
                }

                break;
            case PlanetType.Jovian:
                var type = PlanetType.DwarfPlanet;
                numSatellites = _rollingService.D6(1);
                if (_rollingService.D6(1) == 6) {
                    type = _rollingService.D6(1) <= 5 ? PlanetType.Terrestrial : PlanetType.Helian;
                }

                var first = true;
                for (var i = 0; i < numSatellites; i++) {
                    RttWorldgenPlanet.Satellites.Add(GenerateNewSatellite(first ? type : PlanetType.DwarfPlanet));
                    first = false;
                }

                break;
        }
    }

    public void GenerateWorldType(RttWorldgenStar primaryStar)
    {
        if ((primaryStar.SpectralType == SpectralType.D || primaryStar.Luminosity == Luminosity.III)
            && RttWorldgenPlanet.OrbitPosition <= primaryStar.ExpansionSize) {
            RttWorldgenPlanet = RttWorldgenPlanet.PlanetType switch {
                PlanetType.AsteroidBelt => new RttWorldgenPlanet(RttWorldgenPlanet) {WorldType = WorldType.AsteroidBelt},
                PlanetType.DwarfPlanet => _stygianWorld.Generate(RttWorldgenPlanet),
                PlanetType.Terrestrial => _acheronianWorld.Generate(RttWorldgenPlanet),
                PlanetType.Helian => _asphodelianWorld.Generate(RttWorldgenPlanet),
                PlanetType.Jovian => _chthonianWorld.Generate(RttWorldgenPlanet)
            };
            return;
        }

        RttWorldgenPlanet = RttWorldgenPlanet.PlanetOrbit switch {
            PlanetOrbit.Epistellar => RttWorldgenPlanet.PlanetType switch {
                PlanetType.AsteroidBelt => new RttWorldgenPlanet(RttWorldgenPlanet) {WorldType = WorldType.AsteroidBelt},
                PlanetType.DwarfPlanet => (_rollingService.D6(1) - (RttWorldgenPlanet.ParentType == PlanetType.AsteroidBelt ? 2 : 0)) switch {
                    (<= 3) => _rockballWorld.Generate(RttWorldgenPlanet, primaryStar),
                    4 or 5 => _meltballWorld.Generate(RttWorldgenPlanet),
                    (>= 6) => _rollingService.D6(1) switch {
                        (<= 4) => _hebeanWorld.Generate(RttWorldgenPlanet),
                        (>= 5) => _prometheanWorld.Generate(RttWorldgenPlanet, primaryStar)
                    }
                },
                PlanetType.Terrestrial => _rollingService.D6(1) switch {
                    (<= 4) => _janiLithicWorld.Generate(RttWorldgenPlanet),
                    5 => _vesperianWorld.Generate(RttWorldgenPlanet, primaryStar),
                    (>= 6) => _telluricWorld.Generate(RttWorldgenPlanet)
                },
                PlanetType.Helian => _rollingService.D6(1) switch {
                    (<= 5) => _helianWorld.Generate(RttWorldgenPlanet),
                    (>= 6) => _asphodelianWorld.Generate(RttWorldgenPlanet)
                },
                PlanetType.Jovian => _rollingService.D6(1) switch {
                    (<= 5) => _jovianWorld.Generate(RttWorldgenPlanet, primaryStar),
                    (>= 6) => _chthonianWorld.Generate(RttWorldgenPlanet)
                }
            },
            PlanetOrbit.Inner => RttWorldgenPlanet.PlanetType switch {
                PlanetType.AsteroidBelt => new RttWorldgenPlanet(RttWorldgenPlanet) {WorldType = WorldType.AsteroidBelt},
                PlanetType.DwarfPlanet => (_rollingService.D6(1)
                                           + RttWorldgenPlanet.ParentType switch {
                                               PlanetType.AsteroidBelt => -2,
                                               PlanetType.Helian => 1,
                                               PlanetType.Jovian => 2,
                                               _ => 0
                                           }) switch {
                    (<= 4) => _rockballWorld.Generate(RttWorldgenPlanet, primaryStar),
                    5 or 6 => _areanWorld.Generate(RttWorldgenPlanet, primaryStar),
                    7 => _meltballWorld.Generate(RttWorldgenPlanet),
                    (>= 8) => _rollingService.D6(1) switch {
                        (<= 4) => _hebeanWorld.Generate(RttWorldgenPlanet),
                        (>= 5) => _prometheanWorld.Generate(RttWorldgenPlanet, primaryStar)
                    }
                },
                PlanetType.Terrestrial => _rollingService.D6(2) switch {
                    (<= 4) => _telluricWorld.Generate(RttWorldgenPlanet),
                    5 or 6 => _aridWorld.Generate(RttWorldgenPlanet, primaryStar),
                    7 => _tectonicWorld.Generate(RttWorldgenPlanet, primaryStar),
                    8 or 9 => _oceanicWorld.Generate(RttWorldgenPlanet, primaryStar),
                    10 => _tectonicWorld.Generate(RttWorldgenPlanet, primaryStar),
                    (>= 11) => _telluricWorld.Generate(RttWorldgenPlanet)
                },
                PlanetType.Helian => _rollingService.D6(1) switch {
                    (<= 4) => _helianWorld.Generate(RttWorldgenPlanet),
                    (>= 5) => _panthalassicWorld.Generate(RttWorldgenPlanet, primaryStar)
                },
                PlanetType.Jovian => _jovianWorld.Generate(RttWorldgenPlanet, primaryStar)
            },
            PlanetOrbit.Outer => RttWorldgenPlanet.PlanetType switch {
                PlanetType.AsteroidBelt => new RttWorldgenPlanet(RttWorldgenPlanet) {WorldType = WorldType.AsteroidBelt},
                PlanetType.DwarfPlanet => (_rollingService.D6(1)
                                           + RttWorldgenPlanet.ParentType switch {
                                               PlanetType.AsteroidBelt => -1,
                                               PlanetType.Helian => 1,
                                               PlanetType.Jovian => 2,
                                               _ => 0
                                           }) switch {
                    (<= 3) => _rockballWorld.Generate(RttWorldgenPlanet, primaryStar),
                    (>= 4 and <= 6) => _snowballWorld.Generate(RttWorldgenPlanet, primaryStar),
                    7 => _meltballWorld.Generate(RttWorldgenPlanet),
                    (>= 8) => _rollingService.D6(1) switch {
                        (<= 3) => _hebeanWorld.Generate(RttWorldgenPlanet),
                        4 or 5 => _areanWorld.Generate(RttWorldgenPlanet, primaryStar),
                        (>= 6) => _prometheanWorld.Generate(RttWorldgenPlanet, primaryStar)
                    }
                },
                PlanetType.Terrestrial => (_rollingService.D6(1) + (RttWorldgenPlanet.ParentId != Guid.Empty ? 2 : 0)) switch {
                        (<= 4) => _aridWorld.Generate(RttWorldgenPlanet, primaryStar),
                        5 or 6 => _tectonicWorld.Generate(RttWorldgenPlanet, primaryStar),
                        (>= 7) => _oceanicWorld.Generate(RttWorldgenPlanet, primaryStar)
                    },
                PlanetType.Helian => _helianWorld.Generate(RttWorldgenPlanet),
                PlanetType.Jovian => _jovianWorld.Generate(RttWorldgenPlanet, primaryStar)
            }
        };
    }
}