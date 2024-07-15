using System.ComponentModel.DataAnnotations;

namespace RainfallApi.Models.Rainfall.Requests
{
    public class AddReadingRequest
    {
        [Required]
        public DateTime? DateMeasured { get; set; }

        [Required]
        public decimal? AmountMeasured { get; set; }

        [Required]
        public string? StationId { get; set; }

        public AddReadingRequest(DateTime dateMeasured, decimal amountMeasured, string stationId)
        {
            DateMeasured = dateMeasured;
            AmountMeasured = amountMeasured;
            StationId = stationId;
        }

        public AddReadingRequest()
        {

        }
    }
}
