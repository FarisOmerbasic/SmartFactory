using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;
using DotNetEnv;
using SmartFactoryBackend.Sensors;

namespace SmartFactoryBackend.Models
{
    public class ControlRoom
    {
        public string RoomName { get; set; }
        public List<Sensor> Sensors { get; set; }

        public ControlRoom(string roomName)
        {
            RoomName = roomName;
            Sensors = new List<Sensor>();
        }

        public async Task FetchSensorData()
        {
            try
            {
                string formattedRoomName = FormatRoomName(RoomName);

                string apiUrl = $"https://slb-skyline.on.dataminer.services/api/custom/IndustrySimulator/getCategory?name={formattedRoomName}";
                string token = GetApiToken();

                if (string.IsNullOrEmpty(token))
                {
                    Console.WriteLine("Error: API token is missing.");
                    return;
                }

                // Korišćenje Singleton HttpClient-a
                HttpClient client = HttpClientSingleton.GetClient();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                HttpResponseMessage response = await client.GetAsync(apiUrl);
                response.EnsureSuccessStatusCode();

                string responseBody = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Raw API Response:\n{responseBody}\n");

                List<Sensor> allSensors = JsonSerializer.Deserialize<List<Sensor>>(responseBody);

                if (!allSensors.Any())
                {
                    Console.WriteLine($"No sensors found for {RoomName}.");
                    return;
                }

                foreach (var sensor in allSensors)
                {
                    Sensor specificSensor = CreateSpecificSensor(sensor);
                    if (specificSensor != null)
                    {
                        Sensors.Add(specificSensor);
                        Console.WriteLine($"Added {sensor.Name} sensor to {RoomName}");
                    }
                }

                Console.WriteLine($"Sensor data updated for {RoomName}.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching sensor data: {ex.Message}");
            }
        }

        private static string GetApiToken()
        {
            return Env.GetString("TOKEN");
        }

        private Sensor CreateSpecificSensor(Sensor sensor)
        {
            switch (sensor.Name)
            {
                case "Energy Meter":
                    return new EnergyMeterSensor(sensor.Id) { EnergyUsage = sensor.NumericValue };
                case "Production Line Sensor":
                    return new ProductionLineSensor(sensor.Id) { Throughput = sensor.NumericValue };
                case "Security Camera":
                case "Motion Sensor":
                    return new MotionSensor(sensor.Id) { IsMotionDetected = sensor.NumericValue > 0 };
                default:
                    return null;
            }
        }

        public void DisplaySensorData()
        {
            Console.WriteLine($"--- {RoomName} Sensor Data ---");
            foreach (var sensor in Sensors)
            {
                Console.WriteLine($"{sensor.SensorType} Sensor ({sensor.Id}) - Current Value: {sensor.GetSensorValue(sensor)}");
            }
        }

        public void AnalyzeFactoryPerformance()
        {
            Console.WriteLine($"--- {RoomName} Performance Analysis ---");
            foreach (var sensor in Sensors)
            {
                if (sensor is EnergyMeterSensor energyMeter)
                {
                    Console.WriteLine($"Energy Usage: {energyMeter.EnergyUsage} kW");
                }
                else if (sensor is ProductionLineSensor productionSensor)
                {
                    Console.WriteLine($"Production Throughput: {productionSensor.Throughput} units/hr");
                }
                else if (sensor is MotionSensor motionSensor)
                {
                    Console.WriteLine($"Motion Status: {(motionSensor.IsMotionDetected ? "Motion Detected" : "No Motion")} ");
                }
            }
        }

        private static string FormatRoomName(string roomName)
        {
            return roomName.Replace("ControlRoom", "Control Room");
        }
    }
}
