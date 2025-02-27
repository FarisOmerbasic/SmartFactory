using System.Text.Json.Serialization;

namespace SmartFactoryWebApi.Models
{
    public class Trending
    {
        [JsonPropertyName("Result")]
        public int Result { get; set; }

        [JsonPropertyName("Message")]
        public string? Message { get; set; }

        [JsonPropertyName("Values")]
        public List<string> Values { get; set; } = new();

        [JsonPropertyName("ColumnNames")]
        public List<string> ColumnNames { get; set; } = new();

        [JsonPropertyName("Records")]
        public Dictionary<string, List<SensorRecord>> Records { get; set; } = new Dictionary<string, List<SensorRecord>>();
    }
}
