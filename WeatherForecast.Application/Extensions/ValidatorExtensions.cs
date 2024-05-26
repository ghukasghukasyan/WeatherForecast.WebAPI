using FluentValidation;
using FluentValidation.Results;
using MediatR;
using WeatherForecast.Domain.Enums;
using WeatherForecast.Domain.Exceptions;

namespace WeatherForecast.Application.Extensions
{
    public static class ValidatorExtensions
    {
        public static void ValidateRequest<TCommand>(this IValidator<TCommand> validator, TCommand command) where TCommand : IBaseRequest
        {
            if (command == null)
                throw new ArgumentNullException(nameof(command));

            var validationResult = validator.Validate(command);

            if (!validationResult.IsValid)
                throw new WeatherAPIException(BuildErrorMesage(validationResult.Errors), ErrorCode.Validation);
        }

        private static string BuildErrorMesage(IEnumerable<ValidationFailure> errors)
        {
            return errors.FirstOrDefault().ToString();
        }
    }
}
