using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartFactoryBackend.Sensors
{
    public class Sensor
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public double LowerBound { get; set; }
        public double UpperBound { get; set; }
        public double NumericValue { get; set; }
        public string StringValue { get; set; }
        public string Unit { get; set; }
        public int SimulationType { get; set; }
        public double GrowthRatio { get; set; }
        public string Group1 { get; set; }
        public string Group2 { get; set; }
        public string Group3 { get; set; }
        public bool IsActive { get; set; }
        public int UpdateInterval { get; set; }
        public string SensorType { get; set; }
        public double Latitude { get; set; } = 0.0;
        public double Longitude { get; set; } = 0.0;


        // Prazan konstruktor potreban za deserialization
        public Sensor() { }

        public Sensor(string sensorId, string sensorType)
        {
            Id = sensorId;
            Name = sensorType; // Postavite naziv
            IsActive = true;
        }

        public double GetSensorValue(Sensor sensor)
        {
            return sensor.NumericValue; // Vraća vrednost senzora
        }

        public void ToggleSensor(bool status)
        {
            IsActive = status;
        }
    }
}
