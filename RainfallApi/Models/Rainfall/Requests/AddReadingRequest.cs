using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace RainfallApi.Models.Rainfall.Requests
{
    public class AddReadingRequest
    {
        [Required]
        public DateTime? DateMeasured { get; set; }

        [Required]
        public decimal? AmountMeasured { get; set; }


        public AddReadingRequest(DateTime dateMeasured, decimal amountMeasured)
        {
            DateMeasured = dateMeasured;
            AmountMeasured = amountMeasured;
        }

        public AddReadingRequest()
        {

        }
    }
}
