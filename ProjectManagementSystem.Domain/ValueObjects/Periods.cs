
using CSharpFunctionalExtensions;
using Shared.Errors;

namespace ProjectManagementSystem.Domain.ValueObjects;

public class Periods : ValueObject
{
    public DateTimeOffset StartDate { get; }
    public DateTimeOffset EndDate { get; }
    private Periods() { }
    private Periods(DateTimeOffset startDate, DateTimeOffset endDate)
    {
        StartDate = startDate;
        EndDate = endDate;
    }
    public static Result<Periods, Error> Create(DateTimeOffset startDate, DateTimeOffset endDate)
    {
        if (startDate > endDate)
            return Error.Validation("startDate > endDate");
        return new Periods(startDate, endDate);
    }
    protected override IEnumerable<IComparable> GetEqualityComponents()
    {
        yield return StartDate;
        yield return EndDate;
    }
}
