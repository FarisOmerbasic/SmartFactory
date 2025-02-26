namespace SmartFactoryWebApi.Dtos
{
    public class SensorDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public double LowerBound { get; set; }
        public double UpperBound { get; set; }
        public double NumericValue { get; set; }
        public string StringValue { get; set; }
        public string Unit { get; set; }
        public int SimulationType { get; set; }
        public double GrowthRatio { get; set; }
        public string Group1 { get; set; }
        public string Group2 { get; set; }
        public string Group3 { get; set; }
        public bool IsActive { get; set; }
        public int UpdateInterval { get; set; }
        public string? CurrentThreshold { get; set; }
    }
}
