using SectorCreator.Global;
using SectorCreator.Global.Enums;
using SectorCreator.Models.Basic;
using SectorCreator.Models.Basic.Factories;
using SectorCreator.Models.RTTWorldgen.Worlds;
using SectorCreator.Models.Services.TradeCodeService;

namespace SectorCreator.Models.RTTWorldgen.Factories;

public interface IRttWorldgenPlanetFactory
{
    RttWorldgenPlanet GenerateRttWorldgenPlanet(RttWorldgenStar primaryStar, PlanetOrbit planetOrbit, int orbitPosition, Coordinates coordinates);

    Planet Generate(SectorType sectorType, Coordinates coordinates);
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
        IVesperianWorld vesperianWorld) : base(rollingService)
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

    public RttWorldgenPlanet RttWorldgenPlanet { get; set; } = new(new RollingService());
    public RttWorldgenPlanet RttWorldgenSatellite { get; set; }
    public RttWorldgenStar PrimaryStar { get; set; }

    public RttWorldgenPlanet GenerateRttWorldgenPlanet(RttWorldgenStar primaryStar, PlanetOrbit planetOrbit, int orbitPosition,
        Coordinates coordinates)
    {
        RttWorldgenPlanet = new RttWorldgenPlanet(new RollingService()) {
            IsMainWorld = true,
            PlanetOrbit = planetOrbit,
            OrbitPosition = orbitPosition,
            Coordinates = coordinates
        };
        PrimaryStar = primaryStar;

        RttWorldgenPlanet = GeneratePlanetType(RttWorldgenPlanet, primaryStar);
        RttWorldgenPlanet = GenerateSatellites(RttWorldgenPlanet);
        RttWorldgenPlanet = GenerateWorldType(RttWorldgenPlanet, primaryStar);

        RttWorldgenPlanet.GenerateTemperature(PrimaryStar.SpectralType);

        RttWorldgenPlanet = PopulatePlanet(RttWorldgenPlanet, coordinates);

        return RttWorldgenPlanet;
    }

    private RttWorldgenPlanet PopulatePlanet(RttWorldgenPlanet rttWorldgenPlanet, Coordinates coordinates)
    {
        rttWorldgenPlanet.Coordinates = coordinates;

        if (rttWorldgenPlanet.Size != 16) {
            rttWorldgenPlanet.Populate(PrimaryStar);
        }

        rttWorldgenPlanet.GenerateName();

        rttWorldgenPlanet.TradeCodes = TradeCodeService.AddTradeCodes(rttWorldgenPlanet);
        rttWorldgenPlanet.GenerateImportance();
        rttWorldgenPlanet.GenerateCulturalExtension();
        rttWorldgenPlanet.SetNobility();
        rttWorldgenPlanet.SetTravelZone();
        rttWorldgenPlanet.GenerateCulturalDifferences();
        rttWorldgenPlanet.Bases = rttWorldgenPlanet.Bases.Distinct().ToList();
        rttWorldgenPlanet.Bases.Sort();

        return rttWorldgenPlanet;
    }

    private RttWorldgenPlanet GenerateNewSatellite(PlanetType planetType, Coordinates coordinates, SpectralType primaryStarSpectralType)
    {
        RttWorldgenSatellite = new RttWorldgenPlanet(new RollingService()) {
            PlanetType = planetType,
            ParentId = RttWorldgenPlanet.Id,
            ParentType = RttWorldgenPlanet.PlanetType,
            IsMainWorld = false
        };

        GenerateWorldType(RttWorldgenSatellite, PrimaryStar);

        RttWorldgenSatellite.GenerateTemperature(primaryStarSpectralType);
        RttWorldgenSatellite = PopulatePlanet(RttWorldgenSatellite, coordinates);

        return RttWorldgenSatellite;
    }


    public RttWorldgenPlanet GeneratePlanetType(RttWorldgenPlanet planet, RttWorldgenStar primaryStar)
    {
        var roll = _rollingService.D6(1);

        if (primaryStar.SpectralType == SpectralType.BD) {
            roll--;
        }

        planet.PlanetType = roll switch {
            (<= 1) => PlanetType.AsteroidBelt,
            2 => PlanetType.DwarfPlanet,
            3 => PlanetType.Terrestrial,
            4 => PlanetType.Helian,
            (>= 5) => PlanetType.Jovian
        };

        return planet;
    }

    public RttWorldgenPlanet GenerateSatellites(RttWorldgenPlanet rttWorldgenPlanet)
    {
        int numSatellites;
        switch (rttWorldgenPlanet.PlanetType) {
            case PlanetType.AsteroidBelt:
                if (_rollingService.D6(1) >= 5) {
                    rttWorldgenPlanet.Satellites.Add(
                        new RttWorldgenPlanet(new RollingService(), GenerateNewSatellite(PlanetType.DwarfPlanet, rttWorldgenPlanet.Coordinates, PrimaryStar.SpectralType)));
                }

                break;
            case PlanetType.DwarfPlanet:
                if (_rollingService.D6(1) >= 6) {
                    rttWorldgenPlanet.Satellites.Add(
                        new RttWorldgenPlanet(new RollingService(), GenerateNewSatellite(PlanetType.DwarfPlanet, rttWorldgenPlanet.Coordinates, PrimaryStar.SpectralType)));
                }

                break;
            case PlanetType.Terrestrial:
                if (_rollingService.D6(1) >= 5) {
                    rttWorldgenPlanet.Satellites.Add(
                        new RttWorldgenPlanet(new RollingService(), GenerateNewSatellite(PlanetType.DwarfPlanet, rttWorldgenPlanet.Coordinates, PrimaryStar.SpectralType)));
                }

                break;
            case PlanetType.Helian:
                numSatellites = _rollingService.D6(1) - 3;

                for (var i = 0; i < numSatellites; i++) {
                    rttWorldgenPlanet.Satellites.Add(new RttWorldgenPlanet(new RollingService(), _rollingService.D6(1) == 6
                        ? GenerateNewSatellite(PlanetType.Terrestrial, rttWorldgenPlanet.Coordinates, PrimaryStar.SpectralType)
                        : GenerateNewSatellite(PlanetType.DwarfPlanet, rttWorldgenPlanet.Coordinates, PrimaryStar.SpectralType)));
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
                    rttWorldgenPlanet.Satellites.Add(
                        new RttWorldgenPlanet(new RollingService(), GenerateNewSatellite(first ? type : PlanetType.DwarfPlanet, rttWorldgenPlanet.Coordinates,
                            PrimaryStar.SpectralType)));
                    first = false;
                }

                break;
        }

        return rttWorldgenPlanet;
    }

    public RttWorldgenPlanet GenerateWorldType(RttWorldgenPlanet rttWorldgenPlanet, RttWorldgenStar primaryStar)
    {
        if ((primaryStar.SpectralType == SpectralType.D || primaryStar.LuminosityClass == LuminosityClass.III)
            && rttWorldgenPlanet.OrbitPosition <= primaryStar.ExpansionSize) {
            rttWorldgenPlanet = rttWorldgenPlanet.PlanetType switch {
                PlanetType.AsteroidBelt => new RttWorldgenPlanet(new RollingService(), rttWorldgenPlanet) {WorldType = WorldType.AsteroidBelt},
                PlanetType.DwarfPlanet => _stygianWorld.Generate(rttWorldgenPlanet),
                PlanetType.Terrestrial => _acheronianWorld.Generate(rttWorldgenPlanet),
                PlanetType.Helian => _asphodelianWorld.Generate(rttWorldgenPlanet),
                PlanetType.Jovian => _chthonianWorld.Generate(rttWorldgenPlanet)
            };
            return rttWorldgenPlanet;
        }

        rttWorldgenPlanet = rttWorldgenPlanet.PlanetOrbit switch {
            PlanetOrbit.Epistellar => rttWorldgenPlanet.PlanetType switch {
                PlanetType.AsteroidBelt => new RttWorldgenPlanet(new RollingService(), rttWorldgenPlanet) {WorldType = WorldType.AsteroidBelt},
                PlanetType.DwarfPlanet => (_rollingService.D6(1) - (rttWorldgenPlanet.ParentType == PlanetType.AsteroidBelt ? 2 : 0)) switch {
                    (<= 3) => _rockballWorld.Generate(rttWorldgenPlanet, primaryStar),
                    4 or 5 => _meltballWorld.Generate(rttWorldgenPlanet),
                    (>= 6) => _rollingService.D6(1) switch {
                        (<= 4) => _hebeanWorld.Generate(rttWorldgenPlanet),
                        (>= 5) => _prometheanWorld.Generate(rttWorldgenPlanet, primaryStar)
                    }
                },
                PlanetType.Terrestrial => _rollingService.D6(1) switch {
                    (<= 4) => _janiLithicWorld.Generate(rttWorldgenPlanet),
                    5 => _vesperianWorld.Generate(rttWorldgenPlanet, primaryStar),
                    (>= 6) => _telluricWorld.Generate(rttWorldgenPlanet)
                },
                PlanetType.Helian => _rollingService.D6(1) switch {
                    (<= 5) => _helianWorld.Generate(rttWorldgenPlanet),
                    (>= 6) => _asphodelianWorld.Generate(rttWorldgenPlanet)
                },
                PlanetType.Jovian => _rollingService.D6(1) switch {
                    (<= 5) => _jovianWorld.Generate(rttWorldgenPlanet, primaryStar),
                    (>= 6) => _chthonianWorld.Generate(rttWorldgenPlanet)
                }
            },
            PlanetOrbit.Inner => rttWorldgenPlanet.PlanetType switch {
                PlanetType.AsteroidBelt => new RttWorldgenPlanet(new RollingService(), rttWorldgenPlanet) {WorldType = WorldType.AsteroidBelt},
                PlanetType.DwarfPlanet => (_rollingService.D6(1)
                                           + rttWorldgenPlanet.ParentType switch {
                                               PlanetType.AsteroidBelt => -2,
                                               PlanetType.Helian => 1,
                                               PlanetType.Jovian => 2,
                                               _ => 0
                                           }) switch {
                    (<= 4) => _rockballWorld.Generate(rttWorldgenPlanet, primaryStar),
                    5 or 6 => _areanWorld.Generate(rttWorldgenPlanet, primaryStar),
                    7 => _meltballWorld.Generate(rttWorldgenPlanet),
                    (>= 8) => _rollingService.D6(1) switch {
                        (<= 4) => _hebeanWorld.Generate(rttWorldgenPlanet),
                        (>= 5) => _prometheanWorld.Generate(rttWorldgenPlanet, primaryStar)
                    }
                },
                PlanetType.Terrestrial => _rollingService.D6(2) switch {
                    (<= 4) => _telluricWorld.Generate(rttWorldgenPlanet),
                    5 or 6 => _aridWorld.Generate(rttWorldgenPlanet, primaryStar),
                    7 => _tectonicWorld.Generate(rttWorldgenPlanet, primaryStar),
                    8 or 9 => _oceanicWorld.Generate(rttWorldgenPlanet, primaryStar),
                    10 => _tectonicWorld.Generate(rttWorldgenPlanet, primaryStar),
                    (>= 11) => _telluricWorld.Generate(rttWorldgenPlanet)
                },
                PlanetType.Helian => _rollingService.D6(1) switch {
                    (<= 4) => _helianWorld.Generate(rttWorldgenPlanet),
                    (>= 5) => _panthalassicWorld.Generate(rttWorldgenPlanet, primaryStar)
                },
                PlanetType.Jovian => _jovianWorld.Generate(rttWorldgenPlanet, primaryStar)
            },
            PlanetOrbit.Outer => rttWorldgenPlanet.PlanetType switch {
                PlanetType.AsteroidBelt => new RttWorldgenPlanet(new RollingService(), rttWorldgenPlanet) {WorldType = WorldType.AsteroidBelt},
                PlanetType.DwarfPlanet => (_rollingService.D6(1)
                                           + rttWorldgenPlanet.ParentType switch {
                                               PlanetType.AsteroidBelt => -1,
                                               PlanetType.Helian => 1,
                                               PlanetType.Jovian => 2,
                                               _ => 0
                                           }) switch {
                    (<= 3) => _rockballWorld.Generate(rttWorldgenPlanet, primaryStar),
                    (>= 4 and <= 6) => _snowballWorld.Generate(rttWorldgenPlanet, primaryStar),
                    7 => _meltballWorld.Generate(rttWorldgenPlanet),
                    (>= 8) => _rollingService.D6(1) switch {
                        (<= 3) => _hebeanWorld.Generate(rttWorldgenPlanet),
                        4 or 5 => _areanWorld.Generate(rttWorldgenPlanet, primaryStar),
                        (>= 6) => _prometheanWorld.Generate(rttWorldgenPlanet, primaryStar)
                    }
                },
                PlanetType.Terrestrial => (_rollingService.D6(1) + (rttWorldgenPlanet.ParentId != Guid.Empty ? 2 : 0)) switch {
                    (<= 4) => _aridWorld.Generate(rttWorldgenPlanet, primaryStar),
                    5 or 6 => _tectonicWorld.Generate(rttWorldgenPlanet, primaryStar),
                    (>= 7) => _oceanicWorld.Generate(rttWorldgenPlanet, primaryStar)
                },
                PlanetType.Helian => _helianWorld.Generate(rttWorldgenPlanet),
                PlanetType.Jovian => _jovianWorld.Generate(rttWorldgenPlanet, primaryStar)
            }
        };
        return rttWorldgenPlanet;
    }
}