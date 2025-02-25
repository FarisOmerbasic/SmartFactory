using System;

namespace SmartFactoryBackend.Sensors
{
    public class WeightSensor : Sensor
    {
        public double Weight { get; set; }

        public WeightSensor(string id) : base(id, "Weight")
        {
            Weight = new Random().NextDouble() * 100; 
        }

        public double GetSensorValue()
        {
            return Weight;
        }
    }
}