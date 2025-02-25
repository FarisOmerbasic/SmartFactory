namespace SmartFactoryBackend.Models
{
    public class Room
    {
        public string RoomId { get; set; } 
       public string RoomName { get; set; }
        public List<Sensor> Sensors { get; set; } = new List<Sensor>(); 
        public List<Machine> Machines { get; set; } = new List<Machine>(); 
}