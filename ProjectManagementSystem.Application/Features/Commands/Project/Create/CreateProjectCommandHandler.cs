
using CSharpFunctionalExtensions;
using MediatR;
using ProjectManagementSystem.Domain.ValueObjects.Project;
using Shared.Contracts.Repository;
using Shared.Errors;

namespace ProjectManagementSystem.Application.Features.Commands.Project.Create;

public class CreateProjectCommandHandler(IUnitOfWork unitOfWork, IRepository<Domain.Entities.Project, Guid> repository) : IRequestHandler<CreateProjectCommand, Result<CreateProjectResponse, Error>>
{
    public async Task<Result<CreateProjectResponse, Error>> Handle(CreateProjectCommand request, CancellationToken cancellationToken = default)
    {
        var name = ProjectName.Create(request.Name).Value;
        var companyNames = ProjectCompanyNames.Create(request.CompanyNameForCostumer, request.CompanyNameForExecutor).Value;
        var periods = ProjectPeriods.Create(request.StartDate, request.EndDate).Value;
        var priority = ProjectPriority.Create(request.Priority).Value;
        if (await repository.ExistsAsync(p => p.Name.Name == request.Name))
            return Error.AlreadyExists("Project already exists");
        var project = Domain.Entities.Project.Create(name, companyNames, periods, priority).Value;
        await repository.AddAsync(project, cancellationToken);
        await unitOfWork.CompleteAsync(cancellationToken);
        return new CreateProjectResponse(project.Id);
    }
}
