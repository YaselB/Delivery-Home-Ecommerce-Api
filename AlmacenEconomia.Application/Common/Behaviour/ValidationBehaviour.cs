using FluentValidation;
using MediatR;

namespace AlmacenEconomia.Application.Common.Behaviour;

public class ValidationBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
where TRequest : IRequest<TResponse>
{
    private readonly IEnumerable<IValidator<TRequest>> validators;
    public ValidationBehaviour(IEnumerable<IValidator<TRequest>> validators)
    {
        this.validators = validators;
    }
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        if (!validators.Any())
        {
            return await next();
        }
        var context = new ValidationContext<TRequest>(request);
        var validationFailures = new List<FluentValidation.Results.ValidationFailure>();
        foreach( var i in validators)
        {
            var result = await i.ValidateAsync(context , cancellationToken);
            if (!result.IsValid)
            {
                validationFailures.AddRange(result.Errors);
            }
            if (validationFailures.Any())
            {
                throw new ValidationException(validationFailures);
            }
        }
        return await next();
    }
}