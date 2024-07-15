using RainfallApi.Models.Rainfall;

namespace RainfallApi.Services.RainfallMeasurement
{
    /// <summary>
    /// A service used to get rainfall measurements from an external source, such as government reading stations
    /// </summary>
    public interface IRainfallMeasurementService
    {
        Task<RainfallReading[]> GetMeasurementsForStation(string stationId, int count);

        /// <summary>
        /// Gets the number of rainfall readings in "sinceHours" before the current date time
        /// </summary>
        /// <param name="sinceHours">The number of hours prior to the present time to retrieve readings for</param>
        Task<RainfallReading[]> GetRainfallReadingsSince(int sinceHours);
    }
}
