
namespace RainfallApi.Services.Implementations.RainfallMeasurement.DTO
{
    public class GovernmentRainfallMeasurement
    {
        public Uri @Id { get; set; }
        public DateTime DateTime { get; set; }
        public decimal Value { get; set; }
        public Uri Measure { get; set; }
    }
}
