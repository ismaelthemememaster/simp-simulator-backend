using Microsoft.AspNetCore.Mvc;
using simp_simulator_models;

namespace simp_simulator_backend.Controllers
{
    /// <summary>
    /// Test.
    /// </summary>
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

        /// <summary>
        /// Test comment.
        /// </summary>
        /// <returns>Returns a test object.</returns>
        /// <remarks>
        /// This is a test:
        ///     get tested bro!!!
        ///     wait, that didn't sound like a reference to the get beaned meme
        ///     Anyway, this endpoint is actually a test, so pay no atention to this program
        /// </remarks>
        /// <response code="200">Returns the newly created NOTHING</response>
        /// <response code="400">If this program breaks, which (since I programmed it) is very likely</response>
        [HttpGet(Name = "GetWeatherForecast")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IEnumerable<WeatherForecast> Get()
        {
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }
    }
}