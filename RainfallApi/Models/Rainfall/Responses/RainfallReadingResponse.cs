namespace RainfallApi.Models.Rainfall.Responses
{
    public class RainfallReadingResponse
    {
        public RainfallReading[] Readings { get; }

        public RainfallReadingResponse(RainfallReading[] readings)
        {
            Readings = readings;
        }
    }
}
