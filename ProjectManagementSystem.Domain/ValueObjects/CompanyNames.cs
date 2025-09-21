
using CSharpFunctionalExtensions;
using Shared.Errors;

namespace ProjectManagementSystem.Domain.ValueObjects;

public class CompanyNames : ValueObject
{
    public const int MAX_LENGTH = 20;

    public string CompanyNameForCostumer { get; } = null!;
    public string CompanyNameForExecutor { get; } = null!;
    private CompanyNames() { }
    private CompanyNames(string companyNameForCostumer, string companyNameForExecutor)
    {
        CompanyNameForExecutor = companyNameForExecutor;
        CompanyNameForCostumer = companyNameForCostumer;
    }
    public static Result<CompanyNames, Error> Create(string companyNameForCostumer, string companyNameForExecutor)
    {
        if (string.IsNullOrWhiteSpace(companyNameForCostumer) || companyNameForCostumer.Length > MAX_LENGTH)
            return Error.Validation(null, nameof(CompanyNames.CompanyNameForCostumer));
        if (string.IsNullOrWhiteSpace(companyNameForExecutor) || companyNameForExecutor.Length > MAX_LENGTH)
            return Error.Validation(null, nameof(CompanyNames.CompanyNameForExecutor));

        return new CompanyNames(companyNameForCostumer,companyNameForExecutor);
    }
    protected override IEnumerable<IComparable> GetEqualityComponents()
    {
        yield return CompanyNameForCostumer;
        yield return CompanyNameForExecutor;
    }
}
