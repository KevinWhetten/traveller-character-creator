using SectorCreator.Global;
using SectorCreator.Global.Enums;
using SectorCreator.Models.Basic;
using SectorCreator.Models.StarFrontiers;

namespace SectorCreator.Models.Factories;

public interface IStarFrontiersPlanetFactory
{
    StarFrontiersPlanet Generate(bool habitable, int habitableBase, int orbitNum);
    Planet Generate(SectorType sectorType);
}

public class StarFrontiersPlanetFactory : PlanetFactory, IStarFrontiersPlanetFactory
{
    private readonly IRollingService _rollingService;

    public StarFrontiersPlanetFactory(IRollingService rollingService)
        : base(rollingService)
    {
        _rollingService = rollingService;
    }

    private StarFrontiersPlanet StarFrontiersPlanet => (StarFrontiersPlanet) Planet;

    public StarFrontiersPlanet Generate(bool habitable, int habitableBase, int orbitNum)
    {
        GenerateType();
        GenerateSize();
        GenerateAtmosphere(habitable);
        GenerateHydrographics(habitable, habitableBase, orbitNum);
        GenerateMoons(habitable, habitableBase, orbitNum);
        GenerateSettlement();

        return StarFrontiersPlanet;
    }

    private Planet GenerateSatellite(bool habitable, int habitableBase, int orbitNum)
    {
        GenerateType();
        GenerateSize();
        GenerateAtmosphere(habitable);
        GenerateHydrographics(habitable, habitableBase, orbitNum);
        GenerateSettlement();

        return StarFrontiersPlanet;
    }

    private Planet GenerateSatellite(int size, int atmosphere, int hydrographics)
    {
        StarFrontiersPlanet.Size = size;
        StarFrontiersPlanet.Atmosphere = atmosphere;
        StarFrontiersPlanet.Hydrographics = hydrographics;

        GenerateSettlement();

        return StarFrontiersPlanet;
    }

    private void GenerateSettlement()
    {
        GeneratePopulation(SectorType.StarFrontiers);
        GenerateGovernment();
        GenerateLawLevel();
        GenerateStarport(SectorType.StarFrontiers);
        GenerateTechnologyLevel();
        GetBases();
        GetTravelCode();
    }

    private void GenerateType()
    {
        var roll = _rollingService.D10(1);

        StarFrontiersPlanet.PlanetType = roll switch {
            <= 2 => PlanetType.AsteroidBelt,
            <= 6 => PlanetType.Terrestrial,
            _ => PlanetType.Jovian
        };
    }

    private void GenerateSize()
    {
        StarFrontiersPlanet.Size = StarFrontiersPlanet.PlanetType switch {
            PlanetType.AsteroidBelt => 0,
            PlanetType.Terrestrial => _rollingService.D10(_rollingService.D10(1) <= 5 ? 1 : 2),
            PlanetType.Jovian => _rollingService.D10(1) <= 5
                ? 10 + _rollingService.D10(10)
                : 100 + _rollingService.D10(10),
            _ => throw new ArgumentOutOfRangeException()
        };
    }

    protected void GenerateAtmosphere(bool habitable)
    {
        if (StarFrontiersPlanet.Size is 0 or 1) {
            StarFrontiersPlanet.Atmosphere = 0;
            return;
        }

        StarFrontiersPlanet.Atmosphere = StarFrontiersPlanet.Size + _rollingService.D10(1) - _rollingService.D10(1);

        if (!habitable) {
            if (_rollingService.D10(1) <= 6) {
                StarFrontiersPlanet.Atmosphere -= 5;
            } else {
                StarFrontiersPlanet.Atmosphere += 10;
            }
        }
    }

    protected void GenerateHydrographics(bool habitable, int habitableBase, int planetNum)
    {
        StarFrontiersPlanet.Hydrographics = StarFrontiersPlanet.Size + _rollingService.D10(1) - _rollingService.D10(1);

        if (!habitable) {
            if (planetNum < habitableBase) {
                StarFrontiersPlanet.Hydrographics = 0;
            } else if (planetNum > habitableBase) {
                if (_rollingService.D10(1) <= 9) {
                    StarFrontiersPlanet.Hydrographics -= 5;
                }
            }
        }

        if (StarFrontiersPlanet.Atmosphere is <= 1 or >= 10) {
            StarFrontiersPlanet.Hydrographics -= 5;
        }

        if (StarFrontiersPlanet.PlanetType == PlanetType.Jovian) {
            StarFrontiersPlanet.Hydrographics = 10;
        }
    }

    private void GenerateMoons(bool habitable, int habitableBase, int planetNum)
    {
        var roll = _rollingService.D10(1);

        var numMoons = roll switch {
            <= 3 => (StarFrontiersPlanet.Size / 10) - _rollingService.D(5, 1),
            <= 7 => StarFrontiersPlanet.Size / 10,
            _ => (StarFrontiersPlanet.Size / 10) + _rollingService.D(5, 1)
        };

        for (var i = 0; i < numMoons; i++) {
            GenerateMoon(habitable, habitableBase, planetNum);
        }
    }

    private void GenerateMoon(bool habitable, int habitableBase, int orbitNum)
    {
        StarFrontiersPlanet.Satellites.Add(_rollingService.D10(1) <= 9
            ? GenerateSatellite(0, 0, 0)
            : GenerateSatellite(habitable, habitableBase, orbitNum));
    }
}