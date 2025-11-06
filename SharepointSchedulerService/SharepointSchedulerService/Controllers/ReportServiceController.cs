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

        //CREATE
        [HttpPost("CreateReport")]
        public async Task<ActionResult<byte>> CreateReport()
        {
            byte result = 1; // 1 success, 0 fail
            return Ok(result);
            //Data here
        }
    }
}
