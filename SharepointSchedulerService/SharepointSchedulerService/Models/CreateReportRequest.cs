using SharepointSchedulerService.Models.DTOs;

namespace SharepointSchedulerService.Models
{
    public class CreateReportRequest
    {
        public DocType doctype {  get; set; } = new();
        public ExperimentReportDTO experimentReportDTO { get; set; } = new();
    }
}
