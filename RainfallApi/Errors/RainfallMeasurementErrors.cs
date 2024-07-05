using RainfallApi.Models.Error;

namespace RainfallApi.Errors
{
    public static class RainfallMeasurementErrors
    {
        public static Error StationNotFound(string stationId)
        {
            return new Error(new ErrorDetail[] { new ErrorDetail("stationId", string.Empty) }, "No readings found for the specified stationId");
        }
    }
}
