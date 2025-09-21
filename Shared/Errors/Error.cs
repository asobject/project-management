
using Shared.Enums;

namespace Shared.Errors;

public record Error
{
    public ErrorType Type { get; }
    public string Message { get; } = null!;
    public string? InvalidField { get; } = null;
    private Error(ErrorType type, string message, string? invalidField = null)
    {
        Type = type;
        Message = message;
        InvalidField = invalidField;
    }
    public static Error Validation(string? message, string? invalidField = null)
    {
        message ??= $"Validation error {invalidField}";
        return new Error(ErrorType.Validation, message, invalidField);
    }
    public static Error AlreadyExists(string? message, string? invalidField = null)
    {
        message ??= $"AlreadyExists error {invalidField}";
        return new Error(ErrorType.AlreadyExists, message, invalidField);
    }
    public static Error Conflict(string? message, string? invalidField = null)
    {
        message ??= $"Conflict error {invalidField}";
        return new Error(ErrorType.Conflict, message, invalidField);
    }
    public static Error NotFound(string? message, string? invalidField = null)
    {
        message ??= $"NotFound error {invalidField}";
        return new Error(ErrorType.NotFound, message, invalidField);
    }
    public static Error Failure(string? message, string? invalidField = null)
    {
        message ??= $"Failure error {invalidField}";
        return new Error(ErrorType.Failure, message, invalidField);
    }
}