using WeatherForecast.Domain.Enums;

namespace WeatherForecast.Domain.Rules
{
    public interface IRule
    {
        bool IsBroken();
        string Message { get; }
        ErrorCode ErrorCode { get; }
    }
}
