
using CSharpFunctionalExtensions;
using ProjectManagementSystem.Domain.ValueObjects.Project;
using Shared.Errors;

namespace ProjectManagementSystem.Domain.Entities;

public class Project : Entity<Guid>
{
    public ProjectName Name { get; private set; } = null!;
    public ProjectCompanyNames CompanyNames { get; private set; } = null!;
    public ProjectPeriods Periods { get; private set; } = null!;
    public ProjectPriority Priority { get; private set; } = null!;
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
    public UnitResult<Error> UpdateName(ProjectName newName)
    {
        Name = newName;
        return Result.Success<Error>();
    }
    public UnitResult<Error> UpdateCompanyNames(ProjectCompanyNames newCompanyNames)
    {
        CompanyNames = newCompanyNames;
        return Result.Success<Error>();
    }

    public UnitResult<Error> UpdatePeriods(ProjectPeriods newPeriods)
    {
        Periods = newPeriods;
        return Result.Success<Error>();
    }

    public UnitResult<Error> UpdatePriority(ProjectPriority newPriority)
    {
        Priority = newPriority;
        return Result.Success<Error>();
    }
}
