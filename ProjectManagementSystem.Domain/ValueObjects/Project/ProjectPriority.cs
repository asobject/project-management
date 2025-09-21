
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
    public static Result<ProjectPriority,Error> Create(int value)
    {
        if (value < MinValue)
        {
            return Error.Validation($"Priority < {MinValue}");
        }
        return new ProjectPriority(value);
    }
    protected override IEnumerable<IComparable> GetEqualityComponents()
    {
        throw new NotImplementedException();
    }
}
