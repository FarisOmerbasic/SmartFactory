namespace SmartFactoryBackend.Sensors
{
    public class MotionSensor : Sensor
    {
        public bool IsMotionDetected { get; set; }

        public MotionSensor(string id) : base(id, "Motion Sensor") { }

        public override string GetSensorValue(Sensor sensor)
        {
            return IsMotionDetected ? "Motion Detected" : "No Motion";
        }
    }
}