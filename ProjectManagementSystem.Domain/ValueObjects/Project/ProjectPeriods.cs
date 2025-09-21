
using CSharpFunctionalExtensions;
using Shared.Errors;

namespace ProjectManagementSystem.Domain.ValueObjects.Project;

public class ProjectPeriods : ValueObject
{
    public DateTimeOffset StartDate { get; }
    public DateTimeOffset EndDate { get; }
    private ProjectPeriods() { }
    private ProjectPeriods(DateTimeOffset startDate, DateTimeOffset endDate)
    {
        StartDate = startDate;
        EndDate = endDate;
    }
    public static Result<ProjectPeriods, Error> Create(DateTimeOffset startDate, DateTimeOffset endDate)
    {
        if (startDate > endDate)
            return Error.Validation("startDate > endDate");
        return new ProjectPeriods(startDate, endDate);
    }
    protected override IEnumerable<IComparable> GetEqualityComponents()
    {
        yield return StartDate;
        yield return EndDate;
    }
}
