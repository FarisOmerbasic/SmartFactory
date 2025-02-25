using System;

namespace SmartFactoryBackend.Sensors
{
    public class AirQualitySensor : Sensor
    {
        public double AirQualityIndex { get; set; }
        public double AQIThreshold { get; set; } = 300; 

        public AirQualitySensor(string sensorId) : base(sensorId, "Air Quality")
        {
            AirQualityIndex = new Random().Next(0, 501); 
        }

        public double GetSensorValue()
        {
            return AirQualityIndex;
        }

        public void CheckAirQuality()
        {
            if (AirQualityIndex > AQIThreshold)
            {
                TriggerAirQualityAlert();
            }
            else
            {
                Console.WriteLine($"Air quality is within safe limits. Current AQI: {AirQualityIndex}");
            }
        }

        private void TriggerAirQualityAlert()
        {
            Console.WriteLine($"ALERT: Air quality is unsafe! Current AQI: {AirQualityIndex} (Threshold: {AQIThreshold})");
           
        }
    }
}