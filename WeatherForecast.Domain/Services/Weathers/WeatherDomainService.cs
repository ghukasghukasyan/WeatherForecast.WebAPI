using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using WeatherForecast.Domain.Core;
using WeatherForecast.Domain.Entities.Weathers;
using WeatherForecast.Domain.Interfaces.Weathers;
using WeatherForecast.Domain.Rules.GenericRules;
using WeatherForecast.Domain.Rules.Weathers;

namespace WeatherForecast.Domain.Services.Weathers
{
    public class WeatherDomainService : BaseDomainService, IWeaterDomainService
    {
        public WeatherDomainService(IWeatherContext weatherContext, ILogger<WeatherDomainService> logger) : base(weatherContext, logger)
        {

        }
        public async Task<Weather> CreateAsync(Weather weather, CancellationToken cancellationToken)
        {
            if (weather.Id == Guid.Empty)
            {
                weather.InitializeId();
            }
            else
            {
                await CheckRuleAsync(new EntityIdMustBeUnique<Weather>(weather.Id, _weatherContext, cancellationToken));
            }

            CheckRule(new AddedWeatherShouldBeLessThanSevenDays(weather));

            var createdWeather = new Weather(weather.Id, weather.Date, weather.AverageTemperature, weather.Summary, weather.HourlyWeathers);

            var result = await _weatherContext.Weathers.AddAsync(createdWeather, cancellationToken);

            return result.Entity;
        }

        public async Task<List<Weather>> CreateBulkAsync(List<Weather> weathers, CancellationToken cancellationToken)
        {
            List<Weather> weatherList = new List<Weather>();

            foreach (var weather in weathers)
            {
                if (weather.Id == Guid.Empty)
                {
                    weather.InitializeId();
                }
                else
                {
                    await CheckRuleAsync(new EntityIdMustBeUnique<Weather>(weather.Id, _weatherContext, cancellationToken));
                }

                CheckRule(new AddedWeatherShouldBeLessThanSevenDays(weather));

                var createdWeather = new Weather(weather.Id, weather.Date, weather.AverageTemperature, weather.Summary, weather.HourlyWeathers);

                weatherList.Add(createdWeather);
            }

            await _weatherContext.Weathers.AddRangeAsync(weatherList, cancellationToken);

            return weatherList;
        }

        public async Task<Weather> UpdateAsync(Weather weather, CancellationToken cancellationToken)
        {
            await CheckRuleAsync(new EntityExists<Weather>(weather.Id, _weatherContext, cancellationToken));

            var existingWeather = await _weatherContext.Weathers
                .Include(x => x.HourlyWeathers)
                .FirstOrDefaultAsync(x => x.Id == weather.Id, cancellationToken);

            existingWeather.Update(weather);

            var result = _weatherContext.Weathers.Update(existingWeather);

            return result.Entity;
        }
        public Task DeleteAsync(Weather weather, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
