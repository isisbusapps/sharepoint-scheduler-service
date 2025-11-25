using log4net;
using Microsoft.Extensions.Logging;
using SharepointSchedulerService.Models.DTOs;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace SharepointSchedulerService.Helpers
{
    public class SharepointDataAccess
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(SharepointDataAccess));

        /*
        public async Task<List<ExperimentWithReportDTO>> GetISISExperimentalReportsListItems(int fromYear)
        {
            //Not complete
        }
        */

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
