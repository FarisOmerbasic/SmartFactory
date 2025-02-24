using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;
using DotNetEnv;
using SmartFactoryBackend.Sensors;

namespace SmartFactoryBackend.Models
{
    public class WarehouseRoom
    {
        public string RoomName { get; set; }
        public List<Sensor> Sensors { get; set; }

        private static readonly HttpClient client = new HttpClient();

        public WarehouseRoom(string roomName)
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

                var roomSensors = allSensors.Where(sensor => sensor.Group1 == RoomName).ToList();

                if (!roomSensors.Any())
                {
                    Console.WriteLine($"No sensors found for {RoomName}.");
                    return;
                }

                foreach (var sensor in roomSensors)
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
                case "Temperature Sensor":
                    return new TemperatureSensor(sensor.Id) { Temperature = sensor.NumericValue };
                case "Humidity Sensor":
                    return new HumiditySensor(sensor.Id) { Humidity = sensor.NumericValue };
                case "Inventory Sensor":
                    return new InventorySensor(sensor.Id) { InventoryCount = (int)sensor.NumericValue };
                case "Door Sensor":
                    return new DoorSensor(sensor.Id) { IsDoorOpen = sensor.NumericValue > 0 };
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

        public void AnalyzeWarehousePerformance()
        {
            Console.WriteLine($"--- {RoomName} Performance Analysis ---");
            foreach (var sensor in Sensors)
            {
                if (sensor is TemperatureSensor temperatureSensor)
                {
                    Console.WriteLine($"Temperature: {temperatureSensor.Temperature} °C");
                }
                else if (sensor is HumiditySensor humiditySensor)
                {
                    Console.WriteLine($"Humidity: {humiditySensor.Humidity} %");
                }
                else if (sensor is InventorySensor inventorySensor)
                {
                    Console.WriteLine($"Inventory Count: {inventorySensor.InventoryCount} items");
                }
                else if (sensor is DoorSensor doorSensor)
                {
                    Console.WriteLine($"Door Status: {(doorSensor.IsDoorOpen ? "Open" : "Closed")}");
                }
            }
        }
    }
}






