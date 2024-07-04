namespace RainfallApi.Models.Error
{
    public class ErrorDetail
    {
        public ErrorDetail(string propertyName, string message)
        {
            PropertyName = propertyName;
            Message = message;
        }

        public string PropertyName { get; }

        public string Message { get; }
    }
}
