using FluentValidation;
using MediatR;
using WeatherForecast.Application.Extensions;

namespace WeatherForecast.Application.Behaviours
{
    public interface IValidationBehaviour : IBaseRequest
    {

    }

    public class ValidationBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IValidationBehaviour, IRequest<TResponse>
    {
        private readonly IValidator<TRequest> _validator;

        public ValidationBehaviour(IValidator<TRequest> validator)
        {
            _validator = validator;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            _validator.ValidateRequest(request);

            return await next();
        }
    }
}
