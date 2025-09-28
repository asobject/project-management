
using CSharpFunctionalExtensions;
using MediatR;
using Shared.Contracts.Repository;
using Shared.Errors;

namespace ProjectManagementSystem.Application.Features.Commands.Project.Delete;

public class DeleteProjectCommandHandler(IUnitOfWork unitOfWork, IRepository<Domain.Entities.Project, Guid> repository)
    : IRequestHandler<DeleteProjectCommand, Result<DeleteProjectResponse, Error>>
{
    public async Task<Result<DeleteProjectResponse, Error>> Handle(DeleteProjectCommand request, CancellationToken cancellationToken)
    {
        var project = await repository.FindAsync([request.Id], cancellationToken);
        if (project == null)
            return Error.NotFound($"Project not found with id {request.Id}");
        var result = repository.Delete(project);
        await unitOfWork.CompleteAsync(cancellationToken);
        return new DeleteProjectResponse(result);
    }
}
