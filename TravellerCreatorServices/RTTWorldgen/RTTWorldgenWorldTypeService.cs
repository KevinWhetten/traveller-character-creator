using TravellerCreatorGlobalMethods;
using TravellerCreatorModels.Enums;
using TravellerCreatorModels.RTTWorldgen;

namespace TravellerCreatorServices.RTTWorldgen;

public class RTTWorldgenWorldTypeService
{
    public RTTWorldgenPlanet GenerateWorldType(RTTWorldgenStar primaryStar, RTTWorldgenPlanet planet)
    {
        if (primaryStar.Luminosity == Luminosity.III || primaryStar.SpectralType == SpectralType.D) {
            if (planet.OrbitPosition <= primaryStar.ExpansionSize) {
                planet.WorldType = planet.PlanetType switch {
                    PlanetType.DwarfPlanet => WorldType.Stygian,
                    PlanetType.Terrestrial => WorldType.Acheronian,
                    PlanetType.Helian => WorldType.Asphodelian,
                    PlanetType.Jovian => WorldType.Chthonian,
                    _ => WorldType.None
                };
            }
        } else
            planet.WorldType = planet.PlanetType switch {
                PlanetType.DwarfPlanet => planet.PlanetOrbit switch {
                    PlanetOrbit.Epistellar => GenerateEpistellarDwarfWorldType(),
                    PlanetOrbit.Inner => GenerateInnerDwarfWorldType(),
                    PlanetOrbit.Outer => GenerateOuterDwarfWorldType(),
                    _ => WorldType.None
                },
                PlanetType.Terrestrial => planet.PlanetOrbit switch {
                    PlanetOrbit.Epistellar => GenerateEpistellarTerrestrialWorldType(),
                    PlanetOrbit.Inner => GenerateInnerTerrestrialWorldType(),
                    PlanetOrbit.Outer => GenerateOuterTerrestrialWorldType(),
                    _ => WorldType.None
                },
                PlanetType.Helian => planet.PlanetOrbit switch {
                    PlanetOrbit.Epistellar => GenerateEpistellarHelianWorldType(),
                    PlanetOrbit.Inner => GenerateInnerHelianWorldType(),
                    PlanetOrbit.Outer => GenerateOuterHelianWorldType(),
                    _ => WorldType.None
                },
                PlanetType.Jovian => planet.PlanetOrbit switch {
                    PlanetOrbit.Epistellar => GenerateEpistellarJovianWorldType(),
                    PlanetOrbit.Inner => GenerateInnerJovianWorldType(),
                    PlanetOrbit.Outer => GenerateOuterJovianWorldType(),
                    _ => WorldType.None
                },
                _ => WorldType.None
            };

        return planet;
    }

    private static WorldType GenerateEpistellarDwarfWorldType()
    {
        return Roll.D6(1) switch {
            (<= 3) => WorldType.Rockball,
            (<= 5) => WorldType.Meltball,
            (>= 6) => Roll.D6(1) switch {
                (<= 4) => WorldType.Hebean,
                (>= 5) => WorldType.Promethian
            }
        };
    }

    private static WorldType GenerateInnerDwarfWorldType()
    {
        return Roll.D6(1) switch {
            (<= 4) => WorldType.Rockball,
            (<= 6) => WorldType.Arean,
            7 => WorldType.Meltball,
            8 => Roll.D6(1) switch {
                (<= 4) => WorldType.Hebean,
                (<= 6) => WorldType.Promethian,
                _ => WorldType.None
            },
            _ => WorldType.None
        };
    }

    private static WorldType GenerateOuterDwarfWorldType()
    {
        return Roll.D6(1) switch {
            (<= 4) => WorldType.Snowball,
            (<= 6) => WorldType.Rockball,
            7 => WorldType.Meltball,
            8 => Roll.D6(1) switch {
                (<= 3) => WorldType.Hebean,
                (<= 5) => WorldType.Arean,
                6 => WorldType.Promethian,
                _ => WorldType.None
            },
            _ => WorldType.None
        };
    }

    private WorldType GenerateEpistellarTerrestrialWorldType()
    {
        return Roll.D6(1) switch {
            (<= 4) => WorldType.JaniLithic,
            5 => WorldType.Vesperian,
            6 => WorldType.Telluric,
            _ => WorldType.None
        };
    }

    private WorldType GenerateInnerTerrestrialWorldType()
    {
        return Roll.D6(2) switch {
            (<= 4) => WorldType.Telluric,
            (<= 6) => WorldType.Arid,
            7 => WorldType.Tectonic,
            (<= 9) => WorldType.Oceanic,
            10 => WorldType.Tectonic,
            (<= 12) => WorldType.Telluric,
            _ => WorldType.None
        };
    }

    private WorldType GenerateOuterTerrestrialWorldType()
    {
        return Roll.D6(1) switch {
            (<= 4) => WorldType.Arid,
            (<= 6) => WorldType.Tectonic,
            (<= 8) => WorldType.Oceanic,
            _ => WorldType.None
        };
    }

    private WorldType GenerateEpistellarHelianWorldType()
    {
        return Roll.D6(1) switch {
            (<= 5) => WorldType.Helian,
            6 => WorldType.Asphodelian,
            _ => WorldType.None
        };
    }

    private WorldType GenerateInnerHelianWorldType()
    {
        return Roll.D6(1) switch {
            (<= 4) => WorldType.Helian,
            (<= 6) => WorldType.Panthalassic,
            _ => WorldType.None
        };
    }

    private WorldType GenerateOuterHelianWorldType()
    {
        return WorldType.Helian;
    }

    private WorldType GenerateEpistellarJovianWorldType()
    {
        return Roll.D6(1) switch {
            (<= 5) => WorldType.Jovian,
            6 => WorldType.Chthonian,
            _ => WorldType.None
        };
    }

    private WorldType GenerateInnerJovianWorldType()
    {
        return WorldType.Jovian;
    }

    private WorldType GenerateOuterJovianWorldType()
    {
        return WorldType.Jovian;
    }
}