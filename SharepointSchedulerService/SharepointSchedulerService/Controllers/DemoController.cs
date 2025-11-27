using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SharepointSchedulerService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DemoController : ControllerBase
    {
        private readonly IConfiguration _config;

        public DemoController(IConfiguration config)
        {
            _config = config;
        }

        [HttpGet]
        public String Get() 
        {
            return "Welcome to Docker";
        }

        [HttpGet("localSecret")]
        public IActionResult GetLocalSecret()
        {
            var dummyKey = _config["MySecrets:DummyKey"];
            return Ok(dummyKey);
        }

        /*
        [HttpGet("BISAppSecret")]
        public IActionResult GetBISAppSecret()
        {
            var dummyBISAppKey = _config["MyBISAppSecrets:DummyBISAppKey"];
            return Ok(dummyBISAppKey);
        }
        */

        [HttpGet("365ArtemisSiteDriveId")]
        public IActionResult GetArtemisSiteDriveId()
        {
            var ArtemisSiteDriveId = _config["SP:ExperimentalReports:365ArtemisSiteDriveId"];
            return Ok(ArtemisSiteDriveId);
        }
    }
}
