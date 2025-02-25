namespace SmartFactoryBackend.Sensors
{
    public class MotionSensor : Sensor
    {
        public bool IsMotionDetected { get; set; }

        public MotionSensor(string id) : base(id, "Motion Sensor")
        {
            IsMotionDetected = new Random().Next(0, 2) == 1; 
        }

        public bool GetSensorValue()
        {
            return IsMotionDetected;
        }
    }
}