using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using WeatherForecast.Domain.Core;
using WeatherForecast.DTO.Weathers;

namespace WeatherForecast.Application.Queries.Weathers
{
    public class GetWeathersQuery : IQuery<List<WeatherDTO>>
    {
    }

    public class GetWeathersQueryHandler : BaseQueryHandler, IQueryHandler<GetWeathersQuery, List<WeatherDTO>>
    {
        public GetWeathersQueryHandler(IWeatherContext weatherContext, ILogger<GetWeathersQuery> logger) : base(weatherContext, logger)
        {

        }
        public async Task<List<WeatherDTO>> Handle(GetWeathersQuery request, CancellationToken cancellationToken)
        {
            var weathers = await _weatherContext.Weathers
                 .AsNoTracking()
                 .Select(WeatherDTO.GetSelector())
                 .ToListAsync(cancellationToken);

            return weathers;
        }
    }
}
