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
    }
}
