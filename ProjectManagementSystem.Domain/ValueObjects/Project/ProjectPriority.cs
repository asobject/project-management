
using CSharpFunctionalExtensions;
using Shared.Errors;

namespace ProjectManagementSystem.Domain.ValueObjects.Project;

public class ProjectPriority : ValueObject
{
    public const int MinValue = 0;
    public int Value { get; }
    private ProjectPriority() { }
    private ProjectPriority(int value)
    {
        Value = value;
    }
    public static Result<ProjectPriority, Error> Create(int value)
    {
        var errors = new List<Error>();

        if (value < MinValue)
            errors.Add(Error.Validation($"Priority must be at least {MinValue}",nameof(Value)));

        if (errors.Count != 0)
            return Error.ValidationCollection(errors);

        return new ProjectPriority(value);
    }
    protected override IEnumerable<IComparable> GetEqualityComponents()
    {
        throw new NotImplementedException();
    }
}
