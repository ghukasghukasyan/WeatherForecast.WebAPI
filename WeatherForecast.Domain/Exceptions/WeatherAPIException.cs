using WeatherForecast.Domain.Enums;

namespace WeatherForecast.Domain.Exceptions
{
    public class WeatherAPIException : Exception
    {
        public ErrorCode ErrorCode { get; }

        public object AdditionalInfo { get; }

        public WeatherAPIException(string message, ErrorCode errorCode)
            : base(message)
        {
            ErrorCode = errorCode;
        }

        public WeatherAPIException(string message, ErrorCode errorCode, Exception inner)
            : base(message, inner)
        {
            ErrorCode = errorCode;
        }

        public WeatherAPIException(string message, ErrorCode errorCode, object additionalInfo)
            : base(message)
        {
            ErrorCode = errorCode;
            AdditionalInfo = additionalInfo;
        }

        public WeatherAPIException(string message, ErrorCode errorCode, object additionalInfo, Exception inner)
            : base(message, inner)
        {
            ErrorCode = errorCode;
            AdditionalInfo = additionalInfo;
        }
    }
}
