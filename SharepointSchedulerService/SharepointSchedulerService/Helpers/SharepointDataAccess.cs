using log4net;
using MSGraphClient;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using SharepointSchedulerService.Models.DTOs;
using System.Text.Json;
using System.Text.RegularExpressions;
using Microsoft.Extensions.Configuration;


namespace SharepointSchedulerService.Helpers
{
    public class SharepointDataAccess
    {
        private readonly IConfiguration _config;
        private readonly string experimentalReportsClientId;
        private readonly string experimentalReportsClientSecretForSpSchedule;
        private readonly string isisDriveId;
        private readonly string hplDriveId;
        private readonly string artemisDriveId;
        private readonly string lsfDriveId;


        private GraphClient graphClient;
        /*
        private readonly string experimentalReportsClientId = ConfigurationManager.AppSettings["EXPERIMENTALREPORTS.365ClientId"];
        private readonly string experimentalReportsClientSecretForSpSchedule = ConfigurationManager.AppSettings["SCHEDULE.SpSchedule365ClientSecret"];
        private readonly string isisDriveId = ConfigurationManager.AppSettings["EXPERIMENTALREPORTS.365ISISSiteDriveId"];
        private readonly string hplDriveId = ConfigurationManager.AppSettings["EXPERIMENTALREPORTS.365HPLSiteDriveId"];
        private readonly string artemisDriveId = ConfigurationManager.AppSettings["EXPERIMENTALREPORTS.365ArtemisSiteDriveId"];
        private readonly string lsfDriveId = ConfigurationManager.AppSettings["EXPERIMENTALREPORTS.365LSFSiteDriveId"];
        */
        private static readonly ILog Logger = LogManager.GetLogger(typeof(SharepointDataAccess));
        private readonly string resultsFilter = "?orderby=lastModifiedDateTime desc&$top=500";


        public SharepointDataAccess()
        {
            graphClient = new GraphClient(experimentalReportsClientId, experimentalReportsClientSecretForSpSchedule);

            _config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            experimentalReportsClientId = _config["EXPERIMENTALREPORTS.365ClientId"] ?? "Missing ID";
            experimentalReportsClientSecretForSpSchedule = _config["SCHEDULE.SpSchedule365ClientSecret"] ?? "Missing ID";
            isisDriveId = _config["EXPERIMENTALREPORTS.365ISISSiteDriveId"] ?? "Missing ID";
            hplDriveId = _config["EXPERIMENTALREPORTS.365HPLSiteDriveId"] ?? "Missing ID";
            artemisDriveId = _config["EXPERIMENTALREPORTS.365ArtemisSiteDriveId"] ?? "Missing ID";
            lsfDriveId = _config["EXPERIMENTALREPORTS.365LSFSiteDriveId"] ?? "Missing ID";
        }

        public async Task<List<ExperimentWithReportDTO>> GetISISExperimentalReportsListItems(int fromYear)
        {
            Logger.InfoFormat("Getting ISIS Experimental Reports Data");
            var isisExperimentalReports = await graphClient.GetDriveItemsFiltered(isisDriveId, resultsFilter);

            return JsonElementToExperimentWithReportDTO(isisExperimentalReports, "ISIS", fromYear);
        }
        public async Task<List<ExperimentWithReportDTO>> GetArtemisExperimentalReportsListItems(int fromYear)
        {
            Logger.InfoFormat("Getting Artemis Experimental Reports Data");
            var artemisExperimentalReports = await graphClient.GetDriveItemsFiltered(artemisDriveId, resultsFilter);

            return JsonElementToExperimentWithReportDTO(artemisExperimentalReports, "Artemis", fromYear);
        }

        public async Task<List<ExperimentWithReportDTO>> GetHPLExperimentalReportsListItems(int fromYear)
        {
            Logger.InfoFormat("Getting HPL Experimental Reports Data");
            var hplExperimentalReports = await graphClient.GetDriveItemsFiltered(hplDriveId, resultsFilter);

            return JsonElementToExperimentWithReportDTO(hplExperimentalReports, "HPL", fromYear);
        }

        public async Task<List<ExperimentWithReportDTO>> GetLSFExperimentalReportsListItems(int fromYear)
        {
            Logger.InfoFormat("Getting LSF Experimental Reports Data");
            var lsfExperimentalReports = await graphClient.GetDriveItemsFiltered(lsfDriveId, resultsFilter);

            return JsonElementToExperimentWithReportDTO(lsfExperimentalReports, "LSF", fromYear);
        }

        private List<ExperimentWithReportDTO> JsonElementToExperimentWithReportDTO(List<JsonElement> jsonElements, string facility, int fromYear)
        {
            List<ExperimentWithReportDTO> experimentWithReportDTOs = new List<ExperimentWithReportDTO>();
            foreach (JsonElement element in jsonElements)
            {
                string nameString = element.GetProperty("name").ToString();
                string RbNumber = getRBNumberFromFileName(nameString);
                string lastDateModifiedString = element.GetProperty("lastModifiedDateTime").ToString();
                if (!string.IsNullOrEmpty(RbNumber) && isDateGreaterThanFromYear(fromYear, lastDateModifiedString))
                {
                    ExperimentWithReportDTO reportsDTO = new ExperimentWithReportDTO();
                    reportsDTO.ExperimentNumber = RbNumber;
                    reportsDTO.FacilityName = facility;
                    reportsDTO.modifiedDate = DateTime.Parse(lastDateModifiedString);
                    experimentWithReportDTOs.Add(reportsDTO);
                }

            }
            return experimentWithReportDTOs;
        }

        private bool isDateGreaterThanFromYear(int year, string lastModifiedDate)
        {
            DateTime date = DateTime.Parse(lastModifiedDate);

            if (date.Year >= year)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /*
         * RB Numbers are stored in sharepoint in the column "Name" in the format: 
         * #[0-9]+;(RB|App)<RB_NUMBER>.<FILE_EXTENSION>, we just want <RB_NUMBER>
         */
        private string getRBNumberFromFileName(string fileName)
        {

            Regex rbNumberRegex = new Regex("[aA-zZ]*(\\d+)");
            Match rbNumberMatch;
            string referenceNumber = string.Empty;
            if (!string.IsNullOrWhiteSpace(fileName))
            {
                rbNumberMatch = rbNumberRegex.Match(fileName);

                if (rbNumberMatch != null)
                {
                    // Group 0 is the whole thing, 1 is just the reference number part
                    referenceNumber = rbNumberMatch.Groups[1].Value;
                }
                else
                {
                    Logger.WarnFormat("Ref number {0} not in decimal format as expected...leaving untouched", referenceNumber);
                }
            }
            return referenceNumber;
        }
    }
}
