
using CSharpFunctionalExtensions;
using MediatR;
using ProjectManagementSystem.Domain.Entities;
using Shared.Contracts.Repository;
using Shared.Errors;

namespace ProjectManagementSystem.Application.Features.Commands.Project.Create;

public class CreateProjectCommandHandler(IUnitOfWork unitOfWork, IRepository<Domain.Entities.Project, Guid> repository) : IRequestHandler<CreateProjectCommand, Result<CreateProjectResponse, Error>>
{
    public Task<Result<CreateProjectResponse, Error>> Handle(CreateProjectCommand request, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}
