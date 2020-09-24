using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace webApiTest.Controllers
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
        MyMapprt<Source, Destination> _myMapprt;
        public WeatherForecastController(ILogger<WeatherForecastController> logger,
            MyMapprt<Source,Destination> myMapprt)
        {
            _logger = logger;
            _myMapprt = myMapprt;
        }

        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            _logger.LogInformation("Start-get-WeatherForecast");
            _logger.LogDebug("Debugger-start");
            var src = new Source
            {
                _dtValue = DateTime.Now,
                _expValue = 0,
                _IntVal = 10,
                _StrVal = "string value",
                _tfValue = true
            };
            try
            {
                var aa = (WeatherForecast)((Object)src);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error-get-WeatherForecast");
            }
            var a = _myMapprt.ToDestination(src);
            var b = _myMapprt.ToSource(a);
            //var des = src.ToDestination();
            //des.ExpValue = 10;
            //var newsrc = des.ToSource();

            var rng = new Random();
            var ret = Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            //.Concat(new List<WeatherForecast>() { new WeatherForecast {
            //    Date = DateTime.MinValue,
            //    TemperatureC = rng.Next(-20, 55),
            //    Summary = Summaries[rng.Next(Summaries.Length)]
            //} })
            .ToArray();
            _logger.LogWarning("End-get-WeatherForecast");
            return ret;
        }

        [HttpPost]
        public IEnumerable<WeatherForecast> Post([Required, FromBody] Test test)
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
        }

        [HttpPut]
        public IEnumerable<WeatherForecast> Put([Required, FromForm] Test test)
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
        }

        public class Test
        {
            [Range(0, 10)]
            public int a { get; set; }
            [MaxLength(10)]
            public string b { get; set; }
        }
    }
}
