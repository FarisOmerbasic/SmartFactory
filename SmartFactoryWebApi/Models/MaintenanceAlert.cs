namespace SmartFactoryBackend.Models
{
    public class MaintenanceAlert
    {
        public string Machine { get; set; }       
        public string Issue { get; set; }       
        public string Priority { get; set; }      
        public string Recommendation { get; set; } 
    }
}