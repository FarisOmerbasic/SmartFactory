namespace SmartFactoryBackend.Models
{
    public class Machine
    {
        public string MachineId { get; set; } 
        public string MachineType { get; set; } 
        public string RoomName { get; set; } 
        public bool IsOperational { get; set; } 
        public bool IsActive { get; set; }
        public List<Sensor> Sensors { get; set; } = new List<Sensor>(); 
    }
}
