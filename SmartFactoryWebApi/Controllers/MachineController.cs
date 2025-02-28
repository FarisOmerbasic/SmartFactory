using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using SmartFactoryBackend.Models;
using SmartFactoryWebApi.Dtos;
using SmartFactoryWebApi.InMemoryRepositories;
using SmartFactoryWebApi.Services;
using System.Globalization;

namespace SmartFactoryWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MachineController(IDataMinerConnection dataMinerConnection): ControllerBase
    {
        [HttpGet("machineOverview")]
        public async Task<ActionResult<MachineOverviewDto>> MachineOverview(CancellationToken cancellationToken)
        {

            var result = await dataMinerConnection.GetAllDevices(cancellationToken);

            var temperatureMachines = result.Where(m => m.Group2 == "Temperature Sensor").ToList();

            var machines = temperatureMachines.Where(s => s.Name.Contains("Machine")).ToList();


            (int, int) values = CountThresholds(result);

            int critical = values.Item1;
            int warning = values.Item2;



            var machineNames = machines
                .Where(s => s.Name.Contains("Machine"))
                .Select(s =>
                {
                    int index = s.Name.IndexOf("Machine", StringComparison.OrdinalIgnoreCase);
                    if (index != -1)
                    {
                        int nextSpace = s.Name.IndexOf(' ', index + "Machine".Length);
                        return nextSpace != -1 ? s.Name.Substring(0, nextSpace) : s.Name;
                    }
                    return s.Name;
                })
                .ToList();

            List<MachineOverviewResponse> response = new List<MachineOverviewResponse>();

            for (int i = 0; i < machines.Count; i++)
            {
                response.Add(new MachineOverviewResponse
                {
                    MachineName = machineNames[i],
                    Status = "Runing",
                    UpTime = new Random().Next(70, 300),
                    Temperature = machines[i].NumericValue
                });
            }


            var machineOverview = new MachineOverviewDto
            {
                RunningMachines = result.Count,
                WarningMachinesThreshold = warning,
                CriticalMachinesThreshold = critical,
                IdleMachines = 0,
                Machines = response
            };

            return machineOverview;
        }


        private (int,int) CountThresholds(List<Sensor> sensors)
        {

            var thresholds = ReadFromJson();

            string CurrentThreshold;
            int criticalCount = 0;
            int warningCount = 0;
            foreach (var sensor in sensors)
            {
                var threshold = thresholds.FirstOrDefault(t => t.Name == sensor.Name);
                if (threshold != null)
                {
                    CurrentThreshold = DetermineThreshold(sensor.NumericValue, threshold);

                    if (CurrentThreshold == "Critical")
                    {
                        criticalCount++;
                    }
                    if (CurrentThreshold == "Warning")
                    {
                        warningCount++;
                    }
                }
            }

            return (criticalCount, warningCount);
        }



        private List<JsonTestResponse>? ReadFromJson()
        {
            string jsonFilePath = "C:\\Users\\Lenovo\\Source\\Repos\\SmartFactory\\SmartFactoryWebApi\\threshold.json";
            JsonFileHandler jsonHandler = new JsonFileHandler(jsonFilePath);
            JObject jsonData;

            try
            {
                jsonData = jsonHandler.ReadJson();
            }
            catch (Exception ex)
            {
                return new List<JsonTestResponse>();
            }



            JArray thresholdsArray = (JArray)jsonData["Thresholds"];

            List<JsonTestResponse> thresholds = thresholdsArray.ToObject<List<JsonTestResponse>>();

            return thresholds;
        }


        private static string DetermineThreshold(double value, JsonTestResponse threshold)
        {

            if (value >= threshold.NormalLowThreshold && value <= threshold.NormalHighThreshold) return "Normal";
            if (value >= threshold.WarningLowThreshold && value <= threshold.WarningHighThreshold) return "Warning";
            if (value >= threshold.CriticalLowThreshold && value >= threshold.CriticalHighThreshold) return "Critical";

            return "Normal";
        }

    }

    public class MachineOverviewResponse
    {
        public string MachineName { get; set; }
        public string Status { get; set; }
        public int UpTime { get; set; }
        public double Temperature { get; set; }

    }
}
