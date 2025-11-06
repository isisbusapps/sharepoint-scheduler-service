using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SharepointSchedulerService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportServiceController : ControllerBase
    {
        //GET /api/ReportService/test
        [HttpGet("test")]
        public IActionResult Test()
        {
            return Ok("Reports service is up and running");
        }
    }
}
