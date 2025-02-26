namespace SmartFactoryWebApi.Models
{
    public class MachineEnergyConsumption
    {
        public string MachineName { get; set; } 
        public double Consumption { get; set; } 
        public string Status { get; set; } 
        public string Activity { get; set; } 
        public string Change { get; set; }
    }
}
