using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProblemDetailsDemoWebApi.Controllers
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

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            throw new Exception("An Unhandled Exception");
            throw new StormyWeatherException("/weatherforecast", 101);
            var problem = new StormyWeatherProblemDetails
            {
                Type = "https://awesome-weather-forecasts.com/we-got-problems/storm-destruction",
                Title = "Storm destroyed weather sensors.",
                Detail = "Unable to retrieve latest weather forecast due to loss of hardware.",
                Instance = "/weather-problem/987645/storm/hardware-loss",
                LastReportedWindGust = 98,
            };

            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
        }
    }

    public class StormyWeatherProblemDetails : ProblemDetails
    {
        public int LastReportedWindGust { get; set; }
    }

    public class ProblemDetailsException : Exception
    {
        public string Type { get; set; }
        public string Detail { get; set; }
        public string Title { get; set; }
        public string Instance { get; set; }
    }

    public class StormyWeatherException : ProblemDetailsException
    {
        public int LastReportedWindGust { get; set; }
        public StormyWeatherException(string instance, int lastReportedWindGust)
        {
            Type = "https://awesome-weather-forecasts.com/we-got-problems/storm-destruction";
            Title = "Storm destroyed weather sensors.";
            Detail = "Unable to retrieve latest weather forecast due to loss of hardware.";
            //Instance = "/weather-problem/987645/storm/hardware-loss";
            LastReportedWindGust = lastReportedWindGust;
            Instance = instance;
        }
    }
}
