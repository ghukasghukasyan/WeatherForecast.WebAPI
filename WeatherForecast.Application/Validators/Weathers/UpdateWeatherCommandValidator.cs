using FluentValidation;
using WeatherForecast.Application.Commands.Weathers;

namespace WeatherForecast.Application.Validators.Weathers
{
    public class UpdateWeatherCommandValidator : AbstractValidator<UpdateWeatherCommand>
    {
        public UpdateWeatherCommandValidator()
        {
            RuleFor(command => command.Weather.Id).NotEmpty().WithMessage("Id filed is required");

            RuleFor(command => command.Weather.Date).NotEmpty().WithMessage("The Date field is required");

            RuleFor(command => command.Weather.HourlyWeathers).NotEmpty().WithMessage("The hourly weather is required");

            RuleForEach(command => command.Weather.HourlyWeathers).SetValidator(new HourlyWeatherValidator());
        }
    }
}
