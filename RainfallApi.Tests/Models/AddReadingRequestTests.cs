using System.ComponentModel.DataAnnotations;
using RainfallApi.Models.Rainfall.Requests;

namespace RainfallApi.Tests.Models
{
    public class AddReadingRequestTests
    {
        [Fact]
        public void AddReadingRequest_NoDateMeasured_IsNotValid()
        {
            // Arrange
            var request = new AddReadingRequest()
            {
                DateMeasured = null,
                AmountMeasured = 1.0m,
            };
            var result = new List<ValidationResult>();
            var validationContext = new ValidationContext(request, null, null);

            // Act
            Validator.TryValidateObject(request, validationContext, result, true);

            // Assert
            Assert.Single(result);
            Assert.Contains(nameof(AddReadingRequest.DateMeasured), result[0].ErrorMessage);
        }

        [Fact]
        public void AddReadingRequest_NoAmountMeasured_IsNotValid()
        {
            // Arrange
            var request = new AddReadingRequest()
            {
                DateMeasured = DateTime.UtcNow,
                AmountMeasured = null
            };
            var result = new List<ValidationResult>();
            var validationContext = new ValidationContext(request, null, null);

            // Act
            Validator.TryValidateObject(request, validationContext, result, true);

            // Assert
            Assert.Single(result);
            Assert.Contains(nameof(AddReadingRequest.AmountMeasured), result[0].ErrorMessage);
        }
    }
}
