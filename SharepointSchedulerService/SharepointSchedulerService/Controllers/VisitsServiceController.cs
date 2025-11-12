using log4net;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SharepointSchedulerService.Models;

namespace SharepointSchedulerService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VisitsServiceController : ControllerBase
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(VisitsServiceController));


        //GET /api/VisitsService/test
        [HttpGet("test")]
        public IActionResult Test()
        {
            Logger.Info("VisitsService test endpoint called");
            return Ok("Vists service is up and running");
        }

        //GET /api/VisitsService/getInvestigatorsForExperiments
        [HttpGet("getInvestigatorsForExperiments")]
        public async Task<ActionResult<List<string>>> getInvestigatorsForExperiments(
            [FromQuery] string userNumber,
            [FromQuery] List<string> experimentNumbers)
        {
            return Ok($"User number {userNumber} and experiment numbers {experimentNumbers}");
            //Data here
        }

        //GET /api/VisitsService/getIsisExperimentNumbers
        [HttpGet("getIsisExperimentNumbers")]
        public async Task<ActionResult<List<string>>> getIsisExperimentNumbers([FromQuery] string userNumber)
        {
            return Ok($"User number {userNumber}");
            //Data here
        }

        //GET /api/VisitsService/getMatchingISISScheduledExperimentParts
        [HttpGet("getMatchingISISScheduledExperimentParts")]
        public async Task<ActionResult<List<ScheduledExperiment>>> getMatchingISISScheduledExperimentParts([FromQuery] string pattern)
        {
            return Ok($"Pattern {pattern}");
            //Data here
        }

        //GET /api/VisitsService/getMatchingScheduledExperimentParts
        [HttpGet("getMatchingScheduledExperimentParts")]
        public async Task<ActionResult<List<ScheduledExperiment>>> getMatchingScheduledExperimentParts([FromQuery] string pattern)
        {
            return Ok($"Pattern {pattern}");
            //Data here
        }

        //GET /api/VisitsService/getMatchingScheduledExperimentPartsByFacility
        [HttpGet("getMatchingScheduledExperimentPartsByFacility")]
        public async Task<ActionResult<List<ScheduledExperiment>>> getMatchingScheduledExperimentPartsByFacility(
            [FromQuery] string pattern,
            [FromQuery] string facility
            )
        {
            return Ok($"Pattern {pattern}, facility: {facility}");
            //Data here
        }

        //GET /api/VisitsService/getPotentialVisitors
        [HttpGet("getPotentialVisitors")]
        public async Task<ActionResult<List<string>>> getPotentialVisitors(
            [FromQuery] string userNumber,
            [FromQuery] string experimentNumber
            )
        {
            return Ok($"User number {userNumber}, experiment number: {experimentNumber}");
            //Data here
        }

        //GET /api/VisitsService/getPreviousScheduledCLFExperimentParts
        [HttpGet("getPreviousScheduledCLFExperimentParts")]
        public async Task<ActionResult<List<ScheduledExperiment>>> getPreviousScheduledCLFExperimentParts([FromQuery] string userNumber)
        {
            return Ok($"User number {userNumber}");
            //Data here
        }

        //GET /api/VisitsService/getUpcomingScheduledCLFExperimentParts
        [HttpGet("getUpcomingScheduledCLFExperimentParts")]
        public async Task<ActionResult<List<ScheduledExperiment>>> getUpcomingScheduledCLFExperimentParts([FromQuery] string userNumber)
        {
            return Ok($"User number {userNumber}");
            //Data here
        }
    }
}
