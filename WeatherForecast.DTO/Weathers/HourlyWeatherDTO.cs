using System.Linq.Expressions;
using WeatherForecast.Domain.Entities.Weathers;

namespace WeatherForecast.DTO.Weathers
{
    public class HourlyWeatherDTO
    {
        public int Hour { get; set; }
        public double TemperatureC { get; set; }
        public string Summary { get; set; }

        public static Expression<Func<HourlyWeather, HourlyWeatherDTO>> GetSelector()
        {
            return hourlyWeater => new HourlyWeatherDTO()
            {
                Hour = hourlyWeater.Hour,
                Summary = hourlyWeater.Summary,
                TemperatureC = hourlyWeater.TemperatureC,
            };
        }
    }
}
