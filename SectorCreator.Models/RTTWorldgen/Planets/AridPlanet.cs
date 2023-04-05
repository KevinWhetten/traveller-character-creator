using SectorCreator.Global;
using SectorCreator.Global.Enums;

namespace SectorCreator.Models.RTTWorldgen.Planets;

public static class AridPlanet
{
    public static RttWorldgenPlanet Generate(RttWorldgenPlanet planet, RttWorldgenStar primaryStar)
    {
        planet.Size = Roll.D6(1) + 4;
        planet.Chemistry = GetChemistry(primaryStar, planet);
        planet.Biosphere = GetBiosphere(primaryStar, planet);
        planet.Atmosphere = GetAtmosphere(planet);
        planet.Hydrographics = Roll.D3(1);
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
            (<= 6) => PlanetChemistry.Water,
            (<= 8) => PlanetChemistry.Ammonia,
            (<= 10) => PlanetChemistry.Methane,
            _ => PlanetChemistry.None
        };
    }

    private static int GetBiosphere(RttWorldgenStar primaryStar, RttWorldgenPlanet planet)
    {
        int biosphere;

        if (primaryStar.Age >= Roll.D3(1) + (int) planet.Chemistry) {
            biosphere = Roll.D3(1);
        } else if (primaryStar.Age >= 4 + (int) planet.Chemistry) {
            biosphere = Roll.D6(2);
            if (primaryStar.SpectralType == SpectralType.D) {
                biosphere -= 3;
            }
        } else {
            biosphere = 0;
        }

        return biosphere;
    }

    private static int GetAtmosphere(RttWorldgenPlanet planet)
    {
        return planet.Biosphere >= 3 && planet.Chemistry == PlanetChemistry.Water
        ? Math.Max(Math.Min(Roll.D6(2) - 7 + planet.Size, 9), 2)
        : 10;
    }
}