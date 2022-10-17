using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MozaeekCore.Persistense.MongoDb;
using MozaeekCore.QueryModel;

namespace MozaeekCore.ReadConsistencyService.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IPointQueryService _labelQueryService;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IPointQueryService labelQueryService)
        {
            _logger = logger;
            _labelQueryService = labelQueryService;
        }

        [HttpGet]
        public List<WeatherForecast> Get()
        {
            //await _labelQueryService.Create(new LabelQuery()
            //{
            //    Id = 1,
            //    Title = "test"
            //});

            //var test = await _labelQueryService.Get();

            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToList();
            
        }
        
        [HttpGet("{id}")]
        public string GetById(int id)
        {
            return $"{id}";
        }
    }
}
