namespace SmartFactoryWebApi.Models
{
    public class TrendingAverage
    {
        public int Result { get; set; }
        public string Message { get; set; }
        public List<object> Values { get; set; } = new List<object>();
        public List<string> ColumnNames { get; set; } = new List<string>();
        public Dictionary<string, List<SensorRecordAverage>> Records { get; set; } = new Dictionary<string, List<SensorRecordAverage>>();
    }
}
