using Castle.Core.Logging;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using RainfallApi.Controllers;
using RainfallApi.Errors;
using RainfallApi.Models.Error;
using RainfallApi.Models.Rainfall;
using RainfallApi.Models.Rainfall.Responses;
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
            Assert.IsAssignableFrom<BadRequestObjectResult>(result);
            Assert.Equivalent(((BadRequestObjectResult)result).Value, Errors.RainfallMeasurementErrors.CountNotValid(countToTry, RainfallController.CountMin, RainfallController.CountMax));
        }

        [Fact]
        public async void RainfallController_ThrowsErrorDuringAPICall_Gives500()
        {
            // Arrange
            var expectedErrorMessage = "I represent any exception that could be thrown";
            _mockMeasurementService.Setup(m => m.GetMeasurementsForStation(It.IsAny<string>())).ThrowsAsync(new Exception(expectedErrorMessage));
            var sut = new RainfallController(_mockLogger.Object, _mockMeasurementService.Object);

            // Act
            var result = await sut.Readings("1", 50);

            // Assert
            Assert.IsAssignableFrom<ObjectResult>(result);
            Assert.Equal(((ObjectResult)result).StatusCode, 500);
            Assert.Equivalent(((ObjectResult)result).Value, GenericErrors.UnexpectedError(expectedErrorMessage));
        }

        [Fact]
        public async void RainfallController_HasResultsForStation_GivesOkResultAndExpectedData()
        {
            // Arrange
            var expectedResultsForStation = new RainfallReading[] { new RainfallReading(DateTime.Now, 1.0m) };
            var mockStationId = "1";
            _mockMeasurementService.Setup(m => m.GetMeasurementsForStation(mockStationId)).Returns(Task.FromResult(expectedResultsForStation));
            var sut = new RainfallController(_mockLogger.Object, _mockMeasurementService.Object);

            // Act
            var result = await sut.Readings(mockStationId, 50);

            // Assert
            Assert.IsAssignableFrom<OkObjectResult>(result);
            Assert.Equivalent(((OkObjectResult)result).Value, new RainfallReadingResponse(expectedResultsForStation));
        }
    }
}