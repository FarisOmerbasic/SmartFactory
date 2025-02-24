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
    public class BreakRoom
    {
        public string RoomName { get; set; }
        public List<Sensor> Sensors { get; set; }

        private static readonly HttpClient client = new HttpClient();
        public BreakRoom(string roomName)
        {
            RoomName = roomName;
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

                foreach (var sensor in allSensors)
                {
                    Console.WriteLine($"--- Sensor Data for {sensor.Name} ---");
                    Console.WriteLine($"ID: {sensor.Id}");
                    Console.WriteLine($"Numeric Value: {sensor.NumericValue} {sensor.Unit}");
                    Console.WriteLine($"Lower Bound: {sensor.LowerBound}");
                    Console.WriteLine($"Upper Bound: {sensor.UpperBound}");
                    Console.WriteLine($"Active: {sensor.IsActive}");
                    Console.WriteLine($"Update Interval: {sensor.UpdateInterval} seconds");
                    Console.WriteLine($"Groups: {sensor.Group1}, {sensor.Group2}, {sensor.Group3}");
                    Console.WriteLine();
                    if (sensor.Group1 == RoomName)
                    {
                        Sensors.Add(sensor);
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
            var token = Env.GetString("TOKEN");
            return token;
        }

        public void DisplaySensorData()
        {
            Console.WriteLine($"--- {RoomName} Sensor Data ---");
            foreach (var sensor in Sensors)
            {
                Console.WriteLine($"{sensor.Name} Sensor ({sensor.Id}) - Current Value: {sensor.NumericValue}");
            }
        }
    }
}
