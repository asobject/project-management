using CSharpFunctionalExtensions;
using FluentValidation;
using MediatR;
using Shared.Errors;
using System.Reflection;

namespace ProjectManagementSystem.API.Behaviors;

public class ValidationBehavior<TRequest, TResponse>(IEnumerable<IValidator<TRequest>> validators)
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        if (validators.Any())
        {
            var context = new ValidationContext<TRequest>(request);

            var validationResults = await Task.WhenAll(
                validators.Select(v => v.ValidateAsync(context, cancellationToken)));

            var failures = validationResults
                .SelectMany(r => r.Errors)
                .Where(f => f != null)
                .ToList();

            if (failures.Count != 0)
            {
                var errors = failures.Select(f =>
                    Error.Validation(f.ErrorMessage, f.PropertyName)).ToList();
                var errorCollection = Error.ValidationCollection(errors);
                return CreateFailureResult(errorCollection);
            }
        }

        return await next(cancellationToken);
    }

    private static TResponse CreateFailureResult(Error error)
    {
        var responseType = typeof(TResponse);

        if (!responseType.IsGenericType || responseType.GetGenericTypeDefinition() != typeof(Result<,>))
        {
            throw new InvalidOperationException($"TResponse must be Result<TValue,Error>. Actual: {responseType.FullName}");
        }

        var valueType = responseType.GetGenericArguments()[0];
        var errorType = responseType.GetGenericArguments()[1];

        if (errorType != typeof(Error))
        {
            throw new InvalidOperationException($"TResponse must be Result<TValue,Error>. Actual error type: {errorType.FullName}");
        }

        var failureMethod = typeof(Result)
            .GetMethods(BindingFlags.Public | BindingFlags.Static)
            .FirstOrDefault(m =>
                m.Name == "Failure"
                && m.IsGenericMethodDefinition
                && m.GetGenericArguments().Length == 2 // <TValue, TError>
                && m.GetParameters().Length == 1) 
            ?? throw new InvalidOperationException("Cannot find Result.Failure<TValue,TError>(TError) method. Check CSharpFunctionalExtensions version.");
        var genericFailure = failureMethod.MakeGenericMethod(valueType, typeof(Error));
        var resultObj = genericFailure.Invoke(null, [error]);

        return (TResponse)resultObj!;
    }
}