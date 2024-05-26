using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using WeatherForecast.Application.Behaviours;
using WeatherForecast.Domain.Core;
using WeatherForecast.Domain.Interfaces.Weathers;
using WeatherForecast.DTO.Weathers;

namespace WeatherForecast.Application.Commands.Weathers
{
    public class UpdateWeatherCommand : ICommand<WeatherDTO>, IValidationBehaviour
    {
        public WeatherDTO Weather { get; set; }
        public UpdateWeatherCommand(WeatherDTO weather)
        {
            Weather = weather;
        }
    }

    public class UpdateWeatherCommandHandler : BaseCommandHandler, ICommandHandler<UpdateWeatherCommand, WeatherDTO>
    {
        private readonly IWeaterDomainService _weatherDomainService;

        public UpdateWeatherCommandHandler(IWeaterDomainService weaterDomainService, IWeatherContext weatherContext, ILogger<CreateWeatherCommand> logger) : base(weatherContext, logger)
        {
            _weatherDomainService = weaterDomainService;
        }
        public async Task<WeatherDTO> Handle(UpdateWeatherCommand request, CancellationToken cancellationToken)
        {
            var weather = WeatherDTO.ToEntity(request.Weather);

            var updatedWeather = await _weatherDomainService.UpdateAsync(weather, cancellationToken);

            await _weatherContext.SaveChangesAsync(cancellationToken);

            var result = await _weatherContext.Weathers
                .AsNoTracking()
                .Select(WeatherDTO.GetSelector())
                .FirstOrDefaultAsync(x => x.Id == updatedWeather.Id, cancellationToken);

            return result;
        }
    }
}
