using ClassLibrary1;
using Microsoft.AspNetCore.Mvc;
using SectorCreator.WorldBuilder;

namespace SectorCreator.API.Controllers;

[ApiController]
[Route("[controller]")]
public class GetSectorController : ControllerBase
{

    [HttpGet]
    [Route("WorldBuilderSubsectors")]
    public IActionResult GetWorldBuilderSubsectors(string sectorId)
    {
        try {
            var sectorIdGuid = Guid.Parse(sectorId);
            List<WorldBuilderSubsector> subsectors = WorldBuilderDatabase.GetSubsectors(sectorIdGuid);
            return Ok(subsectors);
        }
        catch (Exception) {
            return StatusCode(500);
        }
    }
    
    [HttpGet]
    [Route("WorldBuilderHexes")]
    public IActionResult GetWorldBuilderHex(string subsectorId)
    {
        try {
            var subsectorIdGuid = Guid.Parse(subsectorId);
            List<WorldBuilderHex> subsectors = WorldBuilderDatabase.GetHexes(subsectorIdGuid);
            return Ok(subsectors);
        }
        catch (Exception) {
            return StatusCode(500);
        }
    }
}