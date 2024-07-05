using RainfallApi.Models.Error;

namespace RainfallApi.Errors
{
    public static class RainfallMeasurementErrors
    {
        public static Error StationNotFound(string stationId)
        {
            return new Error([new ErrorDetail("stationId", string.Empty)], "No readings found for the specified stationId");
        }

        public static Error CountNotValid(int count, int countMin, int countMax)
        {
            return new Error([new ErrorDetail("count", $"The provided count {count} was less than {countMin} or more than {countMax}")], "An invalid count was provided");
        }
    }
}
