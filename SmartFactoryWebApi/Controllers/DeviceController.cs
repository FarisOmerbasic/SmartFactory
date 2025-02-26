using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using SmartFactoryBackend.Models;
using SmartFactoryWebApi.Dtos;
using SmartFactoryWebApi.Services;

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
                    {
                        criticalCount++;
                    }
                    if (sensor.CurrentThreshold == "Warning High" || sensor.CurrentThreshold == "Warning Low")
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



        private List<JsonTestResponse>? ReadFromJson()
        {
            string jsonFilePath = "C:\\Users\\Emin Brankovic\\Desktop\\Coding Battle\\SmartFactory\\SmartFactoryWebApi\\threshold.json";
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

            if (value <= threshold.CriticalLowThreshold) return "Critical Low";

            if (value <= threshold.NormalLowThreshold) return "Normal Low";

            if (value <= threshold.WarningLowThreshold) return "Warning Low";

            if (value >= threshold.NormalHighThreshold) return "Normal High";

            if (value >= threshold.WarningHighThreshold) return "Warning High";

            if (value >= threshold.CriticalHighThreshold) return "Critical High";

            return "Normal";
        }
    }
}
