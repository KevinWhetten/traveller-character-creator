using SectorCreator.Global;
using SectorCreator.Global.Enums;

namespace SectorCreator.Models.RTTWorldgen.Planets;

public static class TectonicPlanet
{
    public static RttWorldgenPlanet Generate(RttWorldgenPlanet planet, RttWorldgenStar primaryStar)
    {
        planet.Size = Roll.D6(1) + 4;
        planet.Chemistry = GetChemistry(primaryStar, planet);
        planet.Biosphere = GetBiosphere(primaryStar, planet);
        planet.Hydrographics = GetAtmosphere(planet);
        planet.Hydrographics = Roll.D6(2) - 2;
        planet = PlanetValidation.ValidatePlanet(planet);
        return planet;
    }

    private static PlanetChemistry GetChemistry(RttWorldgenStar primaryStar, RttWorldgenPlanet planet)
    {
        var roll = Roll.D6(1);

        if (primaryStar.SpectralType == SpectralType.K && primaryStar.Luminosity == Luminosity.V) {
            roll += 2;
        } else if (primaryStar.SpectralType == SpectralType.M && primaryStar.Luminosity == Luminosity.V) {
            roll += 4;
        } else if (primaryStar.SpectralType == SpectralType.L) {
            roll += 5;
        }

        if (planet.PlanetOrbit == PlanetOrbit.Outer) {
            roll += 2;
        }

        return roll switch {
            (<= 6) => Roll.D6(2) switch {
                (<= 8) => PlanetChemistry.Water,
                (<= 11) => PlanetChemistry.Sulfur,
                (<= 12) => PlanetChemistry.Chlorine,
                _ => PlanetChemistry.None
            },
            (<= 8) => PlanetChemistry.Ammonia,
            (<= 10) => PlanetChemistry.Methane,
            _ => PlanetChemistry.None
        };
    }

    private static int GetBiosphere(RttWorldgenStar primaryStar, RttWorldgenPlanet planet)
    {
        if (primaryStar.Age >= 4 + (int) planet.Chemistry) {
            var mod = primaryStar.SpectralType == SpectralType.D ? -3 : 0;
            return Roll.D6(2) + mod;
        }

        if (primaryStar.Age >= Roll.D3(1) + (int) planet.Chemistry) {
            return Roll.D3(1);
        }

        return 0;
    }

    private static int GetAtmosphere(RttWorldgenPlanet planet)
    {
        if (planet.Biosphere >= 3 && planet.Chemistry == PlanetChemistry.Water) {
            var atmosphereRoll = Roll.D6(2) + planet.Size - 7;
            atmosphereRoll = Math.Min(atmosphereRoll, 9);
            atmosphereRoll = Math.Max(atmosphereRoll, 2);
            return atmosphereRoll;
        }

        if (planet.Biosphere >= 3
            && planet.Chemistry is PlanetChemistry.Sulfur or PlanetChemistry.Chlorine) {
            return 11;
        }

        return 10;
    }
}