using RainfallApi.Services.RainfallMeasurement.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RainfallApi.Services.RainfallMeasurement.Data
{
    public interface IRainfallMeasurementDataService
    {
        Task<AddMeasurementResult> AddRainfallMeasurement(string stationId, decimal amountMeasured, DateTime dateMeasured);
    }
}
