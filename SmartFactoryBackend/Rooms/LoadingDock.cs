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
    public class LoadingDock
    {
        public string AreaName { get; set; }
        public List<Sensor> Sensors { get; set; }

        private static readonly HttpClient client = new HttpClient();

        public LoadingDock(string areaName)
        {
            AreaName = areaName;
            Sensors = new List<Sensor>();
        }

        public async Task FetchSensorData()
        {
            try
            {
                string apiUrl = "https://slb-skyline.on.dataminer.services/api/custom/IndustrySimulator/getAllDevices";
                string token = GetApiToken();

                if (string.IsNullOrEmpty(token))
                {
                    Console.WriteLine("Error: API token is missing.");
                    return;
                }

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                HttpResponseMessage response = await client.GetAsync(apiUrl);
                response.EnsureSuccessStatusCode();

                string responseBody = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Raw API Response:\n{responseBody}\n");

                List<Sensor> allSensors = JsonSerializer.Deserialize<List<Sensor>>(responseBody);
                var areaSensors = allSensors.Where(sensor => sensor.Group1 == AreaName).ToList();

                if (!areaSensors.Any())
                {
                    Console.WriteLine($"No sensors found for {AreaName}.");
                    return;
                }

                foreach (var sensor in areaSensors)
                {
                    Sensor specificSensor = CreateSpecificSensor(sensor);
                    if (specificSensor != null)
                    {
                        Sensors.Add(specificSensor);
                        Console.WriteLine($"Added {sensor.Name} sensor to {AreaName}");
                    }
                }

                Console.WriteLine($"Sensor data updated for {AreaName}.");
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
                case "RFID Scanner":
                case "Barcode Scanner":
                    return new TrackingSensor(sensor.Id) { IsItemScanned = sensor.NumericValue > 0 };
                case "Weight Sensor":
                    return new WeightSensor(sensor.Id) { Weight = sensor.NumericValue };
                case "Environmental Sensor":
                    return new EnvironmentalSensor(sensor.Id) { Temperature = sensor.NumericValue };
                default:
                    return null;
            }
        }

        public void DisplaySensorData()
        {
            Console.WriteLine($"--- {AreaName} Sensor Data ---");
            foreach (var sensor in Sensors)
            {
                Console.WriteLine($"{sensor.SensorType} Sensor ({sensor.Id}) - Current Value: {sensor.GetSensorValue(sensor)}");
            }
        }
    }
}
