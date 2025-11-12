using log4net;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Nito.AsyncEx;
using SharepointSchedulerService.Helpers;
using SharepointSchedulerService.Models.DTOs;

namespace SharepointSchedulerService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProposalsServiceController : ControllerBase
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(ProposalsServiceController));

        public ProposalsServiceController()
        {
            Logger.Info("Proposals Logging initialised");
        }

        //GET /api/ProposalsService/test
        [HttpGet("test")]
        public IActionResult Test()
        {
            Logger.Info("Proposals service test endpoint called");
            return Ok("Proposals service is up and running");
        }

        //GET /api/ProposalsService/getClfRounds
        [HttpGet("getClfRounds")]
        public async Task<ActionResult<string>> GetClfRounds()
        {
            return Ok("List of CLF rounds returned here");
            //Data here
        }

        //GET /api/ProposalsService/getIsisRounds
        [HttpGet("getIsisRounds")]
        public async Task<ActionResult<string>> getIsisRounds()
        {
            return Ok("List of ISIS rounds returned here");
            //Data here
        }

        //GET /api/ProposalsService/getExperimentReportsData
        [HttpGet("getExperimentReportsData")]
        public async Task<ActionResult<List<ExperimentWithReportDTO>>> getExperimentReportsData(
            [FromQuery] string facilityName,
            [FromQuery] int fromYear)
        {
            List<ExperimentWithReportDTO> experimentWithReportDTOs = new List<ExperimentWithReportDTO>();

            try
            {
                SharepointDataAccess sharepointDataAccess = new SharepointDataAccess();
                if (facilityName.Equals("ISIS"))
                {
                    experimentWithReportDTOs = AsyncContext.Run(() => sharepointDataAccess.GetISISExperimentalReportsListItems(fromYear));
                }
                else if (facilityName.Equals("CLF"))
                {
                    experimentWithReportDTOs.AddRange(AsyncContext.Run(() => sharepointDataAccess.GetHPLExperimentalReportsListItems(fromYear)));
                    experimentWithReportDTOs.AddRange(AsyncContext.Run(() => sharepointDataAccess.GetLSFExperimentalReportsListItems(fromYear)));
                    experimentWithReportDTOs.AddRange(AsyncContext.Run(() => sharepointDataAccess.GetArtemisExperimentalReportsListItems(fromYear)));
                }
            }
            catch (ArgumentException ex)
            {
                Logger.WarnFormat("Could not look up experimental reports for facility name {0}: {1}", facilityName, ex);
                return new List<ExperimentWithReportDTO>();
            }

            return experimentWithReportDTOs;
        }
    }
}
