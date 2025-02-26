using SmartFactoryBackend.Models;
using System.Collections.Generic;

namespace SmartFactoryWebApi.Services
{
    public static class ProductionService
    {
        // Method to calculate throughput for a specific production line
        public static int CalculateThroughput(string line)
        {
            return line switch
            {
                "Line A" => 92,
                "Line B" => 87,
                "Line C" => 45,
                _ => 0
            };
        }

        // Method to get the status of a production line
        public static string GetStatus(string line)
        {
            return line switch
            {
                "Line A" => "Active",
                "Line B" => "Active",
                "Line C" => "Warning",
                _ => "Unknown"
            };
        }

        // Method to get the machine associated with a production line
        public static string GetMachine(string line)
        {
            return line switch
            {
                "Line C" => "M103",
                "Line B" => "M205",
                _ => "Unknown"
            };
        }

        // Method to get the issue associated with a production line
        public static string GetIssue(string line)
        {
            return line switch
            {
                "Line C" => "Slow Feed Rate",
                "Line B" => "Idle",
                _ => "No Issue"
            };
        }

        // Method to calculate efficiency for a production line
        public static double CalculateEfficiency(string line)
        {
            return line switch
            {
                "Line C" => 45.0,
                "Line B" => 0.0,
                _ => 100.0
            };
        }

        // Method to calculate target deviation for a production line
        public static double CalculateTargetDeviation(string line)
        {
            return line switch
            {
                "Line C" => -47.0,
                "Line B" => -100.0,
                _ => 0.0
            };
        }

        // Method to calculate today's projected output
        public static int CalculateTodaysOutput()
        {
            return 2150;
        }

        // Method to calculate the week's projected output
        public static int CalculateWeeksProjection()
        {
            return 10500;
        }

        // Method to calculate production rate
        public static double CalculateProductionRate(int totalUnitsProduced, double totalTimeTaken)
        {
            if (totalTimeTaken <= 0)
                return 0; // Avoid division by zero

            return totalUnitsProduced / totalTimeTaken;
        }

        // Method to generate production data for a specific line
        public static ProductionDto GetProductionData(string line)
        {
            // Example values for total units produced and total time taken
            int totalUnitsProduced = 1000; // Example value
            double totalTimeTaken = 10.5; // Example value in hours

            var productionData = new ProductionDto
            {
                Line = line,
                Throughput = CalculateThroughput(line),
                Status = GetStatus(line),
                Machine = GetMachine(line),
                Issue = GetIssue(line),
                Efficiency = CalculateEfficiency(line),
                TargetDeviation = CalculateTargetDeviation(line),
                TodaysProjectedOutput = CalculateTodaysOutput(),
                WeeksProjection = CalculateWeeksProjection(),
                ProductionRate = CalculateProductionRate(totalUnitsProduced, totalTimeTaken) // Add production rate
            };

            return productionData;
        }

        // Method to generate optimization suggestions based on production data
        public static List<string> GenerateOptimizationSuggestions(ProductionDto productionData)
        {
            List<string> suggestions = new List<string>();

            if (productionData.Efficiency < 50)
            {
                suggestions.Add($"Improve efficiency of {productionData.Line} - Check {productionData.Machine} for issues.");
            }

            if (productionData.TargetDeviation < -20)
            {
                suggestions.Add($"Address target deviation on {productionData.Line} - Investigate {productionData.Issue}.");
            }

            return suggestions;
        }
    }
}