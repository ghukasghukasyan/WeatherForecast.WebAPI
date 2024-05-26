namespace WeatherForecast.Domain.Entities
{
    public abstract class EntityBase
    {
        public Guid Id { get; protected set; }

        public void InitializeId()
        {
            Id = Guid.NewGuid();
        }
    }
}
