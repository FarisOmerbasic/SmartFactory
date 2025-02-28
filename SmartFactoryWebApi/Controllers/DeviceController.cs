using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using SmartFactoryBackend.Models;
using SmartFactoryWebApi.Dtos;
using SmartFactoryWebApi.Services;
using SmartFactoryWebApi;
using System.Runtime.CompilerServices;
using SmartFactoryWebApi.Models;

namespace SmartFactoryWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeviceController(IDataMinerConnection dataMinerConnection) : ControllerBase
    {

        [HttpGet("GetDeviceByName")]
        public async Task<ActionResult<SensorDto?>> GetDeviceByName([FromQuery] string deviceName, CancellationToken cancellationToken)
        {
            var result = await dataMinerConnection.GetDeviceByName(deviceName,cancellationToken);
            if(result==null) return BadRequest("No device found");

            var thresholds = ReadFromJson();

            var sensorThreshold = thresholds.FirstOrDefault(t => t.Id == result.Id);

            var sensorDto = new SensorDto
            {
                Id = result.Id,
                Name = result.Name,
                LowerBound = result.LowerBound,
                UpperBound = result.UpperBound,
                NumericValue = result.NumericValue,
                StringValue = result.StringValue,
                Unit = result.Unit,
                SimulationType = result.SimulationType,
                GrowthRatio = result.GrowthRatio,
                Group1 = result.Group1,
                Group2 = result.Group2,
                Group3 = result.Group3,
                IsActive = result.IsActive,
                UpdateInterval = result.UpdateInterval,

            };

            sensorDto.CurrentThreshold= DetermineThreshold(result.NumericValue, sensorThreshold);

            return sensorDto;
        }


        [HttpGet("GetAllDevices")]
        public async Task<ActionResult<List<SensorDto>?>> GetAllDevices(CancellationToken cancellationToken)
        {
            var sensors = await dataMinerConnection.GetAllDevices(cancellationToken);

            if (sensors == null) return BadRequest("No devices found");
            if (sensors.Count<=0) return BadRequest("No devices found");

            var thresholds = ReadFromJson();

            var sensorsDto = sensors.Select(s => new SensorDto
            {
                Id = s.Id,
                Name = s.Name,
                LowerBound = s.LowerBound,
                UpperBound = s.UpperBound,
                NumericValue = s.NumericValue,
                StringValue=s.StringValue,
                Unit=s.Unit,
                SimulationType=s.SimulationType,
                GrowthRatio=s.GrowthRatio,
                Group1=s.Group1,
                Group2=s.Group2,
                Group3=s.Group3,
                IsActive=s.IsActive,
                UpdateInterval=s.UpdateInterval,

            }).ToList();


            int criticalCount = 0;
            int warningCount = 0;
            foreach (var sensor in sensorsDto)
            {
                var threshold = thresholds.FirstOrDefault(t => t.Name == sensor.Name);
                if (threshold != null)
                {
                    sensor.CurrentThreshold = DetermineThreshold(sensor.NumericValue, threshold);

                    if (sensor.CurrentThreshold == "Critical High" || sensor.CurrentThreshold == "Critical Low")
                        if (sensor.CurrentThreshold == "Critical")
                        {
                            criticalCount++;
                        }
                    if (sensor.CurrentThreshold == "Warning High" || sensor.CurrentThreshold == "Warning Low")
                        if (sensor.CurrentThreshold == "Warning")
                        {
                            warningCount++;
                        }
                }
                else
                {
                    sensor.CurrentThreshold = "No Threshold Found";
                }
            }

            return sensorsDto;
        }

        [HttpGet("GetDevicesByRoomName")]
        public async Task<ActionResult<List<Sensor>?>> GetDevicesByRoomName([FromQuery] string roomName, CancellationToken cancellationToken)
        {
            var result = await dataMinerConnection.GetDeviceByCategoryName(roomName,cancellationToken);
            if (result == null) return BadRequest($"No devices registered in {roomName}");
            if (result.Count <= 0) return BadRequest($"No devices registered in {roomName}");

            return result;
        }


        [HttpPut("UpdateDeviceThreshold")]
        public  ActionResult UpdateThresholdValue([FromBody] UpdateThresholdDto requset)
        {

            string jsonFilePath = "C:\\Users\\Lenovo\\Source\\Repos\\SmartFactory\\SmartFactoryWebApi\\threshold.json";
            JsonFileHandler jsonHandler = new JsonFileHandler(jsonFilePath);

            int errorCode;

            jsonHandler.UpdateJson(requset);

            return Ok();
        }

        [HttpGet("GetCriticalAndWarningSensors")]
        public async Task<ActionResult<GetCriticalAndWarningSensorsResponse>> GetCriticalAndWarningSensors(CancellationToken cancellationToken)
        {

            var allSensors = await dataMinerConnection.GetAllDevices(cancellationToken);

            List<Sensor> machineSenors= allSensors.Where(s => s.Name.Contains("Machine")).ToList();

            (List<Sensor>, List<Sensor>) result = CountThresholds(machineSenors);

            var response = new GetCriticalAndWarningSensorsResponse
            {
                Critical = result.Item1,
                Warning = result.Item2,
            };

            return Ok(response);

        }

        [HttpGet("GetThresholdById/{id}")]
        public ActionResult<JsonTestResponse> GetThresholdById(int id)
        {
            var thresholds = ReadFromJson();

            var stringId=id.ToString();

            var threshold = thresholds.FirstOrDefault(t => t.Id == stringId);

            return Ok(threshold);
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

        private (List<Sensor>, List<Sensor>) CountThresholds(List<Sensor> sensors)
        {

            var thresholds = ReadFromJson();

            var machineThresholds= thresholds.Where(s => s.Name.Contains("Machine")).ToList();

            string CurrentThreshold;
            List<Sensor> critical = new List<Sensor>();
            List<Sensor> warning = new List<Sensor>();
            foreach (var sensor in sensors)
            {
                var threshold = machineThresholds.FirstOrDefault(t => t.Name == sensor.Name);
                if (threshold != null)
                {
                    CurrentThreshold = DetermineThreshold(sensor.NumericValue, threshold);

                    if (CurrentThreshold == "Critical")
                    {
                        critical.Add(sensor);
                    }
                    if (CurrentThreshold == "Warning")
                    {
                        warning.Add(sensor);
                    }
                }
            }

            return (critical, warning);
        }
    }

    public class GetCriticalAndWarningSensorsResponse
    {
        public List<Sensor> Critical { get; set; }=new List<Sensor>();
        public List<Sensor> Warning { get; set; }=new List<Sensor> ();
    }
}
