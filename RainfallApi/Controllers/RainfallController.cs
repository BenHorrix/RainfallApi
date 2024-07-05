using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using RainfallApi.Errors;
using RainfallApi.Models.Error;
using RainfallApi.Models.Rainfall.Responses;
using RainfallApi.Services.RainfallMeasurement;

namespace RainfallApi.Controllers
{
    [ApiController]
    [Route("rainfall/id/{stationId}")]
    public class RainfallController : ControllerBase
    {
        private readonly ILogger<RainfallController> _logger;
        private readonly IRainfallMeasurementService _rainfallMeasurementService;
        public const int CountMin = 1;
        public const int CountMax = 100;

        public RainfallController(ILogger<RainfallController> logger, IRainfallMeasurementService rainfallMeasurementService)
        {
            _logger = logger;
            _rainfallMeasurementService = rainfallMeasurementService;
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
        public async Task<IActionResult> Readings(string stationId, [FromQuery][Range(CountMin,CountMax)] int count = 10)
        {
            try
            {
                if (count < CountMin || count > CountMax)
                {
                    return new BadRequestObjectResult(RainfallMeasurementErrors.CountNotValid(count, CountMin, CountMax));
                }
                var result = await _rainfallMeasurementService.GetMeasurementsForStation(stationId, count);
                if (!result.Any())
                {
                    return new NotFoundObjectResult(RainfallMeasurementErrors.StationNotFound(stationId));
                }
                return new OkObjectResult(new RainfallReadingResponse(result));
            }
            catch(Exception ex)
            {
                var result = new ObjectResult(GenericErrors.UnexpectedError(ex.Message))
                {
                    StatusCode = 500
                };
                return result;
            }
        }
    }
}
