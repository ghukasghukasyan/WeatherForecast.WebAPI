using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using WeatherForecast.Domain.Core;
using WeatherForecast.Domain.Interfaces.Weathers;
using WeatherForecast.DTO.Weathers;

namespace WeatherForecast.Application.Commands.Weathers
{
    public class CreateBulkWeatherCommand : ICommand<List<WeatherDTO>>
    {
        public List<WeatherDTO> Weathers { get; set; }
        public CreateBulkWeatherCommand(List<WeatherDTO> weathers)
        {
            Weathers = weathers;
        }
    }

    public class CreateBulkWeatherCommandHandler : BaseCommandHandler, ICommandHandler<CreateBulkWeatherCommand, List<WeatherDTO>>
    {
        private readonly IWeaterDomainService _weatherDomainService;
        public CreateBulkWeatherCommandHandler(IWeaterDomainService weaterDomainService, IWeatherContext weatherContext, ILogger<CreateBulkWeatherCommand> logger) : base(weatherContext, logger)
        {
            _weatherDomainService = weaterDomainService;
        }
        public async Task<List<WeatherDTO>> Handle(CreateBulkWeatherCommand request, CancellationToken cancellationToken)
        {
            var weathers = WeatherDTO.ToEntities(request.Weathers);

            var createdWeathers = await _weatherDomainService.CreateBulkAsync(weathers, cancellationToken);

            await _weatherContext.SaveChangesAsync(cancellationToken);

            var result = await _weatherContext.Weathers
                .AsNoTracking()
                .Select(WeatherDTO.GetSelector())
                .Where(x => createdWeathers.Select(y => y.Id).Contains(x.Id)).ToListAsync(cancellationToken);

            return result;
        }
    }
}
