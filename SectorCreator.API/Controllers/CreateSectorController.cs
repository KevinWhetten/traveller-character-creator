using Microsoft.AspNetCore.Mvc;
using SectorCreator.BL;
using SectorCreator.Global;
using SectorCreator.Global.Enums;
using SectorCreator.Models.Factories.Basic;
using SectorCreator.Models.Factories.RttWorldgen;
using SectorCreator.Models.Factories.StarFrontiers;
using SectorCreator.Models.RTTWorldgen.Worlds;

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
                        new RollingService(),
                        new StarSystemFactory(
                            new RollingService(),
                            new PlanetFactory(new RollingService()),
                            new StarFrontiersStarFactory(new RollingService()),
                            new StarFrontiersPlanetFactory(new RollingService())
                        ),
                        new RttWorldgenStarSystemFactory(new RollingService(),
                            new RttWorldgenStarFactory(new RollingService()),
                            new RttWorldgenPlanetFactory(
                                new RollingService(),
                                new AcheronianWorld(new RollingService()),
                                new AreanWorld(new RollingService()),
                                new AridWorld(new RollingService()),
                                new AsphodelianWorld(new RollingService()),
                                new ChthonianWorld(),
                                new HebeanWorld(new RollingService()),
                                new HelianWorld(new RollingService()),
                                new JaniLithicWorld(new RollingService()),
                                new JovianWorld(new RollingService()),
                                new MeltballWorld(new RollingService()),
                                new OceanicWorld(new RollingService()),
                                new PanthalassicWorld(new RollingService()),
                                new PrometheanWorld(new RollingService()),
                                new RockballWorld(new RollingService()),
                                new SnowballWorld(new RollingService()),
                                new StygianWorld(new RollingService()),
                                new TectonicWorld(new RollingService()),
                                new TelluricWorld(new RollingService()),
                                new VesperianWorld(new RollingService())
                            )
                        )
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