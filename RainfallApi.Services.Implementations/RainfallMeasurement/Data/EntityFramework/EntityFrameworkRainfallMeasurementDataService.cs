using RainfallApi.Services.Implementations.RainfallMeasurement.Data.EntityFramework.Contexts;
using RainfallApi.Services.Implementations.RainfallMeasurement.Data.EntityFramework.Models;
using RainfallApi.Services.RainfallMeasurement.Data;
using RainfallApi.Services.RainfallMeasurement.Results;

namespace RainfallApi.Services.Implementations.RainfallMeasurement.Data.EntityFramework
{
    public class EntityFrameworkRainfallMeasurementDataService : IRainfallMeasurementDataService
    {
        private readonly RainfallMeasurementContext _dbContext;

        public EntityFrameworkRainfallMeasurementDataService(RainfallMeasurementContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<AddMeasurementResult> AddRainfallMeasurement(string stationId, decimal amountMeasured, DateTime dateMeasured)
        {
            var model = new UserRainfallReading(stationId, dateMeasured, amountMeasured);
            await _dbContext.UserReadings.AddAsync(model);
            await _dbContext.SaveChangesAsync();

            return new AddMeasurementResult(model.Key);
        }
    }
}
