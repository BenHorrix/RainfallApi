using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace RainfallApi.Models.Rainfall
{
    public class RainfallReadingsRequest
    {
        public const int CountMin = 1;
        public const int CountMax = 100;

        [DefaultValue("3860")]
        public string StationId { get; set; }

        [FromQuery]
        [Range(CountMin, CountMax)]
        [DefaultValue(1)]
        public int Count { get; set; }
    }
}
