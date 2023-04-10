using Microsoft.AspNetCore.Mvc;
using SectorCreator.BL;
using SectorCreator.Global;
using SectorCreator.Global.Enums;
using SectorCreator.Models.Factories;
using SectorCreator.Models.RTTWorldgen;
using SectorCreator.Models.RTTWorldgen.Planets;
using SectorCreator.Models.StarFrontiers;

namespace SectorCreator.API.Controllers;

[ApiController]
[Route("[controller]")]
public class CreateSectorController : ControllerBase
{
    private readonly ISectorGenerator _sectorGenerator;

    public CreateSectorController()
    {
        _sectorGenerator = new SectorGenerator(
            new SectorFactory(
                new SubsectorFactory(
                    new HexFactory(
                        new StarSystemFactory(
                            new RollingService(), new PlanetFactory(
                                new RollingService()
                            ), new RttWorldgenPlanetFactory(
                                new RollingService(),
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
                                new VesperianPlanet(new RollingService(), new PlanetValidation())),
                            new StarFrontiersPlanetFactory(new RollingService())
                        ),
                        new RollingService()
                    )
                )
            )
        );
    }

    [HttpGet]
    [Route("BasicSector")]
    // https://www.traveller-srd.com/core-rules/world-creation/
    public IActionResult GetBasicSector()
    {
        try {
            return Ok(_sectorGenerator.GenerateMongooseSector(SectorType.Basic));
        }
        catch (Exception) {
            return StatusCode(500);
        }
    }

    [HttpGet]
    [Route("SpaceOperaSector")]
    // https://www.traveller-srd.com/core-rules/world-creation/
    public IActionResult GetSpaceOperaSector()
    {
        try {
            return Ok(_sectorGenerator.GenerateMongooseSector(SectorType.SpaceOpera));
        }
        catch (Exception) {
            return StatusCode(500);
        }
    }

    [HttpGet]
    [Route("HardScienceSector")]
    // https://www.traveller-srd.com/core-rules/world-creation/
    public IActionResult GetHardScienceSector()
    {
        try {
            return Ok(_sectorGenerator.GenerateMongooseSector(SectorType.HardScience));
        }
        catch (Exception) {
            return StatusCode(500);
        }
    }

    [HttpGet]
    [Route("T5Sector")]
    // https://travellermap.com/doc/secondsurvey
    public IActionResult GetT5Sector()
    {
        try {
            return Ok(_sectorGenerator.GenerateT5Sector());
        }
        catch (Exception) {
            return StatusCode(500);
        }
    }

    [HttpGet]
    [Route("RttWorldgenSector")]
    // https://wiki.rpg.net/index.php/RTT_Worldgen
    public IActionResult GetRttWorldgenSector()
    {
        try {
            return Ok(_sectorGenerator.GenerateRttWorldgenSector());
        }
        catch (Exception ex) {
            return StatusCode(500, $"{ex.Message}{ex.StackTrace}");
        }
    }

    [HttpGet]
    [Route("StarFrontiersSector")]
    // StarFrontiersMethod.txt
    public IActionResult GetStarFrontiersSector()
    {
        try {
            return Ok(_sectorGenerator.GenerateStarFrontiersSector());
        }
        catch (Exception) {
            return StatusCode(500);
        }
    }
}