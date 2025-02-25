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
    public class Room
    {
        public string RoomName { get; set; }
        public List<Sensor> Sensors { get; set; }
        private static readonly HttpClient client = new HttpClient();

        public Room(string roomName)
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
                case "Energy Meter":
                    return new EnergyMeterSensor(sensor.Id) { EnergyUsage = sensor.NumericValue };
                case "Production Line Sensor":
                    return new ProductionLineSensor(sensor.Id) { Throughput = sensor.NumericValue };
                case "Security Camera":
                case "Motion Sensor":
                    return new SecuritySensor(sensor.Id) { IsMotionDetected = sensor.NumericValue > 0 };
                case "Temperature Sensor":
                    return new TemperatureSensor(sensor.Id) { CurrentTemperature = sensor.NumericValue };
                case "Humidity Sensor":
                    return new HumiditySensor(sensor.Id) { CurrentHumidity = sensor.NumericValue };
                case "Weight Sensor":
                    return new WeightSensor(sensor.Id) { Weight = sensor.NumericValue };
                case "Gas Leak Sensor":
                    return new GasLeakSensor(sensor.Id) { GasConcentration = sensor.NumericValue };
                case "Air Quality Sensor":
                    return new AirQualitySensor(sensor.Id) { AirQualityIndex = sensor.NumericValue };
                default:
                    return sensor; // Return as is if no specific subclass exists
            }
        }

        public void DisplaySensorData()
        {
            Console.WriteLine($"--- {RoomName} Sensor Data ---");
            foreach (var sensor in Sensors)
            {
                Console.WriteLine($"{sensor.Name} Sensor ({sensor.Id}) - Current Value: {sensor.NumericValue}");
            }
        }

        public void AnalyzeRoomPerformance()
        {
            Console.WriteLine($"--- {RoomName} Performance Analysis ---");
            foreach (var sensor in Sensors)
            {
                switch (sensor)
                {
                    case EnergyMeterSensor energyMeter:
                        Console.WriteLine($"Energy Usage: {energyMeter.EnergyUsage} kW");
                        break;
                    case ProductionLineSensor productionSensor:
                        Console.WriteLine($"Production Throughput: {productionSensor.Throughput} units/hr");
                        break;
                    case SecuritySensor securitySensor:
                        Console.WriteLine($"Security Status: {(securitySensor.IsMotionDetected ? "Motion Detected" : "No Motion")}");
                        break;
                    case GasLeakSensor gasSensor when gasSensor.GasConcentration > 5.0:
                        Console.WriteLine($"ALERT: High gas concentration detected ({gasSensor.GasConcentration} ppm)");
                        break;
                    case AirQualitySensor airSensor when airSensor.AirQualityIndex > 150:
                        Console.WriteLine("ALERT: Poor air quality detected!");
                        break;
                }
            }
        }
    }
}
