using WeatherForecast.Domain.Entities.Weathers;
using WeatherForecast.Domain.Enums;

namespace WeatherForecast.Domain.Rules.Weathers
{
    public class AddedWeatherShouldBeLessThanSevenDays : IRule
    {
        private readonly Weather _weahter;

        public AddedWeatherShouldBeLessThanSevenDays(Weather weather)
        {
            _weahter = weather;
        }
        public string Message => $"Added weather day should be not more than 7 days";

        public ErrorCode ErrorCode => ErrorCode.Validation;

        public bool IsBroken()
        {
            TimeSpan difference = _weahter.Date - DateTime.UtcNow;
            
            return Math.Abs(difference.TotalDays) > 7;
        }
    }
}
