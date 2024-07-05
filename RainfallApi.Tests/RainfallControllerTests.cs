using Castle.Core.Logging;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using RainfallApi.Controllers;
using RainfallApi.Models.Error;
using RainfallApi.Models.Rainfall;
using RainfallApi.Services.RainfallMeasurement;

namespace RainfallApi.Tests
{
    public class RainfallControllerTests
    {
        private readonly Mock<IRainfallMeasurementService> _mockMeasurementService;
        private readonly Mock<ILogger<RainfallController>> _mockLogger;

        public RainfallControllerTests()
        {
            _mockMeasurementService = new Mock<IRainfallMeasurementService>();
            _mockLogger = new Mock<ILogger<RainfallController>>();
        }

        [Fact]
        public async void RainfallController_ReadingStationDoesNotExist_Gives404()
        {
            // Arrange
            _mockMeasurementService.Setup(m => m.GetMeasurementsForStation(It.IsAny<string>())).Returns(Task.FromResult(Array.Empty<RainfallReading>()));
            var sut = new RainfallController(_mockLogger.Object, _mockMeasurementService.Object);
            var mockStationId = "1";

            // Act
            var result = await sut.Readings(mockStationId);

            // Assert
            Assert.IsAssignableFrom<NotFoundObjectResult>(result);
            Assert.Equivalent(((NotFoundObjectResult)result).Value, Errors.RainfallMeasurementErrors.StationNotFound(mockStationId));
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(101)]
        public async void RainfallController_CountIsOutOfRange_Gives400(int countToTry)
        {
            // Arrange
            var sut = new RainfallController(_mockLogger.Object, _mockMeasurementService.Object);

            // Act
            var result = await sut.Readings("1", countToTry);

            // Assert
            Assert.IsAssignableFrom<BadRequestResult>(result);
        }
    }
}