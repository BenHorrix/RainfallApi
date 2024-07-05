using RainfallApi.Models.Rainfall;
using RainfallApi.Services.RainfallMeasurement;

namespace RainfallApi.Services.Implementations.RainfallMeasurement
{
    public class GovernmentRainfallMeasurementService : IRainfallMeasurementService
    {
        public async Task<RainfallReading[]> GetMeasurementsForStation(int stationId)
        {
            throw new NotImplementedException();
        }
    }
}
