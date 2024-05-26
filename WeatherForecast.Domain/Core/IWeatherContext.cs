using Microsoft.EntityFrameworkCore;
using WeatherForecast.Domain.Entities.Weathers;

namespace WeatherForecast.Domain.Core
{
    public interface IWeatherContext
    {
        DbSet<Weather> Weathers { get; set; }
        DbSet<HourlyWeather> HourlyWeathers { get; set; }

        DbSet<TEntity> Set<TEntity>() where TEntity : class;
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
