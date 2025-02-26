namespace SmartFactoryBackend.Models
{
    public class MaintenanceTask
    {
        public string Machine { get; set; }  
        public string Task { get; set; }    
        public double Cost { get; set; }    
        public string Downtime { get; set; } 
        public string Status { get; set; }  
    }
}