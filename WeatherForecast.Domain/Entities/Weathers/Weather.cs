namespace WeatherForecast.Domain.Entities.Weathers
{
    public class Weather : EntityBase
    {
        public DateTime Date { get; private set; }
        public double AverageTemperature { get; private set; }
        public string Summary { get; private set; }
        public List<HourlyWeather> HourlyWeathers { get; private set; } = new List<HourlyWeather>();

        public Weather()
        {
                
        }
        public Weather(Guid id, DateTime date, double averageTemperature, string summary, List<HourlyWeather> hourlyWeathers)
        {
            Id = id;
            Date = date;
            AverageTemperature = averageTemperature;
            Summary = summary;
            HourlyWeathers = hourlyWeathers;
        }

        public void Update(Weather weather)
        {
            Date = weather.Date;
            AverageTemperature = weather.AverageTemperature;
            Summary = weather.Summary;
            HourlyWeathers = weather.HourlyWeathers;
        }
    }
}
