using Microsoft.AspNetCore.Mvc;
using SmartFactoryWebApi.Services;

namespace SmartFactoryWebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IDataMinerConnection _dataMinerConnection;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IDataMinerConnection dataMinerConnection)
        {
            _logger = logger;
            _dataMinerConnection = dataMinerConnection;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }


        [HttpGet("GetAllCategories")]
        public async Task<ActionResult<string?>> GetAllCategories()
        {
            var result = await _dataMinerConnection.GetAllCategories();


            return result;
        }

        [HttpGet("GetDeviceByName")]
        public async Task<ActionResult<string?>> GetDeviceByName([FromQuery]string deviceName)
        {
            var result = await _dataMinerConnection.GetDeviceByName(deviceName);

            return result;
        }

        [HttpGet("GetDeviceByCategoryName")]
        public async Task<ActionResult<string?>> GetDeviceByCategoryName([FromQuery] string categoryName)
        {
            var result = await _dataMinerConnection.GetDeviceByCategoryName(categoryName);

            return result;
        }


    }
}
