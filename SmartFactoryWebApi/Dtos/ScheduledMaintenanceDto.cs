using System.ComponentModel.DataAnnotations;

namespace SmartFactoryBackend.Dtos
{
    public class MaintenanceDataDto
    {
        public List<ScheduledMaintenanceDto> Scheduled { get; set; } = new List<ScheduledMaintenanceDto>();
    }

    public class ScheduledMaintenanceDto
    {
        [Required(ErrorMessage = "Machine name is required.")]
        public string Machine { get; set; }

        [Required(ErrorMessage = "Task description is required.")]
        public string Task { get; set; }

        [Required(ErrorMessage = "Scheduled time is required.")]
        public string ScheduledTime { get; set; }

        [Required(ErrorMessage = "Expected duration is required.")]
        public string ExpectedDuration { get; set; }
    }
}