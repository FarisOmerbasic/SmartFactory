using SmartFactoryBackend.Sensors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace SmartFactoryBackend.Models
{
    
    public class BreakRoom
    {
        public string RoomName { get; set; }
        public List<Sensor> Sensors { get; set; }
        public BreakRoom(string roomName)
        {
            RoomName = roomName;
            Sensors = new List<Sensor>();

            Sensors.Add(new TemperatureSensor("Temp01"));
            Sensors.Add(new HumiditySensor("Humid01"));
            Sensors.Add(new AirQualitySensor("Air01"));
            Sensors.Add(new OccupancySensor("Occupy01"));
        }

        public void DisplaySensorData()
        {
            Console.WriteLine($"--- {RoomName} Sensor Data ---");
            foreach (var sensor in Sensors)
            {
                if (sensor.IsActive)
                {
                    Console.WriteLine($"{sensor.SensorType} Sensor ({sensor.SensorId}");
                }
                else
                {
                    Console.WriteLine($"{sensor.SensorType} Sensor ({sensor.SensorId}) is inactive.");
                }
            }
        }
       
        public void ToggleSensorActivation(string sensorId, bool status)
        {
            var sensor = Sensors.Find(s => s.SensorId == sensorId);
            if (sensor != null)
            {
                sensor.ToggleSensor(status);
                Console.WriteLine($"{sensor.SensorType} Sensor ({sensor.SensorId}) is now {(status ? "active" : "inactive")}.");
            }
            else
            {
                Console.WriteLine("Sensor not found!");
            }
        }
    }
}