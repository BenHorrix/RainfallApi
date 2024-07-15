using RainfallApi.Models.Rainfall;
using RainfallApi.Services.Implementations.RainfallMeasurement.DTO;
using RainfallApi.Services.Implementations.RainfallMeasurement.Exceptions;
using RainfallApi.Services.RainfallMeasurement;
using Newtonsoft.Json;
using RainfallApi.Services.RainfallMeasurement.Results;
using RainfallApi.Services.RainfallMeasurement.Data;

namespace RainfallApi.Services.Implementations.RainfallMeasurement
{
    public class GovernmentRainfallMeasurementService : IRainfallMeasurementService
    {
        private readonly IRainfallMeasurementDataService _dataService;
        private readonly HttpClient _httpClient;

        public GovernmentRainfallMeasurementService(IRainfallMeasurementDataService dataService)
        {
            _dataService = dataService;
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

        public Task<AddMeasurementResult> AddMeasurementForStation(string stationId, RainfallReading newReading)
        {
            return _dataService.AddRainfallMeasurement(stationId, newReading.AmountMeasured, newReading.DateMeasured);
        }
    }
}
