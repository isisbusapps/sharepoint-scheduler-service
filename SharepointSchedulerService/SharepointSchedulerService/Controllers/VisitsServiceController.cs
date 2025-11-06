using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SharepointSchedulerService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VisitsServiceController : ControllerBase
    {
        //GET /api/VisitsService/test
        [HttpGet("test")]
        public IActionResult Test()
        {
            return Ok("Vists service is up and running");
        }
    }
}
