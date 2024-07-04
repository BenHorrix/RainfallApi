namespace RainfallApi.Models.Error
{
    public class Error
    {
        public Error(ErrorDetail[] detail, string message)
        {
            Detail = detail;
            Message = message;
        }

        public string Message { get; }

        public ErrorDetail[] Detail { get; }
    }
}
