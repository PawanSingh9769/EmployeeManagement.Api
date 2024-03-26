using Contracts;
using LoggerService;
using Microsoft.AspNetCore.Mvc;

namespace CompanyEmployees.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private ILoggerManager _loggerManager;
        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, ILoggerManager loggerManger)
        {
            _logger = logger;
            _loggerManager = loggerManger;
        }

        [HttpGet]
        public IEnumerable<string> Getlog()
        {
            _loggerManager.LogInfo("Here is info message from our values controller");
            _loggerManager.LogDebug("Here is debug message from our values controller");
            _loggerManager.LogWarn("Here is warn message from our values controller");
            _loggerManager.LogError("Here is error message from our values controller");

            return new string[] { "value1", "value2" };
        }
        /*
        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }*/
    }
}