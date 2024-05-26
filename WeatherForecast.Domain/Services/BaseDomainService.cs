using Microsoft.Extensions.Logging;
using WeatherForecast.Domain.Core;
using WeatherForecast.Domain.Exceptions;
using WeatherForecast.Domain.Rules;

namespace WeatherForecast.Domain.Services
{
    public abstract class BaseDomainService
    {
        protected readonly IWeatherContext _weatherContext;
        protected readonly ILogger _logger;

        public BaseDomainService(IWeatherContext weatherContext, ILogger logger)
        {
            _weatherContext = weatherContext;
            _logger = logger;
        }

        protected static void CheckRule(IRule rule)
        {
            if (rule.IsBroken())
            {
                throw new WeatherAPIException(rule.Message, rule.ErrorCode);
            }
        }

        protected static async Task CheckRuleAsync(IAsyncRule rule)
        {
            if (await rule.IsBroken())
            {
                throw new WeatherAPIException(rule.Message, rule.ErrorCode);
            }
        }
    }
}
