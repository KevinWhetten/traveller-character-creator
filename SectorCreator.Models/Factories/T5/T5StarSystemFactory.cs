using SectorCreator.Global;
using SectorCreator.Global.Enums;
using SectorCreator.Models.Basic;
using SectorCreator.Models.T5;

namespace SectorCreator.Models.Factories.t5;

public interface IT5StarSystemFactory
{
    StarSystem Generate();
}

public class It5StarSystemFactory : IT5StarSystemFactory
{
    private readonly IRollingService _rollingService;

    public readonly T5Star Star = new();
    public readonly T5Planet Planet = new();
    public readonly StarSystem StarSystem = new();

    public It5StarSystemFactory(IRollingService rollingService)
    {
        _rollingService = rollingService;
    }

    public StarSystem Generate()
    {
        GeneratePlanetStarport();
        GenerateMainStar();
        GenerateMainworldOrbit();
        GenerateMainworldIsSatellite();
        GenerateStarSystemPBG();
        GeneratePlanetUWP();

        return StarSystem;
    }

    public void GeneratePlanetStarport()
    {
        Planet.Starport = _rollingService.D6(2) switch {
            (<= 4) => 'A',
            5 or 6 => 'B',
            7 or 8 => 'C',
            9 => 'D',
            10 or 11 => 'E',
            (>= 12) => 'X'
        };
    }

    public void GenerateMainStar()
    {
        GenerateSpectralType();
        GenerateLuminosity();
        GenerateSpectralSubclass();
    }

    public void GenerateSpectralType()
    {
        Star.SpectralType = _rollingService.Flux() switch {
            -6 => SpectralType.O,
            -5 => SpectralType.B,
            -4 or -3 => SpectralType.A,
            -2 or -1 => SpectralType.F,
            0 => SpectralType.G,
            1 or 2 => SpectralType.K,
            _ => SpectralType.M
        };
    }

    public void GenerateLuminosity()
    {
        Star.Luminosity = Star.SpectralType switch {
            SpectralType.O => _rollingService.Flux() switch {
                (<= -6 or -5) => Luminosity.Ia,
                -4 => Luminosity.Ib,
                -3 => Luminosity.II,
                (>= -2 and <= 0) => Luminosity.III,
                (>= 1 and <= 3) => Luminosity.V,
                4 => Luminosity.IV,
                (>= 5) => Luminosity.D
            },
            SpectralType.B => _rollingService.Flux() switch {
                (<= -6 or -5) => Luminosity.Ia,
                -4 => Luminosity.Ib,
                -3 => Luminosity.II,
                (>= -2 and <= 1) => Luminosity.III,
                (2 or 3) => Luminosity.V,
                4 => Luminosity.IV,
                (>= 5) => Luminosity.D
            },
            SpectralType.A => _rollingService.Flux() switch {
                (<= -6 or -5) => Luminosity.Ia,
                -4 => Luminosity.Ib,
                -3 => Luminosity.II,
                -2 => Luminosity.III,
                -1 => Luminosity.IV,
                (>= 0 and <= 4) => Luminosity.V,
                (>= 5) => Luminosity.D
            },
            SpectralType.F or SpectralType.G or SpectralType.K => _rollingService.Flux() switch {
                (<= -6 or -5) => Luminosity.II,
                -4 => Luminosity.III,
                -3 => Luminosity.IV,
                (>= -2 and <= 3) => Luminosity.V,
                4 => Luminosity.VI,
                (>= 5) => Luminosity.D
            },
            SpectralType.M => _rollingService.Flux() switch {
                (<= -3) => Luminosity.II,
                -2 => Luminosity.III,
                (>= -1 and <= 3) => Luminosity.V,
                4 => Luminosity.VI,
                (>= 5) => Luminosity.D
            }
        };
    }

    private void GenerateSpectralSubclass()
    {
        Star.SpectralSubclass = Star.Luminosity != Luminosity.D ? _rollingService.D10(1) : 0;
    }

    public void GenerateMainworldOrbit()
    {
        var mod = Star.SpectralType == SpectralType.M ? 2 : 0;
        mod += Star.SpectralType is SpectralType.O or SpectralType.B ? -2 : 0;

        Planet.Temperature = (_rollingService.Flux() + mod) switch {
            <= -6 => Temperature.Boiling,
            (>= -5 and <= -3) => Temperature.Hot,
            (>= -2 and <= 2) => Temperature.Temperate,
            (>= 3 and <= 5) => Temperature.Cold,
            >= -6 => Temperature.Frozen
        };

        Planet.HZVar = Planet.Temperature switch {
            Temperature.Boiling => -2,
            Temperature.Hot => -1,
            Temperature.Temperate => 0,
            Temperature.Cold => 1,
            Temperature.Frozen => 2
        };
    }

    public void GenerateMainworldIsSatellite()
    {
        Planet.SatelliteOrbitType = _rollingService.Flux() switch {
            -5 or -4 => CompanionOrbit.Distant,
            -3 => CompanionOrbit.Close,
            _ => CompanionOrbit.None
        };

        if (Planet.SatelliteOrbitType == CompanionOrbit.None) return;

        GenerateParentType();
        GenerateOrbitLetter();
    }

    public void GenerateParentType()
    {
        Planet.ParentType = _rollingService.Flux() switch {
            (<= 0) => ParentType.GasGiant,
            (>= 1) => ParentType.Planet
        };
    }

    public void GenerateOrbitLetter()
    {
        Planet.SatelliteOrbit = Planet.SatelliteOrbitType switch {
            CompanionOrbit.Close => _rollingService.Flux() switch {
                (<= -6) => 'A',
                -5 => 'B',
                -4 => 'C',
                -3 => 'D',
                -2 => 'E',
                -1 => 'F',
                0 => 'G',
                1 => 'H',
                2 => 'I',
                3 => 'J',
                4 => 'K',
                5 => 'L',
                (>= 6) => 'M'
            },
            CompanionOrbit.Distant => _rollingService.Flux() switch {
                (<= -6) => 'N',
                -5 => 'O',
                -4 => 'P',
                -3 => 'Q',
                -2 => 'R',
                -1 => 'S',
                0 => 'T',
                1 => 'U',
                2 => 'V',
                3 => 'W',
                4 => 'X',
                5 => 'Y',
                (>= 6) => 'Z'
            }
        };
    }

    public void GenerateStarSystemPBG()
    {
        var planets = _rollingService.D10(1) - 1;
        var belts = _rollingService.D6(1) - 3;
        var gasGiants = (_rollingService.D6(2) / 2) - 2;

        planets = Math.Max(planets, 0);
        belts = Math.Max(belts, 0);
        gasGiants = Math.Max(gasGiants, 0);

        StarSystem.PBG = planets * 100 + belts * 10 + gasGiants;
    }

    private void GeneratePlanetUWP()
    {
        /*
         * SIZE
         * ATMOSPHERE
         * HYDROGRAPHICS
         * POPULATION
         * GOVERNMENT
         * LAW-LEVEL
         * TECH-LEVEL
         */
    }
}