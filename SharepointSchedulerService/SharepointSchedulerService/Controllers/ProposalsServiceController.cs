using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SharepointSchedulerService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProposalsServiceController : ControllerBase
    {
        //GET /api/ProposalsService/test
        [HttpGet("test")]
        public IActionResult Test()
        {
            return Ok("Proposals service is up and running");
        }

        [HttpGet("getClfRounds")]
        public async Task<ActionResult<string>> GetClfRounds()
        {
            return Ok("List of CLF rounds returned here");
            //Data here
        }

        [HttpGet("getIsisRounds")]
        public async Task<ActionResult<string>> getIsisRounds()
        {
            return Ok("List of ISIS rounds returned here");
            //Data here
        }

        [HttpGet("getExperimentReportsData")]
        public async Task<ActionResult<string>> getExperimentReportsData(
            [FromQuery] string facilityName,
            [FromQuery] int fromYear)
        {
            return Ok($"Returning data for {facilityName} from year {fromYear}");
        }

    }
}
