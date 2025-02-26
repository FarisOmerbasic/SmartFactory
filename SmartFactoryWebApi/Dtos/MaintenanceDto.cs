namespace SmartFactoryBackend.Dtos
{
    public class MaintenanceDataDto
    {
        // Maintenance History Table
        public List<MaintenanceTaskDto> History { get; set; } = new List<MaintenanceTaskDto>();

        // Scheduled Maintenance
        public List<ScheduledMaintenanceDto> Scheduled { get; set; } = new List<ScheduledMaintenanceDto>();

        // Aggregated Metrics
        public double TotalCost { get; set; }
        public string TotalDowntime { get; set; }

        // Optimization Suggestions
        public List<string> OptimizationSuggestions { get; set; } = new List<string>();
    }

    public class MaintenanceTaskDto
    {
        public string Machine { get; set; }
        public string Task { get; set; }
        public double Cost { get; set; }
        public string Downtime { get; set; }
        public string Status { get; set; }
    }

    public class ScheduledMaintenanceDto
    {
        public string Machine { get; set; }
        public string Task { get; set; }
        public string ScheduledTime { get; set; }
        public string ExpectedDuration { get; set; }
    }
}