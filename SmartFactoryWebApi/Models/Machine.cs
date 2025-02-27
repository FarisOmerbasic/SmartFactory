using SmartFactoryBackend.Models;
using System.Reflection.Metadata.Ecma335;

namespace SmartFactoryWebApi.Models
{
    public class Machine
    {
            public string MachineId { get; set; }
            public string MachineName { get; set; }
            public string RoomName { get; set; }
            public List<Sensor> Sensors { get; set; }
            public int? CurrentTemperature { get; set; }
            //public double? CurrentEnergyConsumption { get; set; }
            //public int? CurrentRPM { get; set; }
            public double UpTime { get; set; }
            public bool IsOperational { get; set; }
            public bool IsActive { get; set; }
    }
}
