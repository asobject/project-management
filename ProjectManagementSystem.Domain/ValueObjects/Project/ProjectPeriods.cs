
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
        var errors = new List<Error>();

        if (startDate > endDate)
            errors.Add(Error.Validation("Start date must be before end date"));

        if (errors.Count != 0)
            return Error.ValidationCollection(errors);

        return new ProjectPeriods(startDate, endDate);
    }
    protected override IEnumerable<IComparable> GetEqualityComponents()
    {
        yield return StartDate;
        yield return EndDate;
    }
}
