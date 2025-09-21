
using CSharpFunctionalExtensions;
using MediatR;
using Shared.Errors;

namespace ProjectManagementSystem.Application.Features.Commands.Project.Create;

public record CreateProjectCommand(string Name, string CompanyNameForCostumer, string CompanyNameForExecutor,
    int Priority, DateTimeOffset StartDate, DateTimeOffset EndDate) : IRequest<Result<CreateProjectResponse, Error>>;