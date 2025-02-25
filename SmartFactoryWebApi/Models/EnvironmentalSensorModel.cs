namespace SmartFactoryBackend.Models
{
    public class EnvironmentalSensor : Sensor
    {
        public double Temperature { get; set; }
        public double Humidity { get; set; } 
        public double AirPressure { get; set; } 
    }
}