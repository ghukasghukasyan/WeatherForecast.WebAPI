using System.Linq.Expressions;
using WeatherForecast.Domain.Entities.Weathers;

namespace WeatherForecast.DTO.Weathers
{
    public class HourlyWeatherDTO
    {
        public Guid Id { get; set; }
        public int Hour { get; set; }
        public double TemperatureC { get; set; }
        public string Summary { get; set; }
        //public Guid WeatherId { get; set; }

        public static Expression<Func<HourlyWeather, HourlyWeatherDTO>> GetSelector()
        {
            return hourlyWeater => new HourlyWeatherDTO()
            {
                Id = hourlyWeater.Id,
                Hour = hourlyWeater.Hour,
                Summary = hourlyWeater.Summary,
                TemperatureC = hourlyWeater.TemperatureC,
                //WeatherId = hourlyWeater.WeatherId,
            };
        }
    }
}
