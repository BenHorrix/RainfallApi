using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using RainfallApi.Models.Rainfall.Responses;

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
        public RainfallReadingResponse Readings(string stationId, [FromQuery][Range(1,100)] int count = 10)
        {
            throw new NotImplementedException();
        }
    }
}
