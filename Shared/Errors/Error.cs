
using Shared.Enums;

namespace Shared.Errors;

public record Error
{
    public ErrorType Type { get; }
    public string Message { get; }
    public string? InvalidField { get; }
    public string? ErrorCode { get; }
    public Dictionary<string, object>? Metadata { get; }

    protected Error(ErrorType type, string message, string? invalidField = null,
                   string? errorCode = null, Dictionary<string, object>? metadata = null)
    {
        if (string.IsNullOrWhiteSpace(message))
            throw new ArgumentException("Message cannot be empty", nameof(message));

        Type = type;
        Message = message;
        InvalidField = invalidField;
        ErrorCode = errorCode;
        Metadata = metadata ?? [];
    }


    public static Error Validation(string message, string? invalidField = null,
                                 string? errorCode = null, Dictionary<string, object>? metadata = null)
        => new(ErrorType.Validation, message, invalidField, errorCode, metadata);

    public static Error AlreadyExists(string message, string? invalidField = null,
                                    string? errorCode = null, Dictionary<string, object>? metadata = null)
        => new(ErrorType.AlreadyExists, message, invalidField, errorCode, metadata);

    public static Error Conflict(string message, string? invalidField = null,
                               string? errorCode = null, Dictionary<string, object>? metadata = null)
        => new(ErrorType.Conflict, message, invalidField, errorCode, metadata);

    public static Error NotFound(string message, string? invalidField = null,
                               string? errorCode = null, Dictionary<string, object>? metadata = null)
        => new(ErrorType.NotFound, message, invalidField, errorCode, metadata);

    public static Error Failure(string message, string? invalidField = null,
                              string? errorCode = null, Dictionary<string, object>? metadata = null)
        => new(ErrorType.Failure, message, invalidField, errorCode, metadata);

    public static ErrorCollection ValidationCollection(params Error[] errors)
        => new(ErrorType.Validation, errors);

    public static ErrorCollection ValidationCollection(IEnumerable<Error> errors)
        => new(ErrorType.Validation, errors);

    public static ErrorCollection CreateCollection(ErrorType type, params Error[] errors)
        => new(type, errors);

    public static ErrorCollection CreateCollection(ErrorType type, IEnumerable<Error> errors)
        => new(type, errors);

    public Error WithMetadata(string key, object value)
    {
        var newMetadata = Metadata != null
            ? new Dictionary<string, object>(Metadata)
            : [];

        newMetadata[key] = value;

        return new Error(Type, Message, InvalidField, ErrorCode, newMetadata);
    }

    public Error WithErrorCode(string errorCode)
        => new(Type, Message, InvalidField, errorCode, Metadata);
}

public record ErrorCollection : Error
{
    public IReadOnlyList<Error> Errors { get; }

    public ErrorCollection(ErrorType type, IEnumerable<Error> errors)
        : base(type, "Multiple errors occurred", null, null, null)
    {
        Errors = errors.ToList().AsReadOnly();
    }

    public bool ContainsField(string fieldName)
        => Errors.Any(e => e.InvalidField == fieldName);

    public IEnumerable<Error> GetErrorsForField(string fieldName)
        => Errors.Where(e => e.InvalidField == fieldName);
}