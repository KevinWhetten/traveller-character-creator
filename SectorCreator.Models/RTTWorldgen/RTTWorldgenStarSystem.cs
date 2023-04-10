using SectorCreator.Global;
using SectorCreator.Global.Enums;
using SectorCreator.Models.Basic;
using SectorCreator.Models.Factories;
using SectorCreator.Models.RTTWorldgen.Planets;

namespace SectorCreator.Models.RTTWorldgen;

public class RttWorldgenStarSystem : StarSystem
{
    private readonly IRollingService _rollingService;
    private readonly RttWorldgenPlanetFactory _rttWorldgenPlanetFactory = new(new RollingService(),
        new AcheronianPlanet(new RollingService(), new PlanetValidation()),
        new AreanPlanet(new RollingService(), new PlanetValidation()),
        new AridPlanet(new RollingService(), new PlanetValidation()),
        new AsphodelianPlanet(new RollingService(), new PlanetValidation()),
        new ChthonianPlanet(new PlanetValidation()),
        new HebeanPlanet(new RollingService(), new PlanetValidation()),
        new HelianPlanet(new RollingService(), new PlanetValidation()),
        new JaniLithicPlanet(new RollingService(), new PlanetValidation()),
        new JovianPlanet(new RollingService(), new PlanetValidation()),
        new MeltballPlanet(new RollingService(), new PlanetValidation()),
        new OceanicPlanet(new RollingService(), new PlanetValidation()),
        new PanthalassicPlanet(new RollingService(), new PlanetValidation()),
        new PromethianPlanet(new RollingService(), new PlanetValidation()),
        new RockballPlanet(new RollingService(), new PlanetValidation()),
        new SnowballPlanet(new RollingService(), new PlanetValidation()),
        new StygianPlanet(new RollingService(), new PlanetValidation()),
        new TectonicPlanet(new RollingService(), new PlanetValidation()),
        new TelluricPlanet(new RollingService(), new PlanetValidation()),
        new VesperianPlanet(new RollingService(), new PlanetValidation()));

    public RttWorldgenStarSystem(IRollingService rollingService)
    {
        _rollingService = rollingService;
    }

    public RttWorldgenStarSystem(RttWorldgenPlanetFactory rttWorldgenPlanetFactory, IRollingService rollingService)
    {
        _rttWorldgenPlanetFactory = rttWorldgenPlanetFactory;
        _rollingService = rollingService;
    }

    public void Generate(StarSystemType starSystemType)
    {
    }

    public void Generate(RttWorldgenStar star)
    {
    }
}