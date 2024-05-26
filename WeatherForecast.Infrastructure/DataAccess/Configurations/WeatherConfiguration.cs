using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WeatherForecast.Domain.Entities.Weathers;

namespace WeatherForecast.Infrastructure.DataAccess.Configurations
{
    public class WeatherConfiguration : IEntityTypeConfiguration<Weather>
    {
        public void Configure(EntityTypeBuilder<Weather> builder)
        {
            builder.HasMany(w => w.HourlyWeathers)
                  .WithOne(hw => hw.Weather)
                  .HasForeignKey(hw => hw.WeatherId);
        }
    }
}
