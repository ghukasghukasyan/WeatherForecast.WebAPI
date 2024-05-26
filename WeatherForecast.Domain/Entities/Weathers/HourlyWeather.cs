namespace WeatherForecast.Domain.Entities.Weathers
{
    public class HourlyWeather : EntityBase
    {
        public int Hour { get; private set; }
        public double TemperatureC { get; private set; }
        public string Summary { get; private set; }
        public Guid WeatherId { get; private set; }
        public virtual Weather Weather { get; private set; }
        public HourlyWeather(Guid id, int hour, double temperatureC, string summary)
        {
            Id = id;
            Hour = hour;
            TemperatureC = temperatureC;
            Summary = summary;
        }

        public void Update(int hour, double temperatureC, string summary, Guid weatherId)
        {
            Hour = hour;
            TemperatureC = temperatureC;
            Summary = summary;
            WeatherId = weatherId;       
        }


    }
}
