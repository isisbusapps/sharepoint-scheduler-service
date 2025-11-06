namespace SharepointSchedulerService.Models.DTOs
{
    public class ExperimentFundingDTO
    {
        public string Source { get; set; }
        public string GrantNo { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Comments { get; set; }
    }
}
