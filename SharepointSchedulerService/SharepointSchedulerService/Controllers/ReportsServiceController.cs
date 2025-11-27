using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SharepointSchedulerService.Models;

namespace SharepointSchedulerService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportsServiceController : ControllerBase
    {
        //GET /api/ReportService/test
        [HttpGet("test")]
        public IActionResult Test()
        {
            return Ok("Reports service is up and running");
        }

        //CREATE /api/ReportService/CreateReport
        [HttpPost("CreateReport")]
        public async Task<ActionResult<byte>> CreateReport([FromBody] CreateReportRequest request)
        {
            var experimentReportDTO = request.experimentReportDTO;
            var doctype = request.doctype;

            byte result = 1; // 1 success, 0 fail
            return Ok(result);
            //Data here
        }
    }
}
