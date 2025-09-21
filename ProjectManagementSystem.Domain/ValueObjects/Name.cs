
using CSharpFunctionalExtensions;
using Shared.Errors;

namespace ProjectManagementSystem.Domain.ValueObjects;

public class Name : ValueObject
{
    public const int MAX_LENGTH = 20;

    public string Value { get; } = null!;
    private Name() { }
    private Name(string value)
    {
        Value = value;
    }
    public static Result<Name,Error> Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value) || value.Length > MAX_LENGTH)
            return Error.Validation(null,nameof(Name));
        return new Name(value);
    }
    protected override IEnumerable<IComparable> GetEqualityComponents()
    {
        yield return Value;
    }
}
