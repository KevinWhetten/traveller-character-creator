using SectorCreator.Global;
using SectorCreator.Global.Enums;

namespace SectorCreator.Models.RTTWorldgen.Worlds;

public interface IPanthalassicPlanet
{
    RttWorldgenPlanet Generate(RttWorldgenPlanet planet, RttWorldgenStar primaryStar);
}

public class PanthalassicPlanet : IPanthalassicPlanet
{
    private readonly IRollingService _rollingService;
    private readonly IPlanetValidation _planetValidation;

    public PanthalassicPlanet(IRollingService rollingService, IPlanetValidation planetValidation)
    {
        _rollingService = rollingService;
        _planetValidation = planetValidation;
    }

    public RttWorldgenPlanet Generate(RttWorldgenPlanet planet, RttWorldgenStar primaryStar)
    {
        planet.Size = _rollingService.D6(1) + 9;
        planet.Size = _rollingService.D6(1) + 9;
        planet.Atmosphere = Math.Min(_rollingService.D6(1) + 8, 13);
        planet.Hydrographics = 11;
        planet.Chemistry = GetChemistry(primaryStar);
        planet.Biosphere = GetBiosphere(primaryStar, planet);

        if (planet.Chemistry == PlanetChemistry.Ammonia) {
            planet.Chemistry = PlanetChemistry.Methane;
        }
        planet = _planetValidation.ValidatePlanet(planet);
        return planet;
    }

    private PlanetChemistry GetChemistry(RttWorldgenStar primaryStar)
    {
        var chemistryRoll = _rollingService.D6(1);
        if (primaryStar.SpectralType == SpectralType.K && primaryStar.Luminosity == Luminosity.V) {
            chemistryRoll += 2;
        } else if (primaryStar.SpectralType == SpectralType.M && primaryStar.Luminosity == Luminosity.V) {
            chemistryRoll += 4;
        } else if (primaryStar.SpectralType == SpectralType.L) {
            chemistryRoll += 5;
        }

        return chemistryRoll switch {
            <= 6 => _rollingService.D6(2) switch {
                <= 8 => PlanetChemistry.Water,
                <= 11 => PlanetChemistry.Sulfur,
                <= 12 => PlanetChemistry.Chlorine,
                _ => PlanetChemistry.None
            },
            <= 8 => PlanetChemistry.Ammonia,
            <= 10 => PlanetChemistry.Methane,
            _ => PlanetChemistry.None
        };
    }

    private int GetBiosphere(RttWorldgenStar primaryStar, RttWorldgenPlanet planet)
    {
        if (primaryStar.Age >= 4 + (int) planet.Chemistry)
            return _rollingService.D6(2);
        if (primaryStar.Age >= _rollingService.D3(1) + (int) planet.Chemistry)
            return _rollingService.D3(1);
        
        return 0;
    }
}