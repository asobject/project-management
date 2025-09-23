
using CSharpFunctionalExtensions;
using Shared.Errors;

namespace ProjectManagementSystem.Domain.ValueObjects.Project;

public class ProjectCompanyNames : ValueObject
{
    public const int MAX_LENGTH = 20;

    public string CompanyNameForCostumer { get; } = null!;
    public string CompanyNameForExecutor { get; } = null!;
    private ProjectCompanyNames() { }
    private ProjectCompanyNames(string companyNameForCostumer, string companyNameForExecutor)
    {
        CompanyNameForExecutor = companyNameForExecutor;
        CompanyNameForCostumer = companyNameForCostumer;
    }
    public static Result<ProjectCompanyNames, Error> Create(string companyNameForCostumer, string companyNameForExecutor)
    {
        var errors = new List<Error>();

        if (string.IsNullOrWhiteSpace(companyNameForCostumer) || companyNameForCostumer.Length > MAX_LENGTH)
            errors.Add(Error.Validation("Company name for customer is invalid", nameof(CompanyNameForCostumer)));

        if (string.IsNullOrWhiteSpace(companyNameForExecutor) || companyNameForExecutor.Length > MAX_LENGTH)
            errors.Add(Error.Validation("Company name for executor is invalid", nameof(CompanyNameForExecutor)));

        if (errors.Count != 0)
            return Error.ValidationCollection(errors);

        return new ProjectCompanyNames(companyNameForCostumer, companyNameForExecutor);
    }
    protected override IEnumerable<IComparable> GetEqualityComponents()
    {
        yield return CompanyNameForCostumer;
        yield return CompanyNameForExecutor;
    }
}
