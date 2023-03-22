using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using TravellerCharacterCreatorBL;
using TravellerCreatorModels;
using TravellerCreatorModels.Basic;

namespace TravellerCharacterCreatorAPI.Controllers;

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
    public Sector GetBasicSector()
    {
        return _sectorGenerator.GenerateSector(SectorType.Basic);
    }
    
    [HttpGet]
    [Route("SpaceOperaSector")]
    // https://www.traveller-srd.com/core-rules/world-creation/
    public Sector GetSpaceOperaSector()
    {
        return _sectorGenerator.GenerateSector(SectorType.SpaceOpera);
    }
    
    [HttpGet]
    [Route("HardScienceSector")]
    // https://www.traveller-srd.com/core-rules/world-creation/
    public Sector GetHardScienceSector()
    {
        return _sectorGenerator.GenerateSector(SectorType.HardScience);
    }
    
    [HttpGet]
    [Route("SecondSurveySector")]
    // https://travellermap.com/doc/secondsurvey
    public Sector GetSecondSurveySector()
    {
        return _sectorGenerator.GenerateSector(SectorType.SecondSurvey);
    }
    
    [HttpGet]
    [Route("RTTWorldgenSector")]
    // https://wiki.rpg.net/index.php/RTT_Worldgen
    public Sector GetRTTWorldgenSector()
    {
        return _sectorGenerator.GenerateSector(SectorType.RTTWorldgen);
    }
}