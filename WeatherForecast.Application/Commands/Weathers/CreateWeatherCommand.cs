using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using WeatherForecast.Application.Behaviours;
using WeatherForecast.Domain.Core;
using WeatherForecast.Domain.Interfaces.Weathers;
using WeatherForecast.DTO.Weathers;

namespace WeatherForecast.Application.Commands.Weathers
{
    public class CreateWeatherCommand : ICommand<WeatherDTO>, IValidationBehaviour
    {
        public WeatherDTO Weather { get; set; }
        public CreateWeatherCommand(WeatherDTO weather)
        {
            Weather = weather;
        }
    }

    public class CreateWeatherCommandHandler : BaseCommandHandler, ICommandHandler<CreateWeatherCommand, WeatherDTO>
    {
        private readonly IWeaterDomainService _weatherDomainService;

        public CreateWeatherCommandHandler(IWeaterDomainService weaterDomainService, IWeatherContext weatherContext, ILogger<CreateWeatherCommand> logger) : base(weatherContext, logger)
        {
            _weatherDomainService = weaterDomainService;
        }
        public async Task<WeatherDTO> Handle(CreateWeatherCommand request, CancellationToken cancellationToken)
        {
            var weather = WeatherDTO.ToEntity(request.Weather);

            var createdWeather = await _weatherDomainService.CreateAsync(weather, cancellationToken);

            await _weatherContext.SaveChangesAsync(cancellationToken);

            var result = await _weatherContext.Weathers
                .AsNoTracking()
                .Select(WeatherDTO.GetSelector())
                .FirstOrDefaultAsync(x => x.Id == createdWeather.Id, cancellationToken);

            return result;
        }
    }
}
