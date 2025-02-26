namespace SmartFactoryWebApi.Models
{
    public class EnergyUsage
    {
        public double TotalPower { get; set; } 
        public double EfficiencyRate { get; set; } 
        public double CostToday { get; set; }
        public string CostComparison { get; set; } 
        public string EfficiencyComparison { get; set; } 
        public string CostSavings { get; set; }
    }
}
