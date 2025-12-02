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
        private readonly SharepointDataAccess _sharepointDataAccess;

        public ProposalsServiceController(SharepointDataAccess sharepointDataAccess)
        {
            _sharepointDataAccess = sharepointDataAccess;
        }

        [HttpGet("test")]
        public IActionResult Test()
        {
            return Ok("Proposals Service is up and running");
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
        public List<ExperimentWithReportDTO> getExperimentReportsData(
            [FromQuery] string facilityName,
            [FromQuery] int fromYear)
        {
            List<ExperimentWithReportDTO> experimentWithReportDTOs = new List<ExperimentWithReportDTO>();

            try
            {
                if (facilityName.Equals("ISIS"))
                {
                    Logger.Info("ISIS facility");
                    experimentWithReportDTOs = AsyncContext.Run(() => _sharepointDataAccess.GetISISExperimentalReportsListItems(fromYear));
                }
                else if (facilityName.Equals("CLF"))
                {
                    Logger.Info("CLF facility");
                    experimentWithReportDTOs.AddRange(AsyncContext.Run(() => _sharepointDataAccess.GetHPLExperimentalReportsListItems(fromYear)));
                    experimentWithReportDTOs.AddRange(AsyncContext.Run(() => _sharepointDataAccess.GetLSFExperimentalReportsListItems(fromYear)));
                    experimentWithReportDTOs.AddRange(AsyncContext.Run(() => _sharepointDataAccess.GetArtemisExperimentalReportsListItems(fromYear)));
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
