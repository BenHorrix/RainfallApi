using Microsoft.AspNetCore.Mvc;

namespace RainfallApi.Controllers
{
    [ApiController]
    [Route("rainfall/id/{stationId}")]
    public class Rainfall : ControllerBase
    {
        private readonly ILogger<Rainfall> _logger;

        public Rainfall(ILogger<Rainfall> logger)
        {
            _logger = logger;
        }

        [HttpGet("readings")]
        public IEnumerable<WeatherForecast> Readings(string stationId)
        {
            throw new NotImplementedException();
        }
    }
}
