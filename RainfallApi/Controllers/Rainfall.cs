using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using RainfallApi.Models.Error;
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

        /// <summary>
        /// Get rainfall readings by station Id
        /// </summary>
        /// <remarks> 
        /// Retrieve the latest readings for the specified stationId
        /// </remarks> 
        /// <response code="200">A list of rainfall readings successfully retrieved</response>
        /// <response code="400">Invalid request</response>
        /// <response code="404">No readings found for the specified stationId</response>
        /// <response code="500">Internal server error</response>
        [HttpGet("readings")]
        [ProducesResponseType<RainfallReadingResponse>(StatusCodes.Status200OK)]
        [ProducesResponseType<Error>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<Error>(StatusCodes.Status404NotFound)]
        [ProducesResponseType<Error>(StatusCodes.Status500InternalServerError)]
        [Produces("application/json")]
        public async Task<IActionResult> Readings(string stationId, [FromQuery][Range(1,100)] int count = 10)
        {
            throw new NotImplementedException();
        }
    }
}
