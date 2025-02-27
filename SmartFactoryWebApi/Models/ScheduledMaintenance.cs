namespace SmartFactoryBackend.Models
{
    public class ScheduledMaintenance
    {
        public string Machine { get; set; }
        public string Task { get; set; }
        public string ScheduledTime { get; set; }
        public string ExpectedDuration { get; set; }
    }
}