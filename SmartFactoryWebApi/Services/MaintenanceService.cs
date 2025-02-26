using SmartFactoryBackend.Models;
using System.Collections.Generic;
using System.Linq;

namespace SmartFactoryWebApi.Services
{
    public static class MaintenanceService
    {
        // Method to calculate total maintenance cost from tasks
        public static double CalculateTotalCost(List<MaintenanceTask> tasks)
        {
            // Sum up the cost of each maintenance task
            return tasks.Where(task => task.Status == "Completed").Sum(task => task.Cost);
        }

        // Method to calculate total downtime from tasks
        public static double CalculateTotalDowntime(List<MaintenanceTask> tasks)
        {
            // Sum up the downtime of each task (parsed from "12h" strings)
            return tasks.Where(task => task.Status == "Completed").Sum(task => ParseDowntime(task.Downtime));
        }

        // Method to calculate the average cost per maintenance task
        public static double CalculateAverageCost(List<MaintenanceTask> tasks)
        {
            if (tasks.Count == 0)
                return 0;

            return tasks.Average(task => task.Cost);
        }

        // Method to calculate the change in maintenance cost between two periods
        public static double CalculateCostChange(double currentCost, double previousCost)
        {
            if (previousCost == 0)
                return 100; // If previous cost is zero, consider it as 100% change.

            return ((currentCost - previousCost) / previousCost) * 100;
        }

        // Method to generate optimization suggestions based on maintenance tasks
        public static List<string> GenerateOptimizationSuggestions(List<MaintenanceTask> tasks)
        {
            List<string> suggestions = new List<string>();

            foreach (var task in tasks)
            {
                // Example thresholds for high cost and downtime
                if (task.Cost > 3000)
                {
                    suggestions.Add($"High cost detected for {task.Machine}: {task.Task}. Review expenses.");
                }

                if (ParseDowntime(task.Downtime) > 24)
                {
                    suggestions.Add($"Long downtime detected for {task.Machine}: {task.Downtime}. Investigate delays.");
                }
            }

            return suggestions;
        }

        // Helper method to parse downtime strings like "12h"
        private static double ParseDowntime(string downtime)
        {
            if (string.IsNullOrEmpty(downtime))
                return 0;

            // Remove "h" and parse the numeric value
            if (double.TryParse(downtime.Replace("h", "").Trim(), out double result))
            {
                return result;
            }

            return 0;
        }
    }
}