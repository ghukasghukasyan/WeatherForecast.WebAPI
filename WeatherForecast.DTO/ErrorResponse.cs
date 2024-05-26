namespace WeatherForecast.DTO
{
    public class ErrorResponse
    {
        public string Message { get; }

        public string OperationId { get; }

        public object AdditionalInfo { get; }

        public ErrorResponse(string message, string operationId = null, object additionalInfo = null)
        {
            Message = message;
            AdditionalInfo = additionalInfo;
            OperationId = operationId;
        }
    }
}
