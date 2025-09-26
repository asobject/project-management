
using CSharpFunctionalExtensions;
using MediatR;
using ProjectManagementSystem.Domain.ValueObjects.Project;
using Shared.Contracts.Repository;
using Shared.Errors;

namespace ProjectManagementSystem.Application.Features.Commands.Project.Create;

public class CreateProjectCommandHandler(IUnitOfWork unitOfWork, IRepository<Domain.Entities.Project, Guid> repository)
    : IRequestHandler<CreateProjectCommand, Result<CreateProjectResponse, Error>>
{
    public async Task<Result<CreateProjectResponse, Error>> Handle(
    CreateProjectCommand request,
    CancellationToken cancellationToken = default)
    {
        // Собираем все ошибки валидации
        var errors = new List<Error>();

        // Валидируем и собираем ошибки
        var nameResult = ProjectName.Create(request.Name);
        if (nameResult.IsFailure)
        {
            if (nameResult.Error is ErrorCollection collection)
                errors.AddRange(collection.Errors);
            else
                errors.Add(nameResult.Error);
        }

        var companyNamesResult = ProjectCompanyNames.Create(
            request.CompanyNameForCostumer,
            request.CompanyNameForExecutor);
        if (companyNamesResult.IsFailure)
        {
            if (companyNamesResult.Error is ErrorCollection collection)
                errors.AddRange(collection.Errors);
            else
                errors.Add(companyNamesResult.Error);
        }

        var periodsResult = ProjectPeriods.Create(request.StartDate, request.EndDate);
        if (periodsResult.IsFailure)
        {
            if (periodsResult.Error is ErrorCollection collection)
                errors.AddRange(collection.Errors);
            else
                errors.Add(periodsResult.Error);
        }

        var priorityResult = ProjectPriority.Create(request.Priority);
        if (priorityResult.IsFailure)
        {
            if (priorityResult.Error is ErrorCollection collection)
                errors.AddRange(collection.Errors);
            else
                errors.Add(priorityResult.Error);
        }

        if (errors.Count != 0)
            return Error.ValidationCollection(errors);

        if (await repository.ExistsAsync(p => p.Name.Name == request.Name))
            return Error.Conflict("Project already exists", "Name");

        var projectResult = Domain.Entities.Project.Create(
            nameResult.Value,
            companyNamesResult.Value,
            periodsResult.Value,
            priorityResult.Value);

        if (projectResult.IsFailure)
            return projectResult.Error;

        await repository.AddAsync(projectResult.Value, cancellationToken);
        await unitOfWork.CompleteAsync(cancellationToken);

        return new CreateProjectResponse(projectResult.Value.Id);
    }
}
