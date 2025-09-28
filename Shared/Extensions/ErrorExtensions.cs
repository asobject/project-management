
using Shared.Enums;
using Shared.Errors;

namespace Shared.Extensions;

public static class ErrorExtensions
{
    public static bool IsValidationError(this Error error)
        => error.Type == ErrorType.Validation;

    public static bool IsValidationErrorCollection(this Error error)
        => error is ErrorCollection { Type: ErrorType.Validation };

    public static IEnumerable<Error> GetAllErrors(this Error error)
    {
        if (error is ErrorCollection collection)
            return collection.Errors;
        return [error];
    }

    public static int GetStatusCode(this Error error)
    {
        return error.Type switch
        {
            ErrorType.Validation => 400,
            ErrorType.NotFound => 404,
            ErrorType.Conflict => 409,
            ErrorType.Unauthorized => 401,
            ErrorType.Forbidden => 403,
            ErrorType.Failure => 500,
            _ => 500
        };
    }
}