﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace WebApplication1.Controllers
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
        public string Get()
        {
            //var rng = new Random();
            //return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            //{
            //    Date = DateTime.Now.AddDays(index),
            //    TemperatureC = rng.Next(-20, 55),
            //    Summary = Summaries[rng.Next(Summaries.Length)]
            //})
            //.ToArray();
            return $"команди: \n" +
                $" 1) GET знати статтю в вікі по слову https://localhost:44338/api/Test/слово_пошуку \n" +
                $" 2) GET отримати ссилку на статтю по Id https://localhost:44338/api/Test/nomer/id \n" +
                $" 3) GET отримати історію пошуку: https://localhost:44338/api/Test/hisory/id_чата \n" +
                $" 4) POST: добавити запис в історію https://localhost:44338/api/Test \n" +
                $" 5) DELETE: видалити запис з історії (видаляємо запис id з чату id2)  https://localhost:44338/api/ApiWithActions/id/id2";
        }
    }
}