
namespace ProjectManagementSystem.Application.Features.Queries.Project.Get;

public record GetProjectResponse(Guid Id, string Name, string CompanyNameForCostumer, string CompanyNameForExecutor, int Priority, DateTimeOffset StartDate, DateTimeOffset EndDate);