using System.ComponentModel.DataAnnotations;

namespace RainfallApi.Models.Rainfall.Requests
{
    public class AddReadingRequest
    {
        [Required]
        public DateTime DateMeasured { get; }

        [Required]
        public decimal AmountMeasured { get; }

        [Required]
        public string StationId { get; }
    }
}
