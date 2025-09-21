

using FluentValidation;
using ProjectManagementSystem.Domain.ValueObjects;

namespace ProjectManagementSystem.Application.Features.Commands.Project.Create;

public class CreateUserCommandValidator : AbstractValidator<CreateProjectCommand>
{
    public CreateUserCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .MaximumLength(Name.MAX_LENGTH);

        RuleFor(x => x.CompanyNameForCostumer)
            .NotEmpty()
            .MaximumLength(CompanyNames.MAX_LENGTH);

        RuleFor(x => x.CompanyNameForExecutor)
            .NotEmpty()
            .MaximumLength(CompanyNames.MAX_LENGTH);
        RuleFor(x => x.Priority)
           .NotEmpty()
           .GreaterThanOrEqualTo(Priority.MinValue);
        RuleFor(x => x.StartDate)
           .NotEmpty();
        RuleFor(x => x.EndDate)
            .NotEmpty()
            .GreaterThanOrEqualTo(x => x.StartDate);
    }
}
