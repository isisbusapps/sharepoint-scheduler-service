namespace SharepointSchedulerService.Models.DTOs
{
    public enum DocType {Word, PDF};

    public class ExperimentReportDTO
    {
        public List<ExperimentPartDTO> ExperimentPartDTO { get; set; }
        public List<ExperimentFundingDTO> ExperimentFundingDTO { get; set; }
        public ExperimentContactDTO ExperimentContactDTO { get; set; }
        public string FacilityName { get; set; }
        public string AppNo { get; set; }
        public string Title { get; set; }
        public string LabManagerComments { get; set; }
        public string Objectives { get; set; }
        public long ScheduledTime { get; set; }
        public long DeliveredTime { get; set; }
        public long UpTime { get; set; }
        public long ExtraTime { get; set; }
        public long DownTime { get; set; }
        public int ShotsDelivered { get; set; }
        public int ShotsFailed { get; set; }
        public float Reliability { get; set; }
        public float CoreAvailability { get; set; }
        public float OverallAvailability { get; set; }
    }
}
