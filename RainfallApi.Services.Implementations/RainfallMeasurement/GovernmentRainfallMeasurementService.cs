using RainfallApi.Models.Rainfall;
using RainfallApi.Services.Implementations.RainfallMeasurement.DTO;
using RainfallApi.Services.Implementations.RainfallMeasurement.Exceptions;
using RainfallApi.Services.RainfallMeasurement;
using Newtonsoft.Json;

namespace RainfallApi.Services.Implementations.RainfallMeasurement
{
    public class GovernmentRainfallMeasurementService : IRainfallMeasurementService
    {
        private readonly HttpClient _httpClient;

        public GovernmentRainfallMeasurementService()
        {
            _httpClient = new HttpClient();
        }

        public async Task<RainfallReading[]> GetMeasurementsForStation(string stationId, int count)
        {
            var readingsRestUrl = $"https://environment.data.gov.uk/flood-monitoring/id/stations/{stationId}/readings";
            var result = await _httpClient.GetAsync(readingsRestUrl);
            var resultBody = await result.Content.ReadAsStringAsync();
            var deserializedResult = JsonConvert.DeserializeObject<GovernmentRainfallMeasureResponse>(resultBody);
            if(deserializedResult == null)
            {
                throw new UnexpectedGovernmentRainfallDataFormatException(resultBody, typeof(GovernmentRainfallMeasurement));
            }
            return deserializedResult.Items.Select(i => new RainfallReading(i.DateTime, i.Value, RainfallReading.Source.Government)).Take(count).ToArray();
        }

        public Task AddMeasurementForStation(string stationId, RainfallReading newReading)
        {
            throw new NotImplementedException();
        }
    }
}
