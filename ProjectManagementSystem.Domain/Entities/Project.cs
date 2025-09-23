
using CSharpFunctionalExtensions;
using ProjectManagementSystem.Domain.ValueObjects.Project;
using Shared.Errors;

namespace ProjectManagementSystem.Domain.Entities;

public class Project : Entity<Guid>
{
    public ProjectName Name { get; } = null!;
    public ProjectCompanyNames CompanyNames { get; } = null!;
    public ProjectPeriods Periods { get; } = null!;
    public ProjectPriority Priority { get; } = null!;
    private Project() { }
    private Project(
        ProjectName name,
        ProjectCompanyNames companyNames,
        ProjectPeriods periods,
        ProjectPriority priority
        )
    {
        Name = name;
        CompanyNames = companyNames;
        Periods = periods;
        Priority = priority;
    }
    public static Result<Project, Error> Create(
    ProjectName name,
    ProjectCompanyNames companyNames,
    ProjectPeriods periods,
    ProjectPriority priority)
    {
        var project = new Project(name, companyNames, periods, priority);
        return Result.Success<Project, Error>(project);
    }
}
