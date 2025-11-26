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

        [HttpGet("secret")]
        public IActionResult GetSecret()
        {
            var dummyKey = _config["MySecrets:DummyKey"];
            return Ok(dummyKey);
        }
    }
}
