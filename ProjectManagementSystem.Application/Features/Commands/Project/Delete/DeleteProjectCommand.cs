
using CSharpFunctionalExtensions;
using MediatR;
using Shared.Errors;

namespace ProjectManagementSystem.Application.Features.Commands.Project.Delete;

public record DeleteProjectCommand(Guid Id) : IRequest<Result<DeleteProjectResponse, Error>>;