public class ProductionDto
{
    public string Line { get; set; } = "Unknown";
    public int Throughput { get; set; } = 0;
    public string Status { get; set; } = "Unknown";
    public string Machine { get; set; } = "Unknown";
    public string Issue { get; set; } = "None";
    public double Efficiency { get; set; } = 0;
    public double TargetDeviation { get; set; } = 0;
    public int TodaysProjectedOutput { get; set; } = 0;
    public int WeeksProjection { get; set; } = 0;
    public double ProductionRate { get; set; } = 0;
    public List<string> OptimizationSuggestions { get; set; } = new List<string>();
}