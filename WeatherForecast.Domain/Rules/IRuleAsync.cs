using WeatherForecast.Domain.Enums;

namespace WeatherForecast.Domain.Rules
{
    public interface IAsyncRule
    {
        Task<bool> IsBroken();
        string Message { get; }
        ErrorCode ErrorCode { get; }
    }
}
