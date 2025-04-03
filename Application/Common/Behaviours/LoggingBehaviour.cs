using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Common.Behaviours;

public class LoggingBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    private readonly ILogger<LoggingBehaviour<TRequest, TResponse>> _logger;
    private readonly IEnumerable<IValidator<TRequest>> _validators;

    public LoggingBehaviour(ILogger<LoggingBehaviour<TRequest, TResponse>> logger, IEnumerable<IValidator<TRequest>> validators)
    {
        _logger = logger;
        _validators = validators;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        if (_validators.Any())
        {
            var context = new ValidationContext<TRequest>(request);

            var validationResults = await Task.WhenAll(_validators.Select(v => v.ValidateAsync(context, cancellationToken)));

            var failures = validationResults
                            .SelectMany(result => result.Errors)
                            .Where(failure => failure != null)
                            .ToList();


            if (failures.Any())
                throw new ValidationException(failures);
        }

        try
        {
            return await next();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro no handler {HandlerName}", request.GetType().Name);
            return (TResponse)Activator.CreateInstance(typeof(TResponse), false, "Ocorreu um erro no processamento da solicitação.")!;
        }
    }
}
