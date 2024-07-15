namespace RainfallApi.Services.RainfallMeasurement.Results
{
    public class AddMeasurementResult
    {
        public AddMeasurementResult(int addedReadingId)
        {
            AddedReadingId = addedReadingId;
        }

        public int AddedReadingId { get; }
    }
}
