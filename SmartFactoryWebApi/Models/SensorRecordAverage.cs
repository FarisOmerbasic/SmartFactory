namespace SmartFactoryWebApi.Models
{
    public class SensorRecordAverage
    {
        public double AverageValue { get; set; }
        public double MinimumValue { get; set; }
        public double MaximumValue { get; set; }
        public DateTime Time { get; set; }
        public int Status { get; set; }
    }
}
