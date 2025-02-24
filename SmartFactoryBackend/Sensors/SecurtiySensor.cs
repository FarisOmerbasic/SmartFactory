using System;

namespace SmartFactoryBackend.Sensors
{
    public class SecuritySensor : Sensor
    {
        public bool IsMotionDetected { get; set; }

        public SecuritySensor(string sensorId) : base(sensorId, "Security Sensor")
        {
            IsMotionDetected = new Random().Next(0, 2) == 1; 
        }

        public double GetSensorValue()
        {
            //1 for detected motion, 0 for no motion
            return IsMotionDetected ? 1 : 0; 
        }
    }
}
