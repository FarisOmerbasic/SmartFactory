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
    public class ManufacturingArea
    {
        public string AreaName { get; set; }
        public List<Sensor> Sensors { get; private set; }

        private static readonly HttpClient client = new HttpClient();

        public ManufacturingArea(string areaName)
        {
            AreaName = areaName;
            Sensors = new List<Sensor>();
        }


        //public async Task FetchSensorData()
        //{
        //    try
        //    {
        //        string apiUrl = "https://slb-skyline.on.dataminer.services/api/custom/IndustrySimulator/getAllDevices";
        //        string token = GetApiToken();

        //        if (string.IsNullOrEmpty(token))
        //        {
        //            Console.WriteLine("Error: API token is missing.");
        //            return;
        //        }

        //        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        //        HttpResponseMessage response = await client.GetAsync(apiUrl);
        //        response.EnsureSuccessStatusCode();

        //        string responseBody = await response.Content.ReadAsStringAsync();
        //        Console.WriteLine($"Raw API Response:\n{responseBody}\n");

        //        List<Sensor> allSensors = JsonSerializer.Deserialize<List<Sensor>>(responseBody);

        //        var areaSensors = allSensors.Where(sensor => sensor.Group1 == AreaName).ToList();

        //        if (!areaSensors.Any())
        //        {
        //            Console.WriteLine($"No sensors found for {AreaName}.");
        //            return;
        //        }

        //        foreach (var sensor in areaSensors)
        //        {
        //            Sensor specificSensor = CreateSpecificSensor(sensor);
        //            if (specificSensor != null)
        //            {
        //                Sensors.Add(specificSensor);
        //                Console.WriteLine($"Added {sensor.Name} sensor to {AreaName}");
        //            }
        //        }

        //        Console.WriteLine($"Sensor data updated for {AreaName}.");
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine($"Error fetching sensor data: {ex.Message}");
        //    }
        //}

        private static string GetApiToken()
        {
            return Env.GetString("TOKEN");
        }


        //private Sensor CreateSpecificSensor(Sensor sensor)
        //{
        //    switch (sensor.Name)
        //    {
        //        case "Machine Status":
        //            return new MachineStatusSensor(sensor.Id);
        //        case "Vibration Sensor":
        //            return new VibrationSensor(sensor.Id);
        //        case "Temperature Sensor":
        //            return new TemperatureSensor(sensor.Id);
        //        case "Pressure Sensor":
        //            return new PressureSensor(sensor.Id);
        //        case "Energy Meter":
        //            return new EnergyMeterSensor(sensor.Id);
        //        case "Air Quality Sensor":
        //            return new AirQualitySensor(sensor.Id);
        //        default:
        //            return null;
        //    }
        //}

        //public void DisplaySensorData()
        //{
        //    Console.WriteLine($"--- {AreaName} Sensor Data ---");
        //    foreach (var sensor in Sensors)
        //    {
        //        Console.WriteLine($"{sensor.Name} ({sensor.Id}) - Value: {sensor.GetSensorValue(sensor)}");
        //    }
        //}


        public void AnalyzeFactoryPerformance()
        {
            Console.WriteLine($"--- {AreaName} Performance Analysis ---");

            foreach (var sensor in Sensors)
            {
         
                Console.WriteLine($"{sensor.Name}: {sensor.NumericValue}");

         
                string alertLevel = sensor.GetAlertLevel();
                Console.WriteLine($"{sensor.Name} Alert Level: {alertLevel}");
            }
        }
    }
    }

