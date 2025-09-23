

using FluentValidation;
using ProjectManagementSystem.Domain.ValueObjects.Project;

namespace ProjectManagementSystem.Application.Features.Commands.Project.Create;

public class CreateProjectCommandValidator : AbstractValidator<CreateProjectCommand>
{
    public CreateProjectCommandValidator()
    {
        RuleFor(x => x.Name)
     .NotEmpty()
     .MaximumLength(ProjectName.MAX_LENGTH);

        RuleFor(x => x.CompanyNameForCostumer)
            .NotEmpty()
            .MaximumLength(ProjectCompanyNames.MAX_LENGTH);

        RuleFor(x => x.CompanyNameForExecutor)
            .NotEmpty()
            .MaximumLength(ProjectCompanyNames.MAX_LENGTH);
        RuleFor(x => x.Priority)
           .GreaterThanOrEqualTo(ProjectPriority.MinValue);
        RuleFor(x => x.StartDate)
           .NotEmpty();
        RuleFor(x => x.EndDate)
            .NotEmpty()
            .GreaterThanOrEqualTo(x => x.StartDate);
    }
}
