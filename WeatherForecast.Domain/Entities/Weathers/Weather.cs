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
            //UpdateHourlyWeather(weather.HourlyWeathers);
        }

        private void UpdateHourlyWeather(List<HourlyWeather> hourlyWeathers)
        {
            HourlyWeathers.Clear();

            AddHourlyWeathers(hourlyWeathers);
        }

        public void AddHourlyWeathers(List<HourlyWeather> newhourlyWeathers)
        {
            if (newhourlyWeathers == null || !newhourlyWeathers.Any()) return;

            foreach (var newhourlyWeather in newhourlyWeathers)
            {
                AddHourlyWeather(newhourlyWeather);
            }
        }

        public void AddHourlyWeather(HourlyWeather hourlyWeather)
        {
            HourlyWeathers.Add(hourlyWeather);
        }
    }
}
