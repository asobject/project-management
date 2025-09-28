
using FluentValidation;

namespace ProjectManagementSystem.Application.Features.Commands.Project.Delete;

public class DeleteProjectCommandValidator:AbstractValidator<DeleteProjectCommand>
{
    public DeleteProjectCommandValidator()
    {
        RuleFor(x => x.Id)
           .NotEmpty()
           .NotEqual(Guid.Empty);
    }
}
