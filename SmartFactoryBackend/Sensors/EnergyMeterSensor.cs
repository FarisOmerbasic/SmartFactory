using System;

namespace SmartFactoryBackend.Sensors
{
    public class EnergyMeterSensor : Sensor
    {
        public double EnergyUsage { get; set; }

        public EnergyMeterSensor(string sensorId) : base(sensorId, "Energy Meter")
        {
            EnergyUsage = new Random().Next(100, 1000);
        }

        public double GetSensorValue()
        {
            return EnergyUsage;
        }
    }
}
