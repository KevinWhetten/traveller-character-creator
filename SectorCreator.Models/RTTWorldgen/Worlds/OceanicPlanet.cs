using SectorCreator.Global;
using SectorCreator.Global.Enums;

namespace SectorCreator.Models.RTTWorldgen.Worlds;

public interface IOceanicPlanet
{
    RttWorldgenPlanet Generate(RttWorldgenPlanet planet, RttWorldgenStar primaryStar);
}

public class OceanicPlanet : IOceanicPlanet
{
    private readonly IRollingService _rollingService;
    private readonly IPlanetValidation _planetValidation;

    public OceanicPlanet(IRollingService rollingService, IPlanetValidation planetValidation)
    {
        _rollingService = rollingService;
        _planetValidation = planetValidation;
    }

    public RttWorldgenPlanet Generate(RttWorldgenPlanet planet, RttWorldgenStar primaryStar)
    {
        planet.Size = _rollingService.D6(1) + 4;
        planet.Chemistry = GetChemistry(primaryStar, planet);
        planet.Biosphere = GetBiosphere(primaryStar, planet);
        planet.Atmosphere = GetAtmosphere(primaryStar, planet);
        planet.Hydrographics = 11;
        planet = _planetValidation.ValidatePlanet(planet);
        return planet;
    }

    private PlanetChemistry GetChemistry(RttWorldgenStar primaryStar, RttWorldgenPlanet planet)
    {
        var chemistryRoll = _rollingService.D6(1);
        if (primaryStar.SpectralType == SpectralType.K && primaryStar.Luminosity == Luminosity.V) {
            chemistryRoll += 2;
        } else if (primaryStar.SpectralType == SpectralType.M && primaryStar.Luminosity == Luminosity.V) {
            chemistryRoll += 4;
        } else if (primaryStar.SpectralType == SpectralType.L) {
            chemistryRoll += 5;
        } else if (planet.PlanetOrbit == PlanetOrbit.Outer) {
            chemistryRoll += 2;
        }

        return chemistryRoll switch {
            (<= 6) => PlanetChemistry.Water,
            (<= 8) => PlanetChemistry.Ammonia,
            _ => PlanetChemistry.Methane
        };
    }

    private int GetBiosphere(RttWorldgenStar primaryStar, RttWorldgenPlanet planet)
    {
        int biosphere;

        if (primaryStar.Age >= 4 + (int) planet.Chemistry) {
            biosphere = _rollingService.D6(2);
            if (primaryStar.SpectralType == SpectralType.D) {
                biosphere -= 3;
            }
        } else if (primaryStar.Age >= _rollingService.D3(1) + (int) planet.Chemistry) {
            biosphere = _rollingService.D3(1);
        } else {
            biosphere = 0;
        }

        return biosphere;
    }

    private int GetAtmosphere(RttWorldgenStar primaryStar, RttWorldgenPlanet planet)
    {
        int atmosphere;
        if (planet.Chemistry == PlanetChemistry.Water) {
            atmosphere = _rollingService.D6(2) + planet.Size - 6;
            if (primaryStar.SpectralType == SpectralType.K && primaryStar.Luminosity == Luminosity.V) {
                atmosphere--;
            } else if (primaryStar.SpectralType == SpectralType.M && primaryStar.Luminosity == Luminosity.V) {
                atmosphere -= 2;
            } else if (primaryStar.SpectralType == SpectralType.L) {
                atmosphere -= 3;
            } else if (primaryStar.Luminosity == Luminosity.IV) {
                atmosphere--;
            }

            atmosphere = Math.Min(atmosphere, 12);
            atmosphere = Math.Max(atmosphere, 1);
        } else {
            atmosphere = _rollingService.D6(1) switch {
                1 => 1,
                (<= 4) => 10,
                _ => 12
            };
        }

        return atmosphere;
    }
}