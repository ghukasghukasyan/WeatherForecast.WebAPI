using FluentValidation;
using WeatherForecast.Application.Commands.Weathers;
using WeatherForecast.DTO.Weathers;

namespace WeatherForecast.Application.Validators.Weathers
{
    public class CreateWeatherCommandValiator : AbstractValidator<CreateWeatherCommand>
    {
        public CreateWeatherCommandValiator()
        {
            RuleFor(command => command.Weather.Id).NotEmpty().WithMessage("Id filed is required");
            
            RuleFor(command => command.Weather.Date).NotEmpty().WithMessage("The Date field is required");
            
            RuleFor(command => command.Weather.HourlyWeathers).NotEmpty().WithMessage("The hourly weather is required");
            
            RuleForEach(command => command.Weather.HourlyWeathers).SetValidator(new HourlyWeatherValidator());
        }
    }


    public class HourlyWeatherValidator : AbstractValidator<HourlyWeatherDTO>
    {
        public HourlyWeatherValidator()
        {
            RuleFor(hw => hw.Hour).NotNull().WithMessage("The hour filed is required").InclusiveBetween(0, 23)
                .WithMessage("Hour must be between 0 and 23.");
        }

    }

}
