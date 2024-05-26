using WeatherForecast.Domain.Entities.Weathers;

namespace WeatherForecast.Domain.Interfaces.Weathers
{
    public interface IWeaterDomainService : IWeatherForecastDomainService<Weather>
    {
        Task<List<Weather>> CreateBulkAsync(List<Weather> weathers, CancellationToken cancellationToken);
    }
}
