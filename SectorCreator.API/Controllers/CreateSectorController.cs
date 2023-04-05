using Microsoft.AspNetCore.Mvc;
using SectorCreator.Global.Enums;
using TravellerCharacterCreatorBL;

namespace SectorCreator.API.Controllers;

[ApiController]
[Route("[controller]")]
public class CreateSectorController : ControllerBase
{
    private readonly SectorGenerator _sectorGenerator;

    public CreateSectorController()
    {
        _sectorGenerator = new SectorGenerator();
    }

    [HttpGet]
    [Route("BasicSector")]
    // https://www.traveller-srd.com/core-rules/world-creation/
    public IActionResult GetBasicSector()
    {
        try {
            return Ok(_sectorGenerator.GenerateSector(SectorType.Basic));
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
            return Ok(_sectorGenerator.GenerateSector(SectorType.SpaceOpera));
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
            return Ok(_sectorGenerator.GenerateSector(SectorType.HardScience));
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
            return Ok(_sectorGenerator.GenerateSector(SectorType.T5));
        }
        catch (Exception) {
            return StatusCode(500);
        }
    }

    [HttpGet]
    [Route("RTTWorldgenSector")]
    // https://wiki.rpg.net/index.php/RTT_Worldgen
    public IActionResult GetRttWorldgenSector()
    {
        try {
            return Ok(_sectorGenerator.GenerateSector(SectorType.RttWorldgen));
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
            return Ok(_sectorGenerator.GenerateSector(SectorType.StarFrontiers));
        }
        catch (Exception) {
            return StatusCode(500);
        }
    }
}