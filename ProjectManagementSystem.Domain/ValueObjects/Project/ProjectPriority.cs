
using CSharpFunctionalExtensions;
using Shared.Errors;

namespace ProjectManagementSystem.Domain.ValueObjects.Project;

public class ProjectPriority : ValueObject
{
    public const int MinValue = 0;
    public int Priority { get; }
    private ProjectPriority() { }
    private ProjectPriority(int value)
    {
        Priority = value;
    }
    public static Result<ProjectPriority, Error> Create(int value)
    {
        var errors = new List<Error>();

        if (value < MinValue)
            errors.Add(Error.Validation($"Priority must be at least {MinValue}",nameof(Priority)));

        if (errors.Count != 0)
            return Error.ValidationCollection(errors);

        return new ProjectPriority(value);
    }
    protected override IEnumerable<IComparable> GetEqualityComponents()
    {
        throw new NotImplementedException();
    }
}
