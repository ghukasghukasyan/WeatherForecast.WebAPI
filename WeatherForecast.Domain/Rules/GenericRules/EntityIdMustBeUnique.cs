using Microsoft.EntityFrameworkCore;
using WeatherForecast.Domain.Core;
using WeatherForecast.Domain.Entities;
using WeatherForecast.Domain.Enums;

namespace WeatherForecast.Domain.Rules.GenericRules
{
    public class EntityIdMustBeUnique<T> : BaseAsyncRule, IAsyncRule where T : EntityBase
    {
        private readonly Guid _id;
        private readonly IWeatherContext _weatherContext;

        public EntityIdMustBeUnique(Guid id, IWeatherContext weatherContext, CancellationToken cancellationToken) : base(cancellationToken)
        {
            _id = id;
            _weatherContext = weatherContext;
        }

        public string Message => $"Resource with the given id ({_id}) already exists.";

        public ErrorCode ErrorCode => ErrorCode.Conflict;

        public async Task<bool> IsBroken()
        {
            return await _weatherContext.Set<T>().AnyAsync(x => x.Id == _id, _cancellationToken);
        }
    }
}
