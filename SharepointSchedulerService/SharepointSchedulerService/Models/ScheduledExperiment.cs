namespace SharepointSchedulerService.Models
{
    public class ScheduledExperiment : IEquatable<ScheduledExperiment>
    {
        public ScheduledExperiment() { }
        public String experimentNumber { get; set; }
        public String experimentTitle { get; set; }
        public long partNumber { get; set; }
        public String facility { get; set; }
        public String facilityArea { get; set; }
        public DateTime startDate { get; set; }
        public DateTime endDate { get; set; }
        public bool Equals(ScheduledExperiment other)
        {
            return (experimentNumber == other.experimentNumber && partNumber == other.partNumber);
        }
    }
}
