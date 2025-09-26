

using CSharpFunctionalExtensions;
using MediatR;
using Shared.Contracts.Repository;
using Shared.Errors;

namespace ProjectManagementSystem.Application.Features.Queries.Project.Get;

public class GetProjectQueryHandler(IRepository<Domain.Entities.Project, Guid> repository)
    : IRequestHandler<GetProjectQuery, Result<GetProjectResponse, Error>>
{
    public async Task<Result<GetProjectResponse, Error>> Handle(GetProjectQuery request, CancellationToken cancellationToken)
    {
        var project = await repository.FindAsync(request.Id,cancellationToken);
        if (project == null)
        {
            return Error.NotFound($"Project not found with id {request.Id}");
        }
        return new GetProjectResponse(
            Id: project.Id,
            Name: project.Name.Name,
            CompanyNameForCostumer: project.CompanyNames.CompanyNameForCostumer,
            CompanyNameForExecutor: project.CompanyNames.CompanyNameForExecutor,
            Priority: project.Priority.Priority,
            StartDate: project.Periods.StartDate,
            EndDate: project.Periods.EndDate
            );
    }
}
