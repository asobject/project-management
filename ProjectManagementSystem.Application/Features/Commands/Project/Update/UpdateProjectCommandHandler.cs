

using CSharpFunctionalExtensions;
using MediatR;
using ProjectManagementSystem.Domain.ValueObjects.Project;
using Shared.Contracts.Repository;
using Shared.Errors;

namespace ProjectManagementSystem.Application.Features.Commands.Project.Update;

public class UpdateProjectCommandHandler(IUnitOfWork unitOfWork, IRepository<Domain.Entities.Project, Guid> repository)
    : IRequestHandler<UpdateProjectWithIdCommand, Result<UpdateProjectResponse, Error>>
{
    public async Task<Result<UpdateProjectResponse, Error>> Handle(UpdateProjectWithIdCommand request, CancellationToken cancellationToken)
    {
        var project = await repository.FindAsync([request.Id], cancellationToken);
        if (project == null)
            return Error.NotFound($"Project not found with id {request.Id}");

        var errors = new List<Error>();

        if (request.Command.Name is not null)
        {
            var nameResult = ProjectName.Create(request.Command.Name);
            if (nameResult.IsFailure)
            {
                if (nameResult.Error is ErrorCollection collection)
                    errors.AddRange(collection.Errors);
                else
                    errors.Add(nameResult.Error);
            }
            else
            {
                project.UpdateName(nameResult.Value);
            }
        }
        if (request.Command.Priority is not null)
        {
            var priorityResult = ProjectPriority.Create(request.Command.Priority.Value);
            if (priorityResult.IsFailure)
            {
                if (priorityResult.Error is ErrorCollection collection)
                    errors.AddRange(collection.Errors);
                else
                    errors.Add(priorityResult.Error);
            }
            else
            {
                project.UpdatePriority(priorityResult.Value);
            }
        }

        var isCustomerNameChanged = request.Command.CompanyNameForCostumer is not null;
        var isExecutorNameChanged = request.Command.CompanyNameForExecutor is not null;

        if (isCustomerNameChanged || isExecutorNameChanged)
        {
            var customerName = isCustomerNameChanged ? request.Command.CompanyNameForCostumer! : project.CompanyNames.CompanyNameForCostumer;
            var executorName = isExecutorNameChanged ? request.Command.CompanyNameForExecutor! : project.CompanyNames.CompanyNameForExecutor;

            var companyNamesResult = ProjectCompanyNames.Create(customerName, executorName);
            if (companyNamesResult.IsFailure)
            {
                if (companyNamesResult.Error is ErrorCollection collection)
                    errors.AddRange(collection.Errors);
                else
                    errors.Add(companyNamesResult.Error);
            }
            else
            {
                project.UpdateCompanyNames(companyNamesResult.Value);
            }
        }
        var isStartDateChanged = request.Command.StartDate is not null;
        var isEndDateChanged = request.Command.EndDate is not null;

        if (isStartDateChanged || isEndDateChanged)
        {
            var startDate = isStartDateChanged ? request.Command.StartDate!.Value : project.Periods.StartDate;
            var endDate = isEndDateChanged ? request.Command.EndDate!.Value : project.Periods.EndDate;

            var periodsResult = ProjectPeriods.Create(startDate, endDate);
            if (periodsResult.IsFailure)
            {
                if (periodsResult.Error is ErrorCollection collection)
                    errors.AddRange(collection.Errors);
                else
                    errors.Add(periodsResult.Error);
            }
            else
            {
                project.UpdatePeriods(periodsResult.Value);
            }
        }
        if (errors.Count != 0)
            return Error.ValidationCollection(errors);
        await using var transaction = await unitOfWork.BeginTransactionAsync(cancellationToken);
        try
        {
            var existsProjectWithUpdateName = await repository.ExistsAsync(p => p.Name.Value == request.Command.Name && p.Id != project.Id);
            if (existsProjectWithUpdateName)
                return Error.Conflict($"Project already exists with name {request.Command.Name}", nameof(request.Command.Name));
            var result = repository.Save(project);
            await unitOfWork.CompleteAsync(cancellationToken);
            await transaction.CommitAsync(cancellationToken);
            return new UpdateProjectResponse(result);
        }
        catch
        {
            await transaction.RollbackAsync(cancellationToken);
            return Error.Failure("Error updating project");
        }
    }
}
