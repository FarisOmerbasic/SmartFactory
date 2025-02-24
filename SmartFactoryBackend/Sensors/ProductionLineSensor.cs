using System;

namespace SmartFactoryBackend.Sensors
{
    public class ProductionLineSensor : Sensor
    {
        public double Throughput { get; set; }

        public ProductionLineSensor(string sensorId) : base(sensorId, "Production Line Sensor")
        {
            Throughput = new Random().Next(0, 500);
        }

        public double GetSensorValue()
        {
            return Throughput;
        }
    }
}
