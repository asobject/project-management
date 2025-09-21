

using FluentValidation;
using ProjectManagementSystem.Domain.ValueObjects.Project;

namespace ProjectManagementSystem.Application.Features.Commands.Project.Create;

public class CreateProjectCommandValidator : AbstractValidator<CreateProjectCommand>
{
    public CreateProjectCommandValidator()
    {
        RuleFor(x => x.Name)
            .MaximumLength(ProjectName.MAX_LENGTH).When(x => !string.IsNullOrEmpty(x.Name));

        RuleFor(x => x.CompanyNameForCostumer)
            .MaximumLength(ProjectCompanyNames.MAX_LENGTH)
            .When(x => !string.IsNullOrEmpty(x.CompanyNameForCostumer));

        RuleFor(x => x.CompanyNameForExecutor)
            .MaximumLength(ProjectCompanyNames.MAX_LENGTH)
            .When(x => !string.IsNullOrEmpty(x.CompanyNameForExecutor));
        RuleFor(x => x.Priority)
           .NotEmpty()
           .GreaterThanOrEqualTo(ProjectPriority.MinValue);
        RuleFor(x => x.StartDate)
           .NotEmpty();
        RuleFor(x => x.EndDate)
            .NotEmpty()
            .GreaterThanOrEqualTo(x => x.StartDate);
    }
}
