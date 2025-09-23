
using Shared.Enums;
using Shared.Errors;

namespace Shared.Extensions;

public static class ErrorExtensions
{
    public static bool IsValidationError(this Error error)
        => error.Type == ErrorType.Validation;

    public static bool IsValidationErrorCollection(this Error error)
        => error is ErrorCollection collection && collection.Type == ErrorType.Validation;

    public static IEnumerable<Error> GetAllErrors(this Error error)
    {
        if (error is ErrorCollection collection)
            return collection.Errors;
        return [error];
    }

    //public static Dictionary<string, string[]> ToValidationProblemDetails(this Error error)
    //{
    //    var errors = new Dictionary<string, List<string>>();

    //    foreach (var err in error.GetAllErrors())
    //    {
    //        var field = err.InvalidField ?? "general";

    //        if (!errors.ContainsKey(field))
    //            errors[field] = [];

    //        errors[field].Add(err.Message);
    //    }

    //    return errors.ToDictionary(kvp => kvp.Key, kvp => kvp.Value.ToArray());
    //}

    //public static object ToApiResponse(this Error error)
    //{
    //    if (error is ErrorCollection collection)
    //    {
    //        var errorsDict = new Dictionary<string, string[]>();
    //        foreach (var err in collection.Errors)
    //        {
    //            var field = err.InvalidField ?? "general";
    //            if (errorsDict.TryGetValue(field, out string[]? value))
    //                errorsDict[field] = [.. value, err.Message];
    //            else
    //                errorsDict[field] = [err.Message];
    //        }

    //        return new { errors = errorsDict };
    //    }

    //    return new { error = error.Message, field = error.InvalidField };
    //}

    public static int GetStatusCode(this Error error)
    {
        return error.Type switch
        {
            ErrorType.Validation => 400,
            ErrorType.NotFound => 404,
            ErrorType.AlreadyExists => 409,
            ErrorType.Conflict => 409,
            ErrorType.Unauthorized => 401,
            ErrorType.Forbidden => 403,
            ErrorType.Failure => 500,
            _ => 500
        };
    }
}