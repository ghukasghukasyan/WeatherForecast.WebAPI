namespace WeatherForecast.DTO.Weathers
{
    public class GetWeatherDTO
    {
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public double AverageTemperature { get; set; }
        public string Summary { get; set; }
    }
}
