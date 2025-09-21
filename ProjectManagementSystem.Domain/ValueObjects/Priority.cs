
using CSharpFunctionalExtensions;
using Shared.Errors;

namespace ProjectManagementSystem.Domain.ValueObjects;

public class Priority : ValueObject
{
    public const int MinValue = 0;
    public int Value { get; }
    private Priority() { }
    private Priority(int value)
    {
        Value = value;
    }
    public static Result<Priority,Error> Create(int value)
    {
        if (value < MinValue)
        {
            return Error.Validation($"Priority < {MinValue}");
        }
        return new Priority(value);
    }
    protected override IEnumerable<IComparable> GetEqualityComponents()
    {
        throw new NotImplementedException();
    }
}
