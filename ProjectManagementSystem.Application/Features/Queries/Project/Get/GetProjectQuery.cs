
using CSharpFunctionalExtensions;
using MediatR;
using Shared.Errors;

namespace ProjectManagementSystem.Application.Features.Queries.Project.Get;

public record GetProjectQuery(Guid Id) : IRequest<Result<GetProjectResponse, Error>>;
