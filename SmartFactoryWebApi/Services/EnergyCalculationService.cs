using SmartFactoryBackend.Models;
using System.Collections.Generic;
using System.Linq;

namespace SmartFactoryWebApi.Services
{
    public static class EnergyCalculationService
    {
        private const double CostPerKWh = 2.56;

        // Method to calculate total power consumption from devices
        public static double CalculateTotalPower(List<Sensor> devices)
        {
            // Sum up the numeric value of each sensor, assuming this represents power consumption
            return devices.Where(device => device.IsActive).Sum(device => device.NumericValue);
        }

        // Method to calculate efficiency rate based on useful output and total input
        public static double CalculateEfficiencyRate(double usefulOutput, double totalInput)
        {
            if (totalInput == 0)
                return 0;

            return (usefulOutput / totalInput) * 100;
        }

        // Method to calculate the cost based on total power consumed
        public static double CalculateCost(double totalPower)
        {
            return totalPower * CostPerKWh;
        }

        // Method to calculate the change in power consumption between two periods
        public static double CalculateChange(double currentConsumption, double previousConsumption)
        {
            if (previousConsumption == 0)
                return 100; // If previous consumption is zero, consider it as 100% change.

            return ((currentConsumption - previousConsumption) / previousConsumption) * 100;
        }

        // Method to generate optimization suggestions based on device status and consumption
        public static List<string> GenerateOptimizationSuggestions(List<Sensor> devices)
        {
            List<string> suggestions = new List<string>();

            foreach (var device in devices)
            {
                // For this model, I'm assuming that you want suggestions based on the numeric value (power consumption)
                if (device.IsActive)
                {
                    if (device.NumericValue > 100) // Arbitrary threshold for high consumption
                    {
                        suggestions.Add($"Reduce power consumption of {device.Name} - Schedule maintenance check.");
                    }
                }
            }

            return suggestions;
        }
    }
}
