using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using WeatherForecast.Domain.Core;
using WeatherForecast.Domain.Enums;
using WeatherForecast.Domain.Exceptions;
using WeatherForecast.DTO.Weathers;

namespace WeatherForecast.Application.Queries.Weathers
{
    public class GetTodaysHourlyWeatherQuery : IQuery<HourlyWeatherDTO>
    {
        public int Hour { get; set; }

        public GetTodaysHourlyWeatherQuery(int hour)
        {
            Hour = hour;
        }
    }

    public class GetTodaysWeatherQueryHandler : BaseQueryHandler, IQueryHandler<GetTodaysHourlyWeatherQuery, HourlyWeatherDTO>
    {
        public GetTodaysWeatherQueryHandler(IWeatherContext weatherContext, ILogger<GetWeathersQuery> logger) : base(weatherContext, logger)
        {

        }
        public async Task<HourlyWeatherDTO> Handle(GetTodaysHourlyWeatherQuery request, CancellationToken cancellationToken)
        {
            var todayWeather = (await _weatherContext.Weathers
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Date.Day == DateTime.Now.Day, cancellationToken));

            if (todayWeather == null)
            {
                throw new WeatherAPIException($"Todays weather have not been filled yet", ErrorCode.NotFound);
            }
            
            var hourlyWeather = await _weatherContext.HourlyWeathers
                  .AsNoTracking()
                  .Where(x => x.WeatherId == todayWeather.Id && x.Hour == request.Hour)
                  .Select(HourlyWeatherDTO.GetSelector())
                  .FirstOrDefaultAsync(cancellationToken);

            return hourlyWeather;
        }
    }
}
