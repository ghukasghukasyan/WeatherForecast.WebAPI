﻿using System.Linq.Expressions;
using WeatherForecast.Domain.Entities.Weathers;

namespace WeatherForecast.DTO.Weathers
{
    public class WeatherDTO
    {
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public double AverageTemperature { get; set; }
        public string Summary { get; set; }
        public List<HourlyWeatherDTO> HourlyWeathers { get; set; }

        public static Weather ToEntity(WeatherDTO weatherDTO)
        {
            return new Weather(
                weatherDTO.Id,
                weatherDTO.Date,
                weatherDTO.AverageTemperature,
                weatherDTO.Summary,
                weatherDTO.HourlyWeathers.Select(x => new HourlyWeather(x.Id, x.Hour, x.TemperatureC, x.Summary)).ToList());
        }

        public static List<Weather> ToEntities(List<WeatherDTO> weatherDTOs)
        {
            return weatherDTOs.Select(weatherDTO => new Weather(
                 weatherDTO.Id,
                 weatherDTO.Date,
                 weatherDTO.AverageTemperature,
                 weatherDTO.Summary,
                 weatherDTO.HourlyWeathers.Select(x => new HourlyWeather(x.Id, x.Hour, x.TemperatureC, x.Summary)).ToList())).ToList();
        }

        public static Expression<Func<Weather, WeatherDTO>> GetSelector()
        {
            return weather => new WeatherDTO()
            {
                Id = weather.Id,
                Date = weather.Date,
                AverageTemperature = weather.AverageTemperature,
                Summary = weather.Summary,
                HourlyWeathers = weather.HourlyWeathers.Select(x => new HourlyWeatherDTO { Id = x.Id, Hour = x.Hour, Summary = x.Summary, TemperatureC = x.TemperatureC }).ToList()
            };
        }
    }

}