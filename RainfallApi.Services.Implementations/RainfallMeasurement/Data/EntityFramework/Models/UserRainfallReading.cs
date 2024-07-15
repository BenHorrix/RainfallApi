using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RainfallApi.Models.Rainfall;

namespace RainfallApi.Services.Implementations.RainfallMeasurement.Data.EntityFramework.Models
{
    public class UserRainfallReading
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Key { get; set; }

        [Required]
        public DateTime DateMeasured { get; set; }

        [Required]
        public decimal AmountMeasured { get; set; }

        [Required]
        public string StationId { get; set; }

        public UserRainfallReading(string stationId, DateTime dateMeasured, decimal amountMeasured)
        {
            DateMeasured = dateMeasured;
            AmountMeasured = amountMeasured;
            StationId = stationId;
        }

        public UserRainfallReading() { }
    }
}
