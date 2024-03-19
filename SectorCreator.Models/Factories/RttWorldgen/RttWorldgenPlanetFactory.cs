using SectorCreator.Global;
using SectorCreator.Global.Enums;
using SectorCreator.Models.Basic;
using SectorCreator.Models.Factories.Basic;
using SectorCreator.Models.RTTWorldgen;
using SectorCreator.Models.RTTWorldgen.Worlds;
using SectorCreator.Models.Services;
using SectorCreator.Models.Services.TradeCodeService;

namespace SectorCreator.Models.Factories.RttWorldgen;

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

    

    private readonly Race[] races = {
        new() {
            Name = "Human",
            Homeworld = new Planet {
                Name = "Earth",
                Size = 8,
                Atmosphere = 6,
                Hydrographics = 7,
                Temperature = Temperature.Temperate,
                Population = 10,
                Government = 4,
                LawLevel = 9,
                TechLevel = 11,
                Starport = 'A'
            },
            HomeworldCoordinates = new Coordinates {
                X = 16,
                Y = 14
            },
            expansionRate = 10
        },
        new() {
            Name = "Aslan",
            Homeworld = new Planet {
                Name = "Kusyu",
                Size = 8,
                Atmosphere =8,
                Hydrographics = 7,
                Temperature = Temperature.Temperate,
                Population = 9,
                Government = 7,
                LawLevel = 6,
                TechLevel = 10,
                Starport = 'A'
            },
            HomeworldCoordinates = new Coordinates {
                X = 14,
                Y = 8
            },
            expansionRate = 9
        },
        new() {
            Name = "Mannu",
            Homeworld = new Planet {
                Name = "???",
                Size = 6,
                Atmosphere = 8,
                Hydrographics = 8,
                Temperature = Temperature.Cool,
                Population = 6,
                Government = 17,
                LawLevel = 15,
                TechLevel = 10,
                Starport = 'A'
            },
            HomeworldCoordinates = new Coordinates {
                X = 7,
                Y = 31
            },
            expansionRate = 7
        },
        new() {
            Name = "Largosians",
            Homeworld = new Planet {
                Name = "???",
                Size = 5,
                Atmosphere = 7,
                Hydrographics = 2,
                Temperature = Temperature.Hot,
                Population = 10,
                Government = 1,
                LawLevel = 4,
                TechLevel = 10,
                Starport = 'A'
            },
            HomeworldCoordinates = new Coordinates {
                X = 18,
                Y = 28
            },
            expansionRate = 12
        },
        new() {
            Name = "Tortosians",
            Homeworld = new Planet {
                Name = "???",
                Size = 4,
                Atmosphere = 9,
                Hydrographics = 6,
                Temperature = Temperature.Warm,
                Population = 9,
                Government = 8,
                LawLevel = 7,
                TechLevel = 9,
                Starport = 'A'
            },
            HomeworldCoordinates = new Coordinates {
                X = 19,
                Y = 33
            },
            expansionRate = 7
        },
        new() {
            Name = "Ithromir",
            Homeworld = new Planet {
                Name = "???",
                Size = 10,
                Atmosphere = 13,
                Hydrographics = 10,
                Temperature = Temperature.Warm,
                Population = 13,
                Government = 12,
                LawLevel = 12,
                TechLevel = 13,
                Starport = 'S'
            },
            HomeworldCoordinates = new Coordinates {
                X = 29,
                Y = 29
            },
            expansionRate = 10
        },
        new() {
            Name = "Chrotos",
            Homeworld = new Planet {
                Name = "???",
                Size = 3,
                Atmosphere = 1,
                Hydrographics = 4,
                Temperature = Temperature.Frozen,
                Population = 5,
                Government = 5,
                LawLevel = 10,
                TechLevel = 12,
                Starport = 'A'
            },
            HomeworldCoordinates = new Coordinates {
                X = 22,
                Y = 15
            },
            expansionRate = 3
        },
        new() {
            Name = "Ka'Sara",
            Homeworld = new Planet {
                Name = "???",
                Size = 9,
                Atmosphere = 12,
                Hydrographics = 0,
                Temperature = Temperature.Boiling,
                Population = 15,
                Government = 0,
                LawLevel = 21,
                TechLevel = 12,
                Starport = 'A'
            },
            HomeworldCoordinates = new Coordinates {
                X = 6,
                Y = 12
            },
            expansionRate = 9
        },
        new() {
            Name = "Vargr",
            Homeworld = new Planet {
                Name = "Lair",
                Size = 8,
                Atmosphere = 8,
                Hydrographics = 5,
                Temperature = Temperature.Cool,
                Population = 9,
                Government = 11,
                LawLevel = 9,
                TechLevel = 11,
                Starport = 'A'
            },
            HomeworldCoordinates = new Coordinates {
                X = 22,
                Y = 4
            },
            expansionRate = 7
        },
    };
    
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
    public RttWorldgenPlanet RttWorldgenSatellite { get; set; }
    public RttWorldgenStar PrimaryStar { get; set; }

    public RttWorldgenPlanet GenerateRttWorldgenPlanet(RttWorldgenStar primaryStar, PlanetOrbit planetOrbit, int orbitPosition, Coordinates coordinates)
    {
        RttWorldgenPlanet = new RttWorldgenPlanet();
        PrimaryStar = primaryStar;

        RttWorldgenPlanet.PlanetOrbit = planetOrbit;
        RttWorldgenPlanet.OrbitPosition = orbitPosition;

        RttWorldgenPlanet = GeneratePlanetType(RttWorldgenPlanet, primaryStar);
        RttWorldgenPlanet = GenerateSatellites(RttWorldgenPlanet);
        RttWorldgenPlanet = GenerateWorldType(RttWorldgenPlanet, primaryStar);

        RttWorldgenPlanet = GenerateTemperature(RttWorldgenPlanet);
        
        var roll = _rollingService.D6(2) - 7;
        
        foreach (var race in races) {
            var atmosphere = AtmosphereService.GetAtmosphere(RttWorldgenPlanet.Atmosphere);
            var homeworldAtmosphere = AtmosphereService.GetAtmosphere(race.Homeworld.Atmosphere);
            var atmosphereDifference = Math.Abs(atmosphere.Density - homeworldAtmosphere.Density) * 2 -
                                       (atmosphere.Tainted == homeworldAtmosphere.Tainted ? 1 : 0);
            var distanceBetween = DistanceService.DistanceBetween(coordinates, race.HomeworldCoordinates);
            var temperatureDifference = Math.Abs(RttWorldgenPlanet.Temperature - race.Homeworld.Temperature);
        
            if (race.expansionRate + roll > atmosphereDifference + temperatureDifference + distanceBetween) {
                RttWorldgenPlanet.Populate(_rollingService.D6(2), race, distanceBetween);
                RttWorldgenPlanet.SetGovernment(_rollingService.D6(2) - 7, race);
                RttWorldgenPlanet.SetLawLevel(_rollingService.D6(2) - 7, race);
                RttWorldgenPlanet.GenerateStarport(_rollingService.D6(2) - 7);
                RttWorldgenPlanet.GenerateTechLevel(_rollingService.D6(1));
                RttWorldgenPlanet.GenerateBases(_rollingService);
                RttWorldgenPlanet.SetTravelZone();
                RttWorldgenPlanet.GenerateName();
            }
        }

        var tradeCodeService = new TradeCodeService(_rollingService);

        tradeCodeService.AddTradeCodes(RttWorldgenPlanet);
        RttWorldgenPlanet.SetImportance();
        //RttWorldgenPlanet.SetEconomicExtension(new RollingService(), gasGiantCount, beltCount);
        RttWorldgenPlanet.SetCulturalExtension(new RollingService());
        RttWorldgenPlanet.SetNobility();
        RttWorldgenPlanet.SetTravelZone();
        //RttWorldgenPlanet.SetPBG(new RollingService().D10(1), beltCount, gasGiantCount);

        return RttWorldgenPlanet;
    }

    private RttWorldgenPlanet GenerateTemperature(RttWorldgenPlanet planet)
    {
        var tempValue = GetTempValue();

        planet.Temperature = tempValue switch {
            <= 2 => Temperature.Frozen,
            <= 4 => Temperature.Cold,
            <= 5 => Temperature.Cool,
            <= 8 => Temperature.Temperate,
            <= 9 => Temperature.Warm,
            <= 11 => Temperature.Hot,
            _ => Temperature.Boiling
        };

        return planet;
    }

    private int GetTempValue()
    {
        var value = _rollingService.D6(2);

        value += RttWorldgenPlanet.Atmosphere switch {
            0 or 1 => 0,
            2 or 3 => -2,
            4 or 5 or 14 => -1,
            6 or 7 => 0,
            8 or 9 => +1,
            10 or 13 or 15 => 2,
            11 or 12 => 6,
            _ => 0
        };

        value += RttWorldgenPlanet.PlanetOrbit switch {
            PlanetOrbit.Epistellar => _rollingService.D6(1),
            PlanetOrbit.Inner => 0,
            PlanetOrbit.Outer => -_rollingService.D6(1),
            _ => throw new ArgumentOutOfRangeException()
        };
        
        return value;
    }


    private RttWorldgenPlanet GenerateNewSatellite(PlanetType planetType)
    {
        RttWorldgenSatellite = new RttWorldgenPlanet {
            PlanetType = planetType,
            ParentId = RttWorldgenPlanet.Id,
            ParentType = RttWorldgenPlanet.PlanetType
        };

        GenerateWorldType(RttWorldgenSatellite, PrimaryStar);

        var tempValue = GetTempValue();
        
        RttWorldgenSatellite.Temperature = tempValue switch {
            <= 2 => Temperature.Frozen,
            <= 4 => Temperature.Cold,
            <= 5 => Temperature.Cool,
            <= 8 => Temperature.Temperate,
            <= 9 => Temperature.Warm,
            <= 11 => Temperature.Hot,
            _ => Temperature.Boiling
        };

        return RttWorldgenSatellite;
    }


    public RttWorldgenPlanet GeneratePlanetType(RttWorldgenPlanet planet, RttWorldgenStar primaryStar)
    {
        var roll = _rollingService.D6(1);

        if (primaryStar.SpectralType == SpectralType.L) {
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
                    rttWorldgenPlanet.Satellites.Add(new RttWorldgenPlanet(GenerateNewSatellite(PlanetType.DwarfPlanet)));
                }

                break;
            case PlanetType.DwarfPlanet:
                if (_rollingService.D6(1) >= 6) {
                    rttWorldgenPlanet.Satellites.Add(new RttWorldgenPlanet(GenerateNewSatellite(PlanetType.DwarfPlanet)));
                }

                break;
            case PlanetType.Terrestrial:
                if (_rollingService.D6(1) >= 5) {
                    rttWorldgenPlanet.Satellites.Add(new RttWorldgenPlanet(GenerateNewSatellite(PlanetType.DwarfPlanet)));
                }

                break;
            case PlanetType.Helian:
                numSatellites = _rollingService.D6(1) - 3;

                for (var i = 0; i < numSatellites; i++) {
                    rttWorldgenPlanet.Satellites.Add(new RttWorldgenPlanet(_rollingService.D6(1) == 6
                        ? GenerateNewSatellite(PlanetType.Terrestrial)
                        : GenerateNewSatellite(PlanetType.DwarfPlanet)));
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
                    rttWorldgenPlanet.Satellites.Add(new RttWorldgenPlanet(GenerateNewSatellite(first ? type : PlanetType.DwarfPlanet)));
                    first = false;
                }

                break;
        }

        return rttWorldgenPlanet;
    }

    public RttWorldgenPlanet GenerateWorldType(RttWorldgenPlanet rttWorldgenPlanet, RttWorldgenStar primaryStar)
    {
        if ((primaryStar.SpectralType == SpectralType.D || primaryStar.Luminosity == Luminosity.III)
            && rttWorldgenPlanet.OrbitPosition <= primaryStar.ExpansionSize) {
            rttWorldgenPlanet = rttWorldgenPlanet.PlanetType switch {
                PlanetType.AsteroidBelt => new RttWorldgenPlanet(rttWorldgenPlanet) {WorldType = WorldType.AsteroidBelt},
                PlanetType.DwarfPlanet => _stygianWorld.Generate(rttWorldgenPlanet),
                PlanetType.Terrestrial => _acheronianWorld.Generate(rttWorldgenPlanet),
                PlanetType.Helian => _asphodelianWorld.Generate(rttWorldgenPlanet),
                PlanetType.Jovian => _chthonianWorld.Generate(rttWorldgenPlanet)
            };
            return rttWorldgenPlanet;
        }

        rttWorldgenPlanet = rttWorldgenPlanet.PlanetOrbit switch {
            PlanetOrbit.Epistellar => rttWorldgenPlanet.PlanetType switch {
                PlanetType.AsteroidBelt => new RttWorldgenPlanet(rttWorldgenPlanet) {WorldType = WorldType.AsteroidBelt},
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
                PlanetType.AsteroidBelt => new RttWorldgenPlanet(rttWorldgenPlanet) {WorldType = WorldType.AsteroidBelt},
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
                PlanetType.AsteroidBelt => new RttWorldgenPlanet(rttWorldgenPlanet) {WorldType = WorldType.AsteroidBelt},
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



public class Race
{
    public string Name;
    public Planet Homeworld;
    public Coordinates HomeworldCoordinates;
    public int expansionRate;
}