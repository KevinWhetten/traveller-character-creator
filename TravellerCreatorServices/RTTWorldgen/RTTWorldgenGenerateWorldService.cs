using TravellerCreatorGlobalMethods;
using TravellerCreatorModels.Enums;
using TravellerCreatorModels.RTTWorldgen;
using TravellerCreatorServices.RTTWorldgen.Planets;

namespace TravellerCreatorServices.RTTWorldgen;

public class RTTWorldgenGenerateWorldService
{
    public RTTWorldgenPlanet GenerateWorld(RTTWorldgenPlanet planet, RTTWorldgenStar primaryStar)
    {
        return planet.WorldType switch {
            WorldType.Acheronian => AcheronianPlanet.Generate(planet),
            WorldType.Arean => AreanPlanet.Generate(planet, primaryStar),
            WorldType.Arid => AridPlanet.Generate(planet, primaryStar),
            WorldType.Asphodelian => AsphodelianPlanet.Generate(planet),
            WorldType.Chthonian => ChthonianPlanet.Generate(planet),
            WorldType.Hebean => HebeanPlanet.Generate(planet),
            WorldType.Helian => HelianPlanet.Generate(planet),
            WorldType.JaniLithic => JaniLithicPlanet.Generate(planet),
            WorldType.Jovian => JovianPlanet.Generate(planet, primaryStar),
            WorldType.Meltball => MeltballPlanet.Generate(planet),
            WorldType.Oceanic => OceanicPlanet.Generate(planet, primaryStar),
            WorldType.Panthalassic => PanthalassicPlanet.Generate(planet, primaryStar),
            WorldType.Promethian => PromethianPlanet.Generate(planet, primaryStar),
            WorldType.Rockball => RockballPlanet.Generate(planet, primaryStar),
            WorldType.Snowball => SnowballPlanet.Generate(planet, primaryStar),
            WorldType.Stygian => StygianPlanet.Generate(planet),
            WorldType.Tectonic => TectonicPlanet.Generate(planet, primaryStar),
            WorldType.Telluric => TelluricPlanet.Generate(planet),
            WorldType.Vesperian => VesperianPlanet.Generate(planet, primaryStar),
            WorldType.None => planet,
            _ => planet
        };
    }
}