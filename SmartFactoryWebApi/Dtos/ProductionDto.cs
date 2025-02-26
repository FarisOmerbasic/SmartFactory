namespace SmartFactoryBackend.Models
{
    public class ProductionDto
    {
        public string Line { get; set; }
        public int Throughput { get; set; }
        public string Status { get; set; }
        public string Machine { get; set; }
        public string Issue { get; set; }
        public double Efficiency { get; set; }
        public double TargetDeviation { get; set; }
        public int TodaysProjectedOutput { get; set; }
        public int WeeksProjection { get; set; }

        public double ProductionRate { get; set; }

        public List<string> OptimizationSuggestions { get; set; }
    }
}