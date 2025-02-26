namespace SmartFactoryWebApi.Models
{
    public class Trending
    {
        public int Result { get; set; }
        public string? Message { get; set; }
        public List<string> Values { get; set; } = new();
        public List<string> ColumnNames { get; set; } = new();
        public Dictionary<string, List<SensorRecord>> Records { get; set; } = new();
    }
}
