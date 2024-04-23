using SectorCreator.Global;
using SectorCreator.Global.Enums;
using SectorCreator.Models.Services;

namespace SectorCreator.Models.RTTWorldgen.Worlds;

public interface IOceanicWorld
{
    RttWorldgenPlanet Generate(RttWorldgenPlanet planet, RttWorldgenStar primaryStar);
}

public class OceanicWorld : IOceanicWorld
{
    private readonly IRollingService _rollingService;
    
    public OceanicWorld(IRollingService rollingService)
    {
        _rollingService = rollingService;
    }

    public RttWorldgenPlanet Generate(RttWorldgenPlanet planet, RttWorldgenStar primaryStar)
    {
        planet.WorldType = WorldType.Oceanic;
        planet.Size = _rollingService.D6(1) + 4;
        planet.Chemistry = GetChemistry(primaryStar, planet);
        planet.Biosphere = GetBiosphere(primaryStar, planet);
        planet.Atmosphere = GetAtmosphere(primaryStar, planet);
        planet.Hydrographics = 11;
        planet = WorldValidation.ValidatePlanet(planet);
        return planet;
    }

    private PlanetChemistry GetChemistry(RttWorldgenStar primaryStar, RttWorldgenPlanet planet)
    {
        var chemistryRoll = _rollingService.D6(1);
        if (primaryStar.SpectralType == SpectralType.K && primaryStar.LuminosityClass == LuminosityClass.V) {
            chemistryRoll += 2;
        } else if (primaryStar.SpectralType == SpectralType.M && primaryStar.LuminosityClass == LuminosityClass.V) {
            chemistryRoll += 4;
        } else if (primaryStar.SpectralType == SpectralType.BD) {
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

        if (primaryStar.Age >= 4 + ChemistryService.GetAgeMod(planet.Chemistry)) {
            biosphere = _rollingService.D6(2);
            if (primaryStar.SpectralType == SpectralType.D) {
                biosphere -= 3;
            }
        } else if (primaryStar.Age >= _rollingService.D3(1) + ChemistryService.GetAgeMod(planet.Chemistry)) {
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
            if (primaryStar.SpectralType == SpectralType.K && primaryStar.LuminosityClass == LuminosityClass.V) {
                atmosphere--;
            } else if (primaryStar.SpectralType == SpectralType.M && primaryStar.LuminosityClass == LuminosityClass.V) {
                atmosphere -= 2;
            } else if (primaryStar.SpectralType == SpectralType.BD) {
                atmosphere -= 3;
            } else if (primaryStar.LuminosityClass == LuminosityClass.IV) {
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