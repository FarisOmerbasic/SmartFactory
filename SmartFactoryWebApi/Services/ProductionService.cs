using SmartFactoryBackend.Models;
using System.Collections.Generic;

namespace SmartFactoryWebApi.Services
{
    public class ProductionService
    {
        public int CalculateThroughput(string line)
        {
            return line switch
            {
                "Line A" => 92,
                "Line B" => 87,
                "Line C" => 45,
                _ => 0
            };
        }

        public string GetStatus(string line)
        {
            return line switch
            {
                "Line A" => "Active",
                "Line B" => "Active",
                "Line C" => "Warning",
                _ => "Unknown"
            };
        }

        public string GetMachine(string line)
        {
            return line switch
            {
                "Line A" => "M101",
                "Line B" => "M205",
                "Line C" => "M103",
                _ => "No Machine Assigned"
            };
        }

        public string GetIssue(string line)
        {
            return line switch
            {
                "Line C" => "Slow Feed Rate",
                "Line B" => "Idle",
                _ => "No Issue"
            };
        }

        public double CalculateEfficiency(string line)
        {
            return line switch
            {
                "Line C" => 45.0,
                "Line B" => 0.0,
                _ => 100.0
            };
        }

        public double CalculateTargetDeviation(string line)
        {
            return line switch
            {
                "Line C" => -47.0,
                "Line B" => -100.0,
                _ => 0.0
            };
        }

        public int CalculateTodaysOutput(string line)
        {
            int throughput = CalculateThroughput(line);
            return throughput * 8;
        }

        public int CalculateWeeksProjection(string line)
        {
            return CalculateTodaysOutput(line) * 5; 
        }

        public double CalculateProductionRate(int totalUnitsProduced, double totalTimeTaken)
        {
            if (totalTimeTaken <= 0)
                throw new ArgumentException("Total time taken must be greater than zero.");

            return totalUnitsProduced / totalTimeTaken;
        }

        public List<string> GenerateOptimizationSuggestions(ProductionDto productionData)
        {
            List<string> suggestions = new List<string>();
            if (productionData.Efficiency > 70)
                suggestions.Add($"No improvement suggestions");

            if (productionData.Efficiency < 50)
                suggestions.Add($"Improve efficiency of {productionData.Line} - Check {productionData.Machine} for issues.");

            if (productionData.TargetDeviation < -20)
                suggestions.Add($"Address target deviation on {productionData.Line} - Investigate {productionData.Issue}.");

            return suggestions;
        }
    }
}
