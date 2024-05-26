using Microsoft.Extensions.Logging;
using WeatherForecast.Domain.Core;

namespace WeatherForecast.Application.Commands
{
    public abstract class BaseCommandHandler
    {
        protected readonly IWeatherContext _weatherContext;
        protected readonly ILogger _logger;

        protected BaseCommandHandler(IWeatherContext weatherContext, ILogger logger)
        {
            _weatherContext = weatherContext;
            _logger = logger;
        }
    }
}
