
using CSharpFunctionalExtensions;
using ProjectManagementSystem.Domain.ValueObjects;

namespace ProjectManagementSystem.Domain.Entities;

public class Project : Entity<Guid>
{
    public Name Name { get; } = null!;
    public CompanyNames CompanyNames { get; } = null!;
    public Periods Periods { get; } = null!;
    public Priority Priority { get; } = null!;
    private Project() { }
    private Project(
        Name name,
        CompanyNames companyNames,
        Periods periods,
        Priority priority
        )
    {
        Name = name;
        CompanyNames = companyNames;
        Periods = periods;
        Priority = priority;
    }
    public static Result<Project> Create(Name name,
        CompanyNames companyNames,
        Periods periods,
        Priority priority)
    {

        var project = new Project(name, companyNames, periods, priority);
        return Result.Success(project);
    }
}
