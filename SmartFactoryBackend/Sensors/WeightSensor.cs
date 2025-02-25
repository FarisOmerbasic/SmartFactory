namespace SmartFactoryBackend.Sensors
{
    public class WeightSensor : Sensor
    {
        public double Weight { get; set; }

        public WeightSensor(string id) : base(id, "Weight") { }

        public override string GetSensorValue(Sensor sensor)
        {
            return $"{Weight} kg";
        }
    }
}