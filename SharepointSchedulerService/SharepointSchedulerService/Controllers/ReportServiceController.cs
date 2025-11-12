using log4net;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SharepointSchedulerService.Models;

namespace SharepointSchedulerService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportServiceController : ControllerBase
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(ReportServiceController));

        //GET /api/ReportService/test
        [HttpGet("test")]
        public IActionResult Test()
        {
            Logger.Info("VisitsService test endpoint called");
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
