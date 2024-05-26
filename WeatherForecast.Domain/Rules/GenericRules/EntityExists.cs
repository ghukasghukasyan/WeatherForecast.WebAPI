using Microsoft.EntityFrameworkCore;
using WeatherForecast.Domain.Core;
using WeatherForecast.Domain.Entities;
using WeatherForecast.Domain.Enums;

namespace WeatherForecast.Domain.Rules.GenericRules
{
    public class EntityExists<T> : BaseAsyncRule, IAsyncRule where T : EntityBase
    {
        private readonly Guid _id;
        private readonly IWeatherContext _weaterContext;

        public EntityExists(Guid id, IWeatherContext weatherContext, CancellationToken cancellationToken) : base(cancellationToken)
        {
            _id = id;
            _weaterContext = weatherContext;
        }

        public string Message => $"{typeof(T).Name} with the given id ({_id}) does not exist";

        public ErrorCode ErrorCode => ErrorCode.NotFound;

        public async Task<bool> IsBroken()
        {

            return !await _weaterContext.Set<T>().AnyAsync(x => x.Id == _id, _cancellationToken);
        }
    }
}
