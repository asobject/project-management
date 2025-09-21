
using CSharpFunctionalExtensions;
using Shared.Errors;

namespace ProjectManagementSystem.Domain.ValueObjects.Project;

public class ProjectName : ValueObject
{
    public const int MAX_LENGTH = 20;

    public string Name { get; } = null!;
    private ProjectName() { }
    private ProjectName(string value)
    {
        Name = value;
    }
    public static Result<ProjectName,Error> Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value) || value.Length > MAX_LENGTH)
            return Error.Validation(null,nameof(ProjectName));
        return new ProjectName(value);
    }
    protected override IEnumerable<IComparable> GetEqualityComponents()
    {
        yield return Name;
    }
}
