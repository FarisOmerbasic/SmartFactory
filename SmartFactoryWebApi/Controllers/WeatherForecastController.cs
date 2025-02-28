using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using SmartFactoryWebApi.Dtos;
using SmartFactoryWebApi.Models;
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
        private readonly IRenderPDFReport _renderPDF;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IRenderPDFReport renderPDF)
        {
            _logger = logger;
            _renderPDF = renderPDF;
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


        [HttpGet("TestJson")]
        public ActionResult<List<JsonTestResponse>> Test()
        {
            string jsonFilePath = "C:\\Users\\Emin Brankovic\\Desktop\\Coding Battle\\SmartFactory\\SmartFactoryWebApi\\threshold.json";
            JsonFileHandler jsonHandler = new JsonFileHandler(jsonFilePath);
            JObject jsonData;

            try
            {
                jsonData= jsonHandler.ReadJson();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }



            JArray thresholdsArray = (JArray)jsonData["Thresholds"];

            List<JsonTestResponse> thresholds = thresholdsArray.ToObject<List<JsonTestResponse>>();

            return Ok(thresholds);

        }

        //[HttpGet("RenderPDF")]
        //public ActionResult RenderPDF()
        //{
        //    //var userDto = new User
        //    //{
        //    //    FirstName = "Emin",
        //    //    LastName = "Brankovic",
        //    //    Email = "emin@gmail.com",
        //    //    Role = UserRoles.SuperUser
        //    //};


        //    //_renderPDF.RenderUserRegisterReport(userDto);

        //    //return Ok();

        //}
    }

    public class JsonTestResponse
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public double LowerBound { get; set; }
        public double UpperBound { get; set; }
        public double CriticalLowThreshold { get; set; }
        public double NormalLowThreshold { get; set; }
        public double NormalHighThreshold { get; set; }
        public double CriticalHighThreshold { get; set; }
        public double WarningLowThreshold { get; set; }
        public double WarningHighThreshold { get; set;}
    }
}
