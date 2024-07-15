using RainfallApi.Models.Rainfall;
using RainfallApi.Services.RainfallMeasurement.Results;

namespace RainfallApi.Services.RainfallMeasurement
{
    /// <summary>
    /// A service used to get rainfall measurements from an external source, such as government reading stations
    /// </summary>
    public interface IRainfallMeasurementService
    {
        Task<RainfallReading[]> GetMeasurementsForStation(string stationId, int count);

        Task<AddMeasurementResult> AddMeasurementForStation(string stationId, RainfallReading newReading);
    }
}
