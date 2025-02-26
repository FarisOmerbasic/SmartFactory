namespace SmartFactoryWebApi.Dtos
{
    public class EnergyDto
    {
        
        public double TotalPower { get; set; }

        
        public double EfficiencyRate { get; set; }

        
        public double TotalCost { get; set; }

      
        public List<string> OptimizationSuggestions { get; set; }

        public double ConsumptionChange { get; set; }
    }
}
