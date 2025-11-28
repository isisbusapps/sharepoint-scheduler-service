using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SharepointSchedulerService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DemoController : ControllerBase
    {
        [HttpGet]
        public String Get() 
        {
            return "Welcome to Docker";
        }

        //Remove below before committing to dev:
        private readonly IConfiguration _config;

        public DemoController(IConfiguration config)
        {
            _config = config;
        }

        [HttpGet("GetSecrets")]
        public IActionResult GetSecrets()
        {
            var isisDriveId = _config["SP:ExperimentalReports:365ISISSiteDriveId"];
            var hplDriveId = _config["SP:ExperimentalReports:365HPLSiteDriveId"];
            var lsfDriveId = _config["SP:ExperimentalReports:365LSFSiteDriveId"];
            var artemisDriveId = _config["SP:ExperimentalReports:365ArtemisSiteDriveId"];


            return Ok("Values: " + isisDriveId + " " + hplDriveId + " " + lsfDriveId + " " + artemisDriveId);
        }
    }
}
