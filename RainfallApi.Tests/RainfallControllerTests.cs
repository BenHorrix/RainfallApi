using Castle.Core.Logging;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using RainfallApi.Controllers;
using RainfallApi.Errors;
using RainfallApi.Models.Error;
using RainfallApi.Models.Rainfall;
using RainfallApi.Models.Rainfall.Requests;
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
            _mockMeasurementService.Setup(m => m.GetMeasurementsForStation(It.IsAny<string>(), It.IsAny<int>())).Returns(Task.FromResult(Array.Empty<RainfallReading>()));
            var sut = new RainfallController(_mockLogger.Object, _mockMeasurementService.Object);
            var mockStationId = "1";

            // Act
            var result = await sut.Readings(new RainfallReadingsRequest(mockStationId, 1));

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
            var result = await sut.Readings(new RainfallReadingsRequest("1", countToTry));

            // Assert
            Assert.IsAssignableFrom<BadRequestObjectResult>(result);
            Assert.Equivalent(((BadRequestObjectResult)result).Value, Errors.RainfallMeasurementErrors.CountNotValid(countToTry, RainfallReadingsRequest.CountMin, RainfallReadingsRequest.CountMax));
        }

        [Fact]
        public async void RainfallController_ThrowsErrorDuringAPICall_Gives500()
        {
            // Arrange
            var expectedErrorMessage = "I represent any exception that could be thrown";
            _mockMeasurementService.Setup(m => m.GetMeasurementsForStation(It.IsAny<string>(), It.IsAny<int>())).ThrowsAsync(new Exception(expectedErrorMessage));
            var sut = new RainfallController(_mockLogger.Object, _mockMeasurementService.Object);

            // Act
            var result = await sut.Readings(new RainfallReadingsRequest("1", 50));

            // Assert
            Assert.IsAssignableFrom<ObjectResult>(result);
            Assert.Equal(((ObjectResult)result).StatusCode, 500);
            Assert.Equivalent(((ObjectResult)result).Value, GenericErrors.UnexpectedError(expectedErrorMessage));
        }

        [Theory]
        [InlineData(1)]
        [InlineData(99)]
        public async void RainfallController_HasResultsForStation_GivesOkResultAndExpectedData(int resultCount)
        {
            // Arrange
            var expectedResultsForStation = new List<RainfallReading>();
            var expectedValueSource = RainfallReading.Source.Government;
            for (var i = 0; i < resultCount; i++)
            {
                expectedResultsForStation.Add(new RainfallReading(DateTime.Now.AddDays(-1 * i), i, expectedValueSource));
            }
            var expectedResultForStation = expectedResultsForStation.ToArray();
            var mockStationId = "1";
            _mockMeasurementService.Setup(m => m.GetMeasurementsForStation(mockStationId, resultCount)).Returns(Task.FromResult(expectedResultForStation));
            var sut = new RainfallController(_mockLogger.Object, _mockMeasurementService.Object);

            // Act
            var result = await sut.Readings(new RainfallReadingsRequest(mockStationId, resultCount));
            var resultAsObjectResult = result as OkObjectResult;

            // Assert
            Assert.NotNull(resultAsObjectResult);
            Assert.Equivalent(resultAsObjectResult.Value, new RainfallReadingResponse(expectedResultForStation));
            var rainfallReadings = ((RainfallReadingResponse)resultAsObjectResult.Value).Readings;
            Assert.Equal(resultCount, rainfallReadings.Length);
            Assert.All(rainfallReadings, reading => Assert.Equal(RainfallReading.Source.Government, reading.ValueSource));
        }

        [Fact]
        public async void RainfallController_AddUserReading_ReturnsCreatedAtResult()
        {
            // Arrange
            var mockStationId = "1";
            var mockRequest = new AddReadingRequest(DateTime.UtcNow, 1.0m);
            var mockNewId = 42;
            _mockMeasurementService.Setup(m => m.AddMeasurementForStation(mockStationId, It.IsAny<RainfallReading>())).ReturnsAsync(new Services.RainfallMeasurement.Results.AddMeasurementResult(mockNewId));
            var sut = new RainfallController(_mockLogger.Object, _mockMeasurementService.Object);

            // Act
            var result = await sut.AddUserReading(mockStationId, mockRequest);

            // Assert
            Assert.IsAssignableFrom<CreatedAtRouteResult>(result);
            var resultAsCreatedAtResult = (CreatedAtRouteResult)result;
            Assert.Equivalent(mockRequest, resultAsCreatedAtResult.Value);
        }
    }
}