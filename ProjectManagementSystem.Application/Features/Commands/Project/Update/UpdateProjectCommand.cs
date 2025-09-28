

using CSharpFunctionalExtensions;
using MediatR;
using Shared.Errors;

namespace ProjectManagementSystem.Application.Features.Commands.Project.Update;

public record UpdateProjectCommand(string? Name, string? CompanyNameForCostumer, string? CompanyNameForExecutor,
    int? Priority, DateTimeOffset? StartDate, DateTimeOffset? EndDate) : IRequest<Result<UpdateProjectResponse, Error>>;
public record UpdateProjectWithIdCommand(Guid Id,UpdateProjectCommand Command) : IRequest<Result<UpdateProjectResponse, Error>>;
