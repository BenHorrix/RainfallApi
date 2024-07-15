namespace RainfallApi.Models.Rainfall.Responses
{
    public class RainfallReadingsSinceTimeResponse
    {
        public string StationId { get; }

        public RainfallReading[] MeasurementsSince { get; }

        public int TotalReadings
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public int MinimumMeasurement
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public int MaximumMeasurement
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public int MeanMeasurement
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public RainfallReadingsSinceTimeResponse(string stationId, RainfallReading[] measurementsSince)
        {
            StationId = stationId;
            MeasurementsSince = measurementsSince;
        }
    }
}
