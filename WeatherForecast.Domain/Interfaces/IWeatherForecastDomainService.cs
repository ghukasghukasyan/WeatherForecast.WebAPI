namespace WeatherForecast.Domain.Interfaces
{
    public interface IWeatherForecastDomainService<T>
    {
        Task<T> CreateAsync(T entity, CancellationToken cancellationToken);
        Task<T> UpdateAsync(T entity, CancellationToken cancellationToken);
        Task DeleteAsync(T entity, CancellationToken cancellationToken);
    }
}
