using RainfallApi.Services.Implementations.RainfallMeasurement;
using System.ComponentModel;
using Moq;
using RainfallApi.Services.RainfallMeasurement.Data;

namespace RainfallApi.Services.Implementations.Tests
{
    public class GovernmentRainfallMeasurementServiceTests
    {
        [Fact]
        [Category("Integration")]
        public async void RainfallMeasurementService_KnownStationWithId_ReturnsResults()
        {
            // Arrange
            var stationIdWithKnownValues = "3680";
            var mockDataService = new Mock<IRainfallMeasurementDataService>();
            var sut = new GovernmentRainfallMeasurementService(mockDataService.Object);
            var expectedCount = 10;

            // Act
            var result = await sut.GetMeasurementsForStation(stationIdWithKnownValues, expectedCount);

            // Assert
            Assert.Equal(result.Length, expectedCount);
            Assert.True(result.All(r => r.DateMeasured != default), "Some readings in the returned collection had a value equal to the default value of date time, suggesting a deserialisation error");
        }

        /// <summary>
        /// Note that this indicates a problem in the government service, although we now rely on its behaviour - it returns an empty result set instead of a 404
        /// </summary>
        [Fact]
        [Category("Integration")]
        public async void RainfallMeasurementService_KnownUnknownId_ReturnsNoResults()
        {
            // Arrange
            var stationIdWithKnownValues = "invalidStationId";
            var mockDataService = new Mock<IRainfallMeasurementDataService>();
            var sut = new GovernmentRainfallMeasurementService(mockDataService.Object);
            var expectedCount = 0;

            // Act
            var result = await sut.GetMeasurementsForStation(stationIdWithKnownValues, expectedCount);

            // Assert
            Assert.Equal(result.Length, expectedCount);
        }
    }
}