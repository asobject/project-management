using CSharpFunctionalExtensions;
using CSharpFunctionalExtensions.ValueTasks;
using Microsoft.AspNetCore.Mvc;
using Shared.Enums;
using Shared.Errors;
using Shared.Extensions;

namespace ProjectManagementSystem.API.Extensions;

public static class ResultExtensions
{
    public static ObjectResult ToObjectResult<TValue>(this Result<TValue, Error> result)
    {
        if (result.IsSuccess)
        {
            return new OkObjectResult(result.Value);
        }

        var error = result.Error;

        if (error is ErrorCollection collection)
        {
            var errorsDict = new Dictionary<string, string[]>();
            foreach (var err in collection.Errors)
            {
                var field = err.InvalidField ?? "general";
                if (errorsDict.TryGetValue(field, out string[]? value))
                    errorsDict[field] = [.. value, err.Message];
                else
                    errorsDict[field] = [err.Message];
            }

            var dominantErrorType = GetDominantErrorType(collection.Errors);
            var statusCode = dominantErrorType.ToStatusCode();

            return new ObjectResult(new ValidationProblemDetails(errorsDict)
            {
                Title = GetErrorTitle(dominantErrorType),
                Status = statusCode
            })
            {
                StatusCode = statusCode
            };
        }

        return CreateErrorResult(error);
    }
    private static ObjectResult CreateErrorResult(Error error)
    {
        var statusCode = error.Type.ToStatusCode();

        return error.Type switch
        {
            ErrorType.Validation => new BadRequestObjectResult(new { error.Message, error.InvalidField }),
            ErrorType.AlreadyExists => new ConflictObjectResult(new { error.Message }),
            ErrorType.NotFound => new NotFoundObjectResult(new { error.Message }),
            ErrorType.Conflict => new ConflictObjectResult(new { error.Message }),
            ErrorType.Unauthorized => new UnauthorizedObjectResult(new { error.Message }),
            ErrorType.Forbidden => new ObjectResult(new { error.Message }) { StatusCode = statusCode },
            _ => new ObjectResult(new { error.Message }) { StatusCode = statusCode }
        };
    }
    public static ObjectResult ToActionResult(this Error error)
    {
        var statusCode = error.GetStatusCode();

        if (error is ErrorCollection collection)
        {
            return collection.ToActionResult();
        }

        return error.Type switch
        {
            ErrorType.Validation => new BadRequestObjectResult(new { error.Message, error.InvalidField }),
            ErrorType.AlreadyExists => new ConflictObjectResult(new { error.Message }),
            ErrorType.NotFound => new NotFoundObjectResult(new { error.Message }),
            ErrorType.Conflict => new ConflictObjectResult(new { error.Message }),
            ErrorType.Unauthorized => new UnauthorizedObjectResult(new { error.Message }),
            ErrorType.Forbidden => new ObjectResult(new { error.Message }) { StatusCode = 403 },
            _ => new ObjectResult(new { error.Message }) { StatusCode = statusCode }
        };
    }

    private static ObjectResult ToActionResult(this ErrorCollection collection)
    {
        var dominantErrorType = GetDominantErrorType(collection.Errors);
        var statusCode = dominantErrorType.ToStatusCode();

        if (dominantErrorType == ErrorType.Validation)
        {
            var errorsDict = collection.Errors
                .GroupBy(e => e.InvalidField ?? "general")
                .ToDictionary(g => g.Key, g => g.Select(e => e.Message).ToArray());

            return new ObjectResult(new ValidationProblemDetails(errorsDict)
            {
                Title = GetErrorTitle(dominantErrorType),
                Status = statusCode
            })
            {
                StatusCode = statusCode
            };
        }

        // Для не-валидационных коллекций возвращаем массив ошибок
        var errors = collection.Errors.Select(e => new { e.Message, e.InvalidField }).ToArray();
        return new ObjectResult(new { errors, message = "Multiple errors occurred" })
        {
            StatusCode = statusCode
        };
    }

    private static ErrorType GetDominantErrorType(IEnumerable<Error> errors)
    {
        var priorityOrder = new[]
        {
            ErrorType.Unauthorized,
            ErrorType.Forbidden,
            ErrorType.NotFound,
            ErrorType.AlreadyExists,
            ErrorType.Conflict,
            ErrorType.Validation,
            ErrorType.Failure
        };

        foreach (var errorType in priorityOrder)
        {
            if (errors.Any(e => e.Type == errorType))
                return errorType;
        }

        return ErrorType.Validation;
    }

    private static int ToStatusCode(this ErrorType errorType)
    {
        return errorType switch
        {
            ErrorType.Validation => 400,
            ErrorType.NotFound => 404,
            ErrorType.AlreadyExists => 409,
            ErrorType.Conflict => 409,
            ErrorType.Unauthorized => 401,
            ErrorType.Forbidden => 403,
            _ => 500
        };
    }

    private static string GetErrorTitle(ErrorType errorType)
    {
        return errorType switch
        {
            ErrorType.Validation => "Validation error",
            ErrorType.NotFound => "Not found",
            ErrorType.AlreadyExists => "Already exists",
            ErrorType.Conflict => "Conflict",
            ErrorType.Unauthorized => "Unauthorized",
            ErrorType.Forbidden => "Forbidden",
            _ => "Internal server error"
        };
    }
}