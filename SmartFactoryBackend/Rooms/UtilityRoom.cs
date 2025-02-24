using DotNetEnv;
using SmartFactoryBackend.Sensors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace SmartFactoryBackend.Rooms
{
    public class UtilityRoom
    {
        public string RoomName { get; set; }
        public List<Sensor> Sensors { get; private set; }
        private static readonly HttpClient client = new HttpClient();

        public UtilityRoom(string roomName)
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
                    Console.WriteLine("API token is missing.");
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
                case "Water Flow Sensor":
                    return new WaterFlowSensor(sensor.Id) { FlowRate = sensor.NumericValue };
                case "Gas Leak Sensor":
                    return new GasLeakSensor(sensor.Id) { GasConcentration = sensor.NumericValue };
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

        public void AnalyzeUtilityPerformance()
        {
            Console.WriteLine($"--- {RoomName} Utility Performance Analysis ---");
            foreach (var sensor in Sensors)
            {
                if (sensor is EnergyMeterSensor energyMeter)
                {
                    Console.WriteLine($"Energy Usage: {energyMeter.EnergyUsage} kW");
                }
                else if (sensor is WaterFlowSensor waterSensor)
                {
                    Console.WriteLine($"Water Flow Rate: {waterSensor.FlowRate} L");
                }
                else if (sensor is GasLeakSensor gasSensor)
                {
                    Console.WriteLine($"Gas Concentration: {gasSensor.GasConcentration} ppm");
                }
            }
        }

        public void CheckAlerts()
        {
            Console.WriteLine($"--- {RoomName} Alert System ---");
            foreach (var sensor in Sensors)
            {
                if (sensor is GasLeakSensor gasSensor && gasSensor.GasConcentration > 5.0)
                {
                    Console.WriteLine($" ALERT: High gas concentration detected ({gasSensor.GasConcentration} ppm)");
                }
                else if (sensor is EnergyMeterSensor energySensor && energySensor.EnergyUsage > 80)
                {
                    Console.WriteLine($" ALERT: High energy consumption ({energySensor.EnergyUsage} kW)");
                }
            }
        }

        public void EmergencyShutdown()
        {
            Console.WriteLine($"Emergency Shutdown Initiated for {RoomName}");

            foreach (var sensor in Sensors)
            {
                if (sensor is GasLeakSensor gasSensor && gasSensor.GasConcentration > 8.0)
                {
                    Console.WriteLine("Shutting down gas supply due to critical leak");
                }
                else if (sensor is EnergyMeterSensor energySensor && energySensor.EnergyUsage > 90)
                {
                    Console.WriteLine("Shutting down power grid to prevent overload");
                }
            }
        }
    }
}
}
