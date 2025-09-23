
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
    public static Result<ProjectName, Error> Create(string value)
    {
        var errors = new List<Error>();

        if (string.IsNullOrWhiteSpace(value))
            errors.Add(Error.Validation("Name is required", nameof(ProjectName)));
        else if (value.Length > MAX_LENGTH)
            errors.Add(Error.Validation($"Name must be less than {MAX_LENGTH} characters", nameof(ProjectName)));

        if (errors.Count != 0)
            return Error.ValidationCollection(errors);

        return new ProjectName(value);
    }
    protected override IEnumerable<IComparable> GetEqualityComponents()
    {
        yield return Name;
    }
}
