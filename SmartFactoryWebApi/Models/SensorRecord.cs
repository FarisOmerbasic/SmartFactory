namespace SmartFactoryWebApi.Models
{
    public class SensorRecord
    {
        public string Value { get; set; } = string.Empty;
        public DateTime Time { get; set; }
        public int Status { get; set; }
    }
}
