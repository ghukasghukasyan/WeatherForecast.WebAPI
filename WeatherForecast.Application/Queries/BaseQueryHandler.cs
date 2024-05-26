using Microsoft.Extensions.Logging;
using WeatherForecast.Domain.Core;

namespace WeatherForecast.Application.Queries
{
    public abstract class BaseQueryHandler
    {
        protected readonly IWeatherContext _weatherContext;
        protected readonly ILogger _logger;

        public BaseQueryHandler(IWeatherContext weatherContext, ILogger logger)
        {
            _weatherContext = weatherContext;
            _logger = logger;
        }
    }
}
