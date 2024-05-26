using Microsoft.EntityFrameworkCore;
using System.Reflection;
using WeatherForecast.Domain.Core;
using WeatherForecast.Domain.Entities.Weathers;

namespace WeatherForecast.Infrastructure.DataAccess
{
    public class WeatherContext : DbContext, IWeatherContext
    {
        public WeatherContext(DbContextOptions<WeatherContext> options) : base(options)
        {
        }

        public DbSet<Weather> Weathers { get; set; }
        public DbSet<HourlyWeather> HourlyWeathers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);
        }
    }
}
