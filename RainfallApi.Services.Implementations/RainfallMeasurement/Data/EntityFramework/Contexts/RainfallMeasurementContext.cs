using Microsoft.EntityFrameworkCore;
using RainfallApi.Models.Rainfall;
using RainfallApi.Services.Implementations.RainfallMeasurement.Data.EntityFramework.Models;

namespace RainfallApi.Services.Implementations.RainfallMeasurement.Data.EntityFramework.Contexts
{
    public class RainfallMeasurementContext : DbContext
    {
        public DbSet<UserRainfallReading> UserReadings { get; set; }

        public RainfallMeasurementContext(DbContextOptions<RainfallMeasurementContext> options) : base(options)
        {

        }
    }
}
