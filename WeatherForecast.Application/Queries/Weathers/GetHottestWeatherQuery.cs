using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using WeatherForecast.Domain.Core;
using WeatherForecast.DTO.Weathers;

namespace WeatherForecast.Application.Queries.Weathers
{
    public class GetHottestWeatherQuery : IQuery<WeatherDTO>
    {
        public DateTime Date { get; set; }

        public GetHottestWeatherQuery(DateTime date)
        {
            Date = date;
        }
    }

    public class GetHottestWeatherQueryHandler : BaseQueryHandler, IQueryHandler<GetHottestWeatherQuery, WeatherDTO>
    {
        public GetHottestWeatherQueryHandler(IWeatherContext weatherContext, ILogger<GetWeathersQuery> logger) : base(weatherContext, logger)
        {

        }
        public async Task<WeatherDTO> Handle(GetHottestWeatherQuery request, CancellationToken cancellationToken)
        {
            var hottestWeather = await _weatherContext.Weathers
                  .AsNoTracking()
                  .Where(x => (x.Date - request.Date).TotalDays < 7)
                  .OrderByDescending(x => x.AverageTemperature)
                  .Select(WeatherDTO.GetSelector())
                  .FirstOrDefaultAsync(cancellationToken);

            return hottestWeather;
        }
    }
}
